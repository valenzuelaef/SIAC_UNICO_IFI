﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.GetConsultarPaqDisponibles 
{
//INI - RF-02 Evalenzs
    public class ConsultarPaqDisponiblesHeaderResponse
    {
        [DataMember(Name = "HeaderResponse")]
        public Claro.SIACU.Entity.IFI.Common.GetDataPower.HeaderResponse HeaderResponse { get; set; }

    }
}
