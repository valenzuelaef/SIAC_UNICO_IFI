﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetMotiveSOTByTypeJob
{
    [DataContract]
    public class MotiveSOTByTypeJobResponse
    {
        [DataMember]
        public List<GenericItem> List { get; set; }
    }
}
