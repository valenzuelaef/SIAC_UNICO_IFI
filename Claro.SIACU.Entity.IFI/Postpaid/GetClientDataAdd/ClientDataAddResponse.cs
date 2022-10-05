using Claro.SIACU.Entity.IFI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetClientDataAdd
{
    [DataContract(Name = "ClientDataAddResponse")]
    public class ClientDataAddResponse
    {
        [DataMember]
        public string StrResponse { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public Client Cliente { get; set; }
    }
}
