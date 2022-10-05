using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class PCRFConsultationModel
    {
        public string strTelephone { get; set; }
        public string strIdSession { get; set; }
        public string strCurrentUser { get; set; }

        public string strIMSI { get; set; }
        public string strIMSI_Validacion { get; set; }
        public string strLineaAsociada { get; set; }
        public string strLineaAsociada_Validacion { get; set; }
        public string strPerfilPCRF { get; set; }
        public string strPerfilPCRF_Validacion { get; set; }
        public string strCicloFacturacionPCRF { get; set; }
        public string strCicloFacturacionPCRF_Validacion { get; set; }
        public string strTipoServicio { get; set; }
        public string strTipoServicio_Validacion { get; set; }
        public string strSPC_Validacion { get; set; }
        public string strSI_Validacion { get; set; }
        public string strVelocidadDegradada { get; set; }
        public string strDatosConsumidos { get; set; }
        public string strObservacion { get; set; }
        public string strMensaje { get; set; }
        public bool blnMensaje { get; set; }

        public string strAccountId { get; set; }
        public string strTotalVolumeUsage { get; set; }
        public string strTotalVolumeLimit { get; set; }
        public string strAccountIdSession { get; set; }
        public string strCicloFacturacionSession { get; set; }
        public string strNombreClienteSession { get; set; }
        public string strIMSISession { get; set; }

        public bool blnIMSI_Validacion_Color { get; set; }
        public bool blnPerfilPCRF_Validacion_Color { get; set; }
        public bool blnCicloFacturacionPCRF_Validacion_Color { get; set; }
        public bool blnTipoServicio_Validacion_Color { get; set; }
        public bool blnSPC_Validacion_Color { get; set; }
        public bool blnSI_Validacion_Color { get; set; }
        public bool blnVelocidadDegradada_Color { get; set; }

        public bool blnQuotaIniCosum { get; set; }

        public bool blnPerfilSuscriptor { get; set; }
        public bool blnAudit { get; set; }
        //INI - RF-04 - Evalenzs
        public bool bpcrfcConsulta { get; set; }
        public string strUltimoPaqCompradoAdic { get; set; }
        public string strMotDegradacionAdic { get; set; }
        public string strVelDegradadaAdic { get; set; }
        public string strDatosConsumidosAdic { get; set; }
        public bool bBono { get; set; }
    }
}