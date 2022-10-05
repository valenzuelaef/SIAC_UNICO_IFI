using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed
{
    public class Service
    {
        [DataMember]
        public string NRO_CELULAR { get; set; }

        [DataMember]
        public string ESTADO_LINEA { get; set; }
    }
}
