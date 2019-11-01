using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaTea.Controllers;
using PandaTea.Models;
using Xunit;
using Xunit.Abstractions;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PandaTea.Tests
{
    public class UsersControllerTests
    {
        private readonly ITestOutputHelper output;
        private UsersController _usersController;
        private PandaTeaContext _context;

        public UsersControllerTests(ITestOutputHelper output)
        {
            var options = new DbContextOptionsBuilder<PandaTeaContext>()
               .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
               .Options;
            _context = new PandaTeaContext(options);
            _usersController = new UsersController(_context);
            
            this.output = output;
        }

        [Fact]
        public void Index_ReturnsIndexView()
        {
            ActionResult result = _usersController.Index().Result as ActionResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void Validate_ChecksSuccessfulLogin()
        {
            User testUser = new User
            {
                Email = "Dnay8979@conestogac.on.ca",
                Password = "p@ssword"
            };

            JsonResult jsonResult = _usersController.Validate(testUser);
            string ser = JsonConvert.SerializeObject(jsonResult);
            object dec = JsonConvert.DeserializeObject(ser);
            JObject jsonObj = JObject.Parse(dec.ToString());
            string result = jsonObj["Value"]["message"].ToString();

            output.WriteLine("Expected: \"Logged In\"");
            output.WriteLine("Actual: \"{0}\"", result);
            output.WriteLine("JsonResult: {0}", ser);

            Assert.Equal("Logged In", result);
        }

        [Fact]
        public void Validate_ChecksInvalidEmail()
        {
            User testUser = new User
            {
                Email = "testUser@fake.email",
                Password = "p@ssword"
            };

            JsonResult jsonResult = _usersController.Validate(testUser);
            string ser = JsonConvert.SerializeObject(jsonResult);
            object dec = JsonConvert.DeserializeObject(ser);
            JObject jsonObj = JObject.Parse(dec.ToString());
            string result = jsonObj["Value"]["message"].ToString();
            
            Assert.Equal("Invalid Email", result);

            output.WriteLine("Expected: \"Invalid Email\""); 
            output.WriteLine("Actual: \"{0}\"", result);
            output.WriteLine("JsonResult: {0}", ser);
        }

        [Fact]
        public void Validate_ChecksInvalidPassword()
        {
            User testUser = new User
            {
                Email = "Dnay8979@conestogac.on.ca",
                Password = "FakePassword"
            };

            JsonResult jsonResult = _usersController.Validate(testUser);
            string ser = JsonConvert.SerializeObject(jsonResult);
            object dec = JsonConvert.DeserializeObject(ser);
            JObject jsonObj = JObject.Parse(dec.ToString());
            string result = jsonObj["Value"]["message"].ToString();

            output.WriteLine("Expected: \"Invalid Password\"");
            output.WriteLine("Actual: \"{0}\"", result);

            Assert.Equal("Invalid Password", result);
        }

        [Fact]
        public void Login_ReturnsLoginPageView()
        {
            ActionResult result = _usersController.Login() as ActionResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void Logout_ChecksSuccessfulLogout()
        {
            // Have to be logged in first?

            ActionResult jsonResult = _usersController.Logout();
            string ser = JsonConvert.SerializeObject(jsonResult);
            object dec = JsonConvert.DeserializeObject(ser);
            JObject jsonObj = JObject.Parse(dec.ToString());
            string result = jsonObj["Value"]["message"].ToString();

            output.WriteLine("Expected: \"Invalid Email\"");
            output.WriteLine("Actual: \"{0}\"", result);
            output.WriteLine("JsonResult: {0}", ser);

            Assert.Equal("Logout Successful", result);
        }

        //[Fact]
        //public void Details_ReturnsView()
        //{
        //    decimal id = 0;
        //    var mockUserControllers = new Mock<UsersController>();
        //    var mockContext = new Mock<PandaTeaContext>();

        //    mockUserControllers.Setup(x => x.Details(id));
        //    mockUserControllers.Verify(x => x.NotFound(), Times.AtLeastOnce());

        //    UsersController usersController = new UsersController(mockContext.Object);
        //    usersController.NotFound();

        //    var actualJsonResult = _usersController.Validate(testUser);
        //    var expectedJsonResult = _usersController.Validate(testUser);

        //    Assert.Equal(expectedJsonResult, actualJsonResult);
        //}
    }
}

/// Actions to Test
/// UsersController: Login, Logout, Validate
/// OrdersController: StoreQuantity, Create (2nd)
/// MenusController: CheckMenuItem, Index
