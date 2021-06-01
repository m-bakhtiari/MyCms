using MyCms.Data.Context;
using MyCms.Domain.Interfaces;
using System.Threading.Tasks;

namespace MyCms.Data.Repositories
{
    public class SaveChangesRepository : ISaveChangesRepository
    {
        private readonly MyCmsContext _context;

        public SaveChangesRepository(MyCmsContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
