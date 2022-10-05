using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetNumberGWP
{
    [DataContract(Name = "NumberGWPRequest")]
    public class NumberGWPRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Msisdn { get; set; }
    }
}
