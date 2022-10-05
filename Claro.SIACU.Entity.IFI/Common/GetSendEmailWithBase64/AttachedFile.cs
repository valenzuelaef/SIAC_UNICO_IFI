using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.IFI.Common.GetSendEmailWithBase64
{
    [DataContract]
    public class AttachedFile
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string HeadBoard { get; set; }

        [DataMember]
        public byte[] file { get; set; }
    }
}
