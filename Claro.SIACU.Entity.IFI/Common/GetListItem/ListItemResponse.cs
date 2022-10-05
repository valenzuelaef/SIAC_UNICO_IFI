using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetListItem
{
    [DataContract(Name = "ListItemResponse")]
    public class ListItemResponse
    {
        [DataMember]
        public List<Entity.IFI.Common.ListItem> lstListItem { get; set; }
    
    
    }

}
