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

namespace Claro.SIACU.ProxyService.IFI.RegistroAuditoriaWS {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="RegistroAuditoriaPortSB11Binding", Namespace="http://service.eai.auditoria.claro.com.pe/RegistroAuditoriaWS")]
    public partial class RegistroAuditoriaService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback registroAuditoriaOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public RegistroAuditoriaService() {
            this.Url = global::Claro.SIACU.ProxyService.IFI.Properties.Settings.Default.Claro_SIACU_ProxyService_IFI_RegistroAuditoriaWS_RegistroAuditoriaService;
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
        public event registroAuditoriaCompletedEventHandler registroAuditoriaCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.eai.auditoria.claro.com.pe/RegistroAuditoriaWS/registroAuditoria", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("registroAuditoriaResponse", Namespace="http://service.eai.auditoria.claro.com.pe/RegistroAuditoriaWS")]
        public registroAuditoriaResponse registroAuditoria([System.Xml.Serialization.XmlElementAttribute("registroAuditoria", Namespace="http://service.eai.auditoria.claro.com.pe/RegistroAuditoriaWS")] registroAuditoria registroAuditoria1) {
            object[] results = this.Invoke("registroAuditoria", new object[] {
                        registroAuditoria1});
            return ((registroAuditoriaResponse)(results[0]));
        }
        
        /// <remarks/>
        public void registroAuditoriaAsync(registroAuditoria registroAuditoria1) {
            this.registroAuditoriaAsync(registroAuditoria1, null);
        }
        
        /// <remarks/>
        public void registroAuditoriaAsync(registroAuditoria registroAuditoria1, object userState) {
            if ((this.registroAuditoriaOperationCompleted == null)) {
                this.registroAuditoriaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnregistroAuditoriaOperationCompleted);
            }
            this.InvokeAsync("registroAuditoria", new object[] {
                        registroAuditoria1}, this.registroAuditoriaOperationCompleted, userState);
        }
        
        private void OnregistroAuditoriaOperationCompleted(object arg) {
            if ((this.registroAuditoriaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.registroAuditoriaCompleted(this, new registroAuditoriaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://service.eai.auditoria.claro.com.pe/RegistroAuditoriaWS")]
    public partial class registroAuditoria {
        
        private AuditRequest auditField;
        
        private RegistroRequest registroRequestField;
        
        private RequestOpcionalComplexType[] listaOpcionalRequestField;
        
        /// <comentarios/>
        public AuditRequest Audit {
            get {
                return this.auditField;
            }
            set {
                this.auditField = value;
            }
        }
        
        /// <comentarios/>
        public RegistroRequest RegistroRequest {
            get {
                return this.registroRequestField;
            }
            set {
                this.registroRequestField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("RequestOpcional", IsNullable=false)]
        public RequestOpcionalComplexType[] ListaOpcionalRequest {
            get {
                return this.listaOpcionalRequestField;
            }
            set {
                this.listaOpcionalRequestField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.eai.auditoria.claro.com.pe/RegistroAuditoriaWS")]
    public partial class AuditRequest {
        
        private string idTransaccionField;
        
        private string ipAplicacionField;
        
        private string aplicacionField;
        
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
        public string ipAplicacion {
            get {
                return this.ipAplicacionField;
            }
            set {
                this.ipAplicacionField = value;
            }
        }
        
        /// <comentarios/>
        public string aplicacion {
            get {
                return this.aplicacionField;
            }
            set {
                this.aplicacionField = value;
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.eai.auditoria.claro.com.pe/RegistroAuditoriaWS")]
    public partial class ResponseOpcionalComplexType {
        
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.eai.auditoria.claro.com.pe/RegistroAuditoriaWS")]
    public partial class AuditResponse {
        
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.eai.auditoria.claro.com.pe/RegistroAuditoriaWS")]
    public partial class RequestOpcionalComplexType {
        
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.eai.auditoria.claro.com.pe/RegistroAuditoriaWS")]
    public partial class RegistroRequest {
        
        private string transaccionField;
        
        private string servicioField;
        
        private string ipClienteField;
        
        private string nombreClienteField;
        
        private string ipServidorField;
        
        private string nombreServidorField;
        
        private string cuentaUsuarioField;
        
        private string telefonoField;
        
        private string montoField;
        
        private string textoField;
        
        /// <comentarios/>
        public string transaccion {
            get {
                return this.transaccionField;
            }
            set {
                this.transaccionField = value;
            }
        }
        
        /// <comentarios/>
        public string servicio {
            get {
                return this.servicioField;
            }
            set {
                this.servicioField = value;
            }
        }
        
        /// <comentarios/>
        public string ipCliente {
            get {
                return this.ipClienteField;
            }
            set {
                this.ipClienteField = value;
            }
        }
        
        /// <comentarios/>
        public string nombreCliente {
            get {
                return this.nombreClienteField;
            }
            set {
                this.nombreClienteField = value;
            }
        }
        
        /// <comentarios/>
        public string ipServidor {
            get {
                return this.ipServidorField;
            }
            set {
                this.ipServidorField = value;
            }
        }
        
        /// <comentarios/>
        public string nombreServidor {
            get {
                return this.nombreServidorField;
            }
            set {
                this.nombreServidorField = value;
            }
        }
        
        /// <comentarios/>
        public string cuentaUsuario {
            get {
                return this.cuentaUsuarioField;
            }
            set {
                this.cuentaUsuarioField = value;
            }
        }
        
        /// <comentarios/>
        public string telefono {
            get {
                return this.telefonoField;
            }
            set {
                this.telefonoField = value;
            }
        }
        
        /// <comentarios/>
        public string monto {
            get {
                return this.montoField;
            }
            set {
                this.montoField = value;
            }
        }
        
        /// <comentarios/>
        public string texto {
            get {
                return this.textoField;
            }
            set {
                this.textoField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://service.eai.auditoria.claro.com.pe/RegistroAuditoriaWS")]
    public partial class registroAuditoriaResponse {
        
        private AuditResponse auditResponseField;
        
        private ResponseOpcionalComplexType[] listaOpcionalResponseField;
        
        /// <comentarios/>
        public AuditResponse AuditResponse {
            get {
                return this.auditResponseField;
            }
            set {
                this.auditResponseField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ResponseOpcional", IsNullable=false)]
        public ResponseOpcionalComplexType[] listaOpcionalResponse {
            get {
                return this.listaOpcionalResponseField;
            }
            set {
                this.listaOpcionalResponseField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1532.0")]
    public delegate void registroAuditoriaCompletedEventHandler(object sender, registroAuditoriaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1532.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class registroAuditoriaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal registroAuditoriaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public registroAuditoriaResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((registroAuditoriaResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591