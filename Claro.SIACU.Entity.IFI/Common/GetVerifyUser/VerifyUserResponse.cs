﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetVerifyUser
{
    [DataContract]
    public class VerifyUserResponse
    {
        [DataMember]
        public List<ConsultSecurity> LstConsultSecurities { get; set; }
    }
}
