using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "ProductByCurrentPlan")]
    public class EquipmentByCurrentPlan
    {
        [Data.DbColumn("PORT_ID")]
        [DataMember]
        public string PortId { get; set; }

        [Data.DbColumn("IMSI")]
        [DataMember]
        public string IMSI { get; set; }

        [Data.DbColumn("SM_ID")]
        [DataMember]
        public string SmId { get; set; }

        [Data.DbColumn("SERIENUM")]
        [DataMember]
        public string SerieNumber { get; set; }

        [Data.DbColumn("DN_ID")]
        [DataMember]
        public string DnId { get; set; }

        [Data.DbColumn("MSISDN")]
        [DataMember]
        public string MSISDN { get; set; }

    }
}
