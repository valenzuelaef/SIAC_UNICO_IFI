using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class ClienteModel
    {
        public string tipoDoc { get; set; }
        public string numeroDoc { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string telefonoContacto { get; set; }
        
    }
}