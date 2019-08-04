using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProAgil.Domain.ProAgilContext.Entities;
using ProAgil.Domain.ProAgilContext.ViewModel;
namespace ProAgil.Domain.ProAgilContext.Adapter
{
    public static class EventoAdapter
    {
        public static EventoViewModel DomainToViewModel(Evento evento) 
        {
            return new EventoViewModel
            {
                identifyer = evento.identifyer,
                Local = evento.Local,
                DataEvento = evento.DataEvento.ToShortDateString(),
                QtdPessoas = evento.QtdPessoas,
                Tema = evento.Tema,
                ImgUrl = evento.ImgUrl,
                Telefone = evento.Telefone,
                Email = evento.Email
            };
        }


        public static IEnumerable<EventoViewModel> DomainToViewModel(IEnumerable<Evento> eventos)
        {
            List<EventoViewModel> list = new List<EventoViewModel>();
            for (int i = 0; i < eventos.Count(); i++)
            {
                list.Add(new EventoViewModel
                {
                    identifyer = eventos.ElementAt(i).identifyer,
                    Local = eventos.ElementAt(i).Local,
                    DataEvento = eventos.ElementAt(i).DataEvento.ToShortDateString(),
                    QtdPessoas = eventos.ElementAt(i).QtdPessoas,
                    Tema = eventos.ElementAt(i).Tema,
                    ImgUrl = eventos.ElementAt(i).ImgUrl,
                    Telefone = eventos.ElementAt(i).Telefone,
                    Email = eventos.ElementAt(i).Email
                });
            }
            return list;
        }
    }
}