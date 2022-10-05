﻿using Claro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed
{
    public class ETAFlow
    {
        [DbColumn("as_codzona")]
        [DataMember]
        public string as_codzona { get; set; }
        [DbColumn("an_indica")]
        [DataMember]
        public int an_indica { get; set; }
    }
}
