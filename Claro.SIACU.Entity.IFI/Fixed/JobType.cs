using Claro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed
{
    public class JobType
    {
        [DbColumn("tiptra")]
        [DataMember]
        public string tiptra { get; set; }
        [DbColumn("descripcion")]
        [DataMember]
        public string descripcion { get; set; }
        [DbColumn("FLAG_FRANJA")]
        [DataMember]
        public string FLAG_FRANJA { get; set; }

    }
}
