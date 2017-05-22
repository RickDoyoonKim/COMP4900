using System;
using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.DependencyInjection;
using ImpactWebsite.Data;
using ImpactWebsite.Models;
using ImpactWebsite.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Moq;

namespace ImpactWebsite.Tests
{
    public class UnitTest_HomeController
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ApplicationDbContext _context;

        public UnitTest_HomeController()
        {
            var efServiceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(b => b.UseInMemoryDatabase().UseInternalServiceProvider(efServiceProvider));

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void Error_ReturnsErrorView()
        {
            // Arrange
            var mockContext = new Mock<ApplicationDbContext>();
            var controller = new HomeController(_context);
            var errorView = "~/Views/Shared/Error.cshtml";

            // Act
            var result = controller.Error();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.Equal(errorView, viewResult.ViewName);
        }

        [Fact]
        public void Contact_ReturnsContactPage()
        {
            // Arrange
            var mockContext = new Mock<ApplicationDbContext>();
            var controller = new HomeController(_context);
            var contactView = "~/Views/Home/Contact.cshtml";

            // Action
            var result = controller.Contact();
            var viewBagResult = controller.ViewData["Message"];

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.Equal(contactView, viewResult.ViewName);
            Assert.Equal(viewBagResult, "Your contact page.");
        }
    }






    /// <summary>
    /// Test Base web 
    /// </summary>
    public class BaseWebTest
    {
        protected readonly HttpClient _client;
        protected string _contentRoot;

        public BaseWebTest()
        {
            _client = GetClient();
        }

        protected HttpClient GetClient()
        {
            var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;
            _contentRoot = GetProjectPath("src", startupAssembly);
            var builder = new WebHostBuilder()
                .UseContentRoot(_contentRoot)
                .UseStartup<Startup>();

            var server = new TestServer(builder);
            var client = server.CreateClient();

            return client;
        }

        /// <summary>
        /// Gets the full path to the target project path that we wish to test
        /// </summary>
        /// <param name="solutionRelativePath">
        /// The parent directory of the target project.
        /// e.g. src, samples, test, or test/Websites
        /// </param>
        /// <param name="startupAssembly">The target project's assembly.</param>
        /// <returns>The full path to the target project.</returns>
        protected static string GetProjectPath(string solutionRelativePath, Assembly startupAssembly)
        {
            // Get name of the target project which we want to test
            var projectName = startupAssembly.GetName().Name;

            // Get currently executing test project path
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;

            // Find the folder which contains the solution file. We then use this information to find the target
            // project which we want to test.
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                var solutionFileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, "ImpactWebsite.sln"));
                if (solutionFileInfo.Exists)
                {
                    return Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath, projectName));
                }

                directoryInfo = directoryInfo.Parent;
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Solution root could not be located using application root {applicationBasePath}.");
        }

    }
}

