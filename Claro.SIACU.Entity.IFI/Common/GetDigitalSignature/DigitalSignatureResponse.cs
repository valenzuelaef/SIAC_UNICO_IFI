using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.IFI.Common.GetDigitalSignature
{
    [DataContract]
    public class DigitalSignatureResponse
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
        [DataMember]
        public ResponseData ResponseData { get; set; }
    }
}
