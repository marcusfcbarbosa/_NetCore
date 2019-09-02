using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain.ProAgilContext.Entities;

using ProAgil.Domain.ProAgilContext.Repositories.Interfaces;
using ProAgil.Infra.Context;

namespace ProAgil.Infra.Repository
{
    public class PalestranteRepository : BaseRepository<Palestrante>, IPalestranteRepository
    {
        private readonly ProAgilContext _context;
        public PalestranteRepository(ProAgilContext context)
        :base(context){
                _context = context;
        }
        
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(c=>c.RedesSociais);

            if(includeEvento){
                query = query
                .Include(pe=>pe.PalestranteEventos)
                .ThenInclude(p=>p.Evento);
            }
            query = query.OrderByDescending(x=>x.Nome);
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestrantesAsyncById(int id)
        {
            return await _context.Palestrantes.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name)
        {
             IQueryable<Palestrante> query = _context.Palestrantes
                
                .Include(pe=>pe.PalestranteEventos)
                .ThenInclude(p=>p.Evento)
                .Where(x=>x.Nome.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestrantesAsyncById(int id, bool includeEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(c=>c.RedesSociais)
            .Where(x=>x.Id == id);

            if(includeEvento){
                query = query
                .Include(pe=>pe.PalestranteEventos)
                .ThenInclude(p=>p.Evento);
            }
            query = query.OrderBy(x=>x.Nome);
            return await query.FirstAsync();
        }
    }
}