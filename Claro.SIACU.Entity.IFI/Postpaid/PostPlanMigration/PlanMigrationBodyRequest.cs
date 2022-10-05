using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.PostPlanMigration
{
    [DataContract(Name = "PlanMigrationBodyRequest")]
    public class PlanMigrationBodyRequest
    {
        [DataMember]
        public string ServvMsisd { get; set; }

        [DataMember]
        public DateTime ServdProgDate { get; set; }

        [DataMember]
        public string CodId { get; set; }

        [DataMember]
        public string SimCard { get; set; }

        [DataMember]
        public string tmCodeCurrent { get; set; }

        [DataMember]
        public string tmCodeNew { get; set; }

        [DataMember]
        public string CustomerId { get; set; }

        [DataMember]
        public string ServvTypeService { get; set; }

        [DataMember]
        public string ServiCoser { get; set; }

        [DataMember]
        public string TypeReg { get; set; }

        [DataMember]
        public string ServvUserSystem { get; set; }

        [DataMember]
        public string ServvUserApp { get; set; }

        [DataMember]
        public string ServvEmailUserApp { get; set; }

        [DataMember]
        public string ServcState { get; set; }

        [DataMember]
        public string DescCoSer { get; set; }

        [DataMember]
        public string ServcAccountNumber { get; set; }

        [DataMember]
        public string ContactObjId { get; set; }

        [DataMember]
        public string FlagReg { get; set; }

        [DataMember]
        public Interaction MainInteraction { get; set; }

        [DataMember]
        public TemplateInteraction PlusInteraction { get; set; }

        [DataMember]
        public string FlagPenalty { get; set; }

        [DataMember]
        public double Repayment { get; set; }

        [DataMember]
        public double CalculatedPenalty { get; set; }

        [DataMember]
        public double AmountPenaltyCharge { get; set; }

        [DataMember]
        public string FlagLoyalPenalty{ get; set; }

        [DataMember]
        public double AmountLoyalPenalty { get; set; }

        [DataMember]
        public int OCCPenalty { get; set; }

        [DataMember]
        public double OCCConceptPenalty { get; set; }

        [DataMember]
        public string IdeaiWS { get; set; }

        [DataMember]
        public string IpClient { get; set; }

        [DataMember]
        public string UserAccount { get; set; }

        [DataMember]
        public string ServerName { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public List<ServiceByPlan> ServiceList { get; set; }

        [DataMember]
        public List<ServiceByPlan> SnCodeList { get; set; }

        [DataMember]
        public string ConstancyParameter { get; set; }

        [DataMember]
        public string ProductCode { get; set; }
    }
}
