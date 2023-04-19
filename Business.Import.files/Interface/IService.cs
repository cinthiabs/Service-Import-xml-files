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
        public string[] GetFiles(Folder folders);
        public Order FileReader(string file);
        public void ProcessOrder(string[] searchFiles, Folder folders);
        public bool ValidOrder(Order order,string File, Folder folders);
        public void MoveFile(string File, Folder folders, int sucess);
    }
}
