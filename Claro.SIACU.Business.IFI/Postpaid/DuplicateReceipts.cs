using Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Entity.IFI.Postpaid.GetDataCustomer;
using Claro.SIACU.Entity.IFI.Postpaid.GetDuplicateReceipts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Business.IFI.Postpaid
{
    public class DuplicateReceipts
    {

        /// <summary>Permite Actualizar los datos menores de un cliente en la BD (BSCS70_DESA)</summary>
        /// <param name="objRequest">/param> Envia objeto tipo request       
        /// <returns>SaveDuplicateReceiptsResponse</returns>
        /// <remarks>SaveDuplicateReceipts</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018</FecCrea></item></list>
        public static SaveDuplicateReceiptsResponse SaveDuplicateReceipts(SaveDuplicateReceiptsRequest objRequest)
        {
            SaveDuplicateReceiptsResponse objResponse = new SaveDuplicateReceiptsResponse();

            string intTeractionid = "";
            string strFlaginsercion = "";
            string strMessage = "";

            objResponse.StrResult = Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Postpaid.DuplicateReceipts.SaveDuplicateReceipts(objRequest.strSession, objRequest.strTransaction, objRequest.objCliente, out strFlaginsercion, out strMessage);
            });

            objResponse.intTeractionid = intTeractionid;
            objResponse.intTeractionid = strFlaginsercion;
            objResponse.intTeractionid = strMessage;

            return objResponse;
        }
    }
}
