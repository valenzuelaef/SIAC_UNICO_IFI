using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetQueuesCase
{
     [DataContract(Name = "QueuesCaseResponse")]
    public class QueuesCaseResponse
    {
        [DataMember]
         public string Cod_Error { get; set; }

        [DataMember]
        public string Desc_Error { get; set; }

        [DataMember]
        public QueuesCase Queues { get; set; }

        [DataMember]
        public List<QueuesCase> ListQueues { get; set; }
    }
}
