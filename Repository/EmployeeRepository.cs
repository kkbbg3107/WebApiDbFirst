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

        public async Task RemoveEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
