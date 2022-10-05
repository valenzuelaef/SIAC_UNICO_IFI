using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.PostPlanMigration
{
    [DataContract(Name = "PlanMigrationBodyResponse")]
    public class PlanMigrationBodyResponse
    {
        [DataMember(Name = "codigoArbol")]
        public string TreeCode { get; set; }

        [DataMember(Name = "mensajeError")]
        public string ErrorMessage { get; set; }

        [DataMember(Name = "codigoRespuesta")]
        public string ResponseCode { get; set; }

        [DataMember(Name = "mensajeRespuesta")]
        public string ResponseMessage { get; set; }

        [DataMember(Name = "idInteraccion")]
        public string IdInteraction { get; set; }

        [DataMember(Name = "rutaConstancia")]
        public string RouteConstancy { get; set; }

        [DataMember(Name = "Fault")]
        public GetDataPower.FaultResponse Fault { get; set; }
    }
}
