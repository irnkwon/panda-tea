using System;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PandaTea.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaTea.Controllers;
using PandaTea.Models;
using Xunit;
using Xunit.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Moq;

namespace PandaTea.Tests
{
    /// <summary>
    /// UnitTests for Actions in OrdersController
    /// </summary>
    public class OrdersControllerTests
    {
        private readonly ITestOutputHelper output;
        private OrdersController _ordersController;
        private PandaTeaContext _context;

        public OrdersControllerTests()
        {
            var options = new DbContextOptionsBuilder<PandaTeaContext>()
               .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
               .Options;
            _context = new PandaTeaContext(options);
            _ordersController = new OrdersController(_context);

            this.output = output;

            //var _context = new Mock<ControllerContext>();
            //var _session = new Mock<ISession>();

            //_context.Setup(m => m.HttpContext.Session).Returns(_session.Object);

            //OrdersController oc = new OrdersController(null);

            //oc.ControllerContext = _context.Object;
        }

        /// <summary>
        /// Test for Orders Controller StoreQuantity action
        /// </summary>
        [Fact]
        public void StoreQuantity_StoreDrinkQuanityUserWants()
        {
            var testOrder = new Order
            {
                OrderId = 1,
                UserId = 1,
                MenuId = 1,
                StoreId = 1,
                Quantity = 1
            };

            ActionResult actionResult = _ordersController.StoreQuantity(testOrder);
            string ser = JsonConvert.SerializeObject(actionResult);
            object dec = JsonConvert.DeserializeObject(ser);
            JObject jsonObj = JObject.Parse(dec.ToString());
            string result = jsonObj["Value"]["message"].ToString();

            Assert.Contains("Quantity storage successful", result);
        }

        [Theory]
        [InlineData(2, 2, 1, 1, 2)]
        public void Create_InsertsOrderDataToDatabase(int orderID, int userID, int menuID, int storeID, int quantity)
        {
            OrdersController ordersController = new OrdersController(null);

            Order testOrder = new Order
            {
                UserId = userID,
                MenuId = menuID,
                StoreId = storeID,
                Quantity = quantity
            };

            var result = ordersController.Create(testOrder);

            Assert.NotNull(result);
            //string ser = JsonConvert.SerializeObject(actual);
            //object dec = JsonConvert.DeserializeObject(ser);
            //JObject jsonObj = JObject.Parse(dec.ToString());
            //string result = jsonObj["Value"]["message"].ToString();

            //Assert.Equal(testOrder.ToString(), result.ToString());
        }
    }
}
