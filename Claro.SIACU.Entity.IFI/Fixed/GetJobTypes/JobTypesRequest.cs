using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetJobTypes
{
    [DataContract(Name = "JobTypesRequestHfc")]
    public class JobTypesRequest : Claro.Entity.Request
    {
        [DataMember]
        public int p_tipo { get; set; }

    }
}
