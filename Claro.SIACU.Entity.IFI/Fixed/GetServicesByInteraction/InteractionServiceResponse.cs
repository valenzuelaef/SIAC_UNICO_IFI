using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetServicesByInteraction
{
    [DataContract(Name = "InteractionServiceResponseHfc")]
    public class InteractionServiceResponse
    {
        [DataMember]
        public List<ServiceByInteraction> Services { get; set; }
    }
}
