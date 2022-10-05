using System;
using System.Collections.Generic;

using EntitiesFixed = Claro.SIACU.Entity.IFI.Fixed;
using KEY = Claro.ConfigurationManager;


namespace Claro.SIACU.Business.IFI.Fixed
{
    public class SuspensionService
    {

        /// <summary>Método que permite ejecutar la suspensión del servicio</summary>
        /// <param name="objRequest"></param>
        /// <returns>EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse</returns>
        /// <remarks>EjecutaSuspensionDeServicioCodRes</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse EjecutaSuspensionDeServicioCodRes(EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionRequest objRequest)
        {
            var objResponse = new EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse();

            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Fixed.SuspensionService.EjecutaSuspensionDeServicioCodRes(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        /// <summary>Método que permite obtener el servicio de reconexión</summary>
        /// <param name="objRequest"></param>
        /// <returns>EntitiesFixed.GetReconeService.ReconeServiceResponse</returns>
        /// <remarks>GetReconectionService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.GetReconeService.ReconeServiceResponse GetReconectionService(EntitiesFixed.GetReconeService.ReconeServiceRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetReconeService.ReconeServiceResponse();

            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Fixed.SuspensionService.GetReconectionService(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        /// <summary>Método que permite ejecutar la suspensión del servicio Lte</summary>
        /// <param name="objRequest"></param>
        /// <returns>EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse</returns>
        /// <remarks>EjecutaSuspensionDeServicioLte</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse EjecutaSuspensionDeServicioLte(EntitiesFixed.PostSuspensionLte.PostSuspensionLteRequest objRequest)
        {
            var objResponse = new EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse();

            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Fixed.SuspensionService.EjecutaSuspensionDeServicioLte(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        /// <summary>Método que permite ejecutar la reconexión del servicio Lte</summary>
        /// <param name="objRequest"></param>
        /// <returns>EntitiesFixed.PostReconexionLte.ReconexionLteResponse</returns>
        /// <remarks>EjecutaReconexionDeServicioLte</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.PostReconexionLte.ReconexionLteResponse EjecutaReconexionDeServicioLte(EntitiesFixed.PostReconexionLte.ReconexionLteRequest objRequest)
        {
            var objResponse = new EntitiesFixed.PostReconexionLte.ReconexionLteResponse();

            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Fixed.SuspensionService.EjecutaReconexionDeServicioLte(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        /// <summary>Método que actualiza el parametro inter 29 en la plantilla de interacción</summary>
        /// <param name="objRequest"></param>
        /// <returns>EntitiesFixed.GetUpdateInter29.UpdateInter29Response</returns>
        /// <remarks>GetUpdateInter29</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.GetUpdateInter29.UpdateInter29Response GetUpdateInter29(EntitiesFixed.GetUpdateInter29.UpdateInter29Request objRequest)
        {
            var objResponse = new EntitiesFixed.GetUpdateInter29.UpdateInter29Response();

            try
            {
                var rFlagInsercion = string.Empty;
                var rMsgText = string.Empty;

                objResponse.rResult = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.IFI.Fixed.SuspensionService.UpdateInter29(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.p_objid, objRequest.p_texto, objRequest.p_orden, ref rFlagInsercion, ref rMsgText);
                    });

                objResponse.rFlagInsercion = rFlagInsercion;
                objResponse.rMsgText = rMsgText;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }
    }
}
