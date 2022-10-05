using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "DetailReceiptPostPostPaid")]
    public class DetailReceipt
    {

        [DataMember]
        [Data.DbColumn("TotalAccess")]
        public decimal TOTALACCESS { get; set; }


        [DataMember]
        [Data.DbColumn("Larga_Distancia_Internacional")]
        public decimal LARGA_DISTANCIA_INTERNACIONAL { get; set; }


        [DataMember]
        [Data.DbColumn("Larga_Distancia_nacional")]
        public decimal LARGA_DISTANCIA_NACIONAL { get; set; }


        [DataMember]
        [Data.DbColumn("TotalOCCnIGV")]
        public decimal TOTALOCCNIGV { get; set; }


        [DataMember]
        [Data.DbColumn("Trafico_Local_Adicional")]
        public decimal TRAFICO_LOCAL_ADICIONAL { get; set; }


        [DataMember]
        [Data.DbColumn("Trafico_Local_A_Consumo")]
        public decimal TRAFICO_LOCAL_A_CONSUMO { get; set; }


        [DataMember]
        [Data.DbColumn("roaming_internacional")]
        public decimal ROAMING_INTERNACIONAL { get; set; }


        [DataMember]
        [Data.DbColumn("TOTALSUBSCRIPTION")]
        public decimal TOTALSUBSCRIPTION { get; set; }


        [DataMember]
        [Data.DbColumn("TotalOCCs")]
        public decimal TOTALOCCS { get; set; }


        [DataMember]
        [Data.DbColumn("total_cargos_del_mes")]
        public decimal TOTAL_CARGOS_DEL_MES { get; set; }
    }
}
