using MISA.cukcuk.Bussiness.Interfaces;
using MISA.cukcuk.Common.model;
using MISA.cukcuk.DBAccess.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.cukcuk.Bussiness.Bussiness
{
    public class PossitionBussiness:BaseBussiness<Possition>, IPossitionBussiness
    {
        public PossitionBussiness(IPossitionResponsitory positionResponsitory) : base(positionResponsitory)
        {

        }

        public bool DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Possition GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Possition entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Possition entity)
        {
            throw new NotImplementedException();
        }
    }
}
