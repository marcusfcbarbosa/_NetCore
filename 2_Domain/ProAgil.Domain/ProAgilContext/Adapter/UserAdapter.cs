using ProAgil.Domain.Identity;
using ProAgil.Domain.ProAgilContext.Commands.Inputs;
using ProAgil.Domain.ProAgilContext.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProAgil.Domain.ProAgilContext.Adapter
{
    public static class UserAdapter
    {
        public static User CommandToDomain(AthenticationUserCommand command, string context)
        {
            return new User
            {
                Context = context,
                UserName = command.Name,
                Email = command.Email,
                PasswordHash = command.Password,
                FullName = command.FullName
            };
        }


        public static UserLogin DomainToDto(User user)
        {
            return new UserLogin
            {
                Password = user.PasswordHash,
                UserName = user.UserName
            };
        }
    }
}