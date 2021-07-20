using System;
using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.API.Services;
using Demo.AzureConfig.Customers.Core.Application;
using Demo.AzureConfig.Customers.Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace Demo.AzureConfig.Customers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly SecretMessageConfiguration _secretMessageConfiguration;

        public CustomersController(ICustomerService customerService, SecretMessageConfiguration secretMessageConfiguration)
        {
            _customerService = customerService;
            _secretMessageConfiguration = secretMessageConfiguration;
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
            return Ok(new
            {
                operation.Data,
                _secretMessageConfiguration.Message
            });
        }

        [FeatureGate(ApplicationFeatures.CanDelete)]
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomerAsync([FromRoute] string customerId)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerRequest request)
        {
            var operation = await _customerService.CreateAsync(request);
            return Ok(operation);
        }
    }
}