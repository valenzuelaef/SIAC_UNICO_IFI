using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeServiceAddress
{
    public class registrarDireccionRequest
    {
        public string strObjidContacto { get; set; }
        public string idTransaccion { get; set; }
        public string tipoCliente { get; set; }
        public string currentUser { get; set; }
        public string linea { get; set; }
        public string tipoDireccion { get; set; }
        public string nombreDireccion { get; set; }
        public string numeroDireccion { get; set; }
        public string subDireccion { get; set; }
        public string nombreSubDireccion { get; set; }
        public string lote { get; set; }
        public string tipoSubDireccion { get; set; }
        public string numeroSubDireccion { get; set; }
        public string tipoUrbanizacion { get; set; }
        public string nombreUrbanizacion { get; set; }
        public string tipoDomicilio { get; set; }
        public string zonaEtapa { get; set; }
        public string nombreZonaEtapa { get; set; }
        public string referencia { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
        public string codigoPostal { get; set; }
        public string ubigeo { get; set; }
        public string telefonoReferencia { get; set; }
        public string flagCobro { get; set; }
        public responseStatus responseStatus { get; set; }
        public string accion { get; set; }
        public string estado { get; set; }

        #region "Tipificacion"
        public string tipo { get; set; }
        public string claseDes { get; set; }
        public string subClaseDes { get; set; }
        public string tipoCode { get; set; }
        public string claseCode { get; set; }
        public string subClaseCode { get; set; }
        public string notas { get; set; }
        public string agente { get; set; }
        #endregion

        #region "Template Interaccion"
        public string strTelefono { get; set; }
        public string DNI_RUC { get; set; }
        public string strNombres { get; set; }
        public string strApellidos { get; set; }
        public string strDireccion { get; set; }
        public string strReferencia { get; set; }
        public string strContactoCliente { get; set; }
        public string strDistrito { get; set; }
        public string strProvincia { get; set; }
        public string strDepartamento { get; set; }
        public string strPais { get; set; }
        public string strCodPostal { get; set; }
        public string strfullNameUser { get; set; }
        public string strCacDac { get; set; }
        #endregion 
    }
}
