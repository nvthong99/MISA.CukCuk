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
            var validate = Validate(entity);
            if (validate)
                return _employeeResponsitory.Insert(entity);
            else
                return default;
        }

        public bool Update(Employee entity)
        {
            return _employeeResponsitory.Update(entity);
        }


        /// <summary>
        /// validate trước khi thêm vào database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>true: không có lỗi, false: có lỗi </returns>
        public bool Validate(Employee employee)
        {
            var isValidate = true;
            var checkCode = _employeeResponsitory.CkeckEmployee("Proc_GetEmployeeByCode", "EmployeeCode", employee.EmployeeCode);
            if (checkCode != null)
            {
                isValidate = false;
                throw new Exception("Bị trùng mã code với nhân viên " + checkCode.EmployeeName);
            }
            return isValidate;
        }

    }
}
