//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", ConfigurationName="SIACUConsultaSeguridad.ConsultaSeguridad")]
    public interface ConsultaSeguridad {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/leerOpcione" +
            "sMenu", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.leerOpcionesMenuResponse leerOpcionesMenu(Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.leerOpcionesMenuRequest request);
        
        // CODEGEN: Generando contrato de mensaje, ya que la operación tiene múltiples valores de devolución.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/leerOpcione" +
            "sMenu", ReplyAction="*")]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.leerOpcionesMenuResponse> leerOpcionesMenuAsync(Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.leerOpcionesMenuRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/verificaUsu" +
            "ario", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.verificaUsuarioResponse verificaUsuario(Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.verificaUsuarioRequest request);
        
        // CODEGEN: Generando contrato de mensaje, ya que la operación tiene múltiples valores de devolución.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/verificaUsu" +
            "ario", ReplyAction="*")]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.verificaUsuarioResponse> verificaUsuarioAsync(Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.verificaUsuarioRequest request);
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/")]
    public partial class menuType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string aplicCodField;
        
        private string opcCodPadField;
        
        private string opcicCodField;
        
        private string opcicNivPadField;
        
        private string opcicNivField;
        
        private string opcicDesField;
        
        private string opcicAbrevField;
        
        private string opcicNomPagField;
        
        private string opcicNumOrdField;
        
        private string opcicD1Field;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string AplicCod {
            get {
                return this.aplicCodField;
            }
            set {
                this.aplicCodField = value;
                this.RaisePropertyChanged("AplicCod");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string OpcCodPad {
            get {
                return this.opcCodPadField;
            }
            set {
                this.opcCodPadField = value;
                this.RaisePropertyChanged("OpcCodPad");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string OpcicCod {
            get {
                return this.opcicCodField;
            }
            set {
                this.opcicCodField = value;
                this.RaisePropertyChanged("OpcicCod");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string OpcicNivPad {
            get {
                return this.opcicNivPadField;
            }
            set {
                this.opcicNivPadField = value;
                this.RaisePropertyChanged("OpcicNivPad");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string OpcicNiv {
            get {
                return this.opcicNivField;
            }
            set {
                this.opcicNivField = value;
                this.RaisePropertyChanged("OpcicNiv");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string OpcicDes {
            get {
                return this.opcicDesField;
            }
            set {
                this.opcicDesField = value;
                this.RaisePropertyChanged("OpcicDes");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public string OpcicAbrev {
            get {
                return this.opcicAbrevField;
            }
            set {
                this.opcicAbrevField = value;
                this.RaisePropertyChanged("OpcicAbrev");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=7)]
        public string OpcicNomPag {
            get {
                return this.opcicNomPagField;
            }
            set {
                this.opcicNomPagField = value;
                this.RaisePropertyChanged("OpcicNomPag");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=8)]
        public string OpcicNumOrd {
            get {
                return this.opcicNumOrdField;
            }
            set {
                this.opcicNumOrdField = value;
                this.RaisePropertyChanged("OpcicNumOrd");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=9)]
        public string OpcicD1 {
            get {
                return this.opcicD1Field;
            }
            set {
                this.opcicD1Field = value;
                this.RaisePropertyChanged("OpcicD1");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/")]
    public partial class seguridadType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string usuacCodField;
        
        private string perfcCodField;
        
        private string usuacCodVenSapField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string UsuacCod {
            get {
                return this.usuacCodField;
            }
            set {
                this.usuacCodField = value;
                this.RaisePropertyChanged("UsuacCod");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string PerfcCod {
            get {
                return this.perfcCodField;
            }
            set {
                this.perfcCodField = value;
                this.RaisePropertyChanged("PerfcCod");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string UsuacCodVenSap {
            get {
                return this.usuacCodVenSapField;
            }
            set {
                this.usuacCodVenSapField = value;
                this.RaisePropertyChanged("UsuacCodVenSap");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="leerOpcionesMenu", WrapperNamespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", IsWrapped=true)]
    public partial class leerOpcionesMenuRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string idTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ipAplicacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string aplicacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long usuario;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=4)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long appCod;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=5)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long nivel;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=6)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long opcPadre;
        
        public leerOpcionesMenuRequest() {
        }
        
        public leerOpcionesMenuRequest(string idTransaccion, string ipAplicacion, string aplicacion, long usuario, long appCod, long nivel, long opcPadre) {
            this.idTransaccion = idTransaccion;
            this.ipAplicacion = ipAplicacion;
            this.aplicacion = aplicacion;
            this.usuario = usuario;
            this.appCod = appCod;
            this.nivel = nivel;
            this.opcPadre = opcPadre;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="leerOpcionesMenuResponse", WrapperNamespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", IsWrapped=true)]
    public partial class leerOpcionesMenuResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string idTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string errorCode;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string errorMsg;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=3)]
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("menu", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.menuType[] cursor_Menu;
        
        public leerOpcionesMenuResponse() {
        }
        
        public leerOpcionesMenuResponse(string idTransaccion, string errorCode, string errorMsg, Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.menuType[] cursor_Menu) {
            this.idTransaccion = idTransaccion;
            this.errorCode = errorCode;
            this.errorMsg = errorMsg;
            this.cursor_Menu = cursor_Menu;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="verificaUsuario", WrapperNamespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", IsWrapped=true)]
    public partial class verificaUsuarioRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string idTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ipAplicacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string aplicacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string usuarioLogin;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=4)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long appCod;
        
        public verificaUsuarioRequest() {
        }
        
        public verificaUsuarioRequest(string idTransaccion, string ipAplicacion, string aplicacion, string usuarioLogin, long appCod) {
            this.idTransaccion = idTransaccion;
            this.ipAplicacion = ipAplicacion;
            this.aplicacion = aplicacion;
            this.usuarioLogin = usuarioLogin;
            this.appCod = appCod;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="verificaUsuarioResponse", WrapperNamespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", IsWrapped=true)]
    public partial class verificaUsuarioResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string idTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string errorCode;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string errorMsg;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/utilitiesservices/seguridad/consultaseguridad/", Order=3)]
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("seguridad", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.seguridadType[] cursorSeguridad;
        
        public verificaUsuarioResponse() {
        }
        
        public verificaUsuarioResponse(string idTransaccion, string errorCode, string errorMsg, Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.seguridadType[] cursorSeguridad) {
            this.idTransaccion = idTransaccion;
            this.errorCode = errorCode;
            this.errorMsg = errorMsg;
            this.cursorSeguridad = cursorSeguridad;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ConsultaSeguridadChannel : Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.ConsultaSeguridad, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ConsultaSeguridadClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.ConsultaSeguridad>, Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.ConsultaSeguridad {
        
        public ConsultaSeguridadClient() {
        }
        
        public ConsultaSeguridadClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ConsultaSeguridadClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConsultaSeguridadClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConsultaSeguridadClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.leerOpcionesMenuResponse Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.ConsultaSeguridad.leerOpcionesMenu(Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.leerOpcionesMenuRequest request) {
            return base.Channel.leerOpcionesMenu(request);
        }
        
        public string leerOpcionesMenu(ref string idTransaccion, string ipAplicacion, string aplicacion, long usuario, long appCod, long nivel, long opcPadre, out string errorMsg, out Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.menuType[] cursor_Menu) {
            Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.leerOpcionesMenuRequest inValue = new Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.leerOpcionesMenuRequest();
            inValue.idTransaccion = idTransaccion;
            inValue.ipAplicacion = ipAplicacion;
            inValue.aplicacion = aplicacion;
            inValue.usuario = usuario;
            inValue.appCod = appCod;
            inValue.nivel = nivel;
            inValue.opcPadre = opcPadre;
            Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.leerOpcionesMenuResponse retVal = ((Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.ConsultaSeguridad)(this)).leerOpcionesMenu(inValue);
            idTransaccion = retVal.idTransaccion;
            errorMsg = retVal.errorMsg;
            cursor_Menu = retVal.cursor_Menu;
            return retVal.errorCode;
        }
        
        public System.Threading.Tasks.Task<Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.leerOpcionesMenuResponse> leerOpcionesMenuAsync(Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.leerOpcionesMenuRequest request) {
            return base.Channel.leerOpcionesMenuAsync(request);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.verificaUsuarioResponse Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.ConsultaSeguridad.verificaUsuario(Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.verificaUsuarioRequest request) {
            return base.Channel.verificaUsuario(request);
        }
        
        public string verificaUsuario(ref string idTransaccion, string ipAplicacion, string aplicacion, string usuarioLogin, long appCod, out string errorMsg, out Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.seguridadType[] cursorSeguridad) {
            Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.verificaUsuarioRequest inValue = new Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.verificaUsuarioRequest();
            inValue.idTransaccion = idTransaccion;
            inValue.ipAplicacion = ipAplicacion;
            inValue.aplicacion = aplicacion;
            inValue.usuarioLogin = usuarioLogin;
            inValue.appCod = appCod;
            Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.verificaUsuarioResponse retVal = ((Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.ConsultaSeguridad)(this)).verificaUsuario(inValue);
            idTransaccion = retVal.idTransaccion;
            errorMsg = retVal.errorMsg;
            cursorSeguridad = retVal.cursorSeguridad;
            return retVal.errorCode;
        }
        
        public System.Threading.Tasks.Task<Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.verificaUsuarioResponse> verificaUsuarioAsync(Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad.verificaUsuarioRequest request) {
            return base.Channel.verificaUsuarioAsync(request);
        }
    }
}
