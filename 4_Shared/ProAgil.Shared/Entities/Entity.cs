using System;
using FluentValidator;


namespace ProAgil.Shared.Entities
{
    
    public abstract class Entity : Notifiable
    {
        public Entity(){
        }
        public String Id { get; set; }
    }

}