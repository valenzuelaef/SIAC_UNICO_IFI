using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common
{
    [DataContract(Name = "BillingCycle")]
    public class BillingCycle
    {
        [DataMember]
        [Data.DbColumn("strBicicle")]
        public string strBicicle { get; set; }
        [DataMember]
        [Data.DbColumn("strDescription")]
        public string strDescription { get; set; }
        [DataMember]
        [Data.DbColumn("strValidForm")]
        public string strValidForm { get; set; }
        [DataMember]
        [Data.DbColumn("strTypeCustomer")]
        public string strTypeCustomer { get; set; }
    }
}
