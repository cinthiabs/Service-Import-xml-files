using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Import.files.Entities
{
    public class Order
    {
	    public string? Document { get; set; }
        public string? KeyNF { get; set; }
        public string? NumberNF { get; set; }
        public string? SeriesNFE { get; set; }
        public string? TpNF { get; set; }
        public string? Cod_Mun { get; set; }
        public string? DateNF { get; set; }
        public DateTime Date_Inclusion { get; set; }
        public string? Sender_name { get; set; }
        public string? Sender_identification { get; set; }
        public string? Sender_ie { get; set; }
        public string? Sender_address { get; set; }
        public string? Sender_number { get; set; }
        public string? Sender_neighborhood { get; set; }
        public string? Sender_county { get; set; }
        public string? Sender_cep { get; set; }
        public string? Sender_uf { get; set; }
        public string? Addressee_name { get; set; }
        public string? Addressee_identification { get; set; }
        public string? Addressee_ie { get; set; }
        public string? Addressee_address { get; set; }
        public string? Addressee_number { get; set; }
        public string? Addressee_neighborhood { get; set; }
	    public string? Addressee_county { get; set; }
        public string? Addressee_cep { get; set; }
        public string? Addressee_uf { get; set; }
        public string? Additional_information { get; set; }
        public string? Value { get; set; }
        public int Volume { get; set; }
        public string? Weight { get; set; }

    }
}
