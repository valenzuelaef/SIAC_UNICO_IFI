using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI
{
    [DataContract]
    public class SuspensionLte
    {
        [DataMember]
        public string codigoAplicacion { get; set; }
        [DataMember]
        public string ipAplicacion { get; set; }
        [DataMember]
        public int flagAccion { get; set; }
        [DataMember]
        public System.DateTime fechaProgramacion { get; set; }
        [DataMember]
        public string coId { get; set; }
        [DataMember]
        public int nroDias { get; set; }
        [DataMember]
        public int fideliza { get; set; }
        [DataMember]
        public System.DateTime fechaSuspension { get; set; }
        [DataMember]
        public string ticklerCode { get; set; }
        [DataMember]
        public string desTickler { get; set; }
        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public string codCliente { get; set; }
        [DataMember]
        public string coState { get; set; }
        [DataMember]
        public string reason { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string tipoServicio { get; set; }
        [DataMember]
        public string coSer { get; set; }
        [DataMember]
        public string tipoRegistro { get; set; }
        [DataMember]
        public string usuarioSistema { get; set; }
        [DataMember]
        public string usuarioApp { get; set; }
        [DataMember]
        public string emailUsuarioApp { get; set; }
        [DataMember]
        public string desCoSer { get; set; }
        [DataMember]
        public string codigoInteraccion { get; set; }
        [DataMember]
        public string nroCuenta { get; set; }
        [DataMember]
        public string nombreAplicacion { get; set; }
        [DataMember]
        public double montoOCC { get; set; }
    }
}
