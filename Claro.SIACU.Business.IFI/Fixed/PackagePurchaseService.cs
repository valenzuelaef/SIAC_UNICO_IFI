using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.IFI.Fixed;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Entity.IFI.Fixed.GetConsultarClaroPuntos;
using Claro.SIACU.Entity.IFI.Fixed.GetConsultarPaqDisponibles;
using Claro.SIACU.Entity.IFI.Fixed.GetComprarPaquetes;
using Claro.SIACU.Entity.IFI.Fixed.GetPCRFConsultation;
 //INI - RF-02 Evalenzs

namespace Claro.SIACU.Business.IFI.Fixed
{
    public class PackagePurchaseService
    {
        public static ConsultarClaroPuntosResponse ConsultarClaroPuntos(ConsultarClaroPuntosRequest objRequest)
        {
            ConsultarClaroPuntosResponse objResponse = new ConsultarClaroPuntosResponse();

            Claro.SIACU.Entity.IFI.Fixed.GetConsultarClaroPuntos.RestConsultarClaroPuntosRequest oRestRequest = new Claro.SIACU.Entity.IFI.Fixed.GetConsultarClaroPuntos.RestConsultarClaroPuntosRequest
            {
                MessageRequest = new Claro.SIACU.Entity.IFI.Fixed.GetConsultarClaroPuntos.RestConsultarClaroPuntosMessageRequest
                {
                    Header = new Claro.SIACU.Entity.IFI.Fixed.GetConsultarClaroPuntos.RestConsultarClaroPuntosHeader
                    {
                        HeaderRequest = new Claro.SIACU.Entity.IFI.Fixed.GetConsultarClaroPuntos.RestConsultarClaroPuntosHeaderRequest
                        {
                            consumer = objRequest.MessageRequest.Header.HeaderRequest.consumer,
                            country = objRequest.MessageRequest.Header.HeaderRequest.country,
                            dispositivo = objRequest.MessageRequest.Header.HeaderRequest.dispositivo,
                            language = objRequest.MessageRequest.Header.HeaderRequest.language,
                            modulo = objRequest.MessageRequest.Header.HeaderRequest.modulo,
                            msgType = objRequest.MessageRequest.Header.HeaderRequest.msgType,
                            operation = objRequest.MessageRequest.Header.HeaderRequest.operation,
                            pid = objRequest.MessageRequest.Header.HeaderRequest.pid,
                            system = objRequest.MessageRequest.Header.HeaderRequest.system,
                            timestamp = objRequest.MessageRequest.Header.HeaderRequest.timestamp,
                            userId = objRequest.MessageRequest.Header.HeaderRequest.userId,
                            wsIp = objRequest.MessageRequest.Header.HeaderRequest.wsIp
                        }
                    },
                    Body = new Claro.SIACU.Entity.IFI.Fixed.GetConsultarClaroPuntos.RestConsultarClaroPuntosBodyRequest
                    {

                        tipoPuntos = objRequest.MessageRequest.Body.tipoPuntos,
                        tipoDocumento = objRequest.MessageRequest.Body.tipoDocumento,
                        numeroDocumento = objRequest.MessageRequest.Body.numeroDocumento,
                        bolsa = objRequest.MessageRequest.Body.bolsa,
                        tipoConsulta = objRequest.MessageRequest.Body.tipoConsulta
                    }
                }
            };
            objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Claro.SIACU.Data.IFI.Fixed.PackagePurchaseService.ConsultarClaroPuntos(objRequest, oRestRequest);
            });

            return objResponse;
        }


        public static ConsultarPaqDisponiblesResponse ConsultarPaqDisponibles(ConsultarPaqDisponiblesRequest objRequest)
        {
            ConsultarPaqDisponiblesResponse objResponse = new ConsultarPaqDisponiblesResponse();

            Claro.SIACU.Entity.IFI.Fixed.GetConsultarPaqDisponibles.RestConsultarPaqDisponiblesRequest oRestRequest = new Claro.SIACU.Entity.IFI.Fixed.GetConsultarPaqDisponibles.RestConsultarPaqDisponiblesRequest
            {
                MessageRequest = new Claro.SIACU.Entity.IFI.Fixed.GetConsultarPaqDisponibles.RestConsultarPaqDisponiblesMessageRequest
                {
                    Header = new Claro.SIACU.Entity.IFI.Fixed.GetConsultarPaqDisponibles.RestConsultarPaqDisponiblesHeader
                    {
                        HeaderRequest = new Claro.SIACU.Entity.IFI.Fixed.GetConsultarPaqDisponibles.RestConsultarPaqDisponiblesHeaderRequest
                        {
                            consumer = objRequest.MessageRequest.Header.HeaderRequest.consumer,
                            country = objRequest.MessageRequest.Header.HeaderRequest.country,
                            dispositivo = objRequest.MessageRequest.Header.HeaderRequest.dispositivo,
                            language = objRequest.MessageRequest.Header.HeaderRequest.language,
                            modulo = objRequest.MessageRequest.Header.HeaderRequest.modulo,
                            msgType = objRequest.MessageRequest.Header.HeaderRequest.msgType,
                            operation = objRequest.MessageRequest.Header.HeaderRequest.operation,
                            pid = objRequest.MessageRequest.Header.HeaderRequest.pid,
                            system = objRequest.MessageRequest.Header.HeaderRequest.system,
                            timestamp = objRequest.MessageRequest.Header.HeaderRequest.timestamp,
                            userId = objRequest.MessageRequest.Header.HeaderRequest.userId,
                            wsIp = objRequest.MessageRequest.Header.HeaderRequest.wsIp
                        }
                    },
                    Body = new Claro.SIACU.Entity.IFI.Fixed.GetConsultarPaqDisponibles.RestConsultarPaqDisponiblesBodyRequest
                    {

                        idCategoria = objRequest.MessageRequest.Body.idCategoria,
                        idContrato = objRequest.MessageRequest.Body.idContrato,
                        codigoCategoria = objRequest.MessageRequest.Body.codigoCategoria,
                        prepagoCode = objRequest.MessageRequest.Body.prepagoCode,
                        tmCode = objRequest.MessageRequest.Body.tmCode
                    }
                }
            };
            objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Claro.SIACU.Data.IFI.Fixed.PackagePurchaseService.ConsultarPaqDisponibles(objRequest, oRestRequest);
            });

            return objResponse;
        }

        //public static ComprarPaquetesResponse ComprarPaquetes(ComprarPaquetesRequest objRequest)
        //{
        //    ComprarPaquetesResponse objResponse = new ComprarPaquetesResponse();

        //    Claro.SIACU.Entity.IFI.Fixed.GetComprarPaquetes.RestComprarPaquetesRequest oRestRequest = new Claro.SIACU.Entity.IFI.Fixed.GetComprarPaquetes.RestComprarPaquetesRequest
        //    {
        //        MessageRequest = new Claro.SIACU.Entity.IFI.Fixed.GetComprarPaquetes.RestComprarPaquetesMessageRequest
        //        {
        //            Header = new Claro.SIACU.Entity.IFI.Fixed.GetComprarPaquetes.RestComprarPaquetesHeader
        //            {
        //                HeaderRequest = new Claro.SIACU.Entity.IFI.Fixed.GetComprarPaquetes.RestComprarPaquetesHeaderRequest
        //                {
        //                    consumer = objRequest.MessageRequest.Header.HeaderRequest.consumer,
        //                    country = objRequest.MessageRequest.Header.HeaderRequest.country,
        //                    dispositivo = objRequest.MessageRequest.Header.HeaderRequest.dispositivo,
        //                    language = objRequest.MessageRequest.Header.HeaderRequest.language,
        //                    modulo = objRequest.MessageRequest.Header.HeaderRequest.modulo,
        //                    msgType = objRequest.MessageRequest.Header.HeaderRequest.msgType,
        //                    operation = objRequest.MessageRequest.Header.HeaderRequest.operation,
        //                    pid = objRequest.MessageRequest.Header.HeaderRequest.pid,
        //                    system = objRequest.MessageRequest.Header.HeaderRequest.system,
        //                    timestamp = objRequest.MessageRequest.Header.HeaderRequest.timestamp,
        //                    userId = objRequest.MessageRequest.Header.HeaderRequest.userId,
        //                    wsIp = objRequest.MessageRequest.Header.HeaderRequest.wsIp
        //                }
        //            },
        //            Body = new Claro.SIACU.Entity.IFI.Fixed.GetComprarPaquetes.RestComprarPaquetesBodyRequest
        //            {
        //                comprarPaquetesRequest = new Claro.SIACU.Entity.IFI.Fixed.GetComprarPaquetes.comprarPaquetesRequest
        //                {
        //                    msisdn = objRequest.MessageRequest.Body.msisdn,
        //                    monto = objRequest.MessageRequest.Body.monto,
        //                    paquete = objRequest.MessageRequest.Body.paquete,
        //                    customerId = objRequest.MessageRequest.Body.customerId,
        //                    planBase = objRequest.MessageRequest.Body.planBase,
        //                    tipoProducto = objRequest.MessageRequest.Body.tipoProducto,
        //                    tipoCliente = objRequest.MessageRequest.Body.tipoCliente,
        //                    cicloFact = objRequest.MessageRequest.Body.cicloFact,
        //                    fechaAct = objRequest.MessageRequest.Body.fechaAct,
        //                    cargoFijo = objRequest.MessageRequest.Body.cargoFijo,
        //                    tipoPago = objRequest.MessageRequest.Body.tipoPago,
        //                    departamento = objRequest.MessageRequest.Body.departamento,
        //                    provincia = objRequest.MessageRequest.Body.provincia,
        //                    distrito = objRequest.MessageRequest.Body.distrito
        //                }
                     
                            
        //            }
        //        }
        //    };
        //    objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
        //    {
        //        return Claro.SIACU.Data.IFI.Fixed.PackagePurchaseService.ComprarPaquetes(objRequest, oRestRequest);
        //    });

        //    return objResponse;
        //}



        public static ComprarPaquetesBodyResponse ComprarPaquetesRest(ComprarPaquetesMessageRequest objRequest)
        {
            ComprarPaquetesBodyResponse objResponse = new ComprarPaquetesBodyResponse();

            objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Claro.SIACU.Data.IFI.Fixed.PackagePurchaseService.ComprarPaquetesRest(objRequest);
            });

            return objResponse;
        }

        public static PCRFConnectorResponse ConsultarPCRFDegradacion(PCRFConnectorRequest objRequest)
        {
            var objResponse = new PCRFConnectorResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                 () =>
                 {
                     return Claro.SIACU.Data.IFI.Fixed.PackagePurchaseService.ConsultarPCRFDegradacion(objRequest);
                 });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static List<Entity.IFI.Common.Client> GetDatosporNroDocumentos(string strIdSession, string strTransaction, string strTipDoc, string strDocumento, string strEstado)
        {
            var objResponse = new List<Entity.IFI.Common.Client>();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strTransaction,
                    () =>
                    {
                        return Claro.SIACU.Data.IFI.Fixed.PackagePurchaseService.GetDatosporNroDocumentos(strIdSession, strTransaction, strTipDoc, strDocumento, strEstado);
                    });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return objResponse;
        }
    }
}
