using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetStatedebt
{
    [DataContract(Name = "GetStatedebtIFIResponse")]
    public class GetStatedebtResponse
    {
        [DataMember]
        public bool blResult { get; set; }
    }
}
