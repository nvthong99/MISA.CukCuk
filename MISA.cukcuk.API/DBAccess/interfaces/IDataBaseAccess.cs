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
        /// <returns></returns>
        bool Insert(T entity);

        /// <summary>
        /// xóa phần tử
        /// </summary>
        /// <param name="id">id của bản ghi cần xóa</param>
        /// <returns></returns>
        bool DeleteById(Guid id);

        /// <summary>
        ///sửa thông tin phần tử trên database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        bool Update(T entity);

        /// <summary>
        /// mã lớn nhất của đối tượng
        /// </summary>
        /// <returns></returns>
        string GetMaxCode();

        /// <summary>
        /// lấy danh sách entity dể phân trang
        /// </summary>
        /// <param name="maxRecord">số bản ghi tối đa trên 1 trang</param>
        /// <param name="recordBegin">bản ghi bắt đầu lấy </param>
        /// <returns></returns>
        IEnumerable<T> GetEntityPaging(int maxRecord, int recordBegin);

        /// <summary>
        /// lấy số bản ghi trong database
        /// </summary>
        /// <returns>số bản ghi</returns>
        int GetNumEntity();
    }
}
