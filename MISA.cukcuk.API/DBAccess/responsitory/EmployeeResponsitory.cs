using MISA.cukcuk.Common.model;
using MISA.cukcuk.DBAccess.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.cukcuk.DBAccess.responsitory
{
    public class EmployeeResponsitory:BaseResponsitory<Employee>, IEmployeeResponsitory
    {
        
        public EmployeeResponsitory(IDataBaseAccess<Employee> dataBaseAccess) : base(dataBaseAccess)
        {
           
        }

        public string GetEmloyeeMaxCode()
        {
            return _dataBaseAccess.GetMaxCode();
        }

        public IEnumerable<Employee> GetEmployeePaging(int maxRecord, int recordBegin)
        {
            return _dataBaseAccess.GetEntityPaging(maxRecord, recordBegin);
        }

        public int GetNumEmployee()
        {
            return _dataBaseAccess.GetNumEntity();
        }
    }
}
