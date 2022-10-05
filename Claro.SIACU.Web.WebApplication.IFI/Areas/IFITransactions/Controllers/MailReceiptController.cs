
using Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using Claro.SIACU.Web.WebApplication.IFI.PostIFIService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common = Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using KEY = Claro.ConfigurationManager;
using Model = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.AuditRequest;
using Claro.Web;
using Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class MailReceiptController : Controller
    {

        public readonly CommonIFIService.CommonIFIServiceClient oCommonIFIService = new CommonIFIService.CommonIFIServiceClient();
        public readonly PostIFIService.PostIFIServiceClient oPostPaidIFIService = new PostIFIService.PostIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient _oServiceFixed = new Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient oServicePostpaid = new PostIFIService.PostIFIServiceClient();


        ///<summary>Metodo que retorna la vista del controlador</summary>
        ///<returns>View<returns>
        ///<remarks>MailReceipt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        public ActionResult MailReceipt()
        {
            return View();
        }


        ///<summary>Metodo que se ejecuta al cargar la vista</summary>
        ///<param name="strIdSession">/param>
        ///<returns>Json<returns>
        ///<remarks>PageLoad</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public JsonResult PageLoad()
        {
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>("strIdSession");
            return Json(new { data = "" });
        }

        #region Métodos Inicio de Pagina

        ///<summary>Metodo que retorna la tipifación de afiliación por correo electrónico IFI en formato json  </summary>
        ///<param name="strIdSession">/param>
        ///<param name="strStateEmail">/param>
        ///<returns>JsonResult<returns>
        ///<remarks>getTypification</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public JsonResult getTypification(string strIdSession, string strStateEmail)
        {
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

            Common.Typification typificationLoad = null;
            string lblMensaje = "";
            try
            {
                typificationLoad = LoadTypification(audit, ref lblMensaje, strStateEmail);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }
            return Json(new { data = typificationLoad });

        }


        ///<summary>Metodo necesario para obtener la tipificacion de afiliación por correo electrónico IFI</summary>
        ///<param name="audit"></param>
        ///<param name="lblMensaje"></param>
        ///<param name="strStateEmail"></param>
        ///<returns>Common.Typification<returns>
        ///<remarks>LoadTypification</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public Common.Typification LoadTypification(CommonIFIService.AuditRequest audit,
                                                            ref string lblMensaje, string strStateEmail)
        {
            Common.Typification oTypification = null;




            try
            {
                Common.TypificationRequest objTypificationRequest = new Common.TypificationRequest();
                objTypificationRequest.TRANSACTION_NAME = strStateEmail == "A" ? KEY.AppSettings("TRANSACCION_RCB_EMAIL_IFI") : KEY.AppSettings("TRANSACCION_RCBO_FISC_IFI");
                objTypificationRequest.audit = audit;

                Common.TypificationResponse objResponse =
                Claro.Web.Logging.ExecuteMethod<Common.TypificationResponse>(
                    () => { return oCommonIFIService.GetTypification(objTypificationRequest); });
                if (objResponse.ListTypification != null)
                {
                    oTypification = objResponse.ListTypification.First();
                }
                else
                {

                    lblMensaje = "No se cargo las tipificaciones";
                }



                if (oTypification == null)
                {

                    lblMensaje = "No se cargo las tipificaciones";
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                lblMensaje = "No se cargo las tipificaciones";
            }

            return oTypification;
        }



        ///<summary>Metodo para obtener el listado de acciones que se pueden realizar</summary>
        ///<param name="strIdSession"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>GetAction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public JsonResult GetAction(string strIdSession)
        {
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            ArrayList lstGenericItem = new ArrayList();

            try
            {
                lstGenericItem = App_Code.Common.GetXMLList("ListaAccionServicios");


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, audit.transaction, ex.Message);
            }

            return Json(new { data = lstGenericItem });
        }


        ///<summary>Metodo para validar el estado de la afiliación al recibo por correo electrónico</summary>
        ///<param name="strIdSession"></param>
        ///<param name="CustomerCode"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>ValidateEmail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public JsonResult ValidateEmail(string strIdSession, string CustomerCode)
        {
            string validaEmail = string.Empty;

            CommonIFIService.ValidateMailResponse objValidateMailResponse = null;
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            CommonIFIService.ValidateMailRequest objValidateMailRequest = new CommonIFIService.ValidateMailRequest()
            {
                audit = audit,
                CustomerCode = CustomerCode,

            };


            try
            {

                objValidateMailResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.ValidateMailResponse>(
                    () => { return oCommonIFIService.GetValidateMail(objValidateMailRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, audit.transaction, ex.Message);
            }

            if (objValidateMailResponse != null)
            {
                validaEmail = objValidateMailResponse.strValidaCorreo;
            }
            else
            {
                validaEmail = "";
            }

            return Json(new { data = validaEmail });
        }

        #endregion

        #region Registro de Afiliación al recibo
        ///<summary>Metodo necesario para afiliar el correo electrónico</summary>
        ///<param name="strIdSession"></param>
        ///<param name="pCuenta"></param>
        ///<param name="pCustomerID"></param>
        ///<param name="pEmail"></param>
        ///<param name="pFlag"></param>
        ///<param name="pNumCla"></param>
        ///<param name="pObjID"></param>
        ///<param name="pTelConfSMS"></param>
        ///<param name="pTelRef"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>GetActivateServiceMail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>XX/XX/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public JsonResult GetActivateServiceMail(string strIdSession, string pCuenta, string pCustomerID,
                                                 string pEmail, string pFlag,
                                                 string pNumCla, string pObjID, string pTelConfSMS,
                                                 string pTelRef, string Opcion, string email,
                                                 string dominio, Model.MailReceiptModel oModel)
        {
            string[] arrString = null;
            string vDesInteraction = "", vFlagInteraction = "", vInteractionId = "", strRutaArchivo = "", MensajeEmail = "";
            MailReceiptModel objMailReceiptModel = new MailReceiptModel();
            PostIFIService.GetActivateServiceMailIFIResponse objGetActivateServiceMailIFIResponse = null;

            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession);
            PostIFIService.GetActivateServiceMailIFIRequest objGetActivateServiceMailIFIRequest = new PostIFIService.GetActivateServiceMailIFIRequest()
            {
                audit = audit,
                pCuenta = pCuenta,
                pCustomerID = pCustomerID,
                pEmail = pEmail,
                pFlag = pFlag,
                pNumCla = pNumCla,
                pObjID = pObjID,
                pTelConfSMS = pTelConfSMS,
                pTelRef = pTelRef

            };
            try
            {
                if (Opcion == "A")
                {

                    objGetActivateServiceMailIFIResponse = Claro.Web.Logging.ExecuteMethod<PostIFIService.GetActivateServiceMailIFIResponse>(
                        () => { return oPostPaidIFIService.GetActivateServiceMail(objGetActivateServiceMailIFIRequest); });

                    if (objGetActivateServiceMailIFIResponse.strResult == "OK")
                    {
                        objMailReceiptModel.Email = email + "@" + dominio;
                        objMailReceiptModel.flagact = "A";
                        objMailReceiptModel.isEnabled = true;

                        arrString = SaveInteractionMail(oModel);
                        objGetActivateServiceMailIFIRequest.pFlag = objMailReceiptModel.flagact;
                    }



                }
                else
                {
                    objGetActivateServiceMailIFIResponse = Claro.Web.Logging.ExecuteMethod<PostIFIService.GetActivateServiceMailIFIResponse>(
                        () => { return oPostPaidIFIService.GetActivateServiceMail(objGetActivateServiceMailIFIRequest); });
                    if (objGetActivateServiceMailIFIResponse.strResult == "OK")
                    {

                        objMailReceiptModel.Email = email + "@" + dominio;
                        objMailReceiptModel.flagact = "D";
                        objMailReceiptModel.isEnabled = false;

                        arrString = SaveInteractionMail(oModel);



                    }
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
            }
            if (arrString != null && arrString.Length > 0)
            {
                vDesInteraction = arrString[0];
                vFlagInteraction = arrString[1];
                vInteractionId = arrString[2];
                strRutaArchivo = arrString[3];
                MensajeEmail = arrString[4];
            }

            return Json(new { vDesInteraction, vFlagInteraction, vInteractionId, strRutaArchivo, MensajeEmail, objMailReceiptModel });
        }
        #endregion

        #region Interacción
        ///<summary>Metodo que registra el cambio al tipo de afiliación al recibo</summary>
        ///<param name="oModel"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>SaveInteractionMail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public string[] SaveInteractionMail(Model.MailReceiptModel oModel)
        {
            string vInteractionId = string.Empty;
            string vFlagInteraction = string.Empty;
            string vDesInteraction = string.Empty;

            string strRutaArchivo = string.Empty;
            string MensajeEmail = string.Empty;
            string strNombreArchivo = string.Empty;
            bool env = false;
            oModel.fechaActual = DateTime.Now.ToShortDateString();

            #region GrabaInteraccion
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.IdSession);


            List<string> strInteractionId = GrabaInteraccion(oModel);
            vDesInteraction = strInteractionId[0].ToString();
            vFlagInteraction = strInteractionId[2].ToString();
            vInteractionId = strInteractionId[3].ToString();
            #endregion

            if (vDesInteraction == Claro.SIACU.Constants.OK)
            {

                #region CONSTANCY PDF
                vDesInteraction = ConfigurationManager.AppSettings("strMsgTranGrabSatis");



                GetConstancyPDF(oModel.IdSession, vInteractionId, oModel);
                strRutaArchivo = oModel.strFullPathPDF;
                env = true;
                #endregion


            }
            else
            {
                vDesInteraction = ConfigurationManager.AppSettings("strMensajeDeError");

            }


            if (oModel.EmailCondstate && env == true)
            {
                #region SEND EMAIL
                byte[] attachFile = null;
                string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                if (new CommonServicesController().DisplayFileFromServerSharedFile(oModel.IdSession, audit.transaction, strRutaArchivo, out attachFile))
                {
                    List<CommonIFIService.AttachedFile> lstAdjuntos = new List<AttachedFile>();

                    if (attachFile != null)
                    {
                        AttachedFile objFile = new AttachedFile();
                        string nombre = String.Empty;
                        if (oModel.StateMail == "A")
                        {
                            nombre = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailAfilRecibo",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")).ToString();
                        }
                        else if (oModel.StateMail == "D")
                        {
                            nombre = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailDesafilRecibo",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")).ToString();
                        }

                        objFile.file = attachFile;
                        objFile.Name = nombre + ".pdf";
                        objFile.HeadBoard = nombre;
                        lstAdjuntos.Add(objFile);
                        if (GetSendEmail2(oModel, lstAdjuntos) == Claro.SIACU.Constants.OK)
                        {
                            MensajeEmail = "Se enviará un correo de notificación.";
                        }
                        else
                        {
                            MensajeEmail = "Pero no se pudo procesar el envío del correo de notificación.";
                        }
                    }
                    else
                    {
                        MensajeEmail = "Pero no se pudo procesar el envío del correo de notificación.";
                    }
                }
                else
                {
                    MensajeEmail = "No se envió un correo de notificación.";
                }
                #endregion
            }
            else
            {
                MensajeEmail = "No se envió un correo de notificación.";
            }


            #region AUDIT

            Auditoria(oModel);

            #endregion

            string[] arrString = { vDesInteraction, vFlagInteraction, vInteractionId, strRutaArchivo, MensajeEmail };
            return arrString;
        }


        ///<summary>Metodo que registra la interacción en la BD al cambio al tipo de afiliación al recibo</summary>
        ///<param name="oModel"></param>
        ///<returns>List<returns>
        ///<remarks>GrabaInteraccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public List<string> GrabaInteraccion(Model.MailReceiptModel oModel)
        {

            var strUserSession = string.Empty;
            var strUserAplication = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            var strPassUser = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");
            var strNroTelephone = oModel.Telephone;
            var oPlantillaDat = new Model.TemplateInteractionModel();
            var lstaDatTemplate = new List<string>();
            Model.InteractionModel oInteraccion = new Model.InteractionModel();


            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.IdSession);

            try
            {

                oInteraccion = DatosInteraccion(oModel);

                oPlantillaDat = GetDataTemplateInteraction(oModel);

                var resultInteraction = InsertInteractionMail(oInteraccion, oPlantillaDat, strNroTelephone, strUserSession, strUserAplication, strPassUser, true, oModel.IdSession, oModel.CustomerId);

                foreach (KeyValuePair<string, object> par in resultInteraction)
                {
                    lstaDatTemplate.Add(par.Value.ToString());
                }

                if (lstaDatTemplate[0] != Claro.SIACU.Constants.OK && lstaDatTemplate[3] == string.Empty)
                {

                    Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
                    throw new Exception(Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));

                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);

            }

            return lstaDatTemplate;
        }


        ///<summary>Metodo que obtiene los datos de la plantilla interacción</summary>
        ///<param name="oModel"></param>
        ///<returns>Model.TemplateInteractionModel<returns>
        ///<remarks>GetDataTemplateInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public Model.TemplateInteractionModel GetDataTemplateInteraction(Model.MailReceiptModel oModel)
        {
            var oPlantCampDat = new Model.TemplateInteractionModel();

            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.IdSession);

            try
            {

                oPlantCampDat.NOMBRE_TRANSACCION = oModel.StateMail == "A" ? KEY.AppSettings("TRANSACCION_RCB_EMAIL_IFI") : KEY.AppSettings("TRANSACCION_RCBO_FISC_IFI");

                oPlantCampDat.X_INTER_20 = ConfigurationManager.AppSettings("strHPFormatoTrans_AfiliaReciboFisico");
                //fecha afiliacion
                oPlantCampDat.X_INTER_21 = DateTime.Now.ToShortDateString();
                //Nombres Apellidos
                oPlantCampDat.X_INTER_29 = oModel.NameComplet;
                oPlantCampDat.X_INTER_17 = oModel.StateMail == "A" ? "Activación" : "Desactivación";
                oPlantCampDat.X_TYPE_DOCUMENT = oModel.TypeDoc;
                oPlantCampDat.X_DOCUMENT_NUMBER = oModel.NroDoc;
                oPlantCampDat.X_CLARO_NUMBER = oModel.Telephone;
                ////Datos Direccion
                if (!string.IsNullOrEmpty(oModel.AdressDespatch)) oPlantCampDat.X_ADDRESS = oModel.AdressDespatch;
                else oPlantCampDat.X_ADDRESS = "";
                if (!string.IsNullOrEmpty(oModel.Reference)) oPlantCampDat.X_REFERENCE_ADDRESS = oModel.Reference;
                else oPlantCampDat.X_REFERENCE_ADDRESS = "";
                if (!string.IsNullOrEmpty(oModel.Departament_Fact)) oPlantCampDat.X_DEPARTMENT = oModel.Departament_Fact;
                else oPlantCampDat.X_DEPARTMENT = "";
                if (!string.IsNullOrEmpty(oModel.Provincia)) oPlantCampDat.X_CITY = oModel.Provincia;
                else oPlantCampDat.X_CITY = "";
                if (!string.IsNullOrEmpty(oModel.District)) oPlantCampDat.X_DISTRICT = oModel.District;
                else oPlantCampDat.X_DISTRICT = "";

                oPlantCampDat.X_EMAIL = oModel.Email;
                oPlantCampDat.X_REFERENCE_PHONE = oModel.TelephoneSMS;
                if (!string.IsNullOrEmpty(oModel.CacDac)) oPlantCampDat.X_INTER_15 = oModel.CacDac;
                else oPlantCampDat.X_INTER_15 = "";
                if (oModel.EmailCondstate == true)
                {
                    oPlantCampDat.X_INTER_1 = oModel.EmailCond;
                }
                else
                {
                    oPlantCampDat.X_INTER_1 = "";
                }

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, ex.Message);
            }

            return oPlantCampDat;

        }


        ///<summary>Metodo que obtiene los datos de la interacción</summary>
        ///<param name="oModel"></param>
        ///<returns>Model.InteractionModel<returns>
        ///<remarks>DatosInteraccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public Model.InteractionModel DatosInteraccion(Model.MailReceiptModel oModel)
        {

            var oInteraccion = new Model.InteractionModel();
            var typification = new Typification();
            string lblMensaje = "";
            var Interaction = new Model.InteractionModel();
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(oModel.IdSession);


            try
            {
                // Get Datos de la Tipificacion

                var strNroTelephone = oModel.Telephone;
                oInteraccion.ObjidContacto = oModel.ObjIdContact;
                oInteraccion.DateCreaction = Convert.ToString(DateTime.Now);
                oInteraccion.Telephone = oModel.Telephone;
                typification = LoadTypification(audit, ref lblMensaje, oModel.StateMail);
                oInteraccion.Type = typification.TIPO;
                oInteraccion.Class = typification.CLASE;
                oInteraccion.SubClass = typification.SUBCLASE;
                oInteraccion.ClassCode = typification.CLASE_CODE;
                oInteraccion.SubClassCode = typification.SUBCLASE_CODE;
                oInteraccion.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
                oInteraccion.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                oInteraccion.Result = ConfigurationManager.AppSettings("Ninguno");
                oInteraccion.MadeOne = Claro.Constants.NumberZeroString;
                oInteraccion.Note = oModel.Note;
                oInteraccion.Contract = oModel.ContractId;
                oInteraccion.FlagCase = Claro.Constants.NumberZeroString;
                oInteraccion.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                oInteraccion.Agenth = oModel.CurrentUser;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
            }


            return oInteraccion;

        }


        ///<summary>Metodo que inserta los datos enviados en la interacción</summary>
        ///<param name="objInteractionModel"></param>
        ///<param name="oPlantillaDat"></param>
        ///<param name="strNroTelephone"></param>
        ///<param name="strUserSession"></param>
        ///<param name="strUserAplication"></param>
        ///<param name="strPassUser"></param>
        ///<param name="boolEjecutTransaction"></param>
        ///<param name="strIdSession"></param>
        ///<param name="strCustomerId"></param>
        ///<returns>Dictionary<returns>
        ///<remarks>InsertInteractionMail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>XX/XX/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public Dictionary<string, object> InsertInteractionMail(Model.InteractionModel objInteractionModel,
                                                     Model.TemplateInteractionModel oPlantillaDat,
                                                     string strNroTelephone,
                                                     string strUserSession,
                                                     string strUserAplication,
                                                     string strPassUser,
                                                     bool boolEjecutTransaction,
                                                     string strIdSession,
                                                     string strCustomerId)
        {
            string contingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");

            var strTelefono = (strNroTelephone == objInteractionModel.Telephone) ? strNroTelephone : objInteractionModel.Telephone;

            //Obtener Cliente
            string strFlgRegistrado = Claro.Constants.NumberOneString;
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = strTelefono,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };



            CommonIFIService.InsertGeneralRequest objInsertGeneralRequest = new CommonIFIService.InsertGeneralRequest()
            {

                audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession),
                vNroTelefono = strTelefono,
                Interaction = new CommonIFIService.Iteraction()
                {
                    OBJID_CONTACTO = objInteractionModel.ObjidContacto,
                    FECHA_CREACION = objInteractionModel.DateCreaction,
                    TELEFONO = objInteractionModel.Telephone,
                    TIPO = objInteractionModel.Type,
                    CLASE = objInteractionModel.Class,
                    SUBCLASE = objInteractionModel.SubClass,
                    CLASE_CODIGO = objInteractionModel.ClassCode,
                    SUBCLASE_CODIGO = objInteractionModel.SubClassCode,
                    TIPO_INTER = objInteractionModel.TypeInter,
                    METODO = objInteractionModel.Method,
                    RESULTADO = objInteractionModel.Result,
                    HECHO_EN_UNO = objInteractionModel.MadeOne,
                    NOTAS = objInteractionModel.Note,
                    CONTRACT = objInteractionModel.Contract,
                    PLANO = objInteractionModel.Plan,
                    FLAG_CASO = objInteractionModel.FlagCase,
                    USUARIO_PROCESO = objInteractionModel.UserProces,
                    AGENTE = objInteractionModel.Agenth

                },
                InteractionTemplate = new CommonIFIService.InsertTemplateInteraction()
                {

                    _NOMBRE_TRANSACCION = oPlantillaDat.NOMBRE_TRANSACCION,
                    _X_INTER_20 = oPlantillaDat.X_INTER_20,
                    _X_INTER_21 = oPlantillaDat.X_INTER_21,
                    _X_INTER_29 = oPlantillaDat.X_INTER_29,
                    _X_INTER_17 = oPlantillaDat.X_INTER_17,
                    _X_TYPE_DOCUMENT = oPlantillaDat.X_TYPE_DOCUMENT,
                    _X_DOCUMENT_NUMBER = oPlantillaDat.X_DOCUMENT_NUMBER,
                    _X_CLARO_NUMBER = oPlantillaDat.X_CLARO_NUMBER,
                    _X_ADDRESS = oPlantillaDat.X_ADDRESS,
                    _X_REFERENCE_ADDRESS = oPlantillaDat.X_REFERENCE_ADDRESS,
                    _X_DEPARTMENT = oPlantillaDat.X_DEPARTMENT,
                    _X_CITY = oPlantillaDat.X_CITY,
                    _X_DISTRICT = oPlantillaDat.X_DISTRICT,
                    _X_EMAIL = oPlantillaDat.X_EMAIL,
                    _X_INTER_15 = oPlantillaDat.X_INTER_15,
                    _X_REFERENCE_PHONE = oPlantillaDat.X_REFERENCE_PHONE,
                    _X_INTER_1 = oPlantillaDat.X_INTER_1
                }

            };

            var objCustomerResponse = Claro.Web.Logging.ExecuteMethod<CustomerResponse>(() => { return _oServiceFixed.GetCustomer(objGetCustomerRequest); });

            //Validacion de Contingencia
            if (objCustomerResponse.Customer != null)
            {
                objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
            }



            CommonIFIService.InsertGeneralResponse objInsertGeneralResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.InsertGeneralResponse>(() => { return oCommonIFIService.GetinsertInteractionGeneral(objInsertGeneralRequest); });

            var dictionaryResponse = new Dictionary<string, object>()
            {
                {"rFlagInsercion", objInsertGeneralResponse.rFlagInsercion},
                {"rMsgText", objInsertGeneralResponse.rMsgText},
                {"rResult", objInsertGeneralResponse.rResult.ToString()},
                {"rInteraccionId", objInsertGeneralResponse.rInteraccionId},
            };

            var rInteraccionId = objInsertGeneralResponse.rInteraccionId;

            return dictionaryResponse;

        }
        #endregion

        ///<summary>Metodo para obtener el objeto del identificador del contacto(ObjContactId)</summary>
        ///<param name="strIdSession"></param>
        ///<param name="Telefono"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>GetObjContactId</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public JsonResult GetObjContactId(string strIdSession, string Telefono)
        {

            string contactobjid = "";
            string strFlgRegistrado = Claro.Constants.NumberOneString;
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession);
            try
            {
                CustomerResponse objCustomerResponse;

                AuditRequestFixed audit2 = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
                GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
            {
                audit = audit2,
                vPhone = Telefono,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };
                objCustomerResponse = Logging.ExecuteMethod(() =>
            {
                return _oServiceFixed.GetCustomerClf(objGetCustomerRequest);
            });

                contactobjid = objCustomerResponse.contactobjid;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }

            return Json(new { data = contactobjid });

        }

        #region GenerarPDF

        /// <summary>Permite generar la constancia al tipo de afiliación de recibo</summary>
        /// <param name="strIdSession"></param>  
        /// <param name="strInteraction"></param>  
        /// <param name="oModel"></param>  
        /// <returns>Dictionary</returns>
        /// <remarks>GetConstancyPDF</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public Dictionary<string, object> GetConstancyPDF(string strIdSession, string strIdInteraction, Model.MailReceiptModel oModel)
        {

            var listResponse = new Dictionary<string, object>();
            string nombrepath = string.Empty;
            string strTerminacionPdf = ConfigurationManager.AppSettings("strTerminacionPDF").ToString();
            string strInteraccionId = strIdInteraction;
            string strNombreArchivo = string.Empty;
            string strTexto = string.Empty;

            InteractionServiceRequestHfc objInteractionServiceRequest = new InteractionServiceRequestHfc();
            ParametersGeneratePDF parameters = new ParametersGeneratePDF();

            try
            {
                strTexto = Claro.Utils.GetValueFromConfigFileIFI("strMsgReciboEmail", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

                if (oModel.StateMail.ToUpper() == Claro.Constants.LetterA)
                {

                    parameters = new ParametersGeneratePDF();
                    parameters.StrCentroAtencionArea = oModel.CacDac;
                    parameters.StrNroServicio = oModel.Telephone;
                    parameters.StrTitularCliente = oModel.NameComplet;
                    parameters.StrCasoInter = strInteraccionId;
                    parameters.StrRepresLegal = oModel.RepresentLegal;
                    parameters.StrTipoDocIdentidad = oModel.TypeDoc;
                    parameters.StrNroDocIdentidad = oModel.NroDoc;
                    parameters.StrSegmento = oModel.CustomerType;
                    parameters.StrTelfReferencia = oModel.TelefonoReferencia;
                    parameters.strtransaccion = "Afiliación a recibo por correo electrónico.";
                    parameters.StrAccionIFI = (oModel.StateMail != "D") ? "Activación" : "Desactivación";
                    parameters.StrTelfConfirmacion = oModel.TelephoneSMS;
                    parameters.StrFecAfiliacion = (DateTime.Now).ToString("dd/MM/yyyy");


                    parameters.strEnvioCorreo = ((oModel.EmailCondstate != true) ? "NO" : "SI");
                    parameters.StrCodUsuario = oModel.CurrentUser;
                    parameters.StrNombreAgenteUsuario = oModel.FullNameUser;
                    parameters.StrEmail = oModel.Email;
                    parameters.strCorreoCliente = ((oModel.EmailCondstate != true) ? "" : oModel.EmailCond);

                    parameters.StrTipoTransaccion = "Afiliación";
                    parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaAfiliacionRecibo");
                    parameters.StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strNombreArchivoAfiliacionRecibo");
                    parameters.strFirmaDigital = ConfigurationManager.AppSettings("strIsFirmaDigital");

                    Claro.Web.Logging.Info(strIdSession, "Metodo :  GetConstancyPDF - Inicio ", "StrTipoTransaccion : " + parameters.StrTipoTransaccion);
                }
                else
                {
                    parameters = new ParametersGeneratePDF();
                    parameters.StrCentroAtencionArea = oModel.CacDac;
                    parameters.StrNroServicio = oModel.Telephone;
                    parameters.StrTitularCliente = oModel.NameComplet;
                    parameters.StrCasoInter = strInteraccionId;
                    parameters.StrRepresLegal = oModel.RepresentLegal;
                    parameters.StrTipoDocIdentidad = oModel.TypeDoc;
                    parameters.StrNroDocIdentidad = oModel.NroDoc;
                    parameters.StrSegmento = oModel.CustomerType;
                    parameters.StrTelfReferencia = oModel.TelefonoReferencia;
                    parameters.strtransaccion = "Afiliación a recibo por correo electrónico.";
                    parameters.StrAccionIFI = (oModel.StateMail != "D") ? "Activación" : "Desactivación";

                    parameters.StrTelfConfirmacion = oModel.TelephoneSMS;
                    parameters.StrFecAfiliacion = (DateTime.Now).ToString("dd/MM/yyyy");

                    parameters.strEnvioCorreo = ((oModel.EmailCondstate != true) ? "NO" : "SI");
                    parameters.StrDireccion = oModel.AdressDespatch;
                    parameters.StrDistrito = oModel.District;
                    parameters.StrProvincia = oModel.Provincia;
                    parameters.StrDepartamento = oModel.Departament_Fact;
                    parameters.StrReferenciaActual = oModel.Reference;

                    parameters.StrEmail = oModel.Email;
                    parameters.strCorreoCliente = ((oModel.EmailCondstate != true) ? "" : oModel.EmailCond);
                    parameters.StrCodUsuario = oModel.CurrentUser;
                    parameters.StrNombreAgenteUsuario = oModel.FullNameUser;

                    parameters.StrTipoTransaccion = "Desafiliación";

                    parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaDesafiliacionRecibo");
                    parameters.StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strNombreArchivoDesafiliacionRecibo");
                    parameters.strFirmaDigital = ConfigurationManager.AppSettings("strIsFirmaDigital");

                }

                GenerateConstancyResponseCommon response = new CommonServicesController().GenerateContancyPDF(strIdSession, parameters);
                nombrepath = response.FullPathPDF;
                var generado = response.Generated;
                oModel.bGeneratedPDF = response.Generated;
                oModel.strFullPathPDF = response.FullPathPDF;
                listResponse.Add("respuesta", generado);
                listResponse.Add("ruta", nombrepath);
                listResponse.Add("nombreArchivo", strNombreArchivo);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(oModel.IdSession, objInteractionServiceRequest.audit.transaction, ex.Message);
            }
            return listResponse;
        }

        #endregion

        #region Envío Correo

        /// <summary>Metodo para enviar un correo electronico al email ingresado al marcar envio de condiciones </summary>
        ///<param name="strInteraccionId"></param>
        ///<param name="strAdjunto"></param>
        ///<param name="model"></param>
        ///<param name="strNombreArchivoPDF"></param>
        /// <param name="attachFile"></param>
        /// <returns>string</returns>
        /// <remarks>GetSendEmail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public string GetSendEmail(string strInteraccionId, string strAdjunto, Model.MailReceiptModel model, string strNombreArchivoPDF, byte[] attachFile)
        {
            string strResul = string.Empty;
            CommonIFIService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(model.IdSession);
            CommonIFIService.SendEmailRequestCommon objGetSendEmailRequest;
            try
            {
                string strMessage = string.Empty;
                string strDestinatarios = model.EmailCond;
                string strAsunto = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailAfilRecibo",
                    ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                string OpcionRetenido = string.Empty;

                #region "Body Email"

                strMessage = "<html>";
                strMessage += " <head>";
                strMessage += "     <style type='text/css'>";
                strMessage += "     <!--";
                strMessage += "         .Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
                strMessage += "         .Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
                strMessage += "      -->";
                strMessage += "      </style>";
                strMessage += " </head>";
                strMessage += "<body>";
                strMessage += "     <table width='100%' border='0' cellpadding='0' cellspacing='0'>";
                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strEstimadoCliente", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + " </td></tr>";
                if (model.StateMail.ToUpper() == Claro.Constants.LetterA)
                {
                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailAfilRecibo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                }
                else
                {
                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailDesafilRecibo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                }


                strMessage += "<tr>";
                strMessage += " <td align='center'>";
                strMessage += " </td></tr>";

                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>&nbsp;</td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgCordialmente", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailFirma", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailConsultaLlameGratis", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                strMessage += "    </table>";
                strMessage += "  </body>";
                strMessage += "</html>";
                #endregion

                CommonIFIService.SendEmailResponseCommon objGetSendEmailResponse = new CommonIFIService.SendEmailResponseCommon();
                objGetSendEmailRequest =
                    new CommonIFIService.SendEmailRequestCommon()
                    {
                        audit = AuditRequest,
                        strSender = strRemitente,
                        strTo = strDestinatarios,
                        strMessage = strMessage,
                        strAttached = strAdjunto,
                        strSubject = strAsunto,
                        AttachedByte = attachFile
                    };
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailResponseCommon>(() => { return oCommonIFIService.GetSendEmailFixed(objGetSendEmailRequest); });

                if (objGetSendEmailResponse.Exit == Claro.SIACU.Constants.OK)
                {
                    strResul = "OK";
                }
                else
                {
                    strResul = "NO";
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(model.IdSession, AuditRequest.transaction, ex.Message);
                strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
            }
            return strResul;
        }

        #endregion

        #region Auditoría

        ///<summary>Registra los datos requeridos para el sevicio de auditoría</summary>
        ///<param name="oModel"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>Auditoria</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>20/12/2018.</FecAct></item></list>
        public bool Auditoria(Model.MailReceiptModel oModel)
        {
            bool FlatResultado = false;
            string strCodigoAuditoria = KEY.AppSettings("strCodTranRetCancTempINT");
            string strusuarioAutoriza = oModel.CurrentUser;  // validar
            string strAmmount = Claro.Constants.NumberZeroString;
            string[,] strDetails = new string[8, 3];
            string strService = KEY.AppSettings("gConstEvtServicio");

            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(oModel.IdSession);


            string strIpCliente = Claro.Utils.CheckStr(HttpContext.Request.UserHostAddress);
            string strIPServidor = App_Code.Common.GetApplicationIp();
            string strNombreServidor = App_Code.Common.GetApplicationName();
            string strCuentaUsuario = oModel.CurrentUser;
            string strPhone = oModel.Telephone;
            string strNameClient = oModel.NameComplet;

            strDetails[0, 0] = "Número Claro";
            strDetails[0, 1] = oModel.NroCelular;
            strDetails[0, 2] = "Número Claro";

            strDetails[1, 0] = "Código Contrato";
            strDetails[1, 1] = oModel.ContractId;
            strDetails[1, 2] = "Número Claro";

            strDetails[2, 0] = "Transacción Realizada";
            strDetails[2, 1] = (oModel.Accion == "R" ? "Retenido" : "No Retenido");
            strDetails[2, 2] = "Transacción Realizada";

            strDetails[3, 0] = "Motivo Cancelación";
            strDetails[3, 1] = oModel.DesMotivos;
            strDetails[3, 2] = "Motivo Cancelación";

            strDetails[4, 0] = "Acción";
            strDetails[4, 1] = oModel.DesAccion;
            strDetails[4, 2] = "Acción";

            strDetails[5, 0] = "Autoriza Acción Segundo Nivel";
            strDetails[5, 1] = (oModel.hidSupJef == "S" ? strusuarioAutoriza : string.Empty);
            strDetails[5, 2] = "Autoriza Acción Segundo Nivel";

            strDetails[6, 0] = "Fecha Programada para la Cancelación";
            strDetails[6, 1] = string.Empty;
            strDetails[6, 2] = "Fecha Programada para la Cancelación";

            strDetails[7, 0] = "CAC/DAC";
            strDetails[7, 1] = oModel.DescCacDac;
            strDetails[7, 2] = "CAC/DAC";

            var sbTexto = new System.Text.StringBuilder();

            int count = ((strDetails.Length / 4) - 1);
            var sbText = new System.Text.StringBuilder();
            for (int i = 0; i < count; i++)
            {
                if (strDetails.GetValue(i, 1) != null && strDetails.GetValue(i, 2) != null)
                {
                    sbText.Append(" " + strDetails.GetValue(i, 1) + " : ");
                    sbText.Append(strDetails.GetValue(i, 2));
                }
            }

            try
            {
                CommonIFIService.SaveAuditResponseCommon objResponse = null;
                CommonIFIService.SaveAuditRequestCommon objRequest = new CommonIFIService.SaveAuditRequestCommon()
                {
                    vCuentaUsuario = strCuentaUsuario,
                    vIpCliente = strIpCliente,
                    vIpServidor = strIPServidor,
                    vMonto = strAmmount,
                    vNombreCliente = strNameClient,
                    vNombreServidor = strNombreServidor,
                    vServicio = strService,
                    vTelefono = strPhone,
                    vTexto = sbText.ToString(),
                    vTransaccion = strCodigoAuditoria,
                    audit = audit
                };

                objResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SaveAuditResponseCommon>(() =>
               {
                   return oCommonIFIService.SaveAudit(objRequest);
               });
                FlatResultado = objResponse.respuesta;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }


            return FlatResultado;
        }

        /// <summary>Método que permite el envio de la constancia por email</summary>
        /// <param name="model"></param>  
        /// <param name="lstFileAdjunto"></param>  
        /// <returns>string</returns>
        /// <remarks>GetSendEmail2</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>23/03/2019</FecCrea></item></list>
        public string GetSendEmail2(Model.MailReceiptModel model, List<CommonIFIService.AttachedFile> lstFileAdjunto)
        {
            CommonIFIService.SendEmailWithBase64Request objSendEmailWithBase64Request = new CommonIFIService.SendEmailWithBase64Request();
            CommonIFIService.SendEmailWithBase64Response objSendEmailWithBase64Response = null;
            CommonIFIService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(model.IdSession);

            string strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

            if (model != null)
            {

                try
                {
                    string strMessage = string.Empty;
                    string strDestinatarios = model.EmailCond;
                    string strAsunto = String.Empty;

                    if (model.StateMail == "A")
                    {
                        strAsunto = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailAfilRecibo",
                        ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                    }
                    else if (model.StateMail == "D")
                    {
                        strAsunto = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailDesafilRecibo",
                        ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                    }

                    string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");

                    #region "Body Email"

                    strMessage = "<html>";
                    strMessage += " <head>";
                    strMessage += "     <style type='text/css'>";
                    strMessage += "     <!--";
                    strMessage += "         .Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
                    strMessage += "         .Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
                    strMessage += "      -->";
                    strMessage += "      </style>";
                    strMessage += " </head>";
                    strMessage += "<body>";
                    strMessage += "     <table width='100%' border='0' cellpadding='0' cellspacing='0'>";
                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strEstimadoCliente", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + " </td></tr>";
                    if (model.StateMail.ToUpper() == Claro.Constants.LetterA)
                    {
                        strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailAfilRecibo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                    }
                    else
                    {
                        strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailDesafilRecibo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                    }


                    strMessage += "<tr>";
                    strMessage += " <td align='center'>";
                    strMessage += " </td></tr>";

                    strMessage += "         <tr><td height='10'></td>";
                    strMessage += "         <tr><td class='Estilo1'>&nbsp;</td></tr>";
                    strMessage += "         <tr><td height='10'></td>";
                    strMessage += "         <tr><td height='10'></td>";
                    strMessage += "         <tr><td height='10'></td>";
                    strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgCordialmente", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                    strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailFirma", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                    strMessage += "         <tr><td height='10'></td>";
                    strMessage += "         <tr><td height='10'></td>";
                    strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailConsultaLlameGratis", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                    strMessage += "    </table>";
                    strMessage += "  </body>";
                    strMessage += "</html>";
                    #endregion


                    objSendEmailWithBase64Request.audit = objAudit;
                    objSendEmailWithBase64Request.Sender = strRemitente;
                    objSendEmailWithBase64Request.To = strDestinatarios;
                    objSendEmailWithBase64Request.HtmlFlag = KEY.AppSettings("strHtmlFlagCorreoSB").ToString();
                    objSendEmailWithBase64Request.Subject = strAsunto;
                    objSendEmailWithBase64Request.Message = strMessage;
                    if (lstFileAdjunto != null && lstFileAdjunto.Count > 0)
                    {
                        objSendEmailWithBase64Request.ListAttachedFile = lstFileAdjunto;
                    }

                    objSendEmailWithBase64Response = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailWithBase64Response>(() =>
                    {
                        return oCommonIFIService.SendEmailWithBase64(objSendEmailWithBase64Request);
                    });
                    if (objSendEmailWithBase64Response != null && objSendEmailWithBase64Response.ResponseCode == Claro.Constants.NumberZeroString)
                    {
                        strResul = Claro.SIACU.Constants.OK;

                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(model.IdSession, objAudit.transaction, ex.Message);
                }
            }
            return strResul;
        }
        #endregion
    }
}