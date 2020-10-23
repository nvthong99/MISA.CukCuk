using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MISA.cukcuk.Bussiness.Interfaces;
using MISA.cukcuk.Common.model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.cukcuk.API.Controllers
{

    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        IEmployeeBussiness _employeeBussiness;
        public EmployeeController(IEmployeeBussiness employeeBussiness) : base(employeeBussiness)
        {
            _employeeBussiness = employeeBussiness;
        }
        #region Get dữ liệu
        // GET api/<EmployeeAPI>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var employee = _employeeBussiness.GetById(id);
            if (employee != null)
                return Ok(employee);
            else
                return NoContent();
        }

        //Get api/employee/employee-max-code
        [HttpGet("employee-max-code")]
        public string GetEmployeeMaxCode()
        {
            return _employeeBussiness.GetEmloyeeMaxCode();

        }
        //Get api/employee/numEmployee
        [HttpGet("num-paging")]
        public int GetNumEmployee()
        {
            return _employeeBussiness.GetNumEmployee();

        }
        //Get api/employee/maxRecord/recordBegin
        [HttpGet("{maxRecord}/{recordBegin}")]
        public IActionResult GetEmployeePaging(int maxRecord, int recordBegin)
        {
            var employee = _employeeBussiness.GetEmployeePaging(maxRecord, recordBegin);
            if (employee != null)
                return Ok(employee);
            else
                return NoContent();
        }

        #endregion
        // POST api/<EmployeeAPI>
        [HttpPost]
        public string Post([FromBody] Employee employee)
        {
            _employeeBussiness.Insert(employee);
            return "thành công";
        }

        // PUT api/<EmployeeAPI>/5
        [HttpPut]
        public IActionResult Put([FromBody] Employee employee)
        {
            var customerResult = _employeeBussiness.Update(employee);
            if (customerResult)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        // DELETE api/<EmployeeAPI>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var employeeResult = _employeeBussiness.DeleteById(id);
            if (employeeResult)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
