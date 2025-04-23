using Microsoft.AspNetCore.Mvc;
using MilkConference.Services;
using NanoviConference.Catalog.Model.Customer;
using NanoviConference.Catalog.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkConference.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerViewDto>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerViewDto>> GetCustomerById(string id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                return Ok(customer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Customer with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CustomerViewDto>>> SearchCustomers(string term)
        {
            try
            {
                var customers = await _customerService.SearchCustomersAsync(term);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("group/{groupId}")]
        public async Task<ActionResult<IEnumerable<CustomerViewDto>>> GetCustomersByGroup(int groupId)
        {
            try
            {
                var customers = await _customerService.GetCustomersByGroupAsync(groupId);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] CustomerUpdateDto customerDto)
        {
            try
            {
                var updatedCustomer = await _customerService.UpdateCustomerAsync(id, customerDto);
                return Ok(updatedCustomer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Customer with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Customer with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}/orders")]
        public async Task<ActionResult<IEnumerable<OrderViewDto>>> GetOrderHistory(string id)
        {
            try
            {
                var orders = await _customerService.GetOrderHistoryAsync(id);
                return Ok(orders);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Customer with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}/debts")]
        public async Task<ActionResult<IEnumerable<DebtViewDto>>> GetDebtsByCustomer(string id)
        {
            try
            {
                var debts = await _customerService.GetDebtsByCustomerAsync(id);
                return Ok(debts);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Customer with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{id}/increment-purchase")]
        public async Task<IActionResult> IncrementPurchaseCount(string id)
        {
            try
            {
                await _customerService.IncrementPurchaseCountAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Customer with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}/address")]
        public async Task<IActionResult> UpdateAddress(string id, [FromBody] string address)
        {
            try
            {
                await _customerService.UpdateAddressAsync(id, address);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Customer with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CustomerViewDto>>> GetCustomersByUser(Guid userId)
        {
            try
            {
                var customers = await _customerService.GetCustomersByUserAsync(userId);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("customers/{date}/{sessionTime}")]
        public async Task<IActionResult> GetCustomersByDateAndSession(DateTime date, string sessionTime)
        {
            var customers = await _customerService.GetCustomersByDateAndSessionAsync(date, sessionTime);
            return Ok(customers);
        }

        [HttpPost("customers/{date}/{sessionTime}")]
        public async Task<IActionResult> CreateCustomerByDateAndSession(DateTime date, string sessionTime, [FromBody] CustomerCreateDto customer)
        {
            var createdCustomer = await _customerService.CreateCustomerByDateAndSessionAsync(date, sessionTime, customer);
            return CreatedAtAction(nameof(GetCustomersByDateAndSession), new { date, sessionTime }, createdCustomer);
        }
    }
}