using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.IFI.Fixed.GetOrderSubType
{
    [DataContract(Name = "OrderSubTypesRequestHfc")]
    public class OrderSubTypesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string av_cod_tipo_orden { get; set; }

    }
}
