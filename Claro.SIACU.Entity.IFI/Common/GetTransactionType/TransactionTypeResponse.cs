using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetTransactionType
{
    [DataContract(Name = "TransactionTypeResponseCommon")]
    public class TransactionTypeResponse
    {
        [DataMember]
        public List<ListItem> TransactionTypes { get; set; }
    }
}
