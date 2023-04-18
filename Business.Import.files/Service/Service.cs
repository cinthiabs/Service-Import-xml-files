using Business.Import.files.Interface;
using Entities.Import.files.Entities;
using Infrastructure.Import.files.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Business.Import.files.Service
{
    public class Service : IService
    {
        private readonly Query _Query;
        public Service(Query Querys)
        {
            _Query = Querys;
        }

        public void Execute()
        {
            var inputFolder = "C:\\Files";
            var outputFolder = "C:\\Files\\Output";

            var searchFiles = GetFiles(inputFolder);
            if (searchFiles != null)
            {
                ProcessOrder(searchFiles, outputFolder);
            }
            else Console.Write("empty folder!"); 
        }
        public void ProcessOrder(string[] searchFiles, string outputFolder)
        {
            for (var file = 0; file < searchFiles.Length; file++)
            {
                var reader = FileReader(searchFiles[file]);
                ValidOrder(reader, outputFolder , searchFiles[file]);
            }
        }
        public string[] GetFiles(string file)
        {
            string[] files = Directory.GetFiles(file);

            return files;
        }
        public bool ValidOrder(Order order , string outputFolder , string File)
        {
            var query = true;
            _Query.QueryRequest(order);
            if (query == false)
            {
                bool insert = _Query.InsertOrder(order);
                if (insert == true)
                {
                   // InsertLog(order, File, 1);
                    MoveFile(File, outputFolder, 1);
                }
            }
            return query;
        }
        public void MoveFile(string File, string outputFolder, int sucess)
        {
            string dadosConfig = "";
            var filename = File.Split('\\');
            string name = filename[filename.Length - 1];
            Directory.Move(outputFolder + name, dadosConfig + name);
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
                    else
                    {
                        order.Volume = Convert.ToInt32(vol[0]["qVol"].InnerText);
                    }
                    if (String.IsNullOrEmpty(vol[0]["pesoB"].InnerText))
                    {
                        order.Weight = "0";
                    }
                    else
                    {
                        order.Weight = vol[0]["pesoB"].InnerText;
                    }

            }
            catch (Exception Ex)
            {
              _ =  Ex;
            }
           
            return order;
        }
    }
}
