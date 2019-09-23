using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain.ProAgilContext.Adapter;
using ProAgil.Domain.ProAgilContext.Commands.Inputs;
using ProAgil.Domain.ProAgilContext.Handlers;
using ProAgil.Domain.ProAgilContext.Repositories.Interfaces;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace ProAgil.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly EventoHandler _eventoHandler;
        private readonly IEventoRepository _eventoRepository;
        public EventoController(EventoHandler eventoHandler,
        IEventoRepository eventoRepository)
        {
            _eventoHandler = eventoHandler;
            _eventoRepository = eventoRepository;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = EventoAdapter.DomainToViewModel(_eventoRepository.GetAll().OrderBy(x => x.Tema));
                return Ok(results);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> upload()
        {
            try
            {
                //A principio trabalhar em cima desse formato de upload, mas posteriormente salvar em Base64
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources","images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(),folderName);
                if(file.Length > 0){
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave,filename.ToString().Replace("\" "," ").Trim());
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = EventoAdapter.DomainToViewModel(await _eventoRepository.GetAllEventosAsyncById(id, false));
            return Ok(result);
        }

        [HttpGet("{identifyer}")]
        public async Task<IActionResult> Get(string identifyer)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdentifier(identifyer);
                var result = EventoAdapter.DomainToViewModel(evento);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CriaEventoCommand command)
        {
            try
            {
                return Ok(_eventoHandler.Handle(command));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> Put([FromBody]EditaEventoCommand command)
        {
            try
            {
                return Ok(_eventoHandler.Handle(command));

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{identifyer}")]
        public async Task<IActionResult> Delete(string identifyer)
        {
            try
            {

                var evento = await _eventoRepository.GetEventoByIdentifier(identifyer);
                if (evento != null)
                {
                    _eventoRepository.Delete(evento);
                    return Ok(await Task.FromResult(true));
                }
                return Ok(await Task.FromResult(false));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}