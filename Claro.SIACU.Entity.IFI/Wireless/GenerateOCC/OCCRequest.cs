using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Wireless.GenerateOCC
{
    [DataContract]
    public class OCCRequest : Claro.Entity.Request
    {
       
        [DataMember]
        public decimal dcCustomerId { get; set; }
        [DataMember]
        public float dcMonto { get; set; }
        


        

    }
}
