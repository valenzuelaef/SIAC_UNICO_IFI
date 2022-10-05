using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetValidateCustomerID
{
    [DataContract]
    public class ValidateCustomerIdRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Phone { get; set; }

    }
}
