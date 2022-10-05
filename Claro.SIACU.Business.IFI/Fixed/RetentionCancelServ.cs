using System;
using System.Collections.Generic;
using EntitiesFixed = Claro.SIACU.Entity.IFI.Fixed;
using KEY = Claro.ConfigurationManager;

using Claro.SIACU.Entity.IFI.Fixed;
using Claro.SIACU.Entity.IFI.Fixed.GetCaseInsert;

using System.Linq;
using Claro.SIACU.Entity.IFI.Fixed.GetRetentionCancelServices;
using Claro.SIACU.Entity.IFI.Fixed.GetAddDayWork;

namespace Claro.SIACU.Business.IFI.Fixed
{
    public class RetentionCancelServ
    {

        /// <summary>Método que permite mostrar la lista de acciones.</summary>
        /// <param name="objAccionRequest"></param>
        /// <returns>EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse</returns>
        /// <remarks>GetListarAcciones</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetListarAcciones(Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesRequest objAccionRequest)
        {
            var objAccionResponse = new EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse();


            objAccionResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstAcciones = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstAcciones = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objAccionRequest.Audit.Session, objAccionRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetAcciones(objAccionRequest.Audit.Session, objAccionRequest.Audit.Transaction,objAccionRequest.vNivel);
                });
                objAccionResponse.AccionTypes = lstAcciones;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAccionRequest.vNivel.ToString(), objAccionRequest.vtransaction, ex.Message);
                throw ex;
            }

            return objAccionResponse;
        }


        /// <summary>Método que permite mostrar la lista de motivo de cancelación.</summary>
        /// <param name="objMotCancelacionRequest"></param>
        /// <returns>EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse</returns>
        /// <remarks>GetMotCancelacion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetMotCancelacion(Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesRequest objMotCancelacionRequest)
        {
            var objMotCancelacionResponse = new EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse();


            objMotCancelacionResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstMotCancelacion = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstMotCancelacion = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objMotCancelacionRequest.Audit.Session, objMotCancelacionRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetMotCancelacion(objMotCancelacionRequest);
                });
                objMotCancelacionResponse.AccionTypes = lstMotCancelacion;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMotCancelacionRequest.vEstado.ToString(),objMotCancelacionRequest.vtransaction, ex.Message);
                throw ex;
            }

            return objMotCancelacionResponse;
        }


        /// <summary>Método que permite mostrar la lista de submotivo de cancelación.</summary>
        /// <param name="objAccionRequest"></param>
        /// <returns>EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse</returns>
        /// <remarks>GetSubMotiveCancel</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetSubMotiveCancel(Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesRequest objAccionRequest)
        {
            var objSubMotResponse = new EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse();


            objSubMotResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstSubMotivo = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstSubMotivo = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objAccionRequest.Audit.Session, objAccionRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetSubMotiveCancel(objAccionRequest.Audit.Session, objAccionRequest.Audit.Transaction, objAccionRequest.vIdMotive);
                });
                objSubMotResponse.AccionTypes = lstSubMotivo.Where(x => x.Estado == "1").ToList<EntitiesFixed.GenericItem>();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAccionRequest.vIdMotive.ToString(), objAccionRequest.vtransaction, ex.Message);
                throw ex;
            }

            return objSubMotResponse;
        }


        /// <summary>Método que permite mostrar la lista de tipo de trabajo.</summary>
        /// <param name="objTypeRequest"></param>
        /// <returns>EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse</returns>
        /// <remarks>GetTypeWork</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetTypeWork(Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesRequest objTypeRequest)
        {
            var objTypeWorkResponse = new EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse();


            objTypeWorkResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstTypeWork = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstTypeWork = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objTypeRequest.Audit.Session, objTypeRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetTypeWork(objTypeRequest.Audit.Session, objTypeRequest.vIdTypeWork, objTypeRequest.Audit.Transaction);
                });
                objTypeWorkResponse.AccionTypes = lstTypeWork;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypeRequest.Audit.Session, objTypeRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objTypeWorkResponse;
        }


        /// <summary>Método que permite mostrar la lista de subtipo de trabajo.</summary>
        /// <param name="objTypeRequest"></param>
        /// <returns>RetentionCancelServicesResponse</returns>
        /// <remarks>GetSubTypeWork</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static RetentionCancelServicesResponse GetSubTypeWork(RetentionCancelServicesRequest objTypeRequest)
        {
            var objTypeWorkResponse = new RetentionCancelServicesResponse();


            objTypeWorkResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstTypeWork = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstTypeWork = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objTypeRequest.Audit.Session, objTypeRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetSubTypeWork(objTypeRequest.Audit.Session, objTypeRequest.vIdTypeWork, objTypeRequest.Audit.Transaction);
                });
                objTypeWorkResponse.AccionTypes = lstTypeWork;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypeRequest.Audit.Session, objTypeRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objTypeWorkResponse;
        }



        /// <summary>Método que permite mostrar la lista de motivos.</summary>
        /// <param name="objMotiveSOTRequest"></param>
        /// <returns>EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse</returns>
        /// <remarks>GetMotiveSOT</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetMotiveSOT(Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesRequest objMotiveSOTRequest)
        {
            var objMotiveSOTResponse = new EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse();


            objMotiveSOTResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstMotiveSOT = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstMotiveSOT = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objMotiveSOTRequest.Audit.Session, objMotiveSOTRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetMotiveSOT(objMotiveSOTRequest.Audit.Session, objMotiveSOTRequest.Audit.Transaction);
                });
                objMotiveSOTResponse.AccionTypes = lstMotiveSOT;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMotiveSOTRequest.Audit.Session, objMotiveSOTRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objMotiveSOTResponse;
        }



        /// <summary>Método que permite agregar días laborables.</summary>
        /// <param name="objRequest"></param>
        /// <returns>EntitiesFixed.GetAddDayWork.AddDayWorkResponse</returns>
        /// <remarks>GetAddDayWork</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.GetAddDayWork.AddDayWorkResponse GetAddDayWork(EntitiesFixed.GetAddDayWork.AddDayWorkRequest objRequest)
        {
            var objResponse = new AddDayWorkResponse();


            //objResponse = new AddDayWorkResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<AddDayWorkResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetAddDayWork(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.FechaInicio, objRequest.NumeroDias, objRequest.FechaResultado, objRequest.CodError, objRequest.DesError);
                });
                
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }



        /// <summary>Método que permite obtener los parámetros para el terminal TPI.</summary>
        /// <param name="objRequest"></param>
        /// <returns>Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesResponse</returns>
        /// <remarks>GetObtainParameterTerminalTPI</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetObtainParameterTerminalTPI(Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse();

            objResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstParameter = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstParameter = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetObtainParameterTerminalTPI(objRequest.Audit.Session.ToString(), objRequest.Audit.Transaction.ToString(),Convert.ToInt(objRequest.ParameterID), objRequest.Descripcion);
                });
                objResponse.AccionTypes = lstParameter;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }



        /// <summary>Método que permite obtener los parámetros para el terminal TPI postpago.</summary>
        /// <param name="objRequest"></param>
        /// <returns>Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesResponse</returns>
        /// <remarks>GetSoloTFIPostpago</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetSoloTFIPostpago(Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse();

            objResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstParameter = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstParameter = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetObtainParameterTerminalTPI(objRequest.Audit.Session.ToString(), objRequest.Audit.Transaction.ToString(), Convert.ToInt(objRequest.ParameterID), objRequest.Descripcion);
                });
                objResponse.AccionTypes = lstParameter;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }


        /// <summary>Método que permite obtener los datos de facturación.</summary>
        /// <param name="objRequest"></param>
        /// <returns>Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesResponse</returns>
        /// <remarks>ObtenerDatosBSCSExt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesResponse ObtenerDatosBSCSExt(Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse();

            try
            {
                double NroFacturas = 0;
                double CargoFijoActual = 0;
                double CargoFijoNuevoPlan = 0;

                objResponse.Resultado = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {

                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.ObtenerDatosBSCSExt(objRequest.Audit.Session, objRequest.Audit.Transaction,
                        objRequest.NroTelefono, objRequest.CodNuevoPlan, ref NroFacturas, ref CargoFijoActual, ref CargoFijoNuevoPlan);
                });


                objResponse.NroFacturas = NroFacturas;
                objResponse.CargoFijoActual = CargoFijoActual;
                objResponse.CargoFijoNuevoPlan = CargoFijoNuevoPlan;
   
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }



        /// <summary>Método que permite obtener los datos de la penalidad por el acuerdo.</summary>
        /// <param name="objRequest"></param>
        /// <returns>Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesResponse</returns>
        /// <remarks>GetObtainPenalidadExt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetObtainPenalidadExt(Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse();

            
            
            try
            {
                double AcuerdoIdSalida = 0;
                double DiasPendientes =0;
                double CargoFijoDiario = 0;
                double PrecioLista = 0;
                double PrecioVenta = 0;
                double PenalidadPCS = 0;
                double PenalidaAPADECE = 0;

                objResponse.Resultado = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {

                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetObtainPenalidadExt(objRequest.Audit.Session, objRequest.Audit.Transaction, 
                        objRequest.NroTelefono, objRequest.FechaPenalidad,objRequest.NroFacturas, 
                        objRequest.CargoFijoActual, objRequest.CargoFijoNuevoPlan, objRequest.DiasxMes, 
                        objRequest.CodNuevoPlan,ref AcuerdoIdSalida,ref DiasPendientes,ref CargoFijoDiario,
                        ref PrecioLista,ref PrecioVenta,ref PenalidadPCS,ref PenalidaAPADECE);
                });


                objResponse.AcuerdoIdSalida = AcuerdoIdSalida;
                objResponse.DiasPendientes = DiasPendientes;
                objResponse.CargoFijoDiario = CargoFijoDiario;
                objResponse.PrecioLista = PrecioLista;
                objResponse.PrecioVenta = PrecioVenta;
                objResponse.PenalidadPCS = PenalidadPCS;
                objResponse.PenalidaAPADECE = PenalidaAPADECE;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }


        /// <summary>Método que permite validar el identificador del cliente.</summary>
        /// <param name="objRequest"></param>
        /// <returns>Entity.IFI.Fixed.GetValidateCustomerID.ValidateCustomerIdResponse</returns>
        /// <remarks>GetValidateCustomerId</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static Entity.IFI.Fixed.GetValidateCustomerID.ValidateCustomerIdResponse GetValidateCustomerId(Entity.IFI.Fixed.GetValidateCustomerID.ValidateCustomerIdRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdResponse();


            objResponse = new EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdResponse();


            try
            {
                
                int vCONTACTOBJID = 0;
                string  strflgResult = string.Empty;
                string strMSError = string.Empty;

                objResponse.resultado = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetValidateCustomerId(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Phone, ref vCONTACTOBJID, ref strflgResult, ref strMSError);
                });

                objResponse.ContactObjID = vCONTACTOBJID;
                objResponse.FlgResult = strflgResult;
                objResponse.MsError = strMSError;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }


        /// <summary>Método que permite registrar al cliente.</summary>
        /// <param name="objRequest"></param>
        /// <returns>EntitiesFixed.GetCustomer.CustomerResponse</returns>
        /// <remarks>GetRegisterCustomerId</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.GetCustomer.CustomerResponse GetRegisterCustomerId(Entity.IFI.Fixed.Customer objRequest)
        {
            var objResponse = new EntitiesFixed.GetCustomer.CustomerResponse();

            try
            {

                string strflgResult = string.Empty;
                string strMSError = string.Empty;

                objResponse.Resultado = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetRegisterCustomerId(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest, ref strflgResult, ref strMSError);
                });
                objResponse.vFlagConsulta = strflgResult;
                objResponse.rMsgText = strMSError;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }


        /// <summary>Método que permite insertar el caso.</summary>
        /// <param name="objRequest"></param>
        /// <returns>CaseInsertResponse</returns>
        /// <remarks>GetCaseInsert</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static CaseInsertResponse GetCaseInsert(CaseInsertRequest objRequest)
        {
           CaseInsertResponse oResponse = new CaseInsertResponse();

            try
            {

                string rCasoId=string.Empty;
                string rFlagInsercion=string.Empty;
                string rMsgText = string.Empty;

                oResponse.rMsgText = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetCaseInsert(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest, ref rCasoId, ref rFlagInsercion, ref rMsgText);
                });
                oResponse.rCasoId = rCasoId;
                oResponse.rFlagInsercion = rFlagInsercion;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return oResponse;
        }


        /// <summary>Método que obtiene el acuerdo para la adquisición de equipos con descuento especial(APADECE).</summary>
        /// <param name="objRequest"></param>
        /// <returns>RetentionCancelServicesResponse</returns>
        /// <remarks>GetApadeceCancelRet</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static RetentionCancelServicesResponse GetApadeceCancelRet(RetentionCancelServicesRequest objRequest)
        {
            var objResponse = new RetentionCancelServicesResponse();

            try
            {
                double rdbValorApadece = 0;
                string rintCodError = string.Empty;
                string rp_msg_text = string.Empty;
                
                objResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetApadeceCancelRet(objRequest.Audit.Session, objRequest.Audit.Transaction, Convert.ToInt(objRequest.NroTelefono), objRequest.CodId, ref rdbValorApadece, ref rintCodError, ref rp_msg_text);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.vtransaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }


        /// <summary>Método que permite desactivar el contrato.</summary>
        /// <param name="objRequestCliente"></param>
        /// <returns>bool</returns>
        /// <remarks>GetDesactivatedContract</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static bool GetDesactivatedContract( Customer objRequestCliente)
        {
            bool resultado = false;

            resultado = Claro.Web.Logging.ExecuteMethod<bool>(objRequestCliente.Audit.Session, objRequestCliente.Audit.Transaction, () =>
            {
                return Data.IFI.Fixed.RetentionCancelServ.GetDesactivatedContract(objRequestCliente);
            });

            return resultado;

        
        }


        /// <summary>Método que permite crear un caso.</summary>
        /// <param name="oRequest"></param>
        /// <returns>EntitiesFixed.GetCaseInsert.CaseInsertRespons</returns>
        /// <remarks>GetCreateCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.GetCaseInsert.CaseInsertResponse GetCreateCase(EntitiesFixed.GetCaseInsert.CaseInsertRequest oRequest)
        {
            CaseInsertResponse objResponse = new CaseInsertResponse();
            try
            {

                objResponse = Claro.Web.Logging.ExecuteMethod(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetCreateCase(oRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        
        }


        /// <summary>Método que obtiene el motivo por tipo de trabajo.</summary>
        /// <param name="objRequest"></param>
        /// <returns>EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse</returns>
        /// <remarks>GetMotiveSOTByTypeJob</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse GetMotiveSOTByTypeJob(EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobRequest objRequest)
        {
            EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse objResponse = new EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse();
            try
            {
                objResponse.List = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.IFI.Fixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.RetentionCancelServ.GetMotiveSOTByTypeJob(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.tipTra);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }


        /// <summary>Método que obtiene los contratos desactivados</summary>
        /// <param name="objRequest"></param>
        /// <returns>bool</returns>
        /// <remarks>GetDesactivatedContract_LTE</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static bool GetDesactivatedContract_LTE(Customer objRequest,ref string message)
        {
            bool resultado = false;
            string msj = "";
            resultado = Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Fixed.RetentionCancelServ.GetDesactivatedContract_LTE(objRequest, ref msj);
            });
            message = msj;
            return resultado;


        }

    }
}
