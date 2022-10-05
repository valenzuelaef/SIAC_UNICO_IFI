using System.Net;
using System.IO;
using Claro.Data;
using Claro.Data.Configuration;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeServiceAddress;
using System.Xml;
using System.Text;
using System.Xml.Linq;


namespace Claro.SIACU.Data.IFI.Postpaid
{
    public class SoapService
    {
        public static XmlDocument PostInvoque(string strURL, string strXML)
        {
            XmlDocument xmlDocument = null;
            HttpWebRequest request = WebRequest.Create(strURL) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "text/xml";
            
            byte[] bytes = Encoding.UTF8.GetBytes(strXML);

            request.ContentLength = bytes.Length;

            using (Stream putStream = request.GetRequestStream())
            {
                putStream.Write(bytes, 0, bytes.Length);
            }
            WebResponse response = request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(reader.ReadToEnd());
            }
            return xmlDocument;
        }
    }
}
