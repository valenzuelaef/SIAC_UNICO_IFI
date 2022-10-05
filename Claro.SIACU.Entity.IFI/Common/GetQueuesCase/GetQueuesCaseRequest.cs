using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetQueuesCase
{
        [DataContract(Name = "QueuesCaseRequest")]
    public class QueuesCaseRequest : Claro.Entity.Request
    {

        [DataMember]
        public string strIdSession { get; set; }

        [DataMember]
        public string strTransaccion { get; set; }     
        
        [DataMember]
        public string SubClase { get; set; }

        [DataMember]
        public string Des_Title { get; set; }

        [DataMember]
        public string Flag_Buscar { get; set; }

        [DataMember]
        public string Usuario { get; set; }

    }
}
