using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Claro.SIACU.Entity.IFI.Common;
namespace Claro.SIACU.Entity.IFI.Common.GetBusinessInteraction2
{
    [DataContract(Name = "BusinessInteraction2RequestCommon")]
    public class BusinessInteraction2Request: Claro.Entity.Request
    {

        [DataMember]
        public Iteraction Item { get; set; }

        

    }
}
