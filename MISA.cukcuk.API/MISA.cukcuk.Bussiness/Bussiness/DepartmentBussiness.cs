using MISA.cukcuk.Bussiness.Interfaces;
using MISA.cukcuk.Common.model;
using MISA.cukcuk.DBAccess.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.cukcuk.Bussiness.Bussiness
{
    public class DepartmentBussiness : BaseBussiness<Department>, IDepartmentBussiness
    {
        public DepartmentBussiness(IDepartmentResponsitory departmentResponsitory):base(departmentResponsitory)
        {

        }

        public bool DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Department GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Department entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
