using MISA.cukcuk.Bussiness.Interfaces;
using MISA.cukcuk.Common.model;
using MISA.cukcuk.DBAccess.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.cukcuk.Bussiness.Bussiness
{
    public class EmployeeBussiness : BaseBussiness<Employee>,IEmployeeBussiness
    {
        IEmployeeResponsitory _employeeResponsitory;
        public EmployeeBussiness(IEmployeeResponsitory employeeResponsitory):base(employeeResponsitory)
        {
            _employeeResponsitory = employeeResponsitory;
        }
        public bool DeleteById(Guid id)
        {
            return _employeeResponsitory.DeleteById(id);
        }

        public Employee GetById(Guid id)
        {
            return _employeeResponsitory.GetById(id);
        }

        public string GetEmloyeeMaxCode()
        {
            return _employeeResponsitory.GetEmloyeeMaxCode();
        }

        public IEnumerable<Employee> GetEmployeePaging(int maxRecord, int recordBegin)
        {
            return _employeeResponsitory.GetEmployeePaging(maxRecord, recordBegin);
        }

        public int GetNumEmployee()
        {
            return _employeeResponsitory.GetNumEmployee();
        }

        public bool Insert(Employee entity)
        {   
            return _employeeResponsitory.Insert(entity);
        }

        public bool Update(Employee entity)
        {
            return _employeeResponsitory.Update(entity);
        }
    }
}
