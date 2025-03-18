using IndividualTask3.Interfaces;
using IndividualTask3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IndividualTask3.Controllers
{
    //[ApiController]
    //[Route("api/customer-fields")]
    //[Route("CustomerField")]
    public class CustomerFieldsController : Controller
    {
        private readonly ICustomerFieldService _service;
        private readonly ILogger<CustomerFieldsController> _logger;
        public CustomerFieldsController(ICustomerFieldService service, ILogger<CustomerFieldsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        //[HttpGet("{accountNumber}")]
        //public async Task<CustomerField> GetFields(string accountNumber)
        //{
        //    try
        //    {
        //        var customerField = await _service.GetCustomerFieldsAsync(accountNumber);
        //        if (customerField == null)
        //        {
        //            _logger.LogInformation("Customer field is null");
        //            return null;
        //        }

        //        return customerField;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while fetching customer fields.");
        //        return null;
        //    }
        //}

        // Render a View with CustomerField data
        public IActionResult Index()
        {
            return View("Index");
        }

        // Handles the search request
        public async Task<IActionResult> Search(string accountNumber)
        {
            var customerField = await _service.GetCustomerFieldsAsync(accountNumber);

            if (customerField == null)
            {
                ViewBag.ErrorMessage = "No customer fields found for the provided account number.";
                return View("Index"); // Return to the Index view with an error message
            }
            return View("SearchResult", customerField); // Show the search results in SearchResult.cshtml
        }

        // Handles form submission from SearchResult
        //[HttpPost]
        public IActionResult SubmitFields(Dictionary<string, string> fields)
        {
            try
            {
                if (fields == null || fields.Count == 0)
                {
                    _logger.LogWarning("No fields were submitted.");
                    ViewBag.ErrorMessage = "No fields were submitted. Please provide values for all fields.";
                    return View("SearchResult");
                }
                foreach (var field in fields)
                {
                    _logger.LogInformation($"Field: {field.Key}, Value: {field.Value}");
                }

                ViewBag.SuccessMessage = "Fields submitted successfully!";
                // Return the confirmation page with the submitted fields
                return View("SubmitConfirmation", fields);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while submitting fields.");
                ViewBag.ErrorMessage = "An error occurred while submitting fields. Please try again.";
                return View("SearchResult");
            }
        }

    }
}
