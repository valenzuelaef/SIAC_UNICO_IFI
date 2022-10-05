using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetDataPower
{
    [DataContract(Name = "Fault")]
    public class FaultResponse
    {
        [DataMember(Name = "FaultCode")]
        public string FaultCode { get; set; }

        [DataMember(Name = "FaultString")]
        public string FaultString { get; set; }

        [DataMember(Name = "FaultActor")]
        public string FaultActor { get; set; }

        [DataMember(Name = "Detail")]
        public FaultDetail Detail { get; set; }

    }

    [DataContract(Name = "FaultDetail")]
    public class FaultDetail
    {
        [DataMember(Name = "IntegrationError")]
        public string IntegrationError { get; set; }
    }
}
