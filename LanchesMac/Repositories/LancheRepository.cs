using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(lanche => lanche.Categoria);

        public IEnumerable<Lanche> LanchesFavoritos => (from lanche in _context.Lanches
                                                       where lanche.IsLanchePreferido
                                                       select lanche).Include(l => l.Categoria);

        public Lanche GetLancheById(int lancheId)
        {
            Lanche lanche = _context.Lanches.FirstOrDefault(lanche => lanche.LancheId == lancheId);

            return lanche;
        }
    }
}
