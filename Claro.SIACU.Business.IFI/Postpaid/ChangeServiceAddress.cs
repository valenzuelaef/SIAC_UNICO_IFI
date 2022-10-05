using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA = Claro.SIACU.Data.IFI.Postpaid;
using Claro.SIACU.Entity.IFI.Fixed;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeServiceAddress;
using Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi;

namespace Claro.SIACU.Business.IFI.Postpaid
{
    public class ChangeServiceAddress
    {
        /// <summary>
        /// Obtener parametros configurables por nombre.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaccion"></param>
        /// <returns></returns>
        public static List<GenericItem> obtenerParametrosPorTipo(string strIdSession, string strTransaccion)
        {
            List<GenericItem> olisGenericItem = null;
            olisGenericItem = Web.Logging.ExecuteMethod<List<GenericItem>>(strIdSession, strTransaccion, () =>
            {
                return DATA.ChangeServiceAddress.obtenerParametrosPorTipo(strIdSession, strTransaccion);
            });
            return olisGenericItem;
        }
        /// <summary>
        /// Permite registrar o actualizar dirección del servicio IFI.
        /// </summary>
        /// <param name="oheaderRequest">Información de cabecera.</param>
        /// <param name="oregistrarDireccionRequest">Información del cambio de dirección IFI.</param>
        /// <returns></returns>
        public static registrarDireccionResponse registrarDireccion(registrarDireccionRequest oregistrarDireccionRequest)
        {
            registrarDireccionResponse oregistrarDireccionResponse = null;
            oregistrarDireccionResponse = Web.Logging.ExecuteMethod<registrarDireccionResponse>(() =>
            {
                return DATA.ChangeServiceAddress.registrarDireccion(oregistrarDireccionRequest);
            });
            return oregistrarDireccionResponse;
        }
        /// <summary>
        /// Permite validar la cobertura de la dirección para el cambio de dirección.
        /// </summary>
        /// <param name="ovalidarCoberturaMessageRequest"></param>
        /// <returns></returns>
        public static validarCoberturaMessageResponse validarCoberturaDireccion(validarCoberturaMessageRequest ovalidarCoberturaMessageRequest)
        {
            validarCoberturaMessageResponse ovalidarCoberturaMessageResponse = null;
            ovalidarCoberturaMessageResponse = Web.Logging.ExecuteMethod<validarCoberturaMessageResponse>(() =>
            {
                return DATA.ChangeServiceAddress.validarCoberturaDireccion(ovalidarCoberturaMessageRequest);
            });
            return ovalidarCoberturaMessageResponse;
        }

        public static List<ObtenerTipoTecnologia> obtenerTipoTecnologia(string strIdSession, string strTransaction, string strParamGrupo)
        {
            List<ObtenerTipoTecnologia> olistObtenerTipoTecnologia = null;
            olistObtenerTipoTecnologia = Web.Logging.ExecuteMethod<List<ObtenerTipoTecnologia>>(() =>
            {
                return DATA.ChangeServiceAddress.obtenerTipoTecnologia(strIdSession, strTransaction, strParamGrupo);
            });
            return olistObtenerTipoTecnologia;
        }
    }
}