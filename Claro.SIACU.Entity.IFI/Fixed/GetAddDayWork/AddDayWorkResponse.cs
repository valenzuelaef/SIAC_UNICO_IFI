using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetAddDayWork
{
    [DataContract]

    public class AddDayWorkResponse
    {
        [DataMember]
        public string FechaResultado { get; set; }

        [DataMember]
        public int CodError { get; set; }

        [DataMember]
        public string DescError { get; set; }

    }
}
