using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApiDBFirst.Controllers.InterfaceService;
using WebApiDBFirst.DTOModels;
using WebApiDBFirst.Models;
using WebApiDBFirst.Services.InterfaceRepository;

namespace WebApiDBFirst.Services
{
    public class EmployeeDetailService : IService
    {
        private IRepository _employeeRepo;

        private readonly IHttpContextAccessor _contextAccessor;

        public EmployeeDetailService(IRepository employeeRepo, IHttpContextAccessor contextAccessor)
        {
            _employeeRepo = employeeRepo;
            _contextAccessor = contextAccessor;
        }             

        public async Task AdditemSth(EmployeeDetail emp)
        {
            var collection = _contextAccessor.HttpContext.User.Claims.ToList();
            var userEmail = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            await _employeeRepo.AddEmployee(emp);
        }

        public async Task RemoveSth(EmployeeDetail emp)
        {
            await _employeeRepo.RemoveEmployee(emp);
        }

        public async Task<EmployeeDetail> QueryItemSth(int id)
        {
            var result = await _employeeRepo.GetEmployee(id);

            return result;
        }

        public async Task<IEnumerable<EmployeeDetail>> QueryAll()
        {
            var result = await _employeeRepo.GetEmployees().ToListAsync();

            return result;
        }

        public async Task UpdateItemSth(EmployeeDetail emp)
        {
            await _employeeRepo.Update(emp.EmployeeId, emp);
        }

        public Task<Employee> Add(EmployeeDetail emp)
        {
            var e = _employeeRepo.AddEmployee(emp);
            return e;
        }
    }
}
