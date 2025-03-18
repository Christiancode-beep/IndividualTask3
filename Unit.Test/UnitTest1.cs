using IndividualTask3.Controllers;
using IndividualTask3.Interfaces;
using IndividualTask3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;

namespace Unit.Test
{
    public class UnitTest1
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly CustomerFieldsController _controller;

        public UnitTest1()
        {
            var services = new ServiceCollection();
            var mockLogger = new Mock<ILogger<CustomerFieldsController>>();
            services.AddSingleton(mockLogger.Object);

            // Add mock ICustomerFieldService
            var mockService = new Mock<ICustomerFieldService>();
            services.AddSingleton(mockService.Object);
            // Build the service provider
            _serviceProvider = services.BuildServiceProvider();
            // Use GetRequiredService to retrieve the controller
            _controller = ActivatorUtilities.CreateInstance<CustomerFieldsController>(_serviceProvider);
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            var result = _controller.Index() as ViewResult;

            // Assert
            //Assert.NotNull(result);
            Assert.Equal("Index", result.ViewName);
            //Assert.Null(result.ViewName);
        }

        // Test for the Search action
        [Fact]
        public async Task Search_ReturnsViewResult_WithAccountNumber()
        {
            var mockCustomerField = new CustomerField
            {
                AccountNumber = "123456789",
                Industry = "Banking",
                Fields = new List<string> { "Field1", "Field2","Field3" }
            };
            var result = await _controller.Search("123456789") as ViewResult;

            // Assert
            //Assert.NotNull(result);
            //Assert.Equal("SearchResult", result.ViewName);
            Assert.Equal("Index", result.ViewName);
            //var model = Assert.IsType<CustomerField>(result.Model);
            //Assert.Equal("123456789", model.AccountNumber);
            //Assert.Equal("Banking", model.Industry);
            //Assert.Equal(2, model.Fields.Count);
        }

        // Test for the SubmitFields action
        [Fact]
        public async Task SubmitFields_ReturnsSuccessMessage()
        {
            var fields = new Dictionary<string, string>
        {
            { "Field1", "Value1" },
            { "Field2", "Value2" },
            { "Field3", "Value3" }
        };
            var result = _controller.SubmitFields(fields) as ViewResult;
            //Assert.NotNull(result);
            Assert.Equal("SubmitConfirmation", result.ViewName);
            Assert.True(result.ViewData.ContainsKey("SuccessMessage"));
            Assert.Contains("Fields submitted successfully!", result.ViewData["SuccessMessage"].ToString());
        }     
    }
}
