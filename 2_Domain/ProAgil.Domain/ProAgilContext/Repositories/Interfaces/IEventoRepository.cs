using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProAgil.Domain.ProAgilContext.Entities;

namespace ProAgil.Domain.ProAgilContext.Repositories.Interfaces
{
    public interface IEventoRepository : IBaseRepository<Evento>
    {

         Task<Evento[]>  GetAllEventosAsyncBytTema(string tema, bool includePalestrantes);
         Task<Evento[]>  GetAllEventosAsync(bool includePalestrantes = false);
         Task<Evento>  GetAllEventosAsyncById(int id,bool includePalestrantes = false);

         IEnumerable<Evento> GetAll();
    }
}