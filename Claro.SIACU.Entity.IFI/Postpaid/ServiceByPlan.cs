using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid
{
    [DataContract(Name = "ServiceByPlan")]
    public class ServiceByPlan
    {
        [Data.DbColumn("COD_PLAN_SISACT")]
        [DataMember]
        public string SisactPlanCode { get; set; }

        [Data.DbColumn("DES_PLAN_SISACT")]
        [DataMember]
        public string SisactPlanDes { get; set; }

        [Data.DbColumn("TMCODE")]
        [DataMember]
        public string TmCode { get; set; }

        [Data.DbColumn("SOLUCION")]
        [DataMember]
        public string Solution { get; set; }

        [Data.DbColumn("COD_SERV_SISACT")]
        [DataMember]
        public string SisactServCode { get; set; }

        [Data.DbColumn("SNCODE")]
        [DataMember]
        public string SnCode { get; set; }

        [Data.DbColumn("SPCODE")]
        [DataMember]
        public string SpCode { get; set; }

        [Data.DbColumn("COD_TIPO_SERVICIO")]
        [DataMember]
        public string ServiceTypeCode { get; set; }

        [Data.DbColumn("TIPO_SERVICIO")]
        [DataMember]
        public string ServiceTypeDes { get; set; }

        [Data.DbColumn("DES_SERV_SISACT")]
        [DataMember]
        public string SisactServDes { get; set; }

        [Data.DbColumn("COD_GRUPO_SERV")]
        [DataMember]
        public string ServiceGroupCode { get; set; }

        [Data.DbColumn("GRUPO_SERV")]
        [DataMember]
        public string ServiceGroupDes { get; set; }

        [Data.DbColumn("CF")]
        [DataMember]
        public string CF { get; set; }

        [Data.DbColumn("IDEQUIPO")]
        [DataMember]
        public string IdEquipment { get; set; }

        [Data.DbColumn("EQUIPO")]
        [DataMember]
        public string EquipmentDes { get; set; }

        [Data.DbColumn("CANT_EQUIPO")]
        [DataMember]
        public string EquipmentQty { get; set; }

        [Data.DbColumn("CODTIPEQU")]
        [DataMember]
        public string EquipTypeCode { get; set; }

        [Data.DbColumn("DSCEQU")]
        [DataMember]
        public string SapDes { get; set; }

        [Data.DbColumn("TIPEQU")]
        [DataMember]
        public string EquipmentType { get; set; }

        [Data.DbColumn("COD_EXTERNO")]
        [DataMember]
        public string ExternalCode { get; set; }

        [Data.DbColumn("DES_COD_EXTERNO")]
        [DataMember]
        public string ExternalCodeDes { get; set; }

        [Data.DbColumn("SERVV_USUARIO_CREA")]
        [DataMember]
        public string CreationUserServ { get; set; }

        [DataMember]
        public string CfWithIgv { get; set; }

        [DataMember]
        public string PrincipalGroupCode { get; set; }

        [DataMember]
        public string IdLineQuantity { get; set; }

        [DataMember]
        public string Quantity { get; set; }

        [DataMember]
        public string EquipmentGroupCode { get; set; }

        [DataMember]
        public string Action { get; set; }

    }
}
