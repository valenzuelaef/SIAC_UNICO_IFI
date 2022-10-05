using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetCustomer
{
    [DataContract]
    public class CustomerResponse
    {
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public string vFlagConsulta { get; set; }
        [DataMember]
        public string rMsgText { get; set; }
        [DataMember]
        public bool Resultado { get; set; }

        [DataMember]
        public string contactobjid { get; set; }
        [DataMember]
        public string vFlagInsert { get; set; }


    }
}
