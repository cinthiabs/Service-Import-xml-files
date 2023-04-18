using Entities.Import.files.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Import.files.Interface
{
    public interface IQuery
    {
        public int QueryRequest(Order order);
        public bool InsertOrder(Order order);
        public bool InsertLog(string jsonString, string File, int sucess);
    }
}
