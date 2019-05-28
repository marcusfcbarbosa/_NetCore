using System;
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
    public class EventoController: ControllerBase
    {
        private readonly EventoHandler _eventoHandler;
        private readonly IEventoRepository _eventoRepository;
        public EventoController(EventoHandler eventoHandler,
        IEventoRepository eventoRepository){
                _eventoHandler = eventoHandler;
                _eventoRepository = eventoRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                 var results = await _eventoRepository.GetAllEventosAsync(false);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha interna");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _eventoRepository.GetAllEventosAsyncById(id,false);
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CriaEventoCommand command){
            try{
                return Ok(_eventoHandler.Handle(command));
            }catch(System.Exception){
                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha interna");
            }
        }
    }
}