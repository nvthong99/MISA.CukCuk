using MISA.cukcuk.Common.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.cukcuk.DBAccess.interfaces
{
    public interface IEmployeeResponsitory:IBaseResponsitory<Employee>
    {
        /// <summary>
        /// lấy giá trị mã nhân viên lớn nhất từ database
        /// </summary>
        /// <returns>EmployeeCode lớn nhất</returns>
        public string GetEmloyeeMaxCode();
        /// <summary>
        /// lấy danh sách nhân viên để phần trang
        /// </summary>
        /// <param name="maxRecord">số phần tử tối đa trên 1 trang</param>
        /// <param name="recordBegin">vị trí bắt đầu</param>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeePaging(int maxRecord, int recordBegin);

        /// <summary>
        /// lấy số bản ghi trong database
        /// </summary>
        /// <returns>số bản ghi</returns>
        int GetNumEmployee();
    }
}
