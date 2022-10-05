using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetInsertEvidence
{
    [DataContract]
    public class InsertEvidenceResponse
    {
        [DataMember]
        public string StrFlagInsertion { get; set; }
        [DataMember]
        public string StrMsgText { get; set; }
        [DataMember]
        public bool BoolResult { get; set; }
    }
}
