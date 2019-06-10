using System;
using System.Collections.Generic;
using System.Text;

namespace ProAgil.Shared.Interfaces
{
    public interface IEntity
    {
         int Id { get; set; }
         string identifyer { get; set; }
         DateTime CreatedAt { get; set; }
    }
}
