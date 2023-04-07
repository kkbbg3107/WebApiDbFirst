using WebApiDBFirst.Models;

namespace WebApiDBFirst.Services.InterfaceRepository
{
    public interface IRepository
    {
        IQueryable<EmployeeDetail> GetEmployees();
        Task<EmployeeDetail> GetEmployee(int id);

        Task<string> Update(int id, EmployeeDetail employee);

        Task<Employee> AddEmployee(EmployeeDetail employee);

        Task RemoveEmployee(EmployeeDetail employee);
    }
}
