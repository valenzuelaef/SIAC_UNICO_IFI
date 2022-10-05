using System;
using System.ServiceModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Claro.SIACU.Entity.IFI.Common.GetInsertInteractHFC;
using Claro.SIACU.Entity.IFI.Common.GetUser;
using COMMON = Claro.SIACU.Entity.IFI.Common;
using EntitiesCommon = Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.ProxyService.IFI.ServiciosPostpagoWS;
using FUNCTIONS = Claro.Utils;
using Claro.SIACU.Entity.IFI.Common.GetUpdateInter30;
using Claro.SIACU.Entity.IFI.Common.GetValidateMail;
using Claro.SIACU.Entity.IFI.Common.GetNumberSMS;
using Claro.SIACU.Entity.IFI.Common.GetLastInvoiceData;
using Claro.SIACU.Entity.IFI.Common.GetUploadDocumentOnBase;
using Claro.SIACU.Entity.IFI.Common.GetQueuesCase;
using Claro.SIACU.Entity.IFI.Common.GetCaseLa;
using Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi;

namespace Claro.SIACU.Web.Service.IFI
{
    public class CommonIFIService : ICommonIFIService
    {
        /// <summary>
        /// obtiene  interaccion 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>GetInsertBusinnesInteraction2</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetBusinessInteraction2.BusinessInteraction2Response GetInsertBusinnesInteraction2(COMMON.GetBusinessInteraction2.BusinessInteraction2Request request)
        {
            COMMON.GetBusinessInteraction2.BusinessInteraction2Response obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetBusinessInteraction2.BusinessInteraction2Response>
                    (() => { return Business.IFI.Common.GetBusinessInteraction2(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return obj;
        }

        /// <summary>
        /// inserta evidencia
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetInsertEvidence</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetInsertEvidence.InsertEvidenceResponse GetInsertEvidence(Entity.IFI.Common.GetInsertEvidence.InsertEvidenceRequest objRequest)
        {
            Entity.IFI.Common.GetInsertEvidence.InsertEvidenceResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertEvidence.InsertEvidenceResponse>
                    (() => { return Business.IFI.Common.GetInsertEvidence(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// guarda auditoria
        /// </summary>
        /// <param name="objGrabarAuditReq"></param>
        /// <returns></returns>
        /// <remarks>SaveAudit</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetSaveAudit.SaveAuditResponse SaveAudit(Entity.IFI.Common.GetSaveAudit.SaveAuditRequest objGrabarAuditReq)
        {
            Entity.IFI.Common.GetSaveAudit.SaveAuditResponse objGrabarAuditResp = null;

            try
            {
                objGrabarAuditResp = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.GetSaveAudit.SaveAuditResponse>(() => { return Business.IFI.Common.SaveAudit(objGrabarAuditReq); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGrabarAuditReq.Audit.Session, objGrabarAuditReq.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objGrabarAuditResp;
        }
        /// <summary>
        /// obtiene motivo sot
        /// </summary>
        /// <param name="objMotiveSotRequest"></param>
        /// <returns></returns>
        /// <remarks>GetMotiveSot</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetMotiveSot.MotiveSotResponse GetMotiveSot(Entity.IFI.Common.GetMotiveSot.MotiveSotRequest objMotiveSotRequest)
        {
            Entity.IFI.Common.GetMotiveSot.MotiveSotResponse objMotiveSotResponse;

            try
            {
                objMotiveSotResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.GetMotiveSot.MotiveSotResponse>(() => { return Business.IFI.Common.getMotiveSot(objMotiveSotRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMotiveSotRequest.Audit.Session, objMotiveSotRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objMotiveSotResponse;
        }
        /// <summary>
        /// valida calendario
        /// </summary>
        /// <param name="objScheduleRequest"></param>
        /// <returns></returns>
        /// <remarks>ValidateSchedule</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public bool ValidateSchedule(Claro.SIACU.Entity.IFI.Common.GetSchedule.ScheduleRequest objScheduleRequest)
        {

            try
            {
                return Claro.Web.Logging.ExecuteMethod(
                objScheduleRequest.Audit.Session,
                objScheduleRequest.Audit.Transaction,
                () =>
                {
                    return Business.IFI.Common.ValidateSchedule(objScheduleRequest);
                }
               );
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objScheduleRequest.Audit.Session, objScheduleRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }


        }

        /// <summary>
        /// genera constancia
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>GetGenerateContancyPDF</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetGenerateConstancy.GenerateConstancyResponse GetGenerateContancyPDF(COMMON.GetGenerateConstancy.GenerateConstancyRequest request)
        {
            COMMON.GetGenerateConstancy.GenerateConstancyResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetGenerateConstancy.GenerateConstancyResponse>(() =>
                {
                    return Business.IFI.Common.GetGenerateContancyPDF(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }




        /// <summary>
        /// obtiene reglas de negocio
        /// </summary>
        /// <param name="objBusinessRulesRequest"></param>
        /// <returns></returns>
        /// <remarks>GetBusinessRules</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetBusinessRules.BusinessRulesResponse GetBusinessRules(Entity.IFI.Common.GetBusinessRules.BusinessRulesRequest objBusinessRulesRequest)
        {
            Entity.IFI.Common.GetBusinessRules.BusinessRulesResponse objBusinessRulesResponse = new Entity.IFI.Common.GetBusinessRules.BusinessRulesResponse();

            try
            {
                objBusinessRulesResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.GetBusinessRules.BusinessRulesResponse>(() => { return Business.IFI.Common.GetBusinessRules(objBusinessRulesRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBusinessRulesRequest.Audit.Session, objBusinessRulesRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objBusinessRulesResponse;
        }


        /// <summary>
        /// obtiene lista de cacs
        /// </summary>
        /// <param name="objCacDacTypeRequest"></param>
        /// <returns></returns>
        /// <remarks>GetCacDacType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetCacDacType.CacDacTypeResponse GetCacDacType(Entity.IFI.Common.GetCacDacType.CacDacTypeRequest objCacDacTypeRequest)
        {
            Entity.IFI.Common.GetCacDacType.CacDacTypeResponse objCacDacTypeResponse;

            try
            {
                objCacDacTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.GetCacDacType.CacDacTypeResponse>(() => { return Business.IFI.Common.GetCacDacType(objCacDacTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCacDacTypeRequest.Audit.Session, objCacDacTypeRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objCacDacTypeResponse;
        }
        /// <summary>
        /// obtiene estados civiles
        /// </summary>
        /// <param name="objEstCivTypeRequest"></param>
        /// <returns></returns>
        /// <remarks>GetEstCivType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetEstCivType.EstCivTypeResponse GetEstCivType(Entity.IFI.Common.GetEstCivType.EstCivTypeRequest objEstCivTypeRequest)
        {
            Entity.IFI.Common.GetEstCivType.EstCivTypeResponse objEstCivTypeResponse;

            try
            {
                objEstCivTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.GetEstCivType.EstCivTypeResponse>(() => { return Business.IFI.Common.GetEstCivType(objEstCivTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objEstCivTypeRequest.Audit.Session, objEstCivTypeRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objEstCivTypeResponse;
        }
        /// <summary>
        /// obtiene tipo de vias
        /// </summary>
        /// <param name="objViasTypeRequest"></param>
        /// <returns></returns>
        /// <remarks>GetViasType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetViasType.ViasTypeResponse GetViasType(Entity.IFI.Common.GetViasType.ViasTypeRequest objViasTypeRequest)
        {
            Entity.IFI.Common.GetViasType.ViasTypeResponse objViasTypeResponse;

            try
            {
                objViasTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.GetViasType.ViasTypeResponse>(() => { return Business.IFI.Common.GetViasType(objViasTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objViasTypeRequest.Audit.Session, objViasTypeRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objViasTypeResponse;
        }
        /// <summary>
        /// obtiene tipos de manzanas
        /// </summary>
        /// <param name="objManzanasTypeRequest"></param>
        /// <returns></returns>
        /// <remarks>GetManzanasType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetManzanasType.ManzanasTypeResponse GetManzanasType(Entity.IFI.Common.GetManzanasType.ManzanasTypeRequest objManzanasTypeRequest)
        {
            Entity.IFI.Common.GetManzanasType.ManzanasTypeResponse objManzanasTypeResponse;

            try
            {
                objManzanasTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.GetManzanasType.ManzanasTypeResponse>(() => { return Business.IFI.Common.GetManzanasType(objManzanasTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objManzanasTypeRequest.Audit.Session, objManzanasTypeRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objManzanasTypeResponse;
        }
        /// <summary>
        /// obtiene tipo de interiores
        /// </summary>
        /// <param name="objInterioresTypeRequest"></param>
        /// <returns></returns>
        /// <remarks>GetInterioresType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetInterioresType.InterioresTypeResponse GetInterioresType(Entity.IFI.Common.GetInterioresType.InterioresTypeRequest objInterioresTypeRequest)
        {
            Entity.IFI.Common.GetInterioresType.InterioresTypeResponse objInterioresTypeResponse;

            try
            {
                objInterioresTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.GetInterioresType.InterioresTypeResponse>(() => { return Business.IFI.Common.GetInterioresType(objInterioresTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInterioresTypeRequest.Audit.Session, objInterioresTypeRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objInterioresTypeResponse;
        }
        /// <summary>
        /// obtiene tipos de Urbanizaciones
        /// </summary>
        /// <param name="objUrbsTypeRequest"></param>
        /// <returns></returns>
        /// <remarks>GetUrbsType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetUrbsType.UrbsTypeResponse GetUrbsType(Entity.IFI.Common.GetUrbsType.UrbsTypeRequest objUrbsTypeRequest)
        {
            Entity.IFI.Common.GetUrbsType.UrbsTypeResponse objUrbsTypeResponse;

            try
            {
                objUrbsTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.GetUrbsType.UrbsTypeResponse>(() => { return Business.IFI.Common.GetUrbsType(objUrbsTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objUrbsTypeRequest.Audit.Session, objUrbsTypeRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objUrbsTypeResponse;
        }
        /// <summary>
        /// obtiene tipo de zonas
        /// </summary>
        /// <param name="objZonesTypeRequest"></param>
        /// <returns></returns>
        /// <remarks>GetZonesType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetZonesType.ZonesTypeResponse GetZonesType(Entity.IFI.Common.GetZonesType.ZonesTypeRequest objZonesTypeRequest)
        {
            Entity.IFI.Common.GetZonesType.ZonesTypeResponse objZonesTypeResponse;

            try
            {
                objZonesTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.GetZonesType.ZonesTypeResponse>(() => { return Business.IFI.Common.GetZonesType(objZonesTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objZonesTypeRequest.Audit.Session, objZonesTypeRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objZonesTypeResponse;
        }
        /// <summary>
        /// obtiene ubigeo
        /// </summary>
        /// <param name="objUbigeosTypeRequest"></param>
        /// <param name="dep"></param>
        /// <param name="prov"></param>
        /// <returns></returns>
        /// <remarks>GetUbigeosType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetUbigeosType.UbigeosTypeResponse GetUbigeosType(Entity.IFI.Common.GetUbigeosType.UbigeosTypeRequest objUbigeosTypeRequest,int dep,int prov)
        {
            Entity.IFI.Common.GetUbigeosType.UbigeosTypeResponse objUbigeosTypeResponse;

            try
            {
                objUbigeosTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.GetUbigeosType.UbigeosTypeResponse>(() => { return Business.IFI.Common.GetUbigeosType(objUbigeosTypeRequest,dep,prov); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objUbigeosTypeRequest.Audit.Session, objUbigeosTypeRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objUbigeosTypeResponse;
        }
        /// <summary>
        /// obtiene tipo de Nac
        /// </summary>
        /// <param name="objNacTypeRequest"></param>
        /// <returns></returns>
        /// <remarks>GetNacType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetNacType.NacTypeResponse GetNacType(Entity.IFI.Common.GetNacType.NacTypeRequest objNacTypeRequest)
        {
            Entity.IFI.Common.GetNacType.NacTypeResponse objNacTypeResponse;

            try
            {
                objNacTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.GetNacType.NacTypeResponse>(() => { return Business.IFI.Common.GetNacType(objNacTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objNacTypeRequest.Audit.Session, objNacTypeRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objNacTypeResponse;
        }
        /// <summary>
        /// obtiene parametros data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>GetParameterData</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetParameterData.ParameterDataResponse GetParameterData(COMMON.GetParameterData.ParameterDataRequest request)
        {
            COMMON.GetParameterData.ParameterDataResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetParameterData.ParameterDataResponse>(() => { return Business.IFI.Common.GetParameterData(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objResponse;
        }
        /// <summary>
        /// obtiene tipificacion
        /// </summary>
        /// <param name="objTypificationRequest"></param>
        /// <returns></returns>
        /// <remarks>GetTypification</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetTypification.TypificationResponse GetTypification(Claro.SIACU.Entity.IFI.Common.GetTypification.TypificationRequest objTypificationRequest)
        {
            Claro.SIACU.Entity.IFI.Common.GetTypification.TypificationResponse objTypificationResponse = null;

            try
            {
                objTypificationResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.IFI.Common.GetTypification.TypificationResponse>(() => { return Business.IFI.Common.GetTypification(objTypificationRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypificationRequest.Audit.Session, objTypificationRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objTypificationResponse;
        }
        /// <summary>
        /// inserta log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>InsertLogTrx</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetInsertLogTrx.InsertLogTrxResponse InsertLogTrx(COMMON.GetInsertLogTrx.InsertLogTrxRequest request)
        {
            COMMON.GetInsertLogTrx.InsertLogTrxResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertLogTrx.InsertLogTrxResponse>(() => { return Business.IFI.Common.InsertLogTrx(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        #region interaaciones


        /// <summary>
        /// inserta plantilla interaccion
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>GetInsertInteractionTemplate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse GetInsertInteractionTemplate(COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionRequest request)
        {
            COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse>
                (() => { return Business.IFI.Common.GetInsertInteractionTemplate(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }


            return obj;


        }

        //insertar plantila de interaccion
        //
        /// <summary>
        /// insertar plantila de interaccion
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>GetInsInteractionTemplate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse GetInsInteractionTemplate(COMMON.GetInsTemplateInteraction.InsTemplateInteractionRequest request)
        {
            COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse>
                 (() => { return Business.IFI.Common.GetInsInteractionTemplate(request); });
            }

            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return obj;
        }

        //insertar interaccion
        //falta probar
        /// <summary>
        /// insertar interaccion
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>InsertInteract</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetInsertInteract.InsertInteractResponse InsertInteract(COMMON.GetInsertInteract.InsertInteractRequest request)
        {
            COMMON.GetInsertInteract.InsertInteractResponse obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertInteract.InsertInteractResponse>
                    (() => { return Business.IFI.Common.GetInsertInteract(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return obj;

        }

        //insertar interaccion
        //registra interaccion
        //la funcion original se llama insertar
        /// <summary>
        /// registra interaccion
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>InsertInt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetInsertInt.InsertIntResponse InsertInt(COMMON.GetInsertInt.InsertIntRequest request)
        {
            COMMON.GetInsertInt.InsertIntResponse obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertInt.InsertIntResponse>
                    (() => { return Business.IFI.Common.GetInsertInt(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return obj;

        }

        /// <summary>
        /// obtiene cliente
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>GetObClient</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetClient.ClientResponse GetObClient(COMMON.GetClient.ClientRequest request)
        {
            COMMON.GetClient.ClientResponse obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetClient.ClientResponse>(() =>
                { return Business.IFI.Common.GetObtClient(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return obj;
        }

        /// <summary>
        /// inserta interaccion general
        /// </summary>
        /// <param name="objrequest"></param>
        /// <returns></returns>
        /// <remarks>GetinsertInteractionGeneral</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetInsertGeneral.InsertGeneralResponse GetinsertInteractionGeneral(COMMON.GetInsertGeneral.InsertGeneralRequest objrequest)
        {
            COMMON.GetInsertGeneral.InsertGeneralResponse objresponse;

            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertGeneral.InsertGeneralResponse>(() =>

                { return Business.IFI.Common.GetIsertInteractionBusiness(objrequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objresponse;

        }

        //insertaplantilla interaccion general
        /// <summary>
        /// inserta plantilla interaccion general
        /// </summary>
        /// <param name="objrequest"></param>
        /// <returns></returns>
        /// <remarks>GetinsertInteractionTemplateGeneral</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralResponse GetinsertInteractionTemplateGeneral(COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralRequest objrequest)
        {
            COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralResponse objresponse;
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralResponse>(() =>
                {
                    return Business.IFI.Common.GetInserInteractionTemplateresponse(objrequest);
                });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objresponse;

        }




        #endregion








        /// <summary>
        /// guarda auditoria
        /// </summary>
        /// <param name="objRegAuditReq"></param>
        /// <returns></returns>
        /// <remarks>SaveAuditM</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Claro.SIACU.Entity.IFI.Common.GetSaveAuditM.SaveAuditMResponse SaveAuditM(Claro.SIACU.Entity.IFI.Common.GetSaveAuditM.SaveAuditMRequest objRegAuditReq)
        {
            Claro.SIACU.Entity.IFI.Common.GetSaveAuditM.SaveAuditMResponse objRegAuditResp = null;

            try
            {
                objRegAuditResp = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.IFI.Common.GetSaveAuditM.SaveAuditMResponse>(() => { return Business.IFI.Common.SaveAuditM(objRegAuditReq); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegAuditReq.Audit.Session, objRegAuditReq.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objRegAuditResp;
        }
        /// <summary>
        /// genera pdf
        /// </summary>
        /// <param name="objGeneratePDFRequest"></param>
        /// <returns></returns>
        /// <remarks>GeneratePDF</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GeneratePDF.GeneratePDFDataResponse GeneratePDF(COMMON.GeneratePDF.GeneratePDFDataRequest objGeneratePDFRequest)
        {
            COMMON.GeneratePDF.GeneratePDFDataResponse objGeneratePDFResponse = null;
            try
            {
                objGeneratePDFResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GeneratePDF.GeneratePDFDataResponse>(objGeneratePDFRequest.Audit.Session, objGeneratePDFRequest.Audit.Transaction, () =>
                {
                    return Business.IFI.Common.GeneratePDF(objGeneratePDFRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGeneratePDFRequest.Audit.Session, objGeneratePDFRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));

            }
            return objGeneratePDFResponse;
        }
        /// <summary>
        /// insertar interaccion hfc
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetInsertInteractHFC</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public InsertInteractHFCResponse GetInsertInteractHFC(InsertInteractHFCRequest objRequest)
        {
            COMMON.GetInsertInteractHFC.InsertInteractHFCResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.IFI.Common.GetInsertInteractHFC.InsertInteractHFCResponse>(
                    () =>
                    {
                        return Business.IFI.Common.GetInsertInteractHFC(objRequest);
                    });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// inserta interaccion
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetInsertInteract</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetInsertInteract.InsertInteractResponse GetInsertInteract(COMMON.GetInsertInteract.InsertInteractRequest objRequest)
        {
            COMMON.GetInsertInteract.InsertInteractResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.IFI.Common.GetInsertInteract.InsertInteractResponse>(
                    () =>
                    {
                        return Business.IFI.Common.GetInsertInteract(objRequest);
                    });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        #region funciones para eviar email
        //enviaremail
        /// <summary>
        /// envia email
        /// </summary>
        /// <param name="objrequest"></param>
        /// <returns></returns>
        /// <remarks>GetSendEmail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetSendEmail.SendEmailResponse GetSendEmail(COMMON.GetSendEmail.SendEmailRequest objrequest)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = new COMMON.GetSendEmail.SendEmailResponse();
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetSendEmail.SendEmailResponse>(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
                {
                    return Business.IFI.Common.GetSendEmail(objrequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));

                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objresponse;
        }


        //EnviarEmailAlt
        /// <summary>
        /// envia email con adjuntos
        /// </summary>
        /// <param name="objrequest"></param>
        /// <returns></returns>
        /// <remarks>GetSendEmailAlt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetSendEmail.SendEmailResponse GetSendEmailAlt(COMMON.GetSendEmail.SendEmailRequest objrequest)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = null;
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetSendEmail.SendEmailResponse>(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
                {
                    return Business.IFI.Common.GetSendEmailAlt(objrequest);

                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));

                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));

            }
            return objresponse;
        }

        //getfiledefaultimpersonation
        /// <summary>
        /// obtine bytes de archivo
        /// </summary>
        /// <param name="objrequest"></param>
        /// <returns></returns>
        /// <remarks>GetfileDefaultImpersonation</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse GetfileDefaultImpersonation(COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationRequest objrequest)
        {
            COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse objreponse = null;
            try
            {
                objreponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse>(() =>
                {
                    return Business.IFI.Common.GetfileDefaultImpersonation(objrequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw;
            }
            return objreponse;
        }


        #endregion
        /// <summary>
        /// obtiene informacion interaccion plantilla
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetInfoInteractionTemplate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetDatTempInteraction.DatTempInteractionResponse GetInfoInteractionTemplate(COMMON.GetDatTempInteraction.DatTempInteractionRequest objRequest)
        {
            var objResponse = new COMMON.GetDatTempInteraction.DatTempInteractionResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.IFI.Common.GetDatTempInteraction.DatTempInteractionResponse>(
                    () =>
                    {
                        return Business.IFI.Common.GetInfoInteractionTemplate(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// obtiene numero GWP
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetNumberGWP</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetNumberGWP.NumberGWPResponse GetNumberGWP(COMMON.GetNumberGWP.NumberGWPRequest objRequest)
        {
            COMMON.GetNumberGWP.NumberGWPResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetNumberGWP.NumberGWPResponse>(() =>
                {
                    return Business.IFI.Common.GetNumberGWP(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// Obtiene numero EAI
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetNumberEAI</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetNumberEAI.NumberEAIResponse GetNumberEAI(COMMON.GetNumberEAI.NumberEAIRequest objRequest)
        {
            COMMON.GetNumberEAI.NumberEAIResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetNumberEAI.NumberEAIResponse>(() =>
                {
                    return Business.IFI.Common.GetNumberEAI(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        //MG13
        /// <summary>
        /// obtiene empleado por usuario
        /// </summary>
        /// <param name="objEmployeeRequest"></param>
        /// <returns></returns>
        /// <remarks>GetEmployeByUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetEmployeByUser.EmployeeResponse GetEmployeByUser(COMMON.GetEmployeByUser.EmployeeRequest objEmployeeRequest)
        {


            COMMON.GetEmployeByUser.EmployeeResponse objEmployeeResponse = new COMMON.GetEmployeByUser.EmployeeResponse();
            try
            {
                objEmployeeResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetEmployeByUser.EmployeeResponse>(() => { return Business.IFI.Common.GetEmployeByUser(objEmployeeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objEmployeeRequest.Audit.Session, objEmployeeRequest.Audit.Transaction, ex.Message);
            }
            return objEmployeeResponse;


        }
       
        /// <summary>
        /// obtiene opciones de usuario
        /// </summary>
        /// <param name="objReadOptionsByUserRequest"></param>
        /// <returns></returns>
        /// <remarks>ReadOptionsByUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.ReadOptionsByUser.ReadOptionsByUserResponse ReadOptionsByUser(COMMON.ReadOptionsByUser.ReadOptionsByUserRequest objReadOptionsByUserRequest)
        {


            COMMON.ReadOptionsByUser.ReadOptionsByUserResponse objReadOptionsByUserResponse = new COMMON.ReadOptionsByUser.ReadOptionsByUserResponse();
            try
            {
                objReadOptionsByUserResponse = Claro.Web.Logging.ExecuteMethod<COMMON.ReadOptionsByUser.ReadOptionsByUserResponse>(() => { return Business.IFI.Common.ReadOptionsByUser(objReadOptionsByUserRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objReadOptionsByUserRequest.Audit.Session, objReadOptionsByUserRequest.Audit.Transaction, ex.Message);
            }
            return objReadOptionsByUserResponse;


        }
        //MG13
        /// <summary>
        /// valida usuario
        /// </summary>
        /// <param name="objCheckingUserRequest"></param>
        /// <returns></returns>
        /// <remarks>CheckingUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.CheckingUser.CheckingUserResponse CheckingUser(COMMON.CheckingUser.CheckingUserRequest objCheckingUserRequest)
        {


            COMMON.CheckingUser.CheckingUserResponse objCheckingUserResponse = new COMMON.CheckingUser.CheckingUserResponse();
            try
            {
                objCheckingUserResponse = Claro.Web.Logging.ExecuteMethod<COMMON.CheckingUser.CheckingUserResponse>(() => { return Business.IFI.Common.CheckingUser(objCheckingUserRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCheckingUserRequest.Audit.Session, objCheckingUserRequest.Audit.Transaction, ex.Message);
            }
            return objCheckingUserResponse;


        }
        /// <summary>
        /// evalua cantidad o monto
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetEvaluateAmount</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetEvaluateAmount.EvaluateAmountResponse GetEvaluateAmount(COMMON.GetEvaluateAmount.EvaluateAmountRequest objRequest)
        {
            var objResponse = new COMMON.GetEvaluateAmount.EvaluateAmountResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.IFI.Common.GetEvaluateAmount(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// evalua monto DCM
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetEvaluateAmount_DCM</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetEvaluateAmount.EvaluateAmountResponse GetEvaluateAmount_DCM(COMMON.GetEvaluateAmount.EvaluateAmountRequest objRequest)
        {
            var objResponse = new COMMON.GetEvaluateAmount.EvaluateAmountResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.IFI.Common.GetEvaluateAmount_DCM(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        #region Redirect
        /// <summary>
        /// valida redireccion 
        /// </summary>
        /// <param name="objValidateCommunicationRequest"></param>
        /// <returns></returns>
        /// <remarks>ValidateRedirectCommunication</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetValidateCommunication.ValidateCommunicationResponse ValidateRedirectCommunication(Entity.IFI.Common.GetValidateCommunication.ValidateCommunicationRequest objValidateCommunicationRequest)
        {
            Entity.IFI.Common.GetValidateCommunication.ValidateCommunicationResponse onjValidateCommunicationResponse = null;
            try
            {
                onjValidateCommunicationResponse = Business.IFI.Common.ValidateRedirectCommunication(objValidateCommunicationRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objValidateCommunicationRequest.Audit.Session, objValidateCommunicationRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return onjValidateCommunicationResponse;
        }
        #endregion


        #region External/ Internal Transfer





        /// <summary>
        /// obtiene tipos de trabajo
        /// </summary>
        /// <param name="objWorkTypeRequest"></param>
        /// <returns></returns>
        /// <remarks>GetWorkType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetWorkType.WorkTypeResponse GetWorkType(EntitiesCommon.GetWorkType.WorkTypeRequest objWorkTypeRequest)
        {
            EntitiesCommon.GetWorkType.WorkTypeResponse objWorkTypeResponse;

            try
            {
                objWorkTypeResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetWorkType.WorkTypeResponse>(() => { return Business.IFI.Common.GetWorkType(objWorkTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objWorkTypeRequest.Audit.Session, objWorkTypeRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objWorkTypeResponse;
        }





        #endregion






        /// <summary>
        /// obtiene usuarios
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetUser.UserResponse GetUser(COMMON.GetUser.UserRequest objRequest)
        {
            var objResponse = new COMMON.GetUser.UserResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.IFI.Common.GetUser(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objResponse;
        }


        /// <summary>
        /// obtiene fecha de empleados
        /// </summary>
        /// <param name="objDatosEmpleadoRequest"></param>
        /// <returns></returns>
        /// <remarks>GetEmployerDate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetEmployerDate.GetEmployerDateResponse GetEmployerDate(COMMON.GetEmployerDate.GetEmployerDateRequest objDatosEmpleadoRequest)
        {

            COMMON.GetEmployerDate.GetEmployerDateResponse objDatosEmpleadoResponse = new COMMON.GetEmployerDate.GetEmployerDateResponse();
            try
            {
                objDatosEmpleadoResponse = Claro.Web.Logging.ExecuteMethod(() => Business.IFI.Common.GetEmployerDate(objDatosEmpleadoRequest));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDatosEmpleadoRequest.Audit.Session, objDatosEmpleadoRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }


            return objDatosEmpleadoResponse;
        }





        /// <summary>
        /// obtiene parametros terminal TPI
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetParameterTerminalTPI</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse GetParameterTerminalTPI(COMMON.GetParameterTerminalTPI.ParameterTerminalTPIRequest objRequest)
        {
            COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse>
                    (() => { return Business.IFI.Common.GetParameterTerminalTPI(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        /// <summary>
        /// envia email con mas de un archivo
        /// </summary>
        /// <param name="objrequest"></param>
        /// <returns></returns>
        /// <remarks>GetSendEmailFixed</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesCommon.GetSendEmail.SendEmailResponse GetSendEmailFixed(EntitiesCommon.GetSendEmail.SendEmailRequest objrequest)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = new COMMON.GetSendEmail.SendEmailResponse();
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetSendEmail.SendEmailResponse>(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
                {
                    return Business.IFI.Common.GetSendEmailFixed(objrequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));

                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objresponse;
        }
        /// <summary>
        /// envia email con adjunto
        /// </summary>
        /// <param name="objrequest"></param>
        /// <returns></returns>
        /// <remarks>GetSendEmailAltFixed</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesCommon.GetSendEmail.SendEmailResponse GetSendEmailAltFixed(EntitiesCommon.GetSendEmail.SendEmailRequest objrequest)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = new COMMON.GetSendEmail.SendEmailResponse();
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetSendEmail.SendEmailResponse>(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
                {
                    return Business.IFI.Common.GetSendEmailAltFixed(objrequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));

                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objresponse;
        }
        /// <summary>
        /// obtiene tipo de estado
        /// </summary>
        /// <param name="objStateTypeRequest"></param>
        /// <returns></returns>
        /// <remarks>GetStateType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesCommon.GetStateType.StateTypeResponse GetStateType(EntitiesCommon.GetStateType.StateTypeRequest objStateTypeRequest)
        {
            EntitiesCommon.GetStateType.StateTypeResponse objStateTypeResponse;

            try
            {
                objStateTypeResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetStateType.StateTypeResponse>(() => { return Business.IFI.Common.GetStateType(objStateTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objStateTypeRequest.Audit.Session, objStateTypeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objStateTypeResponse;
        }
        /// <summary>
        /// obtiene tipos de transacciones
        /// </summary>
        /// <param name="objTransactionTypeRequest"></param>
        /// <returns></returns>
        /// <remarks>GetTransactionType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesCommon.GetTransactionType.TransactionTypeResponse GetTransactionType(EntitiesCommon.GetTransactionType.TransactionTypeRequest objTransactionTypeRequest)
        {
            EntitiesCommon.GetTransactionType.TransactionTypeResponse objTransactionTypeResponse;

            try
            {
                objTransactionTypeResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetTransactionType.TransactionTypeResponse>(() => { return Business.IFI.Common.GetTransactionType(objTransactionTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTransactionTypeRequest.Audit.Session, objTransactionTypeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objTransactionTypeResponse;
        }
        /// <summary>
        /// actualiza campo inter30
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetUpdatexInter30</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public UpdatexInter30Response GetUpdatexInter30(UpdatexInter30Request objRequest)
        {
            UpdatexInter30Response objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<UpdatexInter30Response>(() => { return Business.IFI.Common.GetUpdatexInter30(objRequest); });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objResponse;
        }
        /// <summary>
        /// obtiene validacion email
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetValidateMail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public ValidateMailResponse GetValidateMail(ValidateMailRequest objRequest)
        {
            ValidateMailResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<ValidateMailResponse>(() => { return Business.IFI.Common.GetValidateMail(objRequest); });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objResponse;
        }
        /// <summary>
        /// obtiene numero sms
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetNumberSMS</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Claro.SIACU.Entity.IFI.Common.GetNumberSMS.NumberSMSResponse GetNumberSMS(Claro.SIACU.Entity.IFI.Common.GetNumberSMS.NumberSMSRequest objRequest)
        {


            NumberSMSResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<NumberSMSResponse>(() => { return Business.IFI.Common.GetNumberSMS(objRequest); });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objResponse;

        }
        /// <summary>
        /// obtiene ultimo recibo 
        /// </summary>
        /// <param name="objLastInvoiceDataRequest"></param>
        /// <param name="strCustomerCode"></param>
        /// <returns></returns>
        /// <remarks>GetLastInvoiceData</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Common.GetLastInvoiceData.LastInvoiceDataResponse GetLastInvoiceData(Entity.IFI.Common.GetLastInvoiceData.LastInvoiceDataRequest objLastInvoiceDataRequest, string strCustomerCode)
        {
            Entity.IFI.Common.GetLastInvoiceData.LastInvoiceDataResponse objLastInvoiceDataResponse;

            try
            {
                objLastInvoiceDataResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.GetLastInvoiceData.LastInvoiceDataResponse>(() => { return Business.IFI.Common.GetLastInvoiceData(objLastInvoiceDataRequest, strCustomerCode); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objLastInvoiceDataRequest.Audit.Session, objLastInvoiceDataRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objLastInvoiceDataResponse;
        }
        /// <summary>
        /// valida credenciales datapower
        /// </summary>
        /// <param name="objIsOkGetKeyRequest"></param>
        /// <returns></returns>
        /// <remarks>IsOkGetKey</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.IsOkGetKey.IsOkGetKeyResponse IsOkGetKey(COMMON.IsOkGetKey.IsOkGetKeyRequest objIsOkGetKeyRequest)
        {



            Entity.IFI.Common.IsOkGetKey.IsOkGetKeyResponse objIsOkGetKeyResponse;

            try
            {
                objIsOkGetKeyResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.IsOkGetKey.IsOkGetKeyResponse>(() => { return Business.IFI.Common.IsOkGetKey(objIsOkGetKeyRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objIsOkGetKeyRequest.Audit.Session, objIsOkGetKeyRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objIsOkGetKeyResponse;


        }
        /// <summary>
        /// genera constancia en onbase
        /// </summary>
        /// <param name="objUploadDocumentOnBaseRequest"></param>
        /// <returns></returns>
        /// <remarks>GetUploadDocumentOnBase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public UploadDocumentOnBaseResponse GetUploadDocumentOnBase(UploadDocumentOnBaseRequest objUploadDocumentOnBaseRequest)
        {

            UploadDocumentOnBaseResponse objUploadDocumentOnBaseResponse;

            try
            {
                objUploadDocumentOnBaseResponse = Claro.Web.Logging.ExecuteMethod<UploadDocumentOnBaseResponse>(() => { return Business.IFI.Common.GetUploadDocumentOnBase(objUploadDocumentOnBaseRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objUploadDocumentOnBaseRequest.Audit.Session, objUploadDocumentOnBaseRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objUploadDocumentOnBaseResponse;
        }

        /// <summary>
        /// obtiene constancia onbase
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>GetConstancyPDFWithOnbase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetGenerateConstancy.GenerateConstancyResponse GetConstancyPDFWithOnbase(COMMON.GetGenerateConstancy.GenerateConstancyRequest request)
        {
            COMMON.GetGenerateConstancy.GenerateConstancyResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetGenerateConstancy.GenerateConstancyResponse>(() =>
                {
                    return Business.IFI.Common.GetConstancyPDFWithOnbase(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// obtiene secuencia 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>GetSequenceCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetSequenceCode.SequenceCodeResponse GetSequenceCode(COMMON.GetSequenceCode.SequenceCodeRequest request)
        {
            COMMON.GetSequenceCode.SequenceCodeResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetSequenceCode.SequenceCodeResponse>(() =>
                {
                    return Business.IFI.Postpaid.ServiceLock.GetSequenceCode(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        /// <summary>
        /// obtiene cola de caso
        /// </summary>
        /// <param name="objQueuesCaseRequest"></param>
        /// <returns></returns>
        /// <remarks>GetQueuesCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetQueuesCase.QueuesCaseResponse GetQueuesCase(Claro.SIACU.Entity.IFI.Common.GetQueuesCase.QueuesCaseRequest objQueuesCaseRequest)
        {
            COMMON.GetQueuesCase.QueuesCaseResponse objQueuesCaseResponse = null;

            try
            {
                objQueuesCaseResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.IFI.Common.GetQueuesCase.QueuesCaseResponse>(() => { return Business.IFI.Common.GetQueuesCase(objQueuesCaseRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objQueuesCaseRequest.Audit.Session, objQueuesCaseRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objQueuesCaseResponse;
        }
        /// <summary>
        /// obtiene caso La
        /// </summary>
        /// <param name="objCaseLaRequest"></param>
        /// <returns></returns>
        /// <remarks>GetCaseLa</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public COMMON.GetCaseLa.GetCaseLaResponse GetCaseLa(Claro.SIACU.Entity.IFI.Common.GetCaseLa.GetCaseLaRequest objCaseLaRequest)
        {
            COMMON.GetCaseLa.GetCaseLaResponse objCaseLaResponse = null;

            try
            {
                objCaseLaResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.IFI.Common.GetCaseLa.GetCaseLaResponse>(() => { return Business.IFI.Common.GetCaseLa(objCaseLaRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCaseLaRequest.Audit.Session, objCaseLaRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objCaseLaResponse;
        }

        /// <summary>Método que envía un correo electrónico.</summary>
        /// <param name="objSendEmailWithBase64Request"></param>   
        /// <returns>EntitiesCommon.GetSendEmailWithBase64.SendEmailWithBase64Response</returns>
        /// <remarks>SendEmailWithBase64</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>18/03/2019</FecCrea></item></list>
        public EntitiesCommon.GetSendEmailWithBase64.SendEmailWithBase64Response SendEmailWithBase64(EntitiesCommon.GetSendEmailWithBase64.SendEmailWithBase64Request objSendEmailWithBase64Request)
        {
            EntitiesCommon.GetSendEmailWithBase64.SendEmailWithBase64Response objSendEmailWithBase64Response = null;
            try
            {
                objSendEmailWithBase64Response = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetSendEmailWithBase64.SendEmailWithBase64Response>(() =>
                {
                    return Business.IFI.Common.SendEmailWithBase64(objSendEmailWithBase64Request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSendEmailWithBase64Request.Audit.Session, objSendEmailWithBase64Request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objSendEmailWithBase64Response;
        }

        /// <summary>Método que permite generar un certificado digital a un documento existente.</summary>
        /// <param name="objDigitalSignatureRequest"></param>   
        /// <returns>EntitiesCommon.GetDigitalSignature.DigitalSignatureResponse</returns>
        /// <remarks>FirmarDocumento</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/04/2019</FecCrea></item></list>
        public EntitiesCommon.GetDigitalSignature.DigitalSignatureResponse FirmarDocumento(EntitiesCommon.GetDigitalSignature.DigitalSignatureRequest objDigitalSignatureRequest)
        {
            EntitiesCommon.GetDigitalSignature.DigitalSignatureResponse objDigitalSignatureResponse = null;
            try
            {
                objDigitalSignatureResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetDigitalSignature.DigitalSignatureResponse>(() =>
                {
                    return Business.IFI.Common.FirmarDocumento(objDigitalSignatureRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDigitalSignatureRequest.Audit.Session, objDigitalSignatureRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objDigitalSignatureResponse;
        }

        public COMMON.GetConsultIGV.ConsultIGVResponse GetConsultIGV(COMMON.GetConsultIGV.ConsultIGVRequest objRequest)
        {
            var objResponse = new COMMON.GetConsultIGV.ConsultIGVResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.IFI.Common.GetConsultIGV(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetOffice.OfficeResponse GetOffice(COMMON.GetOffice.OfficeRequest objOfficeRequest)
        {
            var objOfficeResponse = new COMMON.GetOffice.OfficeResponse();

            try
            {
                objOfficeResponse = Claro.Web.Logging.ExecuteMethod(() => Business.IFI.Common.GetOffice(objOfficeRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objOfficeRequest.Audit.Session, objOfficeRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objOfficeResponse;
        }

        public COMMON.GetVerifyUser.VerifyUserResponse GetVerifyUser(COMMON.GetVerifyUser.VerifyUserRequest objRequest)
        {
            var objResponse = new COMMON.GetVerifyUser.VerifyUserResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.IFI.Common.GetVerifyUser(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetPagOptionXuser.PagOptionXuserResponse GetPagOptionXuser(COMMON.GetPagOptionXuser.PagOptionXuserRequest objRequest)
        {
            var objResponse = new COMMON.GetPagOptionXuser.PagOptionXuserResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.IFI.Common.GetPagOptionXuser(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objResponse;
        }        

        //public Entity.IFI.Fixed.PostPCRFPaquetesAdic.responseData GetPaquetesAdquiridosHistorico(Entity.IFI.Fixed.PostPCRFPaquetesAdic.PCRFPaquetesAdicRequest objRequest, string strMsisdn, string strSncode)
        //{
        //    //Entity.IFI.Fixed.PostPCRFPaquetesAdic.responseData objResponse = new Entity.IFI.Fixed.PostPCRFPaquetesAdic.responseData();
        //    var objResponse = new Entity.IFI.Fixed.PostPCRFPaquetesAdic.responseData();  
            
        //    try
        //    {
        //        Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, " Ingreso CommonIFIService GetPaquetesAdquiridosHistorio");
        //        Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, " Ingreso CommonIFIService strMsisdn: " + strMsisdn + " strSncode:" + strSncode);
        //        objResponse = Claro.Web.Logging.ExecuteMethod(() =>  Business.IFI.Common.GetPaquetesAdquiridosHistorio(objRequest, strMsisdn, strSncode));
        //        Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, " Salida CommonIFIService GetPaquetesAdquiridosHistorio");
        //    }
        //    catch (Exception ex)
        //    {
        //        Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
        //        throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
        //    }
        //    return objResponse;
        //}

        public COMMON.GetPaquetesAdquiridosHistorico.PaquetesAdquiridosHistoricoResponse GetPaquetesAdquiridosHistorico(COMMON.GetPaquetesAdquiridosHistorico.PaquetesAdquiridosHistoricoRequest objRequest, string strMsisdn, string strSncode)
        {
            var objResponse = new COMMON.GetPaquetesAdquiridosHistorico.PaquetesAdquiridosHistoricoResponse();

            try
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, " Ingreso CommonIFIService GetPaquetesAdquiridosHistorio");
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, " Ingreso CommonIFIService strMsisdn: " + strMsisdn + " strSncode:" + strSncode);
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.IFI.Common.GetPaquetesAdquiridosHistorio(objRequest, strMsisdn, strSncode));
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, " Salida CommonIFIService GetPaquetesAdquiridosHistorio");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strParamGrupo"></param>
        /// <returns></returns>
        public List<ObtenerTipoTecnologia> obtenerTipoTecnologia(string strIdSession, string strTransaction, string strParamGrupo)
        {
            List<ObtenerTipoTecnologia> olistObtenerTipoTecnologia = null;
            try
            {
                olistObtenerTipoTecnologia = Claro.Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    return Business.IFI.Postpaid.ChangeServiceAddress.obtenerTipoTecnologia(strIdSession, strTransaction, strParamGrupo);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message.ToString());
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Source.Trim());
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.InnerException.Message);
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.InnerException.StackTrace);

                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return olistObtenerTipoTecnologia;
        }
    }
}
