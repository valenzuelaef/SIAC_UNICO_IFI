using Claro.SIACU.Entity.IFI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetDataLine
{
    [DataContract(Name = "DataLineResponsePostPaid")]
    public class DataLineResponse
    {
        [DataMember]
        public string StrResponse { get; set; }
       
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public Line DataLine { get; set; }
    }
}
