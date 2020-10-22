using MISA.cukcuk.Bussiness.Interfaces;
using MISA.cukcuk.Common.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.cukcuk.API.Controllers
{
    public class PossitionController:BaseController<Possition>
    {
        IPossitionBussiness _possitionBussiness;
        public PossitionController(IPossitionBussiness possitionBussiness):base(possitionBussiness)
        {
            _possitionBussiness = possitionBussiness;
        }
    }
}
