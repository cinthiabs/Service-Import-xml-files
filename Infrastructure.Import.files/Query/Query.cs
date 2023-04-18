using Entities.Import.files.Entities;
using Infrastructure.Import.files.Data;
using Infrastructure.Import.files.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Import.files.Query
{
    public class Query : Connection,IQuery 
    {
        public bool InsertLog(string jsonString, string File, int sucess)
        {
            var data = DateTime.Now.ToString("yyyy-dd-MM HH:mm:ss");
            string sqlQuery = $@"Insert into LogIntegration values('ServiceImportFile','{jsonString}','{File}','{sucess}','{data}');";
            var retorno = ExecuteCommandString(sqlQuery);
            return retorno;
        }

        public bool InsertOrder(Order order)
        {
            var retorno = false;
            var data = DateTime.Now.ToString("yyyy-dd-MM HH:mm:ss");
            try
            {
               var sqlQuery = $@"Insert into Ordertable (
                        document
                        ,keyNF
                        ,numberNF
                        ,seriesNFE
                        ,tpNF
                        ,cod_Mun
                        ,dateNF
                        ,date_Inclusion
                        ,sender_name
                        ,sender_identification
                        ,sender_ie
                        ,sender_address
                        ,sender_number
                        ,sender_neighborhood
                        ,sender_county
                        ,sender_cep
                        ,sender_uf
                        ,addressee_name
                        ,addressee_identification
                        ,addressee_ie
                        ,addressee_address
                        ,addressee_number
                        ,addressee_neighborhood
                        ,addressee_county
                        ,addressee_cep
                        ,addressee_uf
                        ,additional_information
                        ,value
                        ,volume
                        ,weight)

                Values( '{order.Document}',
                        '{order.KeyNF}',
                        '{order.NumberNF}',
                        '{order.SeriesNFE}',
                        '{order.TpNF}',
                        '{order.Cod_Mun}',
                        '{order.DateNF}',
                        '{data}',
                        '{order.Sender_name}',
                        '{order.Sender_identification}',
                        '{order.Sender_ie}',
                        '{order.Sender_address}',
                        '{order.Sender_number}',
                        '{order.Sender_neighborhood}',
                        '{order.Sender_county}',
                        '{order.Sender_cep}',
                        '{order.Sender_uf}',
                        '{order.Addressee_name}',
                        '{order.Addressee_identification}',
                        '{order.Addressee_ie}',
                        '{order.Addressee_address}',
                        '{order.Addressee_number}',
                        '{order.Addressee_neighborhood}',
                        '{order.Addressee_county}',
                        '{order.Addressee_cep}',
                        '{order.Addressee_uf}',
                        '{order.Additional_information}',
                        '{order.Value}',
                        '{order.Volume}',
                        '{order.Weight}'
                        );";
                retorno = ExecuteCommandString(sqlQuery);
            }
            catch (Exception Ex)
            {
                InsertLog("Error", Ex.Message,0);
            }
            return retorno;
        }

        public int QueryRequest(Order order)
        {
            var retornInt = 0;
            try
            {
                string query = $@"select count(0) from Ordertable where keyNF='{order.KeyNF}' and document ='{order.Document}'";
                 retornInt = ExecuteCommandInt<int>(query);
            }
            catch (Exception Ex)
            {
                InsertLog("Error", Ex.Message, 0);
            }
            return retornInt;
        }
    }
}
