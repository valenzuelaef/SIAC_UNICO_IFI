
using Claro.SIACU.ProxyService.IFI.TransaccionOCC;
using Claro.SIACU.ProxyService.IFI.SIACU.EnvioCorreoSB;
using Claro.Data;
using Claro.SIACU.ProxyService.IFI.AuditoriaWS;
using Claro.SIACU.ProxyService.IFI.RegistroAuditoriaWS;
using Claro.SIACU.ProxyService.IFI.ConsultaOpcionesAuditoriaWS;
using Claro.SIACU.ProxyService.IFI.ServiciosPostpagoWS;
using Claro.SIACU.ProxyService.IFI.WSBRMS;
using Claro.SIACU.ProxyService.IFI.SIACU.DigitalSignature;

namespace Claro.SIACU.Data.IFI.Configuration
{
    internal struct WebServiceConfiguration
    {

        public static readonly TransaccionOCC TransaccionOCC = WebService.Create<TransaccionOCC>("TransaccionOCC");
        public static readonly EnvioCorreoSBPortTypeSOAP11BindingQSService SIACUEnvioCorreoSB = WebService.Create<EnvioCorreoSBPortTypeSOAP11BindingQSService>("WSURLEnvioCorreoSB");
        public static readonly EbsAuditoriaService GRABARAUDIT = WebService.Create<EbsAuditoriaService>("strWebServiceSeguridad");
        public static readonly RegistroAuditoriaService REGISTRARAUDIT = WebService.Create<RegistroAuditoriaService>("RegistroAuditoriaWS");
        public static readonly ConsultaOpcionesAuditoriaService OpcionesAuditoria = WebService.Create<ConsultaOpcionesAuditoriaService>("ConsultaOpcionesAuditoriaWS");
        public static readonly ServiciosPostPagoWSService ActDesactServiciosComerciales = WebService.Create<ServiciosPostPagoWSService>("ActDesactServiciosComerciales");
        public static readonly REGLASAUTOMATIZACIONDEDOCUMENTOSDecisionService objWSBRMSService = WebService.Create<REGLASAUTOMATIZACIONDEDOCUMENTOSDecisionService>("strServidorBRMS");
        public static readonly PRS_FirmaDigital_WS SIACUDigitalSignature = WebService.Create<PRS_FirmaDigital_WS>("WSURLDigitalSignature");
    
    }
}
