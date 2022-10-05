using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetImeis
{
    [DataContract(Name = "ImeisResponseIFI")]
    public class ImeisResponse
    {
        [DataMember]
        public List<Terminal> lstTerminal { get; set; }
    }
}
