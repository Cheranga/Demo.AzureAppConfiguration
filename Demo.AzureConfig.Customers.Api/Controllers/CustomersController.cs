using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.DTO.Requests;
using Demo.AzureConfig.Customers.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.AzureConfig.Customers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerSearchService _customerSearchService;

        public CustomersController(ICustomerSearchService customerSearchService)
        {
            _customerSearchService = customerSearchService;
        }
        
        [HttpGet("search/id/{customerId}")]
        public async Task<IActionResult> SearchCustomerByIdAsync([FromRoute] string customerId)
        {
            var request = new SearchCustomerByIdRequest
            {
                Id = customerId
            };

            var operation = await _customerSearchService.SearchAsync(request);
            return Ok(operation.Data);
        }
    }
}