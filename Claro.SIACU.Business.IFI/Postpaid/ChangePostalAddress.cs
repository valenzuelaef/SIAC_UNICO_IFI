using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangePostalAddress;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangePostalAddressClarify;
using Claro.SIACU.Entity.IFI.Common;
using COMMON = Claro.SIACU.Entity.IFI.Common;
using DATA = Claro.SIACU.Data.IFI.Postpaid;

namespace Claro.SIACU.Business.IFI.Postpaid
{
    public class ChangePostalAddress
    {

        /// <summary>Permite actualizar los datos de facturacion de un cliente en la SIAC_POST_BSCS</summary>
        /// <param name="objUpdPostalAddressRequest"></param>      
        /// <returns>SaveChangePostalAddressResponse</returns>
        /// <remarks>GetSaveChangePostalAddress</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>22/10/2018</FecCrea></item></list>
        public static SaveChangePostalAddressResponse GetSaveChangePostalAddress(SaveChangePostalAddressRequest objSaveChangePostalAddressRequest)
        {
            SaveChangePostalAddressResponse objSaveChangePostalAddressResponse = new SaveChangePostalAddressResponse();


            objSaveChangePostalAddressResponse.StrResult = Web.Logging.ExecuteMethod<bool>(objSaveChangePostalAddressRequest.Audit.Session, objSaveChangePostalAddressRequest.Audit.Transaction, () =>
            {
                return DATA.ChangePostalAddress.GetSaveChangePostalAddress(objSaveChangePostalAddressRequest.strSession, objSaveChangePostalAddressRequest.strTransaction, objSaveChangePostalAddressRequest.objCliente);
            });

            return objSaveChangePostalAddressResponse;

        }

    }
}
