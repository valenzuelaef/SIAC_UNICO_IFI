using System.Runtime.Serialization;
using System.Collections.Generic;


namespace Claro.SIACU.Entity.IFI.Fixed.GetOrderType
{
    [DataContract(Name = "OrderTypesResponseHfc")]
    public class OrderTypesResponse
    {
        [DataMember]
        public List<OrderType> ordertypes { get; set; }
    }
}
