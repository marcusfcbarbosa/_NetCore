using ProAgil.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProAgil.Domain.ProAgilContext.Entities
{
    public class Lote : Entity
    {
        protected Lote(){}
        public Lote(string nome,
         decimal preco, DateTime? dataInicio, 
        DateTime? dataFim, 
        int quantidade, Evento evento)
        {
            Nome = nome;
            Preco = preco;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Quantidade = quantidade;
            Evento = evento;
        }
            
            public string Nome { get; private set; }
            public decimal Preco { get; private set; }
            public DateTime? DataInicio { get; private set; }
            public DateTime? DataFim { get; private set; }
            public int Quantidade { get; private set; }
            public int EventoId { get; private set; }
            public Evento Evento {get; private set;}
    }
}