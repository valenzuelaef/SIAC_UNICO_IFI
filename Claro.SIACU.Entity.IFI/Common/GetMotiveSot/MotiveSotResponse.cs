﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetMotiveSot
{
    [DataContract(Name = "MotiveSotResponseCommon")]
    public class MotiveSotResponse
    {
        [DataMember]
        public List<ListItem> getMotiveSot { get; set; }
    }
}
