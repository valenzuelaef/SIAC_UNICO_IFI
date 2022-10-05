using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangePostalAddress
{
    [DataContract(Name = "SaveChangePostalAddressResponse")]
    public class SaveChangePostalAddressResponse
    {
        [DataMember]
        public bool StrResult { get; set; }

    }
}