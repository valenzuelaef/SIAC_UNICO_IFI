﻿using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.PostExecuteSuspension
{
    [DataContract]
    public class ExecuteSuspensionResponse
    {
        [DataMember]
        public string idtrans { get; set; }
        [DataMember]
        public string result { get; set; }
        [DataMember]
        public bool ResultMethod { get; set; }
    }
}
