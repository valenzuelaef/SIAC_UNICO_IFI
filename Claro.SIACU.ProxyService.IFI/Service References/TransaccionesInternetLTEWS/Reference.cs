﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/transaccionesinternetltews", ConfigurationName="TransaccionesInternetLTEWS.TransaccionesInternetLTEWSPortType")]
    public interface TransaccionesInternetLTEWSPortType {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación desactivarContrato no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/postventa/transaccionesinternetltews/desactivarContrato", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.desactivarContratoResponse desactivarContrato(Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.desactivarContratoRequest request);
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/transaccionesinternetltews/types")]
    public partial class desactivarContratoRequestType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private auditRequestType auditRequestField;
        
        private string codigoClienteField;
        
        private string codigoCuentaField;
        
        private string codigoContratoField;
        
        private string codigoServicioField;
        
        private string msisdnField;
        
        private string reasonField;
        
        private string fechaProgramacionField;
        
        private string fechaProgramacionSOTField;
        
        private string franjaHorariaField;
        
        private string tipTraField;
        
        private string montoPenalidadField;
        
        private string tipoClienteField;
        
        private string areaPCSField;
        
        private string motivoPCSField;
        
        private string subMotivoPCSField;
        
        private string cicloFacturacionField;
        
        private string numeroDocumentoField;
        
        private string usuarioAsesorField;
        
        private string tipoServicioField;
        
        private string observacionesField;
        
        private string flagOccApadeceField;
        
        private string flagNdPcsField;
        
        private string cacDacField;
        
        private string montoPCSField;
        
        private string montoFidelizacionField;
        
        private string traceField;
        
        private string fechaActualField;
        
        private string codigoPlanoField;
        
        private string codigoMotivoField;
        
        private string mailUsuarioAplicacionField;
        
        private string codigoInteraccionField;
        
        private AttributeValuePair[] listaRequestOpcionalField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public auditRequestType auditRequest {
            get {
                return this.auditRequestField;
            }
            set {
                this.auditRequestField = value;
                this.RaisePropertyChanged("auditRequest");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string codigoCliente {
            get {
                return this.codigoClienteField;
            }
            set {
                this.codigoClienteField = value;
                this.RaisePropertyChanged("codigoCliente");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string codigoCuenta {
            get {
                return this.codigoCuentaField;
            }
            set {
                this.codigoCuentaField = value;
                this.RaisePropertyChanged("codigoCuenta");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string codigoContrato {
            get {
                return this.codigoContratoField;
            }
            set {
                this.codigoContratoField = value;
                this.RaisePropertyChanged("codigoContrato");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string codigoServicio {
            get {
                return this.codigoServicioField;
            }
            set {
                this.codigoServicioField = value;
                this.RaisePropertyChanged("codigoServicio");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string msisdn {
            get {
                return this.msisdnField;
            }
            set {
                this.msisdnField = value;
                this.RaisePropertyChanged("msisdn");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string reason {
            get {
                return this.reasonField;
            }
            set {
                this.reasonField = value;
                this.RaisePropertyChanged("reason");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string FechaProgramacion {
            get {
                return this.fechaProgramacionField;
            }
            set {
                this.fechaProgramacionField = value;
                this.RaisePropertyChanged("FechaProgramacion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string FechaProgramacionSOT {
            get {
                return this.fechaProgramacionSOTField;
            }
            set {
                this.fechaProgramacionSOTField = value;
                this.RaisePropertyChanged("FechaProgramacionSOT");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string franjaHoraria {
            get {
                return this.franjaHorariaField;
            }
            set {
                this.franjaHorariaField = value;
                this.RaisePropertyChanged("franjaHoraria");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string tipTra {
            get {
                return this.tipTraField;
            }
            set {
                this.tipTraField = value;
                this.RaisePropertyChanged("tipTra");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string montoPenalidad {
            get {
                return this.montoPenalidadField;
            }
            set {
                this.montoPenalidadField = value;
                this.RaisePropertyChanged("montoPenalidad");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public string tipoCliente {
            get {
                return this.tipoClienteField;
            }
            set {
                this.tipoClienteField = value;
                this.RaisePropertyChanged("tipoCliente");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=13)]
        public string areaPCS {
            get {
                return this.areaPCSField;
            }
            set {
                this.areaPCSField = value;
                this.RaisePropertyChanged("areaPCS");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=14)]
        public string motivoPCS {
            get {
                return this.motivoPCSField;
            }
            set {
                this.motivoPCSField = value;
                this.RaisePropertyChanged("motivoPCS");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=15)]
        public string subMotivoPCS {
            get {
                return this.subMotivoPCSField;
            }
            set {
                this.subMotivoPCSField = value;
                this.RaisePropertyChanged("subMotivoPCS");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=16)]
        public string cicloFacturacion {
            get {
                return this.cicloFacturacionField;
            }
            set {
                this.cicloFacturacionField = value;
                this.RaisePropertyChanged("cicloFacturacion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=17)]
        public string numeroDocumento {
            get {
                return this.numeroDocumentoField;
            }
            set {
                this.numeroDocumentoField = value;
                this.RaisePropertyChanged("numeroDocumento");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=18)]
        public string usuarioAsesor {
            get {
                return this.usuarioAsesorField;
            }
            set {
                this.usuarioAsesorField = value;
                this.RaisePropertyChanged("usuarioAsesor");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=19)]
        public string tipoServicio {
            get {
                return this.tipoServicioField;
            }
            set {
                this.tipoServicioField = value;
                this.RaisePropertyChanged("tipoServicio");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=20)]
        public string observaciones {
            get {
                return this.observacionesField;
            }
            set {
                this.observacionesField = value;
                this.RaisePropertyChanged("observaciones");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=21)]
        public string flagOccApadece {
            get {
                return this.flagOccApadeceField;
            }
            set {
                this.flagOccApadeceField = value;
                this.RaisePropertyChanged("flagOccApadece");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=22)]
        public string flagNdPcs {
            get {
                return this.flagNdPcsField;
            }
            set {
                this.flagNdPcsField = value;
                this.RaisePropertyChanged("flagNdPcs");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=23)]
        public string cacDac {
            get {
                return this.cacDacField;
            }
            set {
                this.cacDacField = value;
                this.RaisePropertyChanged("cacDac");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=24)]
        public string montoPCS {
            get {
                return this.montoPCSField;
            }
            set {
                this.montoPCSField = value;
                this.RaisePropertyChanged("montoPCS");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=25)]
        public string montoFidelizacion {
            get {
                return this.montoFidelizacionField;
            }
            set {
                this.montoFidelizacionField = value;
                this.RaisePropertyChanged("montoFidelizacion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=26)]
        public string trace {
            get {
                return this.traceField;
            }
            set {
                this.traceField = value;
                this.RaisePropertyChanged("trace");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=27)]
        public string fechaActual {
            get {
                return this.fechaActualField;
            }
            set {
                this.fechaActualField = value;
                this.RaisePropertyChanged("fechaActual");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=28)]
        public string codigoPlano {
            get {
                return this.codigoPlanoField;
            }
            set {
                this.codigoPlanoField = value;
                this.RaisePropertyChanged("codigoPlano");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=29)]
        public string codigoMotivo {
            get {
                return this.codigoMotivoField;
            }
            set {
                this.codigoMotivoField = value;
                this.RaisePropertyChanged("codigoMotivo");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=30)]
        public string mailUsuarioAplicacion {
            get {
                return this.mailUsuarioAplicacionField;
            }
            set {
                this.mailUsuarioAplicacionField = value;
                this.RaisePropertyChanged("mailUsuarioAplicacion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=31)]
        public string codigoInteraccion {
            get {
                return this.codigoInteraccionField;
            }
            set {
                this.codigoInteraccionField = value;
                this.RaisePropertyChanged("codigoInteraccion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=32)]
        [System.Xml.Serialization.XmlArrayItemAttribute("objetoRequestOpcional", Namespace="http://claro.com.pe/eai/ws/baseschema", IsNullable=false)]
        public AttributeValuePair[] listaRequestOpcional {
            get {
                return this.listaRequestOpcionalField;
            }
            set {
                this.listaRequestOpcionalField = value;
                this.RaisePropertyChanged("listaRequestOpcional");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class auditRequestType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string ipAplicacionField;
        
        private string nombreAplicacionField;
        
        private string usuarioAplicacionField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
                this.RaisePropertyChanged("idTransaccion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ipAplicacion {
            get {
                return this.ipAplicacionField;
            }
            set {
                this.ipAplicacionField = value;
                this.RaisePropertyChanged("ipAplicacion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string nombreAplicacion {
            get {
                return this.nombreAplicacionField;
            }
            set {
                this.nombreAplicacionField = value;
                this.RaisePropertyChanged("nombreAplicacion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string usuarioAplicacion {
            get {
                return this.usuarioAplicacionField;
            }
            set {
                this.usuarioAplicacionField = value;
                this.RaisePropertyChanged("usuarioAplicacion");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class auditResponseType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string codigoRespuestaField;
        
        private string mensajeRespuestaField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
                this.RaisePropertyChanged("idTransaccion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string codigoRespuesta {
            get {
                return this.codigoRespuestaField;
            }
            set {
                this.codigoRespuestaField = value;
                this.RaisePropertyChanged("codigoRespuesta");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string mensajeRespuesta {
            get {
                return this.mensajeRespuestaField;
            }
            set {
                this.mensajeRespuestaField = value;
                this.RaisePropertyChanged("mensajeRespuesta");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/transaccionesinternetltews/types")]
    public partial class ResponseData : object, System.ComponentModel.INotifyPropertyChanged {
        
        private auditResponseType auditResponseField;
        
        private AttributeValuePair[][] listaAdicionalResponseField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public auditResponseType auditResponse {
            get {
                return this.auditResponseField;
            }
            set {
                this.auditResponseField = value;
                this.RaisePropertyChanged("auditResponse");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("objetoResponseOpcional", typeof(AttributeValuePair), Namespace="http://claro.com.pe/eai/ws/baseschema", IsNullable=false)]
        public AttributeValuePair[][] listaAdicionalResponse {
            get {
                return this.listaAdicionalResponseField;
            }
            set {
                this.listaAdicionalResponseField = value;
                this.RaisePropertyChanged("listaAdicionalResponse");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class AttributeValuePair : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string nameField;
        
        private string valueField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
                this.RaisePropertyChanged("value");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class ResponseStatus : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int statusField;
        
        private string codeResponseField;
        
        private string descriptionResponseField;
        
        private string errorLocationField;
        
        private System.DateTime dateField;
        
        private string originField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
                this.RaisePropertyChanged("status");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string codeResponse {
            get {
                return this.codeResponseField;
            }
            set {
                this.codeResponseField = value;
                this.RaisePropertyChanged("codeResponse");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string descriptionResponse {
            get {
                return this.descriptionResponseField;
            }
            set {
                this.descriptionResponseField = value;
                this.RaisePropertyChanged("descriptionResponse");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string errorLocation {
            get {
                return this.errorLocationField;
            }
            set {
                this.errorLocationField = value;
                this.RaisePropertyChanged("errorLocation");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public System.DateTime date {
            get {
                return this.dateField;
            }
            set {
                this.dateField = value;
                this.RaisePropertyChanged("date");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string origin {
            get {
                return this.originField;
            }
            set {
                this.originField = value;
                this.RaisePropertyChanged("origin");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/transaccionesinternetltews/types")]
    public partial class desactivarContratoResponseType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private ResponseStatus responseStatusField;
        
        private ResponseData responseDataField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public ResponseStatus responseStatus {
            get {
                return this.responseStatusField;
            }
            set {
                this.responseStatusField = value;
                this.RaisePropertyChanged("responseStatus");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public ResponseData responseData {
            get {
                return this.responseDataField;
            }
            set {
                this.responseDataField = value;
                this.RaisePropertyChanged("responseData");
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
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class desactivarContratoRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="desactivarContratoRequest", Namespace="http://claro.com.pe/eai/ws/postventa/transaccionesinternetltews/types", Order=0)]
        public Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.desactivarContratoRequestType desactivarContratoRequest1;
        
        public desactivarContratoRequest() {
        }
        
        public desactivarContratoRequest(Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.desactivarContratoRequestType desactivarContratoRequest1) {
            this.desactivarContratoRequest1 = desactivarContratoRequest1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class desactivarContratoResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="desactivarContratoResponse", Namespace="http://claro.com.pe/eai/ws/postventa/transaccionesinternetltews/types", Order=0)]
        public Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.desactivarContratoResponseType desactivarContratoResponse1;
        
        public desactivarContratoResponse() {
        }
        
        public desactivarContratoResponse(Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.desactivarContratoResponseType desactivarContratoResponse1) {
            this.desactivarContratoResponse1 = desactivarContratoResponse1;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface TransaccionesInternetLTEWSPortTypeChannel : Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.TransaccionesInternetLTEWSPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TransaccionesInternetLTEWSPortTypeClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.TransaccionesInternetLTEWSPortType>, Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.TransaccionesInternetLTEWSPortType {
        
        public TransaccionesInternetLTEWSPortTypeClient() {
        }
        
        public TransaccionesInternetLTEWSPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TransaccionesInternetLTEWSPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransaccionesInternetLTEWSPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransaccionesInternetLTEWSPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.desactivarContratoResponse Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.TransaccionesInternetLTEWSPortType.desactivarContrato(Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.desactivarContratoRequest request) {
            return base.Channel.desactivarContrato(request);
        }
        
        public Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.desactivarContratoResponseType desactivarContrato(Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.desactivarContratoRequestType desactivarContratoRequest1) {
            Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.desactivarContratoRequest inValue = new Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.desactivarContratoRequest();
            inValue.desactivarContratoRequest1 = desactivarContratoRequest1;
            Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.desactivarContratoResponse retVal = ((Claro.SIACU.ProxyService.IFI.TransaccionesInternetLTEWS.TransaccionesInternetLTEWSPortType)(this)).desactivarContrato(inValue);
            return retVal.desactivarContratoResponse1;
        }
    }
}
