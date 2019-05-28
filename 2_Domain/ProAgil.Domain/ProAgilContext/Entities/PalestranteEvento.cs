using System;
using System.Collections.Generic;
using System.Linq;

namespace ProAgil.Domain.ProAgilContext.Entities
{
    public class PalestranteEvento 
    {
        protected PalestranteEvento(){}

        public PalestranteEvento(Evento evento, Palestrante palestrante)
        {
            Evento = evento;
            Palestrante = palestrante;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public int EventoId { get; private set; }
        public Evento Evento { get; private set; }
        public int PalestranteId { get; private set; }
        public Palestrante Palestrante { get; private set; }
    }
}