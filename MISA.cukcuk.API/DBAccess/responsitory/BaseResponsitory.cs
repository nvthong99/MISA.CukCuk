using MISA.cukcuk.DBAccess.interfaces;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.cukcuk.DBAccess.responsitory
{
    public class BaseResponsitory<T>:IBaseResponsitory<T>
    {
        public IDataBaseAccess<T> _dataBaseAccess;
        public BaseResponsitory(IDataBaseAccess<T> dataBaseAccess)
        {
            _dataBaseAccess = dataBaseAccess;
        }
        public bool DeleteById(Guid id)
        {
            return _dataBaseAccess.DeleteById(id);
        }

        public T GetById(Guid id)
        {
            return _dataBaseAccess.GetById(id);
        }

        public IEnumerable<T> GetList()
        {
            return _dataBaseAccess.GetList();
        }

        public bool Insert(T entity)
        {
            return _dataBaseAccess.Insert(entity);
        }

        public bool Update(T entity)
        {
            return _dataBaseAccess.Update(entity);
        }
    }
}
