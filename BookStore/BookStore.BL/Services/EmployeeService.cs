using BookStore.BL.Interfaces;
using BookStore.Models.Models.User;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeDetails()
        {
            return await _employeeRepository.GetEmployeeDetails();
        }

        public async Task<Employee?> GetEmployeeDetails(int id)
        {
            return await _employeeRepository.GetEmployeeDetails(id);
        }

        public async Task AddEmployee(Employee employee)
        {
            await _employeeRepository.AddEmployee(employee);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _employeeRepository.UpdateEmployee(employee);
        }

        public async Task DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteEmployee(id);
        }

        public async Task<bool> CheckEmployee(int id)
        {
            return await _employeeRepository.CheckEmployee(id);
        }
    }
}
