using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class ServiceLockModel
    {
        public string customerId { get; set; }
        public string msisdn { get; set; }
        public string contractId { get; set; }
        public string currentUser { get; set; }
        public string fullNameUser { get; set; }
        public string StrTitularCliente { get; set; }

        public string codPlano { get; set; }
        public string idSession { get; set; }
        public string idTransaccion { get; set; }
        public string TipoTransaccion { get; set; }
        public string nroDocumento { get; set; }
        public string firstName { get; set; }
        public string LastName { get; set; }
        public string NameLegal { get; set; }
        public string LegalRep { get; set; }
        public string TipoBloqueo { get; set; }
        public string TipoBloqueoEquipo { get; set; }

        public string strSolicitanteNombre { get; set; }
        public string strSolicitanteApellido { get; set; }
        public string strSolicitanteTipoDoc { get; set; }
        public string strSolicitanteNroDoc { get; set; }
        public string strSolicitanteNro { get; set; }


        public string CodigoBloqueo { get; set; }
        public string CodigoBloqueoEquipo { get; set; }
        public string IMEI { get; set; }
        public string Marca { get; set; }
        public string Solicitante { get; set; }
        public string TelefonoReference { get; set; }
        public string TypeDocument { get; set; }
        public string Email { get; set; }
        public string codeLock { get; set; }
        public string NumRef { get; set; }
        public string EmailEnvio { get; set; }
        public string strIdCac { get; set; }
        public string cac { get; set; }
        public string cuenta { get; set; }

        //huella ini
        public string strFirmaDigitalHuella { get; set; }
        public string strHuellaEncode { get; set; }
        public string strHuellaFechaHora { get; set; }
        public string strHuellaMinucia { get; set; }
        public string strHuellaNomApeCli { get; set; }
        public string strHuellaNroDoc { get; set; }
        public string strHuellaTipDoc { get; set; }
        public string strHuellaFlag { get; set; }

        //huella fin

        #region tipificacion
        public string tipo { get; set; }
        public string claseDes { get; set; }
        public string subClaseDes { get; set; }
        public string notes { get; set; }
        public string tipoCode { get; set; }
        public string claseCode { get; set; }
        public string subClaseCode { get; set; }
        #endregion
    }
}