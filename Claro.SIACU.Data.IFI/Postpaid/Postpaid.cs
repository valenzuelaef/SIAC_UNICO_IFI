using System;
using System.Collections.Generic;
using Claro.Data;
using System.Data;
using System.Web;

using KEY = Claro.ConfigurationManager;
using COMMON = Claro.SIACU.Entity.IFI.Common;
using POSTPAID_CONSULTCLIENT = Claro.SIACU.ProxyService.IFI.SIACPost.Customer;
using Claro.Web;
using Claro.SIACU.Entity.IFI.Postpaid;
using Claro.SIACU.Data.IFI.Configuration;
using Claro.SIACU.Entity.IFI.Postpaid.GetTypeTransactionBRMS;
using Claro.SIACU.ProxyService.IFI.WSBRMS;
using Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Entity.IFI.Postpaid.GetClientDataAdd;

namespace Claro.SIACU.Data.IFI.Postpaid
{
    public class Postpaid
    {
        /// <summary>
        /// obtiene cantidad entrante de llamada
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="Name"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        /// <remarks>GetAmountIncomingCall</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<AmountIncomingCall> GetAmountIncomingCall(string strIdSession, string strTransaction, string Name, ref string Message)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}", "GetAmountIncomingCall", Name);
    
            List<AmountIncomingCall> listAmountIncomingCall = new List<AmountIncomingCall>();
            DbParameter[] parameters = {
                                           new DbParameter("P_NOMBRE", DbType.String, ParameterDirection.Input, Name),
                                           new DbParameter("P_MENSAJE", DbType.String, ParameterDirection.Output),
                                           new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
                                       };
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTENER_DATO, parameters, (IDataReader reader) =>
            {
                AmountIncomingCall entity = null;

                while (reader.Read())
                {
                    entity = new AmountIncomingCall();
                    entity.Description = Claro.Utils.CheckStr(reader["DESCRIPCION"]);
                    entity.ValorN = Claro.Utils.CheckDbl(reader["VALOR_N"]);
                    listAmountIncomingCall.Add(entity);
                }
            });

            Message = parameters[1].Value.ToString();
            return listAmountIncomingCall;
        }
        /// <summary>
        /// Obtiene datos de linea
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="intContractID"></param>
        /// <param name="Dataline"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        /// <remarks>GetDataline</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string GetDataline(string strIdSession, string strTransaction, int intContractID, ref COMMON.Line Dataline, ref string Message)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}", "GetDataline", intContractID);
          
            string strCodReturn = "";

            POSTPAID_CONSULTCLIENT.contrato[] ObjContract = Claro.Web.Logging.ExecuteMethod<POSTPAID_CONSULTCLIENT.contrato[]>
                (strIdSession, strTransaction,
                Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT,
                () =>
                {
                    return Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT.datosContrato(intContractID);
                });

            if (ObjContract.Length >= 1)
            {
                Dataline = new COMMON.Line();
                Dataline.DateStatus = ObjContract[0].fec_estado;
                Dataline.LineStatus = ObjContract[0].estado;
                Dataline.Reason = ObjContract[0].motivo;
                Dataline.Plan = ObjContract[0].plan;
                Dataline.TermContract = ObjContract[0].plazo_contrato;
                Dataline.NumICCID = ObjContract[0].iccid;
                Dataline.NumIMSI = ObjContract[0].imsi;
                Dataline.Sale = ObjContract[0].vendedor;
                Dataline.Bell = ObjContract[0].campania;
                Dataline.ActivationDate = Claro.Utils.CheckStr(ObjContract[0].fecha_act);
                Dataline.FlagPlatform = ObjContract[0].flag_plataforma;
                Dataline.PIN1 = ObjContract[0].pin1;
                Dataline.PIN2 = ObjContract[0].pin2;
                Dataline.PUK1 = ObjContract[0].puk1;
                Dataline.PUK2 = ObjContract[0].puk2;
                Dataline.CodPlanTariff = Claro.Utils.CheckStr(ObjContract[0].codigo_plan_tarifario);
                Dataline.PhoneNumber = ObjContract[0].telefono;
                Dataline.ContractID = Claro.Utils.CheckStr(ObjContract[0].co_id);
                strCodReturn = "1";

            }
            else
            {
                Dataline.LineStatus = "";
                Dataline.Reason = "";
                Dataline.Plan = "";
                Dataline.TermContract = "";
                Dataline.NumICCID = "";
                Dataline.NumIMSI = "";
                Dataline.Sale = "";
                Dataline.Bell = "";
                Dataline.FlagPlatform = "";
                Dataline.PIN1 = "";
                Dataline.PIN2 = "";
                Dataline.PUK1 = "";
                Dataline.PUK2 = "";
                Dataline.PhoneNumber = "";
                Dataline.ContractID = "";
                strCodReturn = "Error: No existe datos";
            }
           
            return strCodReturn;
        }
        /// <summary>
        /// obtiene tipo de transacciones BRMS
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetTypeTransactionBRMS</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static TypeTransactionBRMSResponse GetTypeTransactionBRMS(TypeTransactionBRMSRequest objRequest)
        {
            REGLASAUTOMATIZACIONDEDOCUMENTOSResponse objServiceResponse = new REGLASAUTOMATIZACIONDEDOCUMENTOSResponse();
            REGLASAUTOMATIZACIONDEDOCUMENTOSRequest objServiceRequest = new REGLASAUTOMATIZACIONDEDOCUMENTOSRequest();

            objServiceRequest.pedidoEspecifico = new pedidoEspecifico();
            pedidoEspecifico objSpecificOrder = new pedidoEspecifico();
            objSpecificOrder.pedidoEspecifico1 = new consulta[1];

            consulta objConsult = new consulta();
            producto objProduct = new producto();

            objServiceRequest.pedidoGeneral = new pedidoGeneral();
            pedidoGeneral objGeneralOrder = new pedidoGeneral();

            proceso objProcess = new proceso();
            pedidoGeneral1 objGeneralOrder1 = new pedidoGeneral1();

            TypeTransactionBRMSResponse objResponse = new TypeTransactionBRMSResponse();

            try
            {
                objConsult.identificador = objRequest.StrIdentifier;
                objProduct.retencion = objRequest.StrRetention;
                objProduct.campana = string.Empty;
                objConsult.producto = objProduct;
                objSpecificOrder.pedidoEspecifico1[0] = objConsult;

                objProcess.operacion = objRequest.StrOperationCodSubClass;
                objProcess.transaccion = objRequest.StrTransactionM;

                objGeneralOrder1.proceso = objProcess;
                objGeneralOrder.pedidoGeneral1 = objGeneralOrder1;

                objServiceRequest.pedidoEspecifico = objSpecificOrder;
                objServiceRequest.pedidoGeneral = objGeneralOrder;

                objServiceResponse = Claro.Web.Logging.ExecuteMethod<REGLASAUTOMATIZACIONDEDOCUMENTOSResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                () =>
                {
                    return Configuration.WebServiceConfiguration.objWSBRMSService
                        .REGLASAUTOMATIZACIONDEDOCUMENTOS(objServiceRequest);
                });


                if (objServiceResponse.listaGeneral.listaGeneral1.Length > 0)
                {
                    objResponse.StrResult = objServiceResponse.listaGeneral.listaGeneral1[0];
                }
                else if (objServiceResponse.listaEspecifica.listaEspecifica1.Length > 0)
                {
                    objResponse.StrResult = objServiceResponse.listaEspecifica.listaEspecifica1[0].archivo;
                }

            }
            catch (Exception ex)
            {
                objResponse.StrError = ex.Message;
                return objResponse;
            }


            return objResponse;

        }
        /// <summary>
        /// obtiene clientes
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strcustomerid"></param>
        /// <param name="strtelefono"></param>
        /// <param name="Dataline"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        /// <remarks>GetDataCustomer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string GetDataCustomer(string strIdSession, string strTransaction, string strcustomerid, string strtelefono, ref COMMON.Client Dataline, ref string Message)
        {
            string strCodRetorno = "";
            string strError = "";
           
         try{

            POSTPAID_CONSULTCLIENT.cliente[] objTempCliente = new POSTPAID_CONSULTCLIENT.cliente[1];
            POSTPAID_CONSULTCLIENT.datosClienteResponse objdatosClienteResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID_CONSULTCLIENT.datosClienteResponse>
                (strIdSession, strTransaction,
                Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT,
                () =>
                {
                    return Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT.datosCliente(strcustomerid, strtelefono);
                });

            objTempCliente = objdatosClienteResponse.cliente;
            strError = objdatosClienteResponse.errorsql;
            if (strError == "")
            {
                if (objTempCliente.Length >= 1)
                {
                    Dataline.NOMBRES = Claro.Utils.CheckStr(objTempCliente[0].nombre);
                    Dataline.APELLIDOS = Claro.Utils.CheckStr(objTempCliente[0].apellidos);
                    Dataline.CUENTA = Claro.Utils.CheckStr(objTempCliente[0].cuenta);
                    Dataline.SEXO = Claro.Utils.CheckStr(objTempCliente[0].sexo);
                    Dataline.NRO_DOC = Claro.Utils.CheckStr(objTempCliente[0].num_doc);
                    Dataline.DOMICILIO = objTempCliente[0].direccion_fac;
                    Dataline.ZIPCODE = objTempCliente[0].cod_postal_fac;
                    Dataline.DEPARTAMENTO = objTempCliente[0].departamento_fac;
                    Dataline.DISTRITO = objTempCliente[0].distrito_fac;
                    Dataline.RAZON_SOCIAL = objTempCliente[0].razonSocial;
                    Dataline.PROVINCIA = objTempCliente[0].provincia_fac;
                    Dataline.DNI_RUC = objTempCliente[0].ruc_dni;
                    Dataline.ASESOR = objTempCliente[0].asesor;
                    Dataline.CICLO_FACTURACION = objTempCliente[0].ciclo_fac;
                    Dataline.CONSULTOR = objTempCliente[0].consultor;
                    Dataline.MODALIDAD = objTempCliente[0].modalidad;
                    Dataline.SEGMENTO = objTempCliente[0].segmento;
                    Dataline.CREDIT_SCORE = objTempCliente[0].credit_score;
                    Dataline.ESTADO_CUENTA = objTempCliente[0].status_cuenta;
                    Dataline.FECHA_ACT = objTempCliente[0].fecha_act;
                    Dataline.LIMITE_CREDITO = Claro.Utils.CheckStr(objTempCliente[0].limite_credito);
                    Dataline.TOTAL_CUENTAS = Claro.Utils.CheckStr(objTempCliente[0].num_cuentas);
                    Dataline.TOTAL_LINEAS = Claro.Utils.CheckStr(objTempCliente[0].num_lineas);
                    Dataline.RESPONSABLE_PAGO = objTempCliente[0].respon_pago;
                    Dataline.TIPO_CLIENTE = objTempCliente[0].tipo_cliente;
                    Dataline.REPRESENTANTE_LEGAL = objTempCliente[0].rep_legal;
                    Dataline.EMAIL = objTempCliente[0].email;
                    Dataline.TELEF_REFERENCIA = objTempCliente[0].telef_principal;
                    Dataline.CONTACTO_CLIENTE = objTempCliente[0].contacto_cliente;
                    Dataline.TIPO_DOC = objTempCliente[0].tip_doc;
                    Dataline.NRO_DOC = objTempCliente[0].num_doc;
                    Dataline.NOMBRE_COMERCIAL = objTempCliente[0].nomb_comercial;
                    Dataline.FAX = objTempCliente[0].fax;
                    Dataline.CARGO = objTempCliente[0].cargo;

                    Dataline.CUSTOMER_ID = Claro.Utils.CheckStr(objTempCliente[0].customerId);

                    Dataline.CONTACTO_CLIENTE = objTempCliente[0].contacto_cliente;
                    Dataline.TELEFONO_CONTACTO = objTempCliente[0].telef_contacto;
                    Dataline.CALLE_FAC = objTempCliente[0].direccion_fac;
                    Dataline.POSTAL_FAC = objTempCliente[0].cod_postal_fac;
                    Dataline.URBANIZACION_FAC = objTempCliente[0].urbanizacion_fac;
                    Dataline.DEPARTEMENTO_FAC = objTempCliente[0].departamento_fac;
                    Dataline.PROVINCIA_FAC = objTempCliente[0].provincia_fac;
                    Dataline.DISTRITO_FAC = objTempCliente[0].distrito_fac;
                    Dataline.CALLE_LEGAL = objTempCliente[0].direccion_fac;
                    Dataline.POSTAL_LEGAL = objTempCliente[0].cod_postal_fac;
                    Dataline.URBANIZACION_LEGAL = objTempCliente[0].urbanizacion_leg;
                    Dataline.DEPARTEMENTO_LEGAL = objTempCliente[0].departamento_leg;
                    Dataline.PROVINCIA_LEGAL = objTempCliente[0].provincia_leg;
                    Dataline.DISTRITO_LEGAL = objTempCliente[0].distrito_leg;
                    Dataline.PAIS_FAC = objTempCliente[0].pais_fac;
                    Dataline.PAIS_LEGAL = objTempCliente[0].pais_leg;
                    Dataline.REFERENCIA = objTempCliente[0].urbanizacion_fac;
                    Dataline.CONTRATO_ID = Claro.Utils.CheckStr(objTempCliente[0].co_id);
                    Dataline.LUGAR_NACIMIENTO_DES = Claro.Utils.CheckStr(objTempCliente[0].lug_nac);

                    Dataline.LUGAR_NACIMIENTO_ID = Claro.Utils.CheckStr(objTempCliente[0].nacionalidad);

                    if (objTempCliente[0].fecha_nac.ToString() != "")
                    { Dataline.FECHA_NAC = Claro.Utils.CheckDate(objTempCliente[0].fecha_nac); } // Convert.ToDate convertir // Claro.Utils.CheckDate
                    else
                    { Dataline.FECHA_NAC = Claro.Utils.CheckDate("1/1/1"); }
                    Dataline.ESTADO_CIVIL = Claro.Utils.CheckStr(objTempCliente[0].estado_civil);

                    Dataline.ESTADO_CIVIL_ID = Claro.Utils.CheckStr(objTempCliente[0].estado_civil_id);
                    Dataline.NICHO = Claro.Utils.CheckStr(objTempCliente[0].nicho_id);
                    Dataline.FORMA_PAGO = Claro.Utils.CheckStr(objTempCliente[0].forma_pago);
                    Dataline.COD_TIPO_CLIENTE = Claro.Utils.CheckStr(objTempCliente[0].codigo_tipo_cliente);

                    strCodRetorno = "1";
                }
                else
                {
                    Dataline.NOMBRES = "";
                    Dataline.APELLIDOS = "";
                    Dataline.CUENTA = "";
                    Dataline.NRO_DOC = "";
                    Dataline.DOMICILIO = "";
                    Dataline.ZIPCODE = "";
                    Dataline.DEPARTAMENTO = "";
                    Dataline.DISTRITO = "";
                    Dataline.RAZON_SOCIAL = "";
                    Dataline.PROVINCIA = "";
                    Dataline.DNI_RUC = "";
                    Dataline.ASESOR = "";
                    Dataline.CICLO_FACTURACION = "";
                    Dataline.CONSULTOR = "";
                    Dataline.MODALIDAD = "";
                    Dataline.SEGMENTO = "";
                    Dataline.CREDIT_SCORE = "";
                    Dataline.ESTADO_CUENTA = "";
                    Dataline.FECHA_ACT = Claro.Utils.CheckDate("1/1/1");
                    Dataline.LIMITE_CREDITO = "";
                    Dataline.TOTAL_CUENTAS = "";
                    Dataline.TOTAL_LINEAS = "";
                    Dataline.RESPONSABLE_PAGO = "";
                    Dataline.TIPO_CLIENTE = "";
                    Dataline.REPRESENTANTE_LEGAL = "";
                    Dataline.EMAIL = "";
                    Dataline.TELEF_REFERENCIA = "";
                    Dataline.CONTACTO_CLIENTE = "";
                    Dataline.TIPO_DOC = "";
                    Dataline.NRO_DOC = "";
                    Dataline.NOMBRE_COMERCIAL = "";
                    Dataline.FAX = "";
                    Dataline.CARGO = "";
                    Dataline.CUSTOMER_ID = "";
                    Dataline.CONTACTO_CLIENTE = "";
                    Dataline.TELEFONO_CONTACTO = "";
                    Dataline.CALLE_FAC = "";
                    Dataline.POSTAL_FAC = "";
                    Dataline.URBANIZACION_FAC = "";
                    Dataline.DEPARTEMENTO_FAC = "";
                    Dataline.PROVINCIA_FAC = "";
                    Dataline.DISTRITO_FAC = "";
                    Dataline.CALLE_LEGAL = "";
                    Dataline.POSTAL_LEGAL = "";
                    Dataline.URBANIZACION_LEGAL = "";
                    Dataline.DEPARTEMENTO_LEGAL = "";
                    Dataline.PROVINCIA_LEGAL = "";
                    Dataline.DISTRITO_LEGAL = "";
                    Dataline.PAIS_FAC = "";
                    Dataline.PAIS_LEGAL = "";
                    Dataline.REFERENCIA = "";
                    Dataline.CONTRATO_ID = "";
                    Dataline.LUGAR_NACIMIENTO_DES = "";
                    Dataline.LUGAR_NACIMIENTO_ID = "";
                    Dataline.FECHA_NAC = Claro.Utils.CheckDate("1/1/1");
                    Dataline.ESTADO_CIVIL = "";
                    Dataline.ESTADO_CIVIL_ID = "";
                    Dataline.NICHO = "";
                    strCodRetorno = "Error: No existe datos";
                }
            }
            else
            {
                strCodRetorno = "Error: Al ejecutar SQL - Oracle";
            }

           }
         catch (Exception ex)
         {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
         }
            return strCodRetorno;
        }

        /// <summary>
        /// obtiene cliente datos
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetClientDataAdd</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static ClientDataAddResponse GetClientDataAdd(ClientDataAddRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.strIdSession, objRequest.strTransaccion, "Transaction: Entra a Insert");
            

            DbParameter[] parameters =
            {               
                
                 new DbParameter("P_NRO_INTERACCION", DbType.String,255,ParameterDirection.Input,objRequest.vInteraccionID),
                 new DbParameter("p_objidcontact",DbType.String,255,ParameterDirection.Input,objRequest.v_ContactId),
                 new DbParameter("p_flag_modo", DbType.String,255,ParameterDirection.Input,objRequest.vFlagModo),
                 new DbParameter("FLAG_CONSULTA", DbType.String,255,ParameterDirection.Output ,objRequest.vFLAG_CONSULTA),
                 new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output,objRequest.vMSG_TEXT),
                 new DbParameter("OUT_CURSOR", DbType.Object,ParameterDirection.Output)
                
            };


            ClientDataAddResponse objResponse = new ClientDataAddResponse();
            try
            {


                Web.Logging.ExecuteMethod(objRequest.strIdSession, objRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteReader(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_CLARIFY_SIACSS_OBTERNER_DATOS_ADIC, parameters, reader =>
                        {
                            while (reader.Read())
                            {



                                objResponse.Cliente = new Client()
                                {


                                    PHONE1 = (reader["PHONE1"].ToString()),
                                    PHONE2 = (reader["PHONE2"].ToString()),
                                    EMAIL1 = (reader["E_MAIL1"].ToString()),
                                    EMAIL2 = (reader["E_MAIL2"].ToString()),
                                    COUNTRY_ID = (reader["COUNTRY"].ToString()),
                                    DEPARTAMENTO = (reader["DEPARTAMENTO"].ToString()),
                                    PROVINCIA = (reader["PROVINCIA"].ToString()),
                                    DISTRITO = (reader["DISTRITO"].ToString()),
                                    ADDRESS = (reader["ADDRESS"].ToString())


                                };
                                                               
                             }

                        });
                });

            }

            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            

            return objResponse;
        }

        //made13
        /// <summary>
        /// obtiene estado de equipo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="lsrtIMEI"></param>
        /// <param name="lstrNumeroLinea"></param>
        /// <returns></returns>
        /// <remarks>GetStateEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string GetStateEquipment(string strIdSession, string strTransaction, string lsrtIMEI, string lstrNumeroLinea)
        {
            DbParameter[] parameters =
            {               
                
                 new DbParameter("P_NUMERO_IMEI", DbType.String,255,ParameterDirection.Input,lsrtIMEI),
                 new DbParameter("P_NUMEROLINEA",DbType.String,255,ParameterDirection.Input,lstrNumeroLinea),
                 new DbParameter("P_ESTADO", DbType.String,255,ParameterDirection.Output)
									   
											   };


            string strEstado = "";


            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_IFI_SP_OBTENER_ESTADO, parameters);

            }
            catch (Exception ex)
            {

                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {

                strEstado = Claro.Utils.CheckStr(parameters[parameters.Length - 1].Value);

            };

            return strEstado;
        }
        /// <summary>
        /// inserta movimiento
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="lsrtIMEI"></param>
        /// <param name="lstrNumeroLinea"></param>
        /// <param name="Reportante"></param>
        /// <param name="Asesor_Servicio"></param>
        /// <param name="Marca"></param>
        /// <param name="Modelo"></param>
        /// <param name="Estado"></param>
        /// <param name="Motivo"></param>
        /// <returns></returns>
        /// <remarks>InsertMovement</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static int InsertMovement(string strIdSession, string strTransaction, string lsrtIMEI, string lstrNumeroLinea, string Reportante, string Asesor_Servicio, string Marca, string Modelo, string Estado, string Motivo)
        {

            DbParameter[] parameters = {
												    new DbParameter("P_CODIGO_GENERADO", DbType.Int32, ParameterDirection.Output), 
												    new DbParameter("P_NUMERO_IMEI", DbType.String,255, ParameterDirection.Input,lsrtIMEI), 
												    new DbParameter("P_NUMEROLINEA", DbType.String,255, ParameterDirection.Input,lstrNumeroLinea), 
												    new DbParameter("P_MARCA", DbType.String,255, ParameterDirection.Input,Marca),
												    new DbParameter("P_MODELO", DbType.String,255 ,ParameterDirection.Input,Modelo),
												    new DbParameter("P_REPORTANTE", DbType.String,255, ParameterDirection.Input,Reportante), 
												    new DbParameter("P_ASESOR_SERVICIO", DbType.String,255, ParameterDirection.Input,Asesor_Servicio), 
												    new DbParameter("P_ESTADO", DbType.String,255, ParameterDirection.Input,Estado),
												    new DbParameter("P_DES_MOTIVO", DbType.String,255, ParameterDirection.Input,Motivo),
												    new DbParameter("P_CO_ERROR", DbType.String,50, ParameterDirection.Output), 
												    new DbParameter("P_DES_ERROR", DbType.String,500, ParameterDirection.Output) 												   
											   };


            string CodigoBloqueo = "0";
            string P_CO_ERROR = "";
            string P_DES_ERROR = "";



            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_IFI_SP_REGISTRAR_MOVIMIENTO_2, parameters);
                CodigoBloqueo = Claro.Utils.CheckStr(parameters[0].Value);
            }
            catch (Exception ex)
            {
                 CodigoBloqueo = "0";
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                return Claro.Convert.ToInt(CodigoBloqueo);
            }

            finally
            {

                P_CO_ERROR = Claro.Utils.CheckStr(parameters[9].Value);
                P_DES_ERROR = Claro.Utils.CheckStr(parameters[10].Value);

            }

            return Claro.Convert.ToInt(CodigoBloqueo); ;
        }

        /// <summary>
        /// Actualiza estado de marca
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="lsrtIMEI"></param>
        /// <param name="lstrMarca"></param>
        /// <param name="lstrEstado"></param>
        ///  <remarks>UpdateStateBrand</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static void UpdateStateBrand(string strIdSession, string strTransaction, string lsrtIMEI, string lstrMarca, string lstrEstado)
        {
            int result;
            DbParameter[] parameters = {
												    new DbParameter("P_NUMERO_IMEI", DbType.String,255, ParameterDirection.Input,lsrtIMEI), 
												    new DbParameter("P_MARCA", DbType.String,255, ParameterDirection.Input,lstrMarca), 
												   new DbParameter("P_ESTADO", DbType.String,255, ParameterDirection.Input,lstrEstado)
											   };
            try
            {
                result = DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_IFI_SP_ACTUALIZAR_ESTADO_MARCA, parameters);


            }
            catch (Exception ex)
            {

                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }


        }


    }

}
