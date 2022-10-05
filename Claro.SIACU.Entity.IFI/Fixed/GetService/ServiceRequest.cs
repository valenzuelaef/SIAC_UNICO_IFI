using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetService
{
    public class ServiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string ContractID { get; set; }

        [DataMember]
        public string ProductType { get; set; }
    }
}
