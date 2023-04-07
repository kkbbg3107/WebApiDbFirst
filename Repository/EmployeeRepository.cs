using WebApiDBFirst.Models;
using WebApiDBFirst.Services.InterfaceRepository;

namespace WebApiDBFirst.Repository
{
    public class EmployeeRepository : IRepository
    {
        private readonly TodoListContext _context;

        public EmployeeRepository(TodoListContext context)
        {
            _context = context;
        }

        public async Task AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Employee> GetEmployee()
        {
            return _context.Employees.Select(x => x);
        }

        //public async Task<string> UpdateEmployee(Employee employee)
        //{
        //    var result = _context.Employees.Where(x => x.Id == employee.Id).Select(x => x).FirstOrDefault();
            
        //    if (result is null)
        //    {
        //        return "沒有該id的已存在資料";
        //    }
        //}

        public async Task RemoveEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
