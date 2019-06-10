using FluentValidator;
using ProAgil.Domain.ProAgilContext.Commands.Inputs;
using ProAgil.Domain.ProAgilContext.Commands.Outputs;
using ProAgil.Domain.ProAgilContext.Entities;
using ProAgil.Domain.ProAgilContext.Repositories.Interfaces;
using ProAgil.Domain.ValueObjects;
using ProAgil.Shared.Commands;

namespace ProAgil.Domain.ProAgilContext.Handlers
{
     public class PalestranteHandler : Notifiable,
     ICommandHandler<CriaPalestranteCommand>
    {
        private readonly IPalestranteRepository _palestranteRepository; 

        public PalestranteHandler(IPalestranteRepository palestranteRepository){
                _palestranteRepository = palestranteRepository;
        }
        
        public ICommandResult Handle(CriaPalestranteCommand command)
        {
                var email = new Email(command.Email);
                
                var palestrante = new Palestrante(command.Nome,command.MiniCurriculo,command.ImgUrl
                ,command.Telefone,email);
            
                AddNotifications(email.Notifications);
                AddNotifications(palestrante.Notifications);

            if(Invalid){
                 return  new CriaPalestranteCommandResult(false,"Campos enviados com erro", Notifications);
            }

            _palestranteRepository.Create(palestrante);
            _palestranteRepository.SaveChanges();
            return new CriaPalestranteCommandResult(true,"Bem vindo",new {
                        Id = palestrante.Id,
                        Email = palestrante.Email
            });
            
        }
    }
}