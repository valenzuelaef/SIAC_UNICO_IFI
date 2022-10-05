﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetClient
{
    [DataContract(Name="GetClientResponseComon")]
    public class ClientResponse
    {
        [DataMember]
        public Client listClient { get; set; }


        [DataMember]
        public string Flagquery { get; set; }


        [DataMember]
        public string MsgText { get; set; }
    }
}
