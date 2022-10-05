using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetPlans
{
    [DataContract(Name = "PlansRequest")]
    public class PlansRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strMap { get; set; }
        [DataMember]
        public string strOffer { get; set; }
        [DataMember]
        public string strProductType { get; set; }
        [DataMember]
        public string strOffice { get; set; }
        [DataMember]
        public string strOfficeDefault { get; set; }
        [DataMember]
        public string strFlagEjecution { get; set; }



    }
}
