using MISA.cukcuk.Common.model;
using MISA.cukcuk.DBAccess.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.cukcuk.DBAccess.responsitory
{
    public class DepartmentResponsitory : BaseResponsitory<Department>,IDepartmentResponsitory
    {
        public DepartmentResponsitory(IDataBaseAccess<Department> dataBaseAccess) : base(dataBaseAccess)
        {

        }
    }
}
