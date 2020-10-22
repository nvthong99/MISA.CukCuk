using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.cukcuk.Common.model
{
    public class Department
    {
        /// <summary>
        /// id phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
