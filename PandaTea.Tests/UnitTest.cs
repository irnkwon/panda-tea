/* UnitTest.cs
 * 
 * PROG3050: Programming Microsoft Enterprise Applications
 * Group 7
 * 
 * Revision History
 *          Ji Hong Ahn, 2019-10-26: Created
 *
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaTea.Controllers;
using PandaTea.Models;
using Xunit;

namespace PandaTea.Tests
{
    /// <summary>
    /// UnitTest for Actions in Controllers
    /// </summary>
    public class UnitTest
    {
        private PandaTeaContext context;
        private UsersController usersController;
        private MenusController menusController;
        private OrdersController ordersController;
        public UnitTest()
        {
            var options = new DbContextOptionsBuilder<PandaTeaContext>()
               .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
               .Options;
            context = new PandaTeaContext(options);
            usersController = new UsersController(context);
            menusController = new MenusController(context);
            ordersController = new OrdersController(context);
        }

        /// <summary>
        /// Test for Users Controller Index action
        /// </summary>
        [Fact]
        public void UsersControllerIndexActionReturnsView()
        {
            ActionResult result = usersController.Index().Result as ActionResult;
            Assert.NotNull(result);
        }

        /// <summary>
        /// Test for Users Controller Detail action
        /// </summary>
        [Fact]
        public void UsersControllerDetailActionReturnsView()
        {
            decimal id = 1;
            ActionResult result = usersController.Details(id).Result as ActionResult;
            Assert.NotNull(result);
        }
        
        /// <summary>
        /// Test for Users Controller Edit action
        /// </summary>
        [Fact]
        public void UsersControllerEditActionReturnsView()
        {
            decimal id = 1;
            ActionResult result = usersController.Edit(id).Result as ActionResult;
            Assert.NotNull(result);
        }

        /// <summary>
        /// Test for Menus Controller Detail action
        /// </summary>
        [Fact]
        public void MenusControllerDetailActionReturnsView()
        {
            decimal id = 1;
            ActionResult result = menusController.Details(id).Result as ActionResult;
            Assert.NotNull(result);
        }

        /// <summary>
        /// Test for Menus Controller Edit action
        /// </summary>
        [Fact]
        public void MenusControllerEditActionReturnsView()
        {
            decimal id = 1;
            ActionResult result = menusController.Edit(id).Result as ActionResult;
            Assert.NotNull(result);
        }

        /// <summary>
        /// Test for Orders Controller Detail action
        /// </summary>
        [Fact]
        public void OrdersControllerDetailActionReturnsView()
        {
            decimal id = 1;
            ActionResult result = ordersController.Details(id).Result as ActionResult;
            Assert.NotNull(result);
        }

        /// <summary>
        /// Test for Orders Controller Edit action
        /// </summary>
        [Fact]
        public void OrdersControllerEditActionReturnsView()
        {
            decimal id = 1;
            ActionResult result = ordersController.Edit(id).Result as ActionResult;
            Assert.NotNull(result);
        }

    }
}
