using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.GetTransactionScheduled
{
    [DataContract]
    public class TransactionScheduledResponse
    {
        [DataMember]
        public List<TransactionScheduled> ListTransactionScheduled { get; set; }
    }
}
