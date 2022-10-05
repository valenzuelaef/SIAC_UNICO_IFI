using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetEmployeByUser
{
    [DataContract(Name = "EmployeeResponse")]
    public class EmployeeResponse
    {
        [DataMember]
        public List<Entity.IFI.Common.Employee> lstEmployee { set; get; }
    }
}
