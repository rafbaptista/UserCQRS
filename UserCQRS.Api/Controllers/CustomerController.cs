using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.Interfaces.Services;
using UserCQRS.Domain.ViewModels;

namespace UserCQRS.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerViewModel customer)
        {            
            return Ok(await _customerService.Add(customer));
        }

        [HttpGet]        
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _customerService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {            
            return Ok(await _customerService.GetById(id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Customer customer)
        {
            return Ok(await _customerService.Update(customer));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _customerService.Delete(id));
        }

    }
}
