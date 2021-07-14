using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.API.Services;
using Demo.AzureConfig.Customers.Api.Constants;
using Demo.AzureConfig.Customers.Api.Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace Demo.AzureConfig.Customers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [FeatureGate(ApplicationFeatures.ShowSearchCustomerById)]
        [HttpGet("search/id/{customerId}")]
        public async Task<IActionResult> SearchCustomerByIdAsync([FromRoute] string customerId)
        {
            // Get the customer by id
            var request = new SearchCustomerByIdRequest
            {
                Id = customerId
            };

            var operation = await _customerService.SearchCustomerAsync(request);
            return Ok(operation.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerRequest request)
        {
            var operation = await _customerService.CreateAsync(request);
            return Ok(operation);
        }
    }
}