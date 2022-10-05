using Claro.SIACU.Entity.IFI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeMinorClarify
{
    [DataContract(Name = "SaveChangeMinorClarifyRequest")]
    public class SaveChangeMinorClarifyRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strSession { get; set; }
        [DataMember]
        public Common.Client objCliente { get; set; }
        [DataMember]
        public string strTransaction { get; set; }
    }
}
