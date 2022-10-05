﻿using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetInsertInteractHFC
{
    [DataContract]
    public class InsertInteractHFCResponse
    {
        [DataMember]
        public bool rResult { get; set; }
        [DataMember]
        public string rInteraccionId { get; set; }
        [DataMember]
        public string rFlagInsercion { get; set; }
        [DataMember]
        public string rMsgText { get; set; }
    }
}
