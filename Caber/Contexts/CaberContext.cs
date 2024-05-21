using Microsoft.EntityFrameworkCore;

namespace Caber.Contexts
{
    public class CaberContext : DbContext
    {
        public CaberContext(DbContextOptions<CaberContext> options) : base(options)
        {
            Console.WriteLine("CaberContext created");
            Console.WriteLine(options.ToString());
        }
    }
}
