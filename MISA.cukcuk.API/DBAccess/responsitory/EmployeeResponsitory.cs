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

        
    }
}
