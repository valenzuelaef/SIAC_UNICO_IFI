using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetDeleteScheduledTransaction
{
    [DataContract]
    public class DeleteScheduledTransactionRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strServiCoId { get; set; }
        [DataMember]
        public string codId { get; set; }
        [DataMember]
        public string strServiceState { get; set; }
        [DataMember]
        public string strApplicationCode { get; set; }
        [DataMember]
        public string strApplicationName { get; set; }
        [DataMember]
        public string strUserApp { get; set; }
        [DataMember]
        public string strTransaction { get; set; }
        [DataMember]
        public string strIdSession { get; set; }
    }
}
