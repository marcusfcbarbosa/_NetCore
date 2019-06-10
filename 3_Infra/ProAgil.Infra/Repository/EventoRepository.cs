using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain.ProAgilContext.Entities;
using ProAgil.Domain.ProAgilContext.Repositories.Interfaces;
using ProAgil.Infra.Context;

namespace ProAgil.Infra.Repository
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRepository
    {
        private readonly ProAgilContext _context;
        public EventoRepository(ProAgilContext context)
            :base(context)
        {
            _context = context;
        }
        public async Task<Evento[]> GetAllEventosAsyncBytTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c=>c.Lotes)
            .Include(c=>c.RedesSociais)
            .Where(c=>c.Tema.Contains(tema));

            if(includePalestrantes){
                query = query
                .Include(pe=>pe.PalestranteEventos)
                .ThenInclude(p=>p.Palestrante);
            }
            query = query.OrderByDescending(x=>x.DataEvento);
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c=>c.Lotes)
            .Include(c=>c.RedesSociais);

            if(includePalestrantes){
                query = query
                .Include(pe=>pe.PalestranteEventos)
                .ThenInclude(p=>p.Palestrante);
            }
            query = query.OrderByDescending(x=>x.DataEvento);
            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAllEventosAsyncById(int id,bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c=>c.Lotes)
            .Include(c=>c.RedesSociais)
            .Where(c=>c.Id == id);

            if(includePalestrantes){
                query = query
                .Include(pe=>pe.PalestranteEventos)
                .ThenInclude(p=>p.Palestrante);
            }
            return await query.FirstOrDefaultAsync();
        }

        public IEnumerable<Evento> GetAll()
        {
            IEnumerable<Evento> query = _context.Eventos.ToList();
            return query;
        }
    }
}