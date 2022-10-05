using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.IFI.Common.GeneratePDF
{
    [DataContract(Name = "GeneratePDFDataResponseHfc")]
    public class GeneratePDFDataResponse
    {
        [DataMember]
        public bool Generated { get; set; }
        [DataMember]
        public byte[] EngineMessage { get; set; }
        [DataMember]
        public string StatusMessage { get; set; }

        [DataMember]
        public string FilePath { get; set; }
    }
}
