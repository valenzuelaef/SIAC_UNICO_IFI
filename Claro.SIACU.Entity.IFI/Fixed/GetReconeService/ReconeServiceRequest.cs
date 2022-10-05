using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetReconeService
{
    [DataContract]
    public class ReconeServiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public Reconection GetReconection { get; set; }
    }
}
