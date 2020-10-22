using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.cukcuk.API.model
{
    public class Enumeration
    {   
        /// <summary>
        /// enum giới tính
        /// </summary>
        public enum Gender 
        {
            Female = 0,
            Male = 1,
            Other = 2
        }

        /// <summary>
        /// enum tính trạng công việc
        /// </summary>
        public enum WorkStatus
        {
            Working = 0,
            Stop = 1,
            Probationary =2,
            Fresher = 3
            
        }
    }
}
