using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDBFirst.AuthorizationFilter;
using WebApiDBFirst.Controllers.InterfaceService;
using WebApiDBFirst.DTOModels;
using WebApiDBFirst.Models;

namespace WebApiDBFirst.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {        
        private readonly IService _service;

        public EmployeesController(IService service)
        {
            _service = service;           
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeDTO>> GetEmployeeDTO()
        {           
            var result = await _service.FilterColumn();

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var source = await _service.FilterColumn();
            if (!source.Any(x => x.Id == id))
            {
                return NotFound("查無該id");
            }

            return Ok(source.First(x => x.Id == id));
        }

        [HttpPost("{id}")]
        [BlackListFilter(new string[] { "kk", "ss" })]
        public async Task<ActionResult<Employee>> AddEmployee(Employee emp)
        {
            var source = await _service.FilterColumn();
            if (source.Select(x => x.Id).Contains(emp.Id))
            {
                return NoContent();
            }

            return Ok(_service.AdditemSth(emp));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var result = await _service.EmpolyeeNothing();
            if (!result.Select(x => x.Id).Contains(id))
            {
                return NotFound("無此id");
            }
            var emp = result.Where(x => x.Id == id).First();
            await _service.RemoveSth(emp);
            return Ok("已刪除該項目");
        }       
    }
}
