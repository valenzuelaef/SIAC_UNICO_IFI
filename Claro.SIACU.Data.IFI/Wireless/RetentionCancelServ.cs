using Claro.SIACU.Data.IFI.Configuration;
using Claro.SIACU.Entity.IFI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUSTOMER_INT = Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS;
using GENERATEOCC = Claro.SIACU.ProxyService.IFI.SIACU.BillingService;
using GETAPADECE = Claro.SIACU.ProxyService.IFI.SIACU.GestionAcuerdoIFI;


namespace Claro.SIACU.Data.IFI.Wireless
{
    public class RetentionCancelServ
    {
        /// <summary>
        /// desactiva contrato 
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetDesactivatedContract_INT</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetDesactivatedContract_INT(Customer objRequest)
        {
            bool resultado = false;
            try
            {
                CUSTOMER_INT.desactivarContratoRequestType oRequest = new CUSTOMER_INT.desactivarContratoRequestType();
                CUSTOMER_INT.desactivarContratoResponseType oResponse = new CUSTOMER_INT.desactivarContratoResponseType();
                CUSTOMER_INT.AttributeValuePair oTempServicio = new CUSTOMER_INT.AttributeValuePair();
                CUSTOMER_INT.auditRequestType objAuditRequest = new CUSTOMER_INT.auditRequestType();
                CUSTOMER_INT.auditResponseType objAuditResponse = new CUSTOMER_INT.auditResponseType();

                objAuditRequest.idTransaccion = objRequest.Audit.Transaction;
                objAuditRequest.ipAplicacion = objRequest.Audit.IPAddress;
                objAuditRequest.nombreAplicacion = objRequest.ApplicationName;
                objAuditRequest.usuarioAplicacion = objRequest.UserApplication;
                oRequest.auditRequest = objAuditRequest;
                oRequest.codigoCliente = objRequest.CUSTOMER_ID;
                oRequest.codigoCuenta = objRequest.CUENTA;
                oRequest.codigoContrato = objRequest.CONTRATO_ID;
                oRequest.codigoServicio = objRequest.CODIGO_SERVICIO;
                oRequest.msisdn = objRequest.Msisdn;
                oRequest.reason = objRequest.Reason;           
                oRequest.FechaProgramacion = objRequest.FECHA_PROGRAMACION;
                oRequest.FechaProgramacionSOT = objRequest.FECHA_PROGRAMACION_SOT;
                oRequest.franjaHoraria = string.Empty;             
                oRequest.tipTra = objRequest.TIPO_TRABAJO;
                oRequest.montoPenalidad = objRequest.MONTO_PENALIDAD;               
                oRequest.tipoCliente = objRequest.TIPO_CLIENTE;
                oRequest.areaPCS = objRequest.AREA_PCS;
                oRequest.motivoPCS = objRequest.MOTIVO_PCS;      
                oRequest.subMotivoPCS = objRequest.SUB_MOTIVO_PCS;
                oRequest.cicloFacturacion = objRequest.CICLO_FACTURACION;
                oRequest.numeroDocumento = objRequest.NRO_DOC;         
                oRequest.usuarioAsesor = objRequest.ASESOR; 
                oRequest.tipoServicio = objRequest.TIPO_SERVICIO;
                oRequest.observaciones = objRequest.OBSERVACIONES;    
                oRequest.flagOccApadece = objRequest.FLAG_OCC_APADECE;
                oRequest.flagNdPcs = objRequest.FLAG_ND_PCS;
                oRequest.cacDac = objRequest.Des_CAC;      
                oRequest.montoPCS = objRequest.MONTO_PCS;
                oRequest.montoFidelizacion = objRequest.MONTO_FIDELIZACION;
                oRequest.trace = objRequest.TRACE;       
                oRequest.fechaActual = Convert.ToString(objRequest.FECHA_ACT);
                oRequest.codigoPlano = objRequest.CODIGO_PLANO_INST;
                oRequest.codigoMotivo = objRequest.COD_MOTIVE;
                oRequest.mailUsuarioAplicacion = objRequest.MAIL_USUARIO_APLICACION;
                oRequest.codigoInteraccion = objRequest.CODIGO_INTERACCION;

                oResponse = ServiceConfiguration.TransaccionesInternetLTE.desactivarContrato(oRequest);
                objAuditResponse = oResponse.responseData.auditResponse;

                resultado = objAuditResponse.codigoRespuesta == Claro.Constants.NumberZeroString ? true : false;

                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Fin Método - GetDesactivatedContract_INT|Resultado" + objAuditResponse.codigoRespuesta + " - " + objAuditResponse.mensajeRespuesta);

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - desactivarContrato_LTE :" + ex.Message);
                resultado = false;
            }

            return resultado;
        }

        /// <summary>
        /// genera OCC
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strIpApp"></param>
        /// <param name="dcCustomerId"></param>
        /// <param name="dcMonto"></param>
        /// <returns></returns>
        /// <remarks>GenerateOCC</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string GenerateOCC(string strIdSession, string strTransaction, string strIpApp, decimal dcCustomerId, float dcMonto)
        {
            string result = "";
            decimal registraOcc = -1;
            try
            {
                GENERATEOCC.GeneraOCCRequest objGeneraOCCRequest = new GENERATEOCC.GeneraOCCRequest()
                {
                    codigoOcc = Convert.ToDecimal(ConfigurationManager.AppSettings("KeycodigoOccIFI")),
                    customerId = dcCustomerId,
                    ipApp = strIpApp,
                    montoOcc = dcMonto,
                    nroCuotas = Convert.ToDecimal(ConfigurationManager.AppSettings("KeynroCuotasIFI")),
                    recDate = DateTime.Today,
                    remark = ConfigurationManager.AppSettings("KeyremarkIFI"),
                    txId = strTransaction,
                    usrApp = "SIACUIFI"


                };

                ServiceConfiguration.TransaccionOCCPort.GeneraOCC(objGeneraOCCRequest.txId, objGeneraOCCRequest.ipApp, objGeneraOCCRequest.usrApp, objGeneraOCCRequest.customerId, objGeneraOCCRequest.codigoOcc, objGeneraOCCRequest.nroCuotas, objGeneraOCCRequest.montoOcc, objGeneraOCCRequest.recDate, objGeneraOCCRequest.remark, out registraOcc);
                result = registraOcc.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, "Error WS - desactivarContrato_LTE :" + ex.Message);
                result = "-1";
            }

            return result;
        }
        /// <summary>
        /// Obtiene el apadece
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strIpApp"></param>
        /// <param name="strApplication"></param>
        /// <param name="strUser"></param>
        /// <param name="strMsisdn"></param>
        /// <param name="strCoId"></param>
        /// <returns></returns>
        ///  <remarks>GetApadece</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string GetApadece(string strIdSession, string strTransaction, string strIpApp, string strApplication, string strUser, string strMsisdn, string strCoId)
        {
            string codeResult = string.Empty;
            string msjResult = string.Empty;
            string monto = string.Empty;
            try
            {
                GETAPADECE.obtenerReintegroEquipoRequest oRequest = new GETAPADECE.obtenerReintegroEquipoRequest();
                GETAPADECE.obtenerReintegroEquipoResponse oResponse = new GETAPADECE.obtenerReintegroEquipoResponse();
                GETAPADECE.auditRequestType oAuditType = new GETAPADECE.auditRequestType();
                GETAPADECE.parametrosTypeObjetoOpcional objOpcional1 = new GETAPADECE.parametrosTypeObjetoOpcional();
                GETAPADECE.parametrosTypeObjetoOpcional objOpcional2 = new GETAPADECE.parametrosTypeObjetoOpcional();

                oAuditType.idTransaccion = strTransaction;
                oAuditType.ipAplicacion = strIpApp;
                oAuditType.nombreAplicacion = strApplication;
                oAuditType.usuarioAplicacion = strUser;


                oRequest.auditRequest = oAuditType;
                oRequest.msisdn = strMsisdn;
                oRequest.coId = strCoId;
                oRequest.fechaTransaccion = DateTime.Now.ToShortDateString();
                oRequest.cargoFijoNuevo = Claro.Constants.NumberZeroString;
                oRequest.motivoApadece = ConfigurationManager.AppSettings("strMotivoApadece");
                oRequest.flagEquipo = ConfigurationManager.AppSettings("strFlagEquipo");

                objOpcional1.campo = "";
                objOpcional2.campo = "";

                objOpcional1.valor = "";
                objOpcional2.valor = "";

                GETAPADECE.parametrosTypeObjetoOpcional[] a;
                a = new GETAPADECE.parametrosTypeObjetoOpcional[3];
                oRequest.listaRequestOpcional = a;

                oRequest.listaRequestOpcional[0] = objOpcional1;
                oRequest.listaRequestOpcional[1] = objOpcional2;

                oResponse = ServiceConfiguration.GestionAcuerdoWS.obtenerReintegroEquipo(oRequest);


                if (oResponse.auditResponse != null)
                {
                    codeResult = oResponse.auditResponse.codigoRespuesta;
                    msjResult = oResponse.auditResponse.mensajeRespuesta;
                    if (codeResult == Claro.Constants.NumberZeroString)
                    {
                        monto = oResponse.acuerdoMontoApacedeTotal;
                    }
                }

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, "Error WS - desactivarContrato_LTE :" + ex.Message);

            }

            return monto;
        }
    }
}
