﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetOffice
{
    [DataContract(Name = "OfficeResponseCommon")]
    public class OfficeResponse
    {
        [DataMember]
        public Office objOffice { get; set; }
    }
}
