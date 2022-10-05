using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "ServiceByCurrentPlan")]
    public class ServiceByCurrentPlan
    {
        [Data.DbColumn("DE_GRP")]
        [DataMember]
        public string GroupDes { get; set; }
        [Data.DbColumn("NO_GRP")]
        [DataMember]
        public string GroupNo { get; set; }
        [Data.DbColumn("DE_SER")]
        [DataMember]
        public string ServiceDes { get; set; }
        [Data.DbColumn("CARGOFIJO")]
        [DataMember]
        public string FixedCharge { get; set; }
        [Data.DbColumn("TIPO_SERVICIO")]
        [DataMember]
        public string ServiceType { get; set; }
        [Data.DbColumn("SNCODE")]
        [DataMember]
        public string SnCode { get; set; }
        [Data.DbColumn("STATUS")]
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string FixedChargeWithIgv { get; set; }
    }
}
