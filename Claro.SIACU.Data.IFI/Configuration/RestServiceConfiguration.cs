using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Data.IFI.Configuration
{
    internal struct RestServiceConfiguration
    {
        public static readonly string PlanMigration_Service_DP = "RegisterPlanMigration";
        public static readonly string ConsultarPCRFPaquetesAdic_DP = "ConsultarPCRFPaquetesAdic"; //INI - RF-04 - RF-05 Evalenzs
        public static readonly string ConsultarPaqDisponibles_DP = "ConsultarPaqDisponibles";
        public static readonly string ConsultarClaroPuntos_DP = "ConsultarClaroPuntos";
        public static readonly string ComprarPaquetes_DP = "ComprarPaquetes";
        public static readonly string ValidarCoberturaDireccion_DP = "validarCoberturaDireccion";
    }
}
