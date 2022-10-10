using BookStore.Models.Models.User;

namespace BookStore.BL.Interfaces
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<Employee>> GetEmployeeDetails();
        public Task<Employee?> GetEmployeeDetails(int id);
        public Task AddEmployee(Employee employee);
        public Task UpdateEmployee(Employee employee);
        public Task DeleteEmployee(int id);
        public Task<bool> CheckEmployee(int id);
    }
}
