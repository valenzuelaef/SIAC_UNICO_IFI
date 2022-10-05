using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetJobTypes
{
    [DataContract(Name = "JobTypesResponseHfc")]
    public class JobTypesResponse
    {
        [DataMember]
        public List<JobType> JobTypes { get; set; }
    }
}
