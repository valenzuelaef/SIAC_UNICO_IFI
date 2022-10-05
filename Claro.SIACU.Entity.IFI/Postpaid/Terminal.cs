using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid
{
    [DataContract]
    public class Terminal
    {
        [DataMember]
        public string _strCodigoBloqueo { get; set; }
        [DataMember]
        public string _strNumeroIMEI { get; set; }
        [DataMember]
        public string _strNumeroLinea { get; set; }
        [DataMember]
        public string _strNomCliente { get; set; }
        [DataMember]
        public string _strApeCliente { get; set; }
        [DataMember]
        public string _strNroDocumento { get; set; }
        [DataMember]
        public string _strTipDocumento { get; set; }
        [DataMember]
        public string _strCanal { get; set; }
        [DataMember]
        public string _strMarca { get; set; }
        [DataMember]
        public string _strModelo { get; set; }
        [DataMember]
        public string _strFechaRegistro { get; set; }
        [DataMember]
        public string _strFechaMovimiento { get; set; }
        [DataMember]
        public string _strEstado { get; set; }

        // Nombre del operador
        [DataMember]
        public string _strOperador { get; set; }
        [DataMember]
        public string _strReportante { get; set; }
        [DataMember]
        public string _strAsesorServicio { get; set; }

        // Registro de Terminal y sus movimientos, registro, cambio de titular, 
        [DataMember]
        public string _strTipoMovimiento { get; set; }
    }
}
