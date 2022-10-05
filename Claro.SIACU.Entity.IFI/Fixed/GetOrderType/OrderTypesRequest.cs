using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.IFI.Fixed.GetOrderType
{
    [DataContract(Name = "OrderTypesRequestHfc")]
    public class OrderTypesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vIdtiptra { get; set; }

    }
}
