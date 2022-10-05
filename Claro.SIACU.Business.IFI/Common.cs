using Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Data.IFI.Configuration;
using System;
using System.Net.Mail;
using System.IO;
using System.Collections.Generic;
using Constant = Claro.SIACU;
using Datainfreatructure = Claro.SIACU.Data.IFI;
using networkconfig = Claro.SIACU.Data.IFI.Configuration;
using KEY = Claro.ConfigurationManager;
using COMMON = Claro.SIACU.Entity.IFI.Common;

using ClaroService = Claro.Utils;
using Claro.Data;
using System.Threading;
using Claro.SIACU.Entity.IFI.Common.GetUpdateInter30;
using Claro.SIACU.Entity.IFI.Common.GetValidateMail;
using Claro.SIACU.Entity.IFI.Common.GetNumberSMS;
using Claro.SIACU.Entity.IFI.Common.GetUploadDocumentOnBase;
using Claro.SIACU.Entity.IFI.Common.GetGenerateConstancy;
using EntitiesEmail = Claro.SIACU.Entity.IFI.Common.GetSendEmailWithBase64;
using EntitiesDigitalSignature = Claro.SIACU.Entity.IFI.Common.GetDigitalSignature;

//namespace Claro.SIACU.Entity.IFI.Common.GetRegAudit

namespace Claro.SIACU.Business.IFI
{
    public class Common
    {
        private static List<string> _arrConexionesRed = new List<string>();
        private static System.Timers.Timer tmrController = null;
        private static Thread thrProcess = null;
        private static int intTimeTranscurrido = Claro.Constants.NumberZero;
        private static int intTimeout = Claro.Constants.NumberZero;

        ///<summary>Método que inserta interacciones del negocio</summary>
        ///<param name="request"></param>
        ///<returns>String<returns>
        ///<remarks>COMMON.GetBusinessInteraction2.BusinessInteraction2Response</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetBusinessInteraction2.BusinessInteraction2Response GetBusinessInteraction2(COMMON.GetBusinessInteraction2.BusinessInteraction2Request request)
        {
            string strInteractionId = "", strFlagInsertion = "", strMessage = "";
            COMMON.GetBusinessInteraction2.BusinessInteraction2Response objResponse =
                new COMMON.GetBusinessInteraction2.BusinessInteraction2Response()
                {
                    ProcessOK = Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.IFI.Common.InsertBusinessInteraction2(request.Audit.Session, request.Audit.Transaction, request.Item, out strInteractionId,
                            out strFlagInsertion, out strMessage);
                    })
                };
            objResponse.InteractionId = strInteractionId;
            objResponse.FlagInsertion = strFlagInsertion;
            objResponse.MsgText = strMessage;
            return objResponse;
        }


        ///<summary>Método que obtiene la evidencia de la inserción de la interacción</summary>
        ///<param name="objRequest"></param>
        ///<returns>COMMON.GetInsertEvidence.InsertEvidenceResponse<returns>
        ///<remarks>GetInsertEvidence</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetInsertEvidence.InsertEvidenceResponse GetInsertEvidence(COMMON.GetInsertEvidence.InsertEvidenceRequest objRequest)
        {
            var objResponse = new COMMON.GetInsertEvidence.InsertEvidenceResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Common.GetInsertEvidence(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }


        ///<summary>Método que registra la auditoría</summary>
        ///<param name="objGrabarAuditoria"></param>
        ///<returns>COMMON.GetSaveAudit.SaveAuditResponse<returns>
        ///<remarks>SaveAudit</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetSaveAudit.SaveAuditResponse SaveAudit(COMMON.GetSaveAudit.SaveAuditRequest objGrabarAuditoria)
        {
            COMMON.GetSaveAudit.SaveAuditResponse objAuditResponse = new COMMON.GetSaveAudit.SaveAuditResponse();

            objAuditResponse = Web.Logging.ExecuteMethod(objGrabarAuditoria.Audit.Session, objGrabarAuditoria.Audit.Transaction,
                () =>
                {
                    return Claro.SIACU.Data.IFI.Common.SaveAudit(objGrabarAuditoria.Audit.Session, objGrabarAuditoria.Audit.Transaction, objGrabarAuditoria.vCuentaUsuario, objGrabarAuditoria.vIpCliente, objGrabarAuditoria.vIpServidor, objGrabarAuditoria.vMonto,
                                                                                        objGrabarAuditoria.vNombreCliente, objGrabarAuditoria.vNombreServidor, objGrabarAuditoria.vServicio, objGrabarAuditoria.vTelefono, objGrabarAuditoria.vTexto,
                                                                                         objGrabarAuditoria.vTransaccion);

                });

            return objAuditResponse;
        }

        ///<summary>Método que inserta interacciones del negocio del tipo fixed</summary>
        ///<param name="request"></param>
        ///<returns>COMMON.GetBusinessInteraction2.BusinessInteraction2Response<returns>
        ///<remarks>GetBusinessInteractionFixed</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetBusinessInteraction2.BusinessInteraction2Response GetBusinessInteractionFixed(COMMON.GetBusinessInteraction2.BusinessInteraction2Request request)
        {
            string strInteractionId = "", strFlagInsertion = "", strMessage = "";
            COMMON.GetBusinessInteraction2.BusinessInteraction2Response objResponse =
                new COMMON.GetBusinessInteraction2.BusinessInteraction2Response()
                {
                    ProcessOK = Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.IFI.Common.InsertBusinessInteractionFixed(request.Audit.Session, request.Audit.Transaction, request.Item, ref strInteractionId,
                            ref strFlagInsertion, ref strMessage);
                    })
                };
            objResponse.InteractionId = strInteractionId;
            objResponse.FlagInsertion = strFlagInsertion;
            objResponse.MsgText = strMessage;
            return objResponse;
        }
      

        ///<summary>Método que obtiene el listado de motivos</summary>
        ///<param name="objMotiveSotRequest"></param>
        ///<returns>COMMON.GetMotiveSot.MotiveSotResponse<returns>
        ///<remarks>getMotiveSot</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetMotiveSot.MotiveSotResponse getMotiveSot(COMMON.GetMotiveSot.MotiveSotRequest objMotiveSotRequest)
        {
            var objMotiveSot = new COMMON.GetMotiveSot.MotiveSotResponse
            {
                getMotiveSot = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Common.ListItem>>
                (
                    objMotiveSotRequest.Audit.Session,
                    objMotiveSotRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.GetMotiveSot(objMotiveSotRequest.Audit.Session,
                            objMotiveSotRequest.Audit.Transaction);
                    })
            };
            return objMotiveSot;
        }


        ///<summary>Método que valida el horario</summary>
        ///<param name="objScheduleRequest"></param>
        ///<returns>bool</returns>
        ///<remarks>ValidateSchedule</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static bool ValidateSchedule(Claro.SIACU.Entity.IFI.Common.GetSchedule.ScheduleRequest objScheduleRequest)
        {
            return Claro.Web.Logging.ExecuteMethod(
                   objScheduleRequest.Audit.Session,
                   objScheduleRequest.Audit.Transaction,
                   () =>
                   {
                       return Data.IFI.Common.ValidateSchedule(objScheduleRequest.Audit.Session, objScheduleRequest.Audit.Transaction, objScheduleRequest);
                   }
                  );

        }


        ///<summary>Método que obtiene los datos del parametro</summary>
        ///<param name="request"></param>
        ///<returns>COMMON.GetParameterData.ParameterDataResponse</returns>
        ///<remarks>GetParameterData</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetParameterData.ParameterDataResponse GetParameterData(COMMON.GetParameterData.ParameterDataRequest request)
        {
            string message = "";
            COMMON.GetParameterData.ParameterDataResponse objResponse = new COMMON.GetParameterData.ParameterDataResponse()
            {
                Parameter = Claro.Web.Logging.ExecuteMethod<COMMON.ParameterData>(request.Audit.Session, request.Audit.Transaction,
                () => { return Data.IFI.Common.GetParameterData(request.Audit.Session, request.Audit.Transaction, request.Name, ref message); })
            };
            objResponse.Message = message;
            return objResponse;
        }


        ///<summary>Método que inserta log en la transacción</summary>
        ///<param name="request"></param>
        ///<returns>COMMON.GetInsertLogTrx.InsertLogTrxResponse</returns>
        ///<remarks>InsertLogTrx</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetInsertLogTrx.InsertLogTrxResponse InsertLogTrx(COMMON.GetInsertLogTrx.InsertLogTrxRequest request)
        {
            string flagInsertion = "";
            COMMON.GetInsertLogTrx.InsertLogTrxResponse objResponse = new COMMON.GetInsertLogTrx.InsertLogTrxResponse()
            {
                Exito = Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Data.IFI.Common.InsertLogTrx(request.Aplicacion, request.Transaccion, request.Opcion, request.Accion,
                        request.Phone, request.IdInteraction, request.IdTypification, request.User, request.IPPCClient, request.PCClient, request.IPServer,
                        request.NameServer, request.InputParameters, request.OutpuParameters, ref flagInsertion);
                })
            };
            objResponse.FlagInsertion = flagInsertion;
            return objResponse;
        }
      

        ///<summary>Método que genera la constancia en formato PDF</summary>
        ///<param name="request"></param>
        ///<returns>COMMON.GetGenerateConstancy.GenerateConstancyResponse</returns>
        ///<remarks>GetGenerateContancyPDF</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetGenerateConstancy.GenerateConstancyResponse GetGenerateContancyPDF(COMMON.GetGenerateConstancy.GenerateConstancyRequest request)
        {
            string errorMessage = string.Empty;

            COMMON.GetGenerateConstancy.GenerateConstancyResponse objResponse = new COMMON.GetGenerateConstancy.GenerateConstancyResponse()
            {
                Generated = Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Data.IFI.Common.GenerateConstancyPDF(request.Audit.Session, request.Audit.Transaction,
                        request.ParametersGeneratePDFGeneric, ref errorMessage);
                })
            };

            objResponse.ErrorMessage = errorMessage;

            return objResponse;
        }


        ///<summary>Método que obtiene información de las reglas de negocio.</summary>
        ///<param name="objBusinessRulesRequest"></param>
        ///<returns>COMMON.GetBusinessRules.BusinessRulesResponse</returns>
        ///<remarks>GetBusinessRules</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetBusinessRules.BusinessRulesResponse GetBusinessRules(COMMON.GetBusinessRules.BusinessRulesRequest objBusinessRulesRequest)
        {
            COMMON.GetBusinessRules.BusinessRulesResponse objBusinessRulesResponse = new COMMON.GetBusinessRules.BusinessRulesResponse();
            try
            {
                objBusinessRulesResponse.ListBusinessRules = Claro.Web.Logging.ExecuteMethod<List<COMMON.BusinessRules>>(objBusinessRulesRequest.Audit.Session, objBusinessRulesRequest.Audit.Transaction, () => { return Data.IFI.Common.GetBusinessRules(objBusinessRulesRequest.Audit.Session, objBusinessRulesRequest.Audit.Transaction, objBusinessRulesRequest.SUB_CLASE); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBusinessRulesRequest.Audit.Session, objBusinessRulesRequest.Audit.Transaction, ex.InnerException.Message);
            }

            return objBusinessRulesResponse;
        }


        ///<summary>Método para obtener tipo de cac o dac.</summary>
        ///<param name="objCacDacTypeRequest"></param>
        ///<returns>COMMON.GetCacDacType.CacDacTypeResponse</returns>
        ///<remarks>GetCacDacType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetCacDacType.CacDacTypeResponse GetCacDacType(COMMON.GetCacDacType.CacDacTypeRequest objCacDacTypeRequest)
        {
            var objCacDacTypeResponse = new COMMON.GetCacDacType.CacDacTypeResponse
            {
                CacDacTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Common.ListItem>>
                (objCacDacTypeRequest.Audit.Session,
                    objCacDacTypeRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.GetCacDacType(objCacDacTypeRequest.Audit.Session,
                            objCacDacTypeRequest.Audit.Transaction,
                            Data.IFI.Common.GetCodeList(objCacDacTypeRequest.Audit.Session,
                                objCacDacTypeRequest.Audit.Transaction,
                                Claro.SIACU.Constants.DAC));
                    })
            };

            return objCacDacTypeResponse;
        }



        /// <summary>Método para obtener lista del estado civil.</summary>
        /// <param name="objEstCivTypeRequest"></param>      
        /// <returns>objEstCivTypeResponse</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018</FecCrea></item></list>
        public static COMMON.GetEstCivType.EstCivTypeResponse GetEstCivType(COMMON.GetEstCivType.EstCivTypeRequest objEstCivTypeRequest)
        {
            var objEstCivTypeResponse = new COMMON.GetEstCivType.EstCivTypeResponse
            {
                EstCivTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Common.ListItem>>
                (objEstCivTypeRequest.Audit.Session,
                    objEstCivTypeRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.GetEstCivType(objEstCivTypeRequest.Audit.Session,
                            objEstCivTypeRequest.Audit.Transaction,
                            Data.IFI.Common.GetCodeList(objEstCivTypeRequest.Audit.Session,
                                objEstCivTypeRequest.Audit.Transaction,
                                Claro.SIACU.Constants.DAC));
                    })
            };

            return objEstCivTypeResponse;
        }


        /// <summary>Método para obtener lista de tipo de vías.</summary>
        /// <param name="objViasTypeRequest">/param>      
        /// <returns>COMMON.GetViasType.ViasTypeResponse</returns>
        /// <remarks>GetViasType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018</FecCrea></item></list>
        public static COMMON.GetViasType.ViasTypeResponse GetViasType(COMMON.GetViasType.ViasTypeRequest objViasTypeRequest)
        {
            var objViasTypeResponse = new COMMON.GetViasType.ViasTypeResponse
            {
                ViasTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Common.ListItem>>
                (objViasTypeRequest.Audit.Session,
                    objViasTypeRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.GetViasType(objViasTypeRequest.Audit.Session,
                            objViasTypeRequest.Audit.Transaction,
                            Data.IFI.Common.GetCodeList(objViasTypeRequest.Audit.Session,
                                objViasTypeRequest.Audit.Transaction,
                                Claro.SIACU.Constants.DAC));
                    })
            };

            return objViasTypeResponse;
        }


        /// <summary>Método para obtener lista de tipo de manzanas.</summary>
        /// <param name="objManzanasTypeRequest">/param>      
        /// <returns>COMMON.GetManzanasType.ManzanasTypeResponse</returns>
        /// <remarks>GetManzanasType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018</FecCrea></item></list>
        public static COMMON.GetManzanasType.ManzanasTypeResponse GetManzanasType(COMMON.GetManzanasType.ManzanasTypeRequest objManzanasTypeRequest)
        {
            var objManzanasTypeResponse = new COMMON.GetManzanasType.ManzanasTypeResponse
            {
                ManzanasTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Common.ListItem>>
                (objManzanasTypeRequest.Audit.Session,
                    objManzanasTypeRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.GetManzanasType(objManzanasTypeRequest.Audit.Session,
                            objManzanasTypeRequest.Audit.Transaction,
                            Data.IFI.Common.GetCodeList(objManzanasTypeRequest.Audit.Session,
                                objManzanasTypeRequest.Audit.Transaction,
                                Claro.SIACU.Constants.DAC));
                    })
            };

            return objManzanasTypeResponse;
        }


        /// <summary>Método para obtener lista de tipo de Interiores.</summary>
        /// <param name="objInterioresTypeRequest"></param>      
        /// <returns>COMMON.GetInterioresType.InterioresTypeResponse</returns>
        /// <remarks>GetInterioresType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018</FecCrea></item></list>
        public static COMMON.GetInterioresType.InterioresTypeResponse GetInterioresType(COMMON.GetInterioresType.InterioresTypeRequest objInterioresTypeRequest)
        {
            var objInterioresTypeResponse = new COMMON.GetInterioresType.InterioresTypeResponse
            {
                InterioresTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Common.ListItem>>
                (objInterioresTypeRequest.Audit.Session,
                    objInterioresTypeRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.GetInterioresType(objInterioresTypeRequest.Audit.Session,
                            objInterioresTypeRequest.Audit.Transaction,
                            Data.IFI.Common.GetCodeList(objInterioresTypeRequest.Audit.Session,
                                objInterioresTypeRequest.Audit.Transaction,
                                Claro.SIACU.Constants.DAC));
                    })
            };

            return objInterioresTypeResponse;
        }


        /// <summary>Método para obtener lista de tipo de Urbanizaciones.</summary>
        /// <param name="objUrbsTypeRequest"></param>      
        /// <returns>COMMON.GetUrbsType.UrbsTypeResponse</returns>
        /// <remarks>GetUrbsType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018</FecCrea></item></list>
        public static COMMON.GetUrbsType.UrbsTypeResponse GetUrbsType(COMMON.GetUrbsType.UrbsTypeRequest objUrbsTypeRequest)
        {
            var objUrbsTypeResponse = new COMMON.GetUrbsType.UrbsTypeResponse
            {
                UrbsTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Common.ListItem>>
                (objUrbsTypeRequest.Audit.Session,
                    objUrbsTypeRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.GetUrbsType(objUrbsTypeRequest.Audit.Session,
                            objUrbsTypeRequest.Audit.Transaction,
                            Data.IFI.Common.GetCodeList(objUrbsTypeRequest.Audit.Session,
                                objUrbsTypeRequest.Audit.Transaction,
                                Claro.SIACU.Constants.DAC));
                    })
            };

            return objUrbsTypeResponse;
        }


        /// <summary>Método para obtener lista de tipo de zonas.</summary>
        /// <param name="objZonesTypeRequest">/param>      
        /// <returns>COMMON.GetZonesType.ZonesTypeResponse</returns>
        /// <remarks>GetZonesType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018</FecCrea></item></list>
        public static COMMON.GetZonesType.ZonesTypeResponse GetZonesType(COMMON.GetZonesType.ZonesTypeRequest objZonesTypeRequest)
        {
            var objZonesTypeResponse = new COMMON.GetZonesType.ZonesTypeResponse
            {
                ZonesTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Common.ListItem>>
                (objZonesTypeRequest.Audit.Session,
                    objZonesTypeRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.GetZonesType(objZonesTypeRequest.Audit.Session,
                            objZonesTypeRequest.Audit.Transaction,
                            Data.IFI.Common.GetCodeList(objZonesTypeRequest.Audit.Session,
                                objZonesTypeRequest.Audit.Transaction,
                                Claro.SIACU.Constants.DAC));
                    })
            };

            return objZonesTypeResponse;
        }

        /// <summary>Método para obtener listado de ubigeo </summary>
        /// <param name="objUbigeosTypeRequest"></param>      
        /// <returns>COMMON.GetUbigeosType.UbigeosTypeResponse</returns>
        /// <remarks>GetUbigeosType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>09/10/2018</FecCrea></item></list>
        public static COMMON.GetUbigeosType.UbigeosTypeResponse GetUbigeosType(COMMON.GetUbigeosType.UbigeosTypeRequest objUbigeosTypeRequest,int dep,int prov)
        {
            var objUbigeosTypeResponse = new COMMON.GetUbigeosType.UbigeosTypeResponse
            {
                UbigeosTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Common.ListItem>>
                (objUbigeosTypeRequest.Audit.Session,
                    objUbigeosTypeRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.GetUbigeosType(objUbigeosTypeRequest.Audit.Session,
                            objUbigeosTypeRequest.Audit.Transaction,
                            Data.IFI.Common.GetCodeList(objUbigeosTypeRequest.Audit.Session,
                                objUbigeosTypeRequest.Audit.Transaction,
                                Claro.SIACU.Constants.DAC), dep,prov);
                    })
            };

            return objUbigeosTypeResponse;
        }


        /// <summary>Método para obtener lista del tipo de nacionalidad</summary>
        /// <param name="objNacTypeRequest"></param>
        /// <returns>objNacTypeRequest</returns>
        /// <remarks>GetNacType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018</FecCrea></item></list>
        public static COMMON.GetNacType.NacTypeResponse GetNacType(COMMON.GetNacType.NacTypeRequest objNacTypeRequest)
        {
            var objNacTypeResponse = new COMMON.GetNacType.NacTypeResponse
            {
                NacTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Common.ListItem>>
                (objNacTypeRequest.Audit.Session,
                    objNacTypeRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.GetNacType(objNacTypeRequest.Audit.Session,
                            objNacTypeRequest.Audit.Transaction,
                            Data.IFI.Common.GetCodeList(objNacTypeRequest.Audit.Session,
                                objNacTypeRequest.Audit.Transaction,
                                Claro.SIACU.Constants.DAC));
                    })
            };

            return objNacTypeResponse;
        }


        /// <summary>Carga los datos de la tipificación</summary>
        /// <param name="objTypificationRequest"></param> 
        /// <returns>COMMON.GetTypification.TypificationResponse</returns>
        /// <remarks>GetTypification</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetTypification.TypificationResponse GetTypification(COMMON.GetTypification.TypificationRequest objTypificationRequest)
        {
            COMMON.GetTypification.TypificationResponse objTypificationResponse = new COMMON.GetTypification.TypificationResponse()
            {
                ListTypification = Claro.Web.Logging.ExecuteMethod<List<COMMON.Typification>>(objTypificationRequest.Audit.Session, objTypificationRequest.Audit.Transaction, () => { return Data.IFI.Common.GetTypification(objTypificationRequest.Audit.Session, objTypificationRequest.Audit.Transaction, objTypificationRequest.TRANSACTION_NAME); })
            };

            return objTypificationResponse;
        }

        #region interacciones
     

        /// <summary>Método que inserta la plantilla de la interacción</summary>
        /// <param name="objrequest"></param> 
        /// <returns>COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse</returns>
        /// <remarks>GetInsertInteractionTemplate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse GetInsertInteractionTemplate(COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionRequest objrequest)
        {
            string flagInsercion = "";
            string msgText = "";
            COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse objresponse =
             new COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse()
               {
                   ProcesSucess = Claro.Web.Logging.ExecuteMethod<bool>(objrequest.Audit.Session, objrequest.Audit.Transaction,
                   () =>
                   {
                       return Claro.SIACU.Data.IFI.Common.RegistrationInsertTemplateInteraction(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.item, objrequest.IdInteraction, out flagInsercion, out msgText);

                   })
               };
            objresponse.FlagInsercion = flagInsercion;
            objresponse.MsgText = msgText;
            return objresponse;

        }


        /// <summary>Método que inserta la plantilla de la interacción(similar al metodo de arriba pero diferentes sp y variables)</summary>
        /// <param name="objrequest"></param> 
        /// <returns>COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse</returns>
        /// <remarks>GetInsertInteractionTemplate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse GetInsInteractionTemplate(COMMON.GetInsTemplateInteraction.InsTemplateInteractionRequest objrequest)
        {
            string flagInsercion = "";
            string msgText = "";
            COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse objresponse =
                new COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse()

                {
                    ProcessSucess = Claro.Web.Logging.ExecuteMethod<bool>(objrequest.Audit.Session, objrequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.IFI.Common.InsTemplateInteraction(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.item, objrequest.IdInteraction, out flagInsercion, out msgText);

                    })
                };
            objresponse.FlagInsercion = flagInsercion;
            objresponse.MsgText = msgText;
            return objresponse;


        }


        /// <summary>Método que inserta los datos de la interacción</summary>
        /// <param name="objrequest"></param> 
        /// <returns>COMMON.GetInsertInteract.InsertInteractResponse</returns>
        /// <remarks>GetInsertInteract</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetInsertInteract.InsertInteractResponse GetInsertInteract(COMMON.GetInsertInteract.InsertInteractRequest objrequest)
        {
            Claro.Web.Logging.Info(objrequest.Audit.Session, objrequest.Audit.Transaction, "Transaction: Entra a GetInsertInteract");

            string interactionid = "";
            string flagInsercio = "";
            string msgText = "";
            COMMON.GetInsertInteract.InsertInteractResponse objresponse =
            new COMMON.GetInsertInteract.InsertInteractResponse()
            {
                ProcesSucess = Claro.Web.Logging.ExecuteMethod<bool>(objrequest.Audit.Session, objrequest.Audit.Transaction,
                  () =>
                  {
                      return Claro.SIACU.Data.IFI.Common.InsertInteraction(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.item, out interactionid, out flagInsercio, out  msgText);

                  })

            };
            objresponse.Interactionid = interactionid;
            objresponse.FlagInsercion = flagInsercio;
            objresponse.MsgText = msgText;

            Claro.Web.Logging.Info(objrequest.Audit.Session, objrequest.Audit.Transaction, "Transaction: sale de GetInsertInteract objresponse.Interactionid: " + objresponse.Interactionid + "objresponse.FlagInsercion: " + objresponse.FlagInsercion + "objresponse.MsgText: " + objresponse.MsgText);

            return objresponse;
        }


        /// <summary>Método que inserta los datos de la interacción(similar al metodo de arriba pero diferentes sp y variables)</summary>
        /// <param name="objrequest"></param> 
        /// <returns>COMMON.GetInsertInt.InsertIntResponse</returns>
        /// <remarks>GetInsertInt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetInsertInt.InsertIntResponse GetInsertInt(COMMON.GetInsertInt.InsertIntRequest objrequest)
        {
            Claro.Web.Logging.Info(objrequest.Audit.Session, objrequest.Audit.Transaction, "Transaction: Entra a GetInsertInt");

            string interactionid = "";
            string flagInsercio = "";
            string msgText = "";
            var InteraccionService = KEY.AppSettings("gInteraccionService");

            if (InteraccionService != Claro.SIACU.Constants.Yes)
            {
            COMMON.GetInsertInt.InsertIntResponse objresponse =
                new COMMON.GetInsertInt.InsertIntResponse()
                {
                    ProcesSucess = Claro.Web.Logging.ExecuteMethod<bool>(objrequest.Audit.Session, objrequest.Audit.Transaction,
                       () =>
                       {
                           return Claro.SIACU.Data.IFI.Common.Insert(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.item, out interactionid, out flagInsercio, out msgText);

                       })
                };
            objresponse.Interactionid = interactionid;
            objresponse.FlagInsercion = flagInsercio;
            objresponse.MsgText = msgText;

            Claro.Web.Logging.Info(objrequest.Audit.Session, objrequest.Audit.Transaction, "Interactionid: " + objresponse.Interactionid + "FlagInsercion: " + objresponse.FlagInsercion + "MsgText: " + objresponse.MsgText);

            return objresponse;
        }
        else{
            COMMON.GetInsertInt.InsertIntResponse objresponse =
                new COMMON.GetInsertInt.InsertIntResponse()
                {
                    ProcesSucess = Claro.Web.Logging.ExecuteMethod<bool>(objrequest.Audit.Session, objrequest.Audit.Transaction,
                       () =>
                       {
                           return Claro.SIACU.Data.IFI.Common.InsertService(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.item, out interactionid, out flagInsercio, out msgText);

                       })
                };
            objresponse.Interactionid = interactionid;
            objresponse.FlagInsercion = flagInsercio;
            objresponse.MsgText = msgText;

            Claro.Web.Logging.Info(objrequest.Audit.Session, objrequest.Audit.Transaction, "Interactionid: " + objresponse.Interactionid + "FlagInsercion: " + objresponse.FlagInsercion + "MsgText: " + objresponse.MsgText);

            return objresponse;
            }


        }


        /// <summary>Método que obtiene datos del cliente</summary>
        /// <param name="objrequest"></param> 
        /// <returns>COMMON.GetClient.ClientResponse</returns>
        /// <remarks>GetObtClient</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetClient.ClientResponse GetObtClient(COMMON.GetClient.ClientRequest objrequest)
        {

            Claro.Web.Logging.Info(objrequest.Audit.Session, objrequest.Audit.Transaction, "Transaction: Entra a GetObtClient strphone" + objrequest.strphone + "//straccount : " + objrequest.straccount + "// strContactobjid : " + objrequest.strContactobjid + "//strflagreg : " + objrequest.strflagreg);

            string MsgText = "";
            string flagquery = "";
            COMMON.GetClient.ClientResponse objresponse =
                new COMMON.GetClient.ClientResponse()
                {
                    listClient = Claro.Web.Logging.ExecuteMethod<COMMON.Client>(objrequest.Audit.Session, objrequest.Audit.Transaction,

                    () =>
                    {
                        return Claro.SIACU.Data.IFI.Common.GetClient(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.strphone, objrequest.straccount, objrequest.strContactobjid, objrequest.strflagreg, out  flagquery, out MsgText);
                    })

                };
            objresponse.Flagquery = flagquery;
            objresponse.MsgText = MsgText;

            Claro.Web.Logging.Info(objrequest.Audit.Session, objrequest.Audit.Transaction, "Transaction: sale de GetObtClient Flagquery" + objresponse.Flagquery + "//MsgText : " + objresponse.MsgText);

            return objresponse;

        }


        /// <summary>Método que inserta los datos de la plantilla de interacción</summary>
        /// <param name="objrequest"></param> 
        /// <returns>COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralResponse</returns>
        /// <remarks>GetInserInteractionTemplateresponse</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralResponse GetInserInteractionTemplateresponse(COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralRequest objrequest)
        {
            Claro.Web.Logging.Info(objrequest.Audit.Session, objrequest.Audit.Transaction, "Transaction: Entra a GetInserInteractionTemplateresponse");

            var objresponse = new COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralResponse();
            bool resultadoPlantilla = false;
            string strTransaccion = ClaroService.CheckStr(objrequest.InteractionTemplate._NOMBRE_TRANSACCION);
            string strcontingeciaClarify = KEY.AppSettings("gConstContingenciaClarify");
            var InteraccionService = KEY.AppSettings("gInteraccionService");

            if (strcontingeciaClarify != Claro.SIACU.Constants.Yes)
            {
                var flagInssercion = string.Empty;
                var msgtext = string.Empty;

                if (InteraccionService != Claro.SIACU.Constants.Yes)
                {
                objresponse.rResult = Web.Logging.ExecuteMethod(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Common.RegistrationInsertTemplateInteraction(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.InteractionTemplate, objrequest.vInteraccionId, out flagInssercion, out msgtext);
                });
                objresponse.rFlagInsercion = flagInssercion;
                objresponse.rMsgText = msgtext;
                resultadoPlantilla = objresponse.rResult;
                }
                else{

                objresponse.rResult = Web.Logging.ExecuteMethod(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Common.RegistrationInsertTemplateInteractionService(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.InteractionTemplate, objrequest.vInteraccionId, out flagInssercion, out msgtext);
                });
                objresponse.rFlagInsercion = flagInssercion;
                objresponse.rMsgText = msgtext;
                resultadoPlantilla = objresponse.rResult;

                }

            }
            else
            {
                var flaginsercion = string.Empty;
                var msgtext = string.Empty;
                objresponse.rResult = Web.Logging.ExecuteMethod(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
                  {
                      return Data.IFI.Common.InsTemplateInteraction(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.InteractionTemplate, objrequest.vInteraccionId, out flaginsercion, out msgtext);

                  });
                objresponse.rFlagInsercion = flaginsercion;
                objresponse.rMsgText = msgtext;
                resultadoPlantilla = objresponse.rResult;

            }
            objrequest.InteractionTemplate._ID_INTERACCION = objrequest.vInteraccionId;
            objresponse.rResult = true;
            if (strTransaccion != string.Empty && resultadoPlantilla)
            {
                objresponse.rCodigoRetornoTransaccion = Claro.Constants.NumberZeroString;
                objresponse.rMensajeErrorTransaccion = string.Empty;
            }

            Claro.Web.Logging.Info(objrequest.Audit.Session, objrequest.Audit.Transaction, "Transaction: sale de GetInserInteractionTemplateresponse");


            return objresponse;
        }


        /// <summary>Método que inserta las interacciones generales del negocio</summary>
        /// <param name="objrequest"></param> 
        /// <returns>COMMON.GetInsertGeneral.InsertGeneralResponse</returns>
        /// <remarks>GetIsertInteractionBusiness</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetInsertGeneral.InsertGeneralResponse GetIsertInteractionBusiness(COMMON.GetInsertGeneral.InsertGeneralRequest objrequest)
        {
            var objGetInsertInteractionMixedResponse = new COMMON.GetInsertGeneral.InsertGeneralResponse();
            var ContingenciaClarify = KEY.AppSettings("gConstContingenciaClarify");
            var strMsg1 = string.Empty;
            var strMsg2 = string.Empty;
            var strcoderetorn = string.Empty;
            var strmessagetextTransaccion = string.Empty;
            var rtrmsgTextInteraccion = string.Empty;
            string strTelefono;
            var strFlagInsercionInteraccion = string.Empty;
            var rInteraccionId = string.Empty;
            var rFlagInsercion = string.Empty;
            var rMsgText = string.Empty;
            bool resultado;
            if (objrequest.vNroTelefono == objrequest.Interaction.TELEFONO)
            {

                strTelefono = objrequest.vNroTelefono;
            }
            else
            {
                strTelefono = objrequest.Interaction.TELEFONO;
            }
            var objrequestClient = new COMMON.GetClient.ClientRequest
            {
                strphone = strTelefono,
                straccount = string.Empty,
                strContactobjid = objrequest.Interaction.OBJID_CONTACTO,
                strflagreg = Claro.Constants.NumberOneString,
                Audit = objrequest.Audit
            };
            var objreponseClient = GetObtClient(objrequestClient);

            if (objreponseClient != null)
            {
                if (objreponseClient.listClient != null)
                {
                    objrequest.Interaction.OBJID_CONTACTO = objreponseClient.listClient.OBJID_CONTACTO;
                    objrequest.Interaction.OBJID_SITE = objreponseClient.listClient.OBJID_SITE;
                }
                strMsg1 = objreponseClient.Flagquery;
                strMsg2 = objreponseClient.MsgText;

            }
            if (ContingenciaClarify != Claro.SIACU.Constants.Yes)
            {
                var objInsertrequest = new COMMON.GetInsertInt.InsertIntRequest
               {
                   item = objrequest.Interaction,
                   Audit = objrequest.Audit
               };
                var objinsertireeponse = GetInsertInt(objInsertrequest);
                resultado = objinsertireeponse.ProcesSucess;
                rInteraccionId = objinsertireeponse.Interactionid;
                rFlagInsercion = objinsertireeponse.FlagInsercion;
                rMsgText = objinsertireeponse.MsgText;
            }
            else
            {


                var objinserinteractionrequest = new COMMON.GetInsertInteract.InsertInteractRequest()
                {
                    item = objrequest.Interaction,
                    Audit = objrequest.Audit
                };
                var objinsertinteractionresponse = GetInsertInteract(objinserinteractionrequest);
                resultado = objinsertinteractionresponse.ProcesSucess;
                rInteraccionId = objinsertinteractionresponse.Interactionid;
                rMsgText = objinsertinteractionresponse.MsgText;


            }

            if (!String.IsNullOrEmpty(rInteraccionId))
            {
                if (objrequest.InteractionTemplate != null)
                {
                    var objgetinsertinteractiontemplaterequest = new COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralRequest
                {
                    InteractionTemplate = objrequest.InteractionTemplate,
                    vInteraccionId = rInteraccionId,
                    vNroTelefono = objrequest.vNroTelefono,
                    vUSUARIO_SISTEMA = objrequest.vUSUARIO_SISTEMA,
                    vUSUARIO_APLICACION = objrequest.vPASSWORD_USUARIO,
                    vEjecutarTransaccion = objrequest.vEjecutarTransaccion,
                    Audit = objrequest.Audit

                };
                    var objgetinsertinteractiontemplateresponse = GetInserInteractionTemplateresponse(objgetinsertinteractiontemplaterequest);
                    strcoderetorn = objgetinsertinteractiontemplateresponse.rCodigoRetornoTransaccion;
                    strmessagetextTransaccion = objgetinsertinteractiontemplateresponse.rMensajeErrorTransaccion;
                    rtrmsgTextInteraccion = objgetinsertinteractiontemplateresponse.rMsgText;
                    strFlagInsercionInteraccion = objgetinsertinteractiontemplateresponse.rFlagInsercion;
                }

            }


            objGetInsertInteractionMixedResponse.rInteraccionId = rInteraccionId;
            objGetInsertInteractionMixedResponse.rFlagInsercion = rFlagInsercion;

            //cambiadas
            objGetInsertInteractionMixedResponse.rFlagInsercionInteraccion = strFlagInsercionInteraccion;
            objGetInsertInteractionMixedResponse.rMsgTextInteraccion = rtrmsgTextInteraccion;
            objGetInsertInteractionMixedResponse.rCodigoRetornoTransaccion = strcoderetorn;
            objGetInsertInteractionMixedResponse.rMsgTextTransaccion = strmessagetextTransaccion;
            objGetInsertInteractionMixedResponse.rResult = resultado;
            objGetInsertInteractionMixedResponse.rMsgText = rMsgText;

            return objGetInsertInteractionMixedResponse;

        }

        #endregion


        #region auditoria fdq


        /// <summary>Método que guarda los datos de la auditoría</summary>
        /// <param name="objRegistAuditoria"></param> 
        /// <returns>COMMON.GetSaveAuditM.SaveAuditMResponse</returns>
        /// <remarks>SaveAuditM</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GetSaveAuditM.SaveAuditMResponse SaveAuditM(COMMON.GetSaveAuditM.SaveAuditMRequest objRegistAuditoria)
        {
            COMMON.GetSaveAuditM.SaveAuditMResponse oRegistroAuditoriaResponse = new COMMON.GetSaveAuditM.SaveAuditMResponse();

            oRegistroAuditoriaResponse = Web.Logging.ExecuteMethod(objRegistAuditoria.Audit.Session, objRegistAuditoria.Audit.Transaction,
                   () =>
                   {
                       return Claro.SIACU.Data.IFI.Common.SaveAuditM(objRegistAuditoria);

                   });

            return oRegistroAuditoriaResponse;
        }


        #endregion

        #region jcaa


        /// <summary>Método que genera la constancia en formato PDF</summary>
        /// <param name="RequestParam"></param> 
        /// <returns>COMMON.GeneratePDF.GeneratePDFDataResponse</returns>
        /// <remarks>GeneratePDF</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static COMMON.GeneratePDF.GeneratePDFDataResponse GeneratePDF(COMMON.GeneratePDF.GeneratePDFDataRequest RequestParam)
        {
            COMMON.GeneratePDF.GeneratePDFDataResponse serviceResponse = new COMMON.GeneratePDF.GeneratePDFDataResponse();
            try
            {
                Claro.Web.Logging.Info(RequestParam.Audit.Session, RequestParam.Audit.Transaction, "Antes de entrar al metodo generateConstancePDF- business ");
                serviceResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GeneratePDF.GeneratePDFDataResponse>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.IFI.Service.GeneratePDF.generateConstancePDF(RequestParam);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(RequestParam.Audit.Session, RequestParam.Audit.Transaction, "Error en el business HPXTREAM" + ex.Message);
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);



            }

            COMMON.GeneratePDF.GeneratePDFDataResponse Result = new COMMON.GeneratePDF.GeneratePDFDataResponse()
            {
                Generated = serviceResponse.Generated,
                FilePath = serviceResponse.FilePath
            };
            return Result;
        }
        #endregion


        /// <summary>Método que valida el flujo del ETA.</summary>
        /// <param name="RequestParam"></param>   
        /// <returns>Entity.IFI.Common.ETAFlowValidate.ETAFlowResponse</returns>
        /// <remarks>ETAFlowValidate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static Entity.IFI.Common.ETAFlowValidate.ETAFlowResponse ETAFlowValidate(Entity.IFI.Common.ETAFlowValidate.ETAFlowRequest RequestParam)
        {
            Entity.IFI.Common.ETAFlow listServiceResponse = null;
            Entity.IFI.Common.ETAFlow listService = null;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.ETAFlow>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.IFI.Common.ETAFlowValidate(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.as_origen, RequestParam.av_idplano, RequestParam.av_ubigeo, RequestParam.an_tiptra, RequestParam.an_tipsrv);
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
            Entity.IFI.Common.ETAFlowValidate.ETAFlowResponse Resultado = new Entity.IFI.Common.ETAFlowValidate.ETAFlowResponse()
            {
                ETAFlow = listServiceResponse
            };
            return Resultado;
        }


        #region Redireccion

        /// <summary>Método que valida la comunicación en la redirección.</summary>
        /// <param name="objValidateRedirectRequest"></param>   
        /// <returns>Entity.IFI.Common.GetValidateCommunication.ValidateCommunicationResponse</returns>
        /// <remarks>ValidateRedirectCommunication</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static Entity.IFI.Common.GetValidateCommunication.ValidateCommunicationResponse ValidateRedirectCommunication(Entity.IFI.Common.GetValidateCommunication.ValidateCommunicationRequest objValidateRedirectRequest)
        {
            Entity.IFI.Common.GetValidateCommunication.ValidateCommunicationResponse objValidateRedirectResponse = null;
            string strUrlDest = "", strAvailability = "", strErrorMsg = "", strJsonParameters = "";
            try
            {
                Claro.Web.Logging.Info(objValidateRedirectRequest.Audit.Session, objValidateRedirectRequest.Audit.Transaction, "Business sequence1 " + objValidateRedirectRequest.Sequence);
                objValidateRedirectResponse = new Entity.IFI.Common.GetValidateCommunication.ValidateCommunicationResponse()
                {
                    ResultValCommunication = Claro.Web.Logging.ExecuteMethod<Boolean>(objValidateRedirectRequest.Audit.Session, objValidateRedirectRequest.Audit.Transaction, () => { return Data.IFI.Common.ValidateRedirectCommunication(objValidateRedirectRequest.Audit, objValidateRedirectRequest.Sequence, out strErrorMsg, objValidateRedirectRequest.Server, out strUrlDest, out strAvailability, out strJsonParameters); }),
                    Availability = strAvailability,
                    url = strUrlDest,
                    ErrorMessage = strErrorMsg,
                    JsonString = strJsonParameters
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objValidateRedirectRequest.Audit.Session, objValidateRedirectRequest.Audit.Transaction, "Business ex " + ex + "ex_inner" + ex.InnerException);
                Claro.Web.Logging.Error(objValidateRedirectRequest.Audit.Session, objValidateRedirectRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objValidateRedirectResponse;
        }
        #endregion

        #region ENVIAR EMAIL


        /// <summary>Método que envía un correo electrónico.</summary>
        /// <param name="request"></param>   
        /// <returns>COMMON.GetSendEmail.SendEmailResponse</returns>
        /// <remarks>GetSendEmail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetSendEmail.SendEmailResponse GetSendEmail(COMMON.GetSendEmail.SendEmailRequest request)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = new COMMON.GetSendEmail.SendEmailResponse();
            string strExit = "";
            MailAddress strFrom = new MailAddress(request.strSender);
            MailAddress strTo = new MailAddress(request.strTo);
            MailMessage strmessage = new MailMessage(strFrom, strTo);
            try
            {
                strmessage.IsBodyHtml = true;
                strmessage.Body = request.strMessage;
                strmessage.BodyEncoding = System.Text.Encoding.UTF8;
                strmessage.Subject = request.strSubject;
                strmessage.SubjectEncoding = System.Text.Encoding.UTF8;
                if (!string.IsNullOrEmpty(request.strCC))
                    strmessage.CC.Add(request.strCC);

                if (!string.IsNullOrEmpty(request.strBCC))
                    strmessage.Bcc.Add(request.strBCC);

                strmessage.Priority = MailPriority.High;

                //CAMBIO PARA ENVIAR CORREO POR ARREGLO DE BYTES


                System.IO.MemoryStream memStream = null;
                System.IO.StreamWriter streamWriter = null;
                Attachment thisAttachment = null;

                if (request.AttachedByte != null)
                {
                    memStream = new System.IO.MemoryStream(request.AttachedByte);
                    streamWriter = new System.IO.StreamWriter(memStream);
                    streamWriter.Flush();
                    memStream.Position = 0;
                    thisAttachment = new Attachment(memStream, "application/pdf");
                    thisAttachment.ContentDisposition.FileName = request.strAttached;
                    strmessage.Attachments.Add(thisAttachment);
                }


                SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.Host = "LIMMAILF1.tim.com.pe";
                smtpClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                smtpClient.EnableSsl = false;
                smtpClient.Send(strmessage);
                strExit = Constant.Constants.Message_OK;
                smtpClient.Dispose();

            }
            catch (Exception ex)
            {
                strExit = ex.Message.ToString();
            }
            finally
            {
                strmessage = null;
            }
            objresponse.Exit = strExit;
            return objresponse;
        }


        /// <summary>Método que envía correo electrónico alterno.</summary>
        /// <param name="request"></param>   
        /// <returns>COMMON.GetSendEmail.SendEmailResponse</returns>
        /// <remarks>GetSendEmailAlt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetSendEmail.SendEmailResponse GetSendEmailAlt(COMMON.GetSendEmail.SendEmailRequest request)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = new COMMON.GetSendEmail.SendEmailResponse();
            string strExit = "";
            string strhost = KEY.AppSettings("ServerEmail");
            string strlogginguser = KEY.AppSettings("Userlogging");
            string strpassword = KEY.AppSettings("Userpassword");
            MailAddress strFrom = new MailAddress(request.strSender);
            MailAddress strTo = new MailAddress(request.strTo);
            MailMessage strmessage = new MailMessage(strFrom, strTo);
            int i = 0;
            bool blfileCopy = false;
            string strrouteBegin = "";
            string strRouteDestiny = KEY.AppSettings("CopyDestinationpath");
            try
            {
                byte[] arrbuffer;
                System.IO.FileStream stream;
                strmessage.IsBodyHtml = true;
                strmessage.Body = request.strMessage;
                strmessage.BodyEncoding = System.Text.Encoding.UTF8;
                strmessage.Subject = request.strSubject;
                strmessage.SubjectEncoding = System.Text.Encoding.UTF8;
                if (!string.IsNullOrEmpty(request.strCC))
                    strmessage.CC.Add(request.strCC);

                if (!string.IsNullOrEmpty(request.strBCC))
                    strmessage.Bcc.Add(request.strBCC);

                strmessage.Priority = MailPriority.High;
                GlobalDocument objglobal = new GlobalDocument();
                if (request.strAttached != null)
                {
                    string[] File = request.strAttached.Split('|');
                    string[] FileAtach = request.strJoinfile.Split('|');

                    GlobalDocument global = new GlobalDocument();
                    foreach (string item in File)
                    {
                        strrouteBegin = File[i].Substring(0, File[i].Length - FileAtach[i].Length);
                        string c = strrouteBegin + FileAtach[i];
                        COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationRequest getfiledefaultrequest = new COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationRequest()
                        {

                            Audit = request.Audit,
                            strPath = c
                        };
                        global = GetfileDefaultImpersonation(getfiledefaultrequest).objGlobalDocument;
                        arrbuffer = global.Document;
                        string strnewdestiny = System.IO.Path.Combine(strRouteDestiny, FileAtach[i]);
                        stream = new System.IO.FileStream(strnewdestiny, System.IO.FileMode.Create);
                        stream.Write(arrbuffer, 0, arrbuffer.Length);
                        stream.Close();
                        blfileCopy = false;
                        blfileCopy = System.IO.File.Exists(strnewdestiny);

                        if (blfileCopy == true)
                        {
                            strmessage.Attachments.Add(new Attachment(strnewdestiny));
                        }
                        i = i + 1;
                    }
                }

                SmtpClient smtpClient = new System.Net.Mail.SmtpClient(strhost);
                smtpClient.Port = Convert.ToInt(KEY.AppSettings("PortServer"));
                smtpClient.ServicePoint.MaxIdleTime = 2;
                smtpClient.Credentials = new System.Net.NetworkCredential(strlogginguser, strpassword);
                smtpClient.Timeout = Convert.ToInt(KEY.AppSettings("TimeOutEmail"));
                smtpClient.EnableSsl = true;
                smtpClient.Send(strmessage);
                strExit = Constant.Constants.Message_OK;
                smtpClient.Dispose();

            }
            catch (Exception ex)
            {
                strExit = ex.Message.ToString();
            }
            objresponse.Exit = strExit;
            return objresponse;

        }


        /// <summary>Método que obtiene archivo de suplantación por defecto.</summary>
        /// <param name="request"></param>   
        /// <returns>COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse</returns>
        /// <remarks>GetfileDefaultImpersonation</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse GetfileDefaultImpersonation(COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationRequest request)
        {
            byte[] arrbuffer = null;

            COMMON.GlobalDocument objGlobalDoc = new GlobalDocument();
            INetworkConfiguration objInetWorkConfiguration = NetworkConfiguration.SIAC_POST_DirFacturas;

            string strFileDefault = Claro.Data.Network.Connect(request.Audit.Session, request.Audit.Transaction, objInetWorkConfiguration, System.IO.Path.GetPathRoot(request.strPath)).ToString();

            if (int.TryParse(KEY.AppSettings("timeOutElectronicBills"), out intTimeout))
            {
                intTimeTranscurrido = Claro.Constants.NumberZero;
                tmrController = new System.Timers.Timer(1);
                thrProcess = new Thread(new ThreadStart(() =>
                    {
                        arrbuffer = GetFileDefaultImpersonationTime(request.Audit.Session, request.Audit.Transaction, request.strPath);

                    }));
                thrProcess.Start();
                tmrController.Elapsed += new System.Timers.ElapsedEventHandler(CheckTimeOut);
                tmrController.Start();
                thrProcess.Join();

                objGlobalDoc.strCodeError = Claro.Constants.NumberZeroString;
                objGlobalDoc.Document = arrbuffer;
                objGlobalDoc.strMesaggeError = "";

                if ((intTimeTranscurrido >= intTimeout))
                {
                    Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, "Se excedio el tiempo de espera: " + intTimeout);
                    objGlobalDoc.strMesaggeError = Claro.SIACU.Constants.MessageTimeOut;
                    objGlobalDoc.strCodeError = Claro.Constants.NumberOneString;
                }
                else if (arrbuffer == null)
                {
                    objGlobalDoc.strMesaggeError = Claro.SIACU.Constants.MessageFileNotExist;
                    objGlobalDoc.strCodeError = Claro.Constants.NumberTwoString;
                }

            }
            else
            {
                throw new Exception("no se ha definido el valor del timeout en el key : strValorTimeOutFacturaSE");

            }
            COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse response = new COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse();

            response.objGlobalDocument = objGlobalDoc;


            return response;
        }


        #region metodos que son llamados por el enviaremailalt


        /// <summary>Método que conecta al archivo de suplantacion.</summary>
        /// <param name="strIdsession"></param>      
        /// <param name="strTransaction"></param>      
        /// <param name="strPath"></param>      
        /// <returns>byte[]</returns>
        /// <remarks>GetFileDefaultImpersonationTime</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static byte[] GetFileDefaultImpersonationTime(string strIdSession, string strTransaction, string strPath)
        {
            ConectUnityNetwork(strIdSession, strTransaction, Path.GetPathRoot(strPath));
            byte[] arrbuffer = GetFileInternal(strIdSession, strTransaction, strPath);
            return arrbuffer;
        }


        /// <summary>Método que conecta a la unidad de red.</summary>
        /// <param name="strIdsession"></param>      
        /// <param name="strTransaction"></param>      
        /// <param name="strPath"></param>      
        /// <remarks>ConectUnityNetwork</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static void ConectUnityNetwork(string strIdsession, string strTransaction, string strPath)
        {
            List<string> arrConectionNetwork = new List<string>();

            arrConectionNetwork.Add(strPath);

        }


        /// <summary>Método que obtiene el archivo interno.</summary>
        /// <param name="strIdsession"></param>      
        /// <param name="strTransaction"></param>      
        /// <param name="strPath"></param>      
        /// <returns>byte[] </returns>
        /// <remarks>GetFileInternal</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static byte[] GetFileInternal(string strIdsession, string strTransaction, string strPath)
        {
            byte[] buffer = null;
            if (File.Exists(strPath))
            {
                Stream stream = null;
                try
                {
                    using (stream = File.Open(strPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        long length = stream.Length;
                        if (length > 0L)
                        {
                            buffer = new byte[length];
                            stream.Read(buffer, 0, (int)length);

                        }
                    }
                }
                catch (Exception exception)
                {
                    Claro.Web.Logging.Error(strIdsession, strTransaction, "el stream se cierra correctamente luego de la excepcion - " + exception.Message);


                }
                finally
                {

                    if (stream != null)
                    {
                        stream.Close();
                    }
                }

            }
            return buffer;
        }


        /// <summary>Método para verificar el timeout.</summary>
        /// <param name="sender"></param>      
        /// <param name="e"></param>   
        /// <remarks>CheckTimeOut</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static void CheckTimeOut(object sender, System.Timers.ElapsedEventArgs e)
        {
            if ((intTimeTranscurrido >= intTimeout))
            {
                tmrController.Stop();
                if (thrProcess != null)
                {
                    thrProcess.Abort();
                    thrProcess = null;
                }
            }
        }
        #endregion

        #endregion


        /// <summary>Método que inserta los datos de la interacción por un SP alterno.</summary>
        /// <param name="objRequest"></param>      
        /// <returns>COMMON.GetInsertInteractHFC.InsertInteractHFCResponse</returns>
        /// <remarks>GetInsertInteractHFC</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetInsertInteractHFC.InsertInteractHFCResponse GetInsertInteractHFC(COMMON.GetInsertInteractHFC.InsertInteractHFCRequest objRequest)
        {
            var objResponse = new COMMON.GetInsertInteractHFC.InsertInteractHFCResponse();

            try
            {
                var rInteraccionId = string.Empty;
                var rFlagInsercion = string.Empty;
                var rMsgText = string.Empty;

                objResponse.rResult = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Common.GetInsertInteractHFC(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Interaction, ref rInteraccionId, ref rFlagInsercion, ref rMsgText);
                });

                objResponse.rInteraccionId = rInteraccionId;
                objResponse.rFlagInsercion = rFlagInsercion;
                objResponse.rMsgText = rMsgText;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        /// <summary>Método que obtiene los datos de la plantilla de la interacción.</summary>
        /// <param name="objRequest"></param>      
        /// <returns>COMMON.GetDatTempInteraction.DatTempInteractionResponse</returns>
        /// <remarks>GetInfoInteractionTemplate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetDatTempInteraction.DatTempInteractionResponse GetInfoInteractionTemplate(COMMON.GetDatTempInteraction.DatTempInteractionRequest objRequest)
        {
            var objResponse = new COMMON.GetDatTempInteraction.DatTempInteractionResponse();
            var vFLAG_CONSULTA = string.Empty;
            var vMSG_TEXT = string.Empty;
            try
            {
                var entity = Claro.Web.Logging.ExecuteMethod<COMMON.InteractionTemplate>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.GetDatTempInteraction(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vInteraccionID, ref vFLAG_CONSULTA, ref vMSG_TEXT);
                    });
                objResponse.vFLAG_CONSULTA = vFLAG_CONSULTA;
                objResponse.vMSG_TEXT = vMSG_TEXT;
                objResponse.InteractionTemplate = entity;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        /// <summary>Método que obtiene el número GWP.</summary>
        /// <param name="objRequest"></param>      
        /// <returns>COMMON.GetNumberGWP.NumberGWPResponse</returns>
        /// <remarks>GetNumberGWP</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetNumberGWP.NumberGWPResponse GetNumberGWP(COMMON.GetNumberGWP.NumberGWPRequest objRequest)
        {
            string vNumber = "";
            COMMON.GetNumberGWP.NumberGWPResponse objResponse = new COMMON.GetNumberGWP.NumberGWPResponse()
            {
                Number = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Common.GetNumberGWP(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Msisdn, ref vNumber);
                })
            };
            return objResponse;
        }


        /// <summary>Método que obtiene el número EAI.</summary>
        /// <param name="objRequest"></param>      
        /// <returns>COMMON.GetNumberEAI.NumberEAIResponse</returns>
        /// <remarks>GetNumberEAI</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetNumberEAI.NumberEAIResponse GetNumberEAI(COMMON.GetNumberEAI.NumberEAIRequest objRequest)
        {
            string vNumber = "";
            COMMON.GetNumberEAI.NumberEAIResponse objResponse = new COMMON.GetNumberEAI.NumberEAIResponse()
            {
                Number = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Common.GetNumberEAI(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Msisdn, ref vNumber);
                })
            };
            return objResponse;
        }


        /// <summary>Método que obtiene las opciones habilitadas por usuario.</summary>
        /// <param name="objReadOptionsByUserRequest"></param>      
        /// <returns>COMMON.ReadOptionsByUser.ReadOptionsByUserResponse</returns>
        /// <remarks>ReadOptionsByUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.ReadOptionsByUser.ReadOptionsByUserResponse ReadOptionsByUser(COMMON.ReadOptionsByUser.ReadOptionsByUserRequest objReadOptionsByUserRequest)
        {

            COMMON.ReadOptionsByUser.ReadOptionsByUserResponse objReadOptionsByUserResponse = new COMMON.ReadOptionsByUser.ReadOptionsByUserResponse();
            objReadOptionsByUserResponse.ListOption = Claro.Web.Logging.ExecuteMethod<List<COMMON.PaginaOption>>(objReadOptionsByUserRequest.Audit.Session, objReadOptionsByUserRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Common.ReadOptionsByUser(objReadOptionsByUserRequest.IdSession, objReadOptionsByUserRequest.Transaction, objReadOptionsByUserRequest.IdAplication, objReadOptionsByUserRequest.IdUser);
            });

            return objReadOptionsByUserResponse;
        }


        /// <summary>Método que obtiene los datos del empleado por usuario.</summary>
        /// <param name="objEmployeeRequest"></param>      
        /// <returns>COMMON.GetEmployeByUser.EmployeeResponse</returns>
        /// <remarks>GetEmployeByUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetEmployeByUser.EmployeeResponse GetEmployeByUser(COMMON.GetEmployeByUser.EmployeeRequest objEmployeeRequest)
        {

            COMMON.GetEmployeByUser.EmployeeResponse objEmployeeResponse = new COMMON.GetEmployeByUser.EmployeeResponse();
            objEmployeeResponse.lstEmployee = Claro.Web.Logging.ExecuteMethod<List<COMMON.Employee>>(objEmployeeRequest.Audit.Session, objEmployeeRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Common.GetEmployeByUser(objEmployeeRequest.Audit.Session, objEmployeeRequest.Audit.Transaction, objEmployeeRequest.UserName);
            });

            return objEmployeeResponse;
        }


        /// <summary>Método que verifica el usuario.</summary>
        /// <param name="objCheckingUserRequest"></param>      
        /// <returns>COMMON.CheckingUser.CheckingUserResponse</returns>
        /// <remarks>CheckingUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.CheckingUser.CheckingUserResponse CheckingUser(COMMON.CheckingUser.CheckingUserRequest objCheckingUserRequest)
        {

            COMMON.CheckingUser.CheckingUserResponse objCheckingUserResponse;

            objCheckingUserResponse = Claro.Web.Logging.ExecuteMethod<COMMON.CheckingUser.CheckingUserResponse>(objCheckingUserRequest.Audit.Session, objCheckingUserRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Common.CheckingUser(objCheckingUserRequest.Audit.Session, objCheckingUserRequest.Audit.Transaction, objCheckingUserRequest.IpAplicacion, objCheckingUserRequest.Audit.ApplicationName, objCheckingUserRequest.Usuario, objCheckingUserRequest.AppCod);
            });

            return objCheckingUserResponse;
        }


        /// <summary>Método que valida el monto a autorizar.</summary>
        /// <param name="objRequest"></param>      
        /// <returns>CCOMMON.GetEvaluateAmount.EvaluateAmountResponse</returns>
        /// <remarks>GetEvaluateAmount</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetEvaluateAmount.EvaluateAmountResponse GetEvaluateAmount(COMMON.GetEvaluateAmount.EvaluateAmountRequest objRequest)
        {
            var objResponse = new COMMON.GetEvaluateAmount.EvaluateAmountResponse();
            try
            {
                objResponse.Resultado = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Common.GetEvaluateAmount(objRequest.StrIdSession, objRequest.StrTransaction, objRequest.VListaPerfil, objRequest.VMonto, objRequest.VUnidad, objRequest.VModalidad, objRequest.VTipoTelefono);
                });
            }

            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        /// <summary>Método que valida el monto.</summary>
        /// <param name="objRequest"></param>      
        /// <returns>CCOMMON.GetEvaluateAmount.EvaluateAmountResponse</returns>
        /// <remarks>GetEvaluateAmount_DCM</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetEvaluateAmount.EvaluateAmountResponse GetEvaluateAmount_DCM(COMMON.GetEvaluateAmount.EvaluateAmountRequest objRequest)
        {
            var objResponse = new COMMON.GetEvaluateAmount.EvaluateAmountResponse();
            try
            {
                objResponse.Resultado = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Common.GetEvaluateAmount_DCM(objRequest.StrIdSession, objRequest.StrTransaction, objRequest.VListaPerfil, objRequest.VMonto, objRequest.VUnidad, objRequest.VModalidad, objRequest.VTipoTelefono);
                });
            }

            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        #region External/ Internal Transfer


        /// <summary>Método que obtiene el tipo de trabajo.</summary>
        /// <param name="objWorkTypeRequest"></param>      
        /// <returns>COMMON.GetWorkType.WorkTypeResponse</returns>
        /// <remarks>GetWorkType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetWorkType.WorkTypeResponse GetWorkType(COMMON.GetWorkType.WorkTypeRequest objWorkTypeRequest)
        {
            COMMON.GetWorkType.WorkTypeResponse objWorkTypeResponse = new COMMON.GetWorkType.WorkTypeResponse()
            {
                WorkTypes = Claro.Web.Logging.ExecuteMethod<List<ListItem>>
                (objWorkTypeRequest.Audit.Session,
                objWorkTypeRequest.Audit.Transaction,
                () =>
                {
                    return Data.IFI.Common.GetWorkType(objWorkTypeRequest.Audit.Session,
                        objWorkTypeRequest.Audit.Transaction,
                        objWorkTypeRequest.TransacType);
                })
            };

            return objWorkTypeResponse;
        }


        #endregion


        /// <summary>Método que obtiene los datos del usuario.</summary>
        /// <param name="objUserRequest"></param>      
        /// <returns>COMMON.GetUser.UserResponse</returns>
        /// <remarks>GetUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetUser.UserResponse GetUser(COMMON.GetUser.UserRequest objUserRequest)
        {
            COMMON.GetUser.UserResponse objTypificationResponse = new COMMON.GetUser.UserResponse()
            {
                UserModel = Claro.Web.Logging.ExecuteMethod<User>(objUserRequest.Audit.Session, objUserRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.GetUser(objUserRequest.Audit.Session, objUserRequest.Audit.Transaction, objUserRequest.CodeUser, objUserRequest.CodeRol, objUserRequest.CodeCac, objUserRequest.State);
                    })
            };

            return objTypificationResponse;
        }


        /// <summary>Método que obtiene los datos del empleado.</summary>
        /// <param name="objDatosEmpleadoRequest"></param>      
        /// <returns>COMMON.GetEmployerDate.GetEmployerDateResponse</returns>
        /// <remarks>GetEmployerDate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetEmployerDate.GetEmployerDateResponse GetEmployerDate(COMMON.GetEmployerDate.GetEmployerDateRequest objDatosEmpleadoRequest)
        {

            COMMON.GetEmployerDate.GetEmployerDateResponse objAuditResponse = new COMMON.GetEmployerDate.GetEmployerDateResponse();

            objAuditResponse = Web.Logging.ExecuteMethod(objDatosEmpleadoRequest.Audit.Session, objDatosEmpleadoRequest.Audit.Transaction,
                () =>
                {
                    return Datainfreatructure.Common.GetEmployerDate(objDatosEmpleadoRequest);

                });

            return objAuditResponse;
        }


        /// <summary>Método que permite obtener los parámetros del terminarl TPI.</summary>
        /// <param name="objRequest"></param>      
        /// <returns>COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse</returns>
        /// <remarks>GetParameterTerminalTPI</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse GetParameterTerminalTPI(COMMON.GetParameterTerminalTPI.ParameterTerminalTPIRequest objRequest)
        {
            string Message = "";
            COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse objResponse = new COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse()
            {
                ListParameterTeminalTPI = Claro.Web.Logging.ExecuteMethod<List<ParameterTerminalTPI>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Common.GetParameterTerminalTPI(objRequest.Audit.Session, objRequest.Audit.Transaction,
                        objRequest.ParameterID, ref Message);
                })
            };
            objResponse.Message = Message;
            return objResponse;
        }


        /// <summary>Método que permite enviar correoS electrónicos adjuntado la archivos.</summary>
        /// <param name="request"></param>      
        /// <returns>COMMON.GetSendEmail.SendEmailResponse</returns>
        /// <remarks>GetSendEmailAltFixed</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetSendEmail.SendEmailResponse GetSendEmailFixed(COMMON.GetSendEmail.SendEmailRequest request)
        {
            Claro.Web.Logging.Info(request.Audit.Session, request.Audit.Transaction, "Ingreso a metodo GetSendEmailFixed");
            COMMON.GetSendEmail.SendEmailResponse objresponse = new COMMON.GetSendEmail.SendEmailResponse();
            string strExit = "";
            MailAddress strFrom = new MailAddress(request.strSender);
            Claro.Web.Logging.Info(request.Audit.Session, request.Audit.Transaction, "Email strFrom :" + request.strSender);
            MailAddress strTo = new MailAddress(request.strTo);
            Claro.Web.Logging.Info(request.Audit.Session, request.Audit.Transaction, "Email strTo :" + request.strTo);
            MailMessage strmessage = new MailMessage(strFrom, strTo);
            Claro.Web.Logging.Info(request.Audit.Session, request.Audit.Transaction, "Email message :" + request.strMessage);
            try
            {
                strmessage.IsBodyHtml = true;
                strmessage.Body = request.strMessage;
                strmessage.BodyEncoding = System.Text.Encoding.UTF8;
                strmessage.Subject = request.strSubject;
                strmessage.SubjectEncoding = System.Text.Encoding.UTF8;
                if (!string.IsNullOrEmpty(request.strCC))
                    strmessage.CC.Add(request.strCC);

                if (!string.IsNullOrEmpty(request.strBCC))
                    strmessage.Bcc.Add(request.strBCC);

                strmessage.Priority = MailPriority.High;

                //CAMBIO PARA ENVIAR CORREO POR ARREGLO DE BYTES              

                if (request.strAttached != null)
                {
                    string[] File = request.strAttached.Split('|');
                    foreach (var item in File)
                    {
                        if (System.IO.File.Exists(item))
                        {
                            strmessage.Attachments.Add(new Attachment(item));
                        }
                    }
                }


                System.IO.MemoryStream memStream = null;
                System.IO.StreamWriter streamWriter = null;
                Attachment thisAttachment = null;

                if (request.AttachedByte != null)
                {
                    memStream = new System.IO.MemoryStream(request.AttachedByte);
                    streamWriter = new System.IO.StreamWriter(memStream);
                    streamWriter.Flush();
                    memStream.Position = 0;
                    thisAttachment = new Attachment(memStream, "application/pdf");
                    thisAttachment.ContentDisposition.FileName = request.strAttached;
                    strmessage.Attachments.Add(thisAttachment);
                }


                SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.Host = "LIMMAILF1.tim.com.pe";
                Claro.Web.Logging.Info(request.Audit.Session, request.Audit.Transaction, "Server Email Host: " + smtpClient.Host);
                smtpClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                smtpClient.EnableSsl = false;
                smtpClient.Send(strmessage);
                strExit = Constant.Constants.Message_OK;
                smtpClient.Dispose();

            }
            catch (Exception ex)
            {
                strExit = ex.Message.ToString();
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, "hubo un error GetSendEmailFixed " + strExit);
            }
            finally
            {
                strmessage = null;
            }
            objresponse.Exit = strExit;
            return objresponse;

        }


        /// <summary>Método que permite validar las credenciales.</summary>
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
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static bool Get_Credentials(string strIdSession, string strTransaction, string strConexion, out string struser, out string strpass, out string strdomain)
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
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));

                struser = string.Empty;
                strpass = string.Empty;
                strdomain = string.Empty;
                return false;
            }
        }


        /// <summary>Método que permite mostrar archivos del servidor compartido.</summary>
        /// <param name="strIdSession"></param>     
        /// <param name="strTransaction"></param>  
        /// <param name="strPath"></param>  
        /// <param name="mydata"></param>  
        /// <returns>bool</returns>
        /// <remarks>DisplayFileFromServerSharedFile</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static bool DisplayFileFromServerSharedFile(string strIdSession, string strTransaction, string strPath, out byte[] mydata)
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
                    Claro.Web.Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Suplantación OK");
                    blnResult = true;
                }
                else
                {
                    Claro.Web.Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Error de Suplantación ");
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
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));

                mydata = null;
                blnResult = false;
            }

            obj.undoImpersonatiom();

            return blnResult;
        }


        /// <summary>Método que permite enviar correoS electrónicos adjuntado la archivos.</summary>
        /// <param name="request"></param>      
        /// <returns>COMMON.GetSendEmail.SendEmailResponse</returns>
        /// <remarks>GetSendEmailAltFixed</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static COMMON.GetSendEmail.SendEmailResponse GetSendEmailAltFixed(COMMON.GetSendEmail.SendEmailRequest request)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = new COMMON.GetSendEmail.SendEmailResponse();
            string strExit = "";
            MailAddress strFrom = new MailAddress(request.strSender);
            MailAddress strTo = new MailAddress(request.strTo);
            MailMessage strmessage = new MailMessage(strFrom, strTo);
            try
            {
                strmessage.IsBodyHtml = true;
                strmessage.Body = request.strMessage;
                strmessage.BodyEncoding = System.Text.Encoding.UTF8;
                strmessage.Subject = request.strSubject;
                strmessage.SubjectEncoding = System.Text.Encoding.UTF8;
                if (!string.IsNullOrEmpty(request.strCC))
                    strmessage.CC.Add(request.strCC);

                if (!string.IsNullOrEmpty(request.strBCC))
                    strmessage.Bcc.Add(request.strBCC);

                strmessage.Priority = MailPriority.High;

                //CAMBIO PARA ENVIAR CORREO POR ARREGLO DE BYTES              

                if (request.strAttached != null)
                {
                    string[] File = request.strAttached.Split('|');

                    foreach (var item in File)
                    {

                        byte[] attachFile = null;
                        if (DisplayFileFromServerSharedFile(request.Audit.Session, request.Audit.Transaction, item, out attachFile))
                        {

                            System.IO.MemoryStream memStream = null;
                            System.IO.StreamWriter streamWriter = null;
                            Attachment thisAttachment = null;
                            memStream = new System.IO.MemoryStream(attachFile);
                            streamWriter = new System.IO.StreamWriter(memStream);
                            streamWriter.Flush();
                            memStream.Position = 0;

                            thisAttachment = new Attachment(memStream, "application/pdf");

                            string newItem = item.Substring(item.LastIndexOf(@"\")).Replace(@"\", "");
                            thisAttachment.ContentDisposition.FileName = newItem;
                            strmessage.Attachments.Add(thisAttachment);

                        }

                    }
                }

                SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.Host = "LIMMAILF1.tim.com.pe";
                smtpClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                smtpClient.EnableSsl = false;
                smtpClient.Send(strmessage);
                strExit = Constant.Constants.Message_OK;
                smtpClient.Dispose();

            }
            catch (Exception ex)
            {
                strExit = ex.Message.ToString();
            }
            finally
            {
                strmessage = null;
            }
            objresponse.Exit = strExit;
            return objresponse;

        }

        /// <summary>Método que obtiene el listado con el tipo de estados.</summary>
        /// <param name="objStateTypeRequest"></param>      
        /// <returns>Entity.IFI.Common.GetStateType.StateTypeResponse</returns>
        /// <remarks>GetStateType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static Entity.IFI.Common.GetStateType.StateTypeResponse GetStateType(Entity.IFI.Common.GetStateType.StateTypeRequest objStateTypeRequest)
        {
            Entity.IFI.Common.GetStateType.StateTypeResponse objStateTypeResponse = new Entity.IFI.Common.GetStateType.StateTypeResponse()
            {
                StateTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Common.ListItem>>(objStateTypeRequest.Audit.Session, objStateTypeRequest.Audit.Transaction, 
                () => { return Data.IFI.Common.GetStateType(objStateTypeRequest.Audit.Session, objStateTypeRequest.Audit.Transaction, objStateTypeRequest.IdList); })
            };
            return objStateTypeResponse;
        }


        /// <summary>Método que obtiene el tipo de transacciones.</summary>
        /// <param name="objRequest"></param>      
        /// <returns>Entity.IFI.Common.GetTransactionType.TransactionTypeResponse</returns>
        /// <remarks>GetTransactionType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static Entity.IFI.Common.GetTransactionType.TransactionTypeResponse GetTransactionType(Entity.IFI.Common.GetTransactionType.TransactionTypeRequest objRequest)
        {
            Entity.IFI.Common.GetTransactionType.TransactionTypeResponse objTransactionTypeResponse = new Entity.IFI.Common.GetTransactionType.TransactionTypeResponse()
            {
                TransactionTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Common.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, 
                () => { return Data.IFI.Common.GetTransactionType(objRequest.Audit.Session, objRequest.Audit.Transaction); })
            };

            return objTransactionTypeResponse;
        }


        /// <summary>Método que permite las notas de la interacción.</summary>
        /// <param name="objRequest"></param>      
        /// <returns>UpdatexInter30Response</returns>
        /// <remarks>GetUpdatexInter30</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static UpdatexInter30Response GetUpdatexInter30(UpdatexInter30Request objRequest)
        {
            var objResponse = new UpdatexInter30Response();

            try
            {
                var rFlagInsercion = string.Empty;
                var rMsgText = string.Empty;

                objResponse.rResult = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.UpdatexInter30(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.p_objid, objRequest.p_texto, ref rFlagInsercion, ref rMsgText);
                    });

                objResponse.rFlagInsercion = rFlagInsercion;
                objResponse.rMsgText = rMsgText;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        /// <summary>Método que permite validar el correo electrónico.</summary>
        /// <param name="objRequest"></param>      
        /// <returns>Claro.SIACU.Entity.IFI.Common.GetValidateMail.ValidateMailResponse</returns>
        /// <remarks>GetValidateMail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static Claro.SIACU.Entity.IFI.Common.GetValidateMail.ValidateMailResponse GetValidateMail(Claro.SIACU.Entity.IFI.Common.GetValidateMail.ValidateMailRequest objRequest)
        {
            var objResponse = new ValidateMailResponse();

            try
            {
                objResponse.strValidaCorreo = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.IFI.Common.GetValidateMail(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.CustomerCode); });
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        /// <summary>Método que obtiene el número del cliente a enviar SMS</summary>
        /// <param name="objRequest"></param>      
        /// <returns>Claro.SIACU.Entity.IFI.Common.GetNumberSMS.NumberSMSResponse</returns>
        /// <remarks>GetNumberSMS</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018</FecCrea></item></list>
        public static Claro.SIACU.Entity.IFI.Common.GetNumberSMS.NumberSMSResponse GetNumberSMS(Claro.SIACU.Entity.IFI.Common.GetNumberSMS.NumberSMSRequest objRequest)
        {
            var objResponse = new NumberSMSResponse();

            try
            {
                objResponse.numeroSMS = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.IFI.Common.GetNumberSMS(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.strCustomerCode); });
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        /// <summary>Método que permite obtener datos de la ultima facturación de un cliente</summary>
        /// <param name="objRequest"></param>      
        /// <returns>COMMON.GetLastInvoiceData.LastInvoiceDataResponse</returns>
        /// <remarks>GetLastInvoiceData</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15-10-2018</FecCrea></item></list>
        public static COMMON.GetLastInvoiceData.LastInvoiceDataResponse GetLastInvoiceData(COMMON.GetLastInvoiceData.LastInvoiceDataRequest objLastInvoiceDataRequest, string strCustomerCode)
        {
            var objLastInvoiceDataResponse = new COMMON.GetLastInvoiceData.LastInvoiceDataResponse
            {
                LastInvoiceDatas = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Common.Receipt>>
                (objLastInvoiceDataRequest.Audit.Session,
                    objLastInvoiceDataRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.GetLastInvoiceData(objLastInvoiceDataRequest.Audit.Session,
                            objLastInvoiceDataRequest.Audit.Transaction,
                            Data.IFI.Common.GetCodeList(objLastInvoiceDataRequest.Audit.Session,
                                objLastInvoiceDataRequest.Audit.Transaction,
                                Claro.SIACU.Constants.DAC), strCustomerCode);
                    })
            };

            return objLastInvoiceDataResponse;
        }


        /// <summary>Método que valida claves para datapower.</summary>
        /// <param name="objIsOkGetKeyRequest"></param> 
        /// <returns>COMMON.IsOkGetKey.IsOkGetKeyResponse </returns>
        /// <remarks>IsOkGetKey</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2019.</FecCrea></item></list>
        public static COMMON.IsOkGetKey.IsOkGetKeyResponse IsOkGetKey(COMMON.IsOkGetKey.IsOkGetKeyRequest objIsOkGetKeyRequest)
        {
            string User = "";
            string Pass = "";
            var objIsOkGetKeyResponse = new COMMON.IsOkGetKey.IsOkGetKeyResponse
           {

               result = Claro.Web.Logging.ExecuteMethod<bool>
                 (objIsOkGetKeyRequest.Audit.Session,
                     objIsOkGetKeyRequest.Audit.Transaction,
                     () =>
                     {
                         return Data.IFI.Common.IsOkGetKey(objIsOkGetKeyRequest.Audit.Session,
                                                           objIsOkGetKeyRequest.Audit.Transaction,
                                                           objIsOkGetKeyRequest.Audit.IPAddress,
                                                           objIsOkGetKeyRequest.Audit.IPAddress,
                                                           objIsOkGetKeyRequest.Audit.UserName,
                                                           objIsOkGetKeyRequest.Audit.ApplicationName,
                                                           out User, out  Pass);
                     })
           };
            objIsOkGetKeyResponse.User = User;
            objIsOkGetKeyResponse.Pass = Pass;
            return objIsOkGetKeyResponse;
        }


        /// <summary>Método que carga los documentos por Onbase.</summary>
        /// <param name="objUploadDocumentOnBaseRequest"></param> 
        /// <returns>UploadDocumentOnBaseResponse</returns>
        /// <remarks>GetUploadDocumentOnBase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2019.</FecCrea></item></list>
        public static UploadDocumentOnBaseResponse GetUploadDocumentOnBase(UploadDocumentOnBaseRequest objUploadDocumentOnBaseRequest)
        {

            UploadDocumentOnBaseResponse objUploadDocumentOnBaseResponse = Claro.Web.Logging.ExecuteMethod<UploadDocumentOnBaseResponse>
                     (objUploadDocumentOnBaseRequest.Audit.Session,
                         objUploadDocumentOnBaseRequest.Audit.Transaction,
                         () =>
                         {
                             return Data.IFI.Common.GetUploadDocumentOnBase(objUploadDocumentOnBaseRequest);
                         });


            return objUploadDocumentOnBaseResponse;
        }


        /// <summary>Método que genera las constancias en formato PDF por Onbase.</summary>
        /// <param name="request"></param> 
        /// <returns>COMMON.GetGenerateConstancy.GenerateConstancyResponse</returns>
        /// <remarks>GetConstancyPDFWithOnbase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2019.</FecCrea></item></list>
        public static COMMON.GetGenerateConstancy.GenerateConstancyResponse GetConstancyPDFWithOnbase(COMMON.GetGenerateConstancy.GenerateConstancyRequest request)
        {
            string errorMessage = string.Empty;

            COMMON.GetGenerateConstancy.GenerateConstancyResponse objResponse =
                 Claro.Web.Logging.ExecuteMethod<GenerateConstancyResponse>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Data.IFI.Common.GetConstancyPDFWithOnbase(request.Audit.Session, request.Audit.Transaction,
                        request.ParametersGeneratePDFGeneric, ref errorMessage);
                });
            objResponse.ErrorMessage = errorMessage;

            return objResponse;
        }


        /// <summary>Método para obtener las colas asociadas a una interacción.</summary>
        /// <param name="objRequest"></param> 
        /// <returns>COMMON.GetQueuesCase.QueuesCaseResponse</returns>
        /// <remarks>GetQueuesCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>22/01/2019.</FecCrea></item></list>
        public static COMMON.GetQueuesCase.QueuesCaseResponse GetQueuesCase(COMMON.GetQueuesCase.QueuesCaseRequest objRequest)
        {
            string Message = "";
            string Cod_Error = "";
            COMMON.GetQueuesCase.QueuesCaseResponse objResponse = new COMMON.GetQueuesCase.QueuesCaseResponse()
            {
                ListQueues = Claro.Web.Logging.ExecuteMethod<List<QueuesCase>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Common.GetQueuesCase(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.SubClase, objRequest.Des_Title, objRequest.Flag_Buscar, objRequest.Usuario, ref Message, ref Cod_Error);
                })
            };
            objResponse.Desc_Error = Message;
            return objResponse;
        }


        /// <summary>Método que permite obtener la existencia de un caso registrado.</summary>
        /// <param name="objrequest"></param> 
        /// <returns>COMMON.GetCaseLa.GetCaseLaResponse</returns>
        /// <remarks>GetCaseLa</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>22/01/2019.</FecCrea></item></list>
        public static COMMON.GetCaseLa.GetCaseLaResponse GetCaseLa(COMMON.GetCaseLa.GetCaseLaRequest objrequest)
        {

           var objResponse = new COMMON.GetCaseLa.GetCaseLaResponse();
           
           try
           {
              
                var vFlagConsulta = string.Empty;
                var rMsgText = string.Empty;

                objResponse.Result = Web.Logging.ExecuteMethod(objrequest.strIdSession, objrequest.strTransaccion,
                    () =>
                    {
                        return Data.IFI.Common.GetCaseLa(objrequest.strIdSession, objrequest.strTransaccion, objrequest.vPhone, objrequest.vfechaini, objrequest.vfechafin, ref vFlagConsulta, ref rMsgText);
                    });

                objResponse.vFlagConsulta = vFlagConsulta;
                objResponse.rMsgText = rMsgText;
           }

           catch (Exception ex)
           {
               Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, ex.Message);
           }

           return objResponse;

        }

        /// <summary>Método que envía un correo electrónico.</summary>
        /// <param name="objRequest"></param>   
        /// <returns>EntitiesEmail.SendEmailWithBase64Response</returns>
        /// <remarks>SendEmailWithBase64</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>18/03/2019</FecCrea></item></list>
        public static EntitiesEmail.SendEmailWithBase64Response SendEmailWithBase64(EntitiesEmail.SendEmailWithBase64Request objRequest)
        {
            EntitiesEmail.SendEmailWithBase64Response objResponse = null;
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.SendEmailWithBase64(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }
            return objResponse;
        }

        /// <summary>Método que permite generar un certificado digital a un documento existente.</summary>
        /// <param name="objRequest"></param>   
        /// <returns>EntitiesDigitalSignature.DigitalSignatureResponse</returns>
        /// <remarks>FirmarDocumento</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/04/2019</FecCrea></item></list>
        public static EntitiesDigitalSignature.DigitalSignatureResponse FirmarDocumento(EntitiesDigitalSignature.DigitalSignatureRequest objRequest)
        {
            EntitiesDigitalSignature.DigitalSignatureResponse objResponse = null;
            try
            {
                objResponse = Web.Logging.ExecuteMethod<EntitiesDigitalSignature.DigitalSignatureResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Common.FirmarDocumento(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }
            return objResponse;
        }

        public static COMMON.GetConsultIGV.ConsultIGVResponse GetConsultIGV(COMMON.GetConsultIGV.ConsultIGVRequest objRequest)
        {
            var objResponse = new COMMON.GetConsultIGV.ConsultIGVResponse();
            try
            {
                objResponse.ListConsultIGV = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Common.GetConsultIGV(objRequest.SessionId, objRequest.TransactionId, objRequest.AppId, objRequest.AppName, objRequest.Username);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static COMMON.GetOffice.OfficeResponse GetOffice(COMMON.GetOffice.OfficeRequest objOfficeRequest)
        {
            var objOfficeResponse = new COMMON.GetOffice.OfficeResponse();

            objOfficeResponse = Web.Logging.ExecuteMethod(objOfficeRequest.Audit.Session, objOfficeRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Common.GetOffice(objOfficeRequest);
            });
            return objOfficeResponse;
        }

        public static COMMON.GetVerifyUser.VerifyUserResponse GetVerifyUser(COMMON.GetVerifyUser.VerifyUserRequest objRequest)
        {
            var objResponse = new COMMON.GetVerifyUser.VerifyUserResponse();
            try
            {
                objResponse.LstConsultSecurities = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Common.VerifyUser(objRequest.SessionId, objRequest.TransactionId, objRequest.AppId, objRequest.AppName, objRequest.Username, objRequest.AppCode);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static COMMON.GetPagOptionXuser.PagOptionXuserResponse GetPagOptionXuser(COMMON.GetPagOptionXuser.PagOptionXuserRequest objRequest)
        {
            COMMON.GetPagOptionXuser.PagOptionXuserResponse oRegistroAuditoriaResponse = new COMMON.GetPagOptionXuser.PagOptionXuserResponse();

            oRegistroAuditoriaResponse = Web.Logging.ExecuteMethod<COMMON.GetPagOptionXuser.PagOptionXuserResponse>(
                () =>
                {
                    return Claro.SIACU.Data.IFI.Common.GetPagOptionXuser(objRequest);

                });

            return oRegistroAuditoriaResponse;
        }

        //public static Entity.IFI.Fixed.PostPCRFPaquetesAdic.responseData GetPaquetesAdquiridosHistorio(Entity.IFI.Fixed.PostPCRFPaquetesAdic.PCRFPaquetesAdicRequest objRequest, string strMsisdn, string strSncode)
        //{
        //    Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, " Ingreso Business.Common GetPaquetesAdquiridosHistorio");

        //    var objResponseData = new Entity.IFI.Fixed.PostPCRFPaquetesAdic.responseData();
        //    //var objResponseData = new Entity.IFI.Fixed.PostPCRFPaquetesAdic.responseData
        //    //{
        //    //    listaFact = Claro.Web.Logging.ExecuteMethod
        //    //    (objRequest.Audit.Session,
        //    //        objRequest.Audit.Transaction,  () =>
        //    //        {
        //    //            return Data.IFI.Common.GetPaquetesAdquiridosHistorio(objRequest.Audit.Session,
        //    //                objRequest.Audit.Transaction, strMsisdn, strSncode);
        //    //        })
        //    //};
        //    try
        //    {
        //        objResponseData.listaFact = Claro.Web.Logging.ExecuteMethod
        //                        (objRequest.Audit.Session,
        //                            objRequest.Audit.Transaction, () =>
        //                            {
        //                                return Data.IFI.Common.GetPaquetesAdquiridosHistorio(objRequest.Audit.Session,
        //                                    objRequest.Audit.Transaction, strMsisdn, strSncode);
        //                            });
        //    }
        //    catch (Exception ex)
        //    {
        //        Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
        //    }

        //    return objResponseData;
        //}

        /// <summary>Método para obtener para el listado paquetes adquiridos </summary>
        /// <param name="objUbigeosTypeRequest"></param>      
        /// <returns>PostPCRFPaquetesAdic.responseData</returns>
        /// <remarks>GetPaquetesAdquiridosHistorio</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Hitss</CreadoPor></item>
        /// <item><FecCrea>01/03/2021</FecCrea></item></list>
        public static COMMON.GetPaquetesAdquiridosHistorico.PaquetesAdquiridosHistoricoResponse GetPaquetesAdquiridosHistorio(COMMON.GetPaquetesAdquiridosHistorico.PaquetesAdquiridosHistoricoRequest objRequest, string strMsisdn, string strSncode)
        {
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, " Ingreso Business.Common GetPaquetesAdquiridosHistorio");

            var objResponseData = new COMMON.GetPaquetesAdquiridosHistorico.PaquetesAdquiridosHistoricoResponse();
            try
            {
                objResponseData.listaFact = Claro.Web.Logging.ExecuteMethod
                                (objRequest.Audit.Session,
                                    objRequest.Audit.Transaction, () =>
                                    {
                                        return Data.IFI.Common.GetPaquetesAdquiridosHistorio(objRequest.Audit.Session,
                                            objRequest.Audit.Transaction, strMsisdn, strSncode);
                                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }  

            return objResponseData;
        }
    }
}
