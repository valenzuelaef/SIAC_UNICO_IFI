using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.PostPlanMigration
{

    public class RestPlanMigrationRequest 
    {
        public RestPlanMigrationMessageRequest MessageRequest { get; set; }

    }
    public class RestPlanMigrationHeaderRequest
    {
        public string consumer { get; set; }

        public string country { get; set; }

        public string dispositivo { get; set; }

        public string language { get; set; }

        public string modulo { get; set; }

        public string msgType { get; set; }

        public string operation { get; set; }

        public string pid { get; set; }

        public string system { get; set; }

        public string timestamp { get; set; }

        public string userId { get; set; }

        public string wsIp { get; set; }
    }

    public class RestPlanMigrationBodyRequest
    {
        public string servvMsisd { get; set; }

        public DateTime servdFechaProg { get; set; }

        public string codId { get; set; }

        public string simCard { get; set; }

        public string tmCodeActual { get; set; }

        public string tmCodeNuevo { get; set; }

        public string customerId { get; set; }

        public string servvTipoServicio { get; set; }

        public string serviCoser { get; set; }

        public string tipoReg { get; set; }

        public string servvUsuarioSis { get; set; }

        public string servvUsuarioApp { get; set; }

        public string servvEmailUsuarioApp { get; set; }

        public string servcEstado { get; set; }

        public string descCoSer { get; set; }

        public string servcNrocuenta { get; set; }

        public string contactoObjId { get; set; }

        public string flagReg { get; set; }

        //Main Interaction
        public string account { get; set; }

        public string phone { get; set; }

        public string tipo { get; set; }

        public string clase { get; set; }

        public string subclase { get; set; }

        public string metodoContacto { get; set; }

        public string tipoInter { get; set; }

        public string agente { get; set; }

        public string usrProceso { get; set; }

        public string hechoEnUno { get; set; }

        public string notas { get; set; }

        public string flagCaso { get; set; }

        public string resultado { get; set; }

        public string servafect { get; set; }

        public string inconven { get; set; }

        public string servafectCode { get; set; }

        public string inconvenCode { get; set; }

        public string codPlano { get; set; }

        public string valor1 { get; set; }

        public string valor2 { get; set; }
        //Main Interaction

        //Plus Interaction
        public string inter1 { get; set; }

        public string inter2 { get; set; }

        public string inter3 { get; set; }

        public string inter4 { get; set; }

        public string inter5 { get; set; }

        public string inter6 { get; set; }

        public string inter7 { get; set; }

        public double inter8 { get; set; }

        public double inter9 { get; set; }

        public double inter10 { get; set; }

        public double inter11 { get; set; }

        public double inter12 { get; set; }

        public double inter13 { get; set; }

        public double inter14 { get; set; }

        public string inter15 { get; set; }

        public string inter16 { get; set; }

        public string inter17 { get; set; }

        public string inter18 { get; set; }

        public string inter19 { get; set; }

        public string inter20 { get; set; }

        public string inter21 { get; set; }

        public double inter22 { get; set; }

        public double inter23 { get; set; }

        public double inter24 { get; set; }

        public double inter25 { get; set; }

        public double inter26 { get; set; }

        public double inter27 { get; set; }

        public double inter28 { get; set; }
        
        public string inter29 { get; set; }

        public string inter30 { get; set; }

        public double plusInter2Interact { get; set; }

        public double adjustmentAmount { get; set; }

        public string adjustmentReason { get; set; }

        public string address { get; set; }

        public string amountUnit { get; set; }

        public string birthday { get; set; }

        public string clarifyInteraction { get; set; }

        public string claroLdn1 { get; set; }

        public string claroLdn2 { get; set; }

        public string claroLdn3 { get; set; }

        public string claroLdn4 { get; set; }

        public string claroLocal1 { get; set; }

        public string claroLocal2 { get; set; }

        public string claroLocal3 { get; set; }

        public string claroLocal4 { get; set; }

        public string claroLocal5 { get; set; }

        public string claroLocal6 { get; set; }

        public string contactPhone { get; set; }

        public string dniLegalRep { get; set; }

        public string documentNumber { get; set; }

        public string email { get; set; }

        public string firstName { get; set; }

        public string fixedNumber { get; set; }

        public string flagChangeUser { get; set; }

        public string flagLegalRep { get; set; }

        public string flagOther { get; set; }

        public string flagTitular { get; set; }

        public string imei { get; set; }

        public string lastName { get; set; }

        public string lastNameRep { get; set; }

        public string ldiNumber { get; set; }

        public string nameLegalRep { get; set; }

        public string oldClaroLdn1 { get; set; }

        public string oldClaroLdn2 { get; set; }

        public string oldClaroLdn3 { get; set; }

        public string oldClaroLdn4 { get; set; }

        public string oldClaroLocal1 { get; set; }

        public string oldClaroLocal2 { get; set; }

        public string oldClaroLocal3 { get; set; }

        public string oldClaroLocal4 { get; set; }

        public string oldClaroLocal5 { get; set; }

        public string oldClaroLocal6 { get; set; }

        public string oldDocNumber { get; set; }

        public string oldFirstName { get; set; }

        public string oldFixedPhone { get; set; }

        public string oldLastName { get; set; }

        public string oldLdiNumber { get; set; }

        public string oldFixedNumber { get; set; }

        public string operationType { get; set; }

        public string otherDocNumber { get; set; }

        public string otherFirstName { get; set; }

        public string otherLastName { get; set; }

        public string otherPhone { get; set; }

        public string phoneLegalRep { get; set; }

        public string referencePhone { get; set; }

        public string reason { get; set; }

        public string model { get; set; }

        public string lotCode { get; set; }

        public string flagRegistered { get; set; }

        public string registrationReason { get; set; }

        public string claroNumber { get; set; }

        public string month { get; set; }

        public string ostNumber { get; set; }

        public string basket { get; set; }

        public string expireDate { get; set; }

        public string address5 { get; set; }

        public double chargeAmount { get; set; }

        public string city { get; set; }

        public string contactSex { get; set; }

        public string department { get; set; }

        public string district { get; set; }

        public string emailConfirmation { get; set; }

        public string fax { get; set; }

        public string flagCharge { get; set; }

        public string maritalStatus { get; set; }

        public string occupation { get; set; }

        public string position { get; set; }

        public string referenceAddress { get; set; }

        public string typeDocument { get; set; }

        public string zipCode { get; set; }

        public string iccid { get; set; }

        public string flagContingencia { get; set; }
        //Plus Interaction

        public string flagPenalidad { get; set; }

        public double reintegro { get; set; }

        public double penalidadCalculada { get; set; }

        public double montoPenalidadCobrar { get; set; }

        public string flagFidelizaPenalidad { get; set; }

        public double montofidelizaPenalidad { get; set; }

        public int occPenalidad { get; set; }

        public double conceptooccPenalidad { get; set; }

        public string ideaiWS { get; set; }

        public string ipCliente { get; set; }

        public string cuentaUsuario { get; set; }

        public string nombreServidor { get; set; }

        public string telefono { get; set; }

        public string texto { get; set; }

        public RestPlanMigrationService listaServicios { get; set; }

        public RestPlanMigrationSNCode listaSnCode { get; set; }

        public string parametroConstancia { get; set; }

        public string codigoProducto { get; set; }
    }

    public class RestPlanMigrationServiceList
    {
        public string nombreServicio { get; set; }

		public string tipoServicio { get; set; }

		public string grupoServicio { get; set; }

		public string cf { get; set; }

		public string equipo { get; set; }

		public string cantidad { get; set; }

		public string codigoServicio { get; set; }

		public string snCode { get; set; }

		public string spCode { get; set; }

    }

    public class RestPlanMigrationSnCodeList
    {
        public string sncode { get; set; }

		public string cf { get; set; }

		public string accion { get; set; }

        public string spcode { get; set; }
    }

    public class RestPlanMigrationService
    {
        public List<RestPlanMigrationServiceList> servicio { get; set; }
    }

    public class RestPlanMigrationSNCode
    {
        public List<RestPlanMigrationSnCodeList> datosSnCode { get; set; }
    }

    public class RestPlanMigrationMessageRequest
    {
        public RestPlanMigrationHeader Header { get; set; }

        public RestPlanMigrationBodyRequest Body { get; set; }
    }

    public class RestPlanMigrationHeader
    {
        public RestPlanMigrationHeaderRequest HeaderRequest { get; set; }
    }
}
