using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Wireless.GenerateOCC
{
    [DataContract]
    public class OCCResponse
    {
        [DataMember]
        public string result { get; set; }
    }
}
