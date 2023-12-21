using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Controllers
{
    public class Staff
    {
        
        public string? Name { get; set; }

        
        public string? Email { get; set; }

        
        public string? Phone { get; set; }

        
        public decimal? HourlySalary { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IValidator<Staff> _staffValidator;

        public StaffController(IValidator<Staff> staffValidator)
        {
            _staffValidator = staffValidator;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Staff value)
        {
            var validationResult = _staffValidator.Validate(value);

            if (validationResult.IsValid)
            {
                
                return Ok(value);
            }
            else
            {
                
                return BadRequest(validationResult.Errors);
            }
        }
    }
}
