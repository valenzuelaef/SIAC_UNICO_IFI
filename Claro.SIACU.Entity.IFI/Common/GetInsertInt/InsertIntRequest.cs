using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Claro.SIACU.Entity.IFI.Common;
namespace Claro.SIACU.Entity.IFI.Common.GetInsertInt
{
    [DataContract(Name = "InsertIntRequestCommon")]
   public  class InsertIntRequest:Claro.Entity.Request
    {
       [DataMember]
       public Iteraction item { get; set; }
    }
}
