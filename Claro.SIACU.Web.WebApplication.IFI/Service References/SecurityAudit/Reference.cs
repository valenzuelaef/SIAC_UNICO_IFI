﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.Web.WebApplication.IFI.SecurityAudit {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Request", Namespace="Claro")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Claro.SIACU.Web.WebApplication.IFI.SecurityAudit.RegisterRequest))]
    public partial class Request : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private Claro.SIACU.Web.WebApplication.IFI.SecurityAudit.AuditRequest auditField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Claro.SIACU.Web.WebApplication.IFI.SecurityAudit.Audit AuditServiceField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public Claro.SIACU.Web.WebApplication.IFI.SecurityAudit.AuditRequest audit {
            get {
                return this.auditField;
            }
            set {
                if ((object.ReferenceEquals(this.auditField, value) != true)) {
                    this.auditField = value;
                    this.RaisePropertyChanged("audit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public Claro.SIACU.Web.WebApplication.IFI.SecurityAudit.Audit AuditService {
            get {
                return this.AuditServiceField;
            }
            set {
                if ((object.ReferenceEquals(this.AuditServiceField, value) != true)) {
                    this.AuditServiceField = value;
                    this.RaisePropertyChanged("AuditService");
                }
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AuditRequest", Namespace="Claro")]
    [System.SerializableAttribute()]
    public partial class AuditRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string transactionField;
        
        private string ipAddressField;
        
        private string applicationNameField;
        
        private string userNameField;
        
        private string SessionField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string transaction {
            get {
                return this.transactionField;
            }
            set {
                if ((object.ReferenceEquals(this.transactionField, value) != true)) {
                    this.transactionField = value;
                    this.RaisePropertyChanged("transaction");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public string ipAddress {
            get {
                return this.ipAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.ipAddressField, value) != true)) {
                    this.ipAddressField = value;
                    this.RaisePropertyChanged("ipAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public string applicationName {
            get {
                return this.applicationNameField;
            }
            set {
                if ((object.ReferenceEquals(this.applicationNameField, value) != true)) {
                    this.applicationNameField = value;
                    this.RaisePropertyChanged("applicationName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public string userName {
            get {
                return this.userNameField;
            }
            set {
                if ((object.ReferenceEquals(this.userNameField, value) != true)) {
                    this.userNameField = value;
                    this.RaisePropertyChanged("userName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public string Session {
            get {
                return this.SessionField;
            }
            set {
                if ((object.ReferenceEquals(this.SessionField, value) != true)) {
                    this.SessionField = value;
                    this.RaisePropertyChanged("Session");
                }
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Audit", Namespace="AuditService")]
    [System.SerializableAttribute()]
    public partial class Audit : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string TrackCodeField;
        
        private string ApplicationCodeField;
        
        private string IpClientField;
        
        private string ClientNameField;
        
        private string ServerNameField;
        
        private string SearchValueField;
        
        private string MessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string TrackCode {
            get {
                return this.TrackCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.TrackCodeField, value) != true)) {
                    this.TrackCodeField = value;
                    this.RaisePropertyChanged("TrackCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public string ApplicationCode {
            get {
                return this.ApplicationCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.ApplicationCodeField, value) != true)) {
                    this.ApplicationCodeField = value;
                    this.RaisePropertyChanged("ApplicationCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public string IpClient {
            get {
                return this.IpClientField;
            }
            set {
                if ((object.ReferenceEquals(this.IpClientField, value) != true)) {
                    this.IpClientField = value;
                    this.RaisePropertyChanged("IpClient");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public string ClientName {
            get {
                return this.ClientNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ClientNameField, value) != true)) {
                    this.ClientNameField = value;
                    this.RaisePropertyChanged("ClientName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public string ServerName {
            get {
                return this.ServerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ServerNameField, value) != true)) {
                    this.ServerNameField = value;
                    this.RaisePropertyChanged("ServerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=5)]
        public string SearchValue {
            get {
                return this.SearchValueField;
            }
            set {
                if ((object.ReferenceEquals(this.SearchValueField, value) != true)) {
                    this.SearchValueField = value;
                    this.RaisePropertyChanged("SearchValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=6)]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RegisterRequest", Namespace="Claro.Audit.Register")]
    [System.SerializableAttribute()]
    public partial class RegisterRequest : Claro.SIACU.Web.WebApplication.IFI.SecurityAudit.Request {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string userNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string clientIPField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string serverIPField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string amountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string clientNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string serverNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string serviceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string phoneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string textField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string transactionField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string userName {
            get {
                return this.userNameField;
            }
            set {
                if ((object.ReferenceEquals(this.userNameField, value) != true)) {
                    this.userNameField = value;
                    this.RaisePropertyChanged("userName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public string clientIP {
            get {
                return this.clientIPField;
            }
            set {
                if ((object.ReferenceEquals(this.clientIPField, value) != true)) {
                    this.clientIPField = value;
                    this.RaisePropertyChanged("clientIP");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public string serverIP {
            get {
                return this.serverIPField;
            }
            set {
                if ((object.ReferenceEquals(this.serverIPField, value) != true)) {
                    this.serverIPField = value;
                    this.RaisePropertyChanged("serverIP");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public string amount {
            get {
                return this.amountField;
            }
            set {
                if ((object.ReferenceEquals(this.amountField, value) != true)) {
                    this.amountField = value;
                    this.RaisePropertyChanged("amount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public string clientName {
            get {
                return this.clientNameField;
            }
            set {
                if ((object.ReferenceEquals(this.clientNameField, value) != true)) {
                    this.clientNameField = value;
                    this.RaisePropertyChanged("clientName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public string serverName {
            get {
                return this.serverNameField;
            }
            set {
                if ((object.ReferenceEquals(this.serverNameField, value) != true)) {
                    this.serverNameField = value;
                    this.RaisePropertyChanged("serverName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public string service {
            get {
                return this.serviceField;
            }
            set {
                if ((object.ReferenceEquals(this.serviceField, value) != true)) {
                    this.serviceField = value;
                    this.RaisePropertyChanged("service");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public string phone {
            get {
                return this.phoneField;
            }
            set {
                if ((object.ReferenceEquals(this.phoneField, value) != true)) {
                    this.phoneField = value;
                    this.RaisePropertyChanged("phone");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
        public string text {
            get {
                return this.textField;
            }
            set {
                if ((object.ReferenceEquals(this.textField, value) != true)) {
                    this.textField = value;
                    this.RaisePropertyChanged("text");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=9)]
        public string transaction {
            get {
                return this.transactionField;
            }
            set {
                if ((object.ReferenceEquals(this.transactionField, value) != true)) {
                    this.transactionField = value;
                    this.RaisePropertyChanged("transaction");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SecurityAudit.IAudit")]
    public interface IAudit {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAudit/Register", ReplyAction="http://tempuri.org/IAudit/RegisterResponse")]
        void Register(Claro.SIACU.Web.WebApplication.IFI.SecurityAudit.RegisterRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuditChannel : Claro.SIACU.Web.WebApplication.IFI.SecurityAudit.IAudit, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuditClient : System.ServiceModel.ClientBase<Claro.SIACU.Web.WebApplication.IFI.SecurityAudit.IAudit>, Claro.SIACU.Web.WebApplication.IFI.SecurityAudit.IAudit {
        
        public AuditClient() {
        }
        
        public AuditClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuditClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuditClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuditClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Register(Claro.SIACU.Web.WebApplication.IFI.SecurityAudit.RegisterRequest request) {
            base.Channel.Register(request);
        }
    }
}