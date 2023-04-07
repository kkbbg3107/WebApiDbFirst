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
    public class EmployeesDetailController : ControllerBase
    {
        private readonly IService _service;

        public EmployeesDetailController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeDTO>> GetEmployeeDTO()
        {
            var result = await _service.QueryAll();

            var resultDto = result.Select(x => new EmployeeDTO()
            {
                Id = x.EmployeeId,
                Name = x.Name,
            });

            return resultDto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var source = await _service.QueryItemSth(id);
            if (source is null)
            {
                return NotFound("查無該id");
            }

            return Ok(source);
        }

        // 新增 EmployeeDetail 資料的 API
        [HttpPost("Add/{id}")]
        public async Task<ActionResult<EmployeeDetail>> CreateEmployeeDetail([FromBody] EmployeeDetail employeeDetail)
        {
            // 取得對應的 Employee 物件
            //var employee = await _service.Add(employeeDetail);

            // 設定 Employee 屬性
            //employeeDetail.Employee = employee;

            // 儲存 EmployeeDetail 物件
            _service.AdditemSth(employeeDetail);            

            // 回傳建立的 EmployeeDetail 物件
            return NoContent();
        }


        [HttpPost("{id}")]
        [BlackListFilter(new string[] { "kk", "ss" })]
        public ActionResult<EmployeeDetail> AddEmployee(EmployeeDetail emp)
        {
            return Ok(_service.AdditemSth(emp));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var result = await _service.QueryItemSth(id);
            if (result is null)
            {
                return NotFound("無此id");
            }
            
            await _service.RemoveSth(result);
            return Ok("已刪除該項目");
        }       
    }
}
