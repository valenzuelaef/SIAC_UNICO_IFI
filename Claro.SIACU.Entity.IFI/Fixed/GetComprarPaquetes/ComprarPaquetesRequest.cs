using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.GetComprarPaquetes 
{
//INI - RF-04 - RF-05 Evalenzs


    [DataContract(Name = "comprarPaquetesBodyRequest")]
    public class ComprarPaquetesRequest 
    {
        //[DataMember(Name = "MessageRequest")]
        //public ComprarPaquetesMessageRequest MessageRequest { get; set; }
        [DataMember(Name = "comprarPaquetesRequest")]
        public ComprarPaquetesBodyRequest comprarPaquetesRequest { get; set; }
    }
}
