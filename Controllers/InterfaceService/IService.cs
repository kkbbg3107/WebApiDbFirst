using WebApiDBFirst.DTOModels;
using WebApiDBFirst.Models;

namespace WebApiDBFirst.Controllers.InterfaceService
{
    public interface IService
    {
        Task<IEnumerable<EmployeeDTO>> FilterColumn();
        Task<IEnumerable<EmployeeDTO>> Nothing();

        Task<IEnumerable<Employee>> EmpolyeeNothing();
        Task AdditemSth(Employee emp);

        Task RemoveSth(Employee emp);
    }
}
