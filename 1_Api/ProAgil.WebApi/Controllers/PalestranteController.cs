using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain.ProAgilContext.Commands.Inputs;
using ProAgil.Domain.ProAgilContext.Handlers;
using ProAgil.Domain.ProAgilContext.Repositories.Interfaces;

namespace ProAgil.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        private readonly PalestranteHandler _palestranteHandler;
        private readonly IPalestranteRepository _palestranteRepository;
        public PalestranteController(PalestranteHandler palestranteHandler,
                    IPalestranteRepository palestranteRepository){
                _palestranteHandler = palestranteHandler;
                _palestranteRepository =palestranteRepository;
        }  
        
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CriaPalestranteCommand command){
            try{
                return Ok(_palestranteHandler.Handle(command));
            }catch(System.Exception){
                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha interna");
            }
        }
    }
}