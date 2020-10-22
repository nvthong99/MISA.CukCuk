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
        public EmployeeController(IEmployeeBussiness employeeBussiness):base(employeeBussiness)
        {
            _employeeBussiness = employeeBussiness;
        }
    
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

        // POST api/<EmployeeAPI>
        [HttpPost]
        public string Post([FromBody] Employee employee)
        {
            _employeeBussiness.Insert(employee);
            return "thành công";
        }

        // PUT api/<EmployeeAPI>/5
        [HttpPut]
        public IActionResult Put( [FromBody] Employee employee)
        {
            var customerResult =  _employeeBussiness.Update(employee);
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
