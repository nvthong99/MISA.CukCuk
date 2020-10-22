using MISA.cukcuk.Bussiness.Interfaces;
using MISA.cukcuk.Common.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.cukcuk.API.Controllers
{
    public class DepartmentController:BaseController<Department>
    {
        IDepartmentBussiness _departmentBussiness;
        public DepartmentController(IDepartmentBussiness departmentBussiness) : base(departmentBussiness)
        {
            _departmentBussiness = departmentBussiness;
        }
    }
}
