using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProAgil.Domain.Identity;
using ProAgil.Domain.ProAgilContext.Adapter;
using ProAgil.Domain.ProAgilContext.Commands.Inputs;
using ProAgil.Domain.ProAgilContext.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> GetUser(AthenticationUserCommand command)
        {
            return Ok(command);
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]AthenticationUserCommand command)
        {
            try
            {
                var user = UserAdapter.CommandToDomain(command, "proAgil");
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

        private async Task<string> GenerateJWTonken(User appUser)
        {
            throw new NotImplementedException();
        }
    }
}
