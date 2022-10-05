using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class IFIPackagePurchaseServicesModel
    {
        public string strFullPathPDF { get; set; }
        public string strnamePDF { get; set; }
        public string strIdSession { get; set; }

        //************  Datoscampañas  *****************//
        public string fechaActual { get; set; }
        public string CodeTipification { get; set; }
        public int idCampana { get; set; }
        public string CurrentUser { get; set; }
        public string hidSupJef { get; set; }
        public int intNumeroIntentos { get; set; }
        //******************* DATOS CLIENTE ***********//
        public string strContacto { get; set; }
        public string strCustomerID { get; set; }
        public string strNombreCliente { get; set; }
        public string strTipoCliente { get; set; }
        public string strContrato { get; set; }
        public string strNumeroDoc { get; set; }
        public string strRepresentanteLegal { get; set; }
        public string strPlan { get; set; }
        public string strFechaActivacion { get; set; }
        public string strCicloFacturacion { get; set; }
        public string strLimiteCrediticio { get; set; }

        //********** Direccíón de Facturación ***********//
        public string strDireccion { get; set; }
        public string strNotasDireccion { get; set; }
        public string strPais { get; set; }
        public string strDepartamento { get; set; }
        public string strProvincia { get; set; }
        public string strDistrito { get; set; }
        public string strCodUbigeo { get; set; }

        //************* Datos para Constancia ************ //
        public string strTipoDocIdentidad { get; set; }
        public string strCasoInteraccion { get; set; }
        public string strNumeroCuenta { get; set; }
        public string strNumeroServicio { get; set; }
        public string strCodigoAsesor { get; set; }
        public string strNombreAsesor { get; set; }

        //**************** Datos que cambian *************//
        public string PlanVeloacidadDegrada { get; set; }
        public string MotivoDegradacion { get; set; }
        public string PqtVelocidadDegradacion { get; set; }
        public string strTipoVenta { get; set; }
        public string strSaldoPuntos { get; set;}
        public string strVigencia { get; set; }
        public string strPaquete { get; set; }
        public string strPrecioPaquete { get; set; }
        public string strMBIncluidos { get; set; }
        public string strEmailCliente { get; set; }
        public string strPuntoAtencion { get; set; }
        public string strNotas { get; set; }
        public string FlagEmail { get; set; }
        public bool bGeneratedPDF { get; set; }
        public bool ChkEmail { get; set; }

        /*Tipificacion*/

        public string CLASE { get; set; }
        public string CLASE_CODE { get; set; }
        public string SUBCLASE { get; set; }
        public string SUBCLASE_CODE { get; set; }
        public string TIPO { get; set; }
        public string TIPO_CODE { get; set; }
        public string strObjidContacto { get; set; }
        public string strTelefono { get; set; }  
        /***/
        public string strCodOnBase { get; set; }

        public string strCargoFijo { get; set; }
        public string strMensajeEmail { get; set; }
        public string strErrorMessage { get; set; }
        public byte[] byteArchivoSamba { get; set; }
        public string Document { get; set; }
        public string strPlanBase { get; set; }      

    }
}