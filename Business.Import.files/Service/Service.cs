using Business.Import.files.Interface;
using Entities.Import.files.Entities;
using Infrastructure.Import.files.Interface;
using Infrastructure.Import.files.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Business.Import.files.Service
{
    public class Service : IService
    {
        private readonly IQuery _Query;
        private readonly IConfiguration _Configuration;
        public Service(IQuery Query, IConfiguration Configuration)
        {
            _Query = Query;
            _Configuration = Configuration;
        }

        public void Execute()
        {
            Folder folder = new Folder()
            {
                inputFolder  = "C:\\Files\\",
                outputFolder = "C:\\Files\\Output\\",
                errorFolder  = "C:\\Files\\Error\\"
            };

            var searchFiles = GetFiles(folder);
            var count = searchFiles.Count();
            if (count != 0)
            {
                ProcessOrder(searchFiles, folder);
            }
            else Console.WriteLine("empty folder!"); 
        }
        public void ProcessOrder(string[] searchFiles, Folder folders)
        {
            for (var file = 0; file < searchFiles.Length; file++)
            {
                var reader = FileReader(searchFiles[file]);
                if (reader.Document == null)
                {
                    MoveFile(searchFiles[file], folders, 0);
                    _Query.InsertLog("Error in the order!", searchFiles[file], 0);
                    Console.WriteLine($@"Error in the order! {searchFiles[file]}");
                }
                else
                {
                    ValidOrder(reader, searchFiles[file], folders);
                }
            }
        }
        public string[] GetFiles(Folder file)
        {
            string[] files = Directory.GetFiles(file.inputFolder);
            return files;
        }
        public bool ValidOrder(Order order, string File, Folder folders)
        {
            var query = true;
            try
            {
                int validation =  _Query.QueryRequest(order);
                if (validation == 0)
                {
                    query = _Query.InsertOrder(order);
                    if (query == true)
                    {
                        var jsonString = JsonSerializer.Serialize(order);
                        _Query.InsertLog(jsonString, File, 1);
                        MoveFile(File, folders,1);
                        Console.WriteLine($@"Imported order! {order.Document}");
                    }
                    else
                    {
                        MoveFile(File, folders, 0);
                        Console.WriteLine($@"Error in the order! {order.Document}");
                    }
                }
                else 
                {
                    MoveFile(File,folders,1);
                    Console.WriteLine("Order already imported, moved");
                }
            }
            catch (Exception Ex)
            {
                MoveFile(File, folders, 0);
                Console.WriteLine($@"Error in the order! {order.Document}");
                _Query.InsertLog("Error", Ex.Message, 0);
            }

            return query;
        }
        public void MoveFile(string File, Folder folders,int sucess)
        {
            string config = "";
            var filename = File.Split('\\');
            string name = filename[filename.Length - 1];
          
            if (sucess == 1)
            {
                config = folders.outputFolder;
                if (!Directory.Exists(config))
                {
                    Directory.CreateDirectory(config);
                }
                else
                {
                    FileInfo file = new FileInfo(config + name);
                    if (file.Exists) file.Delete();
                }
            }            
            else 
            { 
                config = folders.errorFolder;
                if (!Directory.Exists(config))
                {
                    Directory.CreateDirectory(config);
                }
                else
                {
                    FileInfo file = new FileInfo(config + name);
                    if (file.Exists) file.Delete();
                }
            }

            Directory.Move(folders.inputFolder + name, config + name);
        }
        public Order FileReader(string file)
        {
            Order order = new Order();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(file);

                XmlNodeList ide = xmlDoc.GetElementsByTagName("ide");
                XmlNodeList emit = xmlDoc.GetElementsByTagName("emit");
                XmlNodeList enderEmit = xmlDoc.GetElementsByTagName("enderEmit");
                XmlNodeList dest = xmlDoc.GetElementsByTagName("dest");
                XmlNodeList enderDest = xmlDoc.GetElementsByTagName("enderDest");
                XmlNodeList prod = xmlDoc.GetElementsByTagName("prod");
                XmlNodeList ICMSTot = xmlDoc.GetElementsByTagName("ICMSTot");
                XmlNodeList vol = xmlDoc.GetElementsByTagName("vol");
                XmlNodeList infProt = xmlDoc.GetElementsByTagName("infProt");

                order = new Order()
                {
                    SeriesNFE =                ide[0]["serie"].InnerText,
                    NumberNF  =                ide[0]["nNF"].InnerText,
                    Document =                 ide[0]["nNF"].InnerText,
                    DateNF =                   ide[0]["dhEmi"].InnerText,
                    TpNF =                     ide[0]["tpNF"].InnerText,
                    Cod_Mun =                  ide[0]["cMunFG"].InnerText,
                    Sender_identification =    emit[0]["CNPJ"].InnerText,
                    Sender_name =              emit[0]["xNome"].InnerText,
                    Sender_ie =                emit[0]["IE"].InnerText,
                    Sender_address =           enderEmit[0]["xLgr"].InnerText,
                    Sender_number =            enderEmit[0]["nro"].InnerText,
                    Sender_neighborhood =      enderEmit[0]["xBairro"].InnerText,
                    Sender_county =            enderEmit[0]["xMun"].InnerText,
                    Sender_uf =                enderEmit[0]["UF"].InnerText,
                    Sender_cep =               enderEmit[0]["CEP"].InnerText,
                    Addressee_identification = dest[0]["CPF"].InnerText,
                    Addressee_name =           dest[0]["xNome"].InnerText,
                    Addressee_address =        enderDest[0]["xLgr"].InnerText,
                    Addressee_number =         enderDest[0]["nro"].InnerText,
                    Addressee_neighborhood =   enderDest[0]["xBairro"].InnerText,
                    Addressee_county =         enderDest[0]["xMun"].InnerText,
                    Addressee_uf =             enderDest[0]["UF"].InnerText,
                    Addressee_cep =            enderDest[0]["CEP"].InnerText,
                    Additional_information =   prod[0]["xProd"].InnerText,
                    Value =                    ICMSTot[0]["vNF"].InnerText,
                    KeyNF =                    infProt[0]["chNFe"].InnerText
                };

                    if (String.IsNullOrEmpty(vol[0]["qVol"].InnerText))
                    {
                        order.Volume = 1;
                    }
                    else order.Volume = Convert.ToInt32(vol[0]["qVol"].InnerText);
                    if (String.IsNullOrEmpty(vol[0]["pesoB"].InnerText))
                    {
                        order.Weight = "0";
                    }
                    else order.Weight = vol[0]["pesoB"].InnerText;
            }
            catch (Exception Ex)
            {
                 Console.WriteLine($@"Error in the order! {order.Document}");
                _Query.InsertLog("Error", Ex.Message, 0);
            }

            return order;
        }
    }
}
