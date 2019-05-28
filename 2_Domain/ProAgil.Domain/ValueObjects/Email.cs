using FluentValidator.Validation;
using ProAgil.Shared.ValueObjects;
using System;

namespace ProAgil.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;
            AddNotifications(new ValidationContract()
            .Requires()
            .IsEmail(Address, "Email.Address", "E-mail inválido")
            );
        }
        public String Address { get; private set; }
    }
}