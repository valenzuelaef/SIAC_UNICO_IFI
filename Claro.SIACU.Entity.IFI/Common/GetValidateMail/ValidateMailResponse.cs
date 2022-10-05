using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetValidateMail
{
    [DataContract]
    public class ValidateMailResponse
    {
        [DataMember]
        public string strValidaCorreo { get; set; }




    }
}
