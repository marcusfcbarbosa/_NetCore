using System;
using System.Linq;
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
     ICommandHandler<CriaEventoCommand>,
     ICommandHandlerAsync<EditaEventoCommand>
    {
        private readonly IEventoRepository _eventoRepository;
        public EventoHandler(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public ICommandResult Handle(CriaEventoCommand command)
        {
            var email = new Email(command.Email);

            var evento = new Evento(command.Local, command.DataEvento,
            command.Tema, command.QtdPessoas, command.ImgUrl, command.Telefone, email);

            AddNotifications(email.Notifications);
            AddNotifications(evento.Notifications);

            if (Invalid)
            {
                return new CriaEventoCommandResult(false, "Campos enviados com erro", Notifications);
            }

            _eventoRepository.Create(evento);
            _eventoRepository.SaveChanges();
            return new CriaEventoCommandResult(true, "Bem vindo", new
            {
                Id = evento.Id,
                Email = evento.Email
            });
        }



        public async Task<ICommandResult> Handle(EditaEventoCommand command)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdentifier(command.identifyer);
                if (command.Valid())
                {
                    evento.EditaEvento(command.Local, command.DataEvento, command.Tema, command.QtdPessoas, command.ImgUrl, command.Telefone, command.Email);
                    _eventoRepository.Edit(evento);
                    return new CriaEventoCommandResult(true, "Editado", new
                    {
                        Id = evento.identifyer,
                        Tema = evento
                    });
                }
                return new CriaEventoCommandResult(false, "Erro", new
                {
                    Erros = command.Notifications
                });
            }
            catch (Exception ex)
            {
                return new CriaEventoCommandResult(false, "Erro", new
                {
                    mensagem = ex.Message
                });
            }
        }
    }
}