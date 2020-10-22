using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MISA.cukcuk.API.DatabaseAccess;

using MISA.cukcuk.API.model;
using MISA.cukcuk.DBAccess.interfaces;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.cukcuk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        IDataBaseAccess<Customer> _mariaDBAccess;

        public CustomerController(IDataBaseAccess<Customer> mariaDBAccess)
        {
            _mariaDBAccess = mariaDBAccess;
        }

        
      
        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _mariaDBAccess.GetList();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {   
            return "value";
        }

        // POST api/<CustomerController>
        [HttpPost]
        public bool Post(Customer customer)
        {
            return _mariaDBAccess.Insert(customer);
        }

        // PUT api/<CustomerController>
        [HttpPut]
        public Customer Put([FromBody] Customer customer)
        {

            /*customer.CustomerId = Guid.NewGuid();
            Customer.CustomerList.Add(customer);*/
            // khởi tạo connection
            string connectionString = "User Id=nvmanh;PassWord=12345678@Abc;Host=35.194.166.58; Database=MISACukCuk_F09_NVThong; Character Set=utf8";


            // kết nối đến database
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            // khởi tạo đối tượng command thao tác với cơ sở dữ liệu
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // khai báo câu truy vấn
            mySqlCommand.CommandText = "Proc_UpdateCustomerById";
            // gán đầu vào cho các giá trị trong store

            mySqlCommand.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
            mySqlCommand.Parameters.AddWithValue("@CustomerCode", customer.CustomerCode);
            mySqlCommand.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
            mySqlCommand.Parameters.AddWithValue("@MemberCard", customer.MemberCard);
            mySqlCommand.Parameters.AddWithValue("@CardClass", customer.CardClass);
            mySqlCommand.Parameters.AddWithValue("@CustomerGroupId", customer.CustomerGroupId);
            mySqlCommand.Parameters.AddWithValue("@Gender", customer.Gender);
            mySqlCommand.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
            mySqlCommand.Parameters.AddWithValue("@DebitMoney", customer.DebitMoney);
            mySqlCommand.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
            mySqlCommand.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
            mySqlCommand.Parameters.AddWithValue("@TaxCode", customer.TaxCode);
            mySqlCommand.Parameters.AddWithValue("@Email", customer.Email);
            mySqlCommand.Parameters.AddWithValue("@Address", customer.Address);
            mySqlCommand.Parameters.AddWithValue("@Note", customer.Note);
            mySqlCommand.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            // mở kết nối đến databse
            mySqlConnection.Open();
            // thực thi công việc 
            var result = mySqlCommand.ExecuteNonQuery();
            // đóng kết nối 
            mySqlConnection.Close();
            return customer;
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public string Delete(Guid id)
        {
            // khởi tạo connection
            string connectionString = "User Id=nvmanh;PassWord=12345678@Abc;Host=35.194.166.58; Database=MISACukCuk_F09_NVThong; Character Set=utf8";

            // kết nối đến database
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            // khởi tạo đối tượng command thao tác với cơ sở dữ liệu
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            // khai báo câu truy vấn
            mySqlCommand.CommandText = "Proc_DeleteCustomerById";
            // gán đầu vào cho các giá trị trong store
            mySqlCommand.Parameters.AddWithValue("@CustomerId", id);
            mySqlConnection.Open();
            var result = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return "done";

        }
    }
}
