using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApiDBFirst.Controllers.InterfaceService;
using WebApiDBFirst.DTOModels;
using WebApiDBFirst.Models;
using WebApiDBFirst.Services.InterfaceRepository;

namespace WebApiDBFirst.Services
{
    public class EmployeeService : IService
    {
        private IRepository _employeeRepo;

        private readonly IHttpContextAccessor _contextAccessor;

        public EmployeeService(IRepository employeeRepo, IHttpContextAccessor contextAccessor)
        {
            _employeeRepo = employeeRepo;
            _contextAccessor = contextAccessor;
        }

        public async Task<IEnumerable<EmployeeDTO>> FilterColumn()
        {
            var result = _employeeRepo.GetEmployee().Where(x => x.Id < 10).Select(x => new EmployeeDTO()
            {
                Id = x.Id,
            });
            return await result.ToListAsync();
        }        

        public async Task<IEnumerable<EmployeeDTO>> Nothing()
        {
            return await _employeeRepo.GetEmployee().Select(x => new EmployeeDTO() 
            {
                Id = x.Id,
            }).ToListAsync();
        }

        public async Task AdditemSth(Employee emp)
        {
            var collection = _contextAccessor.HttpContext.User.Claims.ToList();

            // employeeId
            //var user = collection.Where(x => x.Type == "EmployeeId").First().Value;

            var userEmail = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

            await _employeeRepo.AddEmployee(emp);
        }

        public async Task RemoveSth(Employee emp)
        {
            await _employeeRepo.RemoveEmployee(emp);
        }

        public async Task<IEnumerable<Employee>> EmpolyeeNothing()
        {
            return await _employeeRepo.GetEmployee().Select(x => x).ToListAsync();
        }
    }
}
