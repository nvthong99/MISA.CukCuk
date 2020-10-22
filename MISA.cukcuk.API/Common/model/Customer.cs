using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MISA.cukcuk.API.model.Enumeration;

namespace MISA.cukcuk.API.model
{
    public class Customer
    {
        public Customer()
        {
            CustomerId = Guid.NewGuid();
        }
        public Guid CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string TaxCode { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CardClass { get; set; }
        public Guid CustomerGroupId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double DebitMoney { get; set; }
        public Gender Gender { get; set; }
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

        public string Note { get; set; }
        public string MemberCard { get; set; }

    }
}
