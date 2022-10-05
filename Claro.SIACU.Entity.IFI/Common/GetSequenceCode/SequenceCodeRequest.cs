using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetSequenceCode
{
    [DataContract]
    public class SequenceCodeRequest : Claro.Entity.Request
    {
        [DataMember]
        public bool isFlagUnlock { get; set; }
    }
}
