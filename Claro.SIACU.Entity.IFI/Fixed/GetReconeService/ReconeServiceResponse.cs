using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetReconeService
{
    [DataContract]
    public class ReconeServiceResponse
    {
        [DataMember]
        public string IdTransaction { get; set; }
        [DataMember]
        public string Result { get; set; }
        [DataMember]
        public bool BoolResult { get; set; }
    }
}
