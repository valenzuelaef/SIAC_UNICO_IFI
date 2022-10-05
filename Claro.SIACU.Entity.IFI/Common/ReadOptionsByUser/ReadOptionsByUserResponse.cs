using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.ReadOptionsByUser
{
    [DataContract(Name = "ReadOptionsByUserResponse")]
    public class ReadOptionsByUserResponse
    {
        [DataMember]
        public List<Entity.IFI.Common.PaginaOption> ListOption { set; get; }
    }
}
