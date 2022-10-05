using Claro.Data;

namespace Claro.SIACU.Data.IFI.Configuration
{
    internal struct DbCommandConfiguration : IDbCommandConfig
    {


        public static readonly IDbCommandConfig SIACU_SP_COD_UBIGEO = DbCommandConfiguration.Create("SIACU_SP_COD_UBIGEO");
        public static readonly IDbCommandConfig SIACU_CONSULTA_POSTT_SERVICIOPROG = DbCommandConfiguration.Create("SIACU_CONSULTA_POSTT_SERVICIOPROG");
        public static readonly IDbCommandConfig SIACU_SP_UPDATE_INTERACT_X_INTER29 = DbCommandConfiguration.Create("SIACU_SP_UPDATE_INTERACT_X_INTER29");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_DATO = DbCommandConfiguration.Create("SIACU_SP_OBTENER_DATO");
        public static readonly IDbCommandConfig SIACU_POST_DB_SP_OBTENER_PARAMETRO = DbCommandConfiguration.Create("SIACU_OBTENER_PARAMETRO");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_NUMERO = DbCommandConfiguration.Create("SIACU_SP_OBTENER_NUMERO");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_NUMERO_PORT = DbCommandConfiguration.Create("SIACU_SP_OBTENER_NUMERO_PORT");
        public static readonly IDbCommandConfig SIACU_SP_EVALUAR_MONTO_AUTORIZAR_DCM = Create("SIACU_SP_EVALUAR_MONTO_AUTORIZAR_DCM");
        public static readonly IDbCommandConfig SIACU_SP_EVALUAR_MONTO_AUTORIZAR = Create("SIACU_SP_EVALUAR_MONTO_AUTORIZAR");
        public static readonly IDbCommandConfig SIACU_SP_SHOW_LIST_ELEMENT = Create("SIACU_SP_SHOW_LIST_ELEMENT");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_CODIGO = DbCommandConfiguration.Create("SIACU_SP_OBTENER_CODIGO");
        public static readonly IDbCommandConfig SIACU_TFUN051_GET_DNNUM_FROM_COID = Create("SIACU_TFUN051_GET_DNNUM_FROM_COID");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_QUERY_PLUS_INTER = Create("SIACU_POST_CLARIFY_SP_QUERY_PLUS_INTER");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CREATE_INTERACT_HFC = Create("SIACU_POST_CLARIFY_SP_CREATE_INTERACT_HFC");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC = Create("SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CREATE_PLUS_INTER = Create("SIACU_POST_CLARIFY_SP_CREATE_PLUS_INTER");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_QUERY_INTERACT = Create("SIACU_POST_CLARIFY_SP_QUERY_INTERACT");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CREATE_INTERACT = Create("SIACU_POST_CLARIFY_SP_CREATE_INTERACT");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY = Create("SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY");
        public static readonly IDbCommandConfig SIACU_CONSULTAR_REGLAS_ATENCION = DbCommandConfiguration.Create("SIACU_SP_CONSULTAR_REGLAS_ATENCION");
        public static readonly IDbCommandConfig SIACU_OBTENER_TIPIFICACIONES = DbCommandConfiguration.Create("SIACU_SP_OBTENER_TIPIFICACION");
        public static readonly IDbCommandConfig SIACU_POST_DB_SP_INSERTAR_INTERACT = Create("SIACU_POST_DB_SP_INSERTAR_INTERACT");
        public static readonly IDbCommandConfig SIACU_POST_DB_SP_INSERTAR_X_PLUS_INTER = Create("SIACU_POST_DB_SP_INSERTAR_X_PLUS_INTER");
        public static readonly IDbCommandConfig SIACU_SP_ST_CONSULTAS_USUARIO = Create("SIACU_SP_ST_CONSULTAS_USUARIO");
        public static readonly IDbCommandConfig SIACU_POST_PVU_LISTA_ACCIONES_RETENCION = DbCommandConfiguration.Create("SIACU_POST_PVU_LISTA_ACCIONES_RETENCION");
        public static readonly IDbCommandConfig SIACU_POST_PVU_LISTAR_MOTIVOS_RETENCION = DbCommandConfiguration.Create("SIACU_POST_PVU_LISTAR_MOTIVOS_RETENCION");
        public static readonly IDbCommandConfig SIACU_POST_PVU_LISTAR_SUBMOTIVOS_RETENCION = DbCommandConfiguration.Create("SIACU_POST_PVU_LISTAR_SUBMOTIVOS_RETENCION");
        public static readonly IDbCommandConfig SIACU_SECSS_CON_PARAMETRO_GP = DbCommandConfiguration.Create("SIACU_SECSS_CON_PARAMETRO_GP");
        public static readonly IDbCommandConfig SIACU_POST_SGA_P_CONSULTA_MOTIVO = DbCommandConfiguration.Create("SIACU_POST_SGA_P_CONSULTA_MOTIVO");
        public static readonly IDbCommandConfig SIACU_POST_SGA_P_AGREGAR_DIAS_LABORABLES = DbCommandConfiguration.Create("SIACU_POST_SGA_P_AGREGAR_DIAS_LABORABLES");
        public static readonly IDbCommandConfig SIACU_POST_COBS_SSSIGA_OBTENER_DATOS_BSCS_EXT = DbCommandConfiguration.Create("SIACU_POST_COBS_SSSIGA_OBTENER_DATOS_BSCS_EXT");
        public static readonly IDbCommandConfig SIACU_POST_SIGA_SSSIGA_OBTENER_PENALIDAD_EXT = DbCommandConfiguration.Create("SIACU_POST_SIGA_SSSIGA_OBTENER_PENALIDAD_EXT");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CREATE_CONTACT_USERLDI = DbCommandConfiguration.Create("SIACU_POST_CLARIFY_SP_CREATE_CONTACT_USERLDI");
        public static readonly IDbCommandConfig SIACU_POST_SGA_P_REGISTRA_ETA_SEL = DbCommandConfiguration.Create("SIACU_POST_SGA_P_REGISTRA_ETA_SEL");
        public static readonly IDbCommandConfig SIACU_POST_SIGA_SP_OBTENER_APADECE = DbCommandConfiguration.Create("SIACU_POST_SIGA_SP_OBTENER_APADECE");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_CREATE_CASE_HFC = DbCommandConfiguration.Create("SIACU_POST_CLARIFY_CREATE_CASE_HFC");
        public static readonly IDbCommandConfig SIACU_SP_UPDATE_PLUS_CASE = DbCommandConfiguration.Create("SIACU_SP_UPDATE_PLUS_CASE");
        public static readonly IDbCommandConfig SIACU_SP_INTERACT_ID_HFC = DbCommandConfiguration.Create("SIACU_SP_INTERACT_ID_HFC");
        public static readonly IDbCommandConfig SIACU_POST_COBS_INSERTAR_CASE = DbCommandConfiguration.Create("SIACU_POST_COBS_INSERTAR_CASE");
        public static readonly IDbCommandConfig SIACU_PS_CREATE_PLUS_CASE = DbCommandConfiguration.Create("SIACU_PS_CREATE_PLUS_CASE");
        public static readonly IDbCommandConfig SIACU_POST_INSERTAR_X_PLUS_CASE = DbCommandConfiguration.Create("SIACU_POST_INSERTAR_X_PLUS_CASE");
        public static readonly IDbCommandConfig SIACU_CONSULTA_POSTT_SERVICIOPROG_HFC = DbCommandConfiguration.Create("SIACU_CONSULTA_POSTT_SERVICIOPROG_HFC");
        public static readonly IDbCommandConfig SIACU_POST_SGA_SGASS_MOTIVOS_TIPTRA = DbCommandConfiguration.Create("SIACU_POST_SGA_SGASS_MOTIVOS_TIPTRA");
        public static readonly IDbCommandConfig SIACU_POST_PVU_SP_INSERTAR_EVIDENCIA_A = DbCommandConfiguration.Create("SIACU_POST_PVU_SP_INSERTAR_EVIDENCIA_A");
        public static readonly IDbCommandConfig SIACU_POST_SIGA_CONSULTA_ACUERDO = DbCommandConfiguration.Create("SIACU_POST_SIGA_CONSULTA_ACUERDO");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_CARGOFIJO_SERV_X_CLIENTE = DbCommandConfiguration.Create("SIACU_POST_BSCS_CARGOFIJO_SERV_X_CLIENTE");
        public static readonly IDbCommandConfig SIACU_COMMON_SP_OBTENER_DATO = Create("SIACU_SP_OBTENER_DATO");
        public static readonly IDbCommandConfig SIACU_COMMON_SP_INSERTAR_LOG_TRX = Create("SIACU_SP_INSERTAR_LOG_TRX");
        public static readonly IDbCommandConfig SA_SP_SEARCH_CONTACT_USERLDI = DbCommandConfiguration.Create("SA_SP_SEARCH_CONTACT_USERLDI");
        public static readonly IDbCommandConfig SGA_P_GENERA_TRANSACCION = DbCommandConfiguration.Create("SGA_P_GENERA_TRANSACCION");
        public static readonly IDbCommandConfig SIACU_SP_INS_INTER_SERV_MP = DbCommandConfiguration.Create("SIACU_SP_INS_INTER_SERV_MP");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_OBT_SERVICIO_FIJA = Create("SIACU_SP_CONSULTAOBTSERVICIOFIJA");
        public static readonly IDbCommandConfig SIACU_P_CONSULTA_SUBTIPORD = DbCommandConfiguration.Create("SIACU_P_CONSULTA_SUBTIPORD");
        public static readonly IDbCommandConfig SIACU_SP_OBTIENE_TIPO_ORDEN_TIPTRA = DbCommandConfiguration.Create("SIACU_SP_OBTIENE_TIPO_ORDEN_TIPTRA");
        public static readonly IDbCommandConfig SIACU_SP_VALIDA_FLUJO_ZONA_ADC = DbCommandConfiguration.Create("SIACU_SP_VALIDA_FLUJO_ZONA_ADC");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_TIPTRA = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_TIPTRA");
        public static readonly IDbCommandConfig SIACU_SP_QUERY_INTER_SERV_MP = DbCommandConfiguration.Create("SIACU_SP_QUERY_INTER_SERV_MP");
        public static readonly IDbCommandConfig SIACU_HFCPOST_SP_BORRAR_PROGRAMACION = Create("SIACU_HFCPOST_BORRAR_PROGRAMACION");
        public static readonly IDbCommandConfig SIACU_HFCPOST_SP_ACTUALIZA_PROGRAMACION = Create("SIACU_HFCPOST_ACTUALIZA_PROGRAMACION");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_LISTA_TELEFONO_LTE = DbCommandConfiguration.Create("SIACU_POST_BSCS_SP_LISTA_TELEFONO_LTE");
        public static readonly IDbCommandConfig SIACU_SP_OBTIENE_LISTAS = DbCommandConfiguration.Create("SIACU_SP_OBTIENE_LISTAS");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_UPDATE_X_INTER_30 = Create("SIACU_POST_CLARIFY_SP_UPDATE_X_INTER_30");
        public static readonly IDbCommandConfig SIACU_SP_SHOW_LIST_ELEMENT_EC = Create("SIACU_SP_SHOW_LIST_ELEMENT_EC");
        public static readonly IDbCommandConfig SIACU_SP_SHOW_LIST_ELEMENT_NC = Create("SIACU_SP_SHOW_LIST_ELEMENT_NC"); 
        public static readonly IDbCommandConfig SIACU_POST_BSCS_SP_ACTUALIZA_DATOS_MENORES = Create("SIACU_POST_BSCS_SP_ACTUALIZA_DATOS_MENORES");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_UPDATE_CUSTOMER_CLF = Create("SIACU_POST_CLARIFY_SP_UPDATE_CUSTOMER_CLF");
        public static readonly IDbCommandConfig SIACU_SP_LISTA_TIPO_VIA = Create("SIACU_SP_LISTA_TIPO_VIA");
        public static readonly IDbCommandConfig SIACU_SP_LISTA_TIPO_MZ = Create("SIACU_SP_LISTA_TIPO_MZ");
        public static readonly IDbCommandConfig SIACU_SP_LISTA_TIPO_INTERIOR = Create("SIACU_SP_LISTA_TIPO_INTERIOR");
        public static readonly IDbCommandConfig SIACU_SP_LISTA_TIPO_URBANIZACION = Create("SIACU_SP_LISTA_TIPO_URBANIZACION");
        public static readonly IDbCommandConfig SIACU_SP_LISTA_TIPO_ZONA = Create("SIACU_SP_LISTA_TIPO_ZONA");
        public static readonly IDbCommandConfig SIACU_SP_LISTA_UBIGEO = Create("SIACU_SP_LISTA_UBIGEO");
        public static readonly IDbCommandConfig SIACU_TOLS_OBTENERDATOSDECUENTA = DbCommandConfiguration.Create("SIACU_TOLS_OBTENERDATOSDECUENTA");
        public static readonly IDbCommandConfig SIACU_SP_VALIDA_MAIL = Create("SIACU_SP_VALIDA_MAIL");
        public static readonly IDbCommandConfig SIACU_SP_NUMERO_SMS = Create("SIACU_SP_NUMERO_SMS");
        public static readonly IDbCommandConfig SIACU_IFI_LINEAS_CLIENTE = Create("SIACU_IFI_LINEAS_CLIENTE");
        public static readonly IDbCommandConfig SIACU_IFI_INSERTA_EMAIL = Create("SIACU_IFI_INSERTA_EMAIL");
        public static readonly IDbCommandConfig SIACU_SP_SHOW_LIST_OBTENER_FECHAS_RECIBOS = Create("SIACU_SP_SHOW_LIST_OBTENER_FECHAS_RECIBOS");
        public static readonly IDbCommandConfig SIACU_SP_ACTUALIZAR_DIRECCIONPOSTAL = DbCommandConfiguration.Create("SIACU_SP_ACTUALIZAR_DIRECCIONPOSTAL");
        public static readonly IDbCommandConfig SIACSS_RESPUESTAS_SEGURIDAD = DbCommandConfiguration.Create("SIACSS_RESPUESTAS_SEGURIDAD");
        public static readonly IDbCommandConfig SIACSS_PREGUNTAS_SEGURIDAD = DbCommandConfiguration.Create("SIACSS_PREGUNTAS_SEGURIDAD");
        public static readonly IDbCommandConfig SIACU_MGRSS_TIM_SP_VALIDA_CICLO_FACT = Create("SIACU_MGRSS_TIM_SP_VALIDA_CICLO_FACT");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_UPDATE_PHONE_FIJO = Create("SIACU_POST_CLARIFY_SP_UPDATE_PHONE_FIJO");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SIACSI_AGREGAR_DATOS_ADIC = Create("SIACU_POST_CLARIFY_SIACSI_AGREGAR_DATOS_ADIC");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SIACSS_OBTERNER_DATOS_ADIC = Create("SIACU_POST_CLARIFY_SIACSS_OBTERNER_DATOS_ADIC");
        public static readonly IDbCommandConfig SIACU_IFI_LOCK_CURRENTS = Create("SIACU_IFI_LOCK_CURRENTS");
        public static readonly IDbCommandConfig SIACU_IFI_OBTIENE_CODIGO_BLOQUEO = Create("SIACU_IFI_OBTIENE_CODIGO_BLOQUEO");
        public static readonly IDbCommandConfig SIACU_IFI_OBTIENE_CODIGO_DESBLOQUEO = Create("SIACU_IFI_OBTIENE_CODIGO_DESBLOQUEO");
        public static readonly IDbCommandConfig SIACU_IFI_INSERTAR_BLOQUEO_LINEA_PER = Create("SIACU_IFI_INSERTAR_BLOQUEO_LINEA_PER");
        public static readonly IDbCommandConfig SIACU_IFI_INSERTAR_BLOQUEO_EQUIPO_PER = Create("SIACU_IFI_INSERTAR_BLOQUEO_EQUIPO_PER");
        public static readonly IDbCommandConfig SIACU_IFI_SP_OBTENERIMEI = Create("SIACU_IFI_SP_OBTENERIMEI");
        public static readonly IDbCommandConfig SIACU_IFI_SP_UPDATECODIGODESBLQ = Create("SIACU_IFI_SP_UPDATECODIGODESBLQ");
        
        
        public static readonly IDbCommandConfig SIACU_IFI_QUEUES_CASE = DbCommandConfiguration.Create("SIACU_IFI_QUEUES_CASE");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_CREATE_CASE = DbCommandConfiguration.Create("SIACU_POST_CLARIFY_CREATE_CASE");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_QUERY_CASE_LA = DbCommandConfiguration.Create("SIACU_POST_CLARIFY_SP_QUERY_CASE_LA");
        public static readonly IDbCommandConfig SIACU_IFI_SP_UPDATECODIGODESBLQROLL = Create("SIACU_IFI_SP_UPDATECODIGODESBLQROLLS");
        public static readonly IDbCommandConfig SIACU_IFI_DELETE_LOCK_LINE = Create("SIACU_IFI_DELETE_LOCK_LINES");
        public static readonly IDbCommandConfig SIACU_IFI_SP_UPDATECODIGODESBLQEqui = Create("SIACU_IFI_SP_UPDATECODIGODESBLQEqui");

        public static readonly IDbCommandConfig SIACU_IFI_SP_OBTENER_ESTADO = Create("SIACU_IFI_SP_OBTENER_ESTADO");
        public static readonly IDbCommandConfig SIACU_IFI_SP_REGISTRAR_MOVIMIENTO_2 = Create("SIACU_IFI_SP_REGISTRAR_MOVIMIENTO_2");
        public static readonly IDbCommandConfig SIACU_IFI_SP_ACTUALIZAR_ESTADO_MARCA = Create("SIACU_IFI_SP_ACTUALIZAR_ESTADO_MARCA");

        public static readonly IDbCommandConfig SIACU_IFI_UPDATE_DESBLOQUEO_EQUIPO = Create("SIACU_IFI_UPDATE_DESBLOQUEO_EQUIPO");
        public static readonly IDbCommandConfig SIACU_IFI_DELETE_INSERTA_EQUIPO = Create("SIACU_IFI_DELETE_INSERTA_EQUIPO");

        public static readonly IDbCommandConfig SIACU_SP_CON_PLAN_CAMPANA = DbCommandConfiguration.Create("SIACU_SP_CON_PLAN_CAMPANA_IFI");
        public static readonly IDbCommandConfig SIACU_SP_CON_PLAN_SERVICIO = DbCommandConfiguration.Create("SIACU_SP_CON_PLAN_SERVICIO_IFI");
        public static readonly IDbCommandConfig SIACU_LISTA_SERVICIOS_TELEFONO = DbCommandConfiguration.Create("SIACU_LISTA_SERVICIOS_TELEFONO");
        public static readonly IDbCommandConfig SIACU_IFI_LISTA_DATOS_EQUIPO = DbCommandConfiguration.Create("SIACU_POST_BSCS_SP_DATOS_EQUIPO");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_PDV_USUARIO = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_PDV_USUARIO");

        public static readonly IDbCommandConfig SIACU_POST_BSCSSS_OBT_DATOS = DbCommandConfiguration.Create("SIACU_POST_BSCSSS_OBT_DATOS"); //PROY-14062 - REINICIO
        public static readonly IDbCommandConfig SIACU_POST_BSCSSS_BSCS_HIST = DbCommandConfiguration.Create("SIACU_POST_BSCSSS_BSCS_HIST");
 #region [Fields]

        private string m_name;

        #endregion

        #region [ Properties ]

        #region Name

        public string Name
        {
            get
            {
                return this.m_name;
            }
        }

        #endregion

        #endregion

        #region SetName

        private void SetName(string name)
        {
            this.m_name = name;
        }

        #endregion

        private static IDbCommandConfig Create(string name)
        {
            DbCommandConfiguration helper = new DbCommandConfiguration();

            helper.SetName(name);

            return helper;
        }


    }
}
