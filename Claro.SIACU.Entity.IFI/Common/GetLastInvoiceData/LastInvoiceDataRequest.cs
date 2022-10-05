using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetLastInvoiceData
{
    [DataContract(Name = "LastInvoiceDataRequestCommon")]
    public class LastInvoiceDataRequest : Claro.Entity.Request
    {
            [DataMember]
            public string strCustomerCode { get; set; }

    }
}
