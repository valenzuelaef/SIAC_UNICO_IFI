﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.CurrentLock
{
    [DataContract(Name = "CurrentLockRequestIFI")]
    public class CurrentLockRequest : Claro.Entity.Request
    {
        [DataMember]
        public string codId { get; set; }
    }
}
