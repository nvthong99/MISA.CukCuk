
using MISA.cukcuk.API.model;
using MISA.cukcuk.DBAccess.interfaces;
using MySql.Data.MySqlClient;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.cukcuk.API.DatabaseAccess
{
    public class MariaDBAccess<T> : IDataBaseAccess<T>
    {
        string connectionString;
        MySqlConnection _mySqlConnection;
        MySqlCommand _mySqlCommand;

   

        public MariaDBAccess()
        {
            connectionString = "User Id=nvmanh;PassWord=12345678@Abc;Host=35.194.166.58; Database=MISACukCuk_F09_NVThong; Character Set=utf8";
            _mySqlConnection = new MySqlConnection(connectionString);
            _mySqlConnection.Open();
            _mySqlCommand = _mySqlConnection.CreateCommand();
            _mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            
        }
        public bool DeleteById(Guid id)
        {
            var entityName = typeof(T).Name;
            _mySqlCommand.CommandText = $"Proc_Delete{entityName}ById";
            // gán đầu vào cho các giá trị trong store
            _mySqlCommand.Parameters.AddWithValue($"@{entityName}Id", id);
           
            var result = _mySqlCommand.ExecuteNonQuery();
            _mySqlConnection.Close();
            return true;
        }

        public T GetById(Guid id)
        {
            var ClassName = typeof(T).Name;
            // khai báo câu truy vấn
            _mySqlCommand.CommandText = $"proc_Get{ClassName}ById";

            _mySqlCommand.Parameters.AddWithValue($"@{ClassName}Id", id);
            MySqlDataReader mySqlDataReader = _mySqlCommand.ExecuteReader();
            var entity = Activator.CreateInstance<T>();
            while (mySqlDataReader.Read())
            {

                
                for (int i = 0; i < mySqlDataReader.FieldCount; i++)
                {
                    var columnName = mySqlDataReader.GetName(i);
                    var value = mySqlDataReader.GetValue(i);
                    var propertyInfo = entity.GetType().GetProperty(columnName);
                    if (propertyInfo != null && value != DBNull.Value)
                    {
                        propertyInfo.SetValue(entity, value);
                    }
                }
               
            }
            _mySqlConnection.Close();
            return entity;

        }

        public IEnumerable<T> GetList()
        {
            List<T> entitys = new List<T>();
            var className = typeof(T).Name;
            _mySqlCommand.CommandText = $"proc_Get{className}s";
            
            MySqlDataReader mySqlDataReader = _mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {

                var entity = Activator.CreateInstance<T>();
                for (int i = 0; i < mySqlDataReader.FieldCount; i++)
                {
                    var columnName = mySqlDataReader.GetName(i);
                    var value = mySqlDataReader.GetValue(i);
                    var propertyInfo = entity.GetType().GetProperty(columnName);
                    if (propertyInfo != null && value != DBNull.Value)
                    {
                        propertyInfo.SetValue(entity, value);
                    }
                }
                entitys.Add(entity);

            }
            _mySqlConnection.Close();
            return entitys;
        }

        public bool Insert(T entity)
        {
            var ClassName = typeof(T).Name;
            // khai báo câu truy vấn
            _mySqlCommand.CommandText = $"proc_Insert{ClassName}";

            var propeties = typeof(T).GetProperties();

            foreach (var propety in propeties)
            {
                var propetyName = propety.Name; // lấy tên thuộc tính
                var propetyValue = propety.GetValue(entity); // lấy giá trị của propety đó trong đối tượng truyền vào\
                if (propetyValue == null) propetyValue = DBNull.Value; //  chuyển về dạng null của database 
                _mySqlCommand.Parameters.AddWithValue($"@{propetyName}", propetyValue);
            }
            
            // thực thi công việc 
            var result = _mySqlCommand.ExecuteNonQuery();
            // đóng kết nối 
            _mySqlConnection.Close();
            return default;
        }

        public bool Update(T entity)
        {

            var ClassName = typeof(T).Name;
            // khai báo câu truy vấn
            _mySqlCommand.CommandText = $"proc_Update{ClassName}";

            var propeties = typeof(T).GetProperties();

            foreach (var propety in propeties)
            {
                var propetyName = propety.Name; // lấy tên thuộc tính
                var propetyValue = propety.GetValue(entity); // lấy giá trị của propety đó trong đối tượng truyền vào
                if (propetyValue == null) propetyValue = DBNull.Value;
                _mySqlCommand.Parameters.AddWithValue($"@{propetyName}", propetyValue);
            }

            // thực thi công việc 
            var result = _mySqlCommand.ExecuteNonQuery();
            // đóng kết nối 
            _mySqlConnection.Close();
            return true;
        }


        public string GetMaxCode()
        {
            var className = typeof(T).Name;
            _mySqlCommand.CommandText = $"proc_Get{className}s";
            string value=null;
            MySqlDataReader mySqlDataReader = _mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                    value = (string)mySqlDataReader.GetValue(0);
            }
            _mySqlConnection.Close();
            return value;
        }
    }
}
