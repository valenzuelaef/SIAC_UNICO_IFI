using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using Claro.SIACU.Web.WebApplication.IFI.PostIFIService;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class PlanMigrationController : CommonServicesController
    {
        private readonly Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient _oServicePostpaid = new Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient();

        ///<summary>Metodo que retorna la vista del controlador de plan de migración</summary>
        ///<returns>ActionResult<returns>
        ///<remarks>PlanMigration</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public ActionResult PlanMigration()
        {
            return PartialView();
        }

        public ActionResult ChoosePlan()
        {
            return PartialView();
        }

        public ActionResult ChooseServicesByPlan()
        {
            return PartialView();
        }

        public JsonResult PlanMigrationLoad(string strIDSession, string strUser, string strContract, string strBillingCycle, string strPermitions)
            {
            var oPlanMigrationLoad = new PlanMigrationLoadModel();
            string strMsgPivot = string.Empty;
            string strTransaction = ConfigurationManager.AppSettings("MigracionPlanIFITransaction");
            string strOpcChangeApplicationDate = ConfigurationManager.AppSettings("strOpcIFICPModFechaAplic");
            string strEditApplicationDate = string.Empty;

            int intPositionAccess = strPermitions.ToUpper().IndexOf(strOpcChangeApplicationDate.ToUpper(), StringComparison.OrdinalIgnoreCase);

            if (intPositionAccess < Claro.Constants.NumberZero)
            {
                strEditApplicationDate = Claro.Constants.NumberZeroString;
            }
            else
            {
                strEditApplicationDate = Claro.Constants.NumberOneString;
            }

            try
            {
                oPlanMigrationLoad.strCurrentDate = DateTime.Today.ToShortDateString();
                oPlanMigrationLoad.oOffce = GetOffice(strIDSession, strUser);
                oPlanMigrationLoad.strUserxCac = GetPointOfAttentionByUser(strIDSession, strUser);
                oPlanMigrationLoad.lstPointOfAttention = GetListCacDac(strIDSession);
                var oTypification = LoadTypifications(strIDSession, strTransaction, ref strMsgPivot);
                if (oTypification != null)
                {
                    oPlanMigrationLoad.oTypification = new TypificationModel();
                    oPlanMigrationLoad.oTypification.Type = oTypification.TIPO;
                    oPlanMigrationLoad.oTypification.TypeCode = oTypification.TIPO_CODE;
                    oPlanMigrationLoad.oTypification.Class = oTypification.CLASE;
                    oPlanMigrationLoad.oTypification.ClassCode = oTypification.CLASE_CODE;
                    oPlanMigrationLoad.oTypification.SubClass = oTypification.SUBCLASE;
                    oPlanMigrationLoad.oTypification.SubClassCode = oTypification.SUBCLASE_CODE;
                    oPlanMigrationLoad.oTypification.InteractionCode = oTypification.INTERACCION_CODE;
                }
                oPlanMigrationLoad.lstBusinessRules = GetBusinessRulesLst(strIDSession, oPlanMigrationLoad.oTypification.SubClassCode);
                var oConsultIGV = GetCommonConsultIgv(strIDSession);
                if (oConsultIGV != null)
                    oPlanMigrationLoad.dblIGVPercent = oConsultIGV.igvD;
                else
                    oPlanMigrationLoad.dblIGVPercent = Claro.Convert.ToDouble(ConfigurationManager.AppSettings("valorIGV"));
                oPlanMigrationLoad.dblIGVPercertUnity = oPlanMigrationLoad.dblIGVPercent + Claro.Constants.NumberOne;
                var oResponseCurrentService = GetServicesByCurrentPlan(strIDSession, strContract);
                oPlanMigrationLoad.lstCurrentServices = oResponseCurrentService.lstServicesByCurrentPlan;
                oPlanMigrationLoad.strApplicationDate = GetApplicationDate(strBillingCycle);
                oPlanMigrationLoad.strCurrentGroupInternet = ConfigurationManager.AppSettings("strPlanActualInternet");
                oPlanMigrationLoad.strCurrentTMCode = oResponseCurrentService.strTmCode;
                var oResponseEquipment = GetEquipmentByCurrentPlan(strIDSession, strContract);
                oPlanMigrationLoad.strSIMCard = oResponseEquipment.lstEquipmentByCurrentPlan.First().SerieNumber;
                oPlanMigrationLoad.strEditApplicationDate = strEditApplicationDate;
                oPlanMigrationLoad.strOpcEditApplicationDate = strOpcChangeApplicationDate;
                oPlanMigrationLoad.strPDFServer = ConfigurationManager.AppSettings("strServidorLeerPDF");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIDSession, "PlanMigrationLoad", Claro.Utils.GetExceptionMessage(ex));
            }

            Claro.Web.Logging.Info(strIDSession, "PlanMigrationLoad", "Output: " + JsonConvert.SerializeObject(oPlanMigrationLoad));

            return Json(new { data = oPlanMigrationLoad }, JsonRequestBehavior.AllowGet);
        }

        private PostIFIService.ServicesByCurrentPlanResponse GetServicesByCurrentPlan(string strIdSession, string strIdContract)
        {
            PostIFIService.ServicesByCurrentPlanRequest objServicesByCurrentPlanRequest = new PostIFIService.ServicesByCurrentPlanRequest
            {
                ContractId = strIdContract,
                audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession)
            };

            PostIFIService.ServicesByCurrentPlanResponse objServicesByCurrentPlanResponse = null;

            Claro.Web.Logging.Info(strIdSession, objServicesByCurrentPlanRequest.audit.transaction, "Input: " + JsonConvert.SerializeObject(objServicesByCurrentPlanRequest));

            try
            {
                objServicesByCurrentPlanResponse = Claro.Web.Logging.ExecuteMethod<PostIFIService.ServicesByCurrentPlanResponse>(() =>
                {
                    return new PostIFIServiceClient().GetServicesByCurrentPlan(objServicesByCurrentPlanRequest);
                });

                Claro.Web.Logging.Info(strIdSession, objServicesByCurrentPlanRequest.audit.transaction, "Contracto: " + strIdContract);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objServicesByCurrentPlanRequest.audit.transaction, Claro.Utils.GetExceptionMessage(ex));
            }

            Claro.Web.Logging.Info(strIdSession, objServicesByCurrentPlanRequest.audit.transaction, "Output: " + JsonConvert.SerializeObject(objServicesByCurrentPlanResponse));

            return objServicesByCurrentPlanResponse;
        }

        private PostIFIService.EquipmentByCurrentPlanResponse GetEquipmentByCurrentPlan(string strIdSession, string strIdContract)
        {
            PostIFIService.EquipmentByCurrentPlanRequest objEquipmentByCurrentPlanRequest = new PostIFIService.EquipmentByCurrentPlanRequest
            {
                ContractId = strIdContract,
                audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession)
           };

            PostIFIService.EquipmentByCurrentPlanResponse objEquipmentByCurrentPlanResponse = null;

            Claro.Web.Logging.Info(strIdSession, objEquipmentByCurrentPlanRequest.audit.transaction, "Input: " + JsonConvert.SerializeObject(objEquipmentByCurrentPlanRequest));

            try
            {
                objEquipmentByCurrentPlanResponse = Claro.Web.Logging.ExecuteMethod<PostIFIService.EquipmentByCurrentPlanResponse>(() => {
                    return new PostIFIServiceClient().GetEquipmentByCurrentPlan(objEquipmentByCurrentPlanRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objEquipmentByCurrentPlanRequest.audit.transaction, Claro.Utils.GetExceptionMessage(ex));
            }

            Claro.Web.Logging.Info(strIdSession, objEquipmentByCurrentPlanRequest.audit.transaction, "Output: " + JsonConvert.SerializeObject(objEquipmentByCurrentPlanResponse));

            return objEquipmentByCurrentPlanResponse;
        }

        private string GetApplicationDate(string strBillingCycle)
        {
            string strAppliationDate = string.Empty;
            string strDateString = string.Empty;
            DateTime dtDate;
            int intBillingDay = Claro.Utils.CheckInt(strBillingCycle);
            int intCurrentDay = DateTime.Today.Day;
            int intCurrentMonth = DateTime.Today.Month;
            int intCurrentYear = DateTime.Today.Year;

            strDateString = Claro.Utils.CheckStr(intBillingDay) + "/" + Claro.Utils.CheckStr(intCurrentMonth) + "/" + Claro.Utils.CheckStr(intCurrentYear);
            dtDate = Convert.ToDate(strDateString);

            if (intBillingDay <= intCurrentDay)
                {
                dtDate = dtDate.AddMonths(Claro.Constants.NumberOne);
            }

            dtDate = dtDate.AddDays(-1);
            strAppliationDate = Convert.ToDate(dtDate).ToShortDateString();
            return strAppliationDate;
        }

        [HttpPost]
        public JsonResult ChoosePlanLoad(string strIdSession, string strCodeUser, string strIdContract, string strTypeProduct, string strPermitions, string strOffice)
        {
            PostIFIService.AuditRequest straudit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession);
            var oChoosePlanModel = new ChoosePlanModel();
            oChoosePlanModel.blnPermission = true;

            var strIdOfficeDefault = ConfigurationManager.AppSettings("strPuntoVentaDefault");

            if (strOffice == null || strOffice == string.Empty)
                strOffice = strIdOfficeDefault;

            PostIFIService.PlansResponse objPlansResponse;
            PostIFIService.PlansRequest objPlansRequest = new PostIFIService.PlansRequest
            {
                strOffice = strOffice,
                strOfficeDefault = strIdOfficeDefault,
                strOffer = Claro.Constants.NumberZeroOneString,
                strProductType = strTypeProduct,
                strFlagEjecution = ConfigurationManager.AppSettings("strFlagFiltroPDV"),
                audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession)
            };

            Claro.Web.Logging.Info(strIdSession, objPlansRequest.audit.transaction, "Input: " + JsonConvert.SerializeObject(objPlansRequest));

            try
            {
                objPlansResponse = Claro.Web.Logging.ExecuteMethod<PostIFIService.PlansResponse>(() => {
                    return new PostIFIServiceClient().GetPlans(objPlansRequest); 
                });

                oChoosePlanModel.lstVigencyType = Claro.Utils.GetListValuesXMLIFI("BuscaPlanOpcionesXML", Claro.Constants.NumberZeroString , ConfigurationManager.AppSettings("strDataListArchive"));

                var strPermitionNot = ConfigurationManager.AppSettings("strOpcIFICPSCNoVigentes");
                var intPositionAccess = strPermitions.ToUpper().IndexOf(strPermitionNot.ToUpper(), StringComparison.OrdinalIgnoreCase);

                if (intPositionAccess < 0)
                {
                    var itemToRemove = oChoosePlanModel.lstVigencyType.Single(w => w.Description.Equals("NO VIGENTES"));
                    oChoosePlanModel.lstVigencyType.Remove(itemToRemove);
                    itemToRemove = oChoosePlanModel.lstVigencyType.Single(w => w.Description.Equals("TODOS"));
                    oChoosePlanModel.lstVigencyType.Remove(itemToRemove);
                    oChoosePlanModel.lstPlans = (from x in objPlansResponse.lstPlan
                                                          where x.strStatus == "1"
                                                          select x).ToList();
                    oChoosePlanModel.blnPermission = false;
                }
                else
                {
                    oChoosePlanModel.lstPlans = objPlansResponse.lstPlan;
                    oChoosePlanModel.blnPermission = true;

                }
                oChoosePlanModel.lstCampaignNSolutions = objPlansResponse.lstPlan;
                oChoosePlanModel.lstCampaigns = (from sol in oChoosePlanModel.lstPlans
                                                          select sol.strCampaignDes).Distinct();

                oChoosePlanModel.lstSolutions = (from sol in oChoosePlanModel.lstPlans
                                                          select sol.strSolution).Distinct();

            }
            catch (Exception ex)
            {
                objPlansResponse = null;
                Claro.Web.Logging.Error(strIdSession, straudit.transaction, Claro.Utils.GetExceptionMessage(ex));
            }

            Claro.Web.Logging.Info(strIdSession, objPlansRequest.audit.transaction, "Output: " + JsonConvert.SerializeObject(oChoosePlanModel));

            return Json(new { data = oChoosePlanModel }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChooseCoreServicesLoad(string strIdSession, string strIdContract, string strProductType, string strIdPlan)
        {
            var oChooseCoreServices = new ChooseCoreServicesModel();
            var lstServicesByPlan = new List<PostIFIService.ServiceByPlan>();

            oChooseCoreServices.strGroupCable = ConfigurationManager.AppSettings("strIFIGroupCable");
            oChooseCoreServices.strGroupInternet = ConfigurationManager.AppSettings("strIFIGroupInternet");
            oChooseCoreServices.strGroupTelephony = ConfigurationManager.AppSettings("strIFIGroupTelephony");
            oChooseCoreServices.strServicesType = ConfigurationManager.AppSettings("TipoServicioIFI");
            oChooseCoreServices.arrGroupCable = oChooseCoreServices.strGroupCable.Split(',');
            oChooseCoreServices.arrGroupInternet = oChooseCoreServices.strGroupInternet.Split(',');
            oChooseCoreServices.arrGroupTelephony = oChooseCoreServices.strGroupTelephony.Split(',');
            oChooseCoreServices.arrServicesType = oChooseCoreServices.strServicesType.Split(',');

            PostIFIService.PlanServiceResponse objServicesResponse;
            PostIFIService.PlanServiceRequest objServicesRequest = new PostIFIService.PlanServiceRequest
            {
                strIdPlan = strIdPlan,
                strProductType = strProductType,
                audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession)
            };

            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, "Input: " + JsonConvert.SerializeObject(objServicesRequest));

            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<PostIFIService.PlanServiceResponse>(() => {
                    return new PostIFIService.PostIFIServiceClient().GetServicesByPlan(objServicesRequest); 
                });

                if (objServicesResponse.lstService != null)
                {
                    lstServicesByPlan = objServicesResponse.lstService;
                    lstServicesByPlan = (from ele in lstServicesByPlan
                                         group ele by ele.SisactServCode
                                             into groups
                                             select groups.OrderBy(x => x.ServiceTypeCode).First()).ToList();

                    oChooseCoreServices.lstServicesByPlanCable = (from x in lstServicesByPlan
                                                                                 where oChooseCoreServices.arrGroupCable.Contains(x.ServiceGroupCode) &&
                                                                                       oChooseCoreServices.arrServicesType.Contains(x.ServiceTypeCode)
                                                                                 select x).ToList();

                    oChooseCoreServices.lstServicesByPlanInternet = (from x in lstServicesByPlan
                                                                     where oChooseCoreServices.arrGroupInternet.Contains(x.ServiceGroupCode) &&
                                                                                          oChooseCoreServices.arrServicesType.Contains(x.ServiceTypeCode)
                                                                                    select x).ToList();
                    oChooseCoreServices.lstServicesByPlanTelephone = (from x in lstServicesByPlan
                                                                      where oChooseCoreServices.arrGroupTelephony.Contains(x.ServiceGroupCode) &&
                                                                                           oChooseCoreServices.arrServicesType.Contains(x.ServiceTypeCode)
                                                                                     select x).ToList();


                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, Claro.Utils.GetExceptionMessage(ex));
            }

            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, "Output: " + JsonConvert.SerializeObject(oChooseCoreServices));

            return Json(new { data = oChooseCoreServices });
        }

        [HttpPost]
        public JsonResult GetAdditionalCoreServicesNEquipment(string strIdSession, string strIdContract, string strProductType, string strIdPlan, string strIgv)
        {
            var lstServicePlan = new List<ServiceByPlan>();

            var lstAdditionalCoreServicesInternet = new List<ServiceByPlan>();

            PlanServiceResponse objServicesResponse;
            PlanServiceRequest objServicesRequest = new PlanServiceRequest
            {
                strIdPlan = strIdPlan,
                strProductType = strProductType,
                audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession)
            };

            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, "Input: " + JsonConvert.SerializeObject(objServicesRequest));

            try
            {
                var arrGroupInternet = ConfigurationManager.AppSettings("strIFIGroupInternet").Split(',');
                var arrServicesTypeAdditionalCore = ConfigurationManager.AppSettings("TipoServicioAdicionalCoreIFI").Split(',');

                try
                {
                    objServicesResponse = Claro.Web.Logging.ExecuteMethod<PlanServiceResponse>(() =>
                    {
                        return new PostIFIService.PostIFIServiceClient().GetServicesByPlan(objServicesRequest);
                    });

                    if (objServicesResponse.lstService != null)
                    {
                        var strSolution = objServicesResponse.lstService[Claro.Constants.NumberZero].Solution;

                        lstServicePlan = (from x in objServicesResponse.lstService
                                          where x.Solution == strSolution
                                          select x).ToList(); ;

                        foreach (var objItem in lstServicePlan)
                        {
                            objItem.CfWithIgv = string.Format("{0:0.00}", Math.Round(Double.Parse(objItem.CF) * Double.Parse(strIgv), Claro.Constants.NumberTwo));
                        }


                        var lstService = (from ele in lstServicePlan
                                          group ele by ele.SisactServCode
                                              into groups
                                              select groups.OrderBy(x => x.ServiceTypeCode).First()).ToList();


                        lstAdditionalCoreServicesInternet = (from x in lstService
                                                                where arrGroupInternet.Contains(x.ServiceGroupCode) &&
                                                                        arrServicesTypeAdditionalCore.Contains(x.ServiceTypeCode)
                                                                select x).ToList();
                    }
                }
                catch (Exception ex)
                {
                    objServicesResponse = null;
                    Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, Claro.Utils.GetExceptionMessage(ex));
                }
                Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, "Consulta OK");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, Claro.Utils.GetExceptionMessage(ex));
            }

            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, "Output: " + JsonConvert.SerializeObject(lstAdditionalCoreServicesInternet));

            return Json(new { data = lstAdditionalCoreServicesInternet });
        }

        [HttpPost]
        public JsonResult PlanMigrationSave(PlanMigrationModel oPlanMigrationPost)
        {
            List<PostIFIService.ServiceByPlan> lstServices = new List<PostIFIService.ServiceByPlan>();
            PostIFIService.ServiceByPlan Service = null;
            PostIFIService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oPlanMigrationPost.strIDSession);
            string strCurrentDate;
            string strConstancy;

            if (ConfigurationManager.AppSettings("strFlagContingencyTipiIFI") == Claro.Constants.NumberOneString)
            {   
                strCurrentDate = DateTime.Now.ToString("dd/MM/yyyy"); 
            }
            else
            {   
                strCurrentDate = DateTime.Now.ToString("ddMMyyyy"); 
            }  

            foreach (PostIFIService.ServiceByPlan item in oPlanMigrationPost.lstNewServices)
            {
                Service = new PostIFIService.ServiceByPlan();
                Service.SisactServDes = item.SisactServDes;
                Service.ServiceTypeDes = item.ServiceTypeDes;
                Service.ServiceGroupCode = item.ServiceGroupCode;
                Service.CF = item.CF;
                Service.EquipmentDes = item.EquipmentDes;
                Service.Quantity = item.EquipmentQty;
                Service.SisactServCode = item.SisactServCode;
                Service.SnCode = item.SnCode;
                Service.SpCode = item.SpCode;
                lstServices.Add(Service);
            }

            List<PostIFIService.ServiceByPlan> lstSNCode = new List<PostIFIService.ServiceByPlan>();
            PostIFIService.ServiceByPlan SNService = null;

            if (oPlanMigrationPost.lstNewServices != null)
            {
                foreach (PostIFIService.ServiceByPlan item in oPlanMigrationPost.lstNewServices)
                {
                    SNService = new PostIFIService.ServiceByPlan();
                    SNService.SnCode = item.SnCode;
                    SNService.CF = item.CF;
                    SNService.Action = Claro.Constants.LetterA;
                    SNService.SpCode = item.SpCode;
                    lstSNCode.Add(SNService);
                }
            }

            if (oPlanMigrationPost.lstCurrentServices != null)
            {
                foreach (PostIFIService.ServiceByCurrentPlan item in oPlanMigrationPost.lstCurrentServices)
                {
                    SNService = new PostIFIService.ServiceByPlan();
                    SNService.SnCode = item.SnCode;
                    SNService.CF = item.FixedCharge;
                    SNService.Action = Claro.Constants.LetterD;
                    SNService.SpCode = Claro.Constants.NumberZeroString;
                    lstSNCode.Add(SNService);
                }
            }

            PostIFIService.HeaderRequest1 oHeaderRequest = new PostIFIService.HeaderRequest1
            {
                consumer = ConfigurationManager.AppSettings("consumer"),
                country = ConfigurationManager.AppSettings("country"),
                dispositivo = ConfigurationManager.AppSettings("strDPDispositivo"),
                language = ConfigurationManager.AppSettings("language"),
                modulo = ConfigurationManager.AppSettings("modulo"),
                msgType = ConfigurationManager.AppSettings("msgType"),
                operation = ConfigurationManager.AppSettings("strOperatioCPIFI"),
                pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                system = ConfigurationManager.AppSettings("system"),
                timestamp = DateTime.Now.ToString("o"),
                userId = App_Code.Common.CurrentUser,
                wsIp = App_Code.Common.GetApplicationIp()
            };

            PostIFIService.Interaction oInteraction = new PostIFIService.Interaction { 
                ACCOUNT = oPlanMigrationPost.oInteraction.Cuenta,
                PHONE = oPlanMigrationPost.oInteraction.Telephone,
                TYPE = oPlanMigrationPost.oInteraction.Type,
                CLASS = oPlanMigrationPost.oInteraction.Class,
                SUBCLASS = oPlanMigrationPost.oInteraction.SubClass,
                METHOD = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault"),
                TYPE_INTER = ConfigurationManager.AppSettings("AtencionDefault"),
                AGENT = oPlanMigrationPost.oInteraction.Agenth,
                USER_PROCESS = ConfigurationManager.AppSettings("USRProcesoSU"),
                MADEINONE = Claro.Constants.NumberZeroString,
                NOTES = oPlanMigrationPost.oInteraction.Note,
                FLAG_CASE = Claro.Constants.NumberZeroString,
                RESULT = ConfigurationManager.AppSettings("Ninguno"),
                SERVICE = string.Empty,
                INCONVENIENT = string.Empty,
                SERVICECODE = string.Empty,
                INCONVENIENTCODE = string.Empty,
                MAP = oPlanMigrationPost.oInteraction.Plan,
                VALUE1 = oPlanMigrationPost.strCampaignCode,////INC000003633281
                VALUE2 = string.Empty,
                OBJID_CONTACT = oPlanMigrationPost.oInteraction.ObjidContacto
            };

            PostIFIService.TemplateInteraction oTemplate = new PostIFIService.TemplateInteraction{
                _X_INTER_1 = oPlanMigrationPost.oInteractionTemplate.X_INTER_1,
                _X_INTER_2 = oPlanMigrationPost.oInteractionTemplate.X_INTER_2,
                _X_INTER_3 = oPlanMigrationPost.oInteractionTemplate.X_INTER_3,
                _X_INTER_4 = oPlanMigrationPost.oInteractionTemplate.X_INTER_4,
                _X_INTER_5 = oPlanMigrationPost.oInteractionTemplate.X_INTER_5,
                _X_INTER_6 = oPlanMigrationPost.oInteractionTemplate.X_INTER_6,
                _X_INTER_7 = oPlanMigrationPost.oInteractionTemplate.X_INTER_7,
                _X_INTER_8 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_8),
                _X_INTER_9 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_9),
                _X_INTER_10 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_10),
                _X_INTER_11 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_11),
                _X_INTER_12 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_12),
                _X_INTER_13 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_13),
                _X_INTER_14 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_14),
                _X_INTER_15 = oPlanMigrationPost.oInteractionTemplate.X_INTER_15,
                _X_INTER_16 = oPlanMigrationPost.oInteractionTemplate.X_INTER_16,
                _X_INTER_17 = oPlanMigrationPost.oInteractionTemplate.X_INTER_17,
                _X_INTER_18 = oPlanMigrationPost.oInteractionTemplate.X_INTER_18,
                _X_INTER_19 = oPlanMigrationPost.oInteractionTemplate.X_INTER_19,
                _X_INTER_20 = oPlanMigrationPost.oInteractionTemplate.X_INTER_20,
                _X_INTER_21 = oPlanMigrationPost.oInteractionTemplate.X_INTER_21,
                _X_INTER_22 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_22),
                _X_INTER_23 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_23),
                _X_INTER_24 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_24),
                _X_INTER_25 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_25),
                _X_INTER_26 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_26),
                _X_INTER_27 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_27),
                _X_INTER_28 = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_INTER_28),
                _X_INTER_29 = oPlanMigrationPost.oInteractionTemplate.X_INTER_29,
                _X_INTER_30 = oPlanMigrationPost.oInteractionTemplate.X_INTER_30,
                _X_PLUS_INTER2INTERACT = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_PLUS_INTER2INTERACT),
                _X_ADJUSTMENT_AMOUNT = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_ADJUSTMENT_AMOUNT),
                _X_ADJUSTMENT_REASON = oPlanMigrationPost.oInteractionTemplate.X_ADJUSTMENT_REASON,
                _X_ADDRESS = oPlanMigrationPost.oInteractionTemplate.X_ADDRESS,
                _X_AMOUNT_UNIT = oPlanMigrationPost.oInteractionTemplate.X_AMOUNT_UNIT,
                _X_BIRTHDAY = strCurrentDate,
                _X_CLARIFY_INTERACTION = oPlanMigrationPost.oInteractionTemplate.X_CLARIFY_INTERACTION,
                _X_CLARO_LDN1 = oPlanMigrationPost.oInteractionTemplate.X_CLARO_LDN1,
                _X_CLARO_LDN2 = oPlanMigrationPost.oInteractionTemplate.X_CLARO_LDN2,
                _X_CLARO_LDN3 = oPlanMigrationPost.oInteractionTemplate.X_CLARO_LDN3,
                _X_CLARO_LDN4 = oPlanMigrationPost.oInteractionTemplate.X_CLARO_LDN4,
                _X_CLAROLOCAL1 = oPlanMigrationPost.oInteractionTemplate.X_CLAROLOCAL1,
                _X_CLAROLOCAL2 = oPlanMigrationPost.oInteractionTemplate.X_CLAROLOCAL2,
                _X_CLAROLOCAL3 = oPlanMigrationPost.oInteractionTemplate.X_CLAROLOCAL3,
                _X_CLAROLOCAL4 = oPlanMigrationPost.oInteractionTemplate.X_CLAROLOCAL4,
                _X_CLAROLOCAL5 = oPlanMigrationPost.oInteractionTemplate.X_CLAROLOCAL5,
                _X_CLAROLOCAL6 = oPlanMigrationPost.oInteractionTemplate.X_CLAROLOCAL6,
                _X_CONTACT_PHONE = oPlanMigrationPost.oInteractionTemplate.X_CONTACT_PHONE,
                _X_IDCARD_LEGAL_REP = oPlanMigrationPost.oInteractionTemplate.X_DNI_LEGAL_REP,
                _X_DOCUMENT_NUMBER = oPlanMigrationPost.oInteractionTemplate.X_DOCUMENT_NUMBER,
                _X_EMAIL = oPlanMigrationPost.oInteractionTemplate.X_EMAIL,
                _X_FIRST_NAME = oPlanMigrationPost.oInteractionTemplate.X_FIRST_NAME,
                _X_FIXED_NUMBER = oPlanMigrationPost.oInteractionTemplate.X_FIXED_NUMBER,
                _X_FLAG_CHANGE_USER = oPlanMigrationPost.oInteractionTemplate.X_FLAG_CHANGE_USER,
                _X_FLAG_LEGAL_REP = oPlanMigrationPost.oInteractionTemplate.X_FLAG_LEGAL_REP,
                _X_FLAG_OTHER = oPlanMigrationPost.oInteractionTemplate.X_FLAG_OTHER,
                _X_FLAG_TITULAR = oPlanMigrationPost.oInteractionTemplate.X_FLAG_TITULAR,
                _X_IMEI = oPlanMigrationPost.oInteractionTemplate.X_IMEI,
                _X_LAST_NAME = oPlanMigrationPost.oInteractionTemplate.X_LAST_NAME,
                _X_LASTNAME_REP = oPlanMigrationPost.oInteractionTemplate.X_LASTNAME_REP,
                _X_LDI_NUMBER = oPlanMigrationPost.oInteractionTemplate.X_LDI_NUMBER,
                _X_NAME_LEGAL_REP = oPlanMigrationPost.oInteractionTemplate.X_NAME_LEGAL_REP,
                _X_OLD_CLARO_LDN1 = oPlanMigrationPost.oInteractionTemplate.X_OLD_CLARO_LDN1,
                _X_OLD_CLARO_LDN2 = oPlanMigrationPost.oInteractionTemplate.X_OLD_CLARO_LDN2,
                _X_OLD_CLARO_LDN3 = oPlanMigrationPost.oInteractionTemplate.X_OLD_CLARO_LDN3,
                _X_OLD_CLARO_LDN4 = oPlanMigrationPost.oInteractionTemplate.X_OLD_CLARO_LDN4,
                _X_OLD_CLAROLOCAL1 = oPlanMigrationPost.oInteractionTemplate.X_OLD_CLAROLOCAL1,
                _X_OLD_CLAROLOCAL2 = oPlanMigrationPost.oInteractionTemplate.X_OLD_CLAROLOCAL2,
                _X_OLD_CLAROLOCAL3 = oPlanMigrationPost.oInteractionTemplate.X_OLD_CLAROLOCAL3,
                _X_OLD_CLAROLOCAL4 = oPlanMigrationPost.oInteractionTemplate.X_OLD_CLAROLOCAL4,
                _X_OLD_CLAROLOCAL5 = oPlanMigrationPost.oInteractionTemplate.X_OLD_CLAROLOCAL5,
                _X_OLD_CLAROLOCAL6 = oPlanMigrationPost.oInteractionTemplate.X_OLD_CLAROLOCAL6,
                _X_OLD_DOC_NUMBER = oPlanMigrationPost.oInteractionTemplate.X_OLD_DOC_NUMBER,
                _X_OLD_FIRST_NAME = oPlanMigrationPost.oInteractionTemplate.X_OLD_FIRST_NAME,
                _X_OLD_FIXED_NUMBER = oPlanMigrationPost.oInteractionTemplate.X_OLD_FIXED_NUMBER,
                _X_OLD_FIXED_PHONE = oPlanMigrationPost.oInteractionTemplate.X_OLD_FIXED_PHONE,
                _X_OLD_LAST_NAME = oPlanMigrationPost.oInteractionTemplate.X_OLD_LAST_NAME,
                _X_OLD_LDI_NUMBER = oPlanMigrationPost.oInteractionTemplate.X_OLD_LDI_NUMBER,
                _X_OPERATION_TYPE = ConfigurationManager.AppSettings("strPlanMigrationIFIOperation"),
                _X_OTHER_DOC_NUMBER = oPlanMigrationPost.oInteractionTemplate.X_OTHER_DOC_NUMBER,
                _X_OTHER_FIRST_NAME = oPlanMigrationPost.oInteractionTemplate.X_OTHER_FIRST_NAME,
                _X_OTHER_LAST_NAME = oPlanMigrationPost.oInteractionTemplate.X_OTHER_LAST_NAME,
                _X_OTHER_PHONE = oPlanMigrationPost.oInteractionTemplate.X_OTHER_PHONE,
                _X_PHONE_LEGAL_REP = oPlanMigrationPost.oInteractionTemplate.X_PHONE_LEGAL_REP,
                _X_REFERENCE_PHONE = oPlanMigrationPost.oInteractionTemplate.X_REFERENCE_PHONE,
                _X_REASON = oPlanMigrationPost.oInteractionTemplate.X_REASON,
                _X_MODEL = oPlanMigrationPost.oInteractionTemplate.X_MODEL,
                _X_LOT_CODE = oPlanMigrationPost.oInteractionTemplate.X_LOT_CODE,
                _X_FLAG_REGISTERED = oPlanMigrationPost.oInteractionTemplate.X_FLAG_REGISTERED,
                _X_REGISTRATION_REASON = oPlanMigrationPost.oInteractionTemplate.X_REGISTRATION_REASON,
                _X_CLARO_NUMBER = oPlanMigrationPost.oInteractionTemplate.X_CLARO_NUMBER,
                _X_MONTH = oPlanMigrationPost.oInteractionTemplate.X_MONTH,
                _X_OST_NUMBER = oPlanMigrationPost.oInteractionTemplate.X_OST_NUMBER,
                _X_BASKET = oPlanMigrationPost.oInteractionTemplate.X_BASKET,
                _X_EXPIRE_DATE = strCurrentDate,
                _X_ADDRESS5 = oPlanMigrationPost.oInteractionTemplate.X_ADDRESS5,
                _X_CHARGE_AMOUNT = Claro.Convert.ToInt(oPlanMigrationPost.oInteractionTemplate.X_CHARGE_AMOUNT),
                _X_CITY = oPlanMigrationPost.oInteractionTemplate.X_CITY,
                _X_CONTACT_SEX = oPlanMigrationPost.oInteractionTemplate.X_CONTACT_SEX,
                _X_DEPARTMENT = oPlanMigrationPost.oInteractionTemplate.X_DEPARTMENT,
                _X_DISTRICT = oPlanMigrationPost.oInteractionTemplate.X_DISTRICT,
                _X_EMAIL_CONFIRMATION = oPlanMigrationPost.oInteractionTemplate.X_EMAIL_CONFIRMATION,
                _X_FAX = oPlanMigrationPost.oInteractionTemplate.X_FAX,
                _X_FLAG_CHARGE = oPlanMigrationPost.oInteractionTemplate.X_FLAG_CHARGE,
                _X_MARITAL_STATUS = oPlanMigrationPost.oInteractionTemplate.X_MARITAL_STATUS,
                _X_OCCUPATION = oPlanMigrationPost.oInteractionTemplate.X_OCCUPATION,
                _X_POSITION = oPlanMigrationPost.oInteractionTemplate.X_POSITION,
                _X_REFERENCE_ADDRESS = oPlanMigrationPost.oInteractionTemplate.X_REFERENCE_ADDRESS,
                _X_TYPE_DOCUMENT = oPlanMigrationPost.oInteractionTemplate.X_TYPE_DOCUMENT,
                _X_ZIPCODE = oPlanMigrationPost.oInteractionTemplate.X_ZIPCODE,
                _X_ICCID = oPlanMigrationPost.oInteractionTemplate.X_ICCID,
                _X_FLAG_CONTINGENCY = ConfigurationManager.AppSettings("strFlagContingencyTipiIFI")
            };

            strConstancy = GetGenerateConstancyXml(oAuditRequest.Session, oTemplate);
            byte[] byt = ASCIIEncoding.UTF8.GetBytes(strConstancy);
            strConstancy = System.Convert.ToBase64String(byt);

            PostIFIService.PlanMigrationBodyRequest oBodyRequest = new PostIFIService.PlanMigrationBodyRequest { 
                ServvMsisd = oPlanMigrationPost.oProgramTask.SERVV_MSISDN,
                ServdProgDate = Claro.Convert.ToDate(oPlanMigrationPost.oProgramTask.SERVD_DATEPROG),
                CodId = oPlanMigrationPost.oProgramTask.CO_ID,
                CustomerId = oPlanMigrationPost.oProgramTask.CUSTOMER_ID,
                ServvTypeService = ConfigurationManager.AppSettings("ApplicationNameIFI"),
                ServiCoser = oPlanMigrationPost.oProgramTask.SERVC_CO_SER,
                TypeReg = Claro.Constants.LetterA,
                ServvUserSystem = oPlanMigrationPost.oProgramTask.SERVV_USER_SYSTEM,
                ServvUserApp = oPlanMigrationPost.oProgramTask.SERVV_USER_APLICATION,
                ServvEmailUserApp = oPlanMigrationPost.oProgramTask.SERVV_EMAIL_USER_APP,
                ServcState = Claro.Constants.NumberOneString,
                DescCoSer = oPlanMigrationPost.oProgramTask.SERVC_DES_CO_SER,
                ServcAccountNumber = oPlanMigrationPost.oProgramTask.SERVC_NUMBERACCOUNT,
                MainInteraction = oInteraction,
                PlusInteraction = oTemplate,
                CalculatedPenalty = Claro.Convert.ToDouble(oPlanMigrationPost.strPenalty),
                Repayment = Claro.Convert.ToDouble(oPlanMigrationPost.strRefund),
                AmountPenaltyCharge = Claro.Convert.ToDouble(oPlanMigrationPost.strTotalPenalty),
                FlagPenalty = oPlanMigrationPost.strFlagPenalty,
                FlagLoyalPenalty = oPlanMigrationPost.strFlagLoyaltyPenalty,
                AmountLoyalPenalty = Claro.Convert.ToDouble(oPlanMigrationPost.strTotalDiscountPenalty),
                OCCConceptPenalty = Claro.Convert.ToDouble(oPlanMigrationPost.strOCCPenaltyConcept),
                OCCPenalty = Claro.Convert.ToInt(oPlanMigrationPost.strOCCPenalty),
                IdeaiWS = oPlanMigrationPost.strIdeaiWS,
                IpClient = System.Web.HttpContext.Current.Request.UserHostAddress,
                UserAccount = oPlanMigrationPost.oInteraction.Agenth,
                ServerName = oAuditRequest.applicationName,
                Phone = oPlanMigrationPost.strPhone,
                Text = string.Format("/Ip Cliente: {0}/Usuario: {1}/Opcion: {2}/Fecha y Hora: {3}", System.Web.HttpContext.Current.Request.UserHostAddress, oPlanMigrationPost.oInteraction.Agenth, ConfigurationManager.AppSettings("MigracionPlanIFITransaction"), DateTime.Now.ToString()),
                tmCodeNew = oPlanMigrationPost.lstNewServices.First().TmCode,
                ContactObjId = oPlanMigrationPost.oInteraction.ObjidContacto,
                FlagReg = oPlanMigrationPost.oInteraction.InsertPor,
                SimCard = oPlanMigrationPost.strSIMCard,
                tmCodeCurrent = oPlanMigrationPost.strCurrentTMCode,
                ServiceList = lstServices,
                SnCodeList = lstSNCode,
                ConstancyParameter = strConstancy,
                ProductCode = oPlanMigrationPost.strCodProduct
            };

            PostIFIService.PlanMigrationHeaderRequest oPlanMigrationHeader = new PostIFIService.PlanMigrationHeaderRequest {
                HeaderRequest = oHeaderRequest
            };

            PostIFIService.PlanMigrationMessageRequest oMessageRequest = new PostIFIService.PlanMigrationMessageRequest
            {
                Header = oPlanMigrationHeader,
                Body = oBodyRequest,
            };

            PostIFIService.PlanMigrationRequest oPlanMigrationRequest = new PostIFIService.PlanMigrationRequest
            {
                MessageRequest = oMessageRequest,
                audit = oAuditRequest
            };

            Claro.Web.Logging.Info(oPlanMigrationPost.strIDSession, oPlanMigrationRequest.audit.transaction, "Input: " + JsonConvert.SerializeObject(oPlanMigrationRequest));

            PostIFIService.PlanMigrationResponse oPlanMigrationResponse = null;

            try
            {
                oPlanMigrationResponse = Claro.Web.Logging.ExecuteMethod<PostIFIService.PlanMigrationResponse>(() =>
                {
                    return new PostIFIService.PostIFIServiceClient().PostPlanMigration(oPlanMigrationRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oPlanMigrationPost.strIDSession, oPlanMigrationRequest.audit.transaction, Claro.Utils.GetExceptionMessage(ex));
            }

            Claro.Web.Logging.Info(oPlanMigrationPost.strIDSession, oPlanMigrationRequest.audit.transaction, "Output: " + JsonConvert.SerializeObject(oPlanMigrationResponse));
            
            return Json(new { data = oPlanMigrationResponse });
        }

        public string GetGenerateConstancyXml(string strIDSession, PostIFIService.TemplateInteraction oInteractionTemplate)
        {
            string xmlConstancyPDF = string.Empty;
            try
            {
                string pathFileXml = ConfigurationManager.AppSettings("strPlanMigrationIFIRouteXml");
                var listParamConstancyPdf = new List<string>
                {
                    ConfigurationManager.AppSettings("strPlanMigrationIFIConstancy"), //FORMATO_TRANSACCION
                    oInteractionTemplate._X_INTER_6, //PUNTO_ATENCION
                    DateTime.Now.ToShortDateString(), //FECHA_TRANSACCION_PROGRAM
                    oInteractionTemplate._X_CLAROLOCAL1, //TITULAR_CLIENTE
                    oInteractionTemplate._X_TYPE_DOCUMENT, //TIPO_DOC_IDENTIDAD
                    oInteractionTemplate._X_DOCUMENT_NUMBER, //NRO_DOC_IDENTIDAD
                    oInteractionTemplate._X_NAME_LEGAL_REP, //REPRESENTANTE_LEGAL
                    oInteractionTemplate._X_AMOUNT_UNIT, //CICLO_FACTURACION
                    "$CodigoInteraccion", //CASO_INTER
                    oInteractionTemplate._X_CLARO_NUMBER, //NRO_SERVICIO
                    oInteractionTemplate._X_INTER_1, //PLAN_ACTUAL
                    oInteractionTemplate._X_POSITION, //ESTADO_SERVICIO
                    oInteractionTemplate._X_REASON, //TIPO_ACUERDO
                    oInteractionTemplate._X_INTER_3, //CARGO_FIJO_CON_IGV
                    oInteractionTemplate._X_INTER_2, //NUEVO_PLAN
                    oInteractionTemplate._X_INTER_4, //CARGO_FIJO_NUEVO_PLAN
                    oInteractionTemplate._X_INTER_17, //REINTEGRO
                    oInteractionTemplate._X_INTER_18, //PENALIDAD
                    oInteractionTemplate._X_INTER_19, //TOTAL_PENALIDAD
                    oInteractionTemplate._X_INTER_5, //FECHA_EJECUCION
                    oInteractionTemplate._X_CLAROLOCAL5, //CODIGO_ASESOR
                    oInteractionTemplate._X_ICCID, //NOMBRE_ASESOR
                };
                var listLabels = GetXmlToString(App_Code.Common.GetApplicationRoute() + pathFileXml);
                var count = 0;
                var xmlGenerated = new System.Text.StringBuilder();
                foreach (string key in listLabels)
                {
                    xmlGenerated.Append(string.Format("<{0}>{1}</{2}>\r\n", key, listParamConstancyPdf[count], key));
                    count++;
                }

                xmlConstancyPDF = String.Format("$CabeceraXml<PLANTILLA>{0}</PLANTILLA>", xmlGenerated);
                Claro.Web.Logging.Info(strIDSession, "GetGenerateConstancyXml()", "xmlConstancyPDF:    " + xmlConstancyPDF);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIDSession, "GetGenerateConstancyXml()", "ERROR:    " + ex.InnerException);
            }

            return xmlConstancyPDF.ToString();
        }
	}
}