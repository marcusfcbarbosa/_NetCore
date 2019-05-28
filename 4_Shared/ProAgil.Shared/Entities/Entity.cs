using System;
using FluentValidator;


namespace ProAgil.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        public Entity(){
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }

}