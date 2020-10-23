using MISA.cukcuk.Common.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.cukcuk.Bussiness.Interfaces
{
    public interface IEmployeeBussiness:IBaseBussiness<Employee>
    {
        /// <summary>
        /// lấy giá trị mã nhân viên lớn nhất
        /// </summary>
        /// <returns>EmployeeCode lớn nhất</returns>
        public string GetEmloyeeMaxCode();
        /// <summary>
        /// lấy danh sách nhân viên để phân trang
        /// </summary>
        /// <param name="maxRecord">sô bả ghi tối đa trên 1 trang</param>
        /// <param name="recordBegin">vị trí bắt đầu lấy dữ liệu</param>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeePaging(int maxRecord, int recordBegin);

        /// <summary>
        /// lấy số bản ghi trong database
        /// </summary>
        /// <returns>số bản ghi</returns>
        int GetNumEmployee();
    }
}
