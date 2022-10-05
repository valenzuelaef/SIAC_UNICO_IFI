using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "ReceiptPostPaid")]
    public class Receipt
    {
        public Receipt()
        {
            RECIBO_DETALLADO = new DetailReceipt();
        }

        [DataMember]
        [Data.DbColumn("InvoiceNumber")]
        public string NUMERO_RECIBO { get; set; }

        [DataMember]
        [Data.DbColumn("FechaEmision")]
        public string FECHA_EMISION { get; set; }

        [DataMember]
        [Data.DbColumn("FechaVencimiento")]
        public string FECHA_VENCIMIENTO { get; set; }

        [DataMember]
        [Data.DbColumn("periodo")]
        public string PERIODO { get; set; }

        [DataMember]
        public string INVOICENUMBER { get; set; }

        [DataMember]
        public bool ENVIO_CORREO { get; set; }

        [DataMember]
        public DetailReceipt RECIBO_DETALLADO { get; set; }
        [DataMember]
        [Data.DbColumn("InvoiceNumber")]
        public string INVOICE_NUM { get; set; }

         [DataMember]
         [Data.DbColumn("CCName")]
        public string CCName { get; set; }


        [DataMember]
        [Data.DbColumn("ContactClient")]
         public string ContactClient { get; set; }

        [DataMember]
        [Data.DbColumn("CCAddr1")]
        public string CCAddr1 { get; set; }

        [DataMember]
        [Data.DbColumn("CCAddr2")]
        public string CCAddr2 { get; set; }

        [DataMember]
        [Data.DbColumn("Distrito")]
        public string Distrito { get; set; }

        [DataMember]
        [Data.DbColumn("Provincia")]
        public string Provincia { get; set; }

        [DataMember]
        [Data.DbColumn("Departamento")]
        public string Departamento { get; set; }

        [DataMember]
        [Data.DbColumn("NroDoc")]
        public string NroDoc { get; set; }

        [DataMember]
        [Data.DbColumn("FechaInicio")]
        public string FechaInicio { get; set; }

        [DataMember]
        [Data.DbColumn("FechaFin")]
        public string FechaFin { get; set; }

        [DataMember]
        [Data.DbColumn("CodCliente")]
        public string CodCliente { get; set; }

        [DataMember]
        [Data.DbColumn("NroCiclo")]
        public string NroCiclo { get; set; }

        [DataMember]
        [Data.DbColumn("TotalPrevCharges")]
        public string TotalPrevCharges { get; set; }

        [DataMember]
        [Data.DbColumn("TotalPaymentsRcvd")]
        public string TotalPaymentsRcvd { get; set; }

        [DataMember]
        [Data.DbColumn("TotalCurrentCharges")]
        public string TotalCurrentCharges { get; set; }

        [DataMember]
        [Data.DbColumn("TotalTaxes")]
        public string TotalTaxes { get; set; }

        [DataMember]
        [Data.DbColumn("TotalAmountDue")]
        public string TotalAmountDue { get; set; }

        [DataMember]
        [Data.DbColumn("Mes")]
        public string Mes { get; set; }

        [DataMember]
        [Data.DbColumn("Anho")]
        public string Anho { get; set; }

        [DataMember]
        [Data.DbColumn("version")]
        public string version { get; set; }

        [DataMember]
        [Data.DbColumn("TotalPrevBalance")]
        public string TotalPrevBalance { get; set; }
      
        

        [DataMember]
        [Data.DbColumn("TotalCurrentCharges")]
        public string MTO_ULT_FACTURA { get; set; }
    }
}
