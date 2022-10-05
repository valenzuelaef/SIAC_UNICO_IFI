using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Security.Principal;
using System.Xml.Linq;
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
using FIXED = Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using Claro.Web;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;
using System.DirectoryServices;
using System.Collections;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class CommonServicesController : Controller
    {
        private readonly Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.CommonIFIServiceClient _oServiceCommon = new Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.CommonIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient _oServiceFixed = new Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient();

        private readonly Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient _oServicePostpaid =
         new Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient();

        ///<summary>Metodo que permite mapear los valores</summary>
        ///<returns><returns>
        ///<remarks>CommonServicesController</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public CommonServicesController()
        {
            Mappings.AutoMapperConfig.RegisterMappings();
        }


        ///<summary>Metodo que obtiene el termina actual</summary>
        ///<returns>string<returns>
        ///<remarks>CurrentTerminal</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        public string CurrentTerminal()
        {
            return System.Web.HttpContext.Current.Request.UserHostAddress;
        }


        ///<summary>Metodo que nombre del host del cliente</summary>
        /// <param name="strIpClient"></param>
        ///<returns>string<returns>
        ///<remarks>ClientHostname</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        public string ClientHostname(string strIpClient)
        {
            string hostName = "";
            try
            {
                hostName = Dns.GetHostByAddress(strIpClient).HostName;
            }
            catch
            {

                hostName = strIpClient;
            }
            return hostName;
        }


        ///<summary>Metodo que obtiene el usario actual/summary>
        ///<param name="idSession"></param>
        ///<returns>string<returns>
        ///<remarks>CurrentUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        public string CurrentUser(string idSession)
        {
            return idSession;

        }


        ///<summary>Metodo que obtiene la vista para la validación biométrica</summary>
        ///<returns>ActionResult<returns>
        ///<remarks>ShowBiometrics</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        public ActionResult ShowBiometrics()
        {
            return View("~/Areas/IFITransactions/Views/UnlockService/ShowBiometrics.cshtml");
        }


        ///<summary>Metodo que obtiene un documento en formato excel para su descarga</summary>
        ///<param name="strNewfileName"></param>
        ///<param name="strPath"></param>
        ///<returns>FileResult<returns>
        ///<remarks>DownloadExcel</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        public FileResult DownloadExcel(string strPath, string strNewfileName)
        {
            return File(strPath, "application/vnd.ms-excel", strNewfileName);
        }


        ///<summary>Metodo que obtiene la constancia generada en formato PDF</summary>
        ///<param name="idSession"></param>
        ///<param name="parameters"></param>
        ///<returns>GenerateConstancyResponseCommon<returns>
        ///<remarks>GenerateContancyPDF</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        public GenerateConstancyResponseCommon GenerateContancyPDF(string idSession, ParametersGeneratePDF parameters, Func<GenerateConstancyRequestCommon, List<E_KeywordRequest>> MethodKeywords = null)
        {
            parameters.StrServidorGenerarPDF = KEY.AppSettings("strServidorGenerarPDF");
            parameters.StrServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
            parameters.StrCarpetaPDFs = KEY.AppSettings("strCarpetaPDFs");

            var strTerminacionPDF = KEY.AppSettings("strTerminacionPDF");

            GenerateConstancyRequestCommon request = new GenerateConstancyRequestCommon()
            {
                ParametersGeneratePDFGeneric = parameters,
                audit = Claro.SIACU.Web.WebApplication.IFI.App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(idSession)
            };

            GenerateConstancyResponseCommon objResponse =
            Logging.ExecuteMethod<GenerateConstancyResponseCommon>(() =>
            {
                return _oServiceCommon.GetConstancyPDFWithOnbase(request);
            });

            if (objResponse.Generated)
            {
                string strFechaTransaccion = DateTime.Today.ToShortDateString().Replace("/", "_");

                string strNamePDF = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion, parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion.Replace("/", "_"), strTerminacionPDF);

                string strNamePath = string.Format("{0}{1}{2}", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);

                string strDocumentName = string.Format("{0}_{1}_{2}_{3}", parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion, strTerminacionPDF);

                objResponse.FullPathPDF = strNamePDF;
                objResponse.Path = strNamePath;
                objResponse.Document = strDocumentName;
                if (MethodKeywords != null)
                {
                    UploadDocumentOnBaseResponse objUploadDocumentOnBaseResponse = new UploadDocumentOnBaseResponse();
                    UploadDocumentOnBaseRequest objUploadDocumentOnBaseRequest = new UploadDocumentOnBaseRequest()
                    {

                        listaDocumentos = new E_DocumentRequest()
                        {
                            ListaMetadatos = MethodKeywords(request),
                            Seq = KEY.AppSettings("strConstSEQ"),
                            CodigoOnBase = KEY.AppSettings("strCodigoOnBase"),
                            CodigoTCRM = request.ParametersGeneratePDFGeneric.strCodeTCRM,
                            strExtension = KEY.AppSettings("strExtensionOnBase"),
                            TipoDocumentoOnBase = KEY.AppSettings("TipoDocumentoOnBaseSusp"),
                            abytArchivo = System.Convert.ToBase64String(objResponse.bytesConstancy)
                        },
                        audit = request.audit
                    };


                    objUploadDocumentOnBaseResponse = _oServiceCommon.GetUploadDocumentOnBase(objUploadDocumentOnBaseRequest);

                    if (objUploadDocumentOnBaseResponse != null)
                    {
                        if (objUploadDocumentOnBaseResponse.E_Document.Estado == "OK")
                        {
                            objResponse.FullPathPDF = strNamePDF;
                            objResponse.Path = strNamePath;
                            objResponse.Document = strDocumentName;

                        }
                        else
                        {
                            objResponse.FullPathPDF = string.Empty;
                            objResponse.Path = string.Empty;
                            objResponse.Document = string.Empty;
                            Logging.Error(idSession, request.audit.transaction, "Error al registrar Onbase." + objUploadDocumentOnBaseResponse.E_Document.Estado);
                        }
                    }
                    else
                    {
                        objResponse.FullPathPDF = string.Empty;
                        objResponse.Path = string.Empty;
                        objResponse.Document = string.Empty;

                    }



                }

                Logging.Info(idSession, request.audit.transaction, " FullPathPDF:    " + objResponse.FullPathPDF);
            }
            else
            {
                objResponse.FullPathPDF = string.Empty;
                objResponse.Path = string.Empty;
                objResponse.Document = string.Empty;
            }


            return objResponse;
        }


        ///<summary>Metodo que obtiene la constancia generada en formato PDF</summary>
        ///<param name="strIdSesison"></param>
        ///<param name="strID"></param>
        ///<param name="strID"></param>
        ///<returns>void<returns>
        ///<remarks>InsertEvidence</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
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

            }
            catch (Exception ex)
            {
                Logging.Error(audit.Session, audit.transaction, Claro.Utils.GetExceptionMessage(ex));
            }
        }


        ///<summary>Metodo que obtiene la lista de centros de atención al cliente</summary>
        ///<param name="strIdSession"></param>
        ///<returns>List<returns>
        ///<remarks>GetListCacDac</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        public List<Claro.ItemGeneric> GetListCacDac(string strIdSession)
        {
            List<Claro.ItemGeneric> list = new List<Claro.ItemGeneric>();

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
                    list.Add(new Claro.ItemGeneric()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }

            return list;
        }


        ///<summary>Metodo que obtiene los datos de la interacción</summary>
        ///<param name="idSession"></param>
        ///<param name="idContact"></param>
        ///<param name="strPhone"></param>
        ///<param name="strNotes"></param>
        ///<param name="strPhone"></param>
        ///<param name="strType"></param>
        ///<param name="strClass"></param>
        ///<param name="strSubClass"></param>
        ///<param name="isTFI"></param>
        ///<returns>Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.Iteraction<returns>
        ///<remarks>InteractionData</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
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


        ///<summary>Metodo que obtiene los datos de plantilla de la interacción</summary>
        ///<param name="idInteraction"></param>
        ///<param name="nameTransaction"></param>
        ///<param name="strPhoneNumber"></param>
        ///<param name="strReason"></param>
        ///<param name="firstName"></param>
        ///<param name="lastName"></param>
        ///<param name="strLegalRepresentative"></param>
        ///<param name="strDocumentType"></param>
        ///<param name="strNumberDocument"></param>
        ///<param name="strStartDate"></param>
        ///<param name="strEndDate"></param>
        ///<param name="flagGenerateOCC"></param>
        ///<param name="dblMonto"></param>
        ///<param name="idCACDAC"></param>
        ///<returns>Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.InsertTemplateInteraction<returns>
        ///<remarks>InteractionTemplateData</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
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


        ///<summary>Metodo que inserta la interacción del negocio</summary>
        ///<param name="item"></param>
        ///<param name="idSession"></param>
        ///<param name="rInteractionId"></param>
        ///<param name="rFlagInsertion"></param>
        ///<param name="rMsgText"></param>
        ///<returns>void<returns>
        ///<remarks>InsertBusinessInteraction2</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
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


        ///<summary>Metodo que obtiene el número telefónico</summary>
        ///<param name="idSession"></param>
        ///<param name="flagCountry"></param>
        ///<param name="strNumber"></param>
        ///<returns>string<returns>
        ///<remarks>GetNumber</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        public string GetNumber(string idSession, bool flagCountry, string strNumber)
        {
            DateTime dateAVM;
            DateTime dateCurrent;
            string numberGenerated = String.Empty, numberTelehone = String.Empty;
            string strInternationalCode = KEY.AppSettings("gInternationalCode");
            int maximumLenghNumberTelephone = Int32.Parse(KEY.AppSettings("gMaximumLengthPhone"));

            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
            dateAVM = DateTime.Parse(KEY.AppSettings("strFechaAVM"), culture,
                System.Globalization.DateTimeStyles.AssumeLocal);
            dateCurrent = DateTime.Now;

            if (string.IsNullOrEmpty(strNumber))
            {
                numberTelehone = string.Empty;
            }
            else if (strNumber.Length == maximumLenghNumberTelephone)
            {
                if ((dateCurrent - dateAVM).Days < 0)
                {

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

            return numberTelehone;
        }


        ///<summary>Metodo que carga la tipificación para la página</summary>
        ///<param name="idSession"></param>
        ///<param name="transactionName"></param>
        ///<param name="strType"></param>
        ///<returns>HelperCommon.Typification<returns>
        ///<remarks>LoadTypification</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
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

                    for (int i = 0; i < response.ListTypification.Count; i++)
                    {
                        Logging.Info(idSession, audit.transaction, String.Format("TIPO: {0};CLASE: {1};SUBCLASE:{2}", response.ListTypification[i].TIPO,
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


        ///<summary>Metodo que permite guardar la auditoría</summary>
        ///<param name="idSession"></param>
        ///<param name="strPhone"></param>
        ///<param name="strAmount"></param>
        ///<param name="strText"></param>
        ///<param name="strService"></param>
        ///<param name="strTransaction"></param>
        ///<returns>bool<returns>
        ///<remarks>RegisterAuditGeneral</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
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


        ///<summary>Metodo que permite guardar el log</summary>
        ///<param name="idSession"></param>
        ///<param name="strPhone"></param>
        ///<param name="strInteraction"></param>
        ///<param name="strTypification"></param>
        ///<param name="strParamIN"></param>
        ///<param name="strParamOUT"></param>
        ///<param name="strOpcionCode"></param>
        ///<param name="strAccion"></param>
        ///<param name="strAccionEvento"></param>
        ///<param name="nameTransaction"></param>
        ///<returns>bool<returns>
        ///<remarks>RegisterAuditGeneral</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        public bool RegisterLogTrx(string idSession, string strPhone, string strInteraction, string strTypification,
            string strParamIN, string strParamOUT, string strOpcionCode, string strAccion, string strAccionEvento,
            string nameTransaction)
        {
            bool salida = false;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(idSession);

            string strIpCliente = CurrentTerminal();
            string strCliente = ClientHostname(strIpCliente);

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


        ///<summary>Metodo que permite obtener el número generado</summary>
        ///<param name="idSession"></param>
        ///<param name="msisdn"></param>
        ///<returns>string<returns>
        ///<remarks>GetNumberGenerated</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
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


        ///<summary>Metodo que permite obtener el número generado EAI</summary>
        ///<param name="msisdn"></param>
        ///<param name="audit"></param>
        ///<param name="strNumberGenerated"></param>
        ///<returns>NumberEAIResponse<returns>
        ///<remarks>GetNumberEAI</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
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


        ///<summary>Metodo que permite obtener el número generado GWP</summary>
        ///<param name="msisdn"></param>
        ///<param name="audit"></param>
        ///<param name="strNumberGenerated"></param>
        ///<returns>NumberGWPResponse<returns>
        ///<remarks>GetNumberGWP</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
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


        ///<summary>Metodo que insert el log</summary>
        ///<param name="request"></param>
        ///<returns>string<returns>
        ///<remarks>InsertLogTrx</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        private string InsertLogTrx(InsertLogTrxRequestCommon request)
        {
            InsertLogTrxResponseCommon objResponse =
                Logging.ExecuteMethod<InsertLogTrxResponseCommon>(() =>
                {
                    return _oServiceCommon.InsertLogTrx(request);
                });
            return objResponse.FlagInsertion;
        }


        ///<summary>Metodo que permite obtener las lista de items genéricos</summary>
        ///<param name="listDB"></param>
        ///<returns>List<returns>
        ///<remarks>MappingListItemGeneric</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
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


        ///<summary>Metodo que permite obtener las datos de la tipificación</summary>
        ///<param name="typifDB"></param>
        ///<returns>HelperCommon.Typification<returns>
        ///<remarks>MappingTypification</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
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


        ///<summary>Metodo que permite obtener la lista de cac/dac</summary>
        ///<param name="strIdSession"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>GetCacDacType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
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


        /// <summary>Método que obtiene la lista de estado civil</summary>
        ///<param name="strIdSession"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>GetEstCivType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public JsonResult GetEstCivType(string strIdSession)
        {
            EstCivTypeResponseCommon objEstCivTypeResponseCommon;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);
            EstCivTypeRequestCommon objEstCivTypeRequestCommon =
                new EstCivTypeRequestCommon()
                {
                    audit = audit
                };
            try
            {
                objEstCivTypeResponseCommon =
                    Logging.ExecuteMethod<EstCivTypeResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetEstCivType(objEstCivTypeRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objEstCivTypeRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            CommonServices objCommonServices = null;
            if (objEstCivTypeResponseCommon != null && objEstCivTypeResponseCommon.EstCivTypes != null)
            {
                objCommonServices = new CommonServices();
                List<EstCivTypeVM> listEstCivTypes =
                    new List<EstCivTypeVM>();
                foreach (Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.ListItem item in objEstCivTypeResponseCommon.EstCivTypes)
                {
                    listEstCivTypes.Add(new EstCivTypeVM()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonServices.EstCivTypes = listEstCivTypes;
            }
            return Json(new { data = objCommonServices });
        }


        /// <summary>Método que obtiene la lista de tipo de via</summary>
        ///<param name="strIdSession"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>GetViaType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018.</FecCrea></item></list>
        public JsonResult GetViaType(string strIdSession)
        {
            ViasTypeResponseCommon objViasTypeResponseCommon;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);
            ViasTypeRequestCommon objViasTypeRequestCommon =
                new ViasTypeRequestCommon()
                {
                    audit = audit
                };

            try
            {
                objViasTypeResponseCommon =
                    Logging.ExecuteMethod<ViasTypeResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetViasType(objViasTypeRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objViasTypeRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            CommonServices objCommonServices = null;
            if (objViasTypeResponseCommon != null && objViasTypeResponseCommon.ViasTypes != null)
            {
                objCommonServices = new CommonServices();
                List<ViaType> listViaTypes =
                    new List<ViaType>();

                foreach (Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.ListItem item in objViasTypeResponseCommon.ViasTypes)
                {
                    listViaTypes.Add(new ViaType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonServices.ViaTypes = listViaTypes;
            }

            return Json(new { data = objCommonServices });
        }


        /// <summary>Método que obtiene la lista de tipo de manzanas</summary>
        ///<param name="strIdSession"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>GetManzanaType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018.</FecCrea></item></list>
        public JsonResult GetManzanaType(string strIdSession)
        {

            ManzanasTypeResponseCommon objManzanasTypeResponseCommon;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);
            ManzanasTypeRequestCommon objManzanasTypeRequestCommon =
                new ManzanasTypeRequestCommon()
                {
                    audit = audit
                };

            try
            {
                objManzanasTypeResponseCommon =
                    Logging.ExecuteMethod<ManzanasTypeResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetManzanasType(objManzanasTypeRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objManzanasTypeRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            CommonServices objCommonServices = null;
            if (objManzanasTypeResponseCommon != null && objManzanasTypeResponseCommon.ManzanasTypes != null)
            {
                objCommonServices = new CommonServices();
                List<ManzanaType> listManzanaTypes =
                    new List<ManzanaType>();

                foreach (Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.ListItem item in objManzanasTypeResponseCommon.ManzanasTypes)
                {
                    listManzanaTypes.Add(new ManzanaType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonServices.ManzanaTypes = listManzanaTypes;
            }

            return Json(new { data = objCommonServices });
        }


        /// <summary>Método que obtiene la lista del tipo de Interior</summary>
        ///<param name="strIdSession"></param>
        /// <returns>List</returns> 
        /// <remarks>GetInteriorType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018.</FecCrea></item></list>
        public JsonResult GetInteriorType(string strIdSession)
        {

            InterioresTypeResponseCommon objInterioresTypeResponseCommon;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);
            InterioresTypeRequestCommon objInterioresTypeRequestCommon =
                new InterioresTypeRequestCommon()
                {
                    audit = audit
                };

            try
            {
                objInterioresTypeResponseCommon =
                    Logging.ExecuteMethod<InterioresTypeResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetInterioresType(objInterioresTypeRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objInterioresTypeRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            CommonServices objCommonServices = null;
            if (objInterioresTypeResponseCommon != null && objInterioresTypeResponseCommon.InterioresTypes != null)
            {
                objCommonServices = new CommonServices();
                List<InteriorType> listInteriorTypes =
                    new List<InteriorType>();

                foreach (Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.ListItem item in objInterioresTypeResponseCommon.InterioresTypes)
                {
                    listInteriorTypes.Add(new InteriorType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonServices.InteriorTypes = listInteriorTypes;
            }

            return Json(new { data = objCommonServices });
        }


        /// <summary>Método que obtiene la lista del tipo de urbanizacion</summary>
        ///<param name="strIdSession">/param> Envia el Id de la session
        /// <returns>List</returns>
        /// <remarks>GetUrbType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018.</FecCrea></item></list>
        public JsonResult GetUrbType(string strIdSession)
        {

            UrbsTypeResponseCommon objUrbsTypeResponseCommon;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);
            UrbsTypeRequestCommon objUrbsTypeRequestCommon =
                new UrbsTypeRequestCommon()
                {
                    audit = audit
                };

            try
            {
                objUrbsTypeResponseCommon =
                    Logging.ExecuteMethod<UrbsTypeResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetUrbsType(objUrbsTypeRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objUrbsTypeRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            CommonServices objCommonServices = null;
            if (objUrbsTypeResponseCommon != null && objUrbsTypeResponseCommon.UrbsTypes != null)
            {
                objCommonServices = new CommonServices();
                List<UrbType> listUrbTypes =
                    new List<UrbType>();

                foreach (Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.ListItem item in objUrbsTypeResponseCommon.UrbsTypes)
                {
                    listUrbTypes.Add(new UrbType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonServices.UrbTypes = listUrbTypes;
            }

            return Json(new { data = objCommonServices });
        }


        /// <summary>Método que obtiene la lista del tipo de zonas para la dirección</summary>
        ///<param name="strIdSession"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetZoneType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018.</FecCrea></item></list>
        public JsonResult GetZoneType(string strIdSession)
        {

            ZonesTypeResponseCommon objZonesTypeResponseCommon;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);
            ZonesTypeRequestCommon objZonesTypeRequestCommon =
                new ZonesTypeRequestCommon()
                {
                    audit = audit
                };

            try
            {
                objZonesTypeResponseCommon =
                    Logging.ExecuteMethod<ZonesTypeResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetZonesType(objZonesTypeRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objZonesTypeRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            CommonServices objCommonServices = null;
            if (objZonesTypeResponseCommon != null && objZonesTypeResponseCommon.ZonesTypes != null)
            {
                objCommonServices = new CommonServices();
                List<ZoneType> listZoneTypes =
                    new List<ZoneType>();

                foreach (Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.ListItem item in objZonesTypeResponseCommon.ZonesTypes)
                {
                    listZoneTypes.Add(new ZoneType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonServices.ZoneTypes = listZoneTypes;
            }

            return Json(new { data = objCommonServices });
        }


        /// <summary>Método que obtiene la lista del tipo de ubigeo </summary>
        ///<param name="strIdSession"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetUbigeoType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>09/10/2018.</FecCrea></item></list>
        public JsonResult GetUbigeoType(string strIdSession, int dep, int prov)
        {
            UbigeosTypeResponseCommon objUbigeosTypeResponseCommon;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);
            UbigeosTypeRequestCommon objUbigeosTypeRequestCommon =
                new UbigeosTypeRequestCommon()
                {
                    audit = audit

                };

            try
            {
                objUbigeosTypeResponseCommon =
                    Logging.ExecuteMethod<UbigeosTypeResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetUbigeosType(objUbigeosTypeRequestCommon, dep, prov);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objUbigeosTypeRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            CommonServices objCommonServices = null;
            if (objUbigeosTypeResponseCommon != null && objUbigeosTypeResponseCommon.UbigeosTypes != null)
            {
                objCommonServices = new CommonServices();
                List<UbigeoType> listUbigeoTypes =
                    new List<UbigeoType>();

                foreach (Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.ListItem item in objUbigeosTypeResponseCommon.UbigeosTypes)
                {
                    listUbigeoTypes.Add(new UbigeoType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonServices.UbigeoTypes = listUbigeoTypes;
            }

            return Json(new { data = objCommonServices });
        }


        /// <summary>Método que obtiene la lista de nacionalidad</summary>
        ///<param name="strIdSession"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>GetNacType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public JsonResult GetNacType(string strIdSession)
        {
            NacTypeResponseCommon objNacTypeResponseCommon;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);
            NacTypeRequestCommon objNacTypeRequestCommon =
                new NacTypeRequestCommon()
                {
                    audit = audit
                };
            try
            {
                objNacTypeResponseCommon =
                    Logging.ExecuteMethod<NacTypeResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetNacType(objNacTypeRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objNacTypeRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            CommonServices objCommonServices = null;
            if (objNacTypeResponseCommon != null && objNacTypeResponseCommon.NacTypes != null)
            {
                objCommonServices = new CommonServices();
                List<NacTypeVM> listNacTypes =
                    new List<NacTypeVM>();
                foreach (Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.ListItem item in objNacTypeResponseCommon.NacTypes)
                {
                    listNacTypes.Add(new NacTypeVM()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonServices.NacTypes = listNacTypes;
            }
            return Json(new { data = objCommonServices });
        }


        /// <summary>Método que obtiene la lista del tipo de fechas</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strCustomerId"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>GetdgFechasType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/12/2018.</FecCrea></item></list>
        public JsonResult GetdgFechasType(string strIdSession, string strCustomerId)
        {
            LastInvoiceDataResponseCommon objLastInvoiceDataResponseCommon;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);
            LastInvoiceDataRequestCommon objLastInvoiceDataRequestCommon =
                new LastInvoiceDataRequestCommon()
                {
                    audit = audit

                };

            try
            {
                objLastInvoiceDataResponseCommon =
                    Logging.ExecuteMethod<LastInvoiceDataResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetLastInvoiceData(objLastInvoiceDataRequestCommon, strCustomerId);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objLastInvoiceDataRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            CommonServices objCommonServices = null;
            if (objLastInvoiceDataResponseCommon != null && objLastInvoiceDataResponseCommon.LastInvoiceDatas != null)
            {
                objCommonServices = new CommonServices();
                List<LastInvoiceData> listLastInvoiceData =
                    new List<LastInvoiceData>();


                if (objLastInvoiceDataResponseCommon.LastInvoiceDatas != null)
                {

                    objLastInvoiceDataResponseCommon.LastInvoiceDatas = objLastInvoiceDataResponseCommon.LastInvoiceDatas.OrderByDescending(x => DateTime.Parse(x.FECHA_VENCIMIENTO)).Take(6).ToList();
                }

                foreach (Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.ReceiptPostPaid item in objLastInvoiceDataResponseCommon.LastInvoiceDatas)
                {
                    listLastInvoiceData.Add(new LastInvoiceData()
                    {
                        FECHA_VENCIMIENTO = item.FECHA_VENCIMIENTO,
                        MTO_ULT_FACTURA = item.MTO_ULT_FACTURA,
                        PERIODO = item.PERIODO,
                        INVOICENUMBER = item.INVOICENUMBER
                    });
                }
                objCommonServices.LastInvoiceDatas = listLastInvoiceData;
            }

            return Json(new { data = objCommonServices });

        }


        /// <summary>Método que obtiene la lista del tipo de fechas</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strCustomerId"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>GetdgFechasType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/12/2018.</FecCrea></item></list>
        public List<TypificationModel> GetTypificationHFC(string strIdSession, string strTransactionName)
        {
            var response = new List<TypificationModel>();
            TypificationResponse objTypificationResponse = null;
            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            TypificationRequest objTypificationRequest = new TypificationRequest();
            objTypificationRequest.audit = audit;
            objTypificationRequest.TRANSACTION_NAME = strTransactionName;
            var msg = string.Format("Controlador: {0},Metodo: {1}, WebConfig: {2}", "CallDetailController", "GetTypificationHFC", "SIACU_SP_OBTENER_TIPIFICACION");
            Logging.Info(strIdSession, audit.transaction, msg);
            try
            {
                objTypificationResponse = Logging.ExecuteMethod<TypificationResponse>(() =>
                {
                    return _oServiceCommon.GetTypification(objTypificationRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            var tempLst = objTypificationResponse.ListTypification;

            if (objTypificationResponse.ListTypification != null)
                Logging.Info(strIdSession, audit.transaction, " Total Reg : " + objTypificationResponse.ListTypification.Count().ToString());
            else
                Logging.Info(strIdSession, audit.transaction, " Total Reg : 0 o null");

            response = Mapper.Map<List<TypificationModel>>(tempLst);
            return response;
        }


        /// <summary>Método que obtiene las reglas del negocio</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strSubClase"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>GetBusinessRules</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/12/2018.</FecCrea></item></list>
        public JsonResult GetBusinessRules(string strIdSession, string strSubClase)
        {
            BusinessRulesResponse objBusinessRulesResponse = new BusinessRulesResponse();
            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            BusinessRulesRequest objBusinessRulesRequest = new BusinessRulesRequest();
            objBusinessRulesRequest.audit = audit;
            objBusinessRulesRequest.SUB_CLASE = strSubClase;

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

            return Json(new { data = objBusinessRulesResponse });
        }


        /// <summary>Método que inserta los datos de la interacción</summary>
        ///<param name="objInteractionModel"></param>
        ///<param name="strIdSession"></param>
        /// <returns>Dictionary</returns> 
        /// <remarks>GetInsertInteractionCLFY</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/12/2018.</FecCrea></item></list>
        public Dictionary<string, string> GetInsertInteractionCLFY(InteractionModel objInteractionModel, string strIdSession)
        {
            var serviceModelInteraction = Mapper.Map<CommonIFIService.Interaction>(objInteractionModel);
            CommonIFIService.InsertInteractHFCResponse objInteractHFCResponse = null;
            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "CommonServiceController", "GetInsertInteractionCLFY", "SIACU_POST_CLARIFY_SP_CREATE_INTERACT_HFC");
            Logging.Info(strIdSession, audit.transaction, msg);
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


            return dictionaryResponse;
        }


        /// <summary>Método que inserta los datos de la interacción de contingencia</summary>
        ///<param name="objInteractionModel"></param>
        ///<param name="strIdSession"></param>
        /// <returns>Dictionary</returns> 
        /// <remarks>GetInsertContingencyInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/12/2018.</FecCrea></item></list>
        public Dictionary<string, string> GetInsertContingencyInteraction(InteractionModel objInteractionModel, string strIdSession)
        {
            var serviceModelInteraction = Mapper.Map<CommonIFIService.Iteraction>(objInteractionModel);
            InsertInteractResponseCommon objInsertInteractResponse = null;
            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "CommonServiceController", "GetInsertContingencyInteraction", "SIACU_POST_DB_SP_INSERTAR_INTERACT");
            Logging.Info(strIdSession, audit.transaction, msg);
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


        /// <summary>Método que inserta los datos de la plantilla de interacción </summary>
        ///<param name="objInteractionTemplateModel"></param>
        ///<param name="rInteraccionId"></param>
        ///<param name="strNroTelephone"></param>
        ///<param name="strUserSession"></param>
        ///<param name="strUserAplication"></param>
        ///<param name="strPassUser"></param>
        ///<param name="boolEjecutTransaction"></param>
        ///<param name="strIdSession"></param>
        /// <returns>Dictionary</returns> 
        /// <remarks>InsertPlantInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/12/2018.</FecCrea></item></list>
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


        /// <summary>Método que inserta los datos de la interacción </summary>
        ///<param name="strIdSession"></param>
        ///<param name="strInteraccionId"></param>
        /// <returns>TemplateInteractionModel</returns> 
        /// <remarks>GetInfoInteractionTemplate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/12/2018.</FecCrea></item></list>
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


        /// <summary>Método que guarda los datos de la auditoría</summary>
        ///<param name="strTransaction"></param>
        ///<param name="strService"></param>
        ///<param name="strText"></param>
        ///<param name="strTelephone"></param>
        ///<param name="strNameCustomer"></param>
        ///<param name="strIdSession"></param>
        ///<param name="strNameServidor"></param>
        ///<param name="strIpServidor"></param>
        ///<param name="strIpCustomer"></param>
        ///<param name="strCuentUser"></param>
        ///<param name="strMontoInput"></param>
        /// <returns>bool</returns> 
        /// <remarks>SaveAuditM</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/12/2018.</FecCrea></item></list>
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


        /// <summary>Método que valida las credenciales del dominio</summary>
        ///<param name="strIdSession"></param>
        ///<param name="transaction"></param>
        ///<param name="vUsuario"></param>
        ///<param name="vClave"></param>
        /// <returns>bool</returns> 
        /// <remarks>IsAuthenticated_LDAP</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/12/2018.</FecCrea></item></list>
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


        /// <summary>Método que evalúa el monto</summary>
        ///<param name="strIdSession"></param>
        ///<param name="transaction"></param>
        ///<param name="vModalidad"></param>
        ///<param name="vMonto"></param>
        ///<param name="vUnidad"></param>
        ///<param name="vListaPerfil"></param>
        ///<param name="vTipoTelefono"></param>
        /// <returns>string</returns> 
        /// <remarks>EvaluateAmount</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que evalúa el monto DCM</summary>
        ///<param name="strIdSession"></param>
        ///<param name="transaction"></param>
        ///<param name="vModalidad"></param>
        ///<param name="vMonto"></param>
        ///<param name="vUnidad"></param>
        ///<param name="vListaPerfil"></param>
        ///<param name="vTipoTelefono"></param>
        /// <returns>string</returns> 
        /// <remarks>EvaluateAmount_DCM</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que valida el dominio</summary>
        ///<param name="strIdSession"></param>
        ///<param name="user"></param>
        ///<param name="pass"></param>
        /// <returns>bool</returns> 
        /// <remarks>IsAuthenticated</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        private bool IsAuthenticated(string strIdSession, string user, string pass)
        {
            CommonIFIService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            string strDominio = ConfigurationManager.AppSettings("DominioLDAP");
            System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry(strDominio, user, pass);
            try
            {
                var obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + user + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult resul = search.FindOne();

                if (resul == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objAuditRequest.transaction, ex.Message);
                return false;
            }
        }


        /// <summary>Método que valida el usuario</summary>
        ///<param name="strIdSession"></param>
        ///<param name="user"></param>
        ///<param name="pass"></param>
        ///<param name="option"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>CheckingUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public JsonResult CheckingUser(string strIdSession, string user, string pass, string option)
        {
            CommonIFIService.CheckingUserResponse objCheckingUserResponse = null;
            CommonIFIService.ReadOptionsByUserResponse objReadOptionsByUserResponse = null;
            CommonIFIService.EmployeeResponse objEmployeeResponse = null;
            CommonIFIService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            bool result = false;
            int responseCode = 0;
            string UserValidator = "";
            string usu = "";
            try
            {

                result = IsAuthenticated(strIdSession, user, pass);
                if (result == true)
                {
                    result = false;
                    CommonIFIService.CheckingUserRequest objCheckingUserRequest = new CommonIFIService.CheckingUserRequest()
                    {
                        audit = objAuditRequest,
                        Usuario = user,
                        AppCod = int.Parse(KEY.AppSettings("ApplicationCode"))
                    };
                    objCheckingUserResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.CheckingUserResponse>(() =>
                    {
                        return new CommonIFIService.CommonIFIServiceClient().CheckingUser(objCheckingUserRequest);
                    });

                    if (objCheckingUserResponse != null)
                    {

                        if (objCheckingUserResponse.consultasSeguridad != null && (objCheckingUserResponse.consultasSeguridad != null && objCheckingUserResponse.consultasSeguridad.Count > 0))
                        {
                            usu = objCheckingUserResponse.consultasSeguridad[0].Usuaccod;
                        }
                        int IdUser = 0;
                        bool est = int.TryParse(usu, out IdUser);
                        if (!est)
                        {
                            CommonIFIService.EmployeeRequest objEmployeeRequestEmployee = new CommonIFIService.EmployeeRequest()
                            {
                                audit = objAuditRequest,
                                UserName = user
                            };

                            objEmployeeResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.EmployeeResponse>(() =>
                            {
                                return new CommonIFIService.CommonIFIServiceClient().GetEmployeByUser(objEmployeeRequestEmployee);
                            });
                            if (objEmployeeResponse != null && (objEmployeeResponse.lstEmployee != null && objEmployeeResponse.lstEmployee.Count > 0))
                            {
                                usu = objEmployeeResponse.lstEmployee[0].UserID.ToString();
                            }
                            est = int.TryParse(usu, out IdUser);
                        }
                        if (est)
                        {
                            CommonIFIService.ReadOptionsByUserRequest objReadOptionsByUserRequest = new CommonIFIService.ReadOptionsByUserRequest()
                            {
                                audit = objAuditRequest,
                                IdUser = IdUser,
                                IdAplication = int.Parse(ConfigurationManager.AppSettings("ApplicationCode"))
                            };
                            objReadOptionsByUserResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.ReadOptionsByUserResponse>(() =>
                            {
                                return new CommonIFIService.CommonIFIServiceClient().ReadOptionsByUser(objReadOptionsByUserRequest);
                            });
                        }
                        StringBuilder strPermission = null;
                        if (objReadOptionsByUserResponse != null && (objReadOptionsByUserResponse.ListOption != null && objReadOptionsByUserResponse.ListOption.Count > 0))
                        {
                            strPermission = new StringBuilder();
                            foreach (CommonIFIService.PaginaOption item in objReadOptionsByUserResponse.ListOption)
                            {
                                strPermission.Append(item.Clave);
                                strPermission.Append(",");
                            }
                        }
                        if (strPermission != null)
                        {
                            if (strPermission.ToString().IndexOf(ConfigurationManager.AppSettings(option)) != -1)
                            {
                                responseCode = 1;
                                UserValidator = user;
                            }
                            else
                            {
                                responseCode = 2;
                                UserValidator = "";
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                objCheckingUserResponse = null;
                objReadOptionsByUserResponse = null;
                Claro.Web.Logging.Error(strIdSession, objAuditRequest.transaction, ex.Message);
                responseCode = 3;

            }
            return Json(new { UserValidator = UserValidator, result = responseCode });
        }


        /// <summary>Método que obtiene el listado de valores desde un XML</summary>
        ///<param name="strIdSession"></param>
        ///<param name="fileName"></param>
        ///<param name="strNameFunction"></param>
        ///<param name="strFlagCode"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>ListValueXmlMethodIFI</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public JsonResult ListValueXmlMethodIFI(string strIdSession, string fileName,
            string strNameFunction, string strFlagCode)
        {
            var lstItemVm = new List<ItemGeneric>();
            var responseMethod = string.Empty;
            var objAuditRequest = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

            try
            {

                var objListItemResponse = Claro.Utils.GetListValuesXMLIFI(strNameFunction, strFlagCode, fileName);

                foreach (var item in objListItemResponse)
                {
                    lstItemVm.Add(item);
                }

            }
            catch (Exception ex)
            {
                Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }
            return Json(new { data = lstItemVm });
        }


        /// <summary>Método que permite registrar la auditoría</summary>
        ///<param name="strDescription"></param>
        ///<param name="dblCodeEvent"></param>
        ///<param name="objDetailAuditory"></param>
        ///<param name="strIdSession"></param>
        /// <returns>void</returns> 
        /// <remarks>RegisterAudit</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public void RegisterAudit(string strDescription, double dblCodeEvent, object[,] objDetailAuditory,
            string strIdSession)
        {

            string strTransaction = Convert.ToString(dblCodeEvent);
            string strIpServi = Request.ServerVariables["LOCAL_ADDR"];
            string strNameCustomer = "";

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


        /// <summary>Método que obtiene el usuario</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strCodeUser"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>GetUsers</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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

            return Json(objRegionResponse.UserModel);
        }


        /// <summary>Método que obtiene datos del empleado</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strCodeUser"></param>
        /// <returns>GetEmployerDateResponseCommon</returns> 
        /// <remarks>GetEmployerDate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que obtiene los datos de los parámetros indicados</summary>
        ///<param name="strIdSession"></param>
        ///<param name="name"></param>
        /// <returns>ParameterDataResponseCommon</returns> 
        /// <remarks>GetParameterData</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public ParameterDataResponseCommon GetParameterData(string strIdSession, string name)
        {

            ParameterDataResponseCommon objResponse = new ParameterDataResponseCommon();
            CommonIFIService.AuditRequest audit =
               APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            ParameterDataRequestCommon objRequest = new ParameterDataRequestCommon();

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

            return objResponse;

        }


        /// <summary>Método que obtiene al cliente</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strCustomerId"></param>
        /// <returns>string</returns> 
        /// <remarks>GetCustomer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public string GetCustomer(string strCustomerId, string strIdSession)
        {
            string strObjId = string.Empty;
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
            catch (Exception ex)
            {
                Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            return strObjId;
        }


        /// <summary>Método que obtiene los datos del cliente</summary>
        ///<param name="oCustomer"></param>
        ///<param name="strIdSession"></param>
        /// <returns>CustomerResponse</returns> 
        /// <remarks>GetCustomerData</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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
            catch (Exception ex)
            {
                Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            return objCustomerResponse;
        }


        /// <summary>Método que obtiene los parámetrs del terminal TPI</summary>
        ///<param name="ParameterID"></param>
        ///<param name="audit"></param>
        /// <returns>ParameterTerminalTPIResponse</returns> 
        /// <remarks>GetParameterTerminalTPI</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que permite validar los permisos</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strContratoID"></param>
        ///<param name="hidListNumImportar"></param>
        ///<param name="audit"></param>
        /// <returns>string</returns> 
        /// <remarks>ValidarPermiso</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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

                    objTerTPI = GetParameterTerminalTPI(intCodParam, auditC);


                    objTFI_Post = GetSiacParameterTFIPostpago(strIdSession, intCodParam2);

                    TerminalTPI = objTerTPI.ListParameterTeminalTPI.FirstOrDefault().ValorC.ToString();




                    TerminalTPI_Post = objTFI_Post.ListParameterTeminalTPI.FirstOrDefault().ValorC.ToString();



                    DataLineResponsePostPaid oDataLinea = new DataLineResponsePostPaid();



                    DataLineRequestPostPaid objRequest = new DataLineRequestPostPaid()
                    {
                        ContractID = strContratoID,
                        audit = audit
                    };

                    oDataLinea = _oServicePostpaid.GetDataLine(objRequest);
                    intCodPlanTarif = oDataLinea.DataLine.CodPlanTariff;

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


        /// <summary>Método que obtiene los parámetros indicados del siac postpago</summary>
        ///<param name="strIdSession"></param>
        ///<param name="parametroID"></param>
        /// <returns>ParameterTerminalTPIResponse</returns> 
        /// <remarks>GetSiacParameterTFIPostpago</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que permite crear un caso</summary>
        ///<param name="oRequest"></param>
        /// <returns>FixedIFIService.Interaction</returns> 
        /// <remarks>GetCreateCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que permite insertar un caso</summary>
        ///<param name="oRequest"></param>
        /// <returns>FixedIFIService.Interaction</returns> 
        /// <remarks>GetInsertCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que permite insertar los datos de la plantilla de la interacción</summary>
        ///<param name="oRequest"></param>
        /// <returns>FixedIFIService.CaseTemplate</returns> 
        /// <remarks>GetInsertTemplateCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que permite insertar los datos de la plantilla de contingencia de la interacción</summary>
        ///<param name="oRequest"></param>
        /// <returns>FixedIFIService.CaseTemplate</returns> 
        /// <remarks>GetInsertTemplateCaseContingent</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que permite actualizar los datos de la plantilla del caso</summary>
        ///<param name="oRequest"></param>
        /// <returns>FixedIFIService.CaseTemplate</returns> 
        /// <remarks>ActualizaPlantillaCaso</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que permite obtener el id del contacto</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strCustomerID"></param>
        /// <returns>string</returns> 
        /// <remarks>GetOBJID</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que permite obtener la tipificación de la transaccion indicada</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strTransactionName"></param>
        ///<param name="strType"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>GetTypification</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que permite insertar los datos de la interacción.</summary>
        ///<param name="objInteractionModel"></param>
        ///<param name="oPlantillaDat"></param>
        ///<param name="strNroTelephone"></param>
        ///<param name="strUserSession"></param>
        ///<param name="strUserAplication"></param>
        ///<param name="strPassUser"></param>
        ///<param name="boolEjecutTransaction"></param>
        ///<param name="strIdSession"></param>
        ///<param name="strCustomerId"></param>
        /// <returns>Dictionary</returns> 
        /// <remarks>InsertInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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
                return _oServiceFixed.GetCustomerClf(objGetCustomerRequest);
            });

            if (objCustomerResponse != null)
            {
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;


                if (objCustomerResponse.Customer != null)
                {
                    objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                    objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
                }
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


        /// <summary>Método que verifica si existe un archivo en la ruta especificada.</summary>
        ///<param name="strFilePath"></param>
        ///<param name="strIdSession"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>ExistFileSharedFile</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public JsonResult ExistFileSharedFile(string strFilePath, string strIdSession)
        {
            CommonService.AuditRequest objAudit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            bool ExistFile = false;
            byte[] byteDataFile;

            try
            {
                ExistFile = DisplayFileFromServerSharedFile(strIdSession, objAudit.transaction, strFilePath, out byteDataFile);

                byteDataFile = null;
            }
            catch (Exception ex)
            {
                ExistFile = false;

                Logging.Error(strIdSession, objAudit.transaction, Claro.Utils.GetExceptionMessage(ex));
            }
            return Json(new { Exist = ExistFile }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>Método que obtiene un archivo desde una ruta</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strFilePath"></param>
        /// <returns>FileContentResult</returns> 
        /// <remarks>ShowRecordSharedFile</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que verifica si existe un archivo en el servidor compartido</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strTransaction"></param>
        ///<param name="c"></param>
        ///<param name="mydata"></param>
        /// <returns>bool</returns> 
        /// <remarks>DisplayFileFromServerSharedFile</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public bool DisplayFileFromServerSharedFile(string strIdSession, string strTransaction, string strPath, out byte[] mydata)
        {
            Claro.Web.Logging.Info(strIdSession, strTransaction, "Entro a metodo DisplayFileFromServerSharedFile ");
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
                Claro.Web.Logging.Info(strIdSession, strTransaction, "entrando a metodo Get_Credentials "); 
                credentials = Get_Credentials(strIdSession, strTransaction, ConfigurationManager.AppSettings("ConexionSamba"), out user, out pass, out domain);
                Claro.Web.Logging.Info(strIdSession, strTransaction, "paso a metodo Get_Credentials ");
                if (obj.impersonateValidUser(user, domain, pass))
                {
                    Claro.Web.Logging.Info(strIdSession, strTransaction, "paso a if  impersonateValidUser");
                    fiInfo = new FileInfo(strPath);
                    fs = new FileStream(fiInfo.ToString(), FileMode.Open);
                    br = new BinaryReader(fs);
                    mydata = br.ReadBytes(Convert.ToInt((fs.Length - 1)));

                    blnResult = true;
                }
                else
                {
                    Claro.Web.Logging.Info(strIdSession, strTransaction, "no paso a if  impersonateValidUser");
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


        /// <summary>Método que obtiene los datos de las credenciales</summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strConexion"></param>
        /// <param name="struser"></param>
        /// <param name="strpass"></param>
        /// <param name="strdomain"></param>  
        /// <returns>bool</returns> 
        /// <remarks>Get_Credentials</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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


        /// <summary>Método que obtiene el id de la interacción por el id del caso</summary>
        /// <param name="strIdSession"></param>
        /// <param name="strCasoID"></param>
        /// <returns>Claro.ItemGeneric</returns> 
        /// <remarks>GetInteractIDforCaseID</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public Claro.ItemGeneric GetInteractIDforCaseID(string strIdSession, string strCasoID)
        {
            Claro.ItemGeneric oItem = new Claro.ItemGeneric();
            CaseInsertResponse objResponse = new CaseInsertResponse();
            FixedIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);

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


            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
            }

            return oItem;
        }


        /// <summary>Método que permite cargar la tipificación</summary>
        /// <param name="strIdSession"></param>
        /// <param name="typeProduct"></param>
        /// <param name="lblMensaje"></param>
        /// <returns>CommonService.Typification</returns> 
        /// <remarks>LoadTypifications</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public CommonService.Typification LoadTypifications(string strIdSession, string typeProduct, ref string lblMensaje)
        {
            Claro.Web.Logging.Configure();

            CommonService.Typification oTypification = new CommonService.Typification();
            CommonService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);



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
                    lblMensaje = Claro.Utils.GetValueFromConfigFileIFI("strAjusteNoRecon", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                lblMensaje = Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
            }

            return oTypification;
        }

        #region EMAIL

        /// <summary>Método que envía correo electrónico adjuntando un archivo</summary>
        /// <param name="objSendEmail"></param>
        /// <returns>string</returns> 
        /// <remarks>GetSendEmailConst</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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
                strResul = Claro.Utils.GetValueFromConfigFileIFI("strMensajeEnvioOK", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
            }
            else
            {
                strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
            }

            return strResul;
        }


        /// <summary>Método que obtiene la plantilla para el correo electrónico a enviar</summary>
        /// <param name="strMessage"></param>
        /// <param name="strMsgEmailCall"></param>
        /// <returns>string</returns> 
        /// <remarks>TemplateEmail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
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
            strHtml.Append("<tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strEstimadoCliente", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + " </td></tr>");

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
            strHtml.Append("<tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgCordialmente", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>");
            strHtml.Append("<tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailFirma", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>");

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


        /// <summary>Método que obtiene los datos de la ultima facturacion del cliente</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strCustomerCode"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>GetLastInvoiceData</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018.</FecCrea></item></list>
        public JsonResult GetLastInvoiceData(string strIdSession, string strCustomerCode)
        {
            LastInvoiceDataResponseCommon objLastInvoiceDataResponseCommon;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
                APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);
            LastInvoiceDataRequestCommon objLastInvoiceDataRequestCommon =
                new LastInvoiceDataRequestCommon()
                {
                    audit = audit

                };

            try
            {
                objLastInvoiceDataResponseCommon =
                    Logging.ExecuteMethod<LastInvoiceDataResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetLastInvoiceData(objLastInvoiceDataRequestCommon, strCustomerCode);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objLastInvoiceDataRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            CommonServices objCommonServices = null;
            if (objLastInvoiceDataResponseCommon != null && objLastInvoiceDataResponseCommon.LastInvoiceDatas != null)
            {
                objCommonServices = new CommonServices();
                List<LastInvoiceData> listLastInvoiceData =
                    new List<LastInvoiceData>();

                foreach (Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.ReceiptPostPaid item in objLastInvoiceDataResponseCommon.LastInvoiceDatas)
                {
                    listLastInvoiceData.Add(new LastInvoiceData()
                    {
                        FECHA_VENCIMIENTO = item.FECHA_VENCIMIENTO,
                        MTO_ULT_FACTURA = item.MTO_ULT_FACTURA
                    });
                }
                objCommonServices.LastInvoiceDatas = listLastInvoiceData;
            }

            return Json(new { data = objCommonServices });
        }


        /// <summary>Método que obtiene datos del codigo postal al terminar de seleccionar el ubigeo</summary>
        ///<param name="variable"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>GetPostalCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>18/10/2018.</FecCrea></item></list>
        public JsonResult GetPostalCode(string variable)
        {
            try
            {
                string codDistrito = variable;
                string codpost = "";
                ArrayList codPostal = APPCODE.Common.GetXMLList("ListaCodigoPostal");
                bool found = false;
                foreach (FIXED.GenericItem item in codPostal)
                {
                    if (item.Codigo == codDistrito)
                    {
                        codpost = item.Descripcion;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    codpost = string.Empty;
                }
                return Json(new { data = codpost }); ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>Método que obtiene el número de recibo</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strTransactionID"></param>
        ///<param name="strInvoiceNumber"></param>
        ///<param name="strFechaEmision"></param>
        /// <returns>string</returns> 
        /// <remarks>GetNumberReceipt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>18/10/2018.</FecCrea></item></list>
        public static string GetNumberReceipt(string strIdSession, string strTransactionID, string strInvoiceNumber, string strFechaEmision)
        {
            string strNroReciboTemp = "";
            int intLongitud = Claro.Constants.NumberZero;
            string strParteNro = "";
            string sFechaEmisionCorta = "";
            string sFechaEmision = strFechaEmision.Trim();
            string strFechaCorte = Claro.SIACU.Constants.Receipt8;
            string strFechaCorte1 = Claro.SIACU.Constants.Receipt9;
            string lSalida = "";
            string lSalida7 = "";
            string lSalida8 = "";
            string lSalida9 = "";

            try
            {
                sFechaEmisionCorta = (sFechaEmision.Length > 8 ? sFechaEmision.Substring(6, 4) + sFechaEmision.Substring(3, 2) + sFechaEmision.Substring(0, 2) : sFechaEmision);
                strNroReciboTemp = strInvoiceNumber.Substring(0, strInvoiceNumber.Length - 6);
                intLongitud = strNroReciboTemp.Length;

                if (intLongitud >= 7)
                {
                    lSalida7 = Claro.SIACU.Constants.T001_ + strNroReciboTemp.Substring(intLongitud - 7);
                    lSalida8 = Claro.SIACU.Constants.T001_ + strNroReciboTemp.Substring(intLongitud - 8);
                    lSalida9 = Claro.SIACU.Constants.T001_ + strNroReciboTemp.Substring(0, 10);
                }
                else
                {
                    for (int i = 1; i <= 7 - intLongitud; i++) strParteNro = Claro.Constants.NumberZeroString + strParteNro;
                    lSalida7 = Claro.SIACU.Constants.T001_ + strParteNro + strNroReciboTemp;
                    for (int i = 1; i <= 8 - intLongitud; i++) strParteNro = Claro.Constants.NumberZeroString + strParteNro;
                    lSalida8 = Claro.SIACU.Constants.T001_ + strParteNro + strNroReciboTemp;
                }

                var comparacionFechas = string.Compare(sFechaEmisionCorta, strFechaCorte, StringComparison.InvariantCulture);

                if (comparacionFechas < 0)
                {
                    lSalida = lSalida7;
                }
                else
                {
                    comparacionFechas = string.Compare(sFechaEmisionCorta, strFechaCorte1, StringComparison.InvariantCulture);
                    lSalida = (comparacionFechas < 0 ? lSalida8 : lSalida9);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }

            return lSalida;
        }


        /// <summary>Método que obtiene la ruta del archivo especificado</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strTransactionID"></param>
        ///<param name="strInvoiceNumber"></param>
        ///<param name="strFechaEmision"></param>
        /// <returns>string</returns> 
        /// <remarks>GetFileRute</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>18/10/2018.</FecCrea></item></list>
        public JsonResult GetFileRute(string strIdSession, string strNumDocument, string DateIssue, string Period)
        {
            string sRutaFacturas = ConfigurationManager.AppSettings("strDirectorioFacturas");
            string sRutaArchivos = string.Empty;
            string sNroFactura = string.Empty;
            string sNombreArchivo = string.Empty;

            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest audit =
               APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);
            sNroFactura = GetNumberReceipt(strIdSession, audit.transaction, strNumDocument, DateIssue);

            sRutaArchivos = sRutaFacturas.Replace(@"\", "\\") + "\\";
            sNombreArchivo = Period.Trim() + "\\" + sNroFactura.Trim() + Claro.SIACU.Constants.PointPdf.ToLowerInvariant();


            return Json(new { data = sRutaArchivos + sNombreArchivo });
        }


        /// <summary>Método que obtiene datos del medio de entrega</summary>
        /// <returns>JsonResult</returns> 
        /// <remarks>GetDeliveryMedium</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>18/10/2018.</FecCrea></item></list>
        public JsonResult GetDeliveryMedium()
        {
            try
            {
                ArrayList listaMedio = APPCODE.Common.GetXMLList("MedioEntrega");

                return Json(new { data = listaMedio }); ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>Método que inserta el terminal para el bloqueo/desbloqueo de equipos</summary>
        ///<param name="isLock"></param>
        ///<param name="auditPost"></param>
        ///<param name="objModel"></param>
        /// <returns>int</returns> 
        /// <remarks>InsertTerminalLockUnlockEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>18/10/2018.</FecCrea></item></list>
        public int InsertTerminalLockUnlockEquipment(bool isLock, PostIFIService.AuditRequest auditPost, ServiceLockModel objModel)
        {
            InsertTerminalLockUnlockEquipmentRequestIFI objInsertTerminalLockUnlockEquipmentRequestIFI = new InsertTerminalLockUnlockEquipmentRequestIFI()
            {
                audit = auditPost,
                isTransactionLock = isLock,
                objTerminal = new Terminal()
                {
                    _strMarca = objModel.Marca,
                    _strNumeroIMEI = objModel.IMEI,
                    _strNumeroLinea = objModel.msisdn,
                    _strTipDocumento = objModel.strSolicitanteTipoDoc,
                    _strTipoMovimiento = objModel.TipoBloqueo,
                    _strReportante = "",
                    _strAsesorServicio = auditPost.userName,
                    _strModelo = ""

                }
            };

            InsertTerminalLockUnlockEquipmentResponseIFI objResponseTerminal =
                                         Claro.Web.Logging.ExecuteMethod<InsertTerminalLockUnlockEquipmentResponseIFI>(
                                        () =>
                                        {
                                            return _oServicePostpaid.InsertTerminalLockUnlockEquipment(objInsertTerminalLockUnlockEquipmentRequestIFI);
                                        });
            return objResponseTerminal.resTerminal;

        }

        /// <summary>Método que envía correo electrónico adjuntando un archivo</summary>
        /// <param name="objSendEmail"></param>
        /// <returns>string</returns> 
        /// <remarks>GetSendEmailConst2</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>21/03/2019.</FecCrea></item></list>
        public string GetSendEmailConst2(Model.SendEmailModel objSendEmail)
        {
            CommonIFIService.SendEmailWithBase64Request objSendEmailWithBase64Request = new CommonIFIService.SendEmailWithBase64Request();
            CommonIFIService.SendEmailWithBase64Response objSendEmailWithBase64Response = null;
            string strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
            CommonIFIService.AuditRequest AuditRequest = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objSendEmail.strIdSession);

            try
            {

                objSendEmailWithBase64Request.audit = AuditRequest;
                objSendEmailWithBase64Request.Sender = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                objSendEmailWithBase64Request.To = objSendEmail.strTo;
                objSendEmailWithBase64Request.HtmlFlag = KEY.AppSettings("strHtmlFlagCorreoSB").ToString();
                objSendEmailWithBase64Request.Subject = objSendEmail.strSubject;
                objSendEmailWithBase64Request.Message = TemplateEmail(objSendEmail.strMessage, objSendEmail.strMsgEmailCall);

                List<CommonIFIService.AttachedFile> lstAdjuntos = new List<AttachedFile>();
                AttachedFile objFile = new AttachedFile();
                string nombre = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailAfilRecibo",
                    ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")).ToString();
                objFile.file = objSendEmail.byteAttached;
                objFile.Name = objSendEmail.strAttached;
                objFile.HeadBoard = objSendEmail.strAttached;
                lstAdjuntos.Add(objFile);

                if (lstAdjuntos != null && lstAdjuntos.Count > 0)
                {
                    objSendEmailWithBase64Request.ListAttachedFile = lstAdjuntos;
                }

                objSendEmailWithBase64Response = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailWithBase64Response>(() =>
                {
                    return _oServiceCommon.SendEmailWithBase64(objSendEmailWithBase64Request);
                });
                if (objSendEmailWithBase64Response != null && objSendEmailWithBase64Response.ResponseCode == Claro.Constants.NumberZeroString)
                {
                    strResul = Claro.Utils.GetValueFromConfigFileIFI("strMensajeEnvioOK", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));


                }
                else
                {
                    strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSendEmail.strIdSession, objSendEmailWithBase64Request.audit.transaction, ex.Message);

            }

            return strResul;
        }


        /// <summary>Método que obtiene un archivo(Factura) desde una ruta</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strFilePath"></param>
        /// <returns>FileContentResult</returns> 
        /// <remarks>ShowRecordSharedInvoice</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>26/03/2019.</FecCrea></item></list>
        public FileContentResult ShowRecordSharedInvoice(string strIdSession, string strFilePath)
        {
            CommonService.AuditRequest objAudit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            byte[] databytes;
            string strContenType = string.Empty;
            try
            {
                string strExtentionFile = Path.GetExtension(strFilePath);
                strContenType = Claro.Utils.f_obtieneContentType(strExtentionFile);
                bool dt = false;
                dt = DisplayFileFromServerSharedInvoice(strIdSession, objAudit.transaction, strFilePath, out databytes);
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objAudit.transaction, Claro.Utils.GetExceptionMessage(ex));
                databytes = null;
            }

            return File(databytes, strContenType);
        }

        /// <summary>Método que verifica si existe un archivo(Factura) en el servidor compartido y como parámetro de salida devuelve el archivo en Bytes</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strTransaction"></param>
        ///<param name="strPath"></param>
        ///<param name="mydata"></param>
        /// <returns>bool</returns> 
        /// <remarks>DisplayFileFromServerSharedInvoice</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>26/03/2019.</FecCrea></item></list>
        public bool DisplayFileFromServerSharedInvoice(string strIdSession, string strTransaction, string strPath, out byte[] mydata)
        {
            bool blnResult = false;
            mydata = null;
            CommonIFIService.FileDefaultImpersonationResponseCommon objFileDefaultImpersonationResponse = null;
            CommonIFIService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            try
            {
                CommonIFIService.FileDefaultImpersonationRequestCommon objFileDefaultImpersonationRequestDashboard = new CommonIFIService.FileDefaultImpersonationRequestCommon()
                {
                    strPath = strPath,
                    audit = objAudit
                };

                objFileDefaultImpersonationResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.FileDefaultImpersonationResponseCommon>(1, () => { return new CommonIFIService.CommonIFIServiceClient().GetfileDefaultImpersonation(objFileDefaultImpersonationRequestDashboard); });


                if (objFileDefaultImpersonationResponse != null && objFileDefaultImpersonationResponse.objGlobalDocument != null && objFileDefaultImpersonationResponse.objGlobalDocument.Document != null)
                {
                    blnResult = true;
                    mydata = objFileDefaultImpersonationResponse.objGlobalDocument.Document;
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));

                mydata = null;
                blnResult = false;
            }
            return blnResult;
        }
        /// <summary>Método que verifica si existe un archivo(Factura) en la ruta especificada.</summary>
        ///<param name="strFilePath"></param>
        ///<param name="strIdSession"></param>
        /// <returns>JsonResult</returns> 
        /// <remarks>ExistFileSharedInvoice</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>26/03/2019.</FecCrea></item></list>
        public JsonResult ExistFileSharedInvoice(string strFilePath, string strIdSession)
        {
            CommonService.AuditRequest objAudit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            bool ExistFile = false;
            byte[] byteDataFile;

            try
            {
                ExistFile = DisplayFileFromServerSharedInvoice(strIdSession, objAudit.transaction, strFilePath, out byteDataFile);

                byteDataFile = null;
            }
            catch (Exception ex)
            {
                ExistFile = false;

                Logging.Error(strIdSession, objAudit.transaction, Claro.Utils.GetExceptionMessage(ex));
            }
            return Json(new { Exist = ExistFile }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetStatusLineMsgValidate(string strIDSession, string strTransaction, int intValidationType, string strStatusLine)
        {
            string strResponse = string.Empty;

            if (strStatusLine == null)
            {
                Logging.Error(strIDSession, strTransaction, "La variable strStatusLine está vacía o nula.");
                return Json(new { data = strResponse }, JsonRequestBehavior.AllowGet);
            }

            switch (intValidationType)
            {
                case 1:
                    strResponse = Claro.Utils.GetValueFromConfigFileIFI("strMsgValidacionContratoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                    break;
                case 2:
                    if (strStatusLine.Trim().ToUpper() == Claro.Utils.GetValueFromConfigFileIFI("strEstadoContratoSuspendido", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")).Trim().ToUpper())
                    {
                        strResponse = Claro.Utils.GetValueFromConfigFileIFI("strMsgValidacionContratoSuspendido", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                    }
                    break;
                case 3:
                    if (strStatusLine.Trim().ToUpper() == Claro.Utils.GetValueFromConfigFileIFI("strEstadoContratoReservado", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")).Trim().ToUpper())
                    {
                        strResponse = Claro.Utils.GetValueFromConfigFileIFI("strMsgValidacionContratoReservado", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                    }
                    break;
                case 4:

                    if (strStatusLine.Trim().ToUpper() == Claro.Utils.GetValueFromConfigFileIFI("strEstadoContratoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")).Trim().ToUpper())
                    {
                        strResponse = Claro.Utils.GetValueFromConfigFileIFI("strMsgValidacionContratoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                    }
                    else if (strStatusLine.Trim().ToUpper() == Claro.Utils.GetValueFromConfigFileIFI("strEstadoContratoReservado", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")).Trim().ToUpper())
                    {
                        strResponse = Claro.Utils.GetValueFromConfigFileIFI("strMsgValidacionContratoReservado", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                    }
                    break;
                default:
                    if (strStatusLine.Trim().ToUpper() == Claro.Utils.GetValueFromConfigFileIFI("strEstadoContratoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")).Trim().ToUpper())
                    {
                        strResponse = Claro.Utils.GetValueFromConfigFileIFI("strMsgValidacionContratoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                    }
                    else if (strStatusLine.Trim().ToUpper() == Claro.Utils.GetValueFromConfigFileIFI("strEstadoContratoSuspendido", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")).Trim().ToUpper())
                    {
                        strResponse = Claro.Utils.GetValueFromConfigFileIFI("strMsgValidacionContratoSuspendido", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                    }
                    else if (strStatusLine.Trim().ToUpper() == Claro.Utils.GetValueFromConfigFileIFI("strEstadoContratoReservado", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")).Trim().ToUpper())
                    {
                        strResponse = Claro.Utils.GetValueFromConfigFileIFI("strMsgValidacionContratoReservado", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                    }
                    break;
            }

            Logging.Info(strIDSession, strTransaction, "El estado de la línea es: " + strStatusLine + " y el mensaje: " + strResponse);

            return Json(new { data = strResponse }, JsonRequestBehavior.AllowGet);
        }

        public string GetPointOfAttentionByUser(string strIDSession, string strUser)
        {

            var strCac = string.Empty;

            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.UserResponse oUserResponse = null;

            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIDSession);

            Claro.Web.Logging.Info(strIDSession, audit.transaction, "strCodeUser: " + strUser);

            var oUserRequest = new Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.UserRequest();
            oUserRequest.audit = audit;

            oUserRequest.CodeUser = strUser;
            oUserRequest.CodeRol = Claro.Constants.NumberOneNegativeString;
            oUserRequest.CodeCac = Claro.Constants.NumberOneNegativeString;
            oUserRequest.State = Claro.Constants.NumberOneNegativeString;
            try
            {
                oUserResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.UserResponse>(() =>
                {
                    return _oServiceCommon.GetUser(oUserRequest);
                });
                strCac = oUserResponse.UserModel.Cac;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIDSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            return strCac;
        }

        public List<ItemGeneric> GetBusinessRulesLst(string strIDSession, string strSubClase)
        {
            var lstBussinesRules = new List<ItemGeneric>();

            var objBusinessRulesResponse = new Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.BusinessRulesResponse();
            var audit = APPCODE.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIDSession);
            var objBusinessRulesRequest = new Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.BusinessRulesRequest();
            objBusinessRulesRequest.audit = audit;
            objBusinessRulesRequest.SUB_CLASE = strSubClase;
            Claro.Web.Logging.Info(strIDSession, audit.transaction, "Parámetro strSubClase : " + strSubClase);
            try
            {
                objBusinessRulesResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.BusinessRulesResponse>(() =>
                {
                    return _oServiceCommon.GetBusinessRules(objBusinessRulesRequest);
                });
                if (objBusinessRulesResponse != null && objBusinessRulesResponse.ListBusinessRules != null)
                {
                    foreach (var item in objBusinessRulesResponse.ListBusinessRules)
                    {
                        lstBussinesRules.Add(new ItemGeneric()
                        {
                            Description = item.REGLA
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIDSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info("IdSession: " + strIDSession, "Método : GetBusinessRules", "Fín");
            return lstBussinesRules;
        }

        public ConsultIGVModel GetCommonConsultIgv(string strIdSession)
        {
            var oConsultIgv = new ConsultIGVModel();

            CommonIFIService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

            var objRequest = new ConsultIGVRequest
            {
                audit = audit,
                SessionId = strIdSession,
                TransactionId = audit.transaction,
                AppId = audit.ipAddress,
                AppName = audit.applicationName,
                Username = audit.userName
            };

            try
            {
                var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetConsultIGV(objRequest));

                if (objResponse != null && objResponse.ListConsultIGV.Count > 0)
                {
                    DateTime currentDate = DateTime.Now;
                    foreach (var item in objResponse.ListConsultIGV)
                    {
                        if (Convert.ToDate(item.impudFecIniVigencia) <= currentDate &&
                            Convert.ToDate(item.impudFecFinVigencia) >= currentDate &&
                            item.impunTipDoc == Claro.Constants.NumberZeroString)
                        {
                            oConsultIgv = new ConsultIGVModel
                            {
                                igv = item.igv,
                                igvD = item.igvD,
                                impudFecFinVigencia = item.impudFecFinVigencia,
                                impudFecIniVigencia = item.impudFecIniVigencia,
                                impudFecRegistro = item.impudFecRegistro,
                                impunTipDoc = item.impunTipDoc,
                                imputId = item.imputId,
                                impuvDes = item.impuvDes
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
            }

            Logging.Info(strIdSession, objRequest.audit.transaction, "oConsultIGV.igv: " + oConsultIgv.igv.ToString());
            return oConsultIgv;
        }

        public Office GetOffice(string strIdSession, string strCodeUser)
        {
            var audit = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            var objResponse = new OfficeResponseCommon();
            var objRequest = new OfficeRequestCommon();
            objRequest.audit = audit;
            objRequest.strCodeUser = strCodeUser;
            try
            {
                objResponse = Logging.ExecuteMethod<OfficeResponseCommon>(() =>
                {
                    return _oServiceCommon.GetOffice(objRequest);
                });
                Logging.Info(strIdSession, audit.transaction, "Ejecutó correctamente GetOffice");
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
            }
            if (objResponse.objOffice == null)
            {
                objResponse.objOffice = new Office();
            }
            if (objResponse.objOffice.strCodeOffice == null)
            {
                Logging.Info(strIdSession, audit.transaction, "strCodeOffice es nulo.");
                objResponse.objOffice.strCodeOffice = ConfigurationManager.AppSettings("strPuntoVentaDefault");
            }
            else if (objResponse.objOffice.strCodeOffice == string.Empty)
            {
                Logging.Info(strIdSession, audit.transaction, "strCodeOffice vacio");
                objResponse.objOffice.strCodeOffice = ConfigurationManager.AppSettings("strPuntoVentaDefault");
            }
            return objResponse.objOffice;
        }

        public bool Authorize_TX(string strIdSession, string transaction, string pUsuario, string pClave, string strKey)
        {
            var resultado = IsAuthenticated_LDAP(strIdSession, transaction, pUsuario, pClave);
            //resultado = true;
            var response = false;
            try
            {
                if (resultado)
                {
                    var usuario = string.Empty;
                    var codApp = KEY.AppSettings("CodAplicacion_SIACPO");
                    var ipApp = KEY.AppSettings("strWebIpCod_SIACPO");
                    var nomApp = KEY.AppSettings("NombreAplicacion_SIACPO");

                    CommonService.AuditRequest audit =
                        APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
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

                    CommonService.AuditRequest audit =
                        APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
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
                var currentWindowsIdentity = (WindowsIdentity)System.Web.HttpContext.Current.User.Identity;
                var impersonationContext = currentWindowsIdentity.Impersonate();
                impersonationContext.Undo();

                var audit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
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
                var currentWindowsIdentity = (WindowsIdentity)System.Web.HttpContext.Current.User.Identity;
                var impersonationContext = currentWindowsIdentity.Impersonate();
                impersonationContext.Undo();

                var audit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
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

        public string GetAccessOfPage(string strIdSession)
        {
            var model = new Model.ConsultSecurityModel();
            model.ListConsultSecurity = new List<Model.SecurityModel>();
            var strKey = string.Empty;

            int strCodeApp = Claro.Convert.ToInt(ConfigurationManager.AppSettings("CodAplicacion_SIACPO"));

            CommonService.PagOptionXuserResponse objPagOptionXuserResponse = null;
            CommonService.AuditRequest audit = APPCODE.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            CommonService.PagOptionXuserRequest objPagOptionXuserRequest = new CommonService.PagOptionXuserRequest()
            {
                audit = audit,
                IntUser = Claro.Convert.ToInt(strIdSession),
                IntAplicationCode = strCodeApp
            };

            try
            {
                objPagOptionXuserResponse = Logging.ExecuteMethod<CommonService.PagOptionXuserResponse>(() =>
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
            model.ListConsultSecurity = Mapper.Map<List<Model.SecurityModel>>(tempLst);

            model.ListConsultSecurity.ForEach(x =>
            {
                strKey = strKey + x.Opcicabrev + ",";
            });

            return strKey.ToUpper();
        }

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
                Claro.Utils.GetValueFromConfigFileIFI("JerarquiaPerfiles", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
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
                "ESPERANDO A SESSION";

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
                    if (Claro.Convert.ToInt(hidVeces) > 1)
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
                        ListValueXmlMethod_Auth(strIdSession, transaccion, ConfigurationManager.AppSettings("strDataListArchive"),
                            "ListaValidaMontoXTransaccion", Claro.Constants.NumberOneString) + strConcepto);

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
                    var strRes = string.Empty;
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
                        strRes = Claro.SIACU.Constants.ConstNoAutorizado;
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

                    if (hidPagDCM == Claro.Utils.GetValueFromConfigFileIFI("strConstDebCrdManual", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
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
                        ListValueXmlMethod_Auth(strIdSession, transaccion, "SIACPODatosPos.xml",
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
                        hidAccion = Claro.Utils.GetValueFromConfigFileIFI("strConstActDesServicios", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                        dictionaryValidateUser.Add("hidAccion", hidAccion);

                        if (hidOpcion == Claro.Utils.GetValueFromConfigFileIFI("strConstValidaFidelizaApadece", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" + Claro.Utils.GetValueFromConfigFileIFI("strConstUsrValFA", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Claro.Utils.GetValueFromConfigFileIFI("strConstFechaProgMigracion", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        { 
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" + Claro.Utils.GetValueFromConfigFileIFI("strConstUsrValPF", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Claro.Utils.GetValueFromConfigFileIFI("strConstValidaProgramarFecha", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" + Claro.Utils.GetValueFromConfigFileIFI("strConstUsrValPF", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Claro.Utils.GetValueFromConfigFileIFI("strConstFidelizaMigracion", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        { 
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" + Claro.Utils.GetValueFromConfigFileIFI("strConstUsrValPCHECK", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        } 
                        else if (hidOpcion == Claro.Utils.GetValueFromConfigFileIFI("strConstActivRetencion", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        { 
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" + Claro.Utils.GetValueFromConfigFileIFI("strConstUsrValACTRETCHK", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        } 
                        else if (hidOpcion == Claro.Utils.GetValueFromConfigFileIFI("strConstModFechaProgMigraPlan", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        { 
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" + Claro.Utils.GetValueFromConfigFileIFI("strConstUsrValPF", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Claro.Utils.GetValueFromConfigFileIFI("strConstFidelizaPenalidadMigraPlan", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")))
                        { 
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" + Claro.Utils.GetValueFromConfigFileIFI("strConstUsrValPCHECK", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
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
                        hidAccion = Claro.Utils.GetValueFromConfigFileIFI("strConstUsrValTN", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }
            }

            var userValidatorLog = GetEmployerDate(strIdSession, sUsuario);
            if (userValidatorLog != null)
            {
                dictionaryValidateUser.Add("NamesUserValidator",
               userValidatorLog.strNomb + " " + userValidatorLog.strApPat + ' ' + userValidatorLog.strApMat);
                dictionaryValidateUser.Add("EmailUserValidator", userValidatorLog.strEmail);
            }
            else
            {
                dictionaryValidateUser.Add("NamesUserValidator", string.Empty);
                dictionaryValidateUser.Add("EmailUserValidator", string.Empty);
            }
           

            return new JsonResult
            {
                Data = dictionaryValidateUser,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public string ListValueXmlMethod_Auth(string strIdSession, string transaccion, string fileName, string strNameFunction, string strFlagCode)
        {
            var lstItemVm = new List<HelperCommon.ListItemVM>();
            var responseMethod = string.Empty;
            var objAuditRequest = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

            try
            {

                var objListItemResponse = Claro.Utils.GetListValuesXMLIFI(strNameFunction, strFlagCode, fileName);

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
                    ListValueXmlMethod_Auth(strIdSession, transaccion, ConfigurationManager.AppSettings("strDataListArchive"), "ListaValidaMontoXTransaccion",
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

        public bool LocalAuthorization(string strIdSession, string pUsuario, string strKey)
        {
            var resultado = false;
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

        public static List<string> GetXmlToString(string filename)
        {

            try
            {
                XDocument xdoc = XDocument.Load(filename);
                List<string> listadios = (from o in xdoc.Root.Descendants()
                                          select o.Name.ToString()).ToList();

                return listadios;

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
    }
}
