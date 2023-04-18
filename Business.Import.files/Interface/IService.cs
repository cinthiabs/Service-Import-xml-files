using Entities.Import.files.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Import.files.Interface
{
    public interface IService
    {
        public void Execute();
        public string[] GetFiles(string file);
        public Order FileReader(string file);
        public void ProcessOrder(string[] searchFiles, string outputFolder);
        public bool ValidOrder(Order order, string outputFolder, string File);
        public void MoveFile(string File, string outputFolder, int sucess);
    }
}
