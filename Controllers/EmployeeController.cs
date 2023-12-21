using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Validations;
using FluentValidation;

namespace WebApplication1.Controllers
{
    public class Employee
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public double HourlySalary { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IValidator<Employee> _validator;

        public EmployeeController(IValidator<Employee> validator)
        {
            _validator = validator;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee value)
        {
            var validationResult = _validator.Validate(value);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            // Your logic here

            return Ok(value);
        }

    }
}
