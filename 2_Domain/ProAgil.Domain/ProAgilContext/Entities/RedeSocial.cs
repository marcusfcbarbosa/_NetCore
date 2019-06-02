using ProAgil.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProAgil.Domain.ProAgilContext.Entities
{
     public class RedeSocial : Entity
    {
        protected RedeSocial(){}
        public RedeSocial(string nome, string url, Evento evento = null, Palestrante palestrante = null)
        {
            Nome = nome;
            Url = url;
            Evento =( evento!= null ? evento: null);
            Palestrante = ( palestrante!= null ? palestrante: null);
        }
        public string Nome { get; private set; }   
        public string Url { get; private set; }
        public Guid? EventoId { get; set; }
        public Evento Evento { get; private set; }
        public Guid? PalestranteId { get; set; }
        public Palestrante Palestrante { get; private set; }
    }
}