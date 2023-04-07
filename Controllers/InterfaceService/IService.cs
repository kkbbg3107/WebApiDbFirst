using WebApiDBFirst.DTOModels;
using WebApiDBFirst.Models;

namespace WebApiDBFirst.Controllers.InterfaceService
{
    public interface IService
    {
        Task<EmployeeDetail> QueryItemSth(int id);
        Task<IEnumerable<EmployeeDetail>> QueryAll();
        Task UpdateItemSth(EmployeeDetail emp);
        Task AdditemSth(EmployeeDetail emp);

        Task<Employee> Add(EmployeeDetail emp);
        Task RemoveSth(EmployeeDetail emp);
    }
}
