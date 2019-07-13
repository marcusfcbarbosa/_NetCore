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
        public IActionResult Get()
        {
            try
            {
                var results =  _eventoRepository.GetAll();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _eventoRepository.GetAllEventosAsyncById(id,false);
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CriaEventoCommand command){
            try{
                return Ok(_eventoHandler.Handle(command));
            }catch(Exception ex){
                    return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> Put([FromBody]EditaEventoCommand command){
            try{
                return Ok(_eventoHandler.Handle(command));

            }catch(Exception ex){
                    return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}