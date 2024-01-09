using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Services
{
    public class RelatorioLanchesService
    {
        private readonly AppDbContext _context;

        public RelatorioLanchesService(AppDbContext context)
        {
            _context = context;
        }

        //Retorna os dados de Lanches
        public async Task<IEnumerable<Lanche>> GetLanchesReport()
        {
            var lanches = await _context.Lanches.ToListAsync();

            if (lanches is null)
            {
                return default(IEnumerable<Lanche>);
            }

            return lanches;
        }
        //Retorna os dados de Categoria
        public async Task<IEnumerable<Categoria>> GetCategoriaReport()
        {
            var categorias = await _context.Categorias.ToListAsync();

            if (categorias is null)
            {
                return default(IEnumerable<Categoria>);
            }

            return categorias;
        }

    }
}
