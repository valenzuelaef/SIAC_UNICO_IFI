
using System;
using System.Collections.Generic;
using System.Linq;
using EntitiesFixed = Claro.SIACU.Entity.IFI.Fixed;
using FIXED = Claro.SIACU.Entity.IFI.Fixed;
using COMMON = Claro.SIACU.Entity.IFI.Common;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Entity.IFI.Fixed.GetCaseInsert;
using Claro.SIACU.Entity.IFI.Fixed;
using Claro.SIACU.Entity.IFI.Fixed.GetConsultationServiceByContract;
using Claro.SIACU.Entity.IFI.Postpaid;



namespace Claro.SIACU.Business.IFI.Fixed
{
    public class Fixed
    {

        /// <summary>Método que obtiene los datos del cliente.</summary>
        /// <param name="objRequest"></param>
        /// <returns>FIXED.GetCustomer.CustomerResponse</returns>
        /// <remarks>GetCustomer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static FIXED.GetCustomer.CustomerResponse GetCustomer(FIXED.GetCustomer.GetCustomerRequest objRequest)
        {
            FIXED.GetCustomer.CustomerResponse objResponse = new FIXED.GetCustomer.CustomerResponse();
            objResponse.rMsgText = string.Empty;
            objResponse.Customer = new FIXED.Customer();
            objResponse.vFlagConsulta = string.Empty;
            var vFlagConsulta = string.Empty;
            var rMsgText = string.Empty;
            try
            {
                var objEntity = Claro.Web.Logging.ExecuteMethod<FIXED.Customer>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Fixed.Fixed.GetCustomer(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vPhone, objRequest.vAccount, objRequest.vContactobjid1, objRequest.vFlagReg, ref vFlagConsulta, ref rMsgText);
                    });

                objResponse.rMsgText = rMsgText;
                objResponse.Customer = objEntity;
                objResponse.vFlagConsulta = vFlagConsulta;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        /// <summary>Método que obtiene los datos del cliente de la BD CLARIFY.</summary>
        /// <param name="objRequest"></param>
        /// <returns>FIXED.GetCustomer.CustomerResponse</returns>
        /// <remarks>GetCustomerClf</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static FIXED.GetCustomer.CustomerResponse GetCustomerClf(FIXED.GetCustomer.GetCustomerRequest objRequest)
        {
            FIXED.GetCustomer.CustomerResponse objResponse = new FIXED.GetCustomer.CustomerResponse();
            objResponse.rMsgText = string.Empty;
            objResponse.Customer = new FIXED.Customer();
            objResponse.vFlagConsulta = string.Empty;
            var vFlagConsulta = string.Empty;
            var rMsgText = string.Empty;
            try
            {
                var objEntity = Claro.Web.Logging.ExecuteMethod<FIXED.Customer>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Fixed.Fixed.GetCustomerClf(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vPhone, objRequest.vAccount, objRequest.vContactobjid1, objRequest.vFlagReg, ref vFlagConsulta, ref rMsgText);
                    });

                objResponse.contactobjid = objEntity.OBJID_CONTACTO;
                objResponse.rMsgText = rMsgText;
                objResponse.Customer = objEntity;
                objResponse.vFlagConsulta = vFlagConsulta;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        /// <summary>Método para obtener el número de teléfono por código de contrato.</summary>
        /// <param name="objServiceRequest"></param>
        /// <returns>FIXED.GetService.ServiceResponse</returns>
        /// <remarks>GetTelephoneByContractCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
       public static FIXED.GetService.ServiceResponse GetTelephoneByContractCode(FIXED.GetService.ServiceRequest objServiceRequest)
        {
            FIXED.GetService.ServiceResponse objServiceResponse = null;
            if (objServiceRequest.ProductType.Equals(Claro.SIACU.Constants.LTE))
            {
                objServiceResponse = new FIXED.GetService.ServiceResponse()
                {
                    ListService = Claro.Web.Logging.ExecuteMethod<List<FIXED.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.IFI.Fixed.Fixed.GetTelephoneByContractCodeLTE(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceRequest.Audit.IPAddress, objServiceRequest.Audit.ApplicationName, objServiceRequest.Audit.UserName, objServiceRequest.ContractID); })
                };
            }
            else if (objServiceRequest.ProductType.Equals(Claro.SIACU.Constants.HFC))
            {
                objServiceResponse = new FIXED.GetService.ServiceResponse()
                {
                    ListService = Claro.Web.Logging.ExecuteMethod<List<FIXED.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.IFI.Fixed.Fixed.GetTelephoneByContractCodeHFC(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceRequest.Audit.IPAddress, objServiceRequest.Audit.ApplicationName, objServiceRequest.Audit.UserName, objServiceRequest.ContractID); })
                };
            }
            return objServiceResponse;
        }
      

        #region "Inst/Desinst Decodificadores"


        /// <summary>Método que obtiene los tipos de trabajo.</summary>
        /// <param name="objJobTypesRequest"></param>
        /// <returns>EntitiesFixed.GetJobTypes.JobTypesResponse</returns>
        /// <remarks>GetJobTypes</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static EntitiesFixed.GetJobTypes.JobTypesResponse GetJobTypes(EntitiesFixed.GetJobTypes.JobTypesRequest objJobTypesRequest)
        {
            List<EntitiesFixed.JobType> listServiceResponse = null;
            List<EntitiesFixed.JobType> listService = null;
            List<EntitiesFixed.JobType> listServiceOrdered;

            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.JobType>>(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.Fixed.GetJobTypes(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, objJobTypesRequest.p_tipo);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<EntitiesFixed.JobType>();
                listServiceOrdered = listService.OrderBy(a => a.descripcion).ToList();
                listServiceResponse = listServiceOrdered;
            }

            EntitiesFixed.GetJobTypes.JobTypesResponse objJobTypesResponse = new EntitiesFixed.GetJobTypes.JobTypesResponse()
            {
                JobTypes = listServiceResponse
            };

            return objJobTypesResponse;
        }


        /// <summary>Método que Valida el flujo ETA.</summary>
        /// <param name="RequestParam"></param>
        /// <returns>EntitiesFixed.ETAFlowValidate.ETAFlowResponse</returns>
        /// <remarks>ETAFlowValidate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static EntitiesFixed.ETAFlowValidate.ETAFlowResponse ETAFlowValidate(EntitiesFixed.ETAFlowValidate.ETAFlowRequest RequestParam)
        {
            EntitiesFixed.ETAFlow listServiceResponse = null;
            EntitiesFixed.ETAFlow listService = null;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.ETAFlow>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.Fixed.ETAFlowValidate(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.as_origen, RequestParam.av_idplano, RequestParam.av_ubigeo, RequestParam.an_tiptra, RequestParam.an_tipsrv);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = listService;
            }

            EntitiesFixed.ETAFlowValidate.ETAFlowResponse Resultado = new EntitiesFixed.ETAFlowValidate.ETAFlowResponse()
            {
                ETAFlow = listServiceResponse
            };

            return Resultado;
        }


        /// <summary>Método que obtiene el tipo de orden.</summary>
        /// <param name="RequestParam"></param>
        /// <returns>EntitiesFixed.GetOrderType.OrderTypesResponse </returns>
        /// <remarks>GetOrderType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static EntitiesFixed.GetOrderType.OrderTypesResponse GetOrderType(EntitiesFixed.GetOrderType.OrderTypesRequest RequestParam)
        {
            List<EntitiesFixed.OrderType> listServiceResponse = null;
            List<EntitiesFixed.OrderType> listService = null;
            List<EntitiesFixed.OrderType> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.OrderType>>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.Fixed.GetOrderType(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.vIdtiptra);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<EntitiesFixed.OrderType>();
                listServiceOrdered = listService.OrderBy(a => a.VALOR).ToList();
                listServiceResponse = listServiceOrdered;
            }

            EntitiesFixed.GetOrderType.OrderTypesResponse Resultado = new EntitiesFixed.GetOrderType.OrderTypesResponse()
            {
                ordertypes = listServiceResponse
            };

            return Resultado;
        }


        /// <summary>Método que obtiene el subtipo de orden.</summary>
        /// <param name="RequestParam"></param>
        /// <returns>EntitiesFixed.GetOrderSubType.OrderSubTypesResponse </returns>
        /// <remarks>GetOrderSubType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static EntitiesFixed.GetOrderSubType.OrderSubTypesResponse GetOrderSubType(EntitiesFixed.GetOrderSubType.OrderSubTypesRequest RequestParam)
        {
            List<EntitiesFixed.OrderSubType> listServiceResponse = null;
            List<EntitiesFixed.OrderSubType> listService = null;
            List<EntitiesFixed.OrderSubType> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.OrderSubType>>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.Fixed.GetOrderSubType(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.av_cod_tipo_orden);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<EntitiesFixed.OrderSubType>();
                listServiceOrdered = listService.OrderBy(a => a.COD_SUBTIPO_ORDEN).ToList();
                listServiceResponse = listServiceOrdered;
            }

            EntitiesFixed.GetOrderSubType.OrderSubTypesResponse Resultado = new EntitiesFixed.GetOrderSubType.OrderSubTypesResponse()
            {
                OrderSubTypes = listServiceResponse
            };

            return Resultado;
        }

       
        /// <summary>Método que permite insertar el detalle de la interacción del servicio.</summary>
        /// <param name="RequestParam"></param>
        /// <returns>EntitiesFixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionResponse</returns>
        /// <remarks>GetInsertDetailServiceInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static EntitiesFixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionResponse GetInsertDetailServiceInteraction(EntitiesFixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionRequest RequestParam)
        {
            var objInsertDetailServiceInteractionResponse = new EntitiesFixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionResponse();

            try
            {
                string resultado = "";
                string mensaje = string.Empty;

                objInsertDetailServiceInteractionResponse.rResul = Claro.Web.Logging.ExecuteMethod<bool>(RequestParam.Audit.Session, RequestParam.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.IFI.Fixed.Fixed.GetInsertDetailServiceInteraction(
                            RequestParam.Audit.Session,
                            RequestParam.Audit.Transaction,
                            RequestParam.codinterac,
                            RequestParam.nombreserv,
                            RequestParam.tiposerv,
                            RequestParam.gruposerv,
                            RequestParam.cf,
                            RequestParam.equipo,
                            RequestParam.cantidad,
                            ref resultado,
                            ref mensaje);
                    });
                objInsertDetailServiceInteractionResponse.resultado = resultado;
                objInsertDetailServiceInteractionResponse.mensaje = mensaje;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            return objInsertDetailServiceInteractionResponse;
        }
       

        /// <summary>Método que permite insertar la transacción.</summary>
        /// <param name="RequestParam"></param>
        /// <returns>EntitiesFixed.GetInsertTransaction.InsertTransactionResponse</returns>
        /// <remarks>GetInsertTransaction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static EntitiesFixed.GetInsertTransaction.InsertTransactionResponse GetInsertTransaction(EntitiesFixed.GetInsertTransaction.InsertTransactionRequest RequestParam)
        {
            var objInsertTransactionResponse = new EntitiesFixed.GetInsertTransaction.InsertTransactionResponse();

            try
            {
                string rstrResCod = string.Empty;
                string rstrResDes = string.Empty;

                objInsertTransactionResponse.intNumSot = Claro.Web.Logging.ExecuteMethod<string>(RequestParam.Audit.Session, RequestParam.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.IFI.Fixed.Fixed.GetInsertTransaction(
                            RequestParam.Audit.Session,
                            RequestParam.Audit.Transaction,
                            RequestParam.oTransfer,
                            ref rstrResCod,
                            ref rstrResDes);
                    });
                objInsertTransactionResponse.rintResCod = rstrResCod;
                objInsertTransactionResponse.rstrResDes = rstrResDes;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            return objInsertTransactionResponse;
        }


        /// <summary>Método que obtiene los servicios por interacción.</summary>
        /// <param name="objInteractionServiceRequest"></param>
        /// <returnsEntity.IFI.Fixed.GetServicesByInteraction.InteractionServiceResponse</returns>
        /// <remarks>GetServicesByInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static Entity.IFI.Fixed.GetServicesByInteraction.InteractionServiceResponse GetServicesByInteraction(Entity.IFI.Fixed.GetServicesByInteraction.InteractionServiceRequest objInteractionServiceRequest)
        {
            List<Entity.IFI.Fixed.ServiceByInteraction> listServiceResponse = null;
            List<Entity.IFI.Fixed.ServiceByInteraction> listService = null;
            List<Entity.IFI.Fixed.ServiceByInteraction> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<Entity.IFI.Fixed.ServiceByInteraction>>(objInteractionServiceRequest.Audit.Session, objInteractionServiceRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.Fixed.GetServicesByInteraction(objInteractionServiceRequest.Audit.Session, objInteractionServiceRequest.Audit.Transaction, objInteractionServiceRequest.idInteraccion);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInteractionServiceRequest.Audit.Session, objInteractionServiceRequest.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<Entity.IFI.Fixed.ServiceByInteraction>();
                listServiceOrdered = listService.OrderBy(a => a.COD_INTERAC).ToList();
                listServiceResponse = listServiceOrdered;
            }
            Entity.IFI.Fixed.GetServicesByInteraction.InteractionServiceResponse objInteractionServiceResponse = new Entity.IFI.Fixed.GetServicesByInteraction.InteractionServiceResponse()
            {
                Services = listServiceResponse
            };
            return objInteractionServiceResponse;
        }
        #endregion


        /// <summary>Método que inserta los datos de la interacción del negocio.</summary>
        /// <param name="objRequest"></param>
        /// <returns>FIXED.GetInsertInteractionBusiness.InsertInteractionBusinessResponse</returns>
        /// <remarks>GetInsertInteractionBusiness</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static FIXED.GetInsertInteractionBusiness.InsertInteractionBusinessResponse GetInsertInteractionBusiness(FIXED.GetInsertInteractionBusiness.InsertInteractionBusinessRequest objRequest)
        {
            string strTelefono = string.Empty;
            bool resultado;
            string sInteraccionId = string.Empty;
            string sFlagInsercion = string.Empty;
            string sMsgText = string.Empty;
            string sFlagInsercionInteraccion = string.Empty;
            string sMsgTextInteraccion = string.Empty;
            string sCodigoRetornoTransaccion = string.Empty;
            string sMensajeErrorTransaccion = string.Empty;

            string ContingenciaClarify = KEY.AppSettings("gConstContingenciaClarify");

            FIXED.GetInsertInteractionBusiness.InsertInteractionBusinessResponse oInsertInteractionBusinessResponse = new FIXED.GetInsertInteractionBusiness.InsertInteractionBusinessResponse();



            FIXED.GetCustomer.CustomerResponse oCustomerResponse;

            if (objRequest.Phone == objRequest.Interaction.TELEFONO)
            {
                strTelefono = objRequest.Phone;
            }
            else
            {
                strTelefono = objRequest.Interaction.TELEFONO;
            }

            FIXED.GetCustomer.GetCustomerRequest oCustomerRequest = new FIXED.GetCustomer.GetCustomerRequest();
            oCustomerRequest.vPhone = strTelefono;
            oCustomerRequest.vAccount = string.Empty;
            oCustomerRequest.vContactobjid1 = objRequest.Interaction.OBJID_CONTACTO;
            oCustomerRequest.vFlagReg = Claro.Constants.NumberOneString;
            oCustomerRequest.Audit = objRequest.Audit;

            oCustomerResponse = GetCustomer(oCustomerRequest);

            if (oCustomerResponse.Customer != null)
            {
                objRequest.Interaction.OBJID_CONTACTO = oCustomerResponse.Customer.OBJID_CONTACTO;//TODO
            }

            if (ContingenciaClarify != Constants.Yes)
            {
                #region GetBusinessInteraction2
                COMMON.GetBusinessInteraction2.BusinessInteraction2Request oBusinessInteraction2Request = new COMMON.GetBusinessInteraction2.BusinessInteraction2Request();
                oBusinessInteraction2Request.Item = new COMMON.Iteraction();
                oBusinessInteraction2Request.Item = objRequest.Interaction;
                oBusinessInteraction2Request.Audit = objRequest.Audit;


                COMMON.GetBusinessInteraction2.BusinessInteraction2Response oBusinessInteraction2Response = Common.GetBusinessInteractionFixed(oBusinessInteraction2Request);
                resultado = oBusinessInteraction2Response.ProcessOK;
                sInteraccionId = oBusinessInteraction2Response.InteractionId;
                sFlagInsercion = oBusinessInteraction2Response.FlagInsertion;
                sMsgText = oBusinessInteraction2Response.MsgText ?? "";
                #endregion

            }
            else
            {
                #region GetInsertInteract
                COMMON.GetInsertInteract.InsertInteractRequest oInsertInteractRequest = new COMMON.GetInsertInteract.InsertInteractRequest();
                oInsertInteractRequest.item = new COMMON.Iteraction();
                oInsertInteractRequest.item = objRequest.Interaction;
                oInsertInteractRequest.Audit = objRequest.Audit;


                COMMON.GetInsertInteract.InsertInteractResponse oInsertInteractResponse = Common.GetInsertInteract(oInsertInteractRequest);
                resultado = oInsertInteractResponse.ProcesSucess;
                sInteraccionId = oInsertInteractResponse.Interactionid;
                sFlagInsercion = oInsertInteractResponse.FlagInsercion;
                sMsgText = oInsertInteractResponse.MsgText;
                #endregion
            }

            if (!string.IsNullOrEmpty(sInteraccionId))
            {
                if (objRequest.InteractionTemplate != null)
                {
                    if (ContingenciaClarify != Constants.Yes)
                    {
                        #region GetInsertInteractionTemplate
                        COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionRequest oInsertTemplateInteractionRequest = new COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionRequest();
                        oInsertTemplateInteractionRequest.IdInteraction = sInteraccionId;
                        oInsertTemplateInteractionRequest.item = new COMMON.InsertTemplateInteraction();
                        oInsertTemplateInteractionRequest.item = objRequest.InteractionTemplate;
                        oInsertTemplateInteractionRequest.Audit = objRequest.Audit;

                        COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse oInsertTemplateInteractionResponse = new COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse();
                        oInsertTemplateInteractionResponse = Common.GetInsertInteractionTemplate(oInsertTemplateInteractionRequest);

                        resultado = oInsertTemplateInteractionResponse.ProcesSucess;
                        sFlagInsercionInteraccion = oInsertTemplateInteractionResponse.FlagInsercion;
                        sMsgTextInteraccion = oInsertTemplateInteractionResponse.MsgText;
                        sCodigoRetornoTransaccion = string.Empty;
                        #endregion
                    }
                    else
                    {
                        #region GetInsInteractionTemplate
                        COMMON.GetInsTemplateInteraction.InsTemplateInteractionRequest oInsTemplateInteractionRequest = new COMMON.GetInsTemplateInteraction.InsTemplateInteractionRequest();
                        oInsTemplateInteractionRequest.IdInteraction = sInteraccionId;
                        oInsTemplateInteractionRequest.item = new COMMON.InsertTemplateInteraction();
                        oInsTemplateInteractionRequest.item = objRequest.InteractionTemplate;
                        oInsTemplateInteractionRequest.Audit = objRequest.Audit;

                        COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse oInsTemplateInteractionResponse = Common.GetInsInteractionTemplate(oInsTemplateInteractionRequest);

                        resultado = oInsTemplateInteractionResponse.ProcessSucess;
                        sFlagInsercionInteraccion = oInsTemplateInteractionResponse.FlagInsercion;
                        sMsgTextInteraccion = oInsTemplateInteractionResponse.MsgText;
                        sCodigoRetornoTransaccion = string.Empty;
                        #endregion
                    }


                    string strTransaccion = objRequest.InteractionTemplate._NOMBRE_TRANSACCION;
                    if (!string.IsNullOrEmpty(strTransaccion) && objRequest.ExecuteTransactation == true)
                    {
                        sCodigoRetornoTransaccion = string.Empty;
                        sMensajeErrorTransaccion = string.Empty;
                    }
                    else
                    {
                        sCodigoRetornoTransaccion = Constants.Zero;
                        sMensajeErrorTransaccion = string.Empty;

                    }
                }
            }

            oInsertInteractionBusinessResponse.InteractionId = sInteraccionId;
            oInsertInteractionBusinessResponse.Result = resultado;
            oInsertInteractionBusinessResponse.FlagInsercion = sFlagInsercion;
            oInsertInteractionBusinessResponse.MsgText = sMsgText;
            oInsertInteractionBusinessResponse.FlagInsercionInteraction = sFlagInsercionInteraccion;
            oInsertInteractionBusinessResponse.MsgTextInteraction = sMsgTextInteraccion;
            oInsertInteractionBusinessResponse.CodReturnTransaction = sCodigoRetornoTransaccion;
            oInsertInteractionBusinessResponse.MsgTextTransaccion = sMensajeErrorTransaccion;

            return oInsertInteractionBusinessResponse;
        }


        /// <summary>Método que permite validar los datos del cliente.</summary>
        /// <param name="oGetCustomerRequest"></param>
        /// <returns>FIXED.GetCustomer.CustomerResponse</returns>
        /// <remarks>GetValidateCustomer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static FIXED.GetCustomer.CustomerResponse GetValidateCustomer(EntitiesFixed.GetCustomer.GetCustomerRequest oGetCustomerRequest)
        {

            EntitiesFixed.GetCustomer.CustomerResponse oCustomerResponse = new EntitiesFixed.GetCustomer.CustomerResponse();
            try
            {
              oCustomerResponse =  Claro.Web.Logging.ExecuteMethod(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction, () =>
                                    {
                                        return Data.IFI.Common.GetValidateCustomer(oGetCustomerRequest);
                                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction, ex.Message);
            }
            
            return oCustomerResponse;

        }


        /// <summary>Método que permite crear un caso.</summary>
        /// <param name="oRequest"></param>
        /// <returns>EntitiesFixed.Interaction</returns>
        /// <remarks>GetCreateCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static EntitiesFixed.Interaction GetCreateCase(EntitiesFixed.Interaction oRequest)
        {
            EntitiesFixed.Interaction oResponse = new EntitiesFixed.Interaction();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.Interaction>(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.Fixed.GetCreateCase(oRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, ex.Message);
            }

            return oResponse;

        }


        /// <summary>Método que permite insertar un caso.</summary>
        /// <param name="oItem"></param>
        /// <returns>EntitiesFixed.Interaction</returns>
        /// <remarks>GetInsertCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static EntitiesFixed.Interaction GetInsertCase(EntitiesFixed.Interaction oItem)
        {
            EntitiesFixed.Interaction oResponse = new EntitiesFixed.Interaction();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.Interaction>(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.Fixed.GetInsertCase(oItem);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }

            return oResponse;

        }


        /// <summary>Método que permite insertar los datos de la plantilla del caso.</summary>
        /// <param name="oItem"></param>
        /// <returns>EntitiesFixed.CaseTemplate</returns>
        /// <remarks>GetInsertTemplateCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static EntitiesFixed.CaseTemplate GetInsertTemplateCase(EntitiesFixed.CaseTemplate oItem)
        {
            EntitiesFixed.CaseTemplate oResponse = new EntitiesFixed.CaseTemplate();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.CaseTemplate>(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.Fixed.GetInsertTemplateCase(oItem);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }

            return oResponse;

        }


        /// <summary>Método que permite insertar los datos de la plantilla de contingencia del caso.</summary>
        /// <param name="oItem"></param>
        /// <returns>EntitiesFixed.CaseTemplate</returns>
        /// <remarks>GetInsertTemplateCaseContingent</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static EntitiesFixed.CaseTemplate GetInsertTemplateCaseContingent(EntitiesFixed.CaseTemplate oItem)
        {
            EntitiesFixed.CaseTemplate oResponse = new EntitiesFixed.CaseTemplate();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.CaseTemplate>(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.Fixed.GetInsertTemplateCaseContingent(oItem);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }

            return oResponse;

        }


        /// <summary>Método que permite actualizar la plantilla del caso.</summary>
        /// <param name="oItem"></param>
        /// <returns>EntitiesFixed.CaseTemplate</returns>
        /// <remarks>ActualizaPlantillaCaso</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static EntitiesFixed.CaseTemplate ActualizaPlantillaCaso(EntitiesFixed.CaseTemplate oItem)
        {
            EntitiesFixed.CaseTemplate oResponse = new EntitiesFixed.CaseTemplate();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.CaseTemplate>(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.Fixed.ActualizaPlantillaCaso(oItem);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }
            return oResponse;
        }


        /// <summary>Método que obtiene el servicio por código de contrato.</summary>
        /// <param name="oConsultationServiceByContractRequest"></param>
        /// <returns>ConsultationServiceByContractResponse</returns>
        /// <remarks>GetConsultationServiceByContract</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static ConsultationServiceByContractResponse GetConsultationServiceByContract(ConsultationServiceByContractRequest oConsultationServiceByContractRequest)
        {
            ConsultationServiceByContractResponse oConsultationServiceByContractResponse = new ConsultationServiceByContractResponse();

            try
            {
                oConsultationServiceByContractResponse = Claro.Web.Logging.ExecuteMethod<ConsultationServiceByContractResponse>(oConsultationServiceByContractRequest.Audit.Session, oConsultationServiceByContractRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.Fixed.GetConsultationServiceByContract(oConsultationServiceByContractRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oConsultationServiceByContractRequest.Audit.Session, oConsultationServiceByContractRequest.Audit.Transaction, ex.Message);
            }

            return oConsultationServiceByContractResponse;

        }


        /// <summary>Método que obtiene los servicios programados.</summary>
        /// <param name="objRequest"></param>
        /// <returns>EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse</returns>
        /// <remarks>GetTransactionScheduled</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse GetTransactionScheduled(EntitiesFixed.GetTransactionScheduled.TransactionScheduledRequest objRequest) {
            var objResponse = new EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse();

            try
            {
                if (objRequest.vstrEstado.Equals(Claro.Constants.NumberOneNegativeString))
                    objRequest.vstrEstado = string.Empty;
                if (objRequest.vstrTipoTran.Equals(Claro.Constants.NumberOneNegativeString))
                    objRequest.vstrTipoTran = string.Empty;
                if (objRequest.vstrCacDac.Equals(Claro.Constants.NumberOneNegativeString))
                    objRequest.vstrCacDac = string.Empty;

                objResponse.ListTransactionScheduled = Claro.Web.Logging.ExecuteMethod<List<TransactionScheduled>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.IFI.Fixed.Fixed.GetTransactionScheduled(
                            objRequest.Audit.Session,
                            objRequest.Audit.Transaction,
                            objRequest.vstrCoId,
                            objRequest.vstrCuenta,
                            objRequest.vstrFDesde,
                            objRequest.vstrFHasta,
                            objRequest.vstrEstado,
                            objRequest.vstrAsesor,
                            objRequest.vstrTipoTran,
                            objRequest.vstrCodInter,
                            objRequest.vstrCacDac
                            );
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        /// <summary>Método que obtiene números de linea del cliente.</summary>
        /// <param name="objRequest"></param>
        /// <returns>ConsultationServiceByContractResponse</returns>
        /// <remarks>GetCustomerLineNumber</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static ConsultationServiceByContractResponse GetCustomerLineNumber(ConsultationServiceByContractRequest objRequest)
        {
            ConsultationServiceByContractResponse oConsultationServiceByContractResponse = new ConsultationServiceByContractResponse();

            try
            {
                oConsultationServiceByContractResponse = Claro.Web.Logging.ExecuteMethod<ConsultationServiceByContractResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.Fixed.GetCustomerLineNumber(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return oConsultationServiceByContractResponse;

        }


        /// <summary>Método que obtiene el id de interacción por id de caso.</summary>
        /// <param name="objRequest"></param>
        /// <returns>EntitiesFixed.GetCaseInsert.CaseInsertResponse</returns>
        /// <remarks>GetInteractIDforCaseID</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list> 
        public static EntitiesFixed.GetCaseInsert.CaseInsertResponse GetInteractIDforCaseID(EntitiesFixed.GetCaseInsert.CaseInsertRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.CaseInsertResponse Response = new EntitiesFixed.GetCaseInsert.CaseInsertResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.GetCaseInsert.CaseInsertResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.Fixed.GetInteractIDforCaseID(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;

        }
    }

}
