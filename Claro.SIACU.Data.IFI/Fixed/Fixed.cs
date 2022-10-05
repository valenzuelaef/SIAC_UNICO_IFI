using System;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using Claro.Data;
using Claro.SIACU.Data.IFI.Configuration;
using CSTS = Claro.Utils;
using EntitiesFixed = Claro.SIACU.Entity.IFI.Fixed;
using CUSTOMER_HFC = Claro.SIACU.ProxyService.IFI.SIACFixed.CustomerHFC;
using CUSTOMER_LTE = Claro.SIACU.ProxyService.IFI.SIACFixed.CustomerLTE;

using Claro.SIACU.ProxyService.IFI.TransaccionOCC;
using Claro.SIACU.Entity.IFI.Fixed.GetCaseInsert;
using Claro.SIACU.Entity.IFI.Fixed;
using Claro.SIACU.Entity.IFI.Fixed.GetConsultationServiceByContract;
using Claro.SIACU.Entity.IFI.Fixed.GetReconeService;



namespace Claro.SIACU.Data.IFI.Fixed
{
    public class Fixed
    {
        /// <summary>
        /// obtiene datos clientes
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vPhone"></param>
        /// <param name="vAccount"></param>
        /// <param name="vContactobjid1"></param>
        /// <param name="vFlagReg"></param>
        /// <param name="vFlagConsulta"></param>
        /// <param name="vMsgText"></param>
        /// <returns></returns>
        /// <remarks>GetCustomer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static EntitiesFixed.Customer GetCustomer(string strIdSession, string strTransaction, string vPhone, string vAccount, string vContactobjid1, string vFlagReg, ref  string vFlagConsulta, ref string vMsgText)
        {
            if (vContactobjid1 == "")
                vContactobjid1 = null;

            DbParameter[] parameters = {new DbParameter("P_PHONE", DbType.String,20,ParameterDirection.Input, vPhone),
                new DbParameter("P_ACCOUNT", DbType.String,80,ParameterDirection.Input, vAccount),
                new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input, vContactobjid1),
                new DbParameter("P_FLAG_REG", DbType.String,20,ParameterDirection.Input, vFlagReg),												
                new DbParameter("P_FLAG_CONSULTA", DbType.String,255,ParameterDirection.Output, vFlagConsulta),
                new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output, vMsgText),
                new DbParameter("CUSTOMER", DbType.Object, ParameterDirection.Output)
            };

            var objEntity = new EntitiesFixed.Customer();
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                objEntity = new EntitiesFixed.Customer
                                {
                                    OBJID_CONTACTO =
                                        (String)(Claro.Utils
                                            .DbValueToDefault<decimal>(reader[reader.GetOrdinal("OBJID_CONTACTO")])
                                            .ToString()),
                                    OBJID_SITE = (String)Claro.Utils.DbValueToDefault<decimal>(reader["OBJID_SITE"])
                                        .ToString(CultureInfo.InvariantCulture),
                                    TELEFONO = Claro.Utils.DbValueToDefault<string>(reader["TELEFONO"]),
                                    CUENTA = Claro.Utils.DbValueToDefault<string>(reader["CUENTA"]),
                                    MODALIDAD = Claro.Utils.DbValueToDefault<string>(reader["MODALIDAD"]),
                                    SEGMENTO = Claro.Utils.DbValueToDefault<string>(reader["SEGMENTO"]),
                                    ROL_CONTACTO = Claro.Utils.DbValueToDefault<string>(reader["ROL_CONTACTO"]),
                                    ESTADO_CONTACTO = Claro.Utils.DbValueToDefault<string>(reader["ESTADO_CONTACTO"]),
                                    ESTADO_CONTRATO = Claro.Utils.DbValueToDefault<string>(reader["ESTADO_CONTRATO"]),
                                    ESTADO_SITE = Claro.Utils.DbValueToDefault<string>(reader["ESTADO_SITE"]),
                                    S_NOMBRES = Claro.Utils.DbValueToDefault<string>(reader["S_NOMBRES"]),
                                    S_APELLIDOS = Claro.Utils.DbValueToDefault<string>(reader["S_APELLIDOS"]),
                                    NOMBRES = Claro.Utils.DbValueToDefault<string>(reader["NOMBRES"]),
                                    APELLIDOS = Claro.Utils.DbValueToDefault<string>(reader["APELLIDOS"]),
                                    DOMICILIO = Claro.Utils.DbValueToDefault<string>(reader["DOMICILIO"]),
                                    URBANIZACION = Claro.Utils.DbValueToDefault<string>(reader["URBANIZACION"]),
                                    REFERENCIA = Claro.Utils.DbValueToDefault<string>(reader["REFERENCIA"]),
                                    CIUDAD = Claro.Utils.DbValueToDefault<string>(reader["CIUDAD"]),
                                    DISTRITO = Claro.Utils.DbValueToDefault<string>(reader["DISTRITO"]),
                                    DEPARTAMENTO = Claro.Utils.DbValueToDefault<string>(reader["DEPARTAMENTO"]),
                                    ZIPCODE = Claro.Utils.DbValueToDefault<string>(reader["ZIPCODE"]),
                                    EMAIL = Claro.Utils.DbValueToDefault<string>(reader["EMAIL"]),
                                    TELEF_REFERENCIA = Claro.Utils.DbValueToDefault<string>(reader["TELEF_REFERENCIA"]),
                                    FAX = Claro.Utils.DbValueToDefault<string>(reader["FAX"]),
                                    FECHA_NAC =
                                        Claro.Utils.DbValueToDefault<DateTime>(reader["FECHA_NAC"]).ToShortDateString(),
                                    SEXO = Claro.Utils.DbValueToDefault<string>(reader["SEXO"]),
                                    ESTADO_CIVIL = Claro.Utils.DbValueToDefault<string>(reader["ESTADO_CIVIL"]),
                                    TIPO_DOC = Claro.Utils.DbValueToDefault<string>(reader["TIPO_DOC"]),
                                    NRO_DOC = Claro.Utils.DbValueToDefault<string>(reader["NRO_DOC"]),
                                    FECHA_ACT = Claro.Utils.DbValueToDefault<DateTime>(reader["FECHA_ACT"]),
                                    PUNTO_VENTA = Claro.Utils.DbValueToDefault<string>(reader["PUNTO_VENTA"]),
                                    FLAG_REGISTRADO =
                                        (int)Claro.Utils.DbValueToDefault<decimal>(reader["FLAG_REGISTRADO"]),
                                    OCUPACION = Claro.Utils.DbValueToDefault<string>(reader["OCUPACION"]),
                                    CANT_REG = (string)Claro.Utils.DbValueToDefault<decimal>(reader["CANT_REG"])
                                        .ToString(),
                                    FLAG_EMAIL = Claro.Utils.DbValueToDefault<string>(reader["FLAG_EMAIL"]),
                                    MOTIVO_REGISTRO = Claro.Utils.DbValueToDefault<string>(reader["MOTIVO_REGISTRO"]),
                                    FUNCION = Claro.Utils.DbValueToDefault<string>(reader["FUNCION"]),
                                    CARGO = Claro.Utils.DbValueToDefault<string>(reader["CARGO"]),
                                    LUGAR_NACIMIENTO_DES = Claro.Utils.DbValueToDefault<string>(reader["LUGAR_NAC"])
                                };
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                throw;
            }
            finally
            {
                vFlagConsulta = parameters[parameters.Length - 3].Value.ToString();
                vMsgText = parameters[parameters.Length - 2].Value.ToString();
            }

            return objEntity;
        }
        /// <summary>
        /// obtener clientes clarify
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vPhone"></param>
        /// <param name="vAccount"></param>
        /// <param name="vContactobjid1"></param>
        /// <param name="vFlagReg"></param>
        /// <param name="vFlagConsulta"></param>
        /// <param name="vMsgText"></param>
        /// <returns></returns>
        /// <remarks>GetCustomerClf</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static EntitiesFixed.Customer GetCustomerClf(string strIdSession, string strTransaction, string vPhone, string vAccount, string vContactobjid1, string vFlagReg, ref  string vFlagConsulta, ref string vMsgText)
        {
            if (vContactobjid1 == "")
                vContactobjid1 = null;

            DbParameter[] parameters = {new DbParameter("P_PHONE", DbType.String,20,ParameterDirection.Input, vPhone),
                new DbParameter("P_ACCOUNT", DbType.String,80,ParameterDirection.Input, vAccount),
                new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input, vContactobjid1),
                new DbParameter("P_FLAG_REG", DbType.String,20,ParameterDirection.Input, vFlagReg),												
                new DbParameter("P_FLAG_CONSULTA", DbType.String,255,ParameterDirection.Output, vFlagConsulta),
                new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output, vMsgText),
                new DbParameter("CUSTOMER", DbType.Object, ParameterDirection.Output)
            };

            var objEntity = new EntitiesFixed.Customer();
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                objEntity = new EntitiesFixed.Customer
                                {
                                    OBJID_CONTACTO =
                                        (String)(Claro.Utils
                                            .DbValueToDefault<decimal>(reader[reader.GetOrdinal("OBJID_CONTACTO")])
                                            .ToString()),
                                    OBJID_SITE = (String)Claro.Utils.DbValueToDefault<decimal>(reader["OBJID_SITE"])
                                        .ToString(CultureInfo.InvariantCulture),
                                    TELEFONO = Claro.Utils.DbValueToDefault<string>(reader["TELEFONO"]),
                                    CUENTA = Claro.Utils.DbValueToDefault<string>(reader["CUENTA"]),
                                    MODALIDAD = Claro.Utils.DbValueToDefault<string>(reader["MODALIDAD"]),
                                    SEGMENTO = Claro.Utils.DbValueToDefault<string>(reader["SEGMENTO"]),
                                    ROL_CONTACTO = Claro.Utils.DbValueToDefault<string>(reader["ROL_CONTACTO"]),
                                    ESTADO_CONTACTO = Claro.Utils.DbValueToDefault<string>(reader["ESTADO_CONTACTO"]),
                                    ESTADO_CONTRATO = Claro.Utils.DbValueToDefault<string>(reader["ESTADO_CONTRATO"]),
                                    ESTADO_SITE = Claro.Utils.DbValueToDefault<string>(reader["ESTADO_SITE"]),
                                    S_NOMBRES = Claro.Utils.DbValueToDefault<string>(reader["S_NOMBRES"]),
                                    S_APELLIDOS = Claro.Utils.DbValueToDefault<string>(reader["S_APELLIDOS"]),
                                    NOMBRES = Claro.Utils.DbValueToDefault<string>(reader["NOMBRES"]),
                                    APELLIDOS = Claro.Utils.DbValueToDefault<string>(reader["APELLIDOS"]),
                                    DOMICILIO = Claro.Utils.DbValueToDefault<string>(reader["DOMICILIO"]),
                                    URBANIZACION = Claro.Utils.DbValueToDefault<string>(reader["URBANIZACION"]),
                                    REFERENCIA = Claro.Utils.DbValueToDefault<string>(reader["REFERENCIA"]),
                                    CIUDAD = Claro.Utils.DbValueToDefault<string>(reader["CIUDAD"]),
                                    DISTRITO = Claro.Utils.DbValueToDefault<string>(reader["DISTRITO"]),
                                    DEPARTAMENTO = Claro.Utils.DbValueToDefault<string>(reader["DEPARTAMENTO"]),
                                    ZIPCODE = Claro.Utils.DbValueToDefault<string>(reader["ZIPCODE"]),
                                    EMAIL = Claro.Utils.DbValueToDefault<string>(reader["EMAIL"]),
                                    TELEF_REFERENCIA = Claro.Utils.DbValueToDefault<string>(reader["TELEF_REFERENCIA"]),
                                    FAX = Claro.Utils.DbValueToDefault<string>(reader["FAX"]),
                                    FECHA_NAC =
                                        Claro.Utils.DbValueToDefault<DateTime>(reader["FECHA_NAC"]).ToShortDateString(),
                                    SEXO = Claro.Utils.DbValueToDefault<string>(reader["SEXO"]),
                                    ESTADO_CIVIL = Claro.Utils.DbValueToDefault<string>(reader["ESTADO_CIVIL"]),
                                    TIPO_DOC = Claro.Utils.DbValueToDefault<string>(reader["TIPO_DOC"]),
                                    NRO_DOC = Claro.Utils.DbValueToDefault<string>(reader["NRO_DOC"]),
                                    FECHA_ACT = Claro.Utils.DbValueToDefault<DateTime>(reader["FECHA_ACT"]),
                                    PUNTO_VENTA = Claro.Utils.DbValueToDefault<string>(reader["PUNTO_VENTA"]),
                                    FLAG_REGISTRADO =
                                        (int)Claro.Utils.DbValueToDefault<decimal>(reader["FLAG_REGISTRADO"]),
                                    OCUPACION = Claro.Utils.DbValueToDefault<string>(reader["OCUPACION"]),
                                    CANT_REG = (string)Claro.Utils.DbValueToDefault<decimal>(reader["CANT_REG"])
                                        .ToString(),
                                    FLAG_EMAIL = Claro.Utils.DbValueToDefault<string>(reader["FLAG_EMAIL"]),
                                    MOTIVO_REGISTRO = Claro.Utils.DbValueToDefault<string>(reader["MOTIVO_REGISTRO"]),
                                    FUNCION = Claro.Utils.DbValueToDefault<string>(reader["FUNCION"]),
                                    CARGO = Claro.Utils.DbValueToDefault<string>(reader["CARGO"]),
                                    LUGAR_NACIMIENTO_DES = Claro.Utils.DbValueToDefault<string>(reader["LUGAR_NAC"])
                                };
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                throw;
            }
            finally
            {
                vFlagConsulta = parameters[parameters.Length - 3].Value.ToString();
                vMsgText = parameters[parameters.Length - 2].Value.ToString();
            }

            return objEntity;
        }


        /// <summary>
        /// Método que obtiene una lista con los datos del teléfono  por código de contrato HFC.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos Service con información del servicio.</returns>
        /// <remarks>GetTelephoneByContractCodeHFC</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Claro.SIACU.Entity.IFI.Fixed.Service> GetTelephoneByContractCodeHFC(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strContractId)
        {
            List<Claro.SIACU.Entity.IFI.Fixed.Service> lstService = new List<Claro.SIACU.Entity.IFI.Fixed.Service>();

            CUSTOMER_HFC.consultarListaTelefonoPorCodigoContratoEAIResponse oConsultationListOut =
                 Claro.Web.Logging.ExecuteMethod<CUSTOMER_HFC.consultarListaTelefonoPorCodigoContratoEAIResponse>
            (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.FIXED_CUSTOMER_HFC, () =>
            {
                return Configuration.ServiceConfiguration.FIXED_CUSTOMER_HFC.consultarListaTelefonoPorCodigoContrato(new CUSTOMER_HFC.consultarListaTelefonoPorCodigoContratoEAIRequest()
                {
                    consultarListaTelefonoPorCodigoContratoEaiRequest = new CUSTOMER_HFC.ConsultarListaTelefonoPorCodigoContratoEAIInput()
                    {
                        cabeceraRequest = new CUSTOMER_HFC.CabeceraRequest()
                        {
                            idTransaccion = strIdTransaction,
                            ipAplicacion = strIpApplication,
                            nombreAplicacion = strApplicationName,
                            usuarioAplicacion = strUserApplication
                        },
                        cuerpoRequest = new CUSTOMER_HFC.CuerpoCLTCRequest()
                        {
                            codigoContrato = strContractId
                        }
                    }
                });

            });

            CUSTOMER_HFC.CabeceraResponse oHeaderOutput = oConsultationListOut.consultarListaTelefonoPorCodigoContratoEaiResponse.cabeceraResponse;
            CUSTOMER_HFC.TelefonoPorCodigoContratoType[] oTempCustomer = oConsultationListOut.consultarListaTelefonoPorCodigoContratoEaiResponse.cuerpoResponse.listaTelefonoPorCodigoContrato;

            if (oHeaderOutput.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                for (int i = 0; i < oTempCustomer.Length; i++)
                {
                    Claro.SIACU.Entity.IFI.Fixed.Service objService = new Claro.SIACU.Entity.IFI.Fixed.Service();
                    objService.NRO_CELULAR = Convert.ToString(oTempCustomer[i].dnNum);
                    objService.ESTADO_LINEA = Convert.ToString(oTempCustomer[i].estadoLinea);
                    lstService.Add(objService);
                }
            }

            return lstService;
        }
        /// <summary>
        /// Método que obtiene una lista con la consulta de teléfono por código de contrato LTE.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos Service con información de la misma.</returns>
        /// <remarks>GetTelephoneByContractCodeLTE</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Claro.SIACU.Entity.IFI.Fixed.Service> GetTelephoneByContractCodeLTE(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strContractId)
        {
            List<Claro.SIACU.Entity.IFI.Fixed.Service> lstService = new List<Claro.SIACU.Entity.IFI.Fixed.Service>();
            CUSTOMER_LTE.consultarListaTelefonoPorCodigoContratoEAIResponse oConsultationListOut =
               Claro.Web.Logging.ExecuteMethod<CUSTOMER_LTE.consultarListaTelefonoPorCodigoContratoEAIResponse>
           (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.FIXED_CUSTOMER_LTE, () =>
           {
               return Configuration.ServiceConfiguration.FIXED_CUSTOMER_LTE.consultarListaTelefonoPorCodigoContrato(new CUSTOMER_LTE.consultarListaTelefonoPorCodigoContratoEAIRequest()
               {
                   codigoContrato = strContractId,
                   auditRequest = new CUSTOMER_LTE.AuditRequestType()
                   {
                       idTransaccion = strIdTransaction,
                       ipAplicacion = strIpApplication,
                       nombreAplicacion = strApplicationName,
                       usuarioAplicacion = strUserApplication
                   }
               });
           });

            CUSTOMER_LTE.AuditResponseType objAuditResponse = oConsultationListOut.auditResponse;
            CUSTOMER_LTE.TelefonoPorCodigoContratoType[] oTempCustomer = oConsultationListOut.listaTelefonoPorCodigoContrato;

            if (objAuditResponse.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                for (int i = 0; i < oTempCustomer.Length; i++)
                {
                    Claro.SIACU.Entity.IFI.Fixed.Service objService = new Claro.SIACU.Entity.IFI.Fixed.Service();
                    objService.NRO_CELULAR = Convert.ToString(oTempCustomer[i].dnNum);
                    objService.ESTADO_LINEA = Convert.ToString(oTempCustomer[i].estadoLinea);
                    lstService.Add(objService);
                }
            }
            return lstService;
        }



        #region "Inst/Desinst Decodificadores"
        /// <summary>
        /// obetenr tipos de trabajos
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vintTipoTransaccion"></param>
        /// <returns></returns>
        /// <remarks>GetJobTypes</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<EntitiesFixed.JobType> GetJobTypes(string strIdSession, string strTransaction, int vintTipoTransaccion)
        {
            List<Entity.IFI.Fixed.JobType> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_tipo", DbType.Int32,22, ParameterDirection.Input, vintTipoTransaccion),
            new DbParameter("srv_tipra", DbType.Object, ParameterDirection.Output)
            };

            list = DbFactory.ExecuteReader<List<Entity.IFI.Fixed.JobType>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_CONSULTA_TIPTRA, parameters);

            return list;
        }
        /// <summary>
        /// valida flujo ETA
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="as_origen"></param>
        /// <param name="av_idplano"></param>
        /// <param name="av_ubigeo"></param>
        /// <param name="an_tiptra"></param>
        /// <param name="an_tipsrv"></param>
        /// <returns></returns>
        /// <remarks>ETAFlowValidate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static EntitiesFixed.ETAFlow ETAFlowValidate(string strIdSession, string strTransaction, string as_origen, string av_idplano, string av_ubigeo, int an_tiptra,
                                     string an_tipsrv)
        {

            string as_codzona = "";
            int an_indica = 1;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("as_origen", DbType.String, ParameterDirection.Input,as_origen),
                new DbParameter("av_idplano", DbType.String,255, ParameterDirection.Input, av_idplano),
                new DbParameter("av_ubigeo", DbType.String,255, ParameterDirection.Input, av_ubigeo),
                new DbParameter("an_tiptra", DbType.Int32,20, ParameterDirection.Input, an_tiptra),
                new DbParameter("an_tipsrv", DbType.String,255, ParameterDirection.Input, an_tipsrv),
                new DbParameter("as_codzona", DbType.String,255, ParameterDirection.Output,as_codzona),
                new DbParameter("an_indica", DbType.Int32,20, ParameterDirection.Output,an_indica)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_VALIDA_FLUJO_ZONA_ADC, parameters);

            return new EntitiesFixed.ETAFlow
            {
                an_indica = Convert.ToInt(parameters[6].Value.ToString()),
                as_codzona = Convert.ToString(parameters[5].Value)
            };
        }
        /// <summary>
        /// obtener tipos de ordenes
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vintTipoTra"></param>
        /// <returns></returns>
        /// <remarks>GetOrderType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Fixed.OrderType> GetOrderType(string strIdSession, string strTransaction, string vintTipoTra)
        {
            List<Entity.IFI.Fixed.OrderType> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("vIdtiptra", DbType.String,255, ParameterDirection.Input, vintTipoTra),
                                new DbParameter("cur_tipo", DbType.Object, ParameterDirection.Output)
            };

            list = DbFactory.ExecuteReader<List<Entity.IFI.Fixed.OrderType>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_OBTIENE_TIPO_ORDEN_TIPTRA, parameters);

            return list;
        }
        /// <summary>
        /// obtener subtipos de ordenes
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vintTipoOrden"></param>
        /// <returns></returns>
        /// <remarks>GetOrderSubType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Fixed.OrderSubType> GetOrderSubType(string strIdSession, string strTransaction, string vintTipoOrden)
        {
            List<Entity.IFI.Fixed.OrderSubType> list = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("av_cod_tipo_orden", DbType.String,255, ParameterDirection.Input, vintTipoOrden),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
             
            };

            list = DbFactory.ExecuteReader<List<Entity.IFI.Fixed.OrderSubType>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_CONSULTA_SUBTIPORD, parameters);

            return list;
        }
        /// <summary>
        /// inserta detalle de servicio interaccion
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="codinterac"></param>
        /// <param name="nombreserv"></param>
        /// <param name="tiposerv"></param>
        /// <param name="gruposerv"></param>
        /// <param name="cf"></param>
        /// <param name="equipo"></param>
        /// <param name="cantidad"></param>
        /// <param name="resultado"></param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        /// <remarks>GetInsertDetailServiceInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetInsertDetailServiceInteraction(string strIdSession, string strTransaction, string codinterac,
                                                             string nombreserv, string tiposerv, string gruposerv, string cf, string equipo, string cantidad,
                                                             ref string resultado, ref string mensaje)
        {
            bool strRet = false;

            DbParameter[] parameters = {
				new DbParameter("P_COD_INTER",DbType.String ,ParameterDirection.Input, codinterac),
                new DbParameter("P_NOM_SERV",DbType.String ,ParameterDirection.Input, nombreserv),
                new DbParameter("P_TIP_SERV",DbType.String ,ParameterDirection.Input, tiposerv),
                new DbParameter("P_GRUP_SERV",DbType.String ,ParameterDirection.Input, gruposerv),
                new DbParameter("P_CF",DbType.String ,ParameterDirection.Input, cf),
                new DbParameter("P_EQUIPO",DbType.String ,ParameterDirection.Input, equipo),
                new DbParameter("P_CANTIDAD",DbType.String ,ParameterDirection.Input, cantidad),
				new DbParameter("FLAG_RESULTADO",DbType.Int64,ParameterDirection.Output),
                new DbParameter("MSG_TEXT",DbType.String,255 ,ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_INS_INTER_SERV_MP, parameters);
                });
                strRet = true;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                resultado = CSTS.CheckStr(parameters[parameters.Length - 2].Value);
                mensaje = CSTS.CheckStr(parameters[parameters.Length - 1].Value);
            }

            return strRet;
        }
        /// <summary>
        /// inserta transaccion
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objTransfer"></param>
        /// <param name="rstrResCod"></param>
        /// <param name="rstrResDes"></param>
        /// <returns></returns>
        /// <remarks>GetInsertTransaction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string GetInsertTransaction(string strIdSession, string strTransaction, EntitiesFixed.Transfer objTransfer,
                                                  ref string rstrResCod, ref string rstrResDes)
        {
            string intNumSot = string.Empty;
            if (objTransfer != null)
            {
                var json_object = Newtonsoft.Json.JsonConvert.SerializeObject(objTransfer);
                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Begin a GetInsertTransaction Capa Data --> Parametros de entrada objTransfer: " + json_object); // Temporal


                if (objTransfer.InterCasoID == null)
                    objTransfer.InterCasoID = Claro.Constants.NumberZeroString;
                if (objTransfer.InterCasoID == string.Empty)
                    objTransfer.InterCasoID = Claro.Constants.NumberZeroString;

                if (objTransfer.NumCarta == null)
                    objTransfer.NumCarta = Claro.Constants.NumberZeroString;
                if (objTransfer.NumCarta == string.Empty)
                    objTransfer.NumCarta = Claro.Constants.NumberZeroString;

                DbParameter[] parameters = {
                new DbParameter("p_id", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.InterCasoID)),
                new DbParameter("v_cod_id", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.ConID)),
                new DbParameter("v_customer_id", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.CustomerID)),
                new DbParameter("v_tipotrans", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.TransTipo)),
                new DbParameter("v_codintercaso", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.InterCasoID)),
                new DbParameter("v_tipovia", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.TipoVia)),
                new DbParameter("v_nombrevia", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.NomVia)),
                new DbParameter("v_numerovia", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.NroVia)),
                new DbParameter("v_tipourbanizacion", DbType.Int32, ParameterDirection.Input, CSTS.CheckInt(objTransfer.TipoUrb)),
                new DbParameter("v_nombreurbanizacion", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.NomUrb)),
                new DbParameter("v_manzana", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.NumMZ)),
                new DbParameter("v_lote", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.NumLote)),
                new DbParameter("v_codubigeo", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.Ubigeo)),
                new DbParameter("v_codzona", DbType.Int32, ParameterDirection.Input, CSTS.CheckInt(objTransfer.ZonaID)),
                new DbParameter("v_idplano", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.PlanoID)),
                new DbParameter("v_codedif", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.EdificioID)),
                new DbParameter("v_referencia", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.Referencia)),
                new DbParameter("v_observacion", DbType.String, 4000, ParameterDirection.Input, CSTS.CheckStr(objTransfer.Observacion)),
                new DbParameter("v_fec_prog", DbType.Date, ParameterDirection.Input, CSTS.CheckDate( objTransfer.FechaProgramada)),
                new DbParameter("v_franja_horaria", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.FranjaHora)),
                new DbParameter("v_numcarta", DbType.Double, ParameterDirection.Input, CSTS.CheckDbl(objTransfer.NumCarta)),
                new DbParameter("v_operador", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.Operador)),
                new DbParameter("v_presuscrito", DbType.Int32, ParameterDirection.Input, CSTS.CheckInt(objTransfer.Presuscrito)),
                new DbParameter("v_publicar", DbType.Double, ParameterDirection.Input, CSTS.CheckDbl(objTransfer.Publicar)),
                new DbParameter("v_ad_tmcode", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.TmCode)),
                new DbParameter("v_lista_coser", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.ListaCoSer)),
                new DbParameter("v_lista_spcode", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.ListaSPCode)),
                new DbParameter("v_usuarioreg", DbType.String, 255, ParameterDirection.Input, CSTS.CheckStr(objTransfer.USRREGIS)),
                new DbParameter("v_cargo", DbType.Double, ParameterDirection.Input, CSTS.CheckDbl(objTransfer.Cargo)),
                new DbParameter("v_codsolot", DbType.Int64, ParameterDirection.Output),
                new DbParameter("p_error_code", DbType.Int64, ParameterDirection.Output),
                new DbParameter("p_error_msg", DbType.String, 500, ParameterDirection.Output)
            };

                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, string.Format("BELogNroVia - {0}", objTransfer.NroVia));
                try
                {
                    Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                    {
                        DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SGA_P_GENERA_TRANSACCION, parameters);
                    });
                }
                catch (Exception ex)
                {
                    Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                }
                finally
                {
                    intNumSot = CSTS.CheckStr(parameters[parameters.Length - 3].Value);
                    rstrResCod = CSTS.CheckStr(parameters[parameters.Length - 2].Value);
                    rstrResDes = CSTS.CheckStr(parameters[parameters.Length - 1].Value);
                }

                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Fin a GetInsertTransaction Capa Data --> Parametros de salida intNumSot: " + intNumSot); // Temporal
                return CSTS.CheckStr(intNumSot);
            }
            else
            {
                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Begin a GetInsertTransaction Capa Data --> Parametros de entrada objTransfer: null"); // Temporal
                return (intNumSot);
            }



        }
        /// <summary>
        /// obtiene servicios por interaccion
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="idInteraccion"></param>
        /// <returns></returns>
        /// <remarks>GetServicesByInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Fixed.ServiceByInteraction> GetServicesByInteraction(string strIdSession, string strTransaction, string idInteraccion)
        {
            List<Entity.IFI.Fixed.ServiceByInteraction> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CODINTERAC", DbType.String,255, ParameterDirection.Input, idInteraccion),
                new DbParameter("CURSOR_SALIDA", DbType.Object, ParameterDirection.Output)                
            };

            list = DbFactory.ExecuteReader<List<Entity.IFI.Fixed.ServiceByInteraction>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_QUERY_INTER_SERV_MP, parameters);

            return list;
        }
        #endregion
        /// <summary>
        /// crea casos
        /// </summary>
        /// <param name="oRequest"></param>
        /// <returns></returns>
        /// <remarks>GetCreateCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static Interaction GetCreateCase(Interaction oRequest)
        {
            Interaction oResponse = new Interaction();
            //InsertTemplateInteraction asdassa=new InsertTemplateInteraction() 
            try
            {

                DbParameter[] parameters =
                {   
                   #region Parametros InsertCaso
                                        new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input,oRequest.OBJID_CONTACTO),//"5628895"
				                        new DbParameter("P_SITEOBJID_1", DbType.Int64,ParameterDirection.Input,oRequest.OBJID_SITE),// "5628895"),
				                        new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input,oRequest.CUENTA),
				                        new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input,oRequest.TELEFONO),
				                        new DbParameter("P_TIPO", DbType.String,255,ParameterDirection.Input,oRequest.TIPIFICACION),
				                        new DbParameter("P_CLASE", DbType.String,255,ParameterDirection.Input,oRequest.CLASE),
				                        new DbParameter("P_SUBCLASE", DbType.String,255,ParameterDirection.Input,oRequest.SUBCLASE),
				                        new DbParameter("P_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input,oRequest.METODO),
				                        new DbParameter("P_PRIORIDAD", DbType.String,255,ParameterDirection.Input,oRequest.PRIORIDAD),
				                        new DbParameter("P_SEVERIDAD", DbType.String,255,ParameterDirection.Input,oRequest.SEVERIDAD),
				                        new DbParameter("P_COLA", DbType.String,255,ParameterDirection.Input,oRequest.COLA),
				                        new DbParameter("P_FLAG_INTERACT", DbType.String,255,ParameterDirection.Input,oRequest.FLAG_INTERACCION),
				                        new DbParameter("P_USR_PROCESO", DbType.String,255,ParameterDirection.Input,oRequest.USUARIO_PROCESO),
				                        new DbParameter("P_USUARIO", DbType.String,255,ParameterDirection.Input,oRequest.USUARIO_ID),
				                        new DbParameter("P_TIPO_INTER", DbType.String,255,ParameterDirection.Input,oRequest.TIPO_INTERACCION),
				                        new DbParameter("P_NOTAS", DbType.String,4000,ParameterDirection.Input,oRequest.NOTAS),
				                        new DbParameter("ID_CASO", DbType.String,255,ParameterDirection.Output,oResponse.CASO_ID),
				                        new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output,oResponse.FLAG_INSERCION),
				                        new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output,oResponse.RESULTADO),	
    #endregion
                };
                Web.Logging.ExecuteMethod(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(oRequest.Audit.Session, oRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_CLARIFY_CREATE_CASE, parameters);

                }
                )
                ;
                oResponse.CASO_ID = parameters[16].Value.ToString();
                oResponse.FLAG_INSERCION = parameters[17].Value.ToString();
                oResponse.RESULTADO = parameters[18].Value.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, ex.Message);
            }


            return oResponse;
        }
        /// <summary>
        /// insertar caso
        /// </summary>
        /// <param name="oItem"></param>
        /// <returns></returns>
        /// <remarks>GetInsertCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static EntitiesFixed.Interaction GetInsertCase(EntitiesFixed.Interaction oItem)
        {
            Interaction prueba = new Interaction();
            string FLAG_CREACION = string.Empty;
            string Message = string.Empty;
            string CasoId = string.Empty;

            EntitiesFixed.Interaction oResponse = new EntitiesFixed.Interaction();
            DbParameter[] parameters =
            {
												   new DbParameter("PN_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input,oItem.OBJID_CONTACTO),
												   new DbParameter("PV_SITEOBJID_1", DbType.Int64,ParameterDirection.Input,oItem.OBJID_SITE),
												   new DbParameter("PV_ACCOUNT", DbType.String,255,ParameterDirection.Input,oItem.CUENTA),
												   new DbParameter("PV_PHONE", DbType.String,255,ParameterDirection.Input,oItem.TELEFONO),
												   new DbParameter("PV_TIPO", DbType.String,255,ParameterDirection.Input,oItem.TIPO),
												   new DbParameter("PV_CLASE", DbType.String,255,ParameterDirection.Input,oItem.CLASE),
												   new DbParameter("PV_SUBCLASE", DbType.String,255,ParameterDirection.Input,oItem.SUBCLASE),
												   new DbParameter("PV_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input,oItem.METODO),
												   new DbParameter("PV_PRIORIDAD", DbType.String,255,ParameterDirection.Input,oItem.PRIORIDAD),
												   new DbParameter("PV_SEVERIDAD", DbType.String,255,ParameterDirection.Input,oItem.SEVERIDAD),
												   new DbParameter("PV_COLA", DbType.String,255,ParameterDirection.Input,oItem.COLA),
												   new DbParameter("PV_FLAG_INTERACT", DbType.String,255,ParameterDirection.Input,oItem.FLAG_INTERACCION),
												   new DbParameter("PV_USR_PROCESO", DbType.String,255,ParameterDirection.Input,oItem.USUARIO_PROCESO),
												   new DbParameter("PV_USUARIO", DbType.String,255,ParameterDirection.Input,oItem.USUARIO_ID),
												   new DbParameter("PV_TIPO_INTER", DbType.String,255,ParameterDirection.Input,oItem.TIPO_INTERACCION),
												   new DbParameter("PV_NOTAS", DbType.String,4000,ParameterDirection.Input,oItem.NOTAS),
												   new DbParameter("ID_CASO", DbType.String,255,ParameterDirection.Output,CasoId),
												   new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output,FLAG_CREACION),
												   new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output,Message)	
											   };



            try
            {
                Web.Logging.ExecuteMethod(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(oItem.Audit.Session, oItem.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_DB,
                        DbCommandConfiguration.SIACU_POST_COBS_INSERTAR_CASE, parameters);

                });
                oResponse.CASO_ID = parameters[16].Value.ToString();
                oResponse.FLAG_INSERCION = parameters[17].Value.ToString();
                oResponse.RESULTADO = parameters[18].Value.ToString();



            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }

            return oResponse;

        }
        /// <summary>
        /// insertar plantilla de caso
        /// </summary>
        /// <param name="oItem"></param>
        /// <returns></returns>
        /// <remarks>GetInsertTemplateCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static EntitiesFixed.CaseTemplate GetInsertTemplateCase(EntitiesFixed.CaseTemplate oItem)
        {

            EntitiesFixed.CaseTemplate oResponse = new EntitiesFixed.CaseTemplate();


            string rFlagInsercion = string.Empty;
            DbParameter[] parameters =
            
            {
                   #region Imput
                    new DbParameter("P_NRO_CASO",DbType.String,80,ParameterDirection.Input,oItem.ID_CASO),
                    new DbParameter("P_CAS_1",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_1),
                    new DbParameter("P_CAS_2",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_2),
                    new DbParameter("P_CAS_3",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_3),
                    new DbParameter("P_CAS_4",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_4),
                    new DbParameter("P_CAS_5",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_5),
                    new DbParameter("P_CAS_6",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_6),
                    new DbParameter("P_CAS_7",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_7),
                    new DbParameter("P_CAS_8",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_8),
                    new DbParameter("P_CAS_9",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_9),
                    new DbParameter("P_CAS_10",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_10),
                    new DbParameter("P_CAS_11",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_11),
                    new DbParameter("P_CAS_12",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_12),
                    new DbParameter("P_CAS_13",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_13),
                    new DbParameter("P_CAS_14",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_14),
                    new DbParameter("P_CAS_15",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_15),
                    new DbParameter("P_CAS_16",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_16),
                    new DbParameter("P_CAS_17",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_17),
                    new DbParameter("P_CAS_18",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_18),
                    new DbParameter("P_CAS_19",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_19),
                    new DbParameter("P_CAS_20",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_20),
                    new DbParameter("P_CAS_21",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_21),
                    new DbParameter("P_CAS_22",DbType.Double,ParameterDirection.Input,oItem.X_CAS_22),
                    new DbParameter("P_CAS_23",DbType.Double,ParameterDirection.Input,oItem.X_CAS_23),
                    new DbParameter("P_CAS_24",DbType.Double,ParameterDirection.Input,oItem.X_CAS_24),
                    new DbParameter("P_CAS_25",DbType.Double,ParameterDirection.Input,oItem.X_CAS_25),
                    new DbParameter("P_CAS_26",DbType.Double,ParameterDirection.Input,oItem.X_CAS_26),
                    new DbParameter("P_CAS_27",DbType.Double,ParameterDirection.Input,oItem.X_CAS_27),
                    new DbParameter("P_CAS_28",DbType.Double,ParameterDirection.Input,oItem.X_CAS_28),
                    new DbParameter("P_CAS_29",DbType.String,255,ParameterDirection.Input,oItem.X_CAS_29),
                    new DbParameter("P_CAS_30",DbType.String,255,ParameterDirection.Input,oItem.X_CAS_30),
                    new DbParameter("P_SUSPENSION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_SUSPENSION_DATE),
                    new DbParameter("P_REACTIVATION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_REACTIVATION_DATE),
                    new DbParameter("P_SUSPENSION_QT",DbType.Double,ParameterDirection.Input,oItem.X_SUSPENSION_QT),
                    new DbParameter("P_CONCLUSIONS",DbType.String,255,ParameterDirection.Input,oItem.X_CONCLUSIONS),
                    new DbParameter("P_TEST_MADE",DbType.String,255,ParameterDirection.Input,oItem.X_TEST_MADE),
                    new DbParameter("P_PROBLEM_DESCRIPTION",DbType.String,255,ParameterDirection.Input,oItem.X_PROBLEM_DESCRIPTION),
                    new DbParameter("P_ADDRESS",DbType.String,255,ParameterDirection.Input,oItem.X_ADDRESS),
                    new DbParameter("P_DOCUMENT_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_DOCUMENT_NUMBER),
                    new DbParameter("P_CALL_DURATION",DbType.String,50,ParameterDirection.Input,oItem.X_CALL_DURATION),
                    new DbParameter("P_CALL_COST",DbType.Double,ParameterDirection.Input,oItem.X_CALL_COST),
                    new DbParameter("P_SYSTEM_STATUS",DbType.String,80,ParameterDirection.Input,oItem.X_SYSTEM_STATUS),
                    new DbParameter("P_FLAG_VARIATION",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_VARIATION),
                    new DbParameter("P_SEARCH_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_SEARCH_DATE),
                    new DbParameter("P_VARIATION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_VARIATION_DATE),
                    new DbParameter("P_LAST_QUERY",DbType.DateTime,ParameterDirection.Input,oItem.X_LAST_QUERY),
                    new DbParameter("P_PROBLEM_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_PROBLEM_DATE),
                    new DbParameter("P_PURCHASE_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_PURCHASE_DATE),
                    new DbParameter("P_RECHARGE_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_RECHARGE_DATE),
                    new DbParameter("P_REQUEST_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_REQUEST_DATE),
                    new DbParameter("P_REQUEST_PLACE",DbType.String,80,ParameterDirection.Input,oItem.X_REQUEST_PLACE),
                    new DbParameter("P_FLAG_GPRS",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_GPRS),
                    new DbParameter("P_COMPLAINT_AMOUNT",DbType.Double,ParameterDirection.Input,oItem.X_COMPLAINT_AMOUNT),
                    new DbParameter("P_LINES",DbType.String,20,ParameterDirection.Input,oItem.X_LINES),
                    new DbParameter("P_ERROR_MESSAGE",DbType.String,100,ParameterDirection.Input,oItem.X_ERROR_MESSAGE),
                    new DbParameter("P_MODEL",DbType.String,50,ParameterDirection.Input,oItem.X_MODEL),
                    new DbParameter("P_MARK",DbType.String,50,ParameterDirection.Input,oItem.X_MARK),
                    new DbParameter("P_BAND",DbType.String,50,ParameterDirection.Input,oItem.X_BAND),
                    new DbParameter("P_REPOSITION_REASON",DbType.String,80,ParameterDirection.Input,oItem.X_REPOSITION_REASON),
                    new DbParameter("P_CHURN_REASON",DbType.String,80,ParameterDirection.Input,oItem.X_CHURN_REASON),
                    new DbParameter("P_CELLULAR_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_CELLULAR_NUMBER),
                    new DbParameter("P_CLARIFY_VARIATION",DbType.String,15,ParameterDirection.Input,oItem.X_CLARIFY_VARIATION),
                    new DbParameter("P_FLAG_SEND_RECEIVE",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_SEND_RECEIVE),
                    new DbParameter("P_CUSTOMER_NAME",DbType.String,80,ParameterDirection.Input,oItem.X_CUSTOMER_NAME),
                    new DbParameter("P_PREPAID_CARD_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_PREPAID_CARD_NUMBER),
                    new DbParameter("P_NUMBERS_COMMUNICATION",DbType.String,80,ParameterDirection.Input,oItem.X_NUMBERS_COMMUNICATION),
                    new DbParameter("P_REFERENCE_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_REFERENCE_NUMBER),
                    new DbParameter("P_OST_NUMBER",DbType.String,30,ParameterDirection.Input,oItem.X_OST_NUMBER),
                    new DbParameter("P_FRIENDS_FAMILY",DbType.String,80,ParameterDirection.Input,oItem.X_FRIENDS_FAMILY),
                    new DbParameter("P_COUNTRY_OPERATOR",DbType.String,80,ParameterDirection.Input,oItem.X_COUNTRY_OPERATOR),
                    new DbParameter("P_OLD_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_OLD_PLAN),
                    new DbParameter("P_NEW_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_NEW_PLAN),
                    new DbParameter("P_CURRENT_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_CURRENT_PLAN),
                    new DbParameter("P_CAMPAIGN",DbType.String,80,ParameterDirection.Input,oItem.X_CAMPAIGN),
                    new DbParameter("P_BILL_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_BILL_NUMBER_COMPLAINT),
                    new DbParameter("P_REFERENCE_ADDRESS",DbType.String,100,ParameterDirection.Input,oItem.X_REFERENCE_ADDRESS),
                    new DbParameter("P_CURRENT_BALANCE",DbType.Double,ParameterDirection.Input,oItem.X_CURRENT_BALANCE),
                    new DbParameter("P_LAST_BALANCE",DbType.Double,ParameterDirection.Input,oItem.X_LAST_BALANCE),
                    new DbParameter("P_BALANCE_REQUESTED",DbType.Double,ParameterDirection.Input,oItem.X_BALANCE_REQUESTED),
                    new DbParameter("P_CUSTOMER_SEGMENT",DbType.String,30,ParameterDirection.Input,oItem.X_CUSTOMER_SEGMENT),
                    new DbParameter("P_SERVICE_PROBLEM",DbType.String,50,ParameterDirection.Input,oItem.X_SERVICE_PROBLEM),
                    new DbParameter("P_FLAG_OTHER_PROBLEMS",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_OTHER_PROBLEMS),
                    new DbParameter("P_OPERATOR_PROBLEM",DbType.String,80,ParameterDirection.Input,oItem.X_OPERATOR_PROBLEM),
                    new DbParameter("P_CONTACT_TIME_TERM",DbType.String,80,ParameterDirection.Input,oItem.X_CONTACT_TIME_TERM),
                    new DbParameter("P_STORE",DbType.String,80,ParameterDirection.Input,oItem.X_STORE),
                    new DbParameter("P_DIAL_TYPE",DbType.String,10,ParameterDirection.Input,oItem.X_DIAL_TYPE),
                    new DbParameter("P_PROBLEM_LOCATION",DbType.String,255,ParameterDirection.Input,oItem.X_PROBLEM_LOCATION),
                    new DbParameter("P_FLAG_ADDITIONAL_SERVICES",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_ADDITIONAL_SERVICES),
                    new DbParameter("P_FLAG_WAP",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_WAP),
                    new DbParameter("P_CLARO_LDN1",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN1),
                    new DbParameter("P_CLARO_LDN2",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN2),
                    new DbParameter("P_CLARO_LDN3",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN3),
                    new DbParameter("P_CLARO_LDN4",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN4),
                    new DbParameter("P_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL1),
                    new DbParameter("P_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL2),
                    new DbParameter("P_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL3),
                    new DbParameter("P_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL4),
                    new DbParameter("P_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL5),
                    new DbParameter("P_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL6),
                    new DbParameter("P_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_FIXED_NUMBER),
                    new DbParameter("P_LDI_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_LDI_NUMBER),
                    new DbParameter("ID_CASO",DbType.String,50,ParameterDirection.Output,oItem.ID_CASO),
					new DbParameter("P_FLAG_CREACION", DbType.String,255,ParameterDirection.Output,oItem.FLAG_CREACION),
			   		new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output,oItem.MESSAGE),
                    #endregion
			};

            try
            {
                Web.Logging.ExecuteMethod(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(oItem.Audit.Session, oItem.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_PS_CREATE_PLUS_CASE, parameters);

                });


                oResponse.ID_CASO = parameters[101].Value.ToString();
                oResponse.FLAG_INSERCION = parameters[102].Value.ToString();
                oResponse.MESSAGE = parameters[103].Value.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }

            return oResponse;
        }

        /// <summary>
        /// insertar plantilla de caso contingencia
        /// </summary>
        /// <param name="oItem"></param>
        /// <returns></returns>
        /// <remarks>GetInsertTemplateCaseContingent</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static EntitiesFixed.CaseTemplate GetInsertTemplateCaseContingent(EntitiesFixed.CaseTemplate oItem)
        {
            EntitiesFixed.CaseTemplate oResponse = new EntitiesFixed.CaseTemplate();
            string rFlagInsercion = string.Empty;
            string rMsgText = string.Empty;
            DbParameter[] parameters =
            
            {
             #region Input												   
                                                   new DbParameter("PN_SECUENCIAL",DbType.Double,ParameterDirection.Input,oItem.ID_CASO), 
                                                   new DbParameter("PV_NRO_CASO",DbType.String,80,ParameterDirection.Input,oItem.ID_CASO), 
                                                    new DbParameter("P_CAS_1",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_1),
                                                   new DbParameter("P_CAS_2",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_2),
                                                   new DbParameter("P_CAS_3",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_3),
                                                   new DbParameter("P_CAS_4",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_4),
                                                   new DbParameter("P_CAS_5",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_5),
                                                   new DbParameter("P_CAS_6",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_6),
                                                   new DbParameter("P_CAS_7",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_7),
                                                   new DbParameter("P_CAS_8",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_8),
                                                   new DbParameter("P_CAS_9",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_9),
                                                   new DbParameter("P_CAS_10",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_10),
                                                   new DbParameter("P_CAS_11",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_11),
                                                   new DbParameter("P_CAS_12",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_12),
                                                   new DbParameter("P_CAS_13",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_13),
                                                   new DbParameter("P_CAS_14",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_14),
                                                   new DbParameter("P_CAS_15",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_15),
                                                   new DbParameter("P_CAS_16",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_16),
                                                   new DbParameter("P_CAS_17",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_17),
                                                   new DbParameter("P_CAS_18",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_18),
                                                   new DbParameter("P_CAS_19",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_19),
                                                   new DbParameter("P_CAS_20",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_20),
                                                   new DbParameter("P_CAS_21",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_21),
                                                   new DbParameter("P_CAS_22",DbType.Double,ParameterDirection.Input,oItem.X_CAS_22),
                                                   new DbParameter("P_CAS_23",DbType.Double,ParameterDirection.Input,oItem.X_CAS_23),
                                                   new DbParameter("P_CAS_24",DbType.Double,ParameterDirection.Input,oItem.X_CAS_24),
                                                   new DbParameter("P_CAS_25",DbType.Double,ParameterDirection.Input,oItem.X_CAS_25),
                                                   new DbParameter("P_CAS_26",DbType.Double,ParameterDirection.Input,oItem.X_CAS_26),
                                                   new DbParameter("P_CAS_27",DbType.Double,ParameterDirection.Input,oItem.X_CAS_27),
                                                   new DbParameter("P_CAS_28",DbType.Double,ParameterDirection.Input,oItem.X_CAS_28),
                                                   new DbParameter("P_CAS_29",DbType.String,255,ParameterDirection.Input,oItem.X_CAS_29),
                                                   new DbParameter("P_CAS_30",DbType.String,255,ParameterDirection.Input,oItem.X_CAS_30),
                                                   new DbParameter("P_SUSPENSION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_SUSPENSION_DATE),
                                                   new DbParameter("P_REACTIVATION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_REACTIVATION_DATE),
                                                   new DbParameter("P_SUSPENSION_QT",DbType.Double,ParameterDirection.Input,oItem.X_SUSPENSION_QT),
                                                   new DbParameter("P_CONCLUSIONS",DbType.String,255,ParameterDirection.Input,oItem.X_CONCLUSIONS),
                                                   new DbParameter("P_TEST_MADE",DbType.String,255,ParameterDirection.Input,oItem.X_TEST_MADE),
                                                   new DbParameter("P_PROBLEM_DESCRIPTION",DbType.String,255,ParameterDirection.Input,oItem.X_PROBLEM_DESCRIPTION),
                                                   new DbParameter("P_ADDRESS",DbType.String,255,ParameterDirection.Input,oItem.X_ADDRESS),
                                                   new DbParameter("P_DOCUMENT_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_DOCUMENT_NUMBER),
                                                   new DbParameter("P_CALL_DURATION",DbType.String,50,ParameterDirection.Input,oItem.X_CALL_DURATION),
                                                   new DbParameter("P_CALL_COST",DbType.Double,ParameterDirection.Input,oItem.X_CALL_COST),
                                                   new DbParameter("P_SYSTEM_STATUS",DbType.String,80,ParameterDirection.Input,oItem.X_SYSTEM_STATUS),
                                                   new DbParameter("P_FLAG_VARIATION",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_VARIATION),
                                                   new DbParameter("P_SEARCH_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_SEARCH_DATE),
                                                   new DbParameter("P_VARIATION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_VARIATION_DATE),
                                                   new DbParameter("P_LAST_QUERY",DbType.DateTime,ParameterDirection.Input,oItem.X_LAST_QUERY),
                                                   new DbParameter("P_PROBLEM_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_PROBLEM_DATE),
                                                   new DbParameter("P_PURCHASE_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_PURCHASE_DATE),
                                                   new DbParameter("P_RECHARGE_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_RECHARGE_DATE),
                                                   new DbParameter("P_REQUEST_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_REQUEST_DATE),
                                                   new DbParameter("P_REQUEST_PLACE",DbType.String,80,ParameterDirection.Input,oItem.X_REQUEST_PLACE),
                                                   new DbParameter("P_FLAG_GPRS",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_GPRS),
                                                   new DbParameter("P_COMPLAINT_AMOUNT",DbType.Double,ParameterDirection.Input,oItem.X_COMPLAINT_AMOUNT),
                                                   new DbParameter("P_LINES",DbType.String,20,ParameterDirection.Input,oItem.X_LINES),
                                                   new DbParameter("P_ERROR_MESSAGE",DbType.String,100,ParameterDirection.Input,oItem.X_ERROR_MESSAGE),
                                                   new DbParameter("P_MODEL",DbType.String,50,ParameterDirection.Input,oItem.X_MODEL),
                                                   new DbParameter("P_MARK",DbType.String,50,ParameterDirection.Input,oItem.X_MARK),
                                                   new DbParameter("P_BAND",DbType.String,50,ParameterDirection.Input,oItem.X_BAND),
                                                   new DbParameter("P_REPOSITION_REASON",DbType.String,80,ParameterDirection.Input,oItem.X_REPOSITION_REASON),
                                                   new DbParameter("P_CHURN_REASON",DbType.String,80,ParameterDirection.Input,oItem.X_CHURN_REASON),
                                                   new DbParameter("P_CELLULAR_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_CELLULAR_NUMBER),
                                                   new DbParameter("P_CLARIFY_VARIATION",DbType.String,15,ParameterDirection.Input,oItem.X_CLARIFY_VARIATION),
                                                   new DbParameter("P_FLAG_SEND_RECEIVE",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_SEND_RECEIVE),
                                                   new DbParameter("P_CUSTOMER_NAME",DbType.String,80,ParameterDirection.Input,oItem.X_CUSTOMER_NAME),
                                                   new DbParameter("P_PREPAID_CARD_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_PREPAID_CARD_NUMBER),
                                                   new DbParameter("P_NUMBERS_COMMUNICATION",DbType.String,80,ParameterDirection.Input,oItem.X_NUMBERS_COMMUNICATION),
                                                   new DbParameter("P_REFERENCE_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_REFERENCE_NUMBER),
                                                   new DbParameter("P_OST_NUMBER",DbType.String,30,ParameterDirection.Input,oItem.X_OST_NUMBER),
                                                   new DbParameter("P_FRIENDS_FAMILY",DbType.String,80,ParameterDirection.Input,oItem.X_FRIENDS_FAMILY),
                                                   new DbParameter("P_COUNTRY_OPERATOR",DbType.String,80,ParameterDirection.Input,oItem.X_COUNTRY_OPERATOR),
                                                   new DbParameter("P_OLD_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_OLD_PLAN),
                                                   new DbParameter("P_NEW_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_NEW_PLAN),
                                                   new DbParameter("P_CURRENT_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_CURRENT_PLAN),
                                                   new DbParameter("P_CAMPAIGN",DbType.String,80,ParameterDirection.Input,oItem.X_CAMPAIGN),
                                                   new DbParameter("P_BILL_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_BILL_NUMBER_COMPLAINT),
                                                   new DbParameter("P_REFERENCE_ADDRESS",DbType.String,100,ParameterDirection.Input,oItem.X_REFERENCE_ADDRESS),
                                                   new DbParameter("P_CURRENT_BALANCE",DbType.Double,ParameterDirection.Input,oItem.X_CURRENT_BALANCE),
                                                   new DbParameter("P_LAST_BALANCE",DbType.Double,ParameterDirection.Input,oItem.X_LAST_BALANCE),
                                                   new DbParameter("P_BALANCE_REQUESTED",DbType.Double,ParameterDirection.Input,oItem.X_BALANCE_REQUESTED),
                                                   new DbParameter("P_CUSTOMER_SEGMENT",DbType.String,30,ParameterDirection.Input,oItem.X_CUSTOMER_SEGMENT),
                                                   new DbParameter("P_SERVICE_PROBLEM",DbType.String,50,ParameterDirection.Input,oItem.X_SERVICE_PROBLEM),
                                                   new DbParameter("P_FLAG_OTHER_PROBLEMS",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_OTHER_PROBLEMS),
                                                   new DbParameter("P_OPERATOR_PROBLEM",DbType.String,80,ParameterDirection.Input,oItem.X_OPERATOR_PROBLEM),
                                                   new DbParameter("P_CONTACT_TIME_TERM",DbType.String,80,ParameterDirection.Input,oItem.X_CONTACT_TIME_TERM),
                                                   new DbParameter("P_STORE",DbType.String,80,ParameterDirection.Input,oItem.X_STORE),
                                                   new DbParameter("P_DIAL_TYPE",DbType.String,10,ParameterDirection.Input,oItem.X_DIAL_TYPE),
                                                   new DbParameter("P_PROBLEM_LOCATION",DbType.String,255,ParameterDirection.Input,oItem.X_PROBLEM_LOCATION),
                                                   new DbParameter("P_FLAG_ADDITIONAL_SERVICES",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_ADDITIONAL_SERVICES),
                                                   new DbParameter("P_FLAG_WAP",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_WAP),
                                                   new DbParameter("P_CLARO_LDN1",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN1),
                                                   new DbParameter("P_CLARO_LDN2",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN2),
                                                   new DbParameter("P_CLARO_LDN3",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN3),
                                                   new DbParameter("P_CLARO_LDN4",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN4),
                                                   new DbParameter("P_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL1),
                                                   new DbParameter("P_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL2),
                                                   new DbParameter("P_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL3),
                                                   new DbParameter("P_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL4),
                                                   new DbParameter("P_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL5),
                                                   new DbParameter("P_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL6),
                                                   new DbParameter("P_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_FIXED_NUMBER),
                                                   new DbParameter("P_LDI_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_LDI_NUMBER),
                                                   new DbParameter("ID_CASO",DbType.String,50,ParameterDirection.Output,oItem.ID_CASO),
                                                   new DbParameter("P_FLAG_CREACION", DbType.String,255,ParameterDirection.Output,oItem.FLAG_CREACION),
                                                   new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output,oItem.MESSAGE),
        #endregion
			};



            try
            {
                Web.Logging.ExecuteMethod(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(oItem.Audit.Session, oItem.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_DB,
                        DbCommandConfiguration.SIACU_POST_INSERTAR_X_PLUS_CASE, parameters);

                });


                oResponse.ID_CASO = parameters[102].Value.ToString();
                oResponse.FLAG_INSERCION = parameters[103].Value.ToString();
                oResponse.MESSAGE = parameters[104].Value.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }

            return oResponse;
        }

        /// <summary>
        /// Actualiza Plantilla Caso
        /// </summary>
        /// <param name="oItem"></param>
        /// <returns></returns>
        /// <remarks>ActualizaPlantillaCaso</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static EntitiesFixed.CaseTemplate ActualizaPlantillaCaso(EntitiesFixed.CaseTemplate oItem)
        {
            EntitiesFixed.CaseTemplate oResponse = new EntitiesFixed.CaseTemplate();
            DbParameter[] parameters = 
            {
                #region Parametros
                                                   new DbParameter("PV_NRO_CASO",DbType.String,80,ParameterDirection.Input,oItem.ID_CASO), 
                                                    new DbParameter("P_CAS_1",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_1),
                                                   new DbParameter("P_CAS_2",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_2),
                                                   new DbParameter("P_CAS_3",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_3),
                                                   new DbParameter("P_CAS_4",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_4),
                                                   new DbParameter("P_CAS_5",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_5),
                                                   new DbParameter("P_CAS_6",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_6),
                                                   new DbParameter("P_CAS_7",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_7),
                                                   new DbParameter("P_CAS_8",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_8),
                                                   new DbParameter("P_CAS_9",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_9),
                                                   new DbParameter("P_CAS_10",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_10),
                                                   new DbParameter("P_CAS_11",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_11),
                                                   new DbParameter("P_CAS_12",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_12),
                                                   new DbParameter("P_CAS_13",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_13),
                                                   new DbParameter("P_CAS_14",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_14),
                                                   new DbParameter("P_CAS_15",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_15),
                                                   new DbParameter("P_CAS_16",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_16),
                                                   new DbParameter("P_CAS_17",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_17),
                                                   new DbParameter("P_CAS_18",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_18),
                                                   new DbParameter("P_CAS_19",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_19),
                                                   new DbParameter("P_CAS_20",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_20),
                                                   new DbParameter("P_CAS_21",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_21),
                                                   new DbParameter("P_CAS_22",DbType.Double,ParameterDirection.Input,oItem.X_CAS_22),
                                                   new DbParameter("P_CAS_23",DbType.Double,ParameterDirection.Input,oItem.X_CAS_23),
                                                   new DbParameter("P_CAS_24",DbType.Double,ParameterDirection.Input,oItem.X_CAS_24),
                                                   new DbParameter("P_CAS_25",DbType.Double,ParameterDirection.Input,oItem.X_CAS_25),
                                                   new DbParameter("P_CAS_26",DbType.Double,ParameterDirection.Input,oItem.X_CAS_26),
                                                   new DbParameter("P_CAS_27",DbType.Double,ParameterDirection.Input,oItem.X_CAS_27),
                                                   new DbParameter("P_CAS_28",DbType.Double,ParameterDirection.Input,oItem.X_CAS_28),
                                                   new DbParameter("P_CAS_29",DbType.String,255,ParameterDirection.Input,oItem.X_CAS_29),
                                                   new DbParameter("P_CAS_30",DbType.String,255,ParameterDirection.Input,oItem.X_CAS_30),												   new DbParameter("P_SUSPENSION_DATE",DbType.DateTime,ParameterDirection.Input),
												   new DbParameter("P_REACTIVATION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_REACTIVATION_DATE),
												   new DbParameter("P_SUSPENSION_QT",DbType.Double,ParameterDirection.Input,oItem.X_SUSPENSION_QT),
                                                   new DbParameter("P_CONCLUSIONS",DbType.String,255,ParameterDirection.Input,oItem.X_CONCLUSIONS),
                                                   new DbParameter("P_TEST_MADE",DbType.String,255,ParameterDirection.Input,oItem.X_TEST_MADE),
                                                   new DbParameter("P_PROBLEM_DESCRIPTION",DbType.String,255,ParameterDirection.Input,oItem.X_PROBLEM_DESCRIPTION),
                                                   new DbParameter("P_ADDRESS",DbType.String,255,ParameterDirection.Input,oItem.X_ADDRESS),
                                                   new DbParameter("P_DOCUMENT_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_DOCUMENT_NUMBER),
                                                   new DbParameter("P_CALL_DURATION",DbType.String,50,ParameterDirection.Input,oItem.X_CALL_DURATION),
                                                   new DbParameter("P_CALL_COST",DbType.Double,ParameterDirection.Input,oItem.X_CALL_COST),
                                                   new DbParameter("P_SYSTEM_STATUS",DbType.String,80,ParameterDirection.Input,oItem.X_SYSTEM_STATUS),
                                                   new DbParameter("P_FLAG_VARIATION",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_VARIATION),
                                                   new DbParameter("P_SEARCH_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_SEARCH_DATE),
                                                   new DbParameter("P_VARIATION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_VARIATION_DATE),
                                                   new DbParameter("P_LAST_QUERY",DbType.DateTime,ParameterDirection.Input,oItem.X_LAST_QUERY),
                                                   new DbParameter("P_PROBLEM_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_PROBLEM_DATE),
                                                   new DbParameter("P_PURCHASE_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_PURCHASE_DATE),
                                                   new DbParameter("P_RECHARGE_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_RECHARGE_DATE),
                                                   new DbParameter("P_REQUEST_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_REQUEST_DATE),
                                                   new DbParameter("P_REQUEST_PLACE",DbType.String,80,ParameterDirection.Input,oItem.X_REQUEST_PLACE),
                                                   new DbParameter("P_FLAG_GPRS",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_GPRS),
                                                   new DbParameter("P_COMPLAINT_AMOUNT",DbType.Double,ParameterDirection.Input,oItem.X_COMPLAINT_AMOUNT),
                                                   new DbParameter("P_LINES",DbType.String,20,ParameterDirection.Input,oItem.X_LINES),
                                                   new DbParameter("P_ERROR_MESSAGE",DbType.String,100,ParameterDirection.Input,oItem.X_ERROR_MESSAGE),
                                                   new DbParameter("P_MODEL",DbType.String,50,ParameterDirection.Input,oItem.X_MODEL),
                                                   new DbParameter("P_MARK",DbType.String,50,ParameterDirection.Input,oItem.X_MARK),
                                                   new DbParameter("P_BAND",DbType.String,50,ParameterDirection.Input,oItem.X_BAND),
                                                   new DbParameter("P_REPOSITION_REASON",DbType.String,80,ParameterDirection.Input,oItem.X_REPOSITION_REASON),
                                                   new DbParameter("P_CHURN_REASON",DbType.String,80,ParameterDirection.Input,oItem.X_CHURN_REASON),
                                                   new DbParameter("P_CELLULAR_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_CELLULAR_NUMBER),
                                                   new DbParameter("P_CLARIFY_VARIATION",DbType.String,15,ParameterDirection.Input,oItem.X_CLARIFY_VARIATION),
                                                   new DbParameter("P_FLAG_SEND_RECEIVE",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_SEND_RECEIVE),
                                                   new DbParameter("P_CUSTOMER_NAME",DbType.String,80,ParameterDirection.Input,oItem.X_CUSTOMER_NAME),
                                                   new DbParameter("P_PREPAID_CARD_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_PREPAID_CARD_NUMBER),
                                                   new DbParameter("P_NUMBERS_COMMUNICATION",DbType.String,80,ParameterDirection.Input,oItem.X_NUMBERS_COMMUNICATION),
                                                   new DbParameter("P_REFERENCE_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_REFERENCE_NUMBER),												   
                                                   new DbParameter("P_OST_NUMBER",DbType.String,30,ParameterDirection.Input,oItem.X_OST_NUMBER),
                                                   new DbParameter("P_FRIENDS_FAMILY",DbType.String,80,ParameterDirection.Input,oItem.X_FRIENDS_FAMILY),
                                                   new DbParameter("P_COUNTRY_OPERATOR",DbType.String,80,ParameterDirection.Input,oItem.X_COUNTRY_OPERATOR),
                                                   new DbParameter("P_OLD_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_OLD_PLAN),
                                                   new DbParameter("P_NEW_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_NEW_PLAN),
                                                   new DbParameter("P_CURRENT_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_CURRENT_PLAN),
                                                   new DbParameter("P_CAMPAIGN",DbType.String,80,ParameterDirection.Input,oItem.X_CAMPAIGN),
                                                   new DbParameter("P_BILL_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_BILL_NUMBER_COMPLAINT),
                                                   new DbParameter("P_REFERENCE_ADDRESS",DbType.String,100,ParameterDirection.Input,oItem.X_REFERENCE_ADDRESS),
                                                   new DbParameter("P_CURRENT_BALANCE",DbType.Double,ParameterDirection.Input,oItem.X_CURRENT_BALANCE),
                                                   new DbParameter("P_LAST_BALANCE",DbType.Double,ParameterDirection.Input,oItem.X_LAST_BALANCE),
                                                   new DbParameter("P_BALANCE_REQUESTED",DbType.Double,ParameterDirection.Input,oItem.X_BALANCE_REQUESTED),
                                                   new DbParameter("P_CUSTOMER_SEGMENT",DbType.String,30,ParameterDirection.Input,oItem.X_CUSTOMER_SEGMENT),
                                                   new DbParameter("P_SERVICE_PROBLEM",DbType.String,50,ParameterDirection.Input,oItem.X_SERVICE_PROBLEM),
                                                   new DbParameter("P_FLAG_OTHER_PROBLEMS",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_OTHER_PROBLEMS),
                                                   new DbParameter("P_OPERATOR_PROBLEM",DbType.String,80,ParameterDirection.Input,oItem.X_OPERATOR_PROBLEM),
                                                   new DbParameter("P_CONTACT_TIME_TERM",DbType.String,80,ParameterDirection.Input,oItem.X_CONTACT_TIME_TERM),
                                                   new DbParameter("P_STORE",DbType.String,80,ParameterDirection.Input,oItem.X_STORE),
                                                   new DbParameter("P_DIAL_TYPE",DbType.String,10,ParameterDirection.Input,oItem.X_DIAL_TYPE),
                                                   new DbParameter("P_PROBLEM_LOCATION",DbType.String,255,ParameterDirection.Input,oItem.X_PROBLEM_LOCATION),
                                                   new DbParameter("P_FLAG_ADDITIONAL_SERVICES",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_ADDITIONAL_SERVICES),
                                                   new DbParameter("P_FLAG_WAP",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_WAP),
                                                   new DbParameter("P_CLARO_LDN1",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN1),
                                                   new DbParameter("P_CLARO_LDN2",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN2),
                                                   new DbParameter("P_CLARO_LDN3",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN3),
                                                   new DbParameter("P_CLARO_LDN4",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN4),
                                                   new DbParameter("P_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL1),
                                                   new DbParameter("P_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL2),
                                                   new DbParameter("P_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL3),
                                                   new DbParameter("P_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL4),
                                                   new DbParameter("P_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL5),
                                                   new DbParameter("P_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL6),
                                                   new DbParameter("P_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_FIXED_NUMBER),
                                                   new DbParameter("P_LDI_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_LDI_NUMBER),
                                                   new DbParameter("ID_CASO",DbType.String,50,ParameterDirection.Output,oItem.ID_CASO),
												   new DbParameter("P_FLAG_ACTUALIZACION", DbType.String,255,ParameterDirection.Output,oItem.FLAG_ACTUALIZACION),
												   new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output,oItem.MESSAGE),
#endregion
            };



            try
            {
                Web.Logging.ExecuteMethod(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(oItem.Audit.Session, oItem.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_SP_UPDATE_PLUS_CASE, parameters);

                });
                oResponse.ID_CASO = parameters[100].Value.ToString();
                oResponse.FLAG_INSERCION = parameters[101].Value.ToString();
                oResponse.MESSAGE = parameters[102].Value.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }


            return oResponse;
        }

        /// <summary>
        /// consulta servicios por contrato
        /// </summary>
        /// <param name="oConsultationServiceByContractRequest"></param>
        /// <returns></returns>
        /// <remarks>GetConsultationServiceByContract</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static ConsultationServiceByContractResponse GetConsultationServiceByContract(ConsultationServiceByContractRequest oConsultationServiceByContractRequest)
        {
            string strMsgSalida = string.Empty;
            string msisdn = string.Empty;
            bool bResultado = false;

            ConsultationServiceByContractResponse oConsultationServiceByContractResponse = new ConsultationServiceByContractResponse();

            if (oConsultationServiceByContractRequest.typeProduct == Claro.SIACU.Constants.HFC)
            {
                CUSTOMER_HFC.consultarServicioPorCodigoContratoEAIRequest objConsultaServicioId = new CUSTOMER_HFC.consultarServicioPorCodigoContratoEAIRequest();
                CUSTOMER_HFC.consultarServicioPorCodigoContratoEAIResponse objConsultaServicioIdSalida = new CUSTOMER_HFC.consultarServicioPorCodigoContratoEAIResponse();
                CUSTOMER_HFC.ConsultarServicioPorCodigoContratoEAIInput objConsultaServicioInput = new CUSTOMER_HFC.ConsultarServicioPorCodigoContratoEAIInput();
                CUSTOMER_HFC.ConsultarServicioPorCodigoContratoEAIOutput objConsultaServicioOutput = new CUSTOMER_HFC.ConsultarServicioPorCodigoContratoEAIOutput();
                CUSTOMER_HFC.ServicioPorCodigoContratoType[] objTempServicio = new CUSTOMER_HFC.ServicioPorCodigoContratoType[0];
                CUSTOMER_HFC.CabeceraRequest objCabecera = new CUSTOMER_HFC.CabeceraRequest();
                CUSTOMER_HFC.CuerpoCSCORequest objCuerpo = new CUSTOMER_HFC.CuerpoCSCORequest();

                objCabecera.idTransaccion = oConsultationServiceByContractRequest.Audit.Transaction;
                objCabecera.ipAplicacion = oConsultationServiceByContractRequest.Audit.IPAddress;
                objCabecera.nombreAplicacion = oConsultationServiceByContractRequest.Audit.ApplicationName;
                objCabecera.usuarioAplicacion = oConsultationServiceByContractRequest.Audit.UserName;
                objCuerpo.codigoContrato = oConsultationServiceByContractRequest.strCodContrato;
                objConsultaServicioInput.cabeceraRequest = objCabecera;
                objConsultaServicioInput.cuerpoRequest = objCuerpo;
                objConsultaServicioId.consultarServicioPorCodigoContratoEaiRequest = objConsultaServicioInput;


                CUSTOMER_HFC.CabeceraResponse objCabeceraSalida = new CUSTOMER_HFC.CabeceraResponse();
                CUSTOMER_HFC.CuerpoCSCOResponse objCuerpoSalida = new CUSTOMER_HFC.CuerpoCSCOResponse();
                objConsultaServicioIdSalida = Configuration.ServiceConfiguration.FIXED_CUSTOMER_HFC.consultarServicioPorCodigoContrato(objConsultaServicioId);

                objCabeceraSalida = objConsultaServicioIdSalida.consultarServicioPorCodigoContratoEaiResponse.cabeceraResponse;
                objCuerpoSalida = objConsultaServicioIdSalida.consultarServicioPorCodigoContratoEaiResponse.cuerpoResponse;
                objTempServicio = objConsultaServicioIdSalida.consultarServicioPorCodigoContratoEaiResponse.cuerpoResponse.listaServicioPorCodigoContrato;

                if (objCabeceraSalida.codigoRespuesta == Claro.Constants.NumberZeroString)
                {

                    for (int i = 0; i < objTempServicio.Length; i++)
                    {

                        msisdn = Claro.Utils.CheckStr(objTempServicio[i].msisdn);

                    }
                    strMsgSalida = objCabeceraSalida.mensajeRespuesta;
                    bResultado = true;

                }
                else
                {
                    msisdn = String.Empty;
                    strMsgSalida = objCabeceraSalida.mensajeRespuesta;
                    bResultado = false;
                }

            }
            else
            {
                CUSTOMER_LTE.consultarServicioPorCodigoContratoEAIRequest objConsultaServicioId = new CUSTOMER_LTE.consultarServicioPorCodigoContratoEAIRequest();
                CUSTOMER_LTE.consultarServicioPorCodigoContratoEAIResponse objConsultaServicioIdSalida = new CUSTOMER_LTE.consultarServicioPorCodigoContratoEAIResponse();
                CUSTOMER_LTE.ServicioPorCodigoContratoType[] objTempServicio = new CUSTOMER_LTE.ServicioPorCodigoContratoType[0];
                CUSTOMER_LTE.AuditRequestType objAuditRequest = new CUSTOMER_LTE.AuditRequestType();
                objAuditRequest.idTransaccion = oConsultationServiceByContractRequest.Audit.Transaction;
                objAuditRequest.ipAplicacion = oConsultationServiceByContractRequest.Audit.IPAddress;
                objAuditRequest.nombreAplicacion = oConsultationServiceByContractRequest.Audit.ApplicationName;
                objAuditRequest.usuarioAplicacion = oConsultationServiceByContractRequest.Audit.UserName;
                objConsultaServicioId.auditRequest = objAuditRequest;
                objConsultaServicioId.codigoContrato = oConsultationServiceByContractRequest.strCodContrato;

                CUSTOMER_LTE.AuditResponseType objAuditResponse = new CUSTOMER_LTE.AuditResponseType();
                objConsultaServicioIdSalida = Configuration.ServiceConfiguration.FIXED_CUSTOMER_LTE.consultarServicioPorCodigoContrato(objConsultaServicioId);
                objAuditResponse = objConsultaServicioIdSalida.auditResponse;
                objTempServicio = objConsultaServicioIdSalida.listaServicioPorCodigoContrato;

                if (objAuditResponse.codigoRespuesta == Claro.Constants.NumberZeroString)
                {

                    for (int i = 0; i < objTempServicio.Length; i++)
                    {
                        msisdn = Claro.Utils.CheckStr(objTempServicio[i].msisdn);
                    }
                    strMsgSalida = objAuditResponse.mensajeRespuesta;
                    bResultado = true;

                }
                else
                {
                    msisdn = String.Empty;
                    strMsgSalida = objAuditResponse.mensajeRespuesta;
                    bResultado = false;
                }

            }

            oConsultationServiceByContractResponse.bResultado = bResultado;
            oConsultationServiceByContractResponse.msisdn = msisdn;
            oConsultationServiceByContractResponse.strMsgSalida = strMsgSalida;

            return oConsultationServiceByContractResponse;

        }
        /// <summary>
        /// obtiene tareas programadas
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vstrCoId"></param>
        /// <param name="vstrCuenta"></param>
        /// <param name="vstrFDesde"></param>
        /// <param name="vstrFHasta"></param>
        /// <param name="vstrEstado"></param>
        /// <param name="vstrAsesor"></param>
        /// <param name="vstrTipoTran"></param>
        /// <param name="vstrCodInter"></param>
        /// <param name="vstrCacDac"></param>
        /// <returns></returns>
        /// <remarks>GetTransactionScheduled</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<TransactionScheduled> GetTransactionScheduled(string strIdSession, string strTransaction, string vstrCoId, string vstrCuenta,
                                                                         string vstrFDesde, string vstrFHasta, string vstrEstado, string vstrAsesor,
                                                                         string vstrTipoTran, string vstrCodInter, string vstrCacDac)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("p_servi_coid", DbType.String,255, ParameterDirection.Input, vstrCoId),

                new DbParameter("p_fecha_desde",DbType.Date,ParameterDirection.Input, DBNull.Value),
                new DbParameter("p_fecha_hasta",DbType.Date,ParameterDirection.Input, DBNull.Value),

                new DbParameter("p_estado", DbType.String,255, ParameterDirection.Input, vstrEstado),
                new DbParameter("p_asesor", DbType.String,255, ParameterDirection.Input, vstrAsesor),
                new DbParameter("p_cuenta", DbType.String,255, ParameterDirection.Input, vstrCuenta),
                new DbParameter("p_tipotransaccion", DbType.String,255, ParameterDirection.Input, vstrTipoTran),
                new DbParameter("p_codinteraccion", DbType.String,255, ParameterDirection.Input, vstrCodInter),
                new DbParameter("p_caddac", DbType.String,255, ParameterDirection.Input, vstrCacDac),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            if (!string.IsNullOrEmpty(vstrFDesde))
            {
                parameters[1].Value = Convert.ToDate(vstrFDesde);
            }

            if (!string.IsNullOrEmpty(vstrFHasta))
            {
                parameters[2].Value = Convert.ToDate(vstrFHasta);
            }

            List<TransactionScheduled> listItem = new List<TransactionScheduled>();
            TransactionScheduled item = null;

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_TIMEAI, DbCommandConfiguration.SIACU_CONSULTA_POSTT_SERVICIOPROG_HFC, parameters, dr =>
                {
                    while (dr.Read())
                    {
                        item = new TransactionScheduled();
                        item.CO_ID = CSTS.CheckStr(dr["CO_ID"]);
                        item.CUSTOMER_ID = CSTS.CheckStr(dr["CUSTOMER_ID"]);
                        item.SERVD_FECHAPROG = CSTS.CheckStr(dr["SERVD_FECHAPROG"]);
                        item.SERVD_FECHA_REG = CSTS.CheckStr(dr["SERVD_FECHA_REG"]);
                        item.SERVD_FECHA_EJEC = CSTS.CheckStr(dr["SERVD_FECHA_EJEC"]);
                        item.SERVC_ESTADO = CSTS.CheckStr(dr["SERVC_ESTADO"]);
                        item.DESC_ESTADO = CSTS.CheckStr(dr["DESC_ESTADO"]);
                        item.SERVC_ESBATCH = CSTS.CheckStr(dr["SERVC_ESBATCH"]);
                        item.SERVV_MEN_ERROR = CSTS.CheckStr(dr["SERVV_MEN_ERROR"]);
                        item.SERVV_COD_ERROR = CSTS.CheckStr(dr["SERVV_COD_ERROR"]);
                        item.SERVV_USUARIO_SISTEMA = CSTS.CheckStr(dr["SERVV_USUARIO_SISTEMA"]);
                        item.SERVV_ID_EAI_SW = CSTS.CheckStr(dr["SERVV_ID_EAI_SW"]);
                        item.SERVI_COD = CSTS.CheckStr(dr["SERVI_COD"]);
                        item.DESC_SERVI = CSTS.CheckStr(dr["DESC_SERVI"]);
                        item.SERVV_MSISDN = CSTS.CheckStr(dr["SERVV_MSISDN"]);
                        item.SERVV_ID_BATCH = CSTS.CheckStr(dr["SERVV_ID_BATCH"]);
                        item.SERVV_USUARIO_APLICACION = CSTS.CheckStr(dr["SERVV_USUARIO_APLICACION"]);
                        item.SERVV_EMAIL_USUARIO_APP = CSTS.CheckStr(dr["SERVV_EMAIL_USUARIO_APP"]);
                        item.SERVV_XMLENTRADA = CSTS.CheckStr(dr["SERVV_XMLENTRADA"]);
                        item.SERVC_NROCUENTA = CSTS.CheckStr(dr["SERVC_NROCUENTA"]);
                        item.SERVC_CODIGO_INTERACCION = CSTS.CheckStr(dr["SERVC_CODIGO_INTERACCION"]);
                        item.SERVC_PUNTOVENTA = CSTS.CheckStr(dr["SERVC_PUNTOVENTA"]);
                        item.SERVC_TIPO_SERV = CSTS.CheckStr(dr["SERVC_TIPO_SERV"]);
                        item.SERVC_CO_SER = CSTS.CheckStr(dr["SERVC_CO_SER"]);
                        item.SERVC_TIPO_REG = CSTS.CheckStr(dr["SERVC_TIPO_REG"]);
                        item.SERVC_DES_CO_SER = CSTS.CheckStr(dr["SERVC_DES_CO_SER"]);

                        listItem.Add(item);
                    }
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            Web.Logging.Info(strIdSession, strTransaction, "GetTransactionScheduled Lista Resultado: " + listItem.Count);

            return listItem;
        }
        /// <summary>
        /// obtiene linea de cliente
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>GetCustomerLineNumber</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static ConsultationServiceByContractResponse GetCustomerLineNumber(ConsultationServiceByContractRequest request)
        {
            var model = new ConsultationServiceByContractResponse();
            model.strMsgSalida = string.Empty;

            DbParameter[] parameters =
            {
                new DbParameter("result", DbType.String, 255, ParameterDirection.ReturnValue),
                new DbParameter("p_co_id", DbType.Int32, 255, ParameterDirection.Input, request.strCodContrato)
            };

            try
            {
                Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(request.Audit.Session, request.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_TFUN051_GET_DNNUM_FROM_COID, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
            }
            finally
            {
                var resultadoFuncion = parameters[parameters.Length - 2].Value.ToString();
                model.msisdn = resultadoFuncion;
                model.bResultado = true;
            }

            return model;
        }
        /// <summary>
        /// obtiene interaccion id por caso id
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetInteractIDforCaseID</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static EntitiesFixed.GetCaseInsert.CaseInsertResponse GetInteractIDforCaseID(EntitiesFixed.GetCaseInsert.CaseInsertRequest objRequest)
        {

            EntitiesFixed.GetCaseInsert.CaseInsertResponse oResponse = new EntitiesFixed.GetCaseInsert.CaseInsertResponse();

            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "GetInteractIDforCaseID IN | P_ID_CASO" + objRequest.ID_CASO);

            DbParameter[] parameters =
            
            {
                   #region Imput
                    new DbParameter("P_ID_CASO",DbType.String,80,ParameterDirection.Input,objRequest.ID_CASO),
                    new DbParameter("P_FLAG_CONSULTA",DbType.String,50,ParameterDirection.Output),
					new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output),
			   		new DbParameter("P_INTERACT_ID", DbType.String,255,ParameterDirection.Output),
                    #endregion
			};

            try
            {
                Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_SP_INTERACT_ID_HFC, parameters);

                });


                oResponse.rFlagInsercion = parameters[1].Value.ToString();
                oResponse.rMsgText = parameters[2].Value.ToString();
                oResponse.rCasoId = parameters[3].Value.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return oResponse;
        }

    }
}
