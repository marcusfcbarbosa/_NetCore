using System.Threading.Tasks;
using FluentValidator;
using ProAgil.Domain.ProAgilContext.Commands.Inputs;
using ProAgil.Domain.ProAgilContext.Commands.Outputs;
using ProAgil.Domain.ProAgilContext.Entities;
using ProAgil.Domain.ProAgilContext.Repositories.Interfaces;
using ProAgil.Domain.ValueObjects;
using ProAgil.Shared.Commands;

namespace ProAgil.Domain.ProAgilContext.Handlers
{
    public class EventoHandler : Notifiable,
     ICommandHandler<CriaEventoCommand>
    {
        private readonly IEventoRepository _eventoRepository;
        public EventoHandler(IEventoRepository eventoRepository){
            _eventoRepository = eventoRepository;
        }

        public ICommandResult Handle(CriaEventoCommand command)
        {
            var email = new Email(command.Email);
                
            var evento = new Evento(command.Local,command.DataEvento,
            command.Tema,command.QtdPessoas,command.ImgUrl,command.Telefone,email);

            AddNotifications(email.Notifications);
            AddNotifications(evento.Notifications);
            
            if(Invalid){
                return  new CriaEventoCommandResult(false,"Campos enviados com erro", Notifications);
            }

            _eventoRepository.Add(evento);
            _eventoRepository.SaveChangesAsync();
            return new CriaEventoCommandResult(true,"Bem vindo",new {
                        Id = evento.Id,
                        Email = evento.Email
            });
        }
    }
}