using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using CommonService = Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.AuditRequest;
using KEY = Claro.ConfigurationManager;
using APPCODE = Claro.SIACU.Web.WebApplication.IFI.App_Code;
using Model = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using HelperCommon = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices;
using Claro.SIACU.Web.WebApplication.IFI.PostIFIService;
using System.Web.Mvc;
using Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices;
using Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using Claro.Web;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;
using System.DirectoryServices;

namespace Claro.SIACU.Web.WebApplication.IFI.Controllers
{
    public class CommonServicesController : Controller
    {
        private readonly Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.CommonIFIServiceClient _oServiceCommon = new Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.CommonIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient _oServiceFixed = new Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient();

        private readonly Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient _oServicePostpaid =
         new Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient();

        public CommonServicesController()
        {
            Mappings.AutoMapperConfig.RegisterMappings();
        }




        public string CurrentTerminal()
        {
            return System.Web.HttpContext.Current.Request.UserHostAddress;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIpClient"></param>
        /// <returns></returns>
        public string ClientHostname(string strIpClient)
        {
            string hostName = "";
            try
            {
                hostName = Dns.GetHostByAddress(strIpClient).HostName;
            }
            catch
            {
                //Claro.Web.Logging.Error(audit.Session, audit.transaction, String.Format(Constant.Message_ErrorGetIpClient_LogTrx,
                //    DateTime.Now.ToString("yyyyMMdd"), strIpCliente));
                hostName = strIpClient;
            }
            return hostName;
        }

        public string CurrentUser(string idSession)
        {
            return idSession;
            //var strUser = string.Empty;
            //try
            //{
            //    var strDomainUser = HttpContext.Request.ServerVariables["LOGON_USER"];
            //    //strUser = ConfigurationManager.AppSettings("UsuarioIdPrueba");
            //    if (string.IsNullOrEmpty(strUser))
            //    {
            //        strUser = strDomainUser.Substring(strDomainUser.IndexOf("\\", StringComparison.Ordinal) + 1);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logging.Error(idSession, idSession, ex.Message);
            //}

            ////string user = KEY.AppSettings("TestUser");
            ////string user = string.Empty;
            ////if (!String.IsNullOrEmpty(idSession)) user = idSession;
            ////if (!String.IsNullOrEmpty(user)) user = user.ToUpper();
            //return strUser.ToUpper();
        }

        public GenerateConstancyResponseCommon GenerateContancyPDF(string idSession, ParametersGeneratePDF parameters)
        {
            parameters.StrServidorGenerarPDF = KEY.AppSettings("strServidorGenerarPDF");
            parameters.StrServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
            parameters.StrCarpetaPDFs = KEY.AppSettings("strCarpetaPDFs");

            //ADICIÓN DE S/. AL CARGO FIJO CON IGV
            parameters.StrCargoFijoConIGV = "S/. " + parameters.StrCargoFijoConIGV;
            parameters.StrCostoTransaccion = "S/. " + parameters.StrCostoTransaccion;
            parameters.StrCargoFijo = "S/. " + parameters.StrCargoFijo;

            var strTerminacionPDF = KEY.AppSettings("strTerminacionPDF");

            GenerateConstancyRequestCommon request = new GenerateConstancyRequestCommon()
            {
                ParametersGeneratePDFGeneric = parameters,
                audit = Claro.SIACU.Web.WebApplication.IFI.App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(idSession)
            };
            Logging.Info(idSession, "", "IN GenerateContancyPDF()");
            GenerateConstancyResponseCommon objResponse =
            Logging.ExecuteMethod<GenerateConstancyResponseCommon>(() =>
            {
                return _oServiceCommon.GetGenerateContancyPDF(request);
            });
            Logging.Info(idSession, "", " Generated:  " + objResponse.Generated.ToString());
            if (objResponse.Generated)
            {
                string strFechaTransaccion = DateTime.Today.ToShortDateString().Replace("/", "_");

                string strNamePDF = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion, parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion.Replace("/", "_"), strTerminacionPDF);

                string strNamePath = string.Format("{0}{1}{2}", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);

                string strDocumentName = string.Format("{0}_{1}_{2}_{3}", parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion, strTerminacionPDF);

                objResponse.FullPathPDF = strNamePDF;
                objResponse.Path = strNamePath;
                objResponse.Document = strDocumentName;
                Logging.Info(idSession, "", " FullPathPDF:    " + objResponse.FullPathPDF);
            }
            else
            {
                objResponse.FullPathPDF = string.Empty;
                objResponse.Path = string.Empty;
                objResponse.Document = string.Empty;
            }

            Logging.Info(idSession, "", "OUT GenerateContancyPDF()");
            return objResponse;
        }

        public void InsertEvidence(string strIdSesison, string strID, string strCustomerID, string strSubClass, string strSubClassCode,
            string strInteraction, string strTransactionName, string strPath, string strDocument, string strUser)
        {
            InsertEvidenceResponse oResponse = new InsertEvidenceResponse();
            InsertEvidenceRequest oRequest = new InsertEvidenceRequest();
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSesison);
            oRequest.audit = audit;
            oRequest.Evidence = new Evidence();
            oRequest.Evidence.StrTransactionType = KEY.AppSettings("strTransactionType");
            oRequest.Evidence.StrTransactionCode = strInteraction;
            oRequest.Evidence.StrCustomerCode = strCustomerID;
            oRequest.Evidence.StrPhoneNumber = strID; //PRE-POST: MovilNumber HFC-LTE: Contract
            oRequest.Evidence.StrTypificationCode = strSubClassCode;
            oRequest.Evidence.StrTypificationDesc = strSubClass;
            oRequest.Evidence.StrCommercialDesc = string.Empty;
            oRequest.Evidence.StrProductType = string.Empty;
            oRequest.Evidence.StrServiceChannel = string.Empty;
            oRequest.Evidence.StrTransactionDate = DateTime.Today.ToShortDateString();
            oRequest.Evidence.StrActivationDate = string.Empty;
            oRequest.Evidence.StrSuspensionDate = string.Empty;
            oRequest.Evidence.StrServiceStatus = string.Empty;

            oRequest.Evidence.StrDocumentName = strDocument;
            oRequest.Evidence.StrDocumentType = strTransactionName;
            oRequest.Evidence.StrDocumentExtension = KEY.AppSettings("strDocumentoExtension");
            oRequest.Evidence.StrDocumentPath = strPath;
            oRequest.Evidence.StrUserName = strUser;

            try
            {
                oResponse = Logging.ExecuteMethod<InsertEvidenceResponse>(() =>
                {
                    return _oServiceCommon.GetInsertEvidence(oRequest);
                });
                Logging.Info(audit.Session, audit.transaction, "Evidencia registra correctamente : " + oResponse.StrMsgText);
            }
            catch (Exception ex)
            {
                Logging.Error(audit.Session, audit.transaction, Claro.Utils.GetExceptionMessage(ex));
            }
        }

        public List<Claro.Utils.ItemGeneric> GetListCacDac(string strIdSession)
        {
            List<Claro.Utils.ItemGeneric> list = new List<Claro.Utils.ItemGeneric>();

            CacDacTypeResponseCommon objCacDacTypeResponseCommon;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);

            CacDacTypeRequestCommon objCacDacTypeRequestCommon = new CacDacTypeRequestCommon()
            {
                audit = audit
            };

            try
            {
                objCacDacTypeResponseCommon = Logging.ExecuteMethod<CacDacTypeResponseCommon>(() =>
                {
                    return _oServiceCommon.GetCacDacType(objCacDacTypeRequestCommon);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objCacDacTypeRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }


            if (objCacDacTypeResponseCommon != null && objCacDacTypeResponseCommon.CacDacTypes != null)
            {
                foreach (Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.ListItem item in objCacDacTypeResponseCommon.CacDacTypes)
                {
                    list.Add(new Claro.Utils.ItemGeneric()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }

            return list;
        }

        public Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.Iteraction InteractionData(string idSession, string idContact, string strPhone,
            string strNotes, string strType,
            string strClass, string strSubClass, string isTFI)
        {

            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.Iteraction entity = new Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.Iteraction();
            try
            {
                entity.OBJID_CONTACTO = idContact;
                entity.FECHA_CREACION = DateTime.Now.ToShortDateString();
                entity.TELEFONO = strPhone;
                entity.TIPO = strType;
                entity.CLASE = strClass;
                entity.SUBCLASE = strSubClass;
                entity.TIPO_INTER = ConfigurationManager.AppSettings("AtencionDefault");
                entity.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                entity.RESULTADO = ConfigurationManager.AppSettings("Ninguno");
                entity.HECHO_EN_UNO = Claro.Constants.NumberZeroString;
                entity.NOTAS = strNotes;
                entity.FLAG_CASO = Claro.Constants.NumberZeroString;
                entity.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
                entity.AGENTE = CurrentUser(idSession);
                entity.ES_TFI = isTFI;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entity;
        }

        public Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.InsertTemplateInteraction InteractionTemplateData(string idInteraction,
            string nameTransaction, string strPhoneNumber,
            string strReason, string firstName, string lastName, string strLegalRepresentative, string strDocumentType,
            string strNumberDocument,
            string strStartDate, string strEndDate, string flagGenerateOCC, double dblMonto, string idCACDAC)
        {
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.InsertTemplateInteraction entity =
                new Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.InsertTemplateInteraction();
            try
            {
                entity._NOMBRE_TRANSACCION = nameTransaction;
                entity._ID_INTERACCION = idInteraction;
                entity._X_CLARO_NUMBER = strPhoneNumber;
                entity._X_ADJUSTMENT_REASON = strReason;
                entity._X_TYPE_DOCUMENT = strDocumentType;
                entity._X_DOCUMENT_NUMBER = strNumberDocument;
                entity._X_FIRST_NAME = firstName;
                entity._X_LAST_NAME = lastName;
                entity._X_NAME_LEGAL_REP = strLegalRepresentative;
                entity._X_DNI_LEGAL_REP = strLegalRepresentative;
                entity._X_INTER_1 = DateTime.Now.ToString("dd/MM/yyyy");
                entity._X_INTER_20 = strStartDate;
                entity._X_INTER_21 = strEndDate;
                entity._X_FLAG_REGISTERED = Claro.Constants.NumberZeroString;// X_INTER_3 => Fidelidad
                entity._X_INTER_22 = (flagGenerateOCC == "T") ? dblMonto : 0;// X_INTER_1 => Monto
                entity._X_INTER_15 = idCACDAC;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entity;
        }

        public void InsertBusinessInteraction2(Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.Iteraction item, string idSession,
            out string rInteractionId, out string rFlagInsertion, out string rMsgText)
        {
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(idSession);

            BusinessInteraction2RequestCommon request =
                new BusinessInteraction2RequestCommon()
                {
                    Item = item,
                    audit = audit
                };

            BusinessInteraction2ResponseCommon objResponse =
                Logging.ExecuteMethod<BusinessInteraction2ResponseCommon>(() =>
                {
                    return _oServiceCommon.GetInsertBusinnesInteraction2(request);
                });
            rInteractionId = objResponse.InteractionId;
            rFlagInsertion = objResponse.FlagInsertion;
            rMsgText = objResponse.MsgText;
        }

        public string GetNumber(string idSession, bool flagCountry, string strNumber)
        {
            DateTime dateAVM;
            DateTime dateCurrent;
            string numberGenerated = String.Empty, numberTelehone = String.Empty;
            string strInternationalCode = KEY.AppSettings("gInternationalCode");
            int maximumLenghNumberTelephone = Int32.Parse(KEY.AppSettings("gMaximumLengthPhone"));
            Logging.Info(idSession, "Transaction: ", "IN GetNumber()");
            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
            dateAVM = DateTime.Parse(KEY.AppSettings("strFechaAVM"), culture,
                System.Globalization.DateTimeStyles.AssumeLocal);
            dateCurrent = DateTime.Now;
            Logging.Info(idSession, "Transaction: ", "GetNumber() - strInternationalCode: " + strInternationalCode);

            if (string.IsNullOrEmpty(strNumber))
            {
                numberTelehone = string.Empty;
                Logging.Info(idSession, "Transaction: ", "GetNumber() - numberTelehone: " + numberTelehone);
            }
            else if (strNumber.Length == maximumLenghNumberTelephone)
            {
                if ((dateCurrent - dateAVM).Days < 0)
                {
                    Logging.Info(idSession, "Transaction: ", "GetNumber() - numberTelehone: " + numberTelehone);
                    numberGenerated = GetNumberGenerated(idSession, strNumber);
                    if (!String.IsNullOrEmpty(numberGenerated))
                    {
                        if (flagCountry == true)
                            numberTelehone = strInternationalCode + numberGenerated.Trim();
                        else
                            numberTelehone = numberGenerated.Trim();
                    }
                    else
                        numberTelehone = string.Empty;
                }
                else
                {
                    if (flagCountry == true)
                        numberTelehone = strInternationalCode + strNumber;
                    else
                        numberTelehone = strNumber;
                }
            }
            else
            {
                if (flagCountry == true)
                    numberTelehone = strInternationalCode + strNumber;
                else
                    numberTelehone = strNumber;
            }
            Logging.Info(idSession, "Transaction: ", "OUT GetNumber()");
            return numberTelehone;
        }



        public HelperCommon.Typification LoadTypification(string idSession, string transactionName, string strType)
        {
            HelperCommon.Typification typification = new HelperCommon.Typification();

            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(idSession);
            try
            {
                TypificationRequest request = new TypificationRequest()
                {
                    TRANSACTION_NAME = transactionName,
                    audit = audit
                };
                TypificationResponse response = Logging.ExecuteMethod<TypificationResponse>(() =>
                {
                    return _oServiceCommon.GetTypification(request);
                });

                if (response.ListTypification.Count > 0)
                {
                    if (response.ListTypification.Count == 1)
                    {
                        typification = MappingTypification(response.ListTypification[0]);
                    }
                    else
                    {
                        for (int i = 0; i < response.ListTypification.Count; i++)
                        {
                            if (response.ListTypification[i].TIPO.ToUpper().Equals(strType.ToUpper()))
                            {
                                typification = MappingTypification(response.ListTypification[i]);
                                break;
                            }
                        }
                    }
                }
                //Inicio Bloque Temporal 
                if (String.IsNullOrEmpty(typification.Type))
                {
                    Logging.Info("Persquash", "LoadTypification", String.Format("ListTypification.Count: {0}", response.ListTypification.Count)); // Temporal 
                    for (int i = 0; i < response.ListTypification.Count; i++)
                    {
                        Logging.Info("Persquash", "LoadTypification", String.Format("TIPO: {0};CLASE: {1};SUBCLASE:{2}", response.ListTypification[i].TIPO,
                            response.ListTypification[i].CLASE, response.ListTypification[i].SUBCLASE)); // Temporal 
                    }
                }
                //Fin Bloque Temporal
            }
            catch (Exception ex)
            {
                Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            return typification;
        }

        public bool RegisterAuditGeneral(string idSession, string strPhone, string strAmount, string strText,
            string strService, string strTransaction)
        {
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(idSession);
            if (strText.Length > 255) strText = strText.Substring(0, 255);

            string strIpClient = CurrentTerminal();
            string strClientName = ClientHostname(strIpClient);
            string strUserAccount = CurrentUser(idSession);

            SaveAuditRequestCommon request = new SaveAuditRequestCommon()
            {
                vCuentaUsuario = strUserAccount,
                vIpCliente = strIpClient,
                vIpServidor = audit.ipAddress,
                vMonto = strAmount,
                vNombreCliente = strClientName,
                vNombreServidor = audit.applicationName,
                vServicio = strService,
                vTelefono = strPhone,
                vTexto = strText,
                vTransaccion = strTransaction,
                audit = audit
            };

            SaveAuditResponseCommon objResponse =
                Logging.ExecuteMethod<SaveAuditResponseCommon>(() =>
                {
                    return _oServiceCommon.SaveAudit(request);
                });

            return objResponse.respuesta;
        }

        public bool RegisterLogTrx(string idSession, string strPhone, string strInteraction, string strTypification,
            string strParamIN, string strParamOUT, string strOpcionCode, string strAccion, string strAccionEvento,
            string nameTransaction)
        {
            bool salida = false;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(idSession);

            string strIpCliente = CurrentTerminal();
            string strCliente = ClientHostname(strIpCliente);
            //try
            //{
            //    strCliente = System.Net.Dns.GetHostByAddress(strIpCliente).HostName;
            //}
            //catch
            //{
            //    Claro.Web.Logging.Error(audit.Session, audit.transaction, String.Format(Constant.Message_ErrorGetIpClient_LogTrx,
            //        DateTime.Now.ToString("yyyyMMdd"), strIpCliente));
            //    strCliente = strIpCliente;
            //} 
            string strMsg = "";

            try
            {
                InsertLogTrxRequestCommon request =
                    new InsertLogTrxRequestCommon()
                    {
                        Accion = String.Format("{0} - {1}", strAccion, strAccionEvento),
                        Aplicacion = Claro.SIACU.Constants.NameApplication,
                        audit = audit,
                        IdInteraction = strInteraction,
                        IdTypification = strTypification,
                        InputParameters = strParamIN,
                        OutpuParameters = strParamOUT,
                        IPPCClient = strIpCliente,
                        PCClient = strCliente,
                        IPServer = audit.ipAddress,
                        NameServer = audit.applicationName,
                        Opcion = strOpcionCode,
                        Phone = strPhone,
                        Transaccion = nameTransaction,
                        User = audit.userName
                    };
                string flagInsertion = InsertLogTrx(request);
                if (flagInsertion.Equals(Claro.SIACU.Constants.OK)) salida = true;
            }
            catch (Exception ex)
            {
                Logging.Error(audit.Session, audit.transaction,
                    String.Format(Claro.SIACU.Constants.Message_ErrorGeneral, ex.Message));
                salida = false;
                strMsg = ex.Message;
            }
            return salida;
        }



        //        #region Funciones Privadas







        private string GetNumberGenerated(string idSession, string msisdn)
        {
            string strNumberGenerated = String.Empty;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(idSession);

            NumberEAIResponse objNumberEAI = GetNumberEAI(msisdn, audit, ref strNumberGenerated);
            if (string.IsNullOrEmpty(strNumberGenerated))
            {
                NumberGWPResponse objNumberGWP = GetNumberGWP(msisdn, audit, ref strNumberGenerated);
            }
            return strNumberGenerated;
        }

        private NumberEAIResponse GetNumberEAI(string msisdn, Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit,
            ref string strNumberGenerated)
        {
            NumberEAIRequest objRequest = new NumberEAIRequest()
            {
                Msisdn = msisdn,
                audit = audit
            };

            NumberEAIResponse objResponse = Logging.ExecuteMethod<NumberEAIResponse>(() =>
            {
                return _oServiceCommon.GetNumberEAI(objRequest);
            });
            strNumberGenerated = objResponse.Number;
            return objResponse;
        }

        private NumberGWPResponse GetNumberGWP(string msisdn, Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit,
            ref string strNumberGenerated)
        {
            NumberGWPRequest objRequest = new NumberGWPRequest()
            {
                Msisdn = msisdn,
                audit = audit
            };
            NumberGWPResponse objResponse = Logging.ExecuteMethod<NumberGWPResponse>(() =>
            {
                return _oServiceCommon.GetNumberGWP(objRequest);
            });
            strNumberGenerated = objResponse.Number;
            return objResponse;
        }

        private string InsertLogTrx(InsertLogTrxRequestCommon request)
        {
            InsertLogTrxResponseCommon objResponse =
                Logging.ExecuteMethod<InsertLogTrxResponseCommon>(() =>
                {
                    return _oServiceCommon.InsertLogTrx(request);
                });
            return objResponse.FlagInsertion;
        }

        private List<HelperCommon.GenericItem> MappingListItemGeneric(List<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.ListItem> listDB)
        {
            List<HelperCommon.GenericItem> list = new List<HelperCommon.GenericItem>();
            HelperCommon.GenericItem entity = null;
            foreach (var item in listDB)
            {
                entity = new HelperCommon.GenericItem()
                {
                    Code = item.Code,
                    Description = item.Description
                };
                list.Add(entity);
            }
            return list;
        }



        private HelperCommon.Typification MappingTypification(Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.Typification typifDB)
        {
            HelperCommon.Typification entity = null;
            entity = new HelperCommon.Typification()
            {
                IdClass = typifDB.CLASE_CODE,
                Class = typifDB.CLASE,
                IdSubClass = typifDB.SUBCLASE_CODE,
                SubClass = typifDB.SUBCLASE,
                Type = typifDB.TIPO
            };
            return entity;
        }




        public JsonResult GetCacDacType(string strIdSession)
        {

            CacDacTypeResponseCommon objCacDacTypeResponseCommon;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);
            CacDacTypeRequestCommon objCacDacTypeRequestCommon =
                new CacDacTypeRequestCommon()
                {
                    audit = audit
                };

            try
            {
                objCacDacTypeResponseCommon =
                    Logging.ExecuteMethod<CacDacTypeResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetCacDacType(objCacDacTypeRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objCacDacTypeRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            CommonServices objCommonServices = null;
            if (objCacDacTypeResponseCommon != null && objCacDacTypeResponseCommon.CacDacTypes != null)
            {
                objCommonServices = new CommonServices();
                List<CacDacTypeVM> listCacDacTypes =
                    new List<CacDacTypeVM>();

                foreach (Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.ListItem item in objCacDacTypeResponseCommon.CacDacTypes)
                {
                    listCacDacTypes.Add(new CacDacTypeVM()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonServices.CacDacTypes = listCacDacTypes;
            }

            return Json(new { data = objCommonServices });
        }





        //public JsonResult GetMotive(string strIdSession, string strIdArea)
        //{
        //    try
        //    {
        //        PostTransacService.AuditRequest audit =
        //            APPCODE.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
        //        MotiveByAreaRequest model =
        //            new MotiveByAreaRequest();

        //        #region Momentaneo

        //        model.audit = audit;

        //        #endregion

        //        model.strIdArea = strIdArea;
        //        PostTransacServiceClient Service =
        //            new PostTransacServiceClient();
        //        var result = Service.GetMotiveByArea(model);
        //        return Json(result.lstReasonByArea, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public JsonResult GetSubMotive(string strIdSession, string strIdArea, string strIdMotive)
        //{
        //    try
        //    {
        //        PostTransacService.AuditRequest audit =
        //            APPCODE.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
        //        SubMotiveRequest model =
        //            new SubMotiveRequest();

        //        #region Momentaneo

        //        model.audit = audit;

        //        #endregion

        //        model.strIdArea = strIdArea;
        //        model.strIdMotive = strIdMotive;
        //        PostTransacServiceClient Service =
        //            new PostTransacServiceClient();
        //        var result = Service.GetSubMotive(model);
        //        return Json(result.lstSubMotive, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public JsonResult GetListValueXmlMethod(string strIdSession, string strNameFunction, string strFlagCode = "", string fileName = "")
        //{
        //    List<ListItemVM> lstListItemVm = new List<ListItemVM>();

        //    List<ItemGeneric> listaItem = Functions.GetListValuesXML(strNameFunction, strFlagCode, fileName);
        //    listaItem.ForEach(item =>
        //    {
        //        lstListItemVm.Add(new ListItemVM()
        //        {
        //            Code = item.Code,
        //            Code2 = item.Code2,
        //            Description = item.Description
        //        });
        //    });

        //    lstListItemVm = lstListItemVm.Where(x => x.Code2 != "CO").ToList();

        //    return Json(new { data = lstListItemVm });
        //}

        //public List<ListItemVM> ListValueXmlMethod(string keyAppSettings, string strFlagCode = "", string fileName = "")
        //{
        //    List<ListItemVM> lstListItemVm = new List<ListItemVM>();

        //    List<ItemGeneric> listaItem = Functions.GetListValuesXML(keyAppSettings, strFlagCode, fileName);
        //    listaItem.ForEach(item =>
        //    {
        //        lstListItemVm.Add(new ListItemVM()
        //        {
        //            Code = item.Code,
        //            Code2 = item.Code2,
        //            Description = item.Description
        //        });
        //    });

        //    lstListItemVm = lstListItemVm.Where(x => x.Code2 != "CO").ToList();
        //    return lstListItemVm;

        //    #region MyRegion
        //List<ListItemVM> lstListItemVM = new List<ListItemVM>();
        //var objAuditRequest = App_Code.APPCODE.Common.CreateAuditRequest<AuditRequestCommon>(strIdSession);
        //var objListItemRequest = new ListItemRequest
        //{
        //    audit = objAuditRequest,
        //    strFlagCode = "1",
        //    fileName = fileName
        //};

        //string strTypeLineAct = string.Empty;
        //strTypeLineAct = "CO";

        //if (ConfigurationManager.AppSettings("gConstTipoLineaActual").Split('|')[0].Trim() == keyAppSettings)
        //{
        //    objListItemRequest.strNameFunction = ConfigurationManager.AppSettings("strObtenerRubroTipCliMOV");
        //}
        //else if (ConfigurationManager.AppSettings("gConstTipoLineaActual").Split('|')[1].Trim() == keyAppSettings)
        //{
        //    objListItemRequest.strNameFunction = ConfigurationManager.AppSettings("strObtenerRubroTipCliTFI");
        //}
        //else
        //{
        //    objListItemRequest.strNameFunction = keyAppSettings;
        //    objListItemRequest.strFlagCode = "";
        //}
        //try
        //{
        //    var objListItemResponse = Logging.ExecuteMethod<ListItemResponse>(() =>
        //    {
        //        return _oServiceCommon.GetListValueXML(objListItemRequest);
        //    });
        //    objListItemResponse.lstListItem.ToList().ForEach(item =>
        //    {
        //        lstListItemVM.Add(new ListItemVM()
        //        {
        //            Code = item.Code,
        //            Code2 = item.Code2,
        //            Description = item.Description
        //        });
        //    });

        //    lstListItemVM = lstListItemVM.Where(x => x.Code2 != strTypeLineAct).ToList();
        //}
        //catch (Exception ex)
        //{
        //    Logging.Error(objAuditRequest.Session, objListItemRequest.audit.transaction, ex.Message);
        //    throw new Exception(objAuditRequest.transaction);
        //}
        //return lstListItemVM;


        //    #endregion

        //}


        //        public JsonResult GetCustomerPhone(string strIdSession, int intIdContract)
        //        {
        //            var objListItemVM = new List<ListItemVM>();
        //            var objAuditRequest = APPCODE.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
        //            var objCustomerPhoneRequest = new CustomerPhoneRequest()
        //            {
        //                audit = objAuditRequest,
        //                IdContract = intIdContract
        //            };

        //            try
        //            {
        //                var objCustomerPhoneResponse = Logging.ExecuteMethod(() =>
        //                {
        //                    return _oServiceFixed.GetCustomerPhone(objCustomerPhoneRequest);
        //                });

        //                objCustomerPhoneResponse.LstCustomerPhone.ToList().ForEach(item =>
        //                {
        //                    var objItemVM = new ListItemVM
        //                    {
        //                        Description = item.Descripcion ?? "",
        //                    };
        //                    objListItemVM.Add(objItemVM);
        //                });

        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, objCustomerPhoneRequest.audit.transaction, ex.Message);
        //                throw new Exception(objAuditRequest.transaction);
        //            }
        //            return Json(objListItemVM, JsonRequestBehavior.AllowGet);
        //        }

        //        public JsonResult GetListGeneric(string strIdSession, string strClave)
        //        {
        //            ParameterBusinnesResponse objResponse = null;
        //            PostTransacService.AuditRequest audit =
        //                APPCODE.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
        //            ParameterBusinnesRequest objRequest = new ParameterBusinnesRequest()
        //            {
        //                audit = audit,
        //                strIdList = strClave
        //            };

        //            try
        //            {
        //                objResponse = Logging.ExecuteMethod<ParameterBusinnesResponse>(() =>
        //                {
        //                    return _oServicePostpaid.GetPlanModel(objRequest);
        //                });
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, audit.transaction, ex.Message);
        //                throw new Exception(ex.Message);
        //            }

        //            return Json(new { data = objResponse.lstParameterBusinnes });
        //        }

        //        public JsonResult GetConfig(string strIdSession, string Key)
        //        {
        //            string llave;
        //            try
        //            {
        //                llave = KEY.AppSettings(Key);
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, Common.GetTransactionID(), ex.Message);
        //                throw new Exception(ex.Message);
        //            }
        //            return Json(new { data = llave });
        //        }

        //        //Ruta de Descarga del Excel 
        //        public FileResult DownloadExcel(string strPath, string strNewfileName)
        //        {
        //            return File(strPath, "application/vnd.ms-excel", strNewfileName);
        //        }

        //        public JsonResult GetTypification(string strIdSession, string strTransactionName, string strType = "")
        //        {
        //            TypificationResponse objTypificationResponse = null;
        //            CommonIFIService.AuditRequest audit =
        //                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
        //            TypificationRequest objTypificationRequest =
        //                new TypificationRequest();
        //            objTypificationRequest.audit = audit;
        //            objTypificationRequest.TRANSACTION_NAME = strTransactionName;

        //            try
        //            {
        //                objTypificationResponse = Logging.ExecuteMethod<TypificationResponse>(
        //                () =>
        //                {
        //                    return _oServiceCommon.GetTypification(objTypificationRequest);
        //                });
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, audit.transaction, ex.Message);
        //                throw new Exception(ex.Message);
        //            }

        //            if (!string.IsNullOrEmpty(strType))
        //            {
        //                objTypificationResponse.ListTypification = objTypificationResponse.ListTypification
        //                    .Where(y => y.TIPO == strType).ToList();
        //            }
        //            return Json(objTypificationResponse, JsonRequestBehavior.AllowGet);
        //        }

        public List<TypificationModel> GetTypificationHFC(string strIdSession, string strTransactionName)
        {
            var response = new List<TypificationModel>();
            TypificationResponse objTypificationResponse = null;
            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            TypificationRequest objTypificationRequest = new TypificationRequest();
            objTypificationRequest.audit = audit;
            objTypificationRequest.TRANSACTION_NAME = strTransactionName;
            var msg = string.Format("Controlador: {0},Metodo: {1}, WebConfig: {2}", "CallDetailController", "GetTypificationHFC", "SIACU_SP_OBTENER_TIPIFICACION");
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, msg);
            try
            {
                objTypificationResponse = Logging.ExecuteMethod<TypificationResponse>(() =>
                {
                    return _oServiceCommon.GetTypification(objTypificationRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, "Error GetTypificationHFC : " + ex.Message);
                throw new Exception(ex.Message);
            }

            var tempLst = objTypificationResponse.ListTypification;

            if (objTypificationResponse.ListTypification != null)
                Logging.Info("IdSession: " + strIdSession, " objTypificationResponse.ListTypification: Total Reg : ", objTypificationResponse.ListTypification.Count().ToString());
            else
                Logging.Info("IdSession: " + strIdSession, " objTypificationResponse.ListTypification: Total Reg : ", "0 o null");

            response = Mapper.Map<List<TypificationModel>>(tempLst);
            return response;
        }

        public JsonResult GetBusinessRules(string strIdSession, string strSubClase)
        {
            BusinessRulesResponse objBusinessRulesResponse = new BusinessRulesResponse();
            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            BusinessRulesRequest objBusinessRulesRequest = new BusinessRulesRequest();
            objBusinessRulesRequest.audit = audit;
            objBusinessRulesRequest.SUB_CLASE = strSubClase;

            Logging.Info("IdSession: " + strIdSession, "Inicio Método : GetBusinessRules", "strSubClase : " + strSubClase);
            try
            {
                objBusinessRulesResponse = Logging.ExecuteMethod<BusinessRulesResponse>(() =>
                {
                    return _oServiceCommon.GetBusinessRules(objBusinessRulesRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Logging.Info("IdSession: " + strIdSession, "Método : GetBusinessRules", "Fín");
            return Json(new { data = objBusinessRulesResponse });
        }

        //        public JsonResult GetRegions(string strIdSession)
        //        {
        //            RegionResponse objRegionResponse = null;
        //            CommonIFIService.AuditRequest audit =
        //                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
        //            RegionRequest objRegionRequest = new RegionRequest();
        //            objRegionRequest.audit = audit;

        //            try
        //            {
        //                objRegionResponse = Logging.ExecuteMethod<RegionResponse>(() =>
        //                {
        //                    return _oServiceCommon.GetRegions(objRegionRequest);
        //                });
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, audit.transaction, ex.Message);
        //                throw new Exception(ex.Message);
        //            }

        //            return Json(new { data = objRegionResponse });
        //        }

        //        public bool RestrictPlan(string sCodPlan, string sPlanesRestingidos, string estadoAcceso)
        //        {
        //            var blnRetorno = false;
        //            var blnPerfilAutorizado = false;
        //            var blnPlanRestringido = false;

        //            var strkeyRestringirDetalleLlamada =
        //                ConfigurationManager.AppSettings("gConstkeyRestringirConsultaDetalleLlamadaHFC");
        //            var strPermisos = estadoAcceso;
        //            if (strkeyRestringirDetalleLlamada.IndexOf(strPermisos, StringComparison.OrdinalIgnoreCase) + 1 > 0)
        //            {
        //                blnPerfilAutorizado = true;
        //            }

        //            if (sCodPlan.Length > 0)
        //            {
        //                if (sPlanesRestingidos.Length > 0)
        //                {
        //                    var sArrayPlanesRestringidos = sPlanesRestingidos.Split(',');
        //                    foreach (var sPlanRestringido in sArrayPlanesRestringidos)
        //                    {
        //                        if (sCodPlan.Trim() == sPlanRestringido.Trim())
        //                        {
        //                            blnPlanRestringido = true;
        //                        }
        //                    }
        //                }
        //            }

        //            if (blnPlanRestringido)
        //            {
        //                if (!blnPerfilAutorizado)
        //                {
        //                    blnRetorno = true;
        //                }
        //            }

        //            return blnRetorno;
        //        }

        //        public string GetTotalTR_Detail_Calls(List<Helpers.HFC.CallDetails.BilledCallsDetail> lista)
        //        {
        //            double Total = 0;
        //            double TotalMIN = 0;
        //            double TotalSMS = 0;
        //            double TotalMMS = 0;
        //            double TotalGPRS = 0;
        //            string[] Cantidad;
        //            double Consumo = 0;

        //            try
        //            {
        //                foreach (Helpers.HFC.CallDetails.BilledCallsDetail item in lista)
        //                {
        //                    Cantidad = item.Consumption.Split(char.Parse(":"));

        //                    if (Cantidad.Length.Equals(1))
        //                    {
        //                        Consumo = Functions.CheckDbl(Cantidad[0]);
        //                    }
        //                    else
        //                    {
        //                        if (Cantidad.Length.Equals(2))
        //                            Consumo = (Functions.CheckDbl(Cantidad[0]) * 60) + Functions.CheckDbl(Cantidad[1]);
        //                        else
        //                            Consumo = (Functions.CheckDbl(Cantidad[0]) * 3600) +
        //                                      (Functions.CheckDbl(Cantidad[1]) * 60) + Functions.CheckDbl(Cantidad[2]);
        //                    }
        //                    if (item.TypeCalls != null)
        //                    {
        //                        if (item.TypeCalls.ToUpper().IndexOf("LLAMADA") != -1 ||
        //                            item.TypeCalls.ToUpper().IndexOf("MOC") != -1)
        //                        {
        //                            TotalMIN += Consumo;
        //                        }
        //                        else
        //                        {
        //                            if (item.TypeCalls.ToUpper().IndexOf("SMS") != -1)
        //                            {
        //                                TotalSMS += Consumo;
        //                            }
        //                            else
        //                            {
        //                                if (item.TypeCalls.ToUpper().IndexOf("MMS") != -1)
        //                                {
        //                                    TotalMMS += Consumo;
        //                                }
        //                                else
        //                                {
        //                                    if (item.TypeCalls.ToUpper().IndexOf("GPRS") != -1)
        //                                    {

        //                                        TotalGPRS += Consumo;
        //                                    }
        //                                }
        //                            }
        //                        }

        //                        Total += Functions.CheckDbl(item.CargOriginal);
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw;
        //            }

        //            return Claro.Utils.CheckStr(Total) + ";" + Claro.Utils.CheckStr(TotalMIN) + ";" + Claro.Utils.CheckStr(TotalSMS) +
        //                   ";" + Claro.Utils.CheckStr(TotalMMS) + ";" + Claro.Utils.CheckStr(TotalGPRS);
        //        }

        //        public TemplateInteractionModel GetDatTemplateInteraction(string transaction, string telephone,
        //            string nroDoc,
        //            string email, string monthEmition, string yearEmition, string cacDac,
        //            string invoceNumber, string note, string name, string lastName,
        //            string representLegal, string strIdSession, string contratoId)
        //        {
        //            var oPlantCampDat = new TemplateInteractionModel();

        //            oPlantCampDat.NOMBRE_TRANSACCION = transaction;
        //            oPlantCampDat.X_CLARO_NUMBER = telephone;
        //            oPlantCampDat.X_DOCUMENT_NUMBER = nroDoc;
        //            oPlantCampDat.X_FIRST_NAME = name;
        //            oPlantCampDat.X_LAST_NAME = lastName;
        //            oPlantCampDat.X_NAME_LEGAL_REP = representLegal;

        //            if (!string.IsNullOrEmpty(email))
        //            {
        //                oPlantCampDat.X_FLAG_REGISTERED = ConstantsHFC.strUno;
        //            }
        //            else
        //            {
        //                oPlantCampDat.X_FLAG_REGISTERED = ConstantsHFC.strCero;
        //            }

        //            oPlantCampDat.X_EMAIL = email ?? string.Empty;
        //            oPlantCampDat.X_INTER_30 = note;
        //            var keyAppSettings = "ListaMeses";
        //            var fileName = "Data.xml";

        //            var listMonth = ListValueXmlMethod(keyAppSettings, "", fileName);

        //            var stMonth = monthEmition;
        //            for (int i = 0; i < listMonth.Count; i++)
        //            {
        //                var codeInt = Convert.ToInt(listMonth[i].Code);
        //                if (codeInt.Equals(Convert.ToInt(monthEmition)))
        //                {
        //                    stMonth = listMonth[i].Description;
        //                }
        //            }


        //            oPlantCampDat.X_INTER_29 = stMonth + "-" + yearEmition;
        //            oPlantCampDat.X_INTER_15 = cacDac;
        //            oPlantCampDat.X_INTER_16 = invoceNumber;
        //            oPlantCampDat.X_INTER_18 = contratoId;


        //            return oPlantCampDat;
        //        }

        //Insertar Interaccion CLFY
        public Dictionary<string, string> GetInsertInteractionCLFY(InteractionModel objInteractionModel, string strIdSession)
        {
            var serviceModelInteraction = Mapper.Map<CommonIFIService.Interaction>(objInteractionModel);
            CommonIFIService.InsertInteractHFCResponse objInteractHFCResponse = null;
            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "CommonServiceController", "GetInsertInteractionCLFY", "SIACU_POST_CLARIFY_SP_CREATE_INTERACT_HFC");
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, msg);
            CommonIFIService.InsertInteractHFCRequest objInteractHFCRequest = new CommonIFIService.InsertInteractHFCRequest()
            {
                audit = audit,
                Interaction = serviceModelInteraction
            };
            try
            {
                objInteractHFCResponse = Logging.ExecuteMethod<CommonIFIService.InsertInteractHFCResponse>(() =>
                {
                    return _oServiceCommon.GetInsertInteractHFC(objInteractHFCRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            var dictionaryResponse = new Dictionary<string, string>
                    {
                        {"rInteraccionId", objInteractHFCResponse.rInteraccionId},
                        {"rFlagInsercion", objInteractHFCResponse.rFlagInsercion},
                        {"rMsgText", objInteractHFCResponse.rMsgText},
                        {"rResult", objInteractHFCResponse.rResult.ToString()},
                    };

            Logging.Info(strIdSession, strIdSession, "strInteractionId Common: " + objInteractHFCResponse.rInteraccionId);
            Logging.Info(strIdSession, strIdSession, "strFlagInsert Common: " + objInteractHFCResponse.rFlagInsercion);
            Logging.Info(strIdSession, strIdSession, "strMsgText Common: " + objInteractHFCResponse.rMsgText);
            Logging.Info(strIdSession, strIdSession, "rResult Common: " + objInteractHFCResponse.rResult);

            return dictionaryResponse;
        }



        //Insertar Interaccion de Contingencia
        public Dictionary<string, string> GetInsertContingencyInteraction(InteractionModel objInteractionModel, string strIdSession)
        {
            var serviceModelInteraction = Mapper.Map<CommonIFIService.Iteraction>(objInteractionModel);
            InsertInteractResponseCommon objInsertInteractResponse = null;
            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "CommonServiceController", "GetInsertContingencyInteraction", "SIACU_POST_DB_SP_INSERTAR_INTERACT");
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, msg);
            InsertInteractRequestCommon objInsertInteractRequest = new InsertInteractRequestCommon()
            {
                audit = audit,
                item = serviceModelInteraction
            };


            try
            {
                objInsertInteractResponse =
                    Logging.ExecuteMethod<InsertInteractResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetInsertInteract(objInsertInteractRequest);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            var dictionaryResponse = new Dictionary<string, string>
                    {
                        {"rInteraccionId", objInsertInteractResponse.Interactionid},
                        {"rFlagInsercion", objInsertInteractResponse.FlagInsercion},
                        {"rMsgText", objInsertInteractResponse.MsgText},
                        {"rResult", objInsertInteractResponse.ProcesSucess.ToString()},
                    };

            return dictionaryResponse;
        }

        //Insertar Plantilla de Interaccion
        public Dictionary<string, object> InsertPlantInteraction(TemplateInteractionModel objInteractionTemplateModel, string rInteraccionId, string strNroTelephone, string strUserSession, string strUserAplication, string strPassUser, bool boolEjecutTransaction, string strIdSession)
        {
            string strTransaccion = Claro.Utils.CheckStr(objInteractionTemplateModel.NOMBRE_TRANSACCION);
            string contingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");
            Dictionary<string, object> dictionaryResponse;
            if (contingenciaClarify != Claro.SIACU.Constants.Yes)
            {
                var serviceModelInteraction = Mapper.Map<CommonIFIService.InsertTemplateInteraction>(objInteractionTemplateModel);
                InsertTemplateInteractionResponseCommon objInsertTemplInteractResponse = null;
                CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
                InsertTemplateInteractionRequestCommon objInsertTempInteractRequest = new InsertTemplateInteractionRequestCommon()
                {
                    audit = audit,
                    item = serviceModelInteraction,
                    IdInteraction = rInteraccionId
                };
                try
                {
                    objInsertTemplInteractResponse = Logging.ExecuteMethod<InsertTemplateInteractionResponseCommon>(() => { return _oServiceCommon.GetInsertInteractionTemplate(objInsertTempInteractRequest); });
                }
                catch (Exception ex)
                {
                    Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Exception(ex.Message);
                }
                dictionaryResponse = new Dictionary<string, object>
                        {
                            {"FlagInsercion", objInsertTemplInteractResponse.FlagInsercion},
                            {"MsgText", objInsertTemplInteractResponse.MsgText},
                            {"ProcesSucess", objInsertTemplInteractResponse.ProcesSucess},
                        };

                Logging.Info(strIdSession, strIdSession, "FlagInsercion Common: " + objInsertTemplInteractResponse.FlagInsercion);
                Logging.Info(strIdSession, strIdSession, "MsgText Common: " + objInsertTemplInteractResponse.MsgText);
                Logging.Info(strIdSession, strIdSession, "ProcesSucess Common: " + objInsertTemplInteractResponse.ProcesSucess);
            }
            else
            {
                //Contigencia
                var serviceModelInteraction = Mapper.Map<CommonIFIService.InsertTemplateInteraction>(objInteractionTemplateModel);
                InsTemplateInteractionResponseCommon objInsertTemplInteractResponse = null;
                CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
                InsTemplateInteractionRequestCommon objInsertTempInteractRequest = new InsTemplateInteractionRequestCommon()
                {
                    audit = audit,
                    item = serviceModelInteraction,
                    IdInteraction = rInteraccionId
                };
                try
                {
                    objInsertTemplInteractResponse = Logging.ExecuteMethod<InsTemplateInteractionResponseCommon>(() => { return _oServiceCommon.GetInsInteractionTemplate(objInsertTempInteractRequest); });
                }
                catch (Exception ex)
                {
                    Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Exception(ex.Message);
                }

                dictionaryResponse = new Dictionary<string, object>
                        {
                            {"FlagInsercion", objInsertTemplInteractResponse.FlagInsercion},
                            {"MsgText", objInsertTemplInteractResponse.MsgText},
                            {"ProcessSucess", objInsertTemplInteractResponse.ProcessSucess},
                        };

            }
            return dictionaryResponse;
        }

        //Obtner Datos Plantilla de Interaccion
        public TemplateInteractionModel GetInfoInteractionTemplate(string strIdSession, string strInteraccionId)
        {
            var model = new TemplateInteractionModel();

            DatTempInteractionResponse objDatTempInteractionResponse = null;
            CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            DatTempInteractionRequest objInteractHFCRequest =
                new DatTempInteractionRequest()
                {
                    audit = audit,
                    vInteraccionID = strInteraccionId
                };

            try
            {
                objDatTempInteractionResponse = Logging.ExecuteMethod<DatTempInteractionResponse>(
                    () =>
                    {
                        return _oServiceCommon.GetInfoInteractionTemplate(objInteractHFCRequest);
                    });
                var tempLst = objDatTempInteractionResponse.InteractionTemplate;
                model = Mapper.Map<TemplateInteractionModel>(tempLst);
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return model;
        }

        //        public string CreateArcchivePdf(string html, string Name)
        //        {
        //            var msg1 = string.Format("CONTROLLER: {0},METODO: {1}, RESULTADO: {2}", "CommonServiceController", "CreateArcchivePdf", "Llamando la ruta para el archivo ");
        //            Logging.Info("IdSession: " + "", "Transaccion: " + "", msg1);
        //            var strRutaGeneradorPdf = ConfigurationManager.AppSettings("constRutaArchivosPdf");
        //            var strRutArchivePdf = strRutaGeneradorPdf + Name + SIACU.Transac.Service.Constants.PresentationLayer.gstrExtensionPDF;
        //            return GeneratePdf(html, strRutArchivePdf);
        //        }

        //        public string GeneratePdf(string html, string strRutArchivePdf)
        //        {
        //            var msg1 = string.Format("CONTROLLER: {0},METODO: {1}, RESULTADO: {2}", "CommonServiceController", "GeneratePdf", "Generando el Archivo");
        //            Logging.Info("IdSession: " + "", "Transaccion: " + "", msg1);
        //            var document = new Document();
        //            PdfWriter.GetInstance(document, new FileStream(strRutArchivePdf, FileMode.Create));
        //            document.Open();
        //            var hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
        //            hw.Parse(new StringReader(html));
        //            document.Close();

        //            var msg2 = string.Format("CONTROLLER: {0},METODO: {1}, RESULTADO: {2}", "CommonServiceController", "GeneratePdf", "Guardando el Archivo");
        //            Logging.Info("IdSession: " + "", "Transaccion: " + "", msg2);

        //            return strRutArchivePdf;
        //        }

        //        public string CreateHeaderEmail(string pstrTitle, string pstrCAC, string pstrDate,
        //            string pstrTitular, string pstrCaseInteraction, string pstrRepresent,
        //            string NroClaro, string pstrTypeDocument, string pstrNroDoc, string pstrTelephone)
        //        {

        //            string strReturn = string.Empty;

        //            strReturn = "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
        //            strReturn += "<tr><td width='100%' class='Estilo1'>Estimado Cliente:</td></tr>";
        //            strReturn += "<tr><td height='10'></td>";
        //            if (pstrTitle == "Servicio de Variación de Débito / Crédito Manual Corporativo")
        //            {
        //                strReturn +=
        //                    "<tr><td class='Estilo1'>Por la presente queremos informarle que se realizo un ajuste al saldo de su linea corporativa</td></tr>";
        //            }
        //            else
        //            {
        //                strReturn += "<tr><td class='Estilo1'>Por la presente queremos informarle que su solicitud de " +
        //                             pstrTitle + " fue atendida.</td></tr>";
        //            }

        //            strReturn += "</table>";
        //            return strReturn;
        //        }

        //        public bool GetSendEmail(SendEmailModel model)
        //        {
        //            SendEmailResponseCommon objGetSendEmailResponse = new SendEmailResponseCommon();
        //            CommonIFIService.AuditRequest AuditRequest = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(model.strIdSession);
        //            SendEmailRequestCommon objGetSendEmailRequest;
        //            objGetSendEmailRequest = new SendEmailRequestCommon()
        //            {
        //                audit = AuditRequest,
        //                strSender = model.strTo,
        //                strTo = model.strSender,
        //                strMessage = model.strMessage,
        //                strAttached = model.strAttached,
        //                strSubject = model.strSubject
        //            };

        //            try
        //            {
        //                objGetSendEmailResponse = Logging.ExecuteMethod<SendEmailResponseCommon>(() =>
        //                {
        //                    return _oServiceCommon.GetSendEmail(objGetSendEmailRequest);
        //                });
        //                var result = objGetSendEmailResponse.ExtensionData;
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(model.strIdSession, AuditRequest.transaction, ex.Message);
        //                throw new Exception(ex.Message);
        //            }
        //            return true;
        //        }

        //        public bool SaveAudit(string strIdSession, string strCuentaUsuario, string strIpCliente, string strIpServidor,
        //            string strMonto, string strNombreCliente, string strNombreServidor,
        //            string strServicio, string strTelefono, string strTexto, string strTransaccion)
        //        {
        //            SaveAuditResponseCommon objRegAuditResponseCommon = null;
        //            CommonIFIService.AuditRequest audit =
        //                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
        //            SaveAuditRequestCommon objRegAuditRequestCommon =
        //                new SaveAuditRequestCommon()
        //                {
        //                    audit = audit,
        //                    vCuentaUsuario = strCuentaUsuario,
        //                    vIpCliente = strIpCliente,
        //                    vIpServidor = strIpServidor,
        //                    vMonto = strMonto,
        //                    vNombreCliente = strNombreCliente,
        //                    vNombreServidor = strNombreServidor,
        //                    vServicio = strServicio,
        //                    vTelefono = strTelefono,
        //                    vTexto = strTexto,
        //                    vTransaccion = strTransaccion
        //                };

        //            try
        //            {
        //                objRegAuditResponseCommon = Logging.ExecuteMethod<SaveAuditResponseCommon>(
        //                    () =>
        //                    {
        //                        return _oServiceCommon.SaveAudit(objRegAuditRequestCommon);
        //                    });
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, audit.transaction, ex.Message);
        //                throw new Exception(ex.Message);
        //            }

        //            var result = objRegAuditResponseCommon.respuesta;

        //            return result;
        //        }

        public bool SaveAuditM(string strTransaction, string strService, string strText, string strTelephone, string strNameCustomer, string strIdSession, string strNameServidor, string strIpServidor, string strIpCustomer = "", string strCuentUser = "", string strMontoInput = "0")
        {
            var strMonto = strMontoInput == "0" ? Claro.Constants.NumberZeroString : strMontoInput;

            SaveAuditMResponseCommon objRegAuditResponseMCommon = null;
            CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            SaveAuditMRequestCommon objRegAuditRequestMCommon =
                new SaveAuditMRequestCommon()
                {
                    audit = audit,
                    vTransaccion = strTransaction,
                    vServicio = strService,
                    vIpCliente = strIpCustomer,
                    vNombreCliente = strNameCustomer,
                    vIpServidor = strIpServidor,
                    vNombreServidor = strNameServidor,
                    vCuentaUsuario = strCuentUser,
                    vTelefono = strTelephone,
                    vMonto = strMonto,
                    vTexto = strText
                };

            try
            {
                objRegAuditResponseMCommon = Logging.ExecuteMethod<SaveAuditMResponseCommon>(
                    () =>
                    {
                        return _oServiceCommon.SaveAuditM(objRegAuditRequestMCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            var result = objRegAuditResponseMCommon.respuesta;

            return result;
        }

        public string GetAccessOfPage(string strIdSession)
        {
            var model = new ConsultSecurityModel();
            model.ListConsultSecurity = new List<SecurityModel>();
            var strKey = string.Empty;

            int strCodeApp = Convert.ToInt(ConfigurationManager.AppSettings("CodAplicacion"));

            PagOptionXuserResponse objPagOptionXuserResponse = null;
            CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            PagOptionXuserRequest objPagOptionXuserRequest = new PagOptionXuserRequest()
            {
                audit = audit,
                IntUser = Convert.ToInt(strIdSession),
                IntAplicationCode = strCodeApp
            };

            try
            {
                objPagOptionXuserResponse = Logging.ExecuteMethod<PagOptionXuserResponse>(() =>
                {
                    return _oServiceCommon.GetPagOptionXuser(objPagOptionXuserRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            var tempLst = objPagOptionXuserResponse.ListConsultSecurity;
            model.CodeErr = objPagOptionXuserResponse.CodeErr;
            model.ErrMessage = objPagOptionXuserResponse.ErrMessage;
            model.ListConsultSecurity = Mapper.Map<List<SecurityModel>>(tempLst);

            model.ListConsultSecurity.ForEach(x =>
            {
                strKey = strKey + x.Opcicabrev + ",";
            });

            return strKey.ToUpper();
        }

        public bool LocalAuthorization(string strIdSession, string pUsuario, string strKey)
        {
            var resultado = false;
            var usuario = string.Empty;
            //AUDITORIA

            var codApp = KEY.AppSettings("CodAplicacion_SIACPO");
            var ipApp = KEY.AppSettings("strWebIpCod_SIACPO");
            var nomApp = KEY.AppSettings("NombreAplicacion_SIACPO");

            CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            var objRequest = new VerifyUserRequest
            {
                audit = audit,
                AppName = nomApp,
                TransactionId = audit.transaction,
                AppCode = codApp,
                SessionId = strIdSession,
                AppId = ipApp,
                Username = pUsuario.Trim()
            };

            try
            {
                var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetVerifyUser(objRequest));
                if (objResponse.LstConsultSecurities.Count > 0)
                {
                    var item = objResponse.LstConsultSecurities[0];
                    usuario = item.Usuaccod;
                }
                else
                {
                    usuario = string.Empty;
                }

                //var strPermisos = "PERMISOS"; //Auditoria ListarAccesosPagina(usuario)
                var strPermisos = GetAccessOfPage(strIdSession);
                if (strPermisos.IndexOf(KEY.AppSettings(strKey), StringComparison.OrdinalIgnoreCase) != -1)
                {
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
            }

            return resultado;
        }

        public bool IsAuthenticated_LDAP(string strIdSession, string transaction, string vUsuario, string vClave)
        {
            var resultado = false;
            var strDominio = KEY.AppSettings("DominioLDAP");
            var entry = new DirectoryEntry(strDominio, vUsuario, vClave);
            try
            {
                var obj = entry.NativeObject;
                var search = new DirectorySearcher(entry) { Filter = "(SAMAccountName=" + vUsuario + ")" };
                search.PropertiesToLoad.Add("cn");
                var result = search.FindOne();

                if (result != null)
                {
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, transaction, ex.Message);
            }

            return resultado;
        }

        public bool Authorize_TX(string strIdSession, string transaction, string pUsuario, string pClave, string strKey)
        {
            var resultado = IsAuthenticated_LDAP(strIdSession, transaction, pUsuario, pClave);
            var response = false;
            try
            {
                if (resultado)
                {
                    var usuario = string.Empty;

                    var codApp = KEY.AppSettings("CodAplicacion_SIACPO");
                    var ipApp = KEY.AppSettings("strWebIpCod_SIACPO");
                    var nomApp = KEY.AppSettings("NombreAplicacion_SIACPO");

                    CommonIFIService.AuditRequest audit =
                        APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
                    var objRequest = new VerifyUserRequest
                    {
                        audit = audit,
                        AppName = nomApp,
                        TransactionId = audit.transaction,
                        AppCode = codApp,
                        SessionId = strIdSession,
                        AppId = ipApp,
                        Username = pUsuario.Trim()
                    };

                    var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetVerifyUser(objRequest));

                    if (objResponse.LstConsultSecurities.Count > 0)
                    {
                        var item = objResponse.LstConsultSecurities[0];
                        usuario = item.Usuaccod;

                        //var strPermisos = "PERMISOS"; //Auditoria ListarAccesosPagina(usuario)
                        var strPermisos = GetAccessOfPage(strIdSession);
                        if (strPermisos.IndexOf(KEY.AppSettings(strKey), StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            response = true;
                            Session["UsuarioValidador"] = pUsuario.Trim();
                        }
                        else
                        {
                            Session["UsuarioValidador"] = string.Empty;
                        }
                    }
                    else
                    {
                        usuario = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, transaction, ex.Message);
                throw;
            }

            return response;
        }

        public string ConsultProfile_DCM(string strIdSession, string transaction, string pUsuario, string pClave)
        {
            var resultado = IsAuthenticated_LDAP(strIdSession, transaction, pUsuario, pClave);
            var strCodPerfil = string.Empty;

            var codApp = KEY.AppSettings("CodAplicacion_SIACPO");
            var ipApp = KEY.AppSettings("strWebIpCod_SIACPO");
            var nomApp = KEY.AppSettings("NombreAplicacion_SIACPO");

            if (resultado)
            {
                resultado = false;
                var currentWindowsIdentity = (System.Security.Principal.WindowsIdentity)System.Web.HttpContext.Current.User.Identity;
                var impersonationContext = currentWindowsIdentity.Impersonate();
                impersonationContext.Undo();

                var audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
                var objRequest = new VerifyUserRequest
                {
                    audit = audit,
                    AppName = nomApp,
                    TransactionId = audit.transaction,
                    AppCode = codApp,
                    SessionId = strIdSession,
                    AppId = ipApp,
                    Username = pUsuario.Trim()
                };

                var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetVerifyUser(objRequest));
                if (objResponse.LstConsultSecurities.Count > 0)
                {
                    var objLst = objResponse.LstConsultSecurities;
                    foreach (var obj in objLst)
                    {
                        strCodPerfil = strCodPerfil + obj.Perfccod + ",";
                    }
                }
                else
                {
                    strCodPerfil = "";
                }

                strCodPerfil = strCodPerfil.Substring(0, strCodPerfil.Length - 1);

                var sPerfilAutorizado = strCodPerfil.Length > 0 ? strCodPerfil : "0";

                return sPerfilAutorizado;
            }

            return "0";
        }

        public string ConsultProfile(string strIdSession, string transaction, string pUsuario, string pClave,
            string pCadenaPerfiles)
        {
            var resultado = IsAuthenticated_LDAP(strIdSession, transaction, pUsuario, pClave);
            var strCodPerfil = string.Empty;
            var arrPerfiles = pCadenaPerfiles.Split('|');

            var codApp = KEY.AppSettings("CodAplicacion_SIACPO");
            var ipApp = KEY.AppSettings("strWebIpCod_SIACPO");
            var nomApp = KEY.AppSettings("NombreAplicacion_SIACPO");

            var sPerfilAutorizado = string.Empty;

            if (resultado)
            {
                resultado = false;
                var currentWindowsIdentity = (System.Security.Principal.WindowsIdentity)System.Web.HttpContext.Current.User.Identity;
                var impersonationContext = currentWindowsIdentity.Impersonate();
                impersonationContext.Undo();

                var audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
                var objRequest = new VerifyUserRequest
                {
                    audit = audit,
                    AppName = nomApp,
                    TransactionId = audit.transaction,
                    AppCode = codApp,
                    SessionId = strIdSession,
                    AppId = ipApp,
                    Username = pUsuario.Trim()
                };

                var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetVerifyUser(objRequest));
                if (objResponse.LstConsultSecurities.Count > 0)
                {
                    var objLst = objResponse.LstConsultSecurities;
                    foreach (var obj in objLst)
                    {
                        strCodPerfil = strCodPerfil + obj.Perfccod + ",";
                    }
                }
                else
                {
                    strCodPerfil = "";
                }

                strCodPerfil = strCodPerfil.Substring(0, strCodPerfil.Length - 1);

                if (strCodPerfil.Length > 0)
                {
                    var sArrayPerfiles = strCodPerfil.Split(',');
                    foreach (var itemOne in arrPerfiles)
                    {
                        foreach (var itemTwo in sArrayPerfiles)
                        {
                            if (itemTwo == itemOne)
                            {
                                sPerfilAutorizado = itemOne;
                            }
                        }

                        if (!string.IsNullOrEmpty(sPerfilAutorizado))
                        {
                            break;
                        }
                    }

                    if (string.IsNullOrEmpty(sPerfilAutorizado))
                    {
                        sPerfilAutorizado = "0";
                    }
                }
                else
                {
                    sPerfilAutorizado = "0";
                }

                return sPerfilAutorizado;
            }

            return "0";
        }

        //        public void RecoverAccessPage(string strIdSession, string transaction)
        //        {
        //            try
        //            {
        //                //Servicio Auditoria
        //                //Seteo de permisos a session.
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, transaction, ex.Message);
        //                throw;
        //            }
        //        }



        //        private ArrayList ObtieneFranjasHorarias(TimeZoneVM objTimeZoneVM, string strIdSession = "")
        //        {
        //            ArrayList Items = new ArrayList();
        //            string idTran, ipApp, nomAp, usrAp;
        //            try
        //            {
        //                idTran = Common.GetTransactionID();
        //                ipApp = Common.GetApplicationIp();
        //                nomAp = KEY.AppSettings("NombreAplicacion");
        //                usrAp = Common.CurrentUser;

        //                DateTime dInitialDate = Convert.ToDate(objTimeZoneVM.vCommitmentDate);

        //                int fID = Convert.ToInt(Claro.Utils.GetValueFromConfigFile("strDiasConsultaCapacidad", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")
        //                    ));
        //                DateTime[] dDate = new DateTime[fID];

        //                dDate[0] = dInitialDate;

        //                for (int i = 1; i <= fID; i++)
        //                {
        //                    dInitialDate = dInitialDate.AddDays(1);
        //                    dDate[i] = dInitialDate;
        //                }

        //                Boolean vExistSesion = false;
        //                string strUbicacion = Claro.Utils.GetValueFromConfigFile("strCodigoUbicacion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")
        //                    );
        //                string[] vUbicaciones = { strUbicacion };
        //                Boolean v1, v2, v3, v4, v5, v6, v7, v8;

        //                v1 = Boolean.Parse(Claro.Utils.GetValueFromConfigFile("strCalcDuracion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
        //                v2 = Boolean.Parse(Claro.Utils.GetValueFromConfigFile("strCalcDuracionEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
        //                v3 = Boolean.Parse(
        //                    Claro.Utils.GetValueFromConfigFile("strCalcTiempoViaje", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
        //                v4 = Boolean.Parse(Claro.Utils.GetValueFromConfigFile("strCalcTiempoViajeEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
        //                v5 = Boolean.Parse(Claro.Utils.GetValueFromConfigFile("strCalcHabTrabajo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
        //                v6 = Boolean.Parse(Claro.Utils.GetValueFromConfigFile("strCalcHabTrabajoEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
        //                v7 = Boolean.Parse(Claro.Utils.GetValueFromConfigFile("strObtenerZonaUbi", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")
        //                    ));
        //                v8 = Boolean.Parse(Claro.Utils.GetValueFromConfigFile("strObtenerZonaUbiEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));


        //                String vHabTra = String.Empty;
        //                vHabTra = Claro.Utils.GetValueFromConfigFile("strCodigoHabilidad", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));

        //                string[] vEspacioTiempo = { string.Empty };
        //                string[] HabilidadTrabajo = { vHabTra };

        //                //    Dim vespacioTiempo As String() = {String.Empty}
        //                //Dim vhabilidtrab As String() = {vstrHabTra}


        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }



        //            return Items;
        //        }

        //        public static string ValidatePlanTFI(string strType, string flagTFI)
        //        {
        //            Logging.Info("Session: en ValidatePlanTFI", "Transaction: Entra a ValidatePlanTFI", "Message" + strType + "/" + flagTFI);

        //            string strTFIPostpaid = "";
        //            if (flagTFI == "SI")
        //            {
        //                strTFIPostpaid = KEY.AppSettings("gConstProductoTFIPOSTPAGO");
        //                strType = strTFIPostpaid + strType;
        //            }

        //            Logging.Info("Session: en ValidatePlanTFI", "Transaction: Sale de ValidatePlanTFI", "Message" + strTFIPostpaid + "/" + strType);

        //            return strType;
        //        }

        public JsonResult ValidateUser(string strIdSession, string transaction, string txtUsuario, string txtPass,
            string hidPagina, string hidMonto, string hidUnidad, string hidModalidad, string hidDescripcionProceso,
            string hidTipoA, string hidCo, string hidMotivoA, string hidTelefono, string hidAccion, string hidVeces,
            string hidOpcion, string hidPagDCM, string hidConcepto, string transaccion, string tecnologia)
        {
            var dictionaryValidateUser = new Dictionary<string, object>();
            var blnCorrecto = false;
            var blnRetorno = false;

            var sUsuario = txtUsuario.Trim();
            var sContrasena = txtPass.Trim();
            var sPagina = hidPagina.Trim();
            var sMonto = hidMonto.Trim();
            var sUnidad = hidUnidad.Trim();
            var strmodalidad = hidModalidad.Trim();
            var sDescripcionProceso = hidDescripcionProceso.Trim();

            dictionaryValidateUser.Add("hidTelefono", hidTelefono);

            var resultado = string.Empty;
            var sCodPerfil = string.Empty;
            var sCadenaPerfilesAutorizar =
                Claro.Utils.GetValueFromConfigFile("JerarquiaPerfiles", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
            var vTipoTelefono = string.Empty;


            dictionaryValidateUser.Add("hidUserValidator", txtUsuario);
            dictionaryValidateUser.Add("gConstKeyStrResultValLogOK",
                ConfigurationManager.AppSettings("gConstKeyStrResultValLogOK"));
            dictionaryValidateUser.Add("gConstKeyStrResultValLogCANCEL",
                ConfigurationManager.AppSettings("gConstKeyStrResultValLogCANCEL"));
            dictionaryValidateUser.Add("gConstkeyTransaccionConsultaBiometria",
                ConfigurationManager.AppSettings("gConstkeyTransaccionConsultaBiometria"));

            var hidUserValidator = txtUsuario; // Enviar como resultado.

            //INICIALIZACION DE AUDITORIA :(

            var dblRpta = 0.00;
            var dblCodUsuario = 0.00;
            var strIP = string.Empty;
            var dblCodOpcion = 0.00;
            var strDescripcion = string.Empty;
            var dblCodEvento = 0.00;
            var dblCodPerfil = 0.00;
            var strLogin = string.Empty;
            var intCodEstado = 1;
            var intResultado = 0;

            var detalle = new string[8, 3];
            var strFecha = DateTime.UtcNow.ToShortDateString();
            var strSeparador = string.Empty;
            var arrPerfil =
                "ESPERANDO A SESSION"; //Session["codPerfil"].ToString().Split(System.Convert.ToChar(strSeparador));

            if (!string.IsNullOrEmpty(hidTipoA) && !string.IsNullOrEmpty(hidCo))
            {
                dblCodUsuario = System.Convert.ToDouble(Session["codUsuario"]);
                strIP = Request.ServerVariables["REMOTE_ADDR"];
                dblCodEvento =
                    System.Convert.ToDouble(ConfigurationManager.AppSettings("gConstEvtFallasValidacionUyP"));
                dblCodOpcion = System.Convert.ToDouble(hidCo);
                strDescripcion =
                    "DataThroughWebServicesServiceReference.DataThroughWebServicesServiceClient de Fallas de Validacion de User y Password";

                dblCodPerfil = arrPerfil.Length > 0 ? System.Convert.ToInt64(arrPerfil[0]) : 0;

                var userValidator = string.IsNullOrEmpty(hidUserValidator) ? "CURRENTUSER" : hidUserValidator;

                strLogin = "CurrentUser"; //CurrentUser()

                detalle[1, 1] = "aUsuario";
                detalle[1, 2] = strLogin;
                detalle[1, 3] = "Usuario Login";

                detalle[2, 1] = "bNombrePC";
                detalle[2, 2] = strIP;
                detalle[2, 3] = "IP de PC";

                detalle[3, 1] = "cFecha";
                detalle[3, 2] = strFecha;
                detalle[3, 3] = "Fecha";

                detalle[4, 1] = "dTipoAjuste";
                detalle[4, 2] = hidTipoA;
                detalle[4, 3] = "Tipo de Ajuste";

                detalle[5, 1] = "eMotivoAjuste";
                detalle[5, 2] = hidMotivoA;
                detalle[5, 3] = "Motivo de ajuste";

                detalle[6, 1] = "fImporte";
                detalle[6, 2] = hidMonto;
                detalle[6, 3] = "Importe";

                detalle[7, 1] = "gTelefono";
                detalle[7, 2] = hidTelefono;
                detalle[7, 3] = "Telefono Ajustado";

                detalle[8, 1] = "hUsuarioAprobador";
                detalle[8, 2] = userValidator;
                detalle[8, 3] = "Usuario que Intenta Autorizar";

                try
                {
                    if (Convert.ToInt(hidVeces) > 1)
                    {
                        dblRpta = 0;
                        RegisterAudit(strDescripcion, dblCodEvento, detalle, strIdSession);

                    }
                }
                catch (Exception e)
                {
                    Logging.Info(strIdSession, transaction, e.Message);
                    dictionaryValidateUser.Add("hidAccion", "E");
                }
            }
            if (tecnologia.Equals("Prepaid"))
            {
                if (sPagina == "4" || sPagina == "6")
                {
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                }
                else if (sPagina == "5")
                {
                    blnCorrecto = Authorize_TX_Search(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                }

                if (blnCorrecto)
                {
                    hidAccion = "G";
                    dictionaryValidateUser.Add("hidAccion", hidAccion);
                    Session["UsuarioValidador"] = txtUsuario.Trim();
                    dictionaryValidateUser.Add("Reseteo", hidPagina == "6");
                }
                else if (sPagina == "4" || sPagina == "6" || sPagina == "5")
                {
                    dictionaryValidateUser.Add("hidAccion", "F");
                }

                if (sPagina == "10")
                {
                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        hidAccion = ConfigurationManager.AppSettings("strCCTxtValSG");
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                    else
                    {
                        blnRetorno = false;
                        hidAccion = ConfigurationManager.AppSettings("strCCTxtValF");
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }

                if (sPagina == "16")
                {
                    var strRes = string.Empty;
                    var strPerfiles = string.Empty;
                    var item = string.Empty;
                    var blResultado = false;

                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        hidAccion = "V";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                    else
                    {
                        blnRetorno = false;
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }

                if (sPagina == "17")
                {
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);

                    if (blnCorrecto)
                    {
                        hidAccion = "V";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                    else
                    {
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }
            }
            else if (tecnologia.Equals("Postpaid"))
            {
                if (sPagina == "1" || sPagina == "6")
                {
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                }

                if (blnCorrecto)
                {
                    hidAccion = "G";
                    dictionaryValidateUser.Add("hidAccion", hidAccion);
                    Session["UsuarioValidador"] = txtUsuario.Trim();
                    if (hidPagina == "6")
                    {
                        dictionaryValidateUser.Add("Reseteo", true);
                    }
                    else
                    {
                        dictionaryValidateUser.Add("Reseteo", false);
                    }
                }
                else if (sPagina == "1" || sPagina == "6")
                {
                    dictionaryValidateUser.Add("hidAccion", "F");
                }

                if (sPagina == "2")
                {
                    blnCorrecto = false;
                    strmodalidad = "";
                    if (sMonto == "")
                    {
                        sMonto = "0";
                    }

                    if (hidPagDCM == "DEBITO_CREDITO_MANUAL")
                    {
                        sCodPerfil = ConsultProfile_DCM(strIdSession, transaction, sUsuario, sContrasena);
                        resultado = EvaluateAmount_DCM(strIdSession, transaction, sCodPerfil, sMonto, sUnidad,
                            strmodalidad, hidConcepto);
                    }
                    else
                    {
                        sCodPerfil = ConsultProfile(strIdSession, transaction, sUsuario, sContrasena,
                            sCadenaPerfilesAutorizar);
                        resultado = EvaluateAmount(strIdSession, transaction, sCodPerfil, sMonto, sUnidad, strmodalidad,
                            hidConcepto);

                    }

                    if (resultado == "1")
                    {
                        blnCorrecto = true;
                        Session["UsuarioValidador"] = txtUsuario.Trim();
                    }

                    dictionaryValidateUser.Add("hidAccion", blnCorrecto ? "G" : "F");
                }

                if (sPagina == "3")
                {
                    var strConcepto = strmodalidad;
                    dictionaryValidateUser.Add("hidConcepto",
                        ListValueXmlMethod_Auth(strIdSession, transaccion, "IFIData.xml",
                            "ListaValidaMontoXTransaccion", "1") + strConcepto);

                    blnRetorno = true;
                    if (sMonto == "")
                    {
                        sMonto = "0";
                    }
                    sCodPerfil = ConsultProfile(strIdSession, transaction, sUsuario, sContrasena,
                        sCadenaPerfilesAutorizar);
                    if (sCodPerfil == "0")
                    {
                        dictionaryValidateUser.Add("hidAccion", "L");
                    }
                    else
                    {
                        resultado = EvaluateAmount(strIdSession, transaction, sCodPerfil, sMonto, sUnidad, strmodalidad,
                            hidConcepto);

                        if (resultado == "1")
                        {
                            blnRetorno = true;
                        }
                        else
                        {
                            blnRetorno = false;
                        }

                        if (blnRetorno)
                        {
                            dictionaryValidateUser.Add("hidAccion", "G");
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                        }
                        else
                        {
                            dictionaryValidateUser.Add("hidAccion", "M");
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                        }
                    }
                }

                if (sPagina == "4")
                {
                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        dictionaryValidateUser.Add("hidAccion", "D");
                    }
                    else
                    {
                        blnRetorno = false;
                        dictionaryValidateUser.Add("hidAccion", "F");
                    }
                }

                if (sPagina == "5")
                {
                    blnRetorno = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                    if (blnRetorno)
                    {
                        dictionaryValidateUser.Add("hidAccion", "ADS");
                        if (hidOpcion == "gConstValidaFidelizaApadece")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "FA");
                        }
                        else if (hidOpcion == "gConstFechaProgMigracion")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PF");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PF";
                        }

                        else if (hidOpcion == "gConstValidaProgramarFecha")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PF");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PF";
                        }

                        else if (hidOpcion == "gConstFidelizaMigracion")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PCHECK");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PCHECK";
                        }

                        else if (hidOpcion == "gConstActivRetencion")
                        {
                            hidUserValidator = txtUsuario.Trim() + "_" + "ACTRETCHK";
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "ACTRETCHK");
                        }

                        else if (hidOpcion == "gConstModFechaProgMigraPlan")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PF");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PF";
                        }

                        else if (hidOpcion == "gConstFidelizaPenalidadMigraPlan")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PCHECK");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PCHECK";
                        }

                        else if (hidOpcion == "gConsAccesoFidelizaApalece")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "FIDEAPALECECHK");
                            hidUserValidator = txtUsuario.Trim() + "_" + "FIDEAPALECECHK";
                        }

                        else if (hidOpcion == "gConstModFechaProgCamTipClient")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PF");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PF";
                        }

                        else if (hidOpcion == "gConstFidelizaPenalidadCamTipClient")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PCHECK");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PCHECK";
                        }

                        else if (hidOpcion == "gConstModFechaProgCamCicloFact")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" + "PF";
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PF");
                        }
                        else if (hidOpcion == "gConstSinTopeConsAutorizacion")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "SinTopeConsAut");
                            hidUserValidator = txtUsuario.Trim() + "_" + "SinTopeConsAut";
                        }
                    }
                    else
                    {
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }

                if (sPagina == "99")
                {
                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        hidAccion = "H";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                    else
                    {
                        blnRetorno = false;
                        hidAccion = "TN";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }

                if (sPagina == "10")
                {
                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        hidAccion = ConfigurationManager.AppSettings("strCCTxtValSG");
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                    else
                    {
                        blnRetorno = false;
                        hidAccion = ConfigurationManager.AppSettings("strCCTxtValF");
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }

                if (sPagina == "16")
                {
                    var strRes = string.Empty;
                    var strPerfiles = string.Empty;
                    var item = string.Empty;
                    var blResultado = false;

                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        hidAccion = Claro.Constants.NumberOneString;
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = Claro.SIACU.Constants.ConstAutorizado;
                    }
                    else
                    {
                        blnRetorno = false;
                        hidAccion = Claro.SIACU.Constants.ConstTN;
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = Claro.SIACU.Constants.ConstNoAutorizado;
                    }
                }

                if (sPagina == "17")
                {
                    var strRes = string.Empty; //GetTypificationByTransaction
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);

                    if (blnCorrecto)
                    {
                        hidAccion = "AS4G";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = Claro.SIACU.Constants.ConstAutorizado;
                    }
                    else
                    {
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = Claro.SIACU.Constants.ConstNoAutorizado;
                    }
                }

                if (sPagina == "18")
                {
                    var strRes = string.Empty;
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);

                    if (blnCorrecto)
                    {
                        hidAccion = "DS4G";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = Claro.SIACU.Constants.ConstAutorizado;
                    }
                    else
                    {
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = Claro.SIACU.Constants.ConstAutorizado;
                    }
                }

                if (sPagina == "19")
                {
                    var strRes = string.Empty;
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);

                    if (blnCorrecto)
                    {
                        hidAccion = ConfigurationManager.AppSettings("gConstkeyTransaccionConsultaBiometria");
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = Claro.SIACU.Constants.ConstAutorizado;
                    }
                    else
                    {
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = Claro.SIACU.Constants.ConstNoAutorizado;
                    }
                }

                var strPaginaVal = ConfigurationManager.AppSettings("gConstKeyStrEnvioLog");
                var strEstOkVal = ConfigurationManager.AppSettings("gConstKeyStrResultValLogOK");
                var strEstCancelVal = ConfigurationManager.AppSettings("gConstKeyStrResultValLogCANCEL");
                if (sPagina == strPaginaVal)
                {
                    var objEstado = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                    hidAccion = objEstado ? strEstOkVal : strEstCancelVal;
                    dictionaryValidateUser.Add("hidAccion", hidAccion);
                }

            }
            else if (tecnologia.Equals("Fixed"))
            {
                if (sPagina == "1" || sPagina == "6")
                {
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                }

                if (blnCorrecto)
                {
                    hidAccion = "G";
                    dictionaryValidateUser.Add("hidAccion", hidAccion);
                    Session["UsuarioValidador"] = txtUsuario.Trim();
                    if (hidPagina == "6")
                    {
                        dictionaryValidateUser.Add("Reseteo", true);
                    }
                    else
                    {
                        dictionaryValidateUser.Add("Reseteo", false);
                    }
                }
                else if (sPagina == "1" || sPagina == "6")
                {
                    dictionaryValidateUser.Add("hidAccion", "F");
                }

                if (sPagina == "2")
                {
                    blnCorrecto = false;
                    strmodalidad = "";
                    if (sMonto == "")
                    {
                        sMonto = "0";
                    }

                    if (hidPagDCM == Claro.Utils.GetValueFromConfigFile("strConstDebCrdManual",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")))
                    {
                        sCodPerfil = ConsultProfile_DCM(strIdSession, transaction, sUsuario, sContrasena);
                        resultado = EvaluateAmount_DCM(strIdSession, transaction, sCodPerfil, sMonto, sUnidad,
                            strmodalidad, hidConcepto);
                    }
                    else
                    {
                        sCodPerfil = ConsultProfile(strIdSession, transaction, sUsuario, sContrasena,
                            sCadenaPerfilesAutorizar);
                        resultado = EvaluateAmount(strIdSession, transaction, sCodPerfil, sMonto, sUnidad, strmodalidad,
                            hidConcepto);

                    }

                    if (resultado == "1")
                    {
                        blnCorrecto = true;
                        Session["UsuarioValidador"] = txtUsuario.Trim();
                    }

                    dictionaryValidateUser.Add("hidAccion", blnCorrecto ? "G" : "F");
                }

                if (sPagina == "3")
                {
                    var strConcepto = strmodalidad;
                    dictionaryValidateUser.Add("hidConcepto",
                        ListValueXmlMethod_Auth(strIdSession, transaccion, "IFIData.xml",
                            "ListaValidaMontoXTransaccion", "1") + strConcepto);

                    blnRetorno = true;
                    if (sMonto == "")
                    {
                        sMonto = "0";
                    }
                    sCodPerfil = ConsultProfile(strIdSession, transaction, sUsuario, sContrasena,
                        sCadenaPerfilesAutorizar);
                    if (sCodPerfil == "0")
                    {
                        dictionaryValidateUser.Add("hidAccion", "L");
                    }
                    else
                    {
                        resultado = EvaluateAmount(strIdSession, transaction, sCodPerfil, sMonto, sUnidad, strmodalidad,
                            hidConcepto);

                        blnRetorno = resultado == "1";

                        if (blnRetorno)
                        {
                            dictionaryValidateUser.Add("hidAccion", "G");
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                        }
                        else
                        {
                            dictionaryValidateUser.Add("hidAccion", "M");
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                        }
                    }
                }

                if (sPagina == "4")
                {
                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        dictionaryValidateUser.Add("hidAccion", "D");
                    }
                    else
                    {
                        blnRetorno = false;
                        dictionaryValidateUser.Add("hidAccion", "F");
                    }
                }

                if (sPagina == "5")
                {
                    blnRetorno = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                    if (blnRetorno)
                    {
                        hidAccion = Claro.Utils.GetValueFromConfigFile("strConstActDesServicios",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                        dictionaryValidateUser.Add("hidAccion", hidAccion);

                        if (hidOpcion == Claro.Utils.GetValueFromConfigFile("strConstValidaFidelizaApadece",
                                ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Claro.Utils.GetValueFromConfigFile("strConstUsrValFA",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Claro.Utils.GetValueFromConfigFile("strConstFechaProgMigracion",
                                     ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Claro.Utils.GetValueFromConfigFile("strConstUsrValPF",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Claro.Utils.GetValueFromConfigFile("strConstValidaProgramarFecha",
                                     ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Claro.Utils.GetValueFromConfigFile("strConstUsrValPF",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Claro.Utils.GetValueFromConfigFile("strConstFidelizaMigracion",
                                     ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Claro.Utils.GetValueFromConfigFile("strConstUsrValPCHECK",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Claro.Utils.GetValueFromConfigFile("strConstActivRetencion",
                                     ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Claro.Utils.GetValueFromConfigFile("strConstUsrValACTRETCHK",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Claro.Utils.GetValueFromConfigFile("strConstModFechaProgMigraPlan",
                                     ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Claro.Utils.GetValueFromConfigFile("strConstUsrValPF",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }

                        else if (hidOpcion == Claro.Utils.GetValueFromConfigFile("strConstFidelizaPenalidadMigraPlan",
                                     ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Claro.Utils.GetValueFromConfigFile("strConstUsrValPCHECK",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                    }
                    else
                    {
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }

                if (sPagina == "99")
                {
                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        hidAccion = "H";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                    else
                    {
                        blnRetorno = false;
                        hidAccion = Claro.Utils.GetValueFromConfigFile("strConstUsrValTN",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }
            }

            var userValidatorLog = GetEmployerDate(strIdSession, sUsuario);
            dictionaryValidateUser.Add("NamesUserValidator",
                userValidatorLog.strNomb + " " + userValidatorLog.strApPat + ' ' + userValidatorLog.strApMat);
            dictionaryValidateUser.Add("EmailUserValidator", userValidatorLog.strEmail);

            return new JsonResult
            {
                Data = dictionaryValidateUser,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public string EvaluateAmount(string strIdSession, string transaction, string vModalidad, string vMonto,
            string vUnidad, string vListaPerfil, string vTipoTelefono)
        {
            var audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            var objRequest = new EvaluateAmountRequest
            {
                audit = audit,
                StrIdSession = strIdSession,
                VModalidad = vModalidad,
                StrTransaction = transaction,
                VMonto = System.Convert.ToDouble(vMonto),
                VUnidad = vUnidad,
                VListaPerfil = vListaPerfil,
                VTipoTelefono = vTipoTelefono
            };

            var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetEvaluateAmount(objRequest));
            var resultado = objResponse.Resultado ? "1" : "0";
            return resultado;
        }

        public string EvaluateAmount_DCM(string strIdSession, string transaction, string vModalidad, string vMonto,
            string vUnidad, string vListaPerfil, string vTipoTelefono)
        {
            var audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            var objRequest = new EvaluateAmountRequest
            {
                audit = audit,
                StrIdSession = strIdSession,
                VModalidad = vModalidad,
                StrTransaction = transaction,
                VMonto = System.Convert.ToDouble(vMonto),
                VUnidad = vUnidad,
                VListaPerfil = vListaPerfil,
                VTipoTelefono = vTipoTelefono
            };

            var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetEvaluateAmount_DCM(objRequest));
            var resultado = objResponse.Resultado ? "1" : "0";
            return resultado;
        }

        public JsonResult UserValidate_PageLoad(string strIdSession, string pag = "", string paginadcm = "",
            string monto = "", string opcion = "", string modalidad = "", string tipo = "", string motivo = "",
            string telefono = "", string loginS = "", string co = "", string migracion = "", string descripcion = "",
            string transaccion = "", string detEntAccion = "", string tipotx = "", string unidad = "",
            string hidOpcion = "", string hidAccion = "")
        {
            var hidTelefono = GetNumber(strIdSession, false, telefono);
            Logging.Info(strIdSession, "Transaction: ", "UserValidate_PageLoad() - hidTelefono: " + hidTelefono);
            var dictionaryPageLoad = new Dictionary<string, object>
                    {
                        {"hidPagina", pag},
                        {"hidPagDCM", paginadcm},
                        {"hidMonto", monto},
                        {"hidUnidad", unidad},
                        {"hidOpcion", opcion},
                        {"hidModalidad", modalidad},
                        {"hidTipoA", tipo},
                        {"hidMotivoA", motivo},
                        {"hidTelefono", hidTelefono},
                        {"hidLogin", loginS},
                        {"hidCO", co},
                        {"hidMigracion", migracion},
                        {"hidDescripcionProceso", descripcion},
                        {
                            "hidConcepto",
                            ListValueXmlMethod_Auth(strIdSession, transaccion, "IFIData.xml", "ListaValidaMontoXTransaccion",
                                "1")
                        },
                        {"hidAccionDetEnt", detEntAccion}
                    };

            Logging.Info(strIdSession, "Transaction: ", "UserValidate_PageLoad() - tipotx: " + tipotx);
            switch (tipotx)
            {
                case "L":
                    dictionaryPageLoad.Add("lblTitulo", Claro.SIACU.Constants.ConstAutoLinea);
                    break;
                case "E":
                    dictionaryPageLoad.Add("lblTitulo", Claro.SIACU.Constants.ConstAutoEquipo);
                    break;
                case "M":
                    dictionaryPageLoad.Add("lblTitulo", Claro.SIACU.Constants.ConstAutorizacion);
                    break;
            }

            Logging.Info(strIdSession, "Transaction: ", "UserValidate_PageLoad() - dictionaryPageLoad: " + dictionaryPageLoad.Count().ToString());
            if (!pag.Equals("15"))
            {
                if (pag.Equals("6") && LocalAuthorization(strIdSession, "CurrentUser", hidOpcion))
                {
                    dictionaryPageLoad.Add("ReseteoLinea", true);
                }
                else
                {
                    dictionaryPageLoad.Add("ReseteoLinea", false);
                }
            }

            return new JsonResult
            {
                Data = dictionaryPageLoad,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public string ListValueXmlMethod_Auth(string strIdSession, string transaccion, string fileName,
            string strNameFunction, string strFlagCode)
        {
            var lstItemVm = new List<ListItemVM>();
            var responseMethod = string.Empty;
            var objAuditRequest = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

            try
            {

                var objListItemResponse = Claro.Utils.GetListValuesXML(strNameFunction, strFlagCode, fileName);

                foreach (var item in objListItemResponse)
                {
                    if (item.Code2 == transaccion)
                    {
                        responseMethod = item.Code;
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }
            return responseMethod;
        }

        //        public string ValidatePermissionPost(string idSession, string contractID)
        //        {
        //            string strNameTypePhone = "";
        //            Logging.Info("Session: " + idSession, "Transaction: Start ", "contractID: " + contractID);

        //            if (!string.IsNullOrEmpty(contractID))
        //            {
        //                string var = "";
        //                string[] var2;
        //                string[] var3;
        //                string[] var4;
        //                string varTFI = "";
        //                string[] varTFI2;
        //                string[] varTFI3;
        //                string message = "";
        //                string message2 = "";
        //                string strCodResult = "";
        //                int intCodPlanTariff = 0;
        //                int flagFound = 0;

        //                List<ParameterTerminalTPIHelper> list = new List<ParameterTerminalTPIHelper>();
        //                List<ParameterTerminalTPIHelper> list2 = new List<ParameterTerminalTPIHelper>();
        //                CommonService.AuditRequest auditC =
        //                    APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(idSession);
        //                PostTransacService.AuditRequest audit =
        //                    APPCODE.Common.CreateAuditRequest<PostTransacService.AuditRequest>(idSession);

        //                try
        //                {


        //                    int intCodParam = Convert.ToInt(KEY.AppSettings("gObtenerParametroTerminalTPI"));
        //                    int intCodParam2 = Convert.ToInt(KEY.AppSettings("gObtenerParametroSoloTFIPostpago"));


        //                    ParameterTerminalTPIResponse objResponse = GetParameterTerminalTPI(intCodParam, auditC);
        //                    list = MappingParameterTerminalTPI(objResponse);
        //                    message = objResponse.Message;

        //                    objResponse = GetParameterTerminalTPI(intCodParam2, auditC);
        //                    list2 = MappingParameterTerminalTPI(objResponse);
        //                    message2 = objResponse.Message;

        //                    var = list[0].ValorC.ToString();
        //                    varTFI = list2[0].ValorC.ToString();

        //                    DataLineResponsePostPaid objData = GetDataLine(contractID, audit);

        //                    strCodResult = objData.StrResponse;

        //                    intCodPlanTariff = Convert.ToInt(objData.DataLine.CodPlanTariff);

        //                    if (!string.IsNullOrEmpty(var))
        //                    {
        //                        var2 = var.Split(';');
        //                        for (int i = 0; i < var2.Length - 1; i++)
        //                        {
        //                            var3 = var2[i].Split(':');
        //                            if (var3.Length > 0)
        //                            {
        //                                if (!string.IsNullOrEmpty(var3[1]))
        //                                {
        //                                    var4 = var3[1].Split(',');
        //                                    for (int j = 0; j < var4.Length; j++)
        //                                    {
        //                                        if (intCodPlanTariff == Functions.CheckInt(var4[j].Trim()))
        //                                        {
        //                                            strNameTypePhone = var3[0].Trim();
        //                                            flagFound = 1;
        //                                            break;
        //                                        }
        //                                        else
        //                                        {
        //                                            strNameTypePhone = ConstantsHFC.strTipoLinea_POSTPAGO;
        //                                            flagFound = 0;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            if (flagFound == 1)
        //                            {
        //                                break;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        strNameTypePhone = ConstantsHFC.strTipoLinea_POSTPAGO;
        //                    }

        //                    if (flagFound == 0)
        //                    {
        //                        if (!string.IsNullOrEmpty(varTFI))
        //                        {
        //                            varTFI2 = varTFI.Split(';');
        //                            for (int x = 0; x < varTFI2.Length - 1; x++)
        //                            {
        //                                varTFI3 = varTFI2[x].Split(',');
        //                                for (int y = 0; y < varTFI3.Length; y++)
        //                                {
        //                                    if (intCodPlanTariff == Functions.CheckInt(varTFI3[y].Trim()))
        //                                    {
        //                                        strNameTypePhone = ConstantsHFC.strTipoLinea_FIJO_POST;
        //                                        flagFound = 1;
        //                                        break;
        //                                    }
        //                                    else
        //                                    {
        //                                        strNameTypePhone = ConstantsHFC.strTipoLinea_POSTPAGO;
        //                                        flagFound = 0;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }



        //                }
        //                catch (Exception ex)
        //                {
        //                    Logging.Error(idSession, audit.transaction, ex.Message);
        //                    Logging.Info("Session: " + idSession, "Transaction: Start ", "Error: " + ex.Message);

        //                }


        //            }
        //            else
        //            {
        //                strNameTypePhone = string.Empty;
        //            }
        //            Logging.Info("Session: " + idSession, "Transaction: Start ", "salida: " + strNameTypePhone.ToUpper());

        //            return strNameTypePhone.ToUpper();
        //        }

        //        private ParameterTerminalTPIResponse GetParameterTerminalTPI(int ParameterID,
        //            CommonService.AuditRequest audit)
        //        {
        //            ParameterTerminalTPIRequest objRequest = new ParameterTerminalTPIRequest()
        //            {
        //                ParameterID = ParameterID,
        //                audit = audit
        //            };
        //            ParameterTerminalTPIResponse objResponse =
        //                Logging.ExecuteMethod<ParameterTerminalTPIResponse>(() =>
        //                {
        //                    return _oServiceCommon.GetParameterTerminalTPI(objRequest);
        //                });
        //            return objResponse;
        //        }

        //        private List<ParameterTerminalTPIHelper> MappingParameterTerminalTPI(ParameterTerminalTPIResponse objResponse)
        //        {
        //            List<ParameterTerminalTPIHelper> list = new List<ParameterTerminalTPIHelper>();
        //            ParameterTerminalTPIHelper entity;
        //            foreach (var item in objResponse.ListParameterTeminalTPI)
        //            {
        //                entity = new ParameterTerminalTPIHelper()
        //                {
        //                    ParameterID = item.ParameterID,
        //                    Name = item.Name,
        //                    Description = item.Description,
        //                    Type = item.Type,
        //                    ValorC = item.ValorC,
        //                    ValorN = item.ValorN,
        //                    ValorL = item.ValorL
        //                };
        //                list.Add(entity);
        //            }
        //            return list;
        //        }

        //        private DataLineResponsePostPaid GetDataLine(string contractID, PostTransacService.AuditRequest audit)
        //        {
        //            DataLineRequestPostPaid objRequest = new DataLineRequestPostPaid()
        //            {
        //                ContractID = contractID,
        //                audit = audit
        //            };
        //            DataLineResponsePostPaid objResponse =
        //                Logging.ExecuteMethod<DataLineResponsePostPaid>(() =>
        //                {
        //                    return _oServicePostpaid.GetDataLine(objRequest);
        //                });

        //            return objResponse;
        //        }

        public void RegisterAudit(string strDescription, double dblCodeEvent, object[,] objDetailAuditory,
            string strIdSession)
        {

            //var strIdTransaccion = string.Format("{0}{1}", ConfigurationManager.AppSettings("gConstIDTransaccionAplicacionPostpago"), Functions.CadenaAleatoria());
            string strTransaction = Convert.ToString(dblCodeEvent);
            //string strIPCustomer = ""; // Convert.ToString(HttpContext.Current.Request.UserHostAddress);
            string strIpServi = Request.ServerVariables["LOCAL_ADDR"];
            string strNameCustomer = ""; //Convert.ToString(HttpContext.Current.Session("NombreUsuario"));
            //strNameCustomer = string.IsNullOrEmpty(strNameCustomer) ? strIPCustomer : strNameCustomer;
            string strNameServidor = Request.ServerVariables["SERVER_NAME"];
            string strService = ConfigurationManager.AppSettings("gConstEvtServicio_ModCP");

            var strTelephone = string.Empty;
            var sbTexto = new StringBuilder();
            for (int i = 0; i < ((objDetailAuditory.Length / 4) - 1); i++)
            {
                if (objDetailAuditory.GetValue(i, 1) != null && objDetailAuditory.GetValue(i, 2) != null)
                {
                    sbTexto.Append(" " + objDetailAuditory.GetValue(i, 1) + " : ");
                    sbTexto.Append(objDetailAuditory.GetValue(i, 2));
                    if (objDetailAuditory.GetValue(i, 1) ==
                        Claro.SIACU.Constants.gstrTelefono)
                    {
                        strTelephone = (string)objDetailAuditory.GetValue(i, 2);
                    }
                }
            }
            string strTexto = !string.IsNullOrEmpty(strDescription)
                ? string.Format("{0}{1}", strDescription, sbTexto.ToString())
                : string.Empty;
            strTexto = (strTexto.Length - 1 > 255) ? strTexto.Substring(0, 255) : strTexto;
            SaveAuditM(strTransaction, strService, strTexto, strTelephone, strNameCustomer, strIdSession,
                strNameServidor, strIpServi);
        }

        //        public string StatusLineValidate(string strIdSession, int vintTipoValidacion, string statusLinea)
        //        {
        //            Logging.Info(strIdSession, "CommonServices", "StatusLineValidate - Entro");//Temporal
        //            var respuesta = string.Empty;
        //            switch (vintTipoValidacion)
        //            {
        //                case 1:
        //                    respuesta = Claro.Utils.GetValueFromConfigFile("strMsgValidacionContratoInactivo",
        //                        ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
        //                    break;
        //                case 2:
        //                    if (statusLinea == Claro.Utils.GetValueFromConfigFile("strEstadoContratoSuspendido",
        //                            ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
        //                    {
        //                        respuesta = Claro.Utils.GetValueFromConfigFile("strConstArchivoSIACUIFIConfigMsg",
        //                            "strMsgValidacionContratoSuspendido");
        //                    }
        //                    break;
        //                case 3:
        //                    if (statusLinea == Claro.Utils.GetValueFromConfigFile("strEstadoContratoReservado",
        //                            ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
        //                    {
        //                        respuesta = Claro.Utils.GetValueFromConfigFile("strConstArchivoSIACUIFIConfigMsg",
        //                            "strMsgValidacionContratoReservado");
        //                    }
        //                    break;
        //                case 4:
        //                    if (statusLinea == Claro.Utils.GetValueFromConfigFile("strEstadoContratoInactivo",
        //                            ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
        //                    {
        //                        respuesta = Claro.Utils.GetValueFromConfigFile("strConstArchivoSIACUIFIConfigMsg",
        //                            "strMsgValidacionContratoInactivo");
        //                    }
        //                    if (statusLinea == Claro.Utils.GetValueFromConfigFile("strEstadoContratoReservado",
        //                            ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
        //                    {
        //                        respuesta = Claro.Utils.GetValueFromConfigFile("strConstArchivoSIACUIFIConfigMsg",
        //                            "strMsgValidacionContratoReservado");
        //                    }
        //                    break;
        //                default:
        //                    if (statusLinea == Claro.Utils.GetValueFromConfigFile("strEstadoContratoInactivo",
        //                            ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
        //                    {
        //                        respuesta = Claro.Utils.GetValueFromConfigFile("strConstArchivoSIACUIFIConfigMsg",
        //                            "strMsgValidacionContratoInactivo");
        //                    }
        //                    if (statusLinea == Claro.Utils.GetValueFromConfigFile("strEstadoContratoSuspendido",
        //                            ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
        //                    {
        //                        respuesta = Claro.Utils.GetValueFromConfigFile("strConstArchivoSIACUIFIConfigMsg",
        //                            "strMsgValidacionContratoSuspendido");
        //                    }
        //                    if (statusLinea == Claro.Utils.GetValueFromConfigFile("strEstadoContratoReservado",
        //                            ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
        //                    {
        //                        respuesta = Claro.Utils.GetValueFromConfigFile("strConstArchivoSIACUIFIConfigMsg",
        //                            "strMsgValidacionContratoReservado");
        //                    }
        //                    break;
        //            }
        //            Logging.Info(strIdSession, "CommonServices", "Respuesta:" + respuesta);//Temporal
        //            return respuesta;
        //        }

        //        public JsonResult ValidateCustomerJanus(string strIdSession)
        //        {
        //            var strResult = "T";
        //            if (strResult != ConstantsHFC.strLetraT)
        //            {
        //                strResult = "k";
        //            }
        //            return Json(strResult, JsonRequestBehavior.AllowGet);
        //        }

        public JsonResult GetUsers(string strIdSession, string strCodeUser)
        {

           

            UserResponse objRegionResponse = null;
            CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            UserRequest objRegionRequest = new UserRequest();
            objRegionRequest.audit = audit;

            objRegionRequest.CodeUser = strCodeUser;
            objRegionRequest.CodeRol = Claro.Constants.NumberOneNegativeString;
            objRegionRequest.CodeCac = Claro.Constants.NumberOneNegativeString;
            objRegionRequest.State = Claro.Constants.NumberOneNegativeString;

            try
            {
                objRegionResponse = Logging.ExecuteMethod<UserResponse>(() =>
                {
                    return _oServiceCommon.GetUser(objRegionRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(objRegionResponse.UserModel, JsonRequestBehavior.AllowGet);
        }

        //        //Cargo apadece (ObtenerPenalidadCambioPlan - ObtenerApadeceCancelacionRet)
        //        public JsonResult GetApadece(string strIdSession, string strTypeCustomer, string appTypeCustomer)
        //        {
        //            Boolean booSalida;
        //            var dtApadece = "";
        //            var intCodError = "";
        //            var strDesError = "";

        //            double dblPenalidadPCS;
        //            double dblPenalidadAPADECE;
        //            double dblMontoFidelizar;
        //            double dblNroFacuras;
        //            double dblCargoFijoActual;
        //            double dblCargoFijoNuevoPlan;
        //            double dblcuerdoIdSalida;
        //            double dblDiasPendientes;
        //            double dblCargoFijoDiario;
        //            double dblPrecioLista;
        //            double dblPrecioVenta;

        //            //hidValidaCargoFijoSAP.Value = ""

        //            //booSalida = False

        //            PenaltyChangePlanResponse objPenaltyChangePlanResponse = null;
        //            if (strTypeCustomer == appTypeCustomer)
        //            {
        //                CommonIFIService.AuditRequest audit =
        //                    APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
        //                PenaltyChangePlanRequest objPenaltyChangePlanRequest = new PenaltyChangePlanRequest()
        //                {
        //                    audit = audit
        //                };

        //                try
        //                {
        //                    objPenaltyChangePlanResponse = Logging.ExecuteMethod<PenaltyChangePlanResponse>(
        //                        () =>
        //                        {
        //                            return _oServiceCommon.GetPenaltyChangePlan(objPenaltyChangePlanRequest);
        //                        });
        //                }
        //                catch (Exception ex)
        //                {
        //                    Logging.Error(strIdSession, audit.transaction, ex.Message);
        //                    throw new Exception(ex.Message);
        //                }
        //            }


        //            if (objPenaltyChangePlanResponse.Result)
        //            {
        //                dblPenalidadAPADECE = 0;
        //            }
        //            //if (dblPenalidadAPADECE = 0)
        //            //{

        //            //}
        //            //hidValidaCargoFijoSAP.Value = ConstantesWeb.NumeracionUNO

        //            return Json("", JsonRequestBehavior.AllowGet);
        //        }

        public GetEmployerDateResponseCommon GetEmployerDate(string strIdSession, string strCodeUser)
        {
            GetEmployerDateResponseCommon objRegionResponse = null;
            CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            GetEmployerDateRequestCommon objRegionRequest = new GetEmployerDateRequestCommon();
            objRegionRequest.audit = audit;
            objRegionRequest.strCurrentUser = strCodeUser;
            try
            {
                objRegionResponse = Logging.ExecuteMethod<GetEmployerDateResponseCommon>(() =>
                {
                    return _oServiceCommon.GetEmployerDate(objRegionRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return objRegionResponse;
        }

        public bool Authorize_TX_Search(string strIdSession, string transaction, string pUsuario, string pClave,
            string strKey)
        {
            var resultado = IsAuthenticated_LDAP(strIdSession, transaction, pUsuario, pClave);
            var response = false;
            try
            {
                if (resultado)
                {
                    var usuario = string.Empty;

                    var codApp = KEY.AppSettings("CodAplicacion_SIACPO");
                    var ipApp = KEY.AppSettings("strWebIpCod_SIACPO");
                    var nomApp = KEY.AppSettings("NombreAplicacion_SIACPO");

                    CommonIFIService.AuditRequest audit =
                        APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
                    var objRequest = new VerifyUserRequest
                    {
                        audit = audit,
                        AppName = nomApp,
                        TransactionId = audit.transaction,
                        AppCode = codApp,
                        SessionId = strIdSession,
                        AppId = ipApp,
                        Username = pUsuario.Trim()
                    };

                    var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetVerifyUser(objRequest));

                    if (objResponse.LstConsultSecurities.Count > 0)
                    {
                        var item = objResponse.LstConsultSecurities[0];
                        usuario = item.Usuaccod;

                        var strPermisos = GetAccessOfPage(strIdSession);
                        if (strPermisos.IndexOf(KEY.AppSettings(strKey), StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            Session["UsuarioValidador"] = string.Empty;
                        }
                        else
                        {
                            response = true;
                            Session["UsuarioValidador"] = pUsuario.Trim();
                        }
                    }
                    else
                    {
                        usuario = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, transaction, ex.Message);
                throw;
            }

            return response;
        }

        public ParameterDataResponseCommon GetParameterData(string strIdSession, string name)
        {

            ParameterDataResponseCommon objResponse = new ParameterDataResponseCommon();
            CommonIFIService.AuditRequest audit =
               APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            ParameterDataRequestCommon objRequest = new ParameterDataRequestCommon();

            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Metodo : GetParameterData");
            objRequest.audit = audit;
            objRequest.Name = name;
            try
            {
                objResponse = Logging.ExecuteMethod<ParameterDataResponseCommon>(() =>
                {
                    return _oServiceCommon.GetParameterData(objRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(objRequest.audit.Session, objRequest.audit.transaction, ex.Message);

            }
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Metodo : GetParameterData");
            return objResponse;

        }

        //        public bool GetSendEmailCommon(GetEmployerDateResponseCommon item, string strIdSession, string phonfNroGener,
        //            string cuenta, string currentUser, string currentTerminal)
        //        {
        //            string strTemplateEmail = TemplateEmailCommon(item, phonfNroGener, cuenta, currentUser, currentTerminal);
        //            string strDestinatarios = item.strEmail;
        //            string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");

        //            SendEmailResponseCommon objGetSendEmailResponse =
        //                new SendEmailResponseCommon();
        //            CommonIFIService.AuditRequest AuditRequest =
        //                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
        //            SendEmailRequestCommon objGetSendEmailRequest =
        //                new SendEmailRequestCommon()
        //                {
        //                    audit = AuditRequest,
        //                    strSender = strRemitente,
        //                    strTo = strDestinatarios,
        //                    strMessage = strTemplateEmail,
        //                    strSubject = ConfigurationManager.AppSettings("gConstAsuntoDetLlamadaSaliente")
        //                };
        //            try
        //            {
        //                objGetSendEmailResponse =
        //                    Logging.ExecuteMethod<SendEmailResponseCommon>(() =>
        //                    {
        //                        return _oServiceCommon.GetSendEmail(objGetSendEmailRequest);
        //                    });
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, objGetSendEmailRequest.audit.transaction, ex.Message);
        //                throw new Exception(AuditRequest.transaction);
        //            }

        //            return true;
        //        }

        //        public string TemplateEmailCommon(GetEmployerDateResponseCommon item, string phonfNroGener, string cuenta,
        //            string currentUser, string currentTerminal)
        //        {
        //            string strmessage = string.Empty;
        //            strmessage = "<html>";
        //            strmessage += "<head>";
        //            strmessage += "<style type='text/css'>";
        //            strmessage += "<!--";
        //            strmessage += ".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
        //            strmessage += ".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
        //            strmessage += "-->";
        //            strmessage += "</style>";
        //            strmessage += "<body>";
        //            strmessage += "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
        //            strmessage += string.Format("<tr><td width='180' class='Estilo2' height='22'>Estimado(a) {0} {1} {2} </td>",
        //                item.strNomb, item.strApPat, item.strApMat);
        //            strmessage += "<tr><td height='10'></td>";
        //            strmessage +=
        //                "<tr><td class='Estilo1'>Se le informa que su Código y Password de Autorización ha sido utilizado para realizar una Transacción relacionada a Detalle de Llamadas desde las siguientes entradas</td></tr>";
        //            strmessage += "<tr><td height='10'></td>";

        //            strmessage += "<tr>";
        //            strmessage += "<td align='center'>";
        //            strmessage += "<Table width='90%' border='0' cellpadding='0' cellspacing='0'>";
        //            strmessage +=
        //                string.Format(
        //                    "<tr><td width='180' class='Estilo2' height='22'>Nro. Telefónico :</td><td class='Estilo1'> {0} </td></tr>",
        //                    phonfNroGener);
        //            strmessage +=
        //                string.Format(
        //                    "<tr><td width='180' class='Estilo2' height='22'>Cuenta :</td><td class='Estilo1'>{0}</td></tr>",
        //                    cuenta);
        //            strmessage +=
        //                string.Format(
        //                    "<tr><td width='180' class='Estilo2' height='22'>Usuario Logueado:</td><td class='Estilo1'>{0}</td></tr>",
        //                    currentUser);
        //            strmessage +=
        //                string.Format(
        //                    "<tr><td width='180' class='Estilo2' height='22'>Terminal o Computador :</td><td class='Estilo1'>{0}</td></tr>",
        //                    currentTerminal);
        //            strmessage +=
        //                string.Format(
        //                    "<tr><td width='180' class='Estilo2' height='22'>Fecha y Hora :</td><td class='Estilo1'>{0}</td></tr>",
        //                    DateTime.UtcNow.AddHours(-5));
        //            strmessage += "</Table>";
        //            strmessage += "</td></tr>";

        //            strmessage += "<tr><td height='10'></td>";
        //            strmessage += "<tr><td height='10'></td>";
        //            strmessage += "<tr><td height='10'></td>";
        //            strmessage += "<tr><td class='Estilo1'>Saludos Cordiales,</td></tr>";
        //            strmessage += "<tr><td class='Estilo1'>Atención al Cliente</td></tr>";
        //            strmessage += "<tr><td height='10'></td>";
        //            strmessage += "<tr><td height='10'></td>";
        //            strmessage +=
        //                "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local)</td></tr>";
        //            strmessage += "</table>";
        //            strmessage += "</body>";
        //            strmessage += "</html>";

        //            return strmessage;
        //        }

        //        public JsonResult SendMail(string strIdSession, string codeUser, string phonfNroGener, string cuenta)
        //        {
        //            var receiverUser = GetEmployerDate(strIdSession, codeUser);
        //            var resultado = false;
        //            if (!string.IsNullOrEmpty(receiverUser.strEmail))
        //            {
        //                string currentUser = CurrentUser(strIdSession);
        //                string currentTerminal = CurrentTerminal();
        //                resultado = GetSendEmailCommon(receiverUser, strIdSession, phonfNroGener, cuenta, currentUser, currentTerminal);
        //            }

        //            return new JsonResult
        //            {
        //                Data = resultado,
        //                ContentType = "application/json",
        //                ContentEncoding = Encoding.UTF8,
        //                JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //            };
        //        }

        //        [HttpPost]
        //        public JsonResult SendMail(string strIdSession, string codeUser, string phonfNroGener, string cuenta,
        //            string currentUser, string currentTerminal)
        //        {
        //            var receiverUser = GetEmployerDate(strIdSession, codeUser);
        //            var resultado = false;
        //            if (!string.IsNullOrEmpty(receiverUser.strEmail))
        //            {
        //                resultado = GetSendEmailCommon(receiverUser, strIdSession, phonfNroGener, cuenta, currentUser,
        //                    currentTerminal);
        //            }

        //            return new JsonResult
        //            {
        //                Data = resultado,
        //                ContentType = "application/json",
        //                ContentEncoding = Encoding.UTF8,
        //                JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //            };
        //        }

        //        public JsonResult GetConsultIGV(string strIdSession)
        //        {
        //            ConsultIGVModel oConsultIGV = null;
        //            CommonIFIService.AuditRequest audit =
        //                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

        //            var objRequest = new ConsultIGVRequest
        //            {
        //                audit = audit,
        //                SessionId = strIdSession,
        //                TransactionId = audit.transaction,
        //                AppId = audit.ipAddress,
        //                AppName = audit.applicationName,
        //                Username = audit.userName
        //            };

        //            try
        //            {
        //                var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetConsultIGV(objRequest));

        //                if (objResponse != null && objResponse.ListConsultIGV.Count > 0)
        //                {
        //                    DateTime currentDate = DateTime.Now;
        //                    foreach (var item in objResponse.ListConsultIGV)
        //                    {
        //                        if ((Convert.ToDate(item.impudFecIniVigencia) <= currentDate &&
        //                             Convert.ToDate(item.impudFecFinVigencia) >= currentDate) &&
        //                            item.impunTipDoc == Constant.strCero)
        //                        {
        //                            oConsultIGV = new ConsultIGVModel();
        //                            oConsultIGV.igv = item.igv;
        //                            oConsultIGV.igvD = item.igvD;
        //                            oConsultIGV.impudFecFinVigencia = item.impudFecFinVigencia;
        //                            oConsultIGV.impudFecIniVigencia = item.impudFecIniVigencia;
        //                            oConsultIGV.impudFecRegistro = item.impudFecRegistro;
        //                            oConsultIGV.impunTipDoc = item.impunTipDoc;
        //                            oConsultIGV.imputId = item.imputId;
        //                            oConsultIGV.impuvDes = item.impuvDes;
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
        //            }
        //            string strIGV = oConsultIGV == null ? string.Empty : Claro.Utils.CheckStr(oConsultIGV.igv);
        //            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + objRequest.audit.transaction, "End a GetConsultIGV Controller => oConsultIGV.igv: " + strIGV); // Temporal
        //            return Json(new { data = oConsultIGV });
        //        }

        //        public JsonResult GetMotiveSot(string strIdSession)
        //        {

        //            MotiveSotResponseCommon objMotiveSotResponseCommon;

        //            CommonIFIService.AuditRequest audit =
        //                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
        //            MotiveSotRequestCommon objMotiveSotRequestCommon =
        //                new MotiveSotRequestCommon()
        //                {
        //                    audit = audit
        //                };

        //            try
        //            {
        //                objMotiveSotResponseCommon =
        //                    Logging.ExecuteMethod<MotiveSotResponseCommon>(() =>
        //                    {
        //                        return _oServiceCommon.GetMotiveSot(objMotiveSotRequestCommon);
        //                    });
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, objMotiveSotRequestCommon.audit.transaction, ex.Message);
        //                throw new Exception(audit.transaction);
        //            }

        //            CommonServices objCommonServices = null;
        //            if (objMotiveSotResponseCommon != null && objMotiveSotResponseCommon.getMotiveSot != null)
        //            {
        //                objCommonServices = new CommonServices();
        //                List<CacDacTypeVM> listCacDacTypes =
        //                    new List<CacDacTypeVM>();

        //                foreach (CommonIFIService.ListItem item in objMotiveSotResponseCommon.getMotiveSot)
        //                {
        //                    listCacDacTypes.Add(new CacDacTypeVM()
        //                    {
        //                        Code = item.Code,
        //                        Description = item.Description
        //                    });
        //                }
        //                objCommonServices.CacDacTypes = listCacDacTypes;
        //            }

        //            return Json(new { data = objCommonServices });
        //        }

        //        #region Servicios adicionales LTE/HFC

        //        public void SendEmailAdditionalService(string strAddressee, string strInteractionId, string strNameCustomer,
        //            string strLegalRepresent, string strCustomerId, string strTypeDoc, string strNroDoc,
        //           string strTypeTransaction, string strIdSession, string strAdjunto, byte[] attachFile, string strAccion = "")
        //        {
        //            var objDatInteraction = GetInfoInteractionTemplate(strIdSession, strInteractionId);


        //            if (strAccion == "Activación")
        //            {
        //                strAccion = "Activación de Servicios";
        //            }
        //            else
        //            {
        //                strAccion = "Desactivación de Servicios";
        //            }

        //            var strSubject = "Variación de Servicio";
        //            var strMessage = "<html>";
        //            strMessage += "<head>";
        //            strMessage += "<style type='text/css'>";
        //            strMessage += "<!--";
        //            strMessage += ".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
        //            strMessage += ".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
        //            strMessage += "-->";
        //            strMessage += "</style>";
        //            strMessage += "<body>";
        //            strMessage += "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
        //            strMessage += "<tr><td>";
        //            strMessage += MailHeader(strAccion, objDatInteraction.X_INTER_15, DateTime.Now.ToShortDateString(), strNameCustomer, strInteractionId, strLegalRepresent, strCustomerId, strTypeDoc, strNroDoc);
        //            strMessage += "</td></tr>";

        //            strMessage += "<tr><td height='10'></td>";
        //            strMessage += "<tr><td height='10'></td>";
        //            strMessage += "<tr><td height='10'></td>";
        //            strMessage += "<tr><td class='Estilo1'>Cordialmente</td></tr>";
        //            strMessage += "<tr><td class='Estilo1'>Atención al Cliente</td></tr>";
        //            strMessage += "<tr><td height='10'></td>";
        //            strMessage += "<tr><td height='10'></td>";
        //            strMessage += "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local).</td></tr>";
        //            strMessage += "</table>";
        //            strMessage += "</body>";
        //            strMessage += "</html>";

        //            string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");

        //            SendEmailResponseCommon objGetSendEmailResponse = new SendEmailResponseCommon();
        //            CommonIFIService.AuditRequest AuditRequest = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
        //            SendEmailRequestCommon objGetSendEmailRequest =
        //            new SendEmailRequestCommon()
        //            {
        //                audit = AuditRequest,
        //                strSender = strRemitente,
        //                strTo = strAddressee,
        //                strMessage = strMessage,
        //                strAttached = strAdjunto,
        //                strSubject = strSubject,
        //                AttachedByte = attachFile
        //            };
        //            try
        //            {
        //                objGetSendEmailResponse = Logging.ExecuteMethod<SendEmailResponseCommon>
        //                (
        //                    () => { return _oServiceCommon.GetSendEmailFixed(objGetSendEmailRequest); }
        //                );

        //                Logging.Info(AuditRequest.Session, AuditRequest.transaction, "INFO EMAIL CONTROLLER ACTIVACION DESACTIVACION : " + objGetSendEmailResponse.Exit);
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(objGetSendEmailRequest.audit.Session, objGetSendEmailRequest.audit.transaction, "Error EMAIL : " + ex.Message);
        //            }
        //        }

        //        public string MailHeader(string strTitle, string strCacDac, string strDate, string strHeadline,
        //            string strCaseInteraction, string strRepresentative, string strNroClaro, string strTypeDocument,
        //            string strNroDocument)
        //        {
        //            string strRetorno;
        //            strRetorno = "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
        //            strRetorno += "\t\t\t\t<tr><td width='100%' class='Estilo1'>Estimado Cliente, </td></tr>";
        //            strRetorno += "\t\t\t\t<tr><td height='10'></td>";
        //            if (strTitle == "Servicio de Variación de Débito / Crédito Manual Corporativo")
        //            {
        //                strRetorno +=
        //                    "\t\t\t\t<tr><td class='Estilo1'>Por la presente queremos informarle que se realizo un ajuste al saldo de su linea corporativa</td></tr>";
        //            }
        //            else if (strTitle == "Activación de Servicios")
        //            {
        //                strRetorno +=
        //                    "\t\t\t\t<tr><td class='Estilo1'>Por la presente queremos informarle que su solicitud de " + strTitle +
        //               " Adicionales fue registrada y estará siendo atendida en el plazo establecido.</td></tr>";
        //            }
        //            else if (strTitle == "Desactivación de Servicios")
        //            {
        //                strRetorno +=
        //                    "\t\t\t\t<tr><td class='Estilo1'>Por la presente queremos informarle que su solicitud de " + strTitle +
        //               " Adicionales fue registrada y estará siendo atendida en el plazo establecido.</td></tr>";
        //            }
        //            else
        //            {
        //                strRetorno +=
        //                    "\t\t\t\t<tr><td class='Estilo1'>Por la presente queremos informarle que su solicitud de " +
        //                    strTitle + " fue atendida.</td></tr>";
        //            }

        //            strRetorno += "\t\t\t</table>";

        //            return strRetorno;
        //        }

        //        #endregion

        //        public bool GetValidateCustomerId(CustomersDataModel model, string strCustomerId, string strIdSession)
        //        {
        //            ValidateCustomerIdResponse objRegionResponse = null;
        //            FixedIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
        //            ValidateCustomerIdRequest objRegionRequest = new ValidateCustomerIdRequest();
        //            objRegionRequest.audit = audit;
        //            objRegionRequest.Phone = strCustomerId;
        //            bool valueCustomer = true;

        //            try
        //            {
        //                objRegionResponse = Logging.ExecuteMethod<ValidateCustomerIdResponse>(() =>
        //                {
        //                    return _oServiceFixed.GetValidateCustomerId(objRegionRequest);
        //                });
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, audit.transaction, ex.Message);
        //                throw new Exception(ex.Message);
        //            }

        //            if (objRegionResponse.FlgResult != ConstantsHFC.strCero)
        //            {
        //                if (model.StrModality == null || model.StrModality == "")
        //                    model.StrModality = ConfigurationManager.AppSettings("gConstKeyStrModalidad");

        //                CustomerResponse objCustomerResponse = null;
        //                Customer objCustomerRequest = new Customer();
        //                objCustomerRequest.audit = audit;
        //                objCustomerRequest.Telephone = model.StrPhone;
        //                objCustomerRequest.User = model.StrUser;
        //                objCustomerRequest.Name = model.StrName;
        //                objCustomerRequest.LastName = model.StrLastName;
        //                objCustomerRequest.BusinessName = model.StrBusinessName;
        //                objCustomerRequest.DocumentType = model.StrDocumentType;
        //                objCustomerRequest.DocumentNumber = model.StrDocumentNumber;
        //                objCustomerRequest.Address = model.StrAccount;
        //                objCustomerRequest.District = model.StrDistrict;
        //                objCustomerRequest.Departament = model.StrDepartament;
        //                objCustomerRequest.Province = model.StrProvince;
        //                objCustomerRequest.Modality = model.StrModality;

        //                try
        //                {
        //                    objCustomerResponse = Logging.ExecuteMethod<CustomerResponse>(() =>
        //                    {
        //                        return _oServiceFixed.GetRegisterCustomerId(objCustomerRequest);
        //                    });
        //                    if (objCustomerResponse.Resultado)
        //                    {
        //                        if (objCustomerResponse.vFlagConsulta.Trim() == ConfigurationManager.AppSettings("gConstKeyStrResultInsertCusID"))
        //                        {
        //                            valueCustomer = false;
        //                        }
        //                    }
        //                }
        //                catch (Exception e)
        //                {
        //                    Logging.Error(strIdSession, strIdSession, e.Message);
        //                }
        //            }
        //            return valueCustomer;
        //        }

        //        public CustomersDataModel LoadDatosCustomerId(CustomersDataModel model, string strIdSession)
        //        {
        //            model.StrPhone = string.Format("{0}{1}", ConfigurationManager.AppSettings("gConstKeyCustomerInteract"), model.StrCustomerId);
        //            model.StrModality = ConfigurationManager.AppSettings("gConstKeyStrModalidad");
        //            return model;
        //        }

        public string GetCustomer(string strCustomerId, string strIdSession)
        {
            string strObjId;
            var strFlgRegistrado = Claro.Constants.NumberOneString;
            CustomerResponse objCustomerResponse = null;
            FixedIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            GetCustomerRequest objCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = strCustomerId,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };
            try
            {
                objCustomerResponse = Logging.ExecuteMethod<CustomerResponse>(() =>
                {
                    return _oServiceFixed.GetCustomer(objCustomerRequest);
                });
                strObjId = objCustomerResponse.Customer.ContactCode;

            }
            catch (Exception e)
            {
                throw;
            }
            return strObjId;
        }

        public CustomerResponse GetCustomerData(GetCustomerRequest oCustomer, string strIdSession)
        {
            var strFlgRegistrado = Claro.Constants.NumberOneString;
            CustomerResponse objCustomerResponse = new CustomerResponse();
            FixedIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            GetCustomerRequest objCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = oCustomer.vPhone,
                vAccount = oCustomer.vAccount,
                vContactobjid1 = oCustomer.vContactobjid1,
                vFlagReg = strFlgRegistrado
            };
            try
            {
                objCustomerResponse = Logging.ExecuteMethod<CustomerResponse>(() =>
                {
                    return _oServiceFixed.GetCustomer(objCustomerRequest);
                });


            }
            catch (Exception e)
            {
                throw;
            }
            return objCustomerResponse;
        }
        private ParameterTerminalTPIResponse GetParameterTerminalTPI(int ParameterID,
          CommonIFIService.AuditRequest audit)
        {
            ParameterTerminalTPIRequest objRequest = new ParameterTerminalTPIRequest()
            {
                ParameterID = ParameterID,
                audit = audit
            };
            ParameterTerminalTPIResponse objResponse =
                Logging.ExecuteMethod<ParameterTerminalTPIResponse>(() =>
                {
                    return _oServiceCommon.GetParameterTerminalTPI(objRequest);
                });
            return objResponse;
        }

        public string ValidarPermiso(string strIdSession, string strContratoID, string hidListNumImportar)
        {
            string vContratoID2 = string.Empty;
            string strNombreTipoTelef = string.Empty;
            string TerminalTPI = string.Empty;
            string[] arrTerminalTPI;
            string TerminalTPI_Post = string.Empty;
            string intCodPlanTarif = string.Empty;
            string[] arrTerminalTPI_2;
            string[] arrTerminalTPI_3;
            string[] TerminalTPI_Post_2;
            string[] TerminalTPI_Post_3;

            string cadena4 = string.Empty;
            int flagEncontro = 0;
            //int parametroID =0;

            Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "strContratoID : " + strContratoID);

            int intCodParam = Convert.ToInt(ConfigurationManager.AppSettings("gObtenerParametroTerminalTPI"));
            int intCodParam2 = Convert.ToInt(ConfigurationManager.AppSettings("gObtenerParametroSoloTFIPostpago"));

            ParameterTerminalTPIResponse objTerTPI = new ParameterTerminalTPIResponse();
            ParameterTerminalTPIResponse objTFI_Post = new ParameterTerminalTPIResponse();

            PostIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession);
            CommonIFIService.AuditRequest auditC =
                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

            if (strContratoID != string.Empty)
            {
                if (strContratoID.Length > 0)
                {
                    Logging.Info("IdSession: " + strIdSession, " Método: ValidarPermiso", "Inicio GetParameterTerminalTPI - intCodParam :" + intCodParam);
                    objTerTPI = GetParameterTerminalTPI(intCodParam, auditC);

                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Inicio GetSiacParameterTFIPostpago : strIdSession " + strIdSession + "intCodParam2: " + intCodParam2);
                    objTFI_Post = GetSiacParameterTFIPostpago(strIdSession, intCodParam2);

                    TerminalTPI = objTerTPI.ListParameterTeminalTPI.FirstOrDefault().ValorC.ToString();


                    Logging.Info("IdSession: " + strIdSession, " Método: ValidarPermiso", " GetParameterTerminalTPI - TerminalTPI :" + TerminalTPI);

                    TerminalTPI_Post = objTFI_Post.ListParameterTeminalTPI.FirstOrDefault().ValorC.ToString();

                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Inicio GetSiacParameterTFIPostpago : TerminalTPI_Post " + TerminalTPI_Post);

                    DataLineResponsePostPaid oDataLinea = new DataLineResponsePostPaid();



                    DataLineRequestPostPaid objRequest = new DataLineRequestPostPaid()
                    {
                        ContractID = strContratoID,
                        audit = audit
                    };

                    oDataLinea = _oServicePostpaid.GetDataLine(objRequest);
                    intCodPlanTarif = oDataLinea.DataLine.CodPlanTariff;
                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Inicio Captura strNombreTipoTelef : strNombreTipoTelef " + strNombreTipoTelef);
                    #region "Captura strNombreTipoTelef"

                    if (TerminalTPI != string.Empty)
                    {
                        arrTerminalTPI = TerminalTPI.Split(';');

                        for (int i = 0; i < arrTerminalTPI.Length - 2; i++)
                        {
                            arrTerminalTPI_2 = arrTerminalTPI[i].ToString().Split(':');
                            if (arrTerminalTPI_2 != null)
                            {
                                if (arrTerminalTPI_2[1].ToString() != null)
                                {
                                    arrTerminalTPI_3 = arrTerminalTPI_2[1].ToString().Split(',');

                                    for (int a = 0; a < arrTerminalTPI_3.Length; a++)
                                    {

                                        if (arrTerminalTPI_3[a].ToString().Trim() == intCodPlanTarif)
                                        {
                                            strNombreTipoTelef = arrTerminalTPI_2[0].ToString().Trim();
                                            flagEncontro = 1;

                                        }
                                        else
                                        {
                                            strNombreTipoTelef = ConfigurationManager.AppSettings("strModalidadPostpago");
                                            flagEncontro = 0;
                                        }

                                    }

                                }
                            }
                        }

                    }
                    else
                    {
                        strNombreTipoTelef = ConfigurationManager.AppSettings("strModalidadPostpago");

                    }

                    #endregion
                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Fín Captura strNombreTipoTelef  : strNombreTipoTelef " + strNombreTipoTelef);

                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Inicio Captura strNombreTipoTelef 2 : strNombreTipoTelef " + strNombreTipoTelef);
                    #region "Captura strNombreTipoTelef"
                    if (flagEncontro == 0)
                    {
                        if (TerminalTPI_Post.ToString() != null)
                        {
                            TerminalTPI_Post_2 = TerminalTPI_Post.ToString().Split(';');

                            for (int n = 0; n < TerminalTPI_Post_2.Length; n++)
                            {
                                TerminalTPI_Post_3 = TerminalTPI_Post_2[n].ToString().Split(',');

                                for (int i = 0; i < TerminalTPI_Post_3.Length; i++)
                                {
                                    if (intCodPlanTarif == TerminalTPI_Post_3[i].ToString())
                                    {
                                        strNombreTipoTelef = ConfigurationManager.AppSettings("strModalidadFijoPost");
                                        flagEncontro = 1;
                                        break;
                                    }
                                    else
                                    {
                                        strNombreTipoTelef = ConfigurationManager.AppSettings("strModalidadPostpago");
                                        flagEncontro = 0;
                                    }

                                }


                            }

                        }

                    }
                    # endregion
                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Fín Captura strNombreTipoTelef  2: strNombreTipoTelef " + strNombreTipoTelef);

                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Inicio Captura strNombreTipoTelef 3 : strNombreTipoTelef " + strNombreTipoTelef);
                    #region "strNombreTipoTelef"
                    string strgConsHabilitaCambioTrasladoNumeroSoloTFI = ConfigurationManager.AppSettings("gConsHabilitaCambTrasladoNumeroCambPlanSoloTFI");
                    string[] arrConsHabilitaCambioTrasladoNumeroSoloTFI = strgConsHabilitaCambioTrasladoNumeroSoloTFI.Split(';');
                    string[] arrCHCTNSoloTFI;
                    string[] arrCHCTNSoloTFI2;

                    if (TerminalTPI_Post != string.Empty)
                    {
                        TerminalTPI_Post_2 = TerminalTPI_Post.ToString().Split(';');

                        for (int x = 0; x < TerminalTPI_Post_2.Length; x++)
                        {
                            TerminalTPI_Post_3 = TerminalTPI_Post_2[x].ToString().Split(',');

                            for (int v = 0; v < TerminalTPI_Post_3.Length; v++)
                            {

                                if (intCodPlanTarif == TerminalTPI_Post_3[v].ToString())
                                {
                                    arrCHCTNSoloTFI = arrConsHabilitaCambioTrasladoNumeroSoloTFI.ToString().Split(';');
                                    arrCHCTNSoloTFI2 = arrCHCTNSoloTFI['0'].ToString().Split(',');

                                    for (int j = 0; j < arrCHCTNSoloTFI2.Length - 1; j++)
                                    {
                                        if (intCodPlanTarif == arrCHCTNSoloTFI2[j].ToString())
                                        {
                                            strNombreTipoTelef = "HCTNumeroCPlanSoloTFI";
                                            break;
                                        }
                                    }

                                }

                            }

                        }

                    }
                    #endregion;
                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Fín Captura strNombreTipoTelef  3: strNombreTipoTelef " + strNombreTipoTelef);

                }
                else
                {
                    strNombreTipoTelef = String.Empty;
                }
            }
            else
            {
                strNombreTipoTelef = String.Empty;
            }

            return strNombreTipoTelef;

        }

        public ParameterTerminalTPIResponse GetSiacParameterTFIPostpago(string strIdSession, int parametroID)
        {
            ParameterTerminalTPIResponse objResponse = new ParameterTerminalTPIResponse();

            CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

            //SiacParametroRequestPostPaid objRequest = new SiacParametroRequestPostPaid();
            ParameterTerminalTPIRequest objRequest = new ParameterTerminalTPIRequest();
            objRequest.audit = audit;
            objRequest.ParameterID = parametroID;


            try
            {
                objResponse =
                    Logging.ExecuteMethod<ParameterTerminalTPIResponse>(() =>
                    {
                        return _oServiceCommon.GetParameterTerminalTPI(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(objRequest.audit.transaction);
            }


            return objResponse;
        }

        //        public SendEmailSBModel GetSendEmailSB(string strIdSession, string strRemitente, string strDestinatario,
        //                                 string strAsunto, string strMensaje, string strHTMLFlag, byte[] Archivo)
        //        {
        //            SendEmailSBModel objSendEmailSBModel = null;
        //            CommonIFIService.AuditRequest audit =
        //                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

        //            var objRequest = new SendEmailSBRequest
        //            {
        //                audit = audit,
        //                SessionId = strIdSession,
        //                TransactionId = audit.transaction,
        //                strAppID = audit.ipAddress,
        //                strAppCode = audit.applicationName,
        //                strAppUser = audit.userName,
        //                strRemitente = strRemitente,
        //                strDestinatario = strDestinatario,
        //                strAsunto = strAsunto,
        //                strMensaje = strMensaje,
        //                strHTMLFlag = strHTMLFlag,
        //                Archivo = Archivo
        //            };

        //            try
        //            {
        //                var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetSendEmailSB(objRequest));

        //                if (objResponse != null)
        //                {
        //                    objSendEmailSBModel = new SendEmailSBModel()
        //                    {
        //                        idTransaccion = objResponse.idTransaccion,
        //                        codigoRespuesta = objResponse.codigoRespuesta,
        //                        mensajeRespuesta = objResponse.mensajeRespuesta
        //                    };
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
        //            }

        //            return objSendEmailSBModel;
        //        }

        //        public Dictionary<string, object> GetNoteInteraction(string strIdSession, string strIdInteraction)
        //        {
        //            var dictionaryResponse = new Dictionary<string, object>();
        //            NoteInteractionResponse objNoteInteractionResponse;
        //            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
        //            NoteInteractionRequest objNoteInteractionRequest = new NoteInteractionRequest()
        //            {
        //                audit = audit,
        //                vInteraccionId = strIdInteraction
        //            };

        //            try
        //            {
        //                objNoteInteractionResponse =
        //                Logging.ExecuteMethod<NoteInteractionResponse>(() =>
        //                {
        //                    return _oServiceCommon.GetNoteInteraction(objNoteInteractionRequest);
        //                });

        //                if (objNoteInteractionResponse != null)
        //                {
        //                    dictionaryResponse.Add("FlagInsercion", objNoteInteractionResponse.rFlagInsercion);
        //                    dictionaryResponse.Add("MsgText", objNoteInteractionResponse.rMsgText);
        //                    dictionaryResponse.Add("Nota", objNoteInteractionResponse.strNota);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, objNoteInteractionRequest.audit.transaction, ex.Message);
        //                throw new Exception(audit.transaction);
        //            }

        //            return dictionaryResponse;
        //        }


        public FixedIFIService.Interaction GetCreateCase(FixedIFIService.Interaction oRequest)
        {

            FixedIFIService.Interaction oRequestNew = new FixedIFIService.Interaction();
            FixedIFIService.Interaction oResponse = new FixedIFIService.Interaction();


            try
            {

                oResponse = Logging.ExecuteMethod<FixedIFIService.Interaction>(() =>
                {
                    return _oServiceFixed.GetCreateCase(oRequest);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oResponse;
        }

        //        public CaseInsertResponse GetCaseInsert(CaseInsertRequest oRequest)
        //        {
        //            CaseInsertResponse oResponse = new CaseInsertResponse();
        //            try
        //            {
        //                oResponse = Logging.ExecuteMethod<CaseInsertResponse>(() =>
        //                {
        //                    return _oServiceFixed.GetCaseInsert(oRequest);
        //                });
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //            return oResponse;
        //        }



        public FixedIFIService.Interaction GetInsertCase(FixedIFIService.Interaction oRequest)
        {
            FixedIFIService.Interaction oResponse = new FixedIFIService.Interaction();

            try
            {
                oResponse = Logging.ExecuteMethod<FixedIFIService.Interaction>(() =>
                {
                    return _oServiceFixed.GetInsertCase(oRequest);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oResponse;
        }

        public FixedIFIService.CaseTemplate GetInsertTemplateCase(FixedIFIService.CaseTemplate oRequest)
        {
            FixedIFIService.CaseTemplate oResponse = new FixedIFIService.CaseTemplate();

            try
            {
                oResponse = Logging.ExecuteMethod<FixedIFIService.CaseTemplate>(() =>
                {
                    return _oServiceFixed.GetInsertTemplateCase(oRequest);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oResponse;
        }


        public FixedIFIService.CaseTemplate GetInsertTemplateCaseContingent(FixedIFIService.CaseTemplate oRequest)
        {
            FixedIFIService.CaseTemplate oResponse = new FixedIFIService.CaseTemplate();

            try
            {
                oResponse = Logging.ExecuteMethod<FixedIFIService.CaseTemplate>(() =>
                {
                    return _oServiceFixed.GetInsertTemplateCaseContingent(oRequest);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oResponse;
        }

        //        public CaseTemplateModel GetDynamicCaseTemplateData(string strIdSession, string strIdInteraccion)
        //        {
        //            CaseTemplateModel oCaseTemplate = null;
        //            var audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
        //            var objResponse = new DynamicCaseTemplateDataResponse();
        //            var objRequest = new DynamicCaseTemplateDataRequest()
        //            {
        //                audit = audit,
        //                vCasoID = strIdInteraccion
        //            };

        //            try
        //            {
        //                objResponse = Logging.ExecuteMethod<DynamicCaseTemplateDataResponse>(() =>
        //                {
        //                    return _oServiceCommon.GetDynamicCaseTemplateData(objRequest);
        //                });

        //                if (objResponse != null && objResponse.oCaseTemplate != null)
        //                {
        //                    oCaseTemplate = new CaseTemplateModel();
        //                    oCaseTemplate.X_SUSPENSION_DATE = objResponse.oCaseTemplate.X_SUSPENSION_DATE;
        //                    oCaseTemplate.X_CAS_1 = objResponse.oCaseTemplate.X_CAS_1;
        //                    oCaseTemplate.X_OPERATOR_PROBLEM = objResponse.oCaseTemplate.X_OPERATOR_PROBLEM;
        //                    oCaseTemplate.X_CAS_16 = objResponse.oCaseTemplate.X_CAS_16;
        //                    oCaseTemplate.X_CAS_15 = objResponse.oCaseTemplate.X_CAS_15;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
        //                throw new Exception(audit.transaction);
        //            }

        //            return oCaseTemplate;
        //        }

        public FixedIFIService.CaseTemplate ActualizaPlantillaCaso(FixedIFIService.CaseTemplate oRequest)
        {
            FixedIFIService.CaseTemplate oResponse = new FixedIFIService.CaseTemplate();

            try
            {
                oResponse = Logging.ExecuteMethod<FixedIFIService.CaseTemplate>(() =>
                {
                    return _oServiceFixed.ActualizaPlantillaCaso(oRequest);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oResponse;
        }

        #region JCAA
        public string GetOBJID(string strIdSession, string strCustomerID)
        {
            string strObjId = String.Empty;
            string strFlgRegistrado = Claro.Constants.NumberOneString;
            string strFlagConsulta = string.Empty;
            string strMsgResultado = string.Empty;
            GetClientRequestCommon oClienteRequest = new GetClientRequestCommon()
            {
                strflagreg = strFlgRegistrado,
                strContactobjid = strObjId,
                straccount = string.Empty,
                strphone = strCustomerID,
                audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession)
            };
            GetClientResponseComon oClienteResponse = Logging.ExecuteMethod<GetClientResponseComon>(() =>
            {
                return _oServiceCommon.GetObClient(oClienteRequest);
            });
            return oClienteResponse.listClient.OBJID_CONTACTO;
        }
        public JsonResult GetTypification(string strIdSession, string strTransactionName, string strType = "")
        {
            TypificationResponse objTypificationResponse = null;
            CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            TypificationRequest objTypificationRequest =
                new TypificationRequest();
            objTypificationRequest.audit = audit;
            objTypificationRequest.TRANSACTION_NAME = strTransactionName;

            try
            {
                objTypificationResponse = Logging.ExecuteMethod<TypificationResponse>(
                () =>
                {
                    return _oServiceCommon.GetTypification(objTypificationRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            if (!string.IsNullOrEmpty(strType))
            {
                objTypificationResponse.ListTypification = objTypificationResponse.ListTypification
                    .Where(y => y.TIPO == strType).ToList();
            }
            return Json(objTypificationResponse, JsonRequestBehavior.AllowGet);
        }
        //        public static List<string> GetXmlToString(string filename)
        //        {

        //            try
        //            {
        //                XDocument xdoc = XDocument.Load(filename);
        //                List<string> listadios = (from o in xdoc.Root.Descendants()
        //                                          select o.Name.ToString()).ToList();

        //                return listadios;

        //            }
        //            catch (Exception)
        //            {
        //                return null;
        //                throw;
        //            }
        //        }
        //        public JsonResult ETAValidation(string strIdSession, string vstrTipTra, string vstrIdPLano)
        //        {
        //            Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_Parameters strIdSession: " + strIdSession + " vstrTipTra: " + vstrTipTra + " vstrIdPLano: " + vstrIdPLano);
        //            string v_TipoOrden;
        //            char[] constantToSplit = new char[] { '.', '|' };
        //            Helpers.CommonServices.GenericItem objGenericItem = new Helpers.CommonServices.GenericItem();
        //            OrderTypesRequestHfc objOrderTypesRequest = new OrderTypesRequestHfc()
        //            {
        //                audit = APPCODE.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession),
        //                vIdtiptra = (vstrTipTra.IndexOf(".|") == Claro.Constants.NumberOneNegative ? vstrTipTra : vstrTipTra.Split(constantToSplit)[0])
        //            };
        //            Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_objOrderTypesRequest : " + Newtonsoft.Json.JsonConvert.SerializeObject(objOrderTypesRequest));
        //            try
        //            {
        //                OrderTypesResponseHfc objOrderTypesResponse = Logging.ExecuteMethod<OrderTypesResponseHfc>(() => { return _oServiceFixed.GetOrderType(objOrderTypesRequest); });

        //                Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_objOrderTypesResponse : " + Newtonsoft.Json.JsonConvert.SerializeObject(objOrderTypesResponse));


        //                if (objOrderTypesResponse.ordertypes.Count == 0)
        //                    v_TipoOrden = SIACU.Transac.Service.Constants.strMenosUno;
        //                else
        //                    v_TipoOrden = objOrderTypesResponse.ordertypes[0].VALOR;

        //                ETAFlowRequestHfc oEtaRequest = new ETAFlowRequestHfc()
        //                {
        //                    audit = APPCODE.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession),
        //                    an_tipsrv = ConfigurationManager.AppSettings("gConstHFCTipoServicio"),
        //                    an_tiptra = (vstrTipTra.IndexOf(".|") == Claro.Constants.NumberOneNegative ? Int32.Parse(vstrTipTra) : Int32.Parse(vstrTipTra.Split(constantToSplit)[0])),
        //                    as_origen = ConfigurationManager.AppSettings("gConstHFCOrigen"),
        //                    av_idplano = vstrIdPLano,
        //                    av_ubigeo = String.Empty
        //                };
        //                Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_oEtaRequest : " + Newtonsoft.Json.JsonConvert.SerializeObject(oEtaRequest));

        //                ETAFlowResponseHfc oEtaResponse = new ETAFlowResponseHfc();
        //                oEtaResponse = Logging.ExecuteMethod<ETAFlowResponseHfc>(() =>
        //                {
        //                    return new FixedIFIServiceClient().ETAFlowValidate(oEtaRequest);
        //                });
        //                Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_oEtaResponse : " + Newtonsoft.Json.JsonConvert.SerializeObject(oEtaResponse));

        //                //objGenericItem.Condition = objETAFlowResponse.rResult;
        //                objGenericItem.Code = Claro.Utils.CheckStr(oEtaResponse.ETAFlow.an_indica);
        //                objGenericItem.Code2 = oEtaResponse.ETAFlow.as_codzona + "|" + vstrIdPLano + "|" + v_TipoOrden;
        //                Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_apuntodeentraralswitch");

        //                switch (oEtaResponse.ETAFlow.an_indica)
        //                {
        //                    case -1:
        //                        objGenericItem.Description = Claro.Utils.GetValueFromConfigFile("strMsgNoExistePlano", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")); break;
        //                    case -2:
        //                        objGenericItem.Description = Claro.Utils.GetValueFromConfigFile("strMsgNoExisteUbigeo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")); break;
        //                    case -3:
        //                        objGenericItem.Description = Claro.Utils.GetValueFromConfigFile("strMsgNoExistePlanoUbigeo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")); break;
        //                    case -4:
        //                        objGenericItem.Description = Claro.Utils.GetValueFromConfigFile("strMsgNoExisteTipoTrabajo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")); break;
        //                    case -5:
        //                        objGenericItem.Description = Claro.Utils.GetValueFromConfigFile("strMsgNoExisteTipoServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")); break;
        //                    case 1:
        //                        objGenericItem.Description = CSTS.Constants.DAReclamDatosVariable_OK; break;
        //                    case 0:
        //                        objGenericItem.Description = CSTS.Constants.DAReclamDatosVariableNO_OK; break;
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_Exception: " + Newtonsoft.Json.JsonConvert.SerializeObject(ex));

        //                Logging.Error(strIdSession, objOrderTypesRequest.audit.transaction, ex.Message);
        //                throw new Exception(objOrderTypesRequest.audit.transaction);
        //            }
        //            Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_objGenericItem: " + Newtonsoft.Json.JsonConvert.SerializeObject(objGenericItem));

        //            return Json(new { data = objGenericItem });
        //        }
        //        public JsonResult GetTimeZones(string strIdSession, TimeZoneVM objTimeZoneVM)
        //        {
        //            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
        //            ArrayList lstGenericItem = new ArrayList();
        //            FixedIFIService.GenericItem objGenericItem = new FixedIFIService.GenericItem();

        //            objGenericItem.Descripcion = Claro.Utils.GetValueFromConfigFile("strSeleccionar", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
        //            objGenericItem.Codigo = SIACU.Transac.Service.Constants.PresentationLayer.NumeracionMENOSUNO;

        //            try
        //            {
        //                if (objTimeZoneVM.vValidateETA == SIACU.Transac.Service.Constants.strUno)
        //                {
        //                    lstGenericItem = GetTimeZones(objTimeZoneVM, strIdSession);
        //                }
        //                else
        //                {
        //                    if (objTimeZoneVM.vJobTypes != null)
        //                    {
        //                        if (objTimeZoneVM.vJobTypes.Contains(".|"))
        //                        {
        //                            if (objTimeZoneVM.vJobTypes != KEY.AppSettings("TipoTrabajo_HFC_RETENCION"))
        //                            {

        //                                TimeZonesResponseHfc objFranjasHorariasResponseHfc = new TimeZonesResponseHfc();
        //                                TimeZonesRequestHfc objFranjasHorariasRequestHfc = new TimeZonesRequestHfc();

        //                                FixedIFIService.AuditRequest auditFixed = APPCODE.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);

        //                                try
        //                                {
        //                                    objFranjasHorariasRequestHfc.audit = auditFixed;
        //                                    objFranjasHorariasRequestHfc.strAnTiptra = objTimeZoneVM.vJobTypes.Substring(0, objTimeZoneVM.vJobTypes.Length - 2);
        //                                    objFranjasHorariasRequestHfc.strAnUbigeo = objTimeZoneVM.vUbigeo;
        //                                    objFranjasHorariasRequestHfc.strAdFecagenda = objTimeZoneVM.vCommitmentDate;

        //                                    objFranjasHorariasResponseHfc = Logging.ExecuteMethod<TimeZonesResponseHfc>(() =>
        //                                    {
        //                                        return _oServiceFixed.GetTimeZones(objFranjasHorariasRequestHfc);
        //                                    });



        //                                    foreach (FixedIFIService.TimeZone _item in objFranjasHorariasResponseHfc.TimeZones)
        //                                    {
        //                                        objGenericItem = new FixedIFIService.GenericItem();

        //                                        objGenericItem.Agrupador = _item.TIPTRA;
        //                                        objGenericItem.Codigo = _item.CODCON;
        //                                        objGenericItem.Codigo2 = _item.CODCUADRILLA;
        //                                        objGenericItem.Descripcion = _item.HORA;
        //                                        objGenericItem.Estado = _item.COLOR;

        //                                        lstGenericItem.Add(objGenericItem);
        //                                    }
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    Logging.Error(objFranjasHorariasRequestHfc.audit.Session, objFranjasHorariasRequestHfc.audit.transaction, ex.Message);
        //                                    throw new Exception(auditFixed.transaction);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                lstGenericItem = Common.GetXMLList("FranjasHorariasXML");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            lstGenericItem = Common.GetXMLList("FranjasHorariasXML");

        //                        }
        //                    }
        //                    else
        //                    {
        //                        lstGenericItem = Common.GetXMLList("FranjasHorariasXML");
        //                    }

        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, audit.transaction, ex.Message);

        //            }

        //            return Json(new { data = lstGenericItem });
        //        }

        //        private ArrayList GetTimeZones(TimeZoneVM objTimeZoneVM, string strIdSession = "")
        //        {
        //            ArrayList Items = new ArrayList();
        //            string idTran, ipApp, nomAp, usrAp;
        //            try
        //            {
        //                idTran = Common.GetTransactionID();
        //                ipApp = Common.GetApplicationIp();
        //                nomAp = KEY.AppSettings("NombreAplicacion");
        //                usrAp = Common.CurrentUser;

        //                DateTime dInitialDate = Convert.ToDate(objTimeZoneVM.vCommitmentDate);

        //                int fID = Convert.ToInt(Claro.Utils.GetValueFromConfigFile("strDiasConsultaCapacidad", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")
        //                    ));
        //                DateTime[] dDate = new DateTime[fID];

        //                dDate[0] = dInitialDate;

        //                for (int i = 0; i < fID; i++)
        //                {
        //                    dInitialDate = dInitialDate.AddDays(1);
        //                    dDate[i] = dInitialDate;
        //                }

        //                Boolean vExistSesion = false;
        //                string strUbicacion = Claro.Utils.GetValueFromConfigFile("strCodigoUbicacion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")
        //                    );
        //                string[] vUbicaciones = { strUbicacion };
        //                Boolean v1, v2, v3, v4, v5, v6, v7, v8;

        //                v1 = System.Convert.ToBoolean(Int32.Parse(Claro.Utils.GetValueFromConfigFile("strCalcDuracion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));
        //                v2 = System.Convert.ToBoolean(Int32.Parse(Claro.Utils.GetValueFromConfigFile("strCalcDuracionEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));
        //                v3 = System.Convert.ToBoolean(Int32.Parse(
        //                    Claro.Utils.GetValueFromConfigFile("strCalcTiempoViaje", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));
        //                v4 = System.Convert.ToBoolean(Int32.Parse(Claro.Utils.GetValueFromConfigFile("strCalcTiempoViajeEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));
        //                v5 = System.Convert.ToBoolean(Int32.Parse(Claro.Utils.GetValueFromConfigFile("strCalcHabTrabajo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));
        //                v6 = System.Convert.ToBoolean(Int32.Parse(Claro.Utils.GetValueFromConfigFile("strCalcHabTrabajoEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));
        //                v7 = System.Convert.ToBoolean(Int32.Parse(Claro.Utils.GetValueFromConfigFile("strObtenerZonaUbi", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")
        //                    )));
        //                v8 = System.Convert.ToBoolean(Int32.Parse(Claro.Utils.GetValueFromConfigFile("strObtenerZonaUbiEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));


        //                String vHabTra = String.Empty;
        //                vHabTra = Claro.Utils.GetValueFromConfigFile("strCodigoHabilidad", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));

        //                string[] vEspacioTiempo = { string.Empty };
        //                string[] HabilidadTrabajo = { vHabTra };

        //                //    Dim vespacioTiempo As String() = {String.Empty}
        //                //Dim vhabilidtrab As String() = {vstrHabTra}


        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }



        //            return Items;
        //        }

        //        [HttpPost]
        //        public JsonResult GetCustomerPhoneCommon(string strIdSession, string intIdContract, string strTypeProduct)
        //        {
        //            var objAuditRequest = APPCODE.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
        //            var objRequest = new ConsultationServiceByContractRequest()
        //            {
        //                audit = objAuditRequest,
        //                strCodContrato = intIdContract,
        //                typeProduct = strTypeProduct
        //            };

        //            var objCustomerPhoneResponse = Logging.ExecuteMethod(() =>
        //            {
        //                return _oServiceFixed.GetCustomerLineNumber(objRequest);
        //            });
        //            Logging.Info("IdSession: " + strIdSession, "Metodo: " + "GetCustomerPhone", objCustomerPhoneResponse.msisdn);
        //            var phone = objCustomerPhoneResponse.msisdn;
        //            return Json(phone, JsonRequestBehavior.AllowGet);
        //        }

        //        #endregion

        //        public List<Helpers.CommonServices.GenericItem> GetProgramTask(string sessionId, string transactionId, string strIdLista)
        //        {
        //            var objListViewModel = new List<Helpers.CommonServices.GenericItem>();
        //            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(sessionId);

        //            var objRequest = new ProgramTaskRequest
        //            {
        //                audit = audit,
        //                SessionId = sessionId,
        //                TransactionId = transactionId,
        //                StrIdLista = strIdLista
        //            };

        //            var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetProgramTask(objRequest));

        //            if (objResponse != null)
        //            {
        //                var lstTemp = objResponse.ListProgramTask;
        //                if (lstTemp.Count > 0)
        //                {
        //                    foreach (var item in lstTemp)
        //                    {
        //                        var objViewModel = new Helpers.CommonServices.GenericItem
        //                        {
        //                            Code = item.Codigo,
        //                            Description = item.Descripcion
        //                        };
        //                        objListViewModel.Add(objViewModel);
        //                    }
        //                }
        //            }

        //            return objListViewModel;
        //        }

        //        public List<Helpers.CommonServices.GenericItem> GetTypeTransaction(string sessionId, string transactionId)
        //        {
        //            var objListViewModel = new List<Helpers.CommonServices.GenericItem>();
        //            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(sessionId);

        //            var objRequest = new TypeTransactionRequest
        //            {
        //                audit = audit,
        //                SessionId = sessionId,
        //                TransactionId = transactionId
        //            };

        //            var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetTypeTransaction(objRequest));

        //            if (objResponse != null)
        //            {
        //                var lstTemp = objResponse.TypeTransaction;
        //                if (lstTemp.Count > 0)
        //                {
        //                    foreach (var item in lstTemp)
        //                    {
        //                        var objViewModel = new Helpers.CommonServices.GenericItem
        //                        {
        //                            Code = item.Codigo,
        //                            Description = item.Descripcion
        //                        };
        //                        objListViewModel.Add(objViewModel);
        //                    }
        //                }
        //            }

        //            return objListViewModel;
        //        }

        public Dictionary<string, object> InsertInteraction(InteractionModel objInteractionModel, TemplateInteractionModel oPlantillaDat, string strNroTelephone, string strUserSession, string strUserAplication, string strPassUser, bool boolEjecutTransaction, string strIdSession, string strCustomerId)
        {
            string contingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");

            var strTelefono = strNroTelephone == objInteractionModel.Telephone ? strNroTelephone : objInteractionModel.Telephone;

            //Obtener Cliente
            string strFlgRegistrado = Claro.Constants.NumberOneString;
            CustomerResponse objCustomerResponse;
            AuditRequestFixed audit = APPCODE.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = strTelefono,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };
            objCustomerResponse = Logging.ExecuteMethod(() =>
            {
                return _oServiceFixed.GetCustomer(objGetCustomerRequest);
            });

            if (objCustomerResponse.Customer != null)
            {
                objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
            }

            //Validacion de Contingencia
            Dictionary<string, string> result;
            if (contingenciaClarify != Claro.SIACU.Constants.OK)
            {
                result = GetInsertInteractionCLFY(objInteractionModel, strIdSession);
            }
            else
            {
                result = GetInsertContingencyInteraction(objInteractionModel, strIdSession);
            }


            var rInteraccionId = result.FirstOrDefault().Value;

            var dictionaryResponse = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(rInteraccionId))
            {
                if (oPlantillaDat != null)
                {
                    dictionaryResponse = InsertPlantInteraction(oPlantillaDat, rInteraccionId, strNroTelephone, strUserSession, strUserAplication, strPassUser, boolEjecutTransaction, strIdSession);
                }
            }
            dictionaryResponse["rInteraccionId"] = rInteraccionId;
            dictionaryResponse["strFlagInsertion"] = result.SingleOrDefault(y => y.Key.Equals("rFlagInsercion")).Value;
            dictionaryResponse["strFlagInsertionInteraction"] = dictionaryResponse.SingleOrDefault(y => y.Key.Equals("FlagInsercion")).Value;
            return dictionaryResponse;
        }

        //        public void ExecuteUpdateInter30(string strIdSession, string strTransaction, string strInteractionId, string pText)
        //        {
        //            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

        //            UpdatexInter30Request objUpdateInter30Request = new UpdatexInter30Request
        //            {
        //                audit = audit,
        //                p_objid = strInteractionId,
        //                p_texto = pText
        //            };

        //            Logging.ExecuteMethod(() => { return _oServiceCommon.GetUpdatexInter30(objUpdateInter30Request); });
        //        }


        #region metodos para leer constancia via ftp
        //public JsonResult ExistFile(string strFilePath, string strIdSession)
        //{
        //    CommonService.AuditRequest objAudit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
        //    bool ExistFile = false;
        //    byte[] byteDataFile;
        //    Logging.Info(objAudit.Session, objAudit.transaction, "ExistFile: Ruta = " + strFilePath);
        //    try
        //    {
        //        ExistFile = DisplayFileFromServer(strIdSession, objAudit.transaction, strFilePath, out byteDataFile);
        //        Logging.Info(objAudit.Session, objAudit.transaction, "ExistFile: Salida de Existencia = " + ExistFile.ToString());
        //        byteDataFile = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExistFile = false;

        //        Logging.Error(strIdSession, objAudit.transaction, Claro.Utils.GetExceptionMessage(ex));
        //    }
        //    return Json(new { Exist = ExistFile }, JsonRequestBehavior.AllowGet);
        //}

        //        public FileContentResult ShowRecord(string strIdSession, string strFilePath)
        //        {
        //            CommonService.AuditRequest objAudit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
        //            byte[] databytes;
        //            string strContenType = string.Empty;
        //            try
        //            {
        //                string strExtentionFile = Path.GetExtension(strFilePath);
        //                strContenType = Functions.f_obtieneContentType(strExtentionFile);
        //                bool dt = false;
        //                dt = DisplayFileFromServer(strIdSession, objAudit.transaction, strFilePath, out databytes);
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, objAudit.transaction, Functions.GetExceptionMessage(ex));
        //                databytes = null;
        //            }

        //            return File(databytes, strContenType);

        //        }


        //public bool DisplayFileFromServer(string strIdSession, string strTransaction, string strPath, out byte[] mydata)
        //{
        //    mydata = null;
        //    try
        //    {
        //        string doc = "//172.19.112.75/";
        //        string strFullPath = "ftp:" + doc;
        //        string user = ConfigurationManager.AppSettings("userftp");
        //        string pass = ConfigurationManager.AppSettings("passftp");
        //        Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: User: " + user);
        //        Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Pass: " + pass);
        //        FtpWebRequest sftpRequest = (FtpWebRequest)WebRequest.Create(new Uri(strFullPath));
        //        ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
        //        sftpRequest.Credentials = new NetworkCredential(user, pass);
        //        sftpRequest.KeepAlive = false;
        //        sftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
        //        sftpRequest.UseBinary = true;
        //        sftpRequest.Proxy = null;
        //        sftpRequest.UsePassive = false;
        //        sftpRequest.EnableSsl = true;
        //        Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Lectura de path: " + sftpRequest.ToString());
        //        FtpWebResponse response = (FtpWebResponse)sftpRequest.GetResponse();
        //        Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response: " + sftpRequest.ToString());
        //        Stream responseStream = response.GetResponseStream();
        //        Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response Stream: " + responseStream.ToString());
        //        StreamReader reader = new StreamReader(responseStream);
        //        BinaryReader br = new BinaryReader(responseStream);
        //        Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response BinaryReader: " + br.ToString());
        //        FileInfo fiInfo = new FileInfo("/SIACUNICO/HFC/DETALLE_LLAMADAS_ENTRANTES/1508709011_04_08_2017_DETALLE_LLAMADAS_ENTRANTES_0.pdf");
        //        Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response FileInfo: " + fiInfo.ToString());
        //        FileStream fs = new FileStream(fiInfo.ToString(), FileMode.Open);
        //        Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response FileStream: " + fs.ToString());
        //        mydata = br.ReadBytes(Convert.ToInt((fs.Length - 1)));
        //        Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response mydata: " + mydata.ToString());
        //        reader.Close();
        //        response.Close();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));
        //        return false;
        //    }



        //    //Uri myurl = new Uri(strFullPath);
        //    //if (myurl.Scheme != Uri.UriSchemeFtp)
        //    //{
        //    //    mydata = null;
        //    //    return false;
        //    //}

        //    //try
        //    //{


        //    //    WebClient request = new WebClient();
        //    //    request.Credentials = new NetworkCredential(user, pass);
        //    //    mydata = request.DownloadData(myurl.ToString());
        //    //    return true;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Claro.Web.Logging.Error(strIdSession, strTransaction, CSTS.Functions.GetExceptionMessage(ex));

        //    //    mydata = null;
        //    //    return false;
        //    //}
        //}

        //        #endregion

        //        #region metodos para leer constancia via carpeta compartida

        public JsonResult ExistFileSharedFile(string strFilePath, string strIdSession)
        {
            CommonService.AuditRequest objAudit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            bool ExistFile = false;
            byte[] byteDataFile;
            Logging.Info(objAudit.Session, objAudit.transaction, "ExistFileSharedFile: Ruta = " + strFilePath);
            try
            {
                ExistFile = DisplayFileFromServerSharedFile(strIdSession, objAudit.transaction, strFilePath, out byteDataFile);
                Logging.Info(objAudit.Session, objAudit.transaction, "ExistFileSharedFile: Salida de Existencia = " + ExistFile.ToString());
                byteDataFile = null;
            }
            catch (Exception ex)
            {
                ExistFile = false;

                Logging.Error(strIdSession, objAudit.transaction, Claro.Utils.GetExceptionMessage(ex));
            }
            return Json(new { Exist = ExistFile }, JsonRequestBehavior.AllowGet);
        }

        public FileContentResult ShowRecordSharedFile(string strIdSession, string strFilePath)
        {
            CommonService.AuditRequest objAudit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            byte[] databytes;
            string strContenType = string.Empty;
            try
            {
                string strExtentionFile = Path.GetExtension(strFilePath);
                strContenType = Claro.Utils.f_obtieneContentType(strExtentionFile);
                bool dt = false;
                dt = DisplayFileFromServerSharedFile(strIdSession, objAudit.transaction, strFilePath, out databytes);
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objAudit.transaction, Claro.Utils.GetExceptionMessage(ex));
                databytes = null;
            }

            return File(databytes, strContenType);
        }

        public bool DisplayFileFromServerSharedFile(string strIdSession, string strTransaction, string strPath, out byte[] mydata)
        {
            bool blnResult;
            FileInfo fiInfo = null;
            FileStream fs = null;
            BinaryReader br = null;
            mydata = null;
            Siacu_Impersonation obj = new Siacu_Impersonation();
            try
            {
                string user = string.Empty;
                string pass = string.Empty;
                string domain = string.Empty;
                bool credentials = false;
                credentials = Get_Credentials(strIdSession, strTransaction, ConfigurationManager.AppSettings("ConexionSamba"), out user, out pass, out domain);
                if (obj.impersonateValidUser(user, domain, pass))
                {
                    fiInfo = new FileInfo(strPath);
                    fs = new FileStream(fiInfo.ToString(), FileMode.Open);
                    br = new BinaryReader(fs);
                    mydata = br.ReadBytes(Convert.ToInt((fs.Length - 1)));
                    Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response mydata: " + mydata.ToString());
                    Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Suplantación OK");
                    blnResult = true;
                }
                else
                {
                    Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Error de Suplantación ");
                    blnResult = false;
                }

                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (br != null)
                {
                    br.Close();
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));

                mydata = null;
                blnResult = false;
            }

            obj.undoImpersonatiom();

            return blnResult;
        }

        public bool Get_Credentials(string strIdSession, string strTransaction, string strConexion, out string struser, out string strpass, out string strdomain)
        {
            try
            {
                ConfigurationRecord ConfigurationCredentials = new ConfigurationRecord(strConexion);
                struser = ConfigurationCredentials.LeerUsuario();

                strpass = ConfigurationCredentials.LeerContrasena();

                strdomain = ConfigurationCredentials.LeerBaseDatos();


                return true;
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));

                struser = string.Empty;
                strpass = string.Empty;
                strdomain = string.Empty;
                return false;
            }
        }

        //        #endregion
        //        public JsonResult SaveAuditMJson(string strTransaction, string strService, string strText, string strTelephone, string strNameCustomer, string strIdSession, string strIpCustomer = "", string strCuentUser = "", string strMontoInput = "0")
        //        {
        //            var strMonto = strMontoInput == "0" ? ConstantsHFC.strCero : strMontoInput;
        //            var strNameServidor = Request.ServerVariables["SERVER_NAME"];
        //            var strIpServidor = Request.ServerVariables["LOCAL_ADDR"];

        //            SaveAuditMResponseCommon objRegAuditResponseMCommon = null;
        //            CommonIFIService.AuditRequest audit =
        //                APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
        //            SaveAuditMRequestCommon objRegAuditRequestMCommon =
        //                new SaveAuditMRequestCommon()
        //                {
        //                    audit = audit,
        //                    vTransaccion = strTransaction,
        //                    vServicio = strService,
        //                    vIpCliente = strIpCustomer,
        //                    vNombreCliente = strNameCustomer,
        //                    vIpServidor = strIpServidor,
        //                    vNombreServidor = strNameServidor,
        //                    vCuentaUsuario = strCuentUser,
        //                    vTelefono = strTelephone,
        //                    vMonto = strMonto,
        //                    vTexto = strText
        //                };

        //            try
        //            {
        //                objRegAuditResponseMCommon = Logging.ExecuteMethod<SaveAuditMResponseCommon>(
        //                    () =>
        //                    {
        //                        return _oServiceCommon.SaveAuditM(objRegAuditRequestMCommon);
        //                    });
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, audit.transaction, ex.Message);
        //                throw new Exception(ex.Message);
        //            }

        //            var result = objRegAuditResponseMCommon.respuesta;

        //            return Json(result, JsonRequestBehavior.AllowGet);
        //        }
        //        public void UpdateXInter29(string strIdSession, string strInteractionId, string strText, string strOrder)
        //        {
        //            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
        //            UpdateXInter29RequestCommon objUpdateXInter29Request = new UpdateXInter29RequestCommon
        //            {
        //                audit = audit,
        //                IdInteract = strInteractionId,
        //                Text = strText,
        //                Order = strOrder
        //            };

        //            Logging.ExecuteMethod(() => { return _oServiceCommon.UpdateXInter29(objUpdateXInter29Request); });
        //        }

        //        public static void LogException(string strIdSession, string strTransaction, string text, Exception error)
        //        {
        //            Exception realerror = error;
        //            while (realerror.InnerException != null)
        //                realerror = realerror.InnerException;
        //            Logging.Error(strIdSession, strTransaction, text + realerror.Message);
        //        }

        //        public string DownloandFilePDF(string strIdSession, string strPath, string strNameFile)
        //        {
        //            byte[] buffer;
        //            bool ExistFile = false;
        //            var RutaArchivoConstPDF = ConfigurationManager.AppSettings("strRutaArchivoConstPDF");
        //            var PathServer = System.Web.HttpContext.Current.Server.MapPath(RutaArchivoConstPDF);
        //            string PathFileEmail = PathServer + "\\" + strNameFile;
        //            DirectoryInfo di = null;

        //            CommonService.AuditRequest objAudit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
        //            Logging.Info(strIdSession, objAudit.transaction, "Begin a DownloandFilePDF"); // Temporal
        //            try
        //            {
        //                ExistFile = DisplayFileFromServerSharedFile(strIdSession, objAudit.transaction, strPath, out buffer);
        //                Logging.Info(strIdSession, objAudit.transaction, "DownloandFilePDF -> Obtiene en byte: " + ExistFile.ToString()); // Temporal
        //                if (ExistFile)
        //                {
        //                    //Verifica si existe la carpeta FileEmail
        //                    Logging.Info(strIdSession, objAudit.transaction, "DownloandFilePDF -> Creando la carpeta: " + PathServer); // Temporal
        //                    if (!Directory.Exists(PathServer))
        //                        di = Directory.CreateDirectory(PathServer);

        //                    System.IO.File.WriteAllBytes(PathFileEmail, buffer);
        //                }
        //                else
        //                {
        //                    PathFileEmail = string.Empty;
        //                }
        //                Logging.Info(strIdSession, objAudit.transaction, "DownloandFilePDF -> Descargado el archivo: " + PathFileEmail); // Temporal
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, objAudit.transaction, ex.Message);
        //                PathFileEmail = string.Empty;
        //                return PathFileEmail;
        //            }
        //            Logging.Info(strIdSession, objAudit.transaction, "End a DownloandFilePDF"); // Temporal
        //            return PathFileEmail;
        //        }
        //        public bool DeleteFilePDF(string strIdSession, string strPathFileLocal)
        //        {
        //            bool respuesta = false;
        //            CommonService.AuditRequest objAudit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
        //            Logging.Info(strIdSession, objAudit.transaction, "Begin a DeleteFilePDF"); // Temporal
        //            try
        //            {
        //                Logging.Info(strIdSession, objAudit.transaction, "DeleteFilePDF -> Parametro entrada strPathFileLocal: " + strPathFileLocal); // Temporal
        //                if (System.IO.File.Exists(strPathFileLocal))
        //                {
        //                    System.IO.File.Delete(strPathFileLocal);
        //                    respuesta = true;
        //                    Logging.Info(strIdSession, objAudit.transaction, "DeleteFilePDF -> Archivo Eliminado respuesta: " + respuesta.ToString()); // Temporal
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, objAudit.transaction, ex.Message);
        //                throw new Exception(objAudit.transaction);
        //            }
        //            Logging.Info(strIdSession, objAudit.transaction, "End a DeleteFilePDF"); // Temporal
        //            return respuesta;
        //        }

        //        public ConsultIGVModel GetCommonConsultIgv(string strIdSession)
        //        {
        //            var oConsultIgv = new ConsultIGVModel();

        //            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

        //            var objRequest = new ConsultIGVRequest
        //            {
        //                audit = audit,
        //                SessionId = strIdSession,
        //                TransactionId = audit.transaction,
        //                AppId = audit.ipAddress,
        //                AppName = audit.applicationName,
        //                Username = audit.userName
        //            };

        //            try
        //            {
        //                var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetConsultIGV(objRequest));

        //                if (objResponse != null && objResponse.ListConsultIGV.Count > 0)
        //                {
        //                    DateTime currentDate = DateTime.Now;
        //                    foreach (var item in objResponse.ListConsultIGV)
        //                    {
        //                        if (Convert.ToDate(item.impudFecIniVigencia) <= currentDate &&
        //                            Convert.ToDate(item.impudFecFinVigencia) >= currentDate &&
        //                            item.impunTipDoc == Constant.strCero)
        //                        {
        //                            oConsultIgv = new ConsultIGVModel
        //                            {
        //                                igv = item.igv,
        //                                igvD = item.igvD,
        //                                impudFecFinVigencia = item.impudFecFinVigencia,
        //                                impudFecIniVigencia = item.impudFecIniVigencia,
        //                                impudFecRegistro = item.impudFecRegistro,
        //                                impunTipDoc = item.impunTipDoc,
        //                                imputId = item.imputId,
        //                                impuvDes = item.impuvDes
        //                            };
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
        //            }

        //            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + objRequest.audit.transaction, "End a GetConsultIGV Controller => oConsultIGV.igv: " + oConsultIgv.igv.ToString()); // Temporal
        //            return oConsultIgv;
        //        }


        public Claro.Utils.ItemGeneric GetInteractIDforCaseID(string strIdSession, string strCasoID)
        {
            Claro.Utils.ItemGeneric oItem = new Claro.Utils.ItemGeneric();
            CaseInsertResponse objResponse = new CaseInsertResponse();
            FixedIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            Logging.Info(audit.Session, audit.transaction, "GetInteractIDforCaseID IN | strCasoID: " + strCasoID);
            var objRequest = new CaseInsertRequest
            {
                audit = audit,
                ID_CASO = strCasoID
            };
            try
            {
                objResponse = Logging.ExecuteMethod(() => _oServiceFixed.GetInteractIDforCaseID(objRequest));
                oItem.Code = objResponse.rCasoId;
                oItem.Code2 = objResponse.rFlagInsercion;
                oItem.Description = objResponse.rMsgText;

                Logging.Info(audit.Session, audit.transaction, "GetInteractIDforCaseID OUT | Interaccio: " + oItem.Code);
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
            }

            return oItem;
        }

        //        public bool RegisterAudit(string strIdSession, string strTransaction, string strTransactionAudit,
        //           string strService, string strIpClient, string strNameClient, string strIpServer, string strNameServer,
        //           string strAccountUser, string strPhoneNumber, string strAmount, string strText)
        //        {
        //            bool blnRespose = false;
        //            CommonIFIService.AuditRequest audit = App_Code.APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
        //            SaveAuditResponseCommon saveAuditResponse = new SaveAuditResponseCommon();

        //            try
        //            {
        //                SaveAuditRequestCommon objRequest = new SaveAuditRequestCommon()
        //                {
        //                    vTransaccion = strTransactionAudit,
        //                    vServicio = strService,
        //                    vIpCliente = strIpClient,
        //                    vNombreCliente = strNameClient,
        //                    vIpServidor = strIpServer,
        //                    vNombreServidor = strNameServer,
        //                    vCuentaUsuario = strAccountUser,
        //                    vTelefono = strPhoneNumber,
        //                    vMonto = strAmount,
        //                    vTexto = strText,
        //                    audit = audit
        //                };

        //                saveAuditResponse = Logging.ExecuteMethod(() =>
        //                {
        //                    return _oServiceCommon.SaveAudit(objRequest);
        //                });

        //                if (saveAuditResponse.vestado == Claro.Constants.NumberZeroString)
        //                {
        //                    blnRespose = false;
        //                }
        //                else
        //                {
        //                    blnRespose = true;
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                Logging.Error(strIdSession, strTransaction, ex.Message);
        //                Logging.Info("Session: " + strIdSession, "Transaction: RegisterAudit AdditionalServices HFC", string.Format("Error: {0}", ex.Message));
        //            }

        //            return blnRespose;
        //        }


        public CommonService.Typification LoadTypifications(string strIdSession, string typeProduct, ref string lblMensaje)
        {
            Claro.Web.Logging.Configure();

            CommonService.Typification oTypification = new CommonService.Typification();
            CommonService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);

            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Metodo LoadTypification");

            try
            {
                CommonService.TypificationRequest objTypificationRequest = new CommonIFIService.TypificationRequest();
                objTypificationRequest.TRANSACTION_NAME = typeProduct;
                objTypificationRequest.audit = audit;

                var objResponse =
                Claro.Web.Logging.ExecuteMethod<CommonService.TypificationResponse>(
                    () => { return _oServiceCommon.GetTypification(objTypificationRequest); });



                oTypification = objResponse.ListTypification.First();

                if (oTypification == null)
                {
                    lblMensaje = Claro.Utils.GetValueFromConfigFile("strAjusteNoRecon", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                lblMensaje = Claro.Utils.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
            }

            return oTypification;
        }

        #region EMAIL
        public string GetSendEmailConst(Model.SendEmailModel objSendEmail)
        {
            CommonIFIService.SendEmailResponseCommon objGetSendEmailResponse = new CommonIFIService.SendEmailResponseCommon();
            CommonIFIService.AuditRequest AuditRequest = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objSendEmail.strIdSession);
            CommonIFIService.SendEmailRequestCommon objGetSendEmailRequest =
                new CommonIFIService.SendEmailRequestCommon()
                {
                    audit = AuditRequest,
                    strSender = ConfigurationManager.AppSettings("CorreoServicioAlCliente"),
                    strTo = objSendEmail.strTo,
                    strSubject = objSendEmail.strSubject,
                    strMessage = TemplateEmail(objSendEmail.strMessage, objSendEmail.strMsgEmailCall),
                    strAttached = objSendEmail.strAttached,
                    AttachedByte = objSendEmail.byteAttached
                };
            try
            {
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailResponseCommon>(() => { return _oServiceCommon.GetSendEmailFixed(objGetSendEmailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSendEmail.strIdSession, objGetSendEmailRequest.audit.transaction, ex.Message);
                throw new Exception(AuditRequest.transaction);
            }

            string strResul = string.Empty;

            if (objGetSendEmailResponse.Exit == Claro.SIACU.Constants.OK)
            {
                strResul = Claro.Utils.GetValueFromConfigFile("strMensajeEnvioOK", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
            }
            else
            {
                strResul = Claro.Utils.GetValueFromConfigFile("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
            }

            return strResul;
        }

        public string TemplateEmail(string strMessage, string strMsgEmailCall)
        {
            string strmessage = string.Empty;
            var strHtml = new System.Text.StringBuilder();

            strHtml.Append("<html>");
            strHtml.Append("<head>");
            strHtml.Append("<style type='text/css'>");
            strHtml.Append("<!--");
            strHtml.Append(".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}");
            strHtml.Append(".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}");
            strHtml.Append("-->");
            strHtml.Append("</style>");
            strHtml.Append("<body>");
            strHtml.Append("<table width='100%' border='0' cellpadding='0' cellspacing='0'>");
            strHtml.Append("<tr><td width='180' class='Estilo1' height='22'>Estimado Cliente, </td></tr>");

            strHtml.Append("<tr><td width='180' class='Estilo1' height='22'>");
            strHtml.Append(strMessage);
            strHtml.Append("</td></tr>");

            strHtml.Append("<tr>");
            strHtml.Append("<td align='center'>");
            strHtml.Append("</td></tr>");

            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td class='Estilo1'>&nbsp;</td></tr>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td class='Estilo1'>Cordialmente</td></tr>");
            strHtml.Append("<tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFile("strMsgEmailFirma", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>");
            strHtml.Append("<tr><td class='Estilo1'>Atención al Cliente</td></tr>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append(strMsgEmailCall);
            strHtml.Append("</table>");
            strHtml.Append("</body>");
            strHtml.Append("</html>");

            return strHtml.ToString();

        }
        #endregion
        #endregion
        #endregion
    }
}
