using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Import.files.Entities
{
    public class Log
    {
        public string? application { get; set; }
        public string? json { get; set; }    
        public string? queryReturn { get; set; }
        public int sucess { get; set; }
        public DateTime inclusionDate { get; set; }

    }
}
