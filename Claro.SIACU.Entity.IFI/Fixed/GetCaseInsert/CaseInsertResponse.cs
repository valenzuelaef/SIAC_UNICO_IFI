using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetCaseInsert
{
    [DataContract]
    public class CaseInsertResponse
    {
        [DataMember]
        public string rCasoId { get; set; }

        [DataMember]
        public string rFlagInsercion { get; set; }

        [DataMember]
        public string rMsgText { get; set; }

    }
}
