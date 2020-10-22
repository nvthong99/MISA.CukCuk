using MISA.cukcuk.DBAccess.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.cukcuk.Bussiness.Bussiness
{
    public class BaseBussiness<T>
    {
        IBaseResponsitory<T> _baseResponsitory;
        public BaseBussiness(IBaseResponsitory<T> baseResponsitory) {
            _baseResponsitory = baseResponsitory;
        }
        public IEnumerable<T> GetList()
        {
            return _baseResponsitory.GetList();
        }
    }
}
