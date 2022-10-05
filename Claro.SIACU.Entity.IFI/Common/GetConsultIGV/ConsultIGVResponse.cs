using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetConsultIGV
{
    [DataContract]
    public class ConsultIGVResponse
    {
        [DataMember]
        public List<IGVTax> ListConsultIGV { get; set; }
    }
}
