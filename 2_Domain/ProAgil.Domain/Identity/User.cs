using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProAgil.Domain.Identity
{
    public class User : IdentityUser<long>
    {
        [Column(TypeName = "nvarchar(150)")]
        public string Context { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public override string ToString()
        {
            return this.UserName + " " + FullName;
        }
    }
}