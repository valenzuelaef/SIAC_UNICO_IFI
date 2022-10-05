using Claro.SIACU.Entity.IFI.Fixed;
using System.Runtime.Serialization;
using EntitiesFixed = Claro.SIACU.Entity.IFI.Fixed;

namespace Claro.SIACU.Entity.IFI.Common.GetInsertInteractHFC
{
    public class InsertInteractHFCRequest : Claro.Entity.Request
    {
        [DataMember]
        public EntitiesFixed.Interaction Interaction { get; set; }

        public InsertInteractHFCRequest() {
            Interaction = new EntitiesFixed.Interaction();
        }
    }
}
