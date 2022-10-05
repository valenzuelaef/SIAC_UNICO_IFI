using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetPaquetesAdquiridosHistorico
{
    public class PaquetesAdqueridos
    {
        public string msisdn { get; set; }
        
        public string customerId { get; set; }
        
        public string tipoCliente { get; set; }
        
        public string tipoProducto { get; set; }
       
        public string estado { get; set; }
      
        public string estadoEntrega { get; set; }
       
        public string cicloFact { get; set; }

        
        public string planBase { get; set; }

       
        public string motivoDeg { get; set; }

       
        public string paquete { get; set; }
       
        public string cargoFijo { get; set; }

       
        public string tipoPago { get; set; }

       
        public string monto { get; set; }

        
        public string canal { get; set; }

        
        public string departamento { get; set; }

        
        public string provincia { get; set; }

       
        public string distrito { get; set; }
       
        public string fechaCPR { get; set; }
        
        public string fechaAct { get; set; }
    }
}
