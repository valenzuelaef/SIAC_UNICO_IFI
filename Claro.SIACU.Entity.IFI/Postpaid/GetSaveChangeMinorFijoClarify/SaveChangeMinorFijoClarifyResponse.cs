﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeMinorFijoClarify
{
     [DataContract(Name = "SaveChangeMinorFijoClarifyResponse")]
    public class SaveChangeMinorFijoClarifyResponse
    {
        [DataMember]
        public string StrResult { get; set; }
        [DataMember]
        public string intTeractionid { get; set; }
        [DataMember]
        public string strFlaginsercion { get; set; }
        [DataMember]
        public string strMessage { get; set; }
        [DataMember]
        public string strMessageUpdate { get; set; }

    }
}
