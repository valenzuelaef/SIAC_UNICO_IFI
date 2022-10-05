using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetUploadDocumentOnBase
{
    [DataContract]
    public class ParametersRequest
    {
        [DataMember]
        public ListOptionalRequest ListaRequestOpcional { get; set; }

    }
}
