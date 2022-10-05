using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetServicesByInteraction
{
    [DataContract(Name = "InteractionServiceRequestHfc")]
    public class InteractionServiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string idInteraccion { get; set; }

    }
}
