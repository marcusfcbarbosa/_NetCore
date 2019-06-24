using FluentValidator;
using FluentValidator.Validation;
using ProAgil.Shared.Commands;

namespace ProAgil.Domain.ProAgilContext.Commands.Inputs
{
    public class CriaPalestranteCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public string MiniCurriculo { get;  set; }
        public string ImgUrl { get;  set; }
        public string Telefone { get;  set; }
        public string Email { get; set; }
        public bool Valid()
        {
             AddNotifications(new ValidationContract()
                    .Requires()
                    .IsNotNull(Telefone,"Telefone","Telefone é obrigatório")
                    .IsEmail(Email, "Email", "E-mail inválido")
                    .HasMinLen(Nome, 3,"Nome"," Nome  Nome deve conter pelo menos 3 caracteres")
                    .HasMinLen(MiniCurriculo, 30,"MiniCurriculo","MiniCurriculo deve conter pelo menos 30 caracteres")
                );
            return !this.Invalid;
        }
    }
}