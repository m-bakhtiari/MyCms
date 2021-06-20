using System.Threading.Tasks;

namespace MyCms.Data.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}
