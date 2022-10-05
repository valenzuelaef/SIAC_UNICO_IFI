using Claro.SIACU.Entity.IFI.Wireless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Wireless.GetTransactionScheduled
{
    [DataContract]
    public class TransactionScheduledResponse
    {
        [DataMember]
        public List<TransactionScheduled> ListTransactionScheduled { get; set; }
    }
}
