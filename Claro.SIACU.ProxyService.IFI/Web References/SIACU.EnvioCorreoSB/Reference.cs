﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Claro.SIACU.ProxyService.IFI.SIACU.EnvioCorreoSB {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1532.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="EnvioCorreoSBPortTypeSOAP11Binding", Namespace="http://claro.com.pe/eai/util/enviocorreo")]
    public partial class EnvioCorreoSBPortTypeSOAP11BindingQSService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback enviarCorreoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public EnvioCorreoSBPortTypeSOAP11BindingQSService() {
            this.Url = global::Claro.SIACU.ProxyService.IFI.Properties.Settings.Default.Claro_SIACU_ProxyService_IFI_SIACU_EnvioCorreoSB_EnvioCorreoSBPortTypeSOAP11BindingQSService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event enviarCorreoCompletedEventHandler enviarCorreoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://claro.com.pe/eai/util/enviocorreo/enviarCorreo", RequestElementName="enviarCorreoRequest", RequestNamespace="http://claro.com.pe/eai/util/enviocorreo/types", ResponseNamespace="http://claro.com.pe/eai/util/enviocorreo/types", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("auditResponse")]
        public AuditTypeResponse enviarCorreo(AuditTypeRequest auditRequest, string remitente, string destinatario, string asunto, string mensaje, string htmlFlag, [System.Xml.Serialization.XmlArrayItemAttribute("archivoAdjunto", IsNullable=false)] ArchivoAdjunto[] listaArchivosAdjuntos, [System.Xml.Serialization.XmlArrayItemAttribute("ParametroComplexType", IsNullable=false)] ParametroOpcionalComplexType[] parametrosOpcionalesRequest, [System.Xml.Serialization.XmlArrayItemAttribute("ParametroComplexType", IsNullable=false)] out ParametroOpcionalComplexType[] parametrosOpcionalesResponse) {
            object[] results = this.Invoke("enviarCorreo", new object[] {
                        auditRequest,
                        remitente,
                        destinatario,
                        asunto,
                        mensaje,
                        htmlFlag,
                        listaArchivosAdjuntos,
                        parametrosOpcionalesRequest});
            parametrosOpcionalesResponse = ((ParametroOpcionalComplexType[])(results[1]));
            return ((AuditTypeResponse)(results[0]));
        }
        
        /// <remarks/>
        public void enviarCorreoAsync(AuditTypeRequest auditRequest, string remitente, string destinatario, string asunto, string mensaje, string htmlFlag, ArchivoAdjunto[] listaArchivosAdjuntos, ParametroOpcionalComplexType[] parametrosOpcionalesRequest) {
            this.enviarCorreoAsync(auditRequest, remitente, destinatario, asunto, mensaje, htmlFlag, listaArchivosAdjuntos, parametrosOpcionalesRequest, null);
        }
        
        /// <remarks/>
        public void enviarCorreoAsync(AuditTypeRequest auditRequest, string remitente, string destinatario, string asunto, string mensaje, string htmlFlag, ArchivoAdjunto[] listaArchivosAdjuntos, ParametroOpcionalComplexType[] parametrosOpcionalesRequest, object userState) {
            if ((this.enviarCorreoOperationCompleted == null)) {
                this.enviarCorreoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnenviarCorreoOperationCompleted);
            }
            this.InvokeAsync("enviarCorreo", new object[] {
                        auditRequest,
                        remitente,
                        destinatario,
                        asunto,
                        mensaje,
                        htmlFlag,
                        listaArchivosAdjuntos,
                        parametrosOpcionalesRequest}, this.enviarCorreoOperationCompleted, userState);
        }
        
        private void OnenviarCorreoOperationCompleted(object arg) {
            if ((this.enviarCorreoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.enviarCorreoCompleted(this, new enviarCorreoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/util/enviocorreo/types")]
    public partial class AuditTypeRequest {
        
        private string idTransaccionField;
        
        private string codigoAplicacionField;
        
        private string ipAplicacionField;
        
        private string usrAplicacionField;
        
        /// <comentarios/>
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
            }
        }
        
        /// <comentarios/>
        public string codigoAplicacion {
            get {
                return this.codigoAplicacionField;
            }
            set {
                this.codigoAplicacionField = value;
            }
        }
        
        /// <comentarios/>
        public string ipAplicacion {
            get {
                return this.ipAplicacionField;
            }
            set {
                this.ipAplicacionField = value;
            }
        }
        
        /// <comentarios/>
        public string usrAplicacion {
            get {
                return this.usrAplicacionField;
            }
            set {
                this.usrAplicacionField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/util/enviocorreo/types")]
    public partial class AuditTypeResponse {
        
        private string idTransaccionField;
        
        private string codigoRespuestaField;
        
        private string mensajeRespuestaField;
        
        /// <comentarios/>
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
            }
        }
        
        /// <comentarios/>
        public string codigoRespuesta {
            get {
                return this.codigoRespuestaField;
            }
            set {
                this.codigoRespuestaField = value;
            }
        }
        
        /// <comentarios/>
        public string mensajeRespuesta {
            get {
                return this.mensajeRespuestaField;
            }
            set {
                this.mensajeRespuestaField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/util/enviocorreo/types")]
    public partial class ParametroOpcionalComplexType {
        
        private string claveField;
        
        private string valorField;
        
        /// <comentarios/>
        public string clave {
            get {
                return this.claveField;
            }
            set {
                this.claveField = value;
            }
        }
        
        /// <comentarios/>
        public string valor {
            get {
                return this.valorField;
            }
            set {
                this.valorField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/util/enviocorreo/types")]
    public partial class ArchivoAdjunto {
        
        private string nombreField;
        
        private string cabeceraField;
        
        private byte[] archivoField;
        
        /// <comentarios/>
        public string nombre {
            get {
                return this.nombreField;
            }
            set {
                this.nombreField = value;
            }
        }
        
        /// <comentarios/>
        public string cabecera {
            get {
                return this.cabeceraField;
            }
            set {
                this.cabeceraField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] archivo {
            get {
                return this.archivoField;
            }
            set {
                this.archivoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1532.0")]
    public delegate void enviarCorreoCompletedEventHandler(object sender, enviarCorreoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1532.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class enviarCorreoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal enviarCorreoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public AuditTypeResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((AuditTypeResponse)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public ParametroOpcionalComplexType[] parametrosOpcionalesResponse {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ParametroOpcionalComplexType[])(this.results[1]));
            }
        }
    }
}

#pragma warning restore 1591