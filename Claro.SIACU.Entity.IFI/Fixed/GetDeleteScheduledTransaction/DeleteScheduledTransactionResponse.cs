using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetDeleteScheduledTransaction
{
    [DataContract]
    public class DeleteScheduledTransactionResponse 
    {

        [DataMember]
        public bool CorrectProcess { get; set; }

    }
}
