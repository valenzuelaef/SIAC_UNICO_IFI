﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Wireless.GetApadece
{
    [DataContract]
    public class ApadeceResponse
    {
        [DataMember]
        public string monto { get; set; }
    }
}
