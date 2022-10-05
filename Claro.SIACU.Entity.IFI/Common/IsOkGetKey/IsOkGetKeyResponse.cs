using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.IsOkGetKey
{
    [DataContract(Name = "IsOkGetKeyResponseCommon")]
    public class IsOkGetKeyResponse
    {
        [DataMember]
        public bool result { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string Pass { get; set; }

    }
}
