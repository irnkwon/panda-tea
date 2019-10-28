using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaTea.Controllers;
using PandaTea.Models;
using Xunit;

namespace PandaTea.Tests
{
    public class UnitTest1
    {
        private UsersController _menusController;
        private PandaTeaContext _context;
        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<PandaTeaContext>()
               .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
               .Options;
            _context = new PandaTeaContext(options);
            _menusController = new UsersController(_context);
        }

        [Fact]
        public void IsPrime_InputIs1_ReturnFalse()
        {
            ActionResult result = _menusController.Index().Result as ActionResult;
            Assert.NotNull(result);
        }
    }
}
