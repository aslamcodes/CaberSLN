using Caber;
using Caber.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CaberTests.ServicesTests
{
    internal class CabServiceTests
    {
        private CaberContext context;
        private CabService cabService;
        private CaberContext GetContext()
        {
            return context;
        }

        private void SetContext(CaberContext value)
        {
            context = value;
        }
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CaberContext>()
               .UseInMemoryDatabase("EmployeeTest")
           .Options;

            SetContext(new CaberContext(options));
            GetContext().Database.EnsureCreated();

            cabService = new CabService(new CabRepository(GetContext()), new RideRepository(GetContext()));
        }
    }
}
