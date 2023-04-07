using Microsoft.EntityFrameworkCore;
using WebApiDBFirst.Models;
using WebApiDBFirst.Services.InterfaceRepository;

namespace WebApiDBFirst.Repository
{
    public class EmployeeDetailRepository : IRepository
    {
        private readonly TodoListContext _context;

        public EmployeeDetailRepository(TodoListContext context)
        {
            _context = context;
        }

        //public async Task AddEmployee(EmployeeDetail employee)
        //{
        //    _context.EmployeeDetails.Add(employee);
        //    await _context.SaveChangesAsync();
        //}

        public IQueryable<EmployeeDetail> GetEmployees()
        {
            return _context.EmployeeDetails.Select(x => x);
        }

        public async Task<EmployeeDetail> GetEmployee(int id)
        {
            return await _context.EmployeeDetails.Where(x => x.EmployeeId == id).FirstOrDefaultAsync();
        }

        public async Task RemoveEmployee(EmployeeDetail employee)
        {
            _context.EmployeeDetails.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<string> Update(int id, EmployeeDetail employee)
        {
            if (id != employee.EmployeeId)
            {
                return "無已存在id";
            }

            _context.Entry(employee).State = EntityState.Modified;

            return $"更新id : {id} 資料完成";
        }

        public async Task<Employee> AddEmployee(EmployeeDetail employee)
        {
            _context.EmployeeDetails.Add(employee);
            await _context.SaveChangesAsync();

            return default;
        }
    }
}
