using MISA.cukcuk.Common.model;
using MISA.cukcuk.DBAccess.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.cukcuk.DBAccess.responsitory
{
    public class PossitionResponsitory:BaseResponsitory<Possition>,IPossitionResponsitory
    {
        public PossitionResponsitory(IDataBaseAccess<Possition> dataBaseAccess):base(dataBaseAccess)
        {

        }
    }
}
