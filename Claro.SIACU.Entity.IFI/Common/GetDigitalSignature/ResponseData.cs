using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.IFI.Common.GetDigitalSignature
{
    [DataContract]
    public class ResponseData
    {
        [DataMember]
        public DataSignDocument DatosFirmarDocumento { get; set; }

    }
}
