using Entities.Import.files.Entities;
using Infrastructure.Import.files.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Import.files.Query
{
    public class Query : IQuery
    {
        public bool InsertLog(Order order, string outputFolder, int sucess)
        {
            throw new NotImplementedException();
        }

        public bool InsertOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool QueryRequest(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
