using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetEmployeByUser
{
    [DataContract(Name = "EmployeeRequest")]
    public class EmployeeRequest : Claro.Entity.Request
    {
        [DataMember]
        public string UserName { get; set; }
    }
}
