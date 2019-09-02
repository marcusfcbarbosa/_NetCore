using System;
using FluentValidator;
using FluentValidator.Validation;
using ProAgil.Shared.Commands;

namespace ProAgil.Domain.ProAgilContext.Commands.Inputs
{
    public class DeletaEventoCommand : Notifiable, ICommand
    {
       public string identifyer { get; set; }
        public void Validate()
        {
            AddNotifications(new ValidationContract()
                 .Requires()
                 .IsNotNull(identifyer, "identifyer", "identifyer é obrigatório")
             );
        }
    }
}