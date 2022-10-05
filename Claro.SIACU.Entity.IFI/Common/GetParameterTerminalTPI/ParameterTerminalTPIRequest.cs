using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetParameterTerminalTPI
{
    [DataContract(Name = "ParameterTerminalTPIRequest")]
    public class ParameterTerminalTPIRequest : Claro.Entity.Request
    {
        [DataMember]
        public int ParameterID { get; set; }

      
    }
}
