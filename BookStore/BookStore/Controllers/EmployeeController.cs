using BookStore.BL.Interfaces;
using BookStore.Models.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //[Authorize]
        [HttpGet(nameof(GetEmployeeDetails))]
        public async Task<IActionResult> GetEmployeeDetails()
        {
            return Ok(await _employeeService.GetEmployeeDetails());
        }
        [HttpGet(Name = "GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeDetails(int id)
        {
            return Ok(await _employeeService.GetEmployeeDetails(id));
        }
        [HttpPost(nameof(AddEmployee))]
        public async Task AddEmployee(Employee employee)
        {
            await _employeeService.AddEmployee(employee);
        }
        [HttpPut(nameof(UpdateEmployee))]
        public async Task UpdateEmployee(Employee employee)
        {
            await _employeeService.UpdateEmployee(employee);
        }
        [HttpDelete(nameof(DeleteEmployee))]
        public async Task DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployee(id);
        }
        [HttpGet(nameof(CheckEmployee))]
        public async Task<IActionResult> CheckEmployee(int id)
        {
            return Ok(await _employeeService.CheckEmployee(id));
        }
    }
}
