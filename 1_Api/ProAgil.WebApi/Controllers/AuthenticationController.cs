using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProAgil.Domain.Identity;
using ProAgil.Domain.ProAgilContext.Adapter;
using ProAgil.Domain.ProAgilContext.Commands.Inputs;
using ProAgil.Domain.ProAgilContext.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProAgil.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        public AuthenticationController(IConfiguration config,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            return Ok(new AthenticationUserCommand());
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]AthenticationUserCommand command)
        {
            try
            {
                var user = UserAdapter.CommandToDomain(command, command.Context);
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    return Created("GetUser", command);
                }
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userLogin.UserName);
                var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);
                if (result.Succeeded)
                {
                    var appUser = _userManager.Users
                        .FirstOrDefault(u => u.NormalizedUserName == userLogin.UserName.ToUpper());
                    var userToReturn = UserAdapter.DomainToDto(appUser);

                    return Ok(new
                    {
                        Token = GenerateJWTonken(appUser).Result,
                        user = userToReturn
                    });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha de Login {ex.Message}");
            }
        }

    //Proa alguma razao o swagger nao esta habilitando o campo de Header de autenticação
        private async Task<string> GenerateJWTonken(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }
            var key =  new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
