using System;
using System.Collections.Generic;
using System.Text;
using static MISA.cukcuk.API.model.Enumeration;

namespace MISA.cukcuk.Common.model
{
    public class Employee
    {   
        /// <summary>
        /// id Nhân Viên
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string  EmployeeCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// Ngày sinh nhân viên
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Gender.Female:
                        return "Nữ";
                    case Gender.Male:
                        return "Nam";
                    case Gender.Other:
                        return "Khác";
                    default:
                        return "Không xác định";
                }
            }
        }
        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// số chứng minh nhân dân/ thẻ căn cước
        /// </summary>
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// ngày cấp chứng minh
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// nơi cấp chứng minh 
        /// </summary>
        public string? IdentityPlace { get; set; }
        /// <summary>
        /// Id vị trí
        /// </summary>
        public Guid PossitionId { get; set; }
        /// <summary>
        /// tên vị trí
        /// </summary>
        public string? PossitionName { get; set; }
        /// <summary>
        /// id  phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// tên phòng ban
        /// </summary>
        public string?  DepartmentName { get; set; }
        /// <summary>
        /// mã số thuế
        /// </summary>
        public string? TaxCode { get; set; }
        /// <summary>
        /// tiền lương
        /// </summary>
        public double? Salary { get; set; }
        /// <summary>
        /// ngày  tham gia
        /// </summary>
        public DateTime? JoinDate { get; set; }
        /// <summary>
        /// tính trạng công việc
        /// </summary>
        public WorkStatus WorkStatus { get; set; }
        /// <summary>
        /// Tên tình trạng công việc
        /// </summary>
        public string WorkStatusName 
        {
            get
            {
                
                
                    switch (WorkStatus)
                    {
                        case WorkStatus.Working:
                            return "Đang làm việc";
                        case WorkStatus.Stop:
                            return "Đã nghỉ việc";
                        case WorkStatus.Probationary:
                            return "Đang thử việc";
                        case WorkStatus.Fresher:
                            return "Thực tập";
                        default:
                            return "Không xác định";
                    }
                

            }
        }
    }
}
