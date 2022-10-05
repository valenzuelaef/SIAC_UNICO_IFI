using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.IFI.Common.GetSendEmailWithBase64
{
    [DataContract]
    public class ParameterComplexType
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}
