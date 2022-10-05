using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid
{
    [DataContract]
    public class Interaction
    {
        [DataMember]
        public string OBJID_CONTACT { get; set; }

        [DataMember]
        public string OBJID_SITE { get; set; }

        [DataMember]
        public string ACCOUNT { get; set; }

        [DataMember]
        public string ID_INTERACTION { get; set; }

        [DataMember]
        public string CREATION_DATE { get; set; }

        [DataMember]
        public string START_DATE { get; set; }

        [DataMember]
        public string PHONE { get; set; }

        [DataMember]
        public string TYPE { get; set; }

        [DataMember]
        public string CLASS { get; set; }

        [DataMember]
        public string SUBCLASS { get; set; }

        [DataMember]
        public string TYPIFICATION { get; set; }

        [DataMember]
        public string TYPE_CODE { get; set; }

        [DataMember]
        public string CLASS_CODE { get; set; }

        [DataMember]
        public string SUBCLASS_CODE { get; set; }

        [DataMember]
        public string INSERTED_BY { get; set; }

        [DataMember]
        public string TYPE_INTER { get; set; }

        [DataMember]
        public string METHOD { get; set; }

        [DataMember]
        public string RESULT { get; set; }

        [DataMember]
        public string MADEINONE { get; set; }

        [DataMember]
        public string AGENT { get; set; }

        [DataMember]
        public string AGENT_FIRSTNAME { get; set; }

        [DataMember]
        public string AGENT_LASTNAME { get; set; }

        [DataMember]
        public string ID_CASE { get; set; }

        [DataMember]
        public string NOTES { get; set; }

        [DataMember]
        public string FLAG_CASE { get; set; }

        [DataMember]
        public string USER_PROCESS { get; set; }

        [DataMember]
        public string SERVICE { get; set; }

        [DataMember]
        public string INCONVENIENT { get; set; }

        [DataMember]
        public string IS_TFI { get; set; }

        [DataMember]
        public string CONTRACT { get; set; }

        [DataMember]
        public string MAP { get; set; }

        [DataMember]
        public string SERVICECODE { get; set; }

        [DataMember]
        public string INCONVENIENTCODE { get; set; }

        [DataMember]
        public string VALUE1 { get; set; }

        [DataMember]
        public string VALUE2 { get; set; }
    }
}
