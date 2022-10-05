using System.Runtime.Serialization;
using Claro.Data;
using System.Collections.Generic;
namespace Claro.SIACU.Entity.IFI.Common
{
    [DataContract(Name = "ParametersGeneratePDF")]
    public class ParametersGeneratePDF
    {
        public ParametersGeneratePDF()
        {
        }
        #region Suspension

        [DataMember]
        public string strTipoOperacion { get; set; }
        [DataMember]
        public string strCodeTCRM { get; set; }
        [DataMember]
        public string strCac { get; set; }
        [DataMember]
        public string strNroInter { get; set; }
        [DataMember]
        public string strNombreCliente { get; set; }
        [DataMember]
        public string strTipoCliente { get; set; }
        [DataMember]
        public string strCuentaCliente { get; set; }
        [DataMember]
        public string strTipoDocTitular { get; set; }
        [DataMember]
        public string strNroDocTitutar { get; set; }
        [DataMember]
        public string strRepresentanteLegal { get; set; }
        [DataMember]
        public string strNumeroServicio { get; set; }

        #endregion

        [DataMember]
        public string strFirmaDigital { get; set; }
        [DataMember]
        public string StrFlagExterInter { get; set; }
        [DataMember]
        public string StrDescripTransaccion { get; set; }
        [DataMember]
        public string StrPaisA { get; set; }
        [DataMember]
        public string StrCentroPobladoActual { get; set; }
        [DataMember]
        public string StrCentroPobladoDestino { get; set; }
        [DataMember]
        public string StrAccion { get; set; }

        [DataMember]
        public string StrTelfConfirmacion { get; set; }
        [DataMember]
        public string StrFecAfiliacion { get; set; }

        [DataMember]
        public string StrFechaSuspension { get; set; }
        [DataMember]
        public string StrFechaActivacion { get; set; }
        [DataMember]
        public string StrCostoReactivacion { get; set; }
         
        [DataMember]
        public string StrCodigoLocalA { get; set; }
        [DataMember]
        public string StrReferenciaActual { get; set; }
        [DataMember]
        public string StrReferenciaDestino { get; set; }
        [DataMember]
        public string StrPaisB { get; set; }
        [DataMember]
        public string StrDepartamentoLocalB { get; set; }
        [DataMember]
        public string StrProvinciaLocalB { get; set; }
        [DataMember]
        public string StrDistrtitoLocalB { get; set; }
        [DataMember]
        public string StrCodigoLocalB { get; set; }
        [DataMember]
        public string StrDireccionPostalC { get; set; }
        [DataMember]
        public string StrAplicaCambioDireccion { get; set; }
        [DataMember]
        public string StrAplicaCambioNombre { get; set; }
        [DataMember]
        public List<ServiceArmaPlan> ArrListaInfo { get; set; }
        [DataMember]
        public string StrNroServicio { get; set; }
        [DataMember]
        public string StrTitularCliente { get; set; }
        [DataMember]
        public string StrContactoCliente { get; set; }
        [DataMember]
        public string StrPlanActual { get; set; }
        [DataMember]
        public string StrCentroAtencionArea { get; set; }
        [DataMember]
        public string StrTipoDocIdentidad { get; set; }
        [DataMember]
        public string StrNroDocIdentidad { get; set; }
        [DataMember]
        public string strCodigoBloqueo { get; set; }
        [DataMember]
        public string strSolicitante { get; set; }
        [DataMember]
        public string StrFechaTransaccionProgram { get; set; }
        [DataMember]
        public string StrCasoInter { get; set; }
        [DataMember]
        public string StrRepresLegal { get; set; }
        [DataMember]
        public string StrNroDocRepLegal { get; set; }
        [DataMember]
        public string StrCustomerId { get; set; }
        [DataMember]
        public string StrCuentaPostpago { get; set; }
        [DataMember]
        public string StrNotas { get; set; }
        [DataMember]
        public string StrTelfReferencia { get; set; }
        [DataMember]
        public string StrResultado { get; set; }
        [DataMember]
        public string strMotivo { get; set; }
        [DataMember]
        public string strtransaccion { get; set; }

        [DataMember]
        public string StrCicloFacturacion { get; set; }
        [DataMember]
        public string StrFechaEjecucion { get; set; }
        [DataMember]
        public string StrCodUsuario { get; set; }
        [DataMember]
        public string StrNombreAgenteUsuario { get; set; }
        [DataMember]
        public string StrAplicaEmail { get; set; }
        [DataMember]
        public string StrEmail { get; set; }
        [DataMember]
        public string StrAplicaOtroContacto { get; set; }
        [DataMember]
        public string StrContactoOtro { get; set; }
        [DataMember]
        public string StrNroDocContactoOtro { get; set; }
        [DataMember]
        public string StrMotivoPariente { get; set; }
        [DataMember]
        public string StrTelfOtroContacto { get; set; }
        [DataMember]
        public string StrCanalAtencion { get; set; }
        [DataMember]
        public string StrFlagPlantillaPlazo { get; set; }
        [DataMember]
        public string StrEscenarioServicioCom { get; set; }
        [DataMember]
        public string StrAplicaProgramacion { get; set; }
        [DataMember]
        public string StrCfServicioCom { get; set; }
        [DataMember]
        public string StrFechaPlazo { get; set; }
        [DataMember]
        public string StrPlazo { get; set; }
        [DataMember]
        public string StrCfServicioModif { get; set; }
        [DataMember]
        public string StrDescripcionServicioCom { get; set; }
        [DataMember]
        public string StrPeriodoCuotaServicio { get; set; }
        [DataMember]
        public string StrBolsaSolesAdicionales { get; set; }
        [DataMember]
        public string StrImei  { get; set; }
        [DataMember]
        public string StrMarcaEquipo  { get; set; }
        [DataMember]
        public string StrModeloEquipo { get; set; }
        [DataMember]
        public string StrTransaccionBloqueo  { get; set; }
        [DataMember]
        public string StrMotivoTipoBloqueo  { get; set; }
        [DataMember]
        public string StrTopeConsumo { get; set; }
        [DataMember]
        public string StrFechaEjecTopeCons { get; set; }
        [DataMember]
        public string StrFlagGrilaAtp { get; set; }
        [DataMember]
        public string StrEscenarioMigracion { get; set; }
        [DataMember]
        public string StrNuevoPlan { get; set; }
        [DataMember]
        public string StrCfTotalNuevo { get; set; }
        [DataMember]
        public string StrMontoApadece { get; set; }
        [DataMember]
        public string StrMontoPcs { get; set; }
        [DataMember]
        public string StrMotivoCancelacion { get; set; }
        [DataMember]
        public string StrEscenarioRetencion { get; set; }
        [DataMember]
        public string StrAccionRetencion { get; set; }
        [DataMember]
        public string StrModalidad { get; set; }
        [DataMember]
        public string StrProductos { get; set; }
        [DataMember]
        public string StrSecuencialOrden { get; set; }
        [DataMember]
        public string StrIdProductoCc { get; set; }
        [DataMember]
        public string StrDescProductoCc { get; set; }
        [DataMember]
        public string StrPuntosCc { get; set; }
        [DataMember]
        public string StrCantidadCc { get; set; }
        [DataMember]
        public string StrDescuentoCc { get; set; }
        [DataMember]
        public string StrTipoPremioCc { get; set; }
        [DataMember]
        public string StrMontoRecargaCc { get; set; }
        [DataMember]
        public string StrPuntosCcAntesTrans { get; set; }
        [DataMember]
        public string StrCantidadTotalCanjeDev { get; set; }
        [DataMember]
        public string StrTipoDocFact { get; set; }
        [DataMember]
        public string StrNroDocFact { get; set; }
        [DataMember]
        public string StrDireccionPostal { get; set; }
        [DataMember]
        public string StrDistritoPostal { get; set; }
        [DataMember]
        public string StrProvinciaPostal { get; set; }
        [DataMember]
        public string StrDepartamentoPostal { get; set; }
        [DataMember]
        public string StrFechaEmisionDocFact { get; set; }
        [DataMember]
        public string StrFechaVencDocFact { get; set; }
        [DataMember]
        public string StrFlagPosicionIgv { get; set; }
        [DataMember]
        public string StrCategoriaFact { get; set; }
        [DataMember]
        public string StrSubcategoriaFact { get; set; }
        [DataMember]
        public string StrSubcategoriaFactSinIgv { get; set; }
        [DataMember]
        public string StrNroServicio2 { get; set; }
        [DataMember]
        public string StrFechaDesdeAjuste { get; set; }
        [DataMember]
        public string StrFechaHastaAjuste { get; set; }
        [DataMember]
        public string StrImporteConceptoAjusteSinIgv { get; set; }
        [DataMember]
        public string StrSubtotalAjusteSinIgv { get; set; }
        [DataMember]
        public string StrIgvTax { get; set; }
        [DataMember]
        public string StrTotalConIgv { get; set; }
        [DataMember]
        public string StrTotalAjuste { get; set; }
        [DataMember]
        public string StrMotivoCambioSim { get; set; }
        [DataMember]
        public string StrNuevoSim { get; set; }
        [DataMember]
        public string StrCostoTransaccion { get; set; }
        [DataMember]
        public string StrFlag4G { get; set; }
        [DataMember]
        public string StrSim4GLte { get; set; }
        [DataMember]
        public string StrEstadoServicio4G { get; set; }
        [DataMember]
        public string StrNroDocIdentidadRef { get; set; }
        [DataMember]
        public string StrFechaEmisionDocRef { get; set; }
        [DataMember]
        public string StrCodDesbloqueo { get; set; }

        //Set y Get de variables para guardar opciones del servidor
        [DataMember]
        public string StrServidorGenerarPDF { get; set; }
        [DataMember]
        public string StrServidorLeerPDF { get; set; }
        [DataMember]
        public string StrCarpetaPDFs { get; set; }
        [DataMember]
        public string StrCarpetaTransaccion { get; set; }
        [DataMember]
        public string StrTipoTransaccion { get; set; }
        [DataMember]
        public string StrNombreArchivoTransaccion { get; set; }
        [DataMember]
        public string StrMontSolicitad { get; set; }
        [DataMember]
        public string strAccionEjecutar { get; set; }
        [DataMember]
        public string strNroAnterior { get; set; }
        [DataMember]
        public string strNroNuevo { get; set; }
        [DataMember]
        public string strLocucion { get; set; }
        [DataMember]
        public string strCostoLocucion { get; set; }
        [DataMember]
        public string strDuracionLocucion { get; set; }
        [DataMember]
        public string strEnvioCorreo { get; set; }
        [DataMember]
        public string strContrato { get; set; }
        [DataMember]
        public string strPuntoDeAtencion { get; set; }
        [DataMember]
        public string strNroDoc { get; set; }
        [DataMember]
        public string strFechaTransaccion { get; set; }
        [DataMember]
        public string strCasoInteraccion { get; set; }
        [DataMember]
        public string strTransaccionDescripcion { get; set; }
        [DataMember]
        public string strCostoTransaccion { get; set; }
        [DataMember]
        public string strDireccionClienteActual { get; set; }
        [DataMember]
        public string strRefTransaccionActual { get; set; }
        [DataMember]
        public string strDistritoClienteActual { get; set; }
        [DataMember]
        public string strCodigoPostalActual { get; set; }
        [DataMember]
        public string strPaisClienteActual { get; set; }

        [DataMember]
        public string strProvClienteActual { get; set; }
        [DataMember]
        public string strDirClienteDestino { get; set; }
        [DataMember]
        public string strRefTransaccionDestino { get; set; }
        [DataMember]
        public string strDepClienteDestino { get; set; }
        [DataMember]
        public string strDistClienteDestino { get; set; }
        [DataMember]
        public string strAplicaCambioDirFact { get; set; }
        [DataMember]
        public string strCodigoPostallDestino { get; set; }
        [DataMember]
        public string strPaisClienteDestino { get; set; }

        [DataMember]
        public string strProvClienteDestino { get; set; }
        [DataMember]
        public string strCodigoPlanoDestino { get; set; }
        [DataMember]
        public string strEnviomail { get; set; }
        [DataMember]
        public string strflagTipoTraslado { get; set; }
        [DataMember]
        public string strCorreoCliente { get; set; }
        [DataMember]
        public string strCentroAtencion { get; set; }
        [DataMember]
        public string strContratoCliente { get; set; }
        [DataMember]
        public string strDepClienteActual { get; set; }
        [DataMember]
        public string StrFecInicialReporte { get; set; }
        [DataMember]
        public string StrFecDesistimiento { get; set; }
        [DataMember]
        public string StrAccionIFI { get; set; }
        [DataMember]
        public string StrPuntoAtencion { get; set; }
        [DataMember]
        public string StrIdInteraccion { get; set; }
        
        [DataMember]
        public string StrFecFinalReporte { get; set; }
        [DataMember]
        public string StrMontoOCC { get; set; }
        [DataMember]
        public string StrContenidoComercial { get; set; }
        [DataMember]
        public string StrContenidoComercial2 { get; set; }
        
        #region RetencionCancelacionServicio

        [DataMember]
        public string StrSegmento { get; set; }
        [DataMember]
        public string strSolicitanteNombre { get; set; }
        [DataMember]
        public string strSolicitanteApellido { get; set; }
        [DataMember]
        public string strSolicitanteTipoDoc { get; set; }
        [DataMember]
        public string strSolicitanteNroDoc { get; set; }
        [DataMember]
        public string strSolicitanteNro { get; set; }
        [DataMember]
        public string strLinea { get; set; } 
        [DataMember]
        public string strMarcaModelo { get; set; }
        [DataMember]
        public string strFlagBloqueo { get; set; }
        [DataMember]
        public string StrSubMotivoCancel { get; set; }
        [DataMember]
        public string StrFechaCancel { get; set; }
        [DataMember]
        public string strTextoEnmarcado { get; set; }
        [DataMember]
        public string strFechaHoraAtención { get; set; }
        [DataMember]
        public string strDireccionInstalac{ get; set; }
        #endregion


        #region Instalacion y Desinstalacion de Servicios
        [DataMember]
        public string StrTituloInstalacion { get; set; }
        [DataMember]
        public string StrTituloDesinstalacion { get; set; } 
        [DataMember]
        public string StrNumeroContrato { get; set; }
        [DataMember]
        public string StrFlagTipoDeco { get; set; }
        [DataMember]
        public string StrDireccion { get; set; } 
        [DataMember]
        public string StrNotasDireccion { get; set; }
        [DataMember]
        public string StrDepartamento { get; set; }
        [DataMember]
        public string StrDistrito { get; set; }
        [DataMember]
        public string StrPais { get; set; }
        [DataMember]
        public string StrProvincia { get; set; }
        [DataMember]
        public string StrCodigoPlano { get; set; }
        [DataMember]
        public string StrFechaCompromiso { get; set; }
        [DataMember]
        public string StrXEtiqueta2 { get; set; }

        [DataMember]
        public List<ServiceConstancy> ListDecoder { get; set; }

        [DataMember]
        public string StrNombreServicio { get; set; }
        [DataMember]
        public string StrTipoEquipo { get; set; }
        [DataMember]
        public string StrGrupoServicio { get; set; }
        [DataMember]
        public string StrCargoFijo { get; set; }
        [DataMember]
        public string StrCantidadInstalar { get; set; }
        [DataMember]
        public string StrCantidadDesinstalar { get; set; }
        [DataMember]
        public string StrCargoFijoConIGV { get; set; }
        [DataMember]
        public string StrFidelizar { get; set; }
        [DataMember]
        public string StrCostoInstalacion { get; set; }
        [DataMember]
        public string StrCostoDesinstalacion { get; set; }
        [DataMember]
        public string StrEnviarEmail { get; set; }
        [DataMember]
        public string StrTexto { get; set; } 
        #endregion


        [DataMember]
        public string strIdIteraction { get; set; }

        [DataMember]
        public string strMontoReintegro { get; set; }
        [DataMember]
        public string strServComercial { get; set; }
        [DataMember]
        public string strCargoFijoPromocion { get; set; }
        [DataMember]
        public string strAccionEjecutiva { get; set; }
        [DataMember]
        public string strProgramado { get; set; }
        [DataMember]
        public string strNumPerProm { get; set; }
        [DataMember]
        public string strRepLegNroDocumento { get; set; }
        [DataMember]
        public string strDireccionInstalcion { get; set; }
        [DataMember]
        public string strNroSot { get; set; }

        [DataMember]
        public string StrEstadoCivil { get; set; }

        [DataMember]
        public string StrMovil { get; set; }

        [DataMember]
        public string StrCargo { get; set; }

        [DataMember]
        public string StrFechaNac { get; set; }

        [DataMember]
        public string StrNumeroFax { get; set; }

        [DataMember]
        public string StrNacionalidad { get; set; }

        [DataMember]
        public string StrSexo { get; set; }

        [DataMember]
        public string StrEmail1 { get; set; }

        [DataMember]
        public string StrEmail2 { get; set; }

        [DataMember]
        public string StrPhone1 { get; set; }

        [DataMember]
        public string StrPhone2 { get; set; }

        [DataMember]
        public string strEmailEnviar { get; set; }

        [DataMember]
        public string StrMedio { get; set; }



        //huella ini
        [DataMember]
        public string strFirmaDigitalHuella { get; set; }
        [DataMember]
        public string strHuellaMinucia { get; set; }
        [DataMember]
        public string strHuellaEncode { get; set; }
        [DataMember]
        public string strHuellaFechaHora { get; set; }

        [DataMember]
        public string strHuellaNroDoc { get; set; }
        [DataMember]
        public string strHuellaTipDoc { get; set; }
        [DataMember]
        public string strHuellaNomApeCli { get; set; }

        //huella fin

        #region INICIO - PROY-140513 - REINICIA TU VELOCIDAD

        [DataMember]
        public string strPlanVelDegradada { get; set; }
        [DataMember]
        public string strMotDegradacion { get; set; }
        [DataMember]
        public string strPaqueteVelDegradada { get; set; }
        [DataMember]
        public string strTipoVenta { get; set; }
        [DataMember]
        public string strSaldoPuntos { get; set; }
        [DataMember]
        public string strPaquete { get; set; }
        [DataMember]
        public string strVigenciaPaquete { get; set; }
        [DataMember]
        public string strPrecioPaquete { get; set; }
        [DataMember]
        public string strMBIncluidos { get; set; }
        [DataMember]
        public string strEmailCliente { get; set; }
        [DataMember]
        public string strCentroAtencionDAC { get; set; }
        [DataMember]
        public string strNotas { get; set; }
        [DataMember]
        public string strCuenta { get; set; }
        [DataMember]
        public string FormatoTransaccion { get; set; }
        [DataMember]
        public string StrCodigoAsesor { get; set; }
        [DataMember]
        public string StrNombreAsesor { get; set; }

        #endregion FIN - PROY-140513 - REINICIA TU VELOCIDAD

    }
    public class ServiceConstancy {
        public string StrNombreEquipo { get; set; }
        public string StrTipoServicio { get; set; }
        public string StrCargoFijoSinIGV { get; set; }
    }
}
