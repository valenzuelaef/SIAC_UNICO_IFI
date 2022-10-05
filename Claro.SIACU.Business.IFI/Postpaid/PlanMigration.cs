using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OPlanMigration = Claro.SIACU.Entity.IFI.Postpaid;

namespace Claro.SIACU.Business.IFI.Postpaid
{
    public class PlanMigration
    {

        /// <summary>
        /// Obtiene la lista de planes
        /// </summary>
        /// <param name="objPlansRequest"></param>
        /// <returns>retorna una lista de planes</returns>
        public static OPlanMigration.GetPlans.PlansResponse GetPlans(OPlanMigration.GetPlans.PlansRequest objPlansRequest)
        {
            List<OPlanMigration.ProductPlan> listPlanResponse = null;
            List<OPlanMigration.ProductPlan> listPlan = null;
            List<OPlanMigration.ProductPlan> listPlanOrdered;
            try
            {
                listPlan = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.ProductPlan>>(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Postpaid.PlanMigration.GetPlans(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, objPlansRequest.strOffice, objPlansRequest.strOfficeDefault, objPlansRequest.strOffer, objPlansRequest.strProductType, objPlansRequest.strFlagEjecution);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
            }

            if (listPlan != null)
            {
                listPlanResponse = new List<OPlanMigration.ProductPlan>();
                listPlanOrdered = listPlan.OrderBy(a => Convert.ToDate(a.strStartDate)).ToList();
                listPlanResponse = listPlanOrdered;
            }
            OPlanMigration.GetPlans.PlansResponse objPlansResponse = new OPlanMigration.GetPlans.PlansResponse()
            {
                lstPlan = listPlanResponse
            };
            return objPlansResponse;
        }

        /// <summary>
        /// Obtiene la lista de servicios según plan
        /// </summary>
        /// <param name="objServicesRequest"></param>
        /// <returns>retorna una lista de servicios</returns>
        public static OPlanMigration.GetServicesByPlan.PlanServiceResponse GetServicesByPlan(OPlanMigration.GetServicesByPlan.PlanServiceRequest objServicesRequest)
        {
            List<OPlanMigration.ServiceByPlan> listServiceResponse = null;
            List<OPlanMigration.ServiceByPlan> listService = null;
            List<OPlanMigration.ServiceByPlan> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.ServiceByPlan>>(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Postpaid.PlanMigration.GetServicesByPlan(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, objServicesRequest.strIdPlan, objServicesRequest.strProductType);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
            }

            if (listService != null)
            {
                listServiceResponse = new List<OPlanMigration.ServiceByPlan>();
                listServiceOrdered = listService.OrderBy(a => a.ExternalCode).ToList();
                listServiceResponse = listServiceOrdered;
            }
            OPlanMigration.GetServicesByPlan.PlanServiceResponse objServiceResponse = new OPlanMigration.GetServicesByPlan.PlanServiceResponse()
            {
                lstService = listServiceResponse
            };

            return objServiceResponse;
        }

        /// <summary>
        /// Obtiene la lista de servicios del plan actual
        /// </summary>
        /// <param name="objServicesRequest"></param>
        /// <returns>retorna una lista de servicios</returns>
        public static OPlanMigration.GetServicesByCurrentPlan.ServicesByCurrentPlanResponse GetServicesByCurrentPlan(OPlanMigration.GetServicesByCurrentPlan.ServicesByCurrentPlanRequest objServicesRequest)
        {
            List<OPlanMigration.ServiceByCurrentPlan> listServiceResponse = null;
            List<OPlanMigration.ServiceByCurrentPlan> listService = null;
            List<OPlanMigration.ServiceByCurrentPlan> listServiceOrdered;
            string strTmCode  = "";
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.ServiceByCurrentPlan>>(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Postpaid.PlanMigration.GetServicesByCurrentPlan(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, objServicesRequest.ContractId, ref strTmCode);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
            }

            if (listService != null)
            {
                listServiceResponse = new List<OPlanMigration.ServiceByCurrentPlan>();
                listServiceOrdered = listService.OrderBy(a => a.ServiceDes).ToList();
                listServiceResponse = listServiceOrdered;
            }
            OPlanMigration.GetServicesByCurrentPlan.ServicesByCurrentPlanResponse objServiceResponse = new OPlanMigration.GetServicesByCurrentPlan.ServicesByCurrentPlanResponse()
            {
                lstServicesByCurrentPlan = listServiceResponse
            };

            objServiceResponse.strTmCode = strTmCode;
            return objServiceResponse;
        }

        /// <summary>
        /// Obtiene la lista de datos de los equipos del plan actual
        /// </summary>
        /// <param name="objServicesRequest"></param>
        /// <returns>retorna una lista de datos de equipos</returns>
        public static OPlanMigration.GetEquipmentByCurrentPlan.EquipmentByCurrentPlanResponse GetEquipmentByCurrentPlan(OPlanMigration.GetEquipmentByCurrentPlan.EquipmentByCurrentPlanRequest objEquipmentRequest)
        {
            List<OPlanMigration.EquipmentByCurrentPlan> listEquipmentResponse = null;
            List<OPlanMigration.EquipmentByCurrentPlan> listEquipment = null;
            try
            {
                listEquipment = Claro.Web.Logging.ExecuteMethod<List<OPlanMigration.EquipmentByCurrentPlan>>(objEquipmentRequest.Audit.Session, objEquipmentRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Postpaid.PlanMigration.GetEquipmentByCurrentPlan(objEquipmentRequest.Audit.Session, objEquipmentRequest.Audit.Transaction, objEquipmentRequest.ContractId);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objEquipmentRequest.Audit.Session, objEquipmentRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
            }

            if (listEquipment != null)
            {
                listEquipmentResponse = new List<OPlanMigration.EquipmentByCurrentPlan>();
                listEquipmentResponse = listEquipment;
            }
            OPlanMigration.GetEquipmentByCurrentPlan.EquipmentByCurrentPlanResponse objEquipmentResponse = new OPlanMigration.GetEquipmentByCurrentPlan.EquipmentByCurrentPlanResponse()
            {
                lstEquipmentByCurrentPlan = listEquipmentResponse
            };

            return objEquipmentResponse;
        }

        /// <summary>
        /// Envía los datos a ejecutar la transacción Cambio de Plan
        /// </summary>
        /// <param name="objPlanMigrationRequest"></param>
        /// <returns>retorna la respuesta al proceso</returns>
        public static OPlanMigration.PostPlanMigration.PlanMigrationResponse PostPlanMigration(OPlanMigration.PostPlanMigration.PlanMigrationRequest objPlanMigrationRequest)
        {
            List<OPlanMigration.PostPlanMigration.RestPlanMigrationServiceList> lstService = new List<OPlanMigration.PostPlanMigration.RestPlanMigrationServiceList>();
            OPlanMigration.PostPlanMigration.RestPlanMigrationServiceList oServiceList = null;

            foreach (OPlanMigration.ServiceByPlan item in objPlanMigrationRequest.MessageRequest.Body.ServiceList)
            {
                oServiceList = new OPlanMigration.PostPlanMigration.RestPlanMigrationServiceList();
                oServiceList.nombreServicio = item.SisactServDes;
                oServiceList.tipoServicio = item.ServiceTypeDes;
                oServiceList.grupoServicio = item.ServiceGroupCode;
                oServiceList.cf = item.CF;
                oServiceList.equipo = item.EquipmentDes;
                oServiceList.cantidad = item.Quantity;
                oServiceList.codigoServicio = item.SisactServCode;
                oServiceList.snCode = item.SnCode;
                oServiceList.spCode = item.SpCode;
                lstService.Add(oServiceList);
            }

            List<OPlanMigration.PostPlanMigration.RestPlanMigrationSnCodeList> lstSnCode = new List<OPlanMigration.PostPlanMigration.RestPlanMigrationSnCodeList>();
            OPlanMigration.PostPlanMigration.RestPlanMigrationSnCodeList oSnCodeList = null;

            foreach (OPlanMigration.ServiceByPlan item in objPlanMigrationRequest.MessageRequest.Body.SnCodeList)
            {
                oSnCodeList = new OPlanMigration.PostPlanMigration.RestPlanMigrationSnCodeList();
                oSnCodeList.sncode = item.SnCode;
                oSnCodeList.cf = item.CF;
                oSnCodeList.accion = item.Action;
                oSnCodeList.spcode = item.SpCode;
                lstSnCode.Add(oSnCodeList);
            }

            OPlanMigration.PostPlanMigration.RestPlanMigrationRequest oRestPlanMigrationRequest = new OPlanMigration.PostPlanMigration.RestPlanMigrationRequest
            {
                MessageRequest = new OPlanMigration.PostPlanMigration.RestPlanMigrationMessageRequest
                {
                    Header = new OPlanMigration.PostPlanMigration.RestPlanMigrationHeader
                    {
                        HeaderRequest = new OPlanMigration.PostPlanMigration.RestPlanMigrationHeaderRequest
                        {
                            consumer = objPlanMigrationRequest.MessageRequest.Header.HeaderRequest.consumer,
                            country = objPlanMigrationRequest.MessageRequest.Header.HeaderRequest.country,
                            dispositivo = objPlanMigrationRequest.MessageRequest.Header.HeaderRequest.dispositivo,
                            language = objPlanMigrationRequest.MessageRequest.Header.HeaderRequest.language,
                            modulo = objPlanMigrationRequest.MessageRequest.Header.HeaderRequest.modulo,
                            msgType = objPlanMigrationRequest.MessageRequest.Header.HeaderRequest.msgType,
                            operation = objPlanMigrationRequest.MessageRequest.Header.HeaderRequest.operation,
                            pid = objPlanMigrationRequest.MessageRequest.Header.HeaderRequest.pid,
                            system = objPlanMigrationRequest.MessageRequest.Header.HeaderRequest.system,
                            timestamp = objPlanMigrationRequest.MessageRequest.Header.HeaderRequest.timestamp,
                            userId = objPlanMigrationRequest.MessageRequest.Header.HeaderRequest.userId,
                            wsIp = objPlanMigrationRequest.MessageRequest.Header.HeaderRequest.wsIp
                        }
                    },
                    Body = new OPlanMigration.PostPlanMigration.RestPlanMigrationBodyRequest
                    {
                        servvMsisd = objPlanMigrationRequest.MessageRequest.Body.ServvMsisd,
                        servdFechaProg = objPlanMigrationRequest.MessageRequest.Body.ServdProgDate,
                        codId = objPlanMigrationRequest.MessageRequest.Body.CodId,
                        simCard = objPlanMigrationRequest.MessageRequest.Body.SimCard,
                        tmCodeActual = objPlanMigrationRequest.MessageRequest.Body.tmCodeCurrent,
                        tmCodeNuevo = objPlanMigrationRequest.MessageRequest.Body.tmCodeNew,
                        customerId = objPlanMigrationRequest.MessageRequest.Body.CustomerId,
                        servvTipoServicio = objPlanMigrationRequest.MessageRequest.Body.ServvTypeService,
                        serviCoser = objPlanMigrationRequest.MessageRequest.Body.ServiCoser,
                        tipoReg = objPlanMigrationRequest.MessageRequest.Body.TypeReg,
                        servvUsuarioSis = objPlanMigrationRequest.MessageRequest.Body.ServvUserSystem,
                        servvUsuarioApp = objPlanMigrationRequest.MessageRequest.Body.ServvUserApp,
                        servvEmailUsuarioApp = objPlanMigrationRequest.MessageRequest.Body.ServvEmailUserApp,
                        servcEstado = objPlanMigrationRequest.MessageRequest.Body.ServcState,
                        descCoSer = objPlanMigrationRequest.MessageRequest.Body.DescCoSer,
                        servcNrocuenta = objPlanMigrationRequest.MessageRequest.Body.ServcAccountNumber,
                        contactoObjId = objPlanMigrationRequest.MessageRequest.Body.ContactObjId,
                        flagReg = objPlanMigrationRequest.MessageRequest.Body.FlagReg,

                        //Main Interaction
                        account = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.ACCOUNT,
                        phone = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.PHONE,
                        tipo = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.TYPE,
                        clase = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.CLASS,
                        subclase = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.SUBCLASS,
                        metodoContacto = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.METHOD,
                        tipoInter = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.TYPE_INTER,
                        agente = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.AGENT,
                        usrProceso = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.USER_PROCESS,
                        hechoEnUno = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.MADEINONE,
                        notas = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.NOTES,
                        flagCaso = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.FLAG_CASE,
                        resultado = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.RESULT,
                        servafect = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.SERVICE,
                        inconven = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.INCONVENIENT,
                        servafectCode = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.SERVICECODE,
                        inconvenCode = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.INCONVENIENTCODE,
                        codPlano = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.MAP,
                        valor1 = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.VALUE1,
                        valor2 = objPlanMigrationRequest.MessageRequest.Body.MainInteraction.VALUE2,

                        //Plus Interaction
                        inter1 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_1,
                        inter2 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_2,
                        inter3 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_3,
                        inter4 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_4,
                        inter5 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_5,
                        inter6 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_6,
                        inter7 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_7,
                        inter8 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_8,
                        inter9 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_9,
                        inter10 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_10,
                        inter11 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_11,
                        inter12 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_12,
                        inter13 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_13,
                        inter14 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_14,
                        inter15 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_15,
                        inter16 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_16,
                        inter17 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_17,
                        inter18 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_18,
                        inter19 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_19,
                        inter20 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_20,
                        inter21 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_21,
                        inter22 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_22,
                        inter23 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_23,
                        inter24 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_24,
                        inter25 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_25,
                        inter26 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_26,
                        inter27 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_27,
                        inter28 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_28,
                        inter29 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_29,
                        inter30 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_INTER_30,
                        plusInter2Interact = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_PLUS_INTER2INTERACT,
                        adjustmentAmount = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_ADJUSTMENT_AMOUNT,
                        adjustmentReason = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_ADJUSTMENT_REASON,
                        address = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_ADDRESS,
                        amountUnit = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_AMOUNT_UNIT,
                        birthday = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_BIRTHDAY,
                        clarifyInteraction = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CLARIFY_INTERACTION,
                        claroLdn1 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CLARO_LDN1,
                        claroLdn2 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CLARO_LDN2,
                        claroLdn3 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CLARO_LDN3,
                        claroLdn4 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CLARO_LDN4,
                        claroLocal1 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CLAROLOCAL1,
                        claroLocal2 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CLAROLOCAL2,
                        claroLocal3 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CLAROLOCAL3,
                        claroLocal4 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CLAROLOCAL4,
                        claroLocal5 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CLAROLOCAL5,
                        claroLocal6 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CLAROLOCAL6,
                        contactPhone = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CONTACT_PHONE,
                        dniLegalRep = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_IDCARD_LEGAL_REP,
                        documentNumber = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_DOCUMENT_NUMBER,
                        email = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_EMAIL,
                        firstName = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_FIRST_NAME,
                        fixedNumber = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_FIXED_NUMBER,
                        flagChangeUser = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_FLAG_CHANGE_USER,
                        flagLegalRep = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_FLAG_LEGAL_REP,
                        flagOther = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_FLAG_OTHER,
                        flagTitular = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_FLAG_TITULAR,
                        imei = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_IMEI,
                        lastName = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_LAST_NAME,
                        lastNameRep = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_LASTNAME_REP,
                        ldiNumber = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_LDI_NUMBER,
                        nameLegalRep = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_NAME_LEGAL_REP,
                        oldClaroLdn1 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_CLARO_LDN1,
                        oldClaroLdn2 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_CLARO_LDN2,
                        oldClaroLdn3 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_CLARO_LDN3,
                        oldClaroLdn4 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_CLARO_LDN4,
                        oldClaroLocal1 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_CLAROLOCAL1,
                        oldClaroLocal2 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_CLAROLOCAL2,
                        oldClaroLocal3 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_CLAROLOCAL3,
                        oldClaroLocal4 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_CLAROLOCAL4,
                        oldClaroLocal5 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_CLAROLOCAL5,
                        oldClaroLocal6 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_CLAROLOCAL6,
                        oldDocNumber = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_DOC_NUMBER,
                        oldFirstName = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_FIRST_NAME,
                        oldFixedPhone = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_FIXED_PHONE,
                        oldLastName = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_LAST_NAME,
                        oldLdiNumber = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_LDI_NUMBER,
                        oldFixedNumber = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OLD_FIXED_NUMBER,
                        operationType = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OPERATION_TYPE,
                        otherDocNumber = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OTHER_DOC_NUMBER,
                        otherFirstName = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OTHER_FIRST_NAME,
                        otherLastName = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OTHER_LAST_NAME,
                        otherPhone = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OTHER_PHONE,
                        phoneLegalRep = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_PHONE_LEGAL_REP,
                        referencePhone = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_REFERENCE_PHONE,
                        reason = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_REASON,
                        model = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_MODEL,
                        lotCode = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_LOT_CODE,
                        flagRegistered = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_FLAG_REGISTERED,
                        registrationReason = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_REGISTRATION_REASON,
                        claroNumber = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CLARO_NUMBER,
                        month = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_MONTH,
                        ostNumber = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OST_NUMBER,
                        basket = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_BASKET,
                        expireDate = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_EXPIRE_DATE,
                        address5 = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_ADDRESS5,
                        chargeAmount = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CHARGE_AMOUNT,
                        city = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CITY,
                        contactSex = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_CONTACT_SEX,
                        department = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_DEPARTMENT,
                        district = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_DISTRICT,
                        emailConfirmation = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_EMAIL_CONFIRMATION,
                        fax = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_FAX,
                        flagCharge = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_FLAG_CHARGE,
                        maritalStatus = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_MARITAL_STATUS,
                        occupation = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_OCCUPATION,
                        position = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_POSITION,
                        referenceAddress = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_REFERENCE_ADDRESS,
                        typeDocument = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_TYPE_DOCUMENT,
                        zipCode = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_ZIPCODE,
                        iccid = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_ICCID,
                        flagContingencia = objPlanMigrationRequest.MessageRequest.Body.PlusInteraction._X_FLAG_CONTINGENCY,

                        flagPenalidad = objPlanMigrationRequest.MessageRequest.Body.FlagPenalty,
                        reintegro = objPlanMigrationRequest.MessageRequest.Body.Repayment,
                        penalidadCalculada = objPlanMigrationRequest.MessageRequest.Body.CalculatedPenalty,
                        montoPenalidadCobrar = objPlanMigrationRequest.MessageRequest.Body.AmountPenaltyCharge,
                        flagFidelizaPenalidad = objPlanMigrationRequest.MessageRequest.Body.FlagLoyalPenalty,
                        montofidelizaPenalidad = objPlanMigrationRequest.MessageRequest.Body.AmountLoyalPenalty,
                        occPenalidad = objPlanMigrationRequest.MessageRequest.Body.OCCPenalty,
                        conceptooccPenalidad = objPlanMigrationRequest.MessageRequest.Body.OCCConceptPenalty,
                        ideaiWS = objPlanMigrationRequest.MessageRequest.Body.IdeaiWS,
                        ipCliente = objPlanMigrationRequest.MessageRequest.Body.IpClient,
                        cuentaUsuario = objPlanMigrationRequest.MessageRequest.Body.UserAccount,
                        nombreServidor = objPlanMigrationRequest.MessageRequest.Body.ServerName,
                        telefono = objPlanMigrationRequest.MessageRequest.Body.Phone,
                        texto = objPlanMigrationRequest.MessageRequest.Body.Text,

                        listaServicios = new OPlanMigration.PostPlanMigration.RestPlanMigrationService 
                        {
                            servicio = lstService
                        },
                        listaSnCode = new OPlanMigration.PostPlanMigration.RestPlanMigrationSNCode
                        {
                            datosSnCode = lstSnCode
                        },
                        parametroConstancia = objPlanMigrationRequest.MessageRequest.Body.ConstancyParameter,
                        codigoProducto = objPlanMigrationRequest.MessageRequest.Body.ProductCode
                    }
                }
            };
            
            return Claro.Web.Logging.ExecuteMethod<OPlanMigration.PostPlanMigration.PlanMigrationResponse>(objPlanMigrationRequest.Audit.Session, objPlanMigrationRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Postpaid.PlanMigration.PostPlanMigration(objPlanMigrationRequest, oRestPlanMigrationRequest);
            });
        }
    }
}
