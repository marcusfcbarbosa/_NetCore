using System;
using FluentValidator;
using FluentValidator.Validation;
using ProAgil.Shared.Commands;

namespace ProAgil.Domain.ProAgilContext.Commands.Inputs
{
    public class CriaEventoCommand : Notifiable, ICommand
    {
        public string Local { get;  set; }
        public DateTime DataEvento { get;  set; }
        public string Tema { get;  set; }
        public int QtdPessoas { get;  set; }
        public string ImgUrl { get;  set; }
        public string Telefone { get;  set; }
        public string Email {get;  set;}
        public bool Valid()
        {
               AddNotifications(new ValidationContract()
                    .Requires()
                    .IsNotNull(Telefone,"Telefone","Telefone é obrigatório")
                    .IsEmail(Email, "Email", "E-mail inválido")
                    .IsNotNull(QtdPessoas,"QtdPessoas","QtdPessoas é obrigatório")
                    .IsNotNull(DataEvento,"DataEvento","DataEvento é obrigatório")
                    .HasMinLen(Local, 3,"Local"," Local deve conter pelo menos 3 caracteres")
                );
            return Valid();
        }
    }
}