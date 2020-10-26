
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


        #region Constructer khởi tạo connection đến DataBase
        public MariaDBAccess()
        {
            connectionString = "User Id=nvmanh;PassWord=12345678@Abc;Host=35.194.166.58; Database=MISACukCuk_F09_NVThong; Character Set=utf8";
            _mySqlConnection = new MySqlConnection(connectionString);
            _mySqlConnection.Open();
            _mySqlCommand = _mySqlConnection.CreateCommand();
            _mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            
        }
        #endregion

        #region Xóa dữ liệu
        public bool DeleteById(Guid id)
        {
            var entityName = typeof(T).Name;
            _mySqlCommand.CommandText = $"Proc_Delete{entityName}ById";
            // gán đầu vào cho các giá trị trong store
            _mySqlCommand.Parameters.AddWithValue($"@{entityName}Id", id);
           
            var result = _mySqlCommand.ExecuteNonQuery();
            _mySqlConnection.Close();
            if (result > 0)
            {
                return true;
            }
            else return false;
        }
        #endregion

        #region Lấy dữ liệu
        public T GetById(Guid id)
        {
            var ClassName = typeof(T).Name;
            // khai báo câu truy vấn
            _mySqlCommand.CommandText = $"Proc_Get{ClassName}ById";

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

        public T CheckEntity(string storeName, string fieldName, object value)
        {

            // khai báo câu truy vấn
            _mySqlCommand.CommandText = storeName;
            _mySqlCommand.Parameters.AddWithValue($"@{fieldName}", value);
            MySqlDataReader mySqlDataReader = _mySqlCommand.ExecuteReader();
            
            while (mySqlDataReader.Read())
            {

                var entity = Activator.CreateInstance<T>();
                for (int i = 0; i < mySqlDataReader.FieldCount; i++)
                {
                    var columnName = mySqlDataReader.GetName(i);
                    var val = mySqlDataReader.GetValue(i);
                    var propertyInfo = entity.GetType().GetProperty(columnName);
                    if (propertyInfo != null && val != DBNull.Value)
                    {
                        propertyInfo.SetValue(entity, val);
                    }
                }
                _mySqlConnection.Close();
                return entity;
            }
            _mySqlConnection.Close();
            return default;
        }

        public IEnumerable<T> GetList()
        {
            List<T> entitys = new List<T>();
            var className = typeof(T).Name;
            _mySqlCommand.CommandText = $"Proc_Get{className}s";
            
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
        public string GetMaxCode()
        {
            var className = typeof(T).Name;
            _mySqlCommand.CommandText = $"Proc_Get{className}CodeMax";
            string value = null;
            MySqlDataReader mySqlDataReader = _mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                value = (string)mySqlDataReader.GetValue(0);
            }
            _mySqlConnection.Close();
            return value;
        }

        public IEnumerable<T> GetEntityPaging(int maxRecord, int recordBegin)
        {

            List<T> entitys = new List<T>();
            var className = typeof(T).Name;
            _mySqlCommand.CommandText = $"Proc_Get{className}Paging";
            _mySqlCommand.Parameters.AddWithValue($"@maxRecord", maxRecord);
            _mySqlCommand.Parameters.AddWithValue($"@recordBegin", recordBegin);

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

        public int GetNumEntity()
        {
            _mySqlCommand.Parameters.Clear();
            var className = typeof(T).Name;
            _mySqlCommand.CommandText = $"Proc_GetNum{className}sPaging";
            int value = 0;
            MySqlDataReader mySqlDataReader = _mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                value = Convert.ToInt32(mySqlDataReader.GetValue(0));
            }
            _mySqlConnection.Close();
            return value;
        }

     


        #endregion

        #region Thêm dữ liệu
        public bool Insert(T entity)
        {
            _mySqlConnection.Open();
            _mySqlCommand.Parameters.Clear();
            var ClassName = typeof(T).Name;
            // khai báo câu truy vấn
            _mySqlCommand.CommandText = $"Proc_Insert{ClassName}";
            
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
            if (result > 0)
            {
                return true;
            }
            else return false;
            
        }
        #endregion

        #region Sửa dữ liệu
        public bool Update(T entity)
        {
            _mySqlCommand.Parameters.Clear();
            var ClassName = typeof(T).Name;
            // khai báo câu truy vấn
            _mySqlCommand.CommandText = $"Proc_Update{ClassName}";

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
            if (result > 0)
            {
                return true;
            }
            else return false;
        }


        #endregion


    }
}
