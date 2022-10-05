using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.GetPCRFConsultation
{
    [DataContract]
    public class PCRFConnectorResponse
    {
        [DataMember]
        public SuscriberQuota listSuscriberQuota { get; set; }
        [DataMember]
        public Suscriber listSuscriber { get; set; }
        [DataMember]
        public string strMensajeQuota { get; set; }
        [DataMember]
        public string strMensaje { get; set; }
        [DataMember]
        public string strTelefonoLTE { get; set; }
        //RF-02
        [DataMember]
        public string codRespuesta { get; set; }
        public string msjRespuesta { get; set; } 
        [DataMember]
        public List<SuscriberQuota> listaSuscriberQuota { get; set; }

        [DataMember]
        public bool bBono { get; set; }
    }
}
