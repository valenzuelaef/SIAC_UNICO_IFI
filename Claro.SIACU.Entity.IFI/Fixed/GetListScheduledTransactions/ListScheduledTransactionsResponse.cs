using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetListScheduledTransactions
{
    [DataContract(Name = "ListScheduledTransactionsResponse")]
    public class ListScheduledTransactionsResponse
    {
        [DataMember]
        public List<ScheduledTransaction> LstTransactions { get; set; }

        [DataMember]
        public bool CorrectProcess { get; set; }
    }
}
