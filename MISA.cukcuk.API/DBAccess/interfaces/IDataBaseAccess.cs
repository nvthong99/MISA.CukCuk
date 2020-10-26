using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.cukcuk.DBAccess.interfaces
{
    public interface IDataBaseAccess<T>
    {
        /// <summary>
        /// Lấy ra danh sách đối tượng T từ database 
        /// </summary>
        /// CreatedBy: NVThong (26/10/2020)
        /// <returns>trả về  danh sách entity</returns>
        IEnumerable<T> GetList();

        /// <summary>
        /// Lấy đối tượng T từ DB bằng Id
        /// </summary>
        /// CreatedBy: NVThong (16/20/2020)
        /// <param name="id">id của phần tử cần lấy dữ liệu</param>
        /// <returns>trả về 1 đối tượng entity</returns>
        T GetById(Guid id);

        /// <summary>
        /// Thêm Phần tử vào database
        /// </summary>
        /// author: NVThong (16/10/2020)
        /// <param name="entity">dữ liệu cần thêm</param>
        /// <returns>true: thành công, false: thât bại</returns>
        bool Insert(T entity);

        /// <summary>
        /// xóa phần tử
        /// </summary>
        /// <param name="id">id của bản ghi cần xóa</param>
        /// <returns>true: thành công, false: thât bại</returns>
        bool DeleteById(Guid id);

        /// <summary>
        ///sửa thông tin phần tử trên database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true: thành công, false: thât bại</returns>
        /// 
        bool Update(T entity);

        /// <summary>
        /// mã lớn nhất của đối tượng
        /// </summary>
        /// <returns>giá trị mã lớn nhất</returns>
        string GetMaxCode();

        /// <summary>
        /// lấy danh sách entity dể phân trang
        /// </summary>
        /// <param name="maxRecord">số bản ghi tối đa trên 1 trang</param>
        /// <param name="recordBegin">bản ghi bắt đầu lấy </param>
        /// <returns>danh sách đối tượng được giới hạn </returns>
        IEnumerable<T> GetEntityPaging(int maxRecord, int recordBegin);

        /// <summary>
        /// lấy số bản ghi trong database
        /// </summary>
        /// <returns>số bản ghi</returns>
        int GetNumEntity();

        /// <summary>
        /// lấy ra đối tượng theo thuộc tính được truyền vào
        /// </summary>
        /// <param name="fieldName">tên thuộc tính</param>
        /// <param name="value">giá trị của trường lưu thuộc tính</param>
        /// <returns>trả về đối tượng có thuộc tính được truyền vào, null: không tìm thấy</returns>
        T CheckEntity(string storeName,string fieldName, object value);
        
    }
}
