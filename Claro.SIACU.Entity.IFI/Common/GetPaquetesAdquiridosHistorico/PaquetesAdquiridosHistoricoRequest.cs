using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetPaquetesAdquiridosHistorico
{
    
        public class PaquetesAdquiridosHistoricoRequest : Claro.Entity.Request
        {
            [DataMember]
            public string strCodeUser { get; set; }
        }
   
}
