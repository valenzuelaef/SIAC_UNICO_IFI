using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common
{
    [DataContract(Name = "QueuesCase")]
    public class QueuesCase
    {
        [Data.DbColumn("CORRELATIVO")]
        [DataMember]
        public string CORRELATIVO { get; set; }

        [Data.DbColumn("TITLE_COLA")]
        [DataMember]
        public string TITLE_COLA { get; set; }

        [Data.DbColumn("COD_SUBCLASE")]
        [DataMember]
        public string COD_SUBCLASE { get; set; }

        [Data.DbColumn("FLAG_ACTIVO")]
        [DataMember]
        public string FLAG_ACTIVO { get; set; }

        [Data.DbColumn("FEC_CREACION")]
        [DataMember]
        public string FEC_CREACION { get; set; }

        [Data.DbColumn("FEC_MODIFICACION")]
        [DataMember]
        public string FEC_MODIFICACION { get; set; }

        [Data.DbColumn("USER_CREACION")]
        [DataMember]
        public string USER_CREACION { get; set; }

        [Data.DbColumn("USER_MODIFICACION")]
        [DataMember]
        public string USER_MODIFICACION { get; set; }

        [Data.DbColumn("COLA_ID")]
        [DataMember]
        public string COLA_ID { get; set; }

    }
}
