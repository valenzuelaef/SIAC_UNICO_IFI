﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.GetComprarPaquetes 
{
//INI - RF-04 - RF-05 Evalenzs
    [DataContract(Name = "ComprarPaquetesMessageRequest")]
    public class ComprarPaquetesMessageRequest : Claro.Entity.Request
    {
        //[DataMember(Name = "Header")]
        //public ComprarPaquetesHeaderRequest Header { get; set; }
        [DataMember(Name = "comprarPaquetesBodyRequest")]
        public ComprarPaquetesRequest ComprarPaquetesRequest { get; set; }
    }          
}
