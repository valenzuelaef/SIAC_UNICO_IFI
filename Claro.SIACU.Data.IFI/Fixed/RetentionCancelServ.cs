using System;
using System.Collections.Generic;
using System.Data;
using Claro.Data;
using System.Configuration;
using Claro.SIACU.Data.IFI.Configuration;

using EntitiesFixed = Claro.SIACU.Entity.IFI.Fixed;

using Claro.SIACU.Entity.IFI.Fixed;
using KEY = Claro.ConfigurationManager;

using Claro.SIACU.Entity.IFI.Fixed.GetCaseInsert;
using CUSTOMER_HFC = Claro.SIACU.ProxyService.IFI.SIACFixed.CustomerHFC;
using CUSTOMER_LTE = Claro.SIACU.ProxyService.IFI.SIACFixed.CustomerLTE;
using Claro.SIACU.Entity.IFI.Fixed.GetRetentionCancelServices;
using Claro.SIACU.Entity.IFI.Fixed.GetAddDayWork;


namespace Claro.SIACU.Data.IFI.Fixed
{
    public class RetentionCancelServ
    {
        //Name: Obtener las Lista de Acciones de Retención/Cancelación
        /// <summary>
        /// obtiene acciones
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vNivel"></param>
        /// <returns></returns>
        /// <remarks>GetAcciones</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<GenericItem> GetAcciones(string strIdSession, string strTransaction, Int32 vNivel)
        {
            List<GenericItem> oLstAcciones = new List<GenericItem>();
            DbParameter[] parameters =
            {   new DbParameter("P_NIVEL", DbType.Int64, 255,ParameterDirection.Input, vNivel), 
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB,  //SIAC_POST_PVU
                        DbCommandConfiguration.SIACU_POST_PVU_LISTA_ACCIONES_RETENCION, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                if (reader["COD_TIPO_SERV"].ToString() == "3")
                                {

                                    var item = new GenericItem
                                    {

                                        Codigo = reader["CODIGO"].ToString(),
                                        Descripcion = reader["DESCRIPCION"].ToString(),
                                        Cod_tipo_servicio = Convert.ToInt(reader["COD_TIPO_SERV"].ToString())

                                    };
                                    oLstAcciones.Add(item);
                                }

                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLstAcciones;
        }

        //Name: Obtener las Lista Motivos de Retención/Cancelación
        /// <summary>
        /// obtiene motivo de cancelacion
        /// </summary>
        /// <param name="oRequest"></param>
        /// <returns></returns>
        /// <remarks>GetMotCancelacion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<GenericItem> GetMotCancelacion(RetentionCancelServicesRequest oRequest)
        {

            List<GenericItem> oLstMotCancelacion = new List<GenericItem>();
            DbParameter[] parameters =
            {   new DbParameter("P_ESTADO", DbType.Int16, 10,ParameterDirection.Input, oRequest.vEstado), 
                new DbParameter("P_TIPO", DbType.Int16, 10,ParameterDirection.Input,oRequest.vTipoLista ),  
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteReader(oRequest.Audit.Session, oRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_DB,  
                        DbCommandConfiguration.SIACU_POST_PVU_LISTAR_MOTIVOS_RETENCION, parameters, reader =>
                        {
                            while (reader.Read())
                            {

                                var item = new GenericItem
                                {

                                    Codigo = reader["CODIGO"].ToString(),
                                    Descripcion = reader["DESCRIPCION"].ToString(),
                                    Cod_tipo_servicio = Convert.ToInt(reader["COD_TIPO_SERV"].ToString())

                                };
                                oLstMotCancelacion.Add(item);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(oRequest.Audit.Session.ToString(), oRequest.Audit.Transaction, "Error GetMotCancelacion : " + ex.Message);
            }
            return oLstMotCancelacion;
        }

        //Name: Obtener las Lista Sub Motivos de Retención/Cancelación  
        /// <summary>
        /// obtiene submotivo de cancelacion
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="pIdMotivo"></param>
        /// <returns></returns>
        /// <remarks>GetSubMotiveCancel</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<GenericItem> GetSubMotiveCancel(string strIdSession, string strTransaction, int pIdMotivo)
        {
            List<EntitiesFixed.GenericItem> oLstSubMotive = new List<EntitiesFixed.GenericItem>();
            try
            {
                DbParameter[] parameters = new DbParameter[]{
                    new DbParameter("PI_IDMOTIVO", DbType.Int32,ParameterDirection.Input, pIdMotivo), 
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output)
            };

                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_POST_PVU_LISTAR_SUBMOTIVOS_RETENCION, parameters, (IDataReader reader) =>
            {
                Entity.IFI.Fixed.GenericItem item;
                while (reader.Read())
                {
                    item = new Entity.IFI.Fixed.GenericItem();
                    item.Codigo = Claro.Convert.ToString(reader[0]);
                    item.Descripcion = Claro.Convert.ToString(reader[1]);
                    item.Estado = Claro.Convert.ToString(reader[3]);
                    oLstSubMotive.Add(item);

                }
            });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));
                throw new Exception(Claro.Utils.GetExceptionMessage(ex));
            }

            return oLstSubMotive;
        }
       
        //Name: Obtener las Lista de Tipos de Trabajo de Retención/Cancelación  
        /// <summary>
        /// obtiene tipo de trabajo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="IntIdTypeWork"></param>
        /// <param name="strTransaction"></param>
        /// <returns></returns>
        /// <remarks>GetTypeWork</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<GenericItem> GetTypeWork(string strIdSession, Int64 IntIdTypeWork, string strTransaction)
        {

            List<GenericItem> oLstTypeWork = new List<GenericItem>();

            DbParameter[] parameters =
            {   new DbParameter("p_tipo", DbType.Int64, 255,ParameterDirection.Input, IntIdTypeWork), 
                new DbParameter("srv_tipra", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA,
                        DbCommandConfiguration.SIACU_SP_CONSULTA_TIPTRA, parameters, reader =>
                        {
                            while (reader.Read())
                            {

                                var item = new GenericItem
                                {

                                    Codigo = reader["tiptra"].ToString(),
                                    Descripcion = reader["descripcion"].ToString(),
                                    Codigo2 = reader["FLAG_FRANJA"].ToString()

                                };
                                oLstTypeWork.Add(item);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLstTypeWork;
        }

        //Name: Obtener las Lista de Sub Tipos de Trabajo de Retención/Cancelación  
        /// <summary>
        /// obtiene subtipos de trabajo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="vIdTypeWork"></param>
        /// <param name="strTransaction"></param>
        /// <returns></returns>
        /// <remarks>GetSubTypeWork</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<GenericItem> GetSubTypeWork(string strIdSession, Int64 vIdTypeWork, string strTransaction)
        {

            List<GenericItem> oLstTypeWork = new List<GenericItem>();

            DbParameter[] parameters =
            {   new DbParameter("vIdtiptra", DbType.String, 22,ParameterDirection.Input, vIdTypeWork), 
                new DbParameter("cur_tipo", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA,
                        DbCommandConfiguration.SIACU_P_CONSULTA_SUBTIPORD, parameters, reader =>
                        {
                            while (reader.Read())
                            {

                                var item = new GenericItem
                                {

                                    Codigo = reader["VALOR"].ToString(),
                                    Descripcion = reader["DESCRIPCION"].ToString(),

                                };
                                oLstTypeWork.Add(item);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }




            return oLstTypeWork;
        }

        //Name: Obtener las Lista de Motivos SOT - de Retención/Cancelación   - Temporal
        /// <summary>
        /// obtiene motivo sot
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <returns></returns>
        /// <remarks>GetMotiveSOT</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<GenericItem> GetMotiveSOT(string strIdSession, string strTransaction)
        {
            List<GenericItem> oLstMotiveSOT = new List<GenericItem>();
            DbParameter[] parameters =
            {    
                new DbParameter("srv_cur", DbType.Object, ParameterDirection.Output)
            };

            try
            {

                Web.Logging.ExecuteMethod(strIdSession.ToString(), strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA,
                        DbCommandConfiguration.SIACU_POST_SGA_P_CONSULTA_MOTIVO, parameters, reader =>
                        {
                            while (reader.Read())
                            {

                                var item = new GenericItem
                                {

                                    Codigo = reader["CODMOTOT"].ToString(),
                                    Descripcion = reader["MOTIVO"].ToString(),

                                };
                                oLstMotiveSOT.Add(item);

                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLstMotiveSOT;
        }

        //Name: Agregar días laborables
        /// <summary>
        /// agrega un dia de tarbajo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strFechaIni"></param>
        /// <param name="intNroDias"></param>
        /// <param name="strFechResult"></param>
        /// <param name="intCodError"></param>
        /// <param name="strDesError"></param>
        /// <returns></returns>
        /// <remarks>GetAddDayWork</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static AddDayWorkResponse GetAddDayWork(string strIdSession, string strTransaction, string strFechaIni, int intNroDias, string strFechResult, int intCodError, string strDesError)
        {
            AddDayWorkResponse oAddDayWork = new AddDayWorkResponse();
            try
            {
                DbParameter[] parameters =
                {    
                   new DbParameter("P_FECHA_INI", DbType.String, 255,ParameterDirection.Input, strFechaIni), 
                   new DbParameter("P_NRO_DIAS", DbType.Int64, 255,ParameterDirection.Input, intNroDias), 
                    new DbParameter("P_FECHA_RESULT", DbType.String,20, ParameterDirection.Output),
                    new DbParameter("P_COD_ERROR", DbType.Int16,22, ParameterDirection.Output),
                    new DbParameter("P_DES_ERROR", DbType.String,1000, ParameterDirection.Output),
                };


                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_POST_SGA_P_AGREGAR_DIAS_LABORABLES, parameters);

                oAddDayWork = new AddDayWorkResponse();

                oAddDayWork.FechaResultado = parameters[2].Value.ToString();
                oAddDayWork.CodError = Convert.ToInt(parameters[3].Value.ToString());
                oAddDayWork.DescError = parameters[4].Value.ToString();
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession.ToString(), strTransaction, ex.Message);
            }

            return oAddDayWork;
        }

        //Name: Obtener Obtener Parametro Terminal TPI  
        /// <summary>
        /// obtiene parametro terminal TPI
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="parametroID"></param>
        /// <param name="Strmessage"></param>
        /// <returns></returns>
        /// <remarks>GetObtainParameterTerminalTPI</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<GenericItem> GetObtainParameterTerminalTPI(string strIdSession, string strTransaction, int parametroID, string Strmessage)
        {
            List<GenericItem> oLst = new List<GenericItem>();

            DbParameter[] parameters =
            {    
                new DbParameter("P_PARAMETRO_ID", DbType.Int16, 22,ParameterDirection.Input, parametroID), 
                new DbParameter("P_MENSAJE", DbType.String, 255,ParameterDirection.Input, Strmessage), 
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {

                Web.Logging.ExecuteMethod(strIdSession.ToString(), strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB,
                        DbCommandConfiguration.SIACU_POST_DB_SP_OBTENER_PARAMETRO, parameters, reader =>
                        {
                            while (reader.Read())
                            {

                                var item = new GenericItem
                                {

                                    ParameterID = reader["0"].ToString(),
                                    Nombre = reader["1"].ToString(),
                                    Descripcion = reader["2"].ToString(),
                                    Tipo = reader["3"].ToString(),
                                    Valor_C = reader["4"].ToString(),
                                };


                                oLst.Add(item);
                            }


                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLst;
        }


        //Name: Obtener Obtener Parametro Terminal Solo TFI Postpago 
        /// <summary>
        /// obtiene solo tfi
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="parametroID"></param>
        /// <param name="Strmessage"></param>
        /// <returns></returns>
        /// <remarks>GetSoloTFIPostpago</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<GenericItem> GetSoloTFIPostpago(string strIdSession, string strTransaction, int parametroID, string Strmessage)
        {
            List<GenericItem> oLst = new List<GenericItem>();

            DbParameter[] parameters =
            {    
                new DbParameter("P_PARAMETRO_ID", DbType.Int16, 22,ParameterDirection.Input, parametroID), 
                new DbParameter("P_MENSAJE", DbType.String, 255,ParameterDirection.Input, Strmessage), 
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {

                Web.Logging.ExecuteMethod(strIdSession.ToString(), strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB,
                        DbCommandConfiguration.SIACU_POST_COBS_SSSIGA_OBTENER_DATOS_BSCS_EXT, parameters, reader =>
                        {
                            if (reader.Read())
                            {

                                var item = new GenericItem
                                {

                                    ParameterID = reader["0"].ToString(),
                                    Nombre = reader["1"].ToString(),
                                    Descripcion = reader["2"].ToString(),
                                    Tipo = reader["3"].ToString(),
                                    Valor_C = reader["4"].ToString(),

                                };

                                oLst.Add(item);
                               
                            }

                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLst;
        }

        //Name: Obtener Datos BSCS Ext
        /// <summary>
        /// obtiene datos BSCS
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vNroTelefono"></param>
        /// <param name="vCodNuevoPlan"></param>
        /// <param name="rNroFacturas"></param>
        /// <param name="rCargoFijoActual"></param>
        /// <param name="rCargoFijoNuevoPlan"></param>
        /// <returns></returns>
        /// <remarks>ObtenerDatosBSCSExt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool ObtenerDatosBSCSExt(string strIdSession, string strTransaction, string vNroTelefono, double vCodNuevoPlan,
        ref double rNroFacturas, ref double rCargoFijoActual, ref double rCargoFijoNuevoPlan)
        {
            bool resultado = true;
            DbParameter[] parameters =
            {    
												   new DbParameter("p_nro_telefono", DbType.String,50,ParameterDirection.Input,vNroTelefono),
												   new DbParameter("p_tmcode_men", DbType.Double,10,ParameterDirection.Input,vCodNuevoPlan),
												   new DbParameter("p_num_fact", DbType.Double,10,ParameterDirection.Output,rNroFacturas),
												   new DbParameter("p_cargo_fijo_act", DbType.Double,10,ParameterDirection.Output,rCargoFijoActual),
												   new DbParameter("p_cargo_fijo_men", DbType.Double,10,ParameterDirection.Output,rCargoFijoNuevoPlan),
            };

            try
            {

                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                    {
                        DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_POST_COBS_SSSIGA_OBTENER_DATOS_BSCS_EXT, parameters);

                    });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession.ToString(), strTransaction, "Error ObtenerDatosBSCSExt :" + ex.Message.ToString());
                resultado = false;
                return resultado;
            }

            rNroFacturas = Convert.ToDouble(parameters[2].Value.ToString());
            rCargoFijoActual = Convert.ToDouble(parameters[3].Value.ToString());
            rCargoFijoNuevoPlan = Convert.ToDouble(parameters[4].Value.ToString());

            return resultado;
        }

        //Name: Obtener Obtener Penalidad Ext. 
        /// <summary>
        /// Obtiene penalidad
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vNroTelefono"></param>
        /// <param name="vFechaPenalidad"></param>
        /// <param name="vNroFacturas"></param>
        /// <param name="vCargoFijoActual"></param>
        /// <param name="vCargoFijoNuevoPlan"></param>
        /// <param name="vDiasxMes"></param>
        /// <param name="vCodNuevoPlan"></param>
        /// <param name="rAcuerdoIdSalida"></param>
        /// <param name="rDiasPendientes"></param>
        /// <param name="rCargoFijoDiario"></param>
        /// <param name="rPrecioLista"></param>
        /// <param name="rPrecioVenta"></param>
        /// <param name="rPenalidadPCS"></param>
        /// <param name="rPenalidaAPADECE"></param>
        /// <returns></returns>
        /// <remarks>GetObtainPenalidadExt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetObtainPenalidadExt(string strIdSession, string strTransaction, string vNroTelefono, DateTime vFechaPenalidad,
            double vNroFacturas, double vCargoFijoActual, double vCargoFijoNuevoPlan, double vDiasxMes, double vCodNuevoPlan,
            ref double rAcuerdoIdSalida, ref double rDiasPendientes, ref double rCargoFijoDiario, ref double rPrecioLista, ref double rPrecioVenta,
            ref double rPenalidadPCS, ref double rPenalidaAPADECE)
        {
            bool resultado = true;

            DbParameter[] parameters =
            {    
													   new DbParameter("p_acuerdo_id", DbType.Double,10,ParameterDirection.Input,"0"),
													   new DbParameter("p_nro_telefono", DbType.String,50,ParameterDirection.Input,vNroTelefono),
													   new DbParameter("p_fecha_penalidad", DbType.DateTime,10,ParameterDirection.Input,vFechaPenalidad),
													   new DbParameter("p_numero_facturas", DbType.Double,10,ParameterDirection.Input,vNroFacturas),
													   new DbParameter("p_cargo_fijo_act", DbType.Double,10,ParameterDirection.Input,vCargoFijoActual),
													   new DbParameter("p_cargo_fijo_inf", DbType.Double,10,ParameterDirection.Input,vCargoFijoNuevoPlan),
													   new DbParameter("p_diasxmes", DbType.Double,10,ParameterDirection.Input,vDiasxMes),
													   new DbParameter("p_codigo_plan_nuevo", DbType.Double,10,ParameterDirection.Input,vCodNuevoPlan),
													   new DbParameter("p_acuerdo_id_salida", DbType.Double,22,ParameterDirection.Output,rAcuerdoIdSalida),
													   new DbParameter("p_dias_pendientes", DbType.Double,22,ParameterDirection.Output,rDiasPendientes),
													   new DbParameter("p_cargo_fijo_diario", DbType.Double,22,ParameterDirection.Output,rCargoFijoDiario),
													   new DbParameter("p_precio_lista", DbType.Double,22,ParameterDirection.Output,rPrecioLista),
													   new DbParameter("p_precio_venta", DbType.Double,22,ParameterDirection.Output,rPrecioVenta),
													   new DbParameter("p_monto_pcs", DbType.Double,10,ParameterDirection.Output,rPenalidadPCS),
													   new DbParameter("p_monto_apadece", DbType.Double,10,ParameterDirection.Output,rPenalidaAPADECE),

                
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                    {
                        DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SIGA,
                            DbCommandConfiguration.SIACU_POST_SIGA_SSSIGA_OBTENER_PENALIDAD_EXT, parameters);


                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession.ToString(), strTransaction, ex.Message);
                resultado = false;
                return resultado;
            }
            rAcuerdoIdSalida = (parameters[8].Value.ToString() == "null") ? 0.00 : Convert.ToDouble(parameters[8].Value.ToString());
            rDiasPendientes = (parameters[9].Value.ToString() == "null") ? 0.00 : Convert.ToDouble(parameters[9].Value.ToString());
            rCargoFijoDiario = (parameters[10].Value.ToString() == "null") ? 0.00 : Convert.ToDouble(parameters[10].Value.ToString());
            rPrecioLista = (parameters[11].Value.ToString() == "null") ? 0.00 : Convert.ToDouble(parameters[11].Value.ToString());
            rPrecioVenta = (parameters[12].Value.ToString() == "null") ? 0.00 : Convert.ToDouble(parameters[12].Value.ToString());
            rPenalidadPCS = (parameters[13].Value.ToString() == "null") ? 0.00 : Convert.ToDouble(parameters[13].Value.ToString());
            rPenalidaAPADECE = (parameters[14].Value.ToString() == "null") ? 0.00 : Convert.ToDouble(parameters[14].Value.ToString());

            return resultado;

        }

        //Name: Validar el CustomerId
        /// <summary>
        /// Valida el CustomerId
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vPHONE"></param>
        /// <param name="vCONTACTOBJID"></param>
        /// <param name="strflgResult"></param>
        /// <param name="strMSError"></param>
        /// <returns></returns>
        /// <remarks>GetValidateCustomerId</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetValidateCustomerId(string strIdSession, string strTransaction,
                                              string vPHONE, ref int vCONTACTOBJID,
                                              ref string strflgResult, ref string strMSError)
        {
            bool resultado = true;

            DbParameter[] parameters =
            {    
                                               new DbParameter("p_phone", DbType.String,30,ParameterDirection.Input,vPHONE),												   
                                               new DbParameter("p_contactobjid", DbType.Int64,22,ParameterDirection.Output,vCONTACTOBJID),
                                               new DbParameter("p_flag_insert", DbType.String,255,ParameterDirection.Output,strflgResult),												   
                                               new DbParameter("p_msg_text", DbType.String,255,ParameterDirection.Output,strMSError	),											 

            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                 {
                     DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SA_SP_SEARCH_CONTACT_USERLDI, parameters);

                 });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            vCONTACTOBJID = Convert.ToInt(parameters[1].Value.ToString());
            strflgResult = parameters[2].Value.ToString();
            strMSError = parameters[3].Value.ToString();

            return resultado;
        }


        // Name: Registrar Cliente 
        /// <summary>
        /// Registrar Cliente 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="oItem"></param>
        /// <param name="strflgResult"></param>
        /// <param name="strMSError"></param>
        /// <returns></returns>
        /// <remarks>GetRegisterCustomerId</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetRegisterCustomerId(string strIdSession, string strTransaction,
                                             Customer oItem, ref string strflgResult, ref string strMSError)
        {
            bool resultado = true;
            DbParameter[] parameters =
            {    
                                                   new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input,oItem.TELEFONO),
                                                   new DbParameter("P_USUARIO", DbType.String,255,ParameterDirection.Input, oItem.USUARIO),
                                                   new DbParameter("P_NOMBRES", DbType.String,255,ParameterDirection.Input, oItem.NOMBRES),
                                                   new DbParameter("P_APELLIDOS", DbType.String,255,ParameterDirection.Input, oItem.APELLIDOS),
                                                   new DbParameter("P_RAZONSOCIAL", DbType.String,255,ParameterDirection.Input, oItem.RAZON_SOCIAL),
                                                   new DbParameter("P_TIPO_DOC", DbType.String,255,ParameterDirection.Input, oItem.TIPO_DOC),
                                                   new DbParameter("P_NUM_DOC", DbType.String,255,ParameterDirection.Input, oItem.NRO_DOC),
                                                   new DbParameter("P_DOMICILIO", DbType.String,255,ParameterDirection.Input, oItem.DOMICILIO),
                                                   new DbParameter("P_DISTRITO", DbType.String,255,ParameterDirection.Input, oItem.DISTRITO),													  
                                                   new DbParameter("P_DEPARTAMENTO", DbType.String,255,ParameterDirection.Input, oItem.DEPARTAMENTO),													  
                                                   new DbParameter("P_PROVINCIA", DbType.String,255,ParameterDirection.Input, oItem.PROVINCIA),													  												   
                                                   new DbParameter("P_MODALIDAD", DbType.String,255,ParameterDirection.Input, oItem.MODALIDAD),
                                                   new DbParameter("P_FLAG_INSERT", DbType.String,255,ParameterDirection.Output, strflgResult),
                                                   new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output,strMSError),									 

            };

            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = DBNull.Value;

            int i = 0;
            if (oItem.TELEFONO != null)
                parameters[i].Value = oItem.TELEFONO;

            i++;
            if (oItem.USUARIO != null)
                parameters[i].Value = oItem.USUARIO;

            i++;
            if (oItem.NOMBRES != null)
            {
                if (oItem.NOMBRES.Length > 30)
                {
                    parameters[i].Value = oItem.NOMBRES.Substring(0, 30);
                }
                else
                {
                    parameters[i].Value = oItem.NOMBRES;
                }
            }

            i++;
            if (oItem.APELLIDOS != null)
            {
                if (oItem.APELLIDOS.Length > 30)
                {
                    parameters[i].Value = oItem.APELLIDOS.Substring(0, 30);
                }
                else
                {
                    parameters[i].Value = oItem.APELLIDOS;
                }
            }

            i++;
            if (oItem.RAZON_SOCIAL != null)
            {
                if (oItem.RAZON_SOCIAL.Length > 30)
                {
                    parameters[i].Value = oItem.RAZON_SOCIAL.Substring(0, 30);
                }
                else
                {
                    parameters[i].Value = oItem.RAZON_SOCIAL;
                }
            }

            i++;
            if (oItem.TIPO_DOC != null)
                parameters[i].Value = "DNI";

            i++;
            if (oItem.NRO_DOC != null)
                parameters[i].Value = oItem.NRO_DOC;

            i++;
            if (oItem.DOMICILIO != null)
            {
                if (oItem.DOMICILIO.Length > 200)
                {
                    parameters[i].Value = oItem.DOMICILIO.Substring(0, 200);
                }
                else
                {
                    parameters[i].Value = oItem.DOMICILIO;
                }
            }

            i++;
            if (oItem.DISTRITO != null)
            {
                if (oItem.DISTRITO.Length > 200)
                {
                    parameters[i].Value = oItem.DISTRITO.Substring(0, 200);
                }
                else
                {
                    parameters[i].Value = oItem.DISTRITO;
                }
            }

            i++;
            if (oItem.DEPARTAMENTO != null)
            {
                if (oItem.DEPARTAMENTO.Length > 40)
                {
                    parameters[i].Value = oItem.DEPARTAMENTO.Substring(0, 40);
                }
                else
                {
                    parameters[i].Value = oItem.DEPARTAMENTO;
                }
            }

            i++;
            if (oItem.PROVINCIA != null)
            {
                if (oItem.PROVINCIA.Length > 200)
                {
                    parameters[i].Value = oItem.PROVINCIA.Substring(0, 200);
                }
                else
                {
                    parameters[i].Value = oItem.PROVINCIA;
                }
            }

            i++;
            if (oItem.MODALIDAD != null)
                parameters[i].Value = oItem.MODALIDAD;
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_CONTACT_USERLDI, parameters);

                });

                strflgResult = parameters[12].Value.ToString();
                strMSError = parameters[13].Value.ToString();

                if (strflgResult == "NO OK")
                    resultado = false;


            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession.ToString(), strTransaction, ex.Message);

            }

            return resultado;
        }


      
        /// <summary>
        /// inserta caso
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="oItem"></param>
        /// <param name="rCasoId"></param>
        /// <param name="rFlagInsercion"></param>
        /// <param name="rMsgText"></param>
        /// <returns></returns>
        /// <remarks>GetCaseInsert</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string GetCaseInsert(string strIdSession, string strTransaction, CaseInsertRequest oItem, ref string rCasoId, ref string rFlagInsercion, ref string rMsgText)
        {

            DbParameter[] parameters =
            {   
						new DbParameter("PN_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input,oItem.OBJID_CONTACTO),
						new DbParameter("PV_SITEOBJID_1", DbType.Int64,ParameterDirection.Input,oItem.OBJID_SITE),
						new DbParameter("PV_ACCOUNT", DbType.String,255,ParameterDirection.Input,oItem.ACCOUNT),
						new DbParameter("PV_PHONE", DbType.String,255,ParameterDirection.Input,oItem.PHONE),
						new DbParameter("PV_TIPO", DbType.String,255,ParameterDirection.Input,oItem.TIPO),
						new DbParameter("PV_CLASE", DbType.String,255,ParameterDirection.Input,oItem.CLASE),
						new DbParameter("PV_SUBCLASE", DbType.String,255,ParameterDirection.Input,oItem.SUBCLASE),
						new DbParameter("PV_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input,oItem.METODO_CONTACTO),
						new DbParameter("PV_PRIORIDAD", DbType.String,255,ParameterDirection.Input,oItem.PRIORIDAD),
						new DbParameter("PV_SEVERIDAD", DbType.String,255,ParameterDirection.Input,oItem.SEVERIDAD),
						new DbParameter("PV_COLA", DbType.String,255,ParameterDirection.Input,oItem.COLA),
						new DbParameter("PV_FLAG_INTERACT", DbType.String,255,ParameterDirection.Input,oItem.FLAG_INTERACCION),
						new DbParameter("PV_USR_PROCESO", DbType.String,255,ParameterDirection.Input,oItem.USUARIO_PROCESO),
						new DbParameter("PV_USUARIO", DbType.String,255,ParameterDirection.Input,oItem.USUARIO_ID),
						new DbParameter("PV_TIPO_INTER", DbType.String,255,ParameterDirection.Input,oItem.TIPO_INTERACCION),
						new DbParameter("PV_NOTAS", DbType.String,4000,ParameterDirection.Input,oItem.NOTAS),
						new DbParameter("ID_CASO", DbType.String,255,ParameterDirection.Output,rCasoId),
						new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output,rFlagInsercion),
						new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output,rMsgText),	

            };
            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = DBNull.Value;

            int i = 0;
            if (oItem.OBJID_CONTACTO != null)
                parameters[i].Value = 0;
            i++;
            if (oItem.OBJID_SITE != null)
                parameters[i].Value = oItem.OBJID_SITE; 

            i++;
            if (oItem.CUENTA != null)
                parameters[i].Value = oItem.CUENTA;
            i++;
            if (oItem.TELEFONO != null)
                parameters[i].Value = oItem.TELEFONO;
            i++;

            if (oItem.TIPIFICACION != null)
                parameters[i].Value = oItem.TIPIFICACION;
            i++;

            if (oItem.CLASE != null)
                parameters[i].Value = oItem.CLASE;
            i++;

            if (oItem.SUBCLASE != null)
                parameters[i].Value = oItem.SUBCLASE;
            i++;

            if (oItem.METODO_CONTACTO != null)
                parameters[i].Value = oItem.METODO_CONTACTO;
            i++;

            if (oItem.PRIORIDAD != null)
                parameters[i].Value = oItem.PRIORIDAD;
            i++;

            if (oItem.SEVERIDAD != null)
                parameters[i].Value = oItem.SEVERIDAD;
            i++;

            if (oItem.COLA != null)
                parameters[i].Value = oItem.COLA;
            i++;

            if (oItem.FLAG_INTERACCION != null)
                parameters[i].Value = oItem.FLAG_INTERACCION;
            i++;

            if (oItem.USUARIO_PROCESO != null)
                parameters[i].Value = oItem.USUARIO_PROCESO;
            i++;

            if (oItem.USUARIO_ID != null)
                parameters[i].Value = oItem.USUARIO_ID;
            i++;

            if (oItem.TIPO_INTERACCION != null)
                parameters[i].Value = oItem.TIPO_INTERACCION;
            i++;

            if (oItem.NOTAS != null)
                parameters[i].Value = oItem.NOTAS;
            i++;

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_SGA_P_REGISTRA_ETA_SEL, parameters);

                });

                rMsgText = parameters[19].Value.ToString();
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                rMsgText = ex.Message;
            }

            return rMsgText;
        }

        // Name: Obtener Apadece Cancelacion Ret
        /// <summary>
        /// obtiene apadece retencion
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="numTelef"></param>
        /// <param name="codId"></param>
        /// <param name="rdbValorApadece"></param>
        /// <param name="rintCodError"></param>
        /// <param name="rp_msg_text"></param>
        /// <returns></returns>
        /// <remarks>GetApadeceCancelRet</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static RetentionCancelServicesResponse GetApadeceCancelRet(string strIdSession, string strTransaction, int numTelef, int codId,
                                                ref double rdbValorApadece, ref string rintCodError, ref string rp_msg_text)
        {
            RetentionCancelServicesResponse oResponse = new RetentionCancelServicesResponse();


            DbParameter[] parameters =
            {   
                                    new DbParameter("PI_MSISDN", DbType.Int64,22,ParameterDirection.Input,numTelef), 
                                    new DbParameter("PI_COD_ID", DbType.Int64,22,ParameterDirection.Input,codId),
                                    new DbParameter("PI_ACUERDO_ID", DbType.Int64,22,ParameterDirection.Input,DBNull.Value),
                                    new DbParameter("PI_FECHA_TRANSACCION", DbType.String,10,ParameterDirection.Input,DBNull.Value),
                                    new DbParameter("PI_TIPO_ACUERDO", DbType.Int64,22,ParameterDirection.Input,DBNull.Value),
                                    new DbParameter("PI_MOTIVO_APADECE", DbType.Int64,22,ParameterDirection.Input,3),
                                    new DbParameter("PI_CF_NUEVO", DbType.Double,ParameterDirection.Input,0),
                                    new DbParameter("PI_FLG_EQUIPO", DbType.Int64,22,ParameterDirection.Input,DBNull.Value),												   												   
                                    new DbParameter("PI_ACUERDO_VIGENTE", DbType.Int64,22,ParameterDirection.Input,0),												   
                                    new DbParameter("PO_MONTO_APADECE", DbType.Double,22,ParameterDirection.Output,rdbValorApadece),
                                    new DbParameter("PO_TIPO_PRODUCTO", DbType.String,10,ParameterDirection.Output,DBNull.Value),
                                    new DbParameter("PO_CODERROR", DbType.String,22,ParameterDirection.Output,rintCodError),
                                    new DbParameter("PO_DESERROR", DbType.String,1000,ParameterDirection.Output,rp_msg_text),
                                    new DbParameter("CUR_SEC", DbType.Object,ParameterDirection.Output),

            };


            try
            {

                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SIGA,
                    DbCommandConfiguration.SIACU_POST_SIGA_SP_OBTENER_APADECE, parameters, reader =>
                    {

                    });

                oResponse.ValorApadece = (parameters[10].Value.ToString() == "null" ? 0.00 : Convert.ToDouble(parameters[10].Value.ToString()));
                oResponse.CodMessage = parameters[12].Value.ToString();
                oResponse.Message = parameters[13].Value.ToString();



            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                oResponse.CodMessage = Claro.SIACU.Constants.NO_OK;
            }

            return oResponse;
        }

        /// <summary>
        /// desactiva contrato
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetDesactivatedContract</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetDesactivatedContract(Customer objRequest)
        {
            bool resultado = false;
            try
            {

                CUSTOMER_HFC.desactivarContratoEAIRequest objClienteResquest = new CUSTOMER_HFC.desactivarContratoEAIRequest();
                CUSTOMER_HFC.DesactivarContratoEAIInput oDesactivarContratoEAIInput = new CUSTOMER_HFC.DesactivarContratoEAIInput();
                CUSTOMER_HFC.desactivarContratoEAIResponse objClienteResponse = new CUSTOMER_HFC.desactivarContratoEAIResponse();
                CUSTOMER_HFC.CabeceraRequest oCabeceraRequest = new CUSTOMER_HFC.CabeceraRequest();
                CUSTOMER_HFC.CuerpoDESARequest oCuerpoDESARequest = new CUSTOMER_HFC.CuerpoDESARequest();


                oCabeceraRequest.idTransaccion = objRequest.Audit.Transaction;
                oCabeceraRequest.ipAplicacion = objRequest.Audit.IPAddress;
                oCabeceraRequest.nombreAplicacion = objRequest.ApplicationName;
                oCabeceraRequest.usuarioAplicacion = objRequest.UserApplication;
                oDesactivarContratoEAIInput.cabeceraRequest = oCabeceraRequest;
                #region Datos del Cliente
                oCuerpoDESARequest.codigoCliente = objRequest.CUSTOMER_ID;
                oCuerpoDESARequest.codigoCuenta = objRequest.CUENTA;
                oCuerpoDESARequest.codigoContrato = objRequest.CONTRATO_ID;
                oCuerpoDESARequest.cacDac = objRequest.Des_CAC;
                oCuerpoDESARequest.cicloFacturacion = objRequest.CICLO_FACTURACION;
                oCuerpoDESARequest.msisdn = objRequest.TELEFONO;
                oCuerpoDESARequest.reason = objRequest.Reason;
                oCuerpoDESARequest.codigoMotivo = objRequest.COD_MOTIVE;
                oCuerpoDESARequest.fechaActual = objRequest.FECHA_ACT.ToString("yyyy-MM-dd");  //Convert.ToString(objRequest.FECHA_ACT);
                oCuerpoDESARequest.flagNdPcs = objRequest.FLAG_ND_PCS;
                oCuerpoDESARequest.flagOccApadece = objRequest.FLAG_OCC_APADECE;
                oCuerpoDESARequest.montoFidelizacion = objRequest.MONTO_FIDELIZACION;
                oCuerpoDESARequest.montoPCS = objRequest.MONTO_PCS;
                oCuerpoDESARequest.FechaProgramacion = objRequest.FECHA_PROGRAMACION;
                oCuerpoDESARequest.tipoServicio = objRequest.TIPO_SERVICIO;
                oCuerpoDESARequest.numeroDocumento = objRequest.NRO_DOC;
                oCuerpoDESARequest.codigoPlano = objRequest.CODIGO_PLANO_FACT;
                oCuerpoDESARequest.subMotivoPCS = objRequest.SUB_MOTIVO_PCS;
                oCuerpoDESARequest.tipoCliente = objRequest.TIPO_CLIENTE;
                oCuerpoDESARequest.observaciones = objRequest.OBSERVACIONES;
                oCuerpoDESARequest.motivoPCS = objRequest.MOTIVO_PCS;
                oCuerpoDESARequest.montoPenalidad = objRequest.MONTO_PENALIDAD;
                oCuerpoDESARequest.mailUsuarioAplicacion = objRequest.EMAIL;
                oCuerpoDESARequest.areaPCS = objRequest.AREA_PCS;
                oCuerpoDESARequest.codigoInteraccion = objRequest.CODIGO_INTERACCION;
                oCuerpoDESARequest.codigoServicio = objRequest.CODIGO_SERVICIO;
                oCuerpoDESARequest.FechaProgramacionSOT = objRequest.FECHA_PROGRAMACION_SOT;
                oCuerpoDESARequest.franjaHoraria = objRequest.FRANJA_HORARIO;
                oCuerpoDESARequest.trace = objRequest.TRACE;
                oCuerpoDESARequest.tipTra = objRequest.TIPO_TRABAJO;
                oCuerpoDESARequest.usuarioAsesor = objRequest.ASESOR;
                #endregion
                oDesactivarContratoEAIInput.cuerpoRequest = oCuerpoDESARequest;
                objClienteResquest.desactivarContratoEaiRequest = oDesactivarContratoEAIInput;
                objClienteResponse = ServiceConfiguration.FIXED_CUSTOMER_HFC.desactivarContrato(objClienteResquest);

                if (objClienteResponse.desactivarContratoEaiResponse.cabeceraResponse.codigoRespuesta == Claro.Constants.NumberZeroString)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }


            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - desactivarContrato_HFC:" + ex.Message);
                return false;
            }

            return resultado;

        }
        /// <summary>
        /// desactiva contrato lte
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetDesactivatedContract_LTE</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetDesactivatedContract_LTE(Customer objRequest, ref string message)
        {
            bool resultado = false;

            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Inicio Método GetDesactivatedContract_LTE IFI");
            try
            {
                CUSTOMER_LTE.desactivarContratoEAIRequest oRequest = new CUSTOMER_LTE.desactivarContratoEAIRequest();
                CUSTOMER_LTE.desactivarContratoEAIResponse oResponse = new CUSTOMER_LTE.desactivarContratoEAIResponse();
                CUSTOMER_LTE.ServicioPorCodigoClienteType[] oTempServicio = new CUSTOMER_LTE.ServicioPorCodigoClienteType[0];
                CUSTOMER_LTE.AuditRequestType objAuditRequest = new CUSTOMER_LTE.AuditRequestType();
                CUSTOMER_LTE.AuditResponseType objAuditResponse = new CUSTOMER_LTE.AuditResponseType();

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
                oRequest.listaRequestOpcional = new CUSTOMER_LTE.ListaCamposOpcionalesTypeCampoOpcional[1] {
                    
                    new CUSTOMER_LTE.ListaCamposOpcionalesTypeCampoOpcional(){
                
                                campo="PRODUCTO",
                                valor="IFI"
                
                }
              };

                oResponse = ServiceConfiguration.FIXED_CUSTOMER_LTE.desactivarContrato(oRequest);
                objAuditResponse = oResponse.auditResponse;
                if (objAuditResponse.codigoRespuesta == "0")
                {
                    resultado = true;
                    message = "";
                }
                else
                {
                    message = objAuditResponse.mensajeRespuesta;
                    resultado = false;
                }

                return resultado;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - desactivarContrato_LTE :" + ex.Message);
                message ="Ocurrio un Error en la Transaccion";
                return false;
            }


        }
        /// <summary>
        /// crea caso
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
        public static CaseInsertResponse GetCreateCase(CaseInsertRequest oRequest)
        {
            CaseInsertResponse oResponse = new CaseInsertResponse();
            try
            {

                DbParameter[] parameters =
                {   


                            #region Parametros InsertCaso
                                        new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input,oRequest.CONTRATO),
				                        new DbParameter("P_SITEOBJID_1", DbType.Int64,ParameterDirection.Input,oRequest.OBJID_SITE),
				                        new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input,oRequest.CONTRATO),
				                        new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input,oRequest.TELEFONO),
				                        new DbParameter("P_TIPO", DbType.String,255,ParameterDirection.Input,oRequest.TIPO),
				                        new DbParameter("P_CLASE", DbType.String,255,ParameterDirection.Input,oRequest.CLASE),
				                        new DbParameter("P_SUBCLASE", DbType.String,255,ParameterDirection.Input,oRequest.SUBCLASE),
				                        new DbParameter("P_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input,oRequest.METODO_CONTACTO),
				                        new DbParameter("P_PRIORIDAD", DbType.String,255,ParameterDirection.Input,oRequest.PRIORIDAD),
				                        new DbParameter("P_SEVERIDAD", DbType.String,255,ParameterDirection.Input,oRequest.SEVERIDAD),
				                        new DbParameter("P_COLA", DbType.String,255,ParameterDirection.Input,oRequest.COLA),
				                        new DbParameter("P_FLAG_INTERACT", DbType.String,255,ParameterDirection.Input,oRequest.FLAG_INTERACCION),
				                        new DbParameter("P_USR_PROCESO", DbType.String,255,ParameterDirection.Input,oRequest.USUARIO_PROCESO),
				                        new DbParameter("P_USUARIO", DbType.String,255,ParameterDirection.Input,oRequest.USUARIO_ID),
				                        new DbParameter("P_TIPO_INTER", DbType.String,255,ParameterDirection.Input,oRequest.TIPO_INTERACCION),
				                        new DbParameter("P_NOTAS", DbType.String,4000,ParameterDirection.Input,oRequest.NOTAS),
 				                        new DbParameter("P_SERVAFECT", DbType.String,255,ParameterDirection.Input,oRequest.SERVICIO),
 				                        new DbParameter("P_INCONVEN", DbType.String,255,ParameterDirection.Input,oRequest.INCONVENIENTE),
 				                        new DbParameter("P_SERVAFECT_CODE", DbType.String,255,ParameterDirection.Input,oRequest.SERVICIO_CODE),
 				                        new DbParameter("P_INCONVEN_CODE", DbType.String,255,ParameterDirection.Input,oRequest.INCONVENIENTE_CODE),
 				                        new DbParameter("P_CO_ID", DbType.String,255,ParameterDirection.Input,oRequest.CONTRATO),
 				                        new DbParameter("P_COD_PLANO", DbType.String,255,ParameterDirection.Input,oRequest.PLANO),
 				                        new DbParameter("P_VALOR1", DbType.String,255,ParameterDirection.Input,oRequest.VALOR_1),
 				                        new DbParameter("P_VALOR2", DbType.String,255,ParameterDirection.Input,oRequest.VALOR_2),
 				                        new DbParameter("P_DUMMY_ID", DbType.String,255,ParameterDirection.Input,oRequest.DUMMY_ID),
 				                        new DbParameter("P_CASE_FATHER", DbType.String,255,ParameterDirection.Input,oRequest.CASO_PADRE_ID),
				                        new DbParameter("ID_CASO", DbType.String,255,ParameterDirection.Output,oResponse.rCasoId),
				                        new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output,oResponse.rFlagInsercion),
				                        new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output,oResponse.rMsgText),	
    #endregion
                };
                Web.Logging.ExecuteMethod(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(oRequest.Audit.Session, oRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_CLARIFY_CREATE_CASE_HFC, parameters);

                });
                oResponse.rCasoId = parameters[26].Value.ToString();
                oResponse.rFlagInsercion = parameters[27].Value.ToString();
                oResponse.rMsgText = parameters[28].Value.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, ex.Message);
            }


            return oResponse;
        }
        /// <summary>
        /// obtiene motivo sot por tipo de trabajo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="tipTra"></param>
        /// <returns></returns>
        /// <remarks>GetMotiveSOTByTypeJob</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<GenericItem> GetMotiveSOTByTypeJob(string strIdSession, string strTransaction, int tipTra)
        {
            List<GenericItem> oLstMotive = new List<GenericItem>();
            DbParameter[] parameters =
            {   new DbParameter("pi_tiptra", DbType.Int64, 300,ParameterDirection.Input, tipTra), 
                new DbParameter("po_motivos", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_POST_SGA_SGASS_MOTIVOS_TIPTRA, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                var item = new GenericItem
                                {
                                    Codigo = reader["CODMOTOT"].ToString(),
                                    Descripcion = reader["DESCRIPCION"].ToString()
                                };
                                oLstMotive.Add(item);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLstMotive;
        }
    }
}