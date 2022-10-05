using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid
{
    //JFlores
    [DataContract(Name = "ProductPlanIFI")]
    public class ProductPlan
    {
        [Data.DbColumn("COD_PLAN_SISACT")]
        [DataMember]
        public string strSisactPlanCode { get; set; }
        [Data.DbColumn("TMCODE")]
        [DataMember]
        public string strTmCode { get; set; }
        [Data.DbColumn("DES_PLAN_SISACT")]
        [DataMember]
        public string strSisactPlanDes { get; set; }
        [Data.DbColumn("VERSION")]
        [DataMember]
        public string strVersion { get; set; }
        [Data.DbColumn("CAT_PROD")]
        [DataMember]
        public string strCategoryProduct { get; set; }
        [Data.DbColumn("DES_TMCODE")]
        [DataMember]
        public string strTmDes { get; set; }

        [DataMember]
        public string strStartDate { get; set; }

        [DataMember]
        public string strEndDate { get; set; }
        [Data.DbColumn("TIPO_PROD")]
        [DataMember]
        public string strProductType { get; set; }
        [Data.DbColumn("USER_CREA")]
        [DataMember]
        public string strCreationUser { get; set; }
        [Data.DbColumn("SOLUCION")]
        [DataMember]
        public string strSolution { get; set; }
        [Data.DbColumn("COD_PLANO")]
        [DataMember]
        public string strMapCode { get; set; }
        [Data.DbColumn("PLNC_ESTADO")]
        [DataMember]
        public string strStatus { get; set; }
        [Data.DbColumn("CAMPV_CODIGO")]
        [DataMember]
        public string strCampaignCode { get; set; }
        [Data.DbColumn("CAMPV_DESCRIPCION")]
        [DataMember]
        public string strCampaignDes { get; set; }
        [Data.DbColumn("CAMPD_FECHA_FIN")]
        [DataMember]
        public DateTime dtCampaignDateEnd { get; set; }
    }
}
