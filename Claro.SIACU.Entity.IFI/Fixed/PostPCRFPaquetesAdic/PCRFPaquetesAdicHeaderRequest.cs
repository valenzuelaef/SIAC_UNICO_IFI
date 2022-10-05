using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
//INI - RF-04 - RF-05 Evalenzs
namespace Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic
{
    [DataContract(Name = "PCRFPaquetesAdicHeaderRequest")]
    public class PCRFPaquetesAdicHeaderRequest
    {
        [DataMember(Name = "HeaderRequest")]
        public Claro.SIACU.Entity.IFI.Postpaid.GetDataPower.HeaderRequest HeaderRequest { get; set; }
    }
}
