

using Claro.SIACU.ProxyService.IFI.RedirectWS;
using Claro.SIACU.ProxyService.IFI.SIACFixed.CustomerHFC;
using Claro.SIACU.ProxyService.IFI.SIACFixed.CustomerLTE;
using Claro.SIACU.ProxyService.IFI.SIACU.ConsultIGV;
using Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad;
using Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS;
using Claro.SIACU.ProxyService.IFI.SIACFixed.ServAddLTE;
using ActivateDesactivateLTE = Claro.SIACU.ProxyService.IFI.SIACFixed.ServAddLTE;
using AdditionalServicesHFC = Claro.SIACU.ProxyService.IFI.SIACFixed.ServAddHFC;
using Claro.SIACU.ProxyService.IFI.SIACPost.Customer;
using Claro.SIACU.ProxyService.IFI.SIACSecurity.Permissions;
using Claro.SIACU.ProxyService.IFI.SIACFixed.PCRFConnectorLTE;
using Claro.SIACU.ProxyService.IFI.SIACU.TriacionPostpagoWS;
using Claro.SIACU.ProxyService.IFI.SIACU.BillingService;
using Claro.SIACU.ProxyService.IFI.SIACU.GestionAcuerdoIFI;
using Claro.Transversal.ProxyService.ConsultaClaves;
using Claro.SIACU.ProxyService.IFI.SIACU.IFI.LockLine;
using Claro.SIACU.ProxyService.IFI.SIACU.IFI.UnlockLine;
using Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment;
using Claro.SIACU.ProxyService.IFI.SIACU.OnBaseLoadDocumentIFI;
using Claro.SIACU.ProxyService.IFI.SIACU.IFI.TransaccionInteracciones;
namespace Claro.SIACU.Data.IFI.Configuration
{
    internal struct ServiceConfiguration
    {
        public static readonly ValidarCredencialesSUWSPortTypeClient AUDIT_CREDENTIALS = Claro.Data.Service.Create<ValidarCredencialesSUWSPortTypeClient>("RedirectWS");
        public static readonly ConsultaSeguridadClient SIACU_ConsultaSeguridad = Claro.Data.Service.Create<ConsultaSeguridadClient>("SIACU_ConsultaSeguridad");
        public static readonly clienteHFCPortTypeClient FIXED_CUSTOMER_HFC = Claro.Data.Service.Create<clienteHFCPortTypeClient>("WSURLClienteHFC");
        public static readonly clienteLTEPortTypeClient FIXED_CUSTOMER_LTE = Claro.Data.Service.Create<clienteLTEPortTypeClient>("WSURLClienteLTE");
        public static readonly ConsultaIGVWSPortTypeClient SIACUConsultaIGV = Claro.Data.Service.Create<ConsultaIGVWSPortTypeClient>("WSURLConsultIGV");
        public static readonly TransaccionesInternetLTEWSPortTypeClient TransaccionesInternetLTE = Claro.Data.Service.Create<TransaccionesInternetLTEWSPortTypeClient>("WSTransaccionesInternetLTE");
       
        public static readonly ActivateDesactivateLTE.ServiciosLTEPortTypeClient SiacFixedActivationDesactivacionLte = Claro.Data.Service.Create<ActivateDesactivateLTE.ServiciosLTEPortTypeClient>("FixedAddServLTE");
        public static readonly AdditionalServicesHFC.ebsServiciosHFCSB11Client SiacFixedActivationDesactivacionHfc = Claro.Data.Service.Create<AdditionalServicesHFC.ebsServiciosHFCSB11Client>("FixedAddServHFC");
        public static readonly SIACPostpagoConsultasWSClient POSTPAID_CONSULTCLIENT = Claro.Data.Service.Create<SIACPostpagoConsultasWSClient>("SIACPostpagoConsultas");
        public static readonly EbsAuditoriaClient SECURITY_PERMISSIONS = Claro.Data.Service.Create<EbsAuditoriaClient>("strWebServiceSecurityPermissions");

        public static readonly PCRFWSPortTypeSOAP11Client SiacFixedPCRFConnector = Claro.Data.Service.Create<PCRFWSPortTypeSOAP11Client>("WSPCRFConnector");
        public static readonly  SIACPostpagoTxWSClient ServiceTriacion= Claro.Data.Service.Create<SIACPostpagoTxWSClient>("strWebServiceTriacion");
        public static readonly TransaccionOCCPortClient TransaccionOCCPort = Claro.Data.Service.Create<TransaccionOCCPortClient>("WSTransaccionOCC");

        public static readonly GestionAcuerdoWSPortTypeClient GestionAcuerdoWS = Claro.Data.Service.Create<GestionAcuerdoWSPortTypeClient>("WSGestionAcuerdoWS");

        public static readonly ebsConsultaClavesPortTypeClient CONSULTA_CLAVES = Claro.Data.Service.Create<ebsConsultaClavesPortTypeClient>("strConsultaClavesWS");
        public static readonly Claro.SIACU.ProxyService.IFI.SIACU.IFI.LockLine.BloqueoDesbloqueoLineaIFIPortTypeClient strServiceLock = Claro.Data.Service.Create<Claro.SIACU.ProxyService.IFI.SIACU.IFI.LockLine.BloqueoDesbloqueoLineaIFIPortTypeClient>("strServiceLock");
        public static readonly Claro.SIACU.ProxyService.IFI.SIACU.IFI.UnlockLine.BloqueoDesbloqueoLineaIFIPortTypeClient strServiceUnlock = Claro.Data.Service.Create<Claro.SIACU.ProxyService.IFI.SIACU.IFI.UnlockLine.BloqueoDesbloqueoLineaIFIPortTypeClient>("strServiceUnlock");
        public static readonly BloqueoDesbloqueoIMEIWSClient BLOQDESEQUIPO = Claro.Data.Service.Create<BloqueoDesbloqueoIMEIWSClient>("strBLOQDESEQUIPO");
        public static readonly svcOnBaseClaroCargaSoapClient ONBASELOAD = Claro.Data.Service.Create<svcOnBaseClaroCargaSoapClient>("strOnbaseLoad");

        public static readonly TransaccionInteraccionesClient ServiceInteraccion = Claro.Data.Service.Create<TransaccionInteraccionesClient>("strServiceInteraccion");
    
    }
}
