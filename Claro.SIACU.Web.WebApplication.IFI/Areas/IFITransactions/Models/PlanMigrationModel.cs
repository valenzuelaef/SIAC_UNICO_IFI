using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices;
using Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using Claro.SIACU.Web.WebApplication.IFI.PostIFIService;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class PlanMigrationModel
    {
        public string strIDSession { get; set; }
        public ScheduledTransactionModel oProgramTask { get; set; }
        public InteractionModel oInteraction { get; set; }
        public TemplateInteractionModel oInteractionTemplate { get; set; }
        public string strPenalty { get; set; }
        public string strTotalPenalty { get; set; }
        public string strRefund { get; set; }
        public string strFlagPenalty { get; set; }
        public string strFlagLoyaltyPenalty { get; set; }
        public string strTotalDiscountPenalty { get; set; }
        public string strOCCPenalty { get; set; }
        public string strOCCPenaltyConcept { get; set; }
        public string strIdeaiWS { get; set; }
        public string strIpCustomer { get; set; }
        public string strUserAccount { get; set; }
        public string strServerName { get; set; }
        public string strPhone { get; set; }
        public string strText { get; set; }
        public string strCurrentTMCode { get; set; }
        public string strSIMCard { get; set; }
        public string strCodProduct { get; set; }
        public string strCampaignCode { get; set; }////INC000003633281
        public List<Helpers.PostpaidServices.ServicesType> lstServices { get; set; }
        public List<Helpers.PostpaidServices.ServicesBSCSType> lstServicesBSCS { get; set; }
        public List<PostIFIService.ServiceByCurrentPlan> lstCurrentServices { get; set; }
        public List<PostIFIService.ServiceByPlan> lstNewServices { get; set; }
    }

    public class PlanMigrationLoadModel
    {
        public string strCurrentDate { get; set; }
        public string strUserxCac { get; set; }
        public List<ItemGeneric> lstPointOfAttention { get; set; }
        public List<ItemGeneric> lstBusinessRules { get; set; }
        public TypificationModel oTypification { get; set; }
        public double dblIGVPercent { get; set; }
        public double dblIGVPercertUnity { get; set; }
        public List<PostIFIService.ServiceByCurrentPlan> lstCurrentServices { get; set; }
        public CommonIFIService.Office oOffce { get; set; }
        public string strApplicationDate { get; set; }
        public string strCurrentGroupInternet { get; set; }
        public string strCurrentTMCode { get; set; }
        public string strSIMCard { get; set; }
        public string strEditApplicationDate { get; set; }
        public string strOpcEditApplicationDate { get; set;}
        public string strPDFServer { get; set; }
    }

    public class ChoosePlanModel
    {
        public bool blnPermission { get; set; }
        public List<ItemGeneric> lstVigencyType { get; set; }
        public List<PostIFIService.ProductPlanIFI> lstPlans { get; set; }
        public List<PostIFIService.ProductPlanIFI> lstCampaignNSolutions { get; set; }
        public IEnumerable<string> lstSolutions { get; set; }
        public IEnumerable<string> lstCampaigns { get; set; }
    }

    public class ChooseCoreServicesModel
    {
        public List<PostIFIService.ServiceByPlan> lstServicesByPlanCable { get; set; }
        public List<PostIFIService.ServiceByPlan> lstServicesByPlanInternet { get; set; }
        public List<PostIFIService.ServiceByPlan> lstServicesByPlanTelephone { get; set; }
        public string strServicesType { get; set; }
        public string strGroupCable { get; set; }
        public string strGroupInternet { get; set; }
        public string strGroupTelephony { get; set; }
        public string[] arrGroupCable { get; set; }
        public string[] arrGroupInternet { get; set; }
        public string[] arrServicesType { get; set; }
        public string[] arrGroupTelephony { get; set; }
    }
}