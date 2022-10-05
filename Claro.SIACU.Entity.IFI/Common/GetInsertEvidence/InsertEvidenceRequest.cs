using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetInsertEvidence
{
    [DataContract]
    public class InsertEvidenceRequest : Claro.Entity.Request
    {
        [DataMember]
        public Evidence Evidence { get; set; }
    }
}
