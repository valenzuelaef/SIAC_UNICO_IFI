using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common
{
    public class Typification
    {
        [DataMember]
        [Data.DbColumn("TIPO")]
        public string TIPO { get; set; }

        [DataMember]
        [Data.DbColumn("CLASE")]
        public string CLASE { get; set; }

        [DataMember]
        [Data.DbColumn("SUBCLASE")]
        public string SUBCLASE { get; set; }

        [DataMember]
        [Data.DbColumn("INTERACCION_CODE")]
        public string INTERACCION_CODE { get; set; }

        [DataMember]
        [Data.DbColumn("TIPO_CODE")]
        public string TIPO_CODE { get; set; }

        [DataMember]
        [Data.DbColumn("CLASE_CODE")]
        public string CLASE_CODE { get; set; }

        [DataMember]
        [Data.DbColumn("SUBCLASE_CODE")]
        public string SUBCLASE_CODE { get; set; }
    }
}
