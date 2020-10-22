using Microsoft.AspNetCore.Mvc;
using MISA.cukcuk.Bussiness.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.cukcuk.API.Controllers
{
    [Route("api/[controller]")]
    public class BaseController<T>: ControllerBase
    {
        IBaseBussiness<T> _baseBussiness;
        public BaseController(IBaseBussiness<T> baseBussiness)
        {
            _baseBussiness = baseBussiness;
        }
        [HttpGet]
        public ActionResult<T> Get()
        {
            var listEntitys = _baseBussiness.GetList();
            if (listEntitys.Count() > 0)
                return Ok(listEntitys);
            else
                return NoContent();
        }
    }
}
