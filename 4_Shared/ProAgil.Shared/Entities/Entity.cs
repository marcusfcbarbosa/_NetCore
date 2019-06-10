using System;
using FluentValidator;
using ProAgil.Shared.Interfaces;

namespace ProAgil.Shared.Entities
{
    
    public abstract class Entity : Notifiable , IEntity
    {
        public int Id { get; set; }
        public string identifyer { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}