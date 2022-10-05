using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic
{
//INI - RF-04 - RF-05 Evalenzs
    public class PCRFPaquetesAdicHeaderResponse
    {
        [DataMember(Name = "HeaderResponse")]
        public Claro.SIACU.Entity.IFI.Common.GetDataPower.HeaderResponse HeaderResponse { get; set; }

    }
}
