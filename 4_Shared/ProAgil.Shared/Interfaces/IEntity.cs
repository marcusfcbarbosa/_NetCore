using System;
using System.Collections.Generic;
using System.Text;

namespace ProAgil.Shared.Interfaces
{
    public interface IEntity
    {
         int Id { get; private set; }
         string identifyer { get; private set; }
         DateTime CreatedAt { get; private set; }
    }
}
