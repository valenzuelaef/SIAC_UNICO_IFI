﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetManzanasType
{
    [DataContract(Name = "ManzanasTypeResponseCommon")]
    public class ManzanasTypeResponse
    {
    [DataMember]
    public List<ListItem> ManzanasTypes { get; set; }
    }
}
