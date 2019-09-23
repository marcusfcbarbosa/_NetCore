using FluentValidator;
using FluentValidator.Validation;
using ProAgil.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProAgil.Domain.ProAgilContext.Commands.Inputs
{
    public class AthenticationUserCommand : Notifiable, ICommand
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string Context { get; set; }

        public void Validate()
        {
            AddNotifications(new ValidationContract()
                  .Requires()
                  .IsNotNull(Name, "Name", "Name é obrigatório")
                  .IsEmail(Email, "Email", "Email inválido")
                  .IsNotNull(Password, "Password", "Password é obrigatório")
                  .IsNotNull(FullName, "FullName", "FullName é obrigatório")
                  .IsNotNull(Context, "Context", "Context é obrigatório")
              );
            
        }
    }
}
