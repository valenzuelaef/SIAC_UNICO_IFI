using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed
{
    public class OrderSubType
    {
        [DbColumn("COD_SUBTIPO_ORDEN")]
        [DataMember]
        public string COD_SUBTIPO_ORDEN { get; set; }
        [DbColumn("TIEMPO_MIN")]
        [DataMember]
        public string TIEMPO_MIN { get; set; }
        [DbColumn("DESCRIPCION")]
        [DataMember]
        public string DESCRIPCION { get; set; }

    }
}
