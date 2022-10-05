using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetLastInvoiceData
{
    [DataContract(Name = "LastInvoiceDataResponseCommon")]
    public class LastInvoiceDataResponse
    {
        [DataMember]
        public List<Receipt> LastInvoiceDatas { get; set; }
    }
}
