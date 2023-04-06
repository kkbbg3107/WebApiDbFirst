using WebApiDBFirst.Models;

namespace WebApiDBFirst.Services.InterfaceRepository
{
    public interface IRepository
    {
        IQueryable<Employee> GetEmployee();

        Task AddEmployee(Employee employee);

        Task RemoveEmployee(Employee employee);
    }
}
