using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetService
{
    public class ServiceResponse
    {
        [DataMember]
        public Entity.IFI.Fixed.Service ObjService { get; set; }

        [DataMember]
        public List<Entity.IFI.Fixed.Service> ListService { get; set; }
    }
}
