﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.IFI.SIACU.BillingService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AuditType", Namespace="http://claro.com.pe/eai/servicecommons/")]
    [System.SerializableAttribute()]
    public partial class AuditType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string txIdField;
        
        private string errorCodeField;
        
        private string errorMsgField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false)]
        public string txId {
            get {
                return this.txIdField;
            }
            set {
                if ((object.ReferenceEquals(this.txIdField, value) != true)) {
                    this.txIdField = value;
                    this.RaisePropertyChanged("txId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=1)]
        public string errorCode {
            get {
                return this.errorCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.errorCodeField, value) != true)) {
                    this.errorCodeField = value;
                    this.RaisePropertyChanged("errorCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=2)]
        public string errorMsg {
            get {
                return this.errorMsgField;
            }
            set {
                if ((object.ReferenceEquals(this.errorMsgField, value) != true)) {
                    this.errorMsgField = value;
                    this.RaisePropertyChanged("errorMsg");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", ConfigurationName="SIACU.BillingService.TransaccionOCCPort")]
    public interface TransaccionOCCPort {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de contenedor (generaOCCReq) del mensaje GeneraOCCRequest no coincide con el valor predeterminado (GeneraOCC)
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/GeneraOCC", ReplyAction="*")]
        Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCResponse GeneraOCC(Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de contenedor (anularOCCReq) del mensaje AnularOCCRequest no coincide con el valor predeterminado (AnularOCC)
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/AnularOCC", ReplyAction="*")]
        Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AnularOCCResponse AnularOCC(Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AnularOCCRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de contenedor (GeneraOCCDetalleRequest) del mensaje GeneraOCCDetalleRequest no coincide con el valor predeterminado (GeneraOCCDetalle)
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/GeneraOCCDetalle", ReplyAction="*")]
        Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCDetalleResponse GeneraOCCDetalle(Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCDetalleRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de contenedor (EliminarOCCRequest) del mensaje EliminarOCCRequest no coincide con el valor predeterminado (EliminarOCC)
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/EliminarOCC", ReplyAction="*")]
        Claro.SIACU.ProxyService.IFI.SIACU.BillingService.EliminarOCCResponse EliminarOCC(Claro.SIACU.ProxyService.IFI.SIACU.BillingService.EliminarOCCRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="generaOCCReq", WrapperNamespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", IsWrapped=true)]
    public partial class GeneraOCCRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=0)]
        public string txId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=1)]
        public string ipApp;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=2)]
        public string usrApp;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=3)]
        public decimal customerId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=4)]
        public decimal codigoOcc;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=5)]
        public decimal nroCuotas;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=6)]
        public float montoOcc;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=7)]
        public System.DateTime recDate;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=8)]
        public string remark;
        
        public GeneraOCCRequest() {
        }
        
        public GeneraOCCRequest(string txId, string ipApp, string usrApp, decimal customerId, decimal codigoOcc, decimal nroCuotas, float montoOcc, System.DateTime recDate, string remark) {
            this.txId = txId;
            this.ipApp = ipApp;
            this.usrApp = usrApp;
            this.customerId = customerId;
            this.codigoOcc = codigoOcc;
            this.nroCuotas = nroCuotas;
            this.montoOcc = montoOcc;
            this.recDate = recDate;
            this.remark = remark;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="generaOCCResponse", WrapperNamespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", IsWrapped=true)]
    public partial class GeneraOCCResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=0)]
        public Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AuditType audit;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=1)]
        public decimal registraOcc;
        
        public GeneraOCCResponse() {
        }
        
        public GeneraOCCResponse(Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AuditType audit, decimal registraOcc) {
            this.audit = audit;
            this.registraOcc = registraOcc;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="anularOCCReq", WrapperNamespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", IsWrapped=true)]
    public partial class AnularOCCRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=0)]
        public string txId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=1)]
        public string ipApp;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=2)]
        public string usrApp;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=3)]
        public string cuenta;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=4)]
        public string numOcc;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=5)]
        public string motivo;
        
        public AnularOCCRequest() {
        }
        
        public AnularOCCRequest(string txId, string ipApp, string usrApp, string cuenta, string numOcc, string motivo) {
            this.txId = txId;
            this.ipApp = ipApp;
            this.usrApp = usrApp;
            this.cuenta = cuenta;
            this.numOcc = numOcc;
            this.motivo = motivo;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="anularOCCResponse", WrapperNamespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", IsWrapped=true)]
    public partial class AnularOCCResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=0)]
        public Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AuditType audit;
        
        public AnularOCCResponse() {
        }
        
        public AnularOCCResponse(Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AuditType audit) {
            this.audit = audit;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GeneraOCCDetalleRequest", WrapperNamespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", IsWrapped=true)]
    public partial class GeneraOCCDetalleRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=0)]
        public string txId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=1)]
        public string ipApp;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=2)]
        public string usrApp;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=3)]
        public decimal customerId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=4)]
        public decimal codigoOcc;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=5)]
        public decimal nroCuotas;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=6)]
        public float montoOcc;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=7)]
        public System.DateTime recDate;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=8)]
        public string remark;
        
        public GeneraOCCDetalleRequest() {
        }
        
        public GeneraOCCDetalleRequest(string txId, string ipApp, string usrApp, decimal customerId, decimal codigoOcc, decimal nroCuotas, float montoOcc, System.DateTime recDate, string remark) {
            this.txId = txId;
            this.ipApp = ipApp;
            this.usrApp = usrApp;
            this.customerId = customerId;
            this.codigoOcc = codigoOcc;
            this.nroCuotas = nroCuotas;
            this.montoOcc = montoOcc;
            this.recDate = recDate;
            this.remark = remark;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GeneraOCCDetalleResponse", WrapperNamespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", IsWrapped=true)]
    public partial class GeneraOCCDetalleResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=0)]
        public Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AuditType audit;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=1)]
        public string numeroOcc;
        
        public GeneraOCCDetalleResponse() {
        }
        
        public GeneraOCCDetalleResponse(Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AuditType audit, string numeroOcc) {
            this.audit = audit;
            this.numeroOcc = numeroOcc;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="EliminarOCCRequest", WrapperNamespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", IsWrapped=true)]
    public partial class EliminarOCCRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=0)]
        public string txId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=1)]
        public string ipApp;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=2)]
        public string usrApp;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=3)]
        public decimal customerId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=4)]
        public decimal numSeq;
        
        public EliminarOCCRequest() {
        }
        
        public EliminarOCCRequest(string txId, string ipApp, string usrApp, decimal customerId, decimal numSeq) {
            this.txId = txId;
            this.ipApp = ipApp;
            this.usrApp = usrApp;
            this.customerId = customerId;
            this.numSeq = numSeq;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="EliminarOCCResponse", WrapperNamespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", IsWrapped=true)]
    public partial class EliminarOCCResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/billingservices/bscs/transaccionocc/", Order=0)]
        public Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AuditType audit;
        
        public EliminarOCCResponse() {
        }
        
        public EliminarOCCResponse(Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AuditType audit) {
            this.audit = audit;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface TransaccionOCCPortChannel : Claro.SIACU.ProxyService.IFI.SIACU.BillingService.TransaccionOCCPort, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TransaccionOCCPortClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.IFI.SIACU.BillingService.TransaccionOCCPort>, Claro.SIACU.ProxyService.IFI.SIACU.BillingService.TransaccionOCCPort {
        
        public TransaccionOCCPortClient() {
        }
        
        public TransaccionOCCPortClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TransaccionOCCPortClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransaccionOCCPortClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransaccionOCCPortClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCResponse Claro.SIACU.ProxyService.IFI.SIACU.BillingService.TransaccionOCCPort.GeneraOCC(Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCRequest request) {
            return base.Channel.GeneraOCC(request);
        }
        
        public Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AuditType GeneraOCC(string txId, string ipApp, string usrApp, decimal customerId, decimal codigoOcc, decimal nroCuotas, float montoOcc, System.DateTime recDate, string remark, out decimal registraOcc) {
            Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCRequest inValue = new Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCRequest();
            inValue.txId = txId;
            inValue.ipApp = ipApp;
            inValue.usrApp = usrApp;
            inValue.customerId = customerId;
            inValue.codigoOcc = codigoOcc;
            inValue.nroCuotas = nroCuotas;
            inValue.montoOcc = montoOcc;
            inValue.recDate = recDate;
            inValue.remark = remark;
            Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCResponse retVal = ((Claro.SIACU.ProxyService.IFI.SIACU.BillingService.TransaccionOCCPort)(this)).GeneraOCC(inValue);
            registraOcc = retVal.registraOcc;
            return retVal.audit;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AnularOCCResponse Claro.SIACU.ProxyService.IFI.SIACU.BillingService.TransaccionOCCPort.AnularOCC(Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AnularOCCRequest request) {
            return base.Channel.AnularOCC(request);
        }
        
        public Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AuditType AnularOCC(string txId, string ipApp, string usrApp, string cuenta, string numOcc, string motivo) {
            Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AnularOCCRequest inValue = new Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AnularOCCRequest();
            inValue.txId = txId;
            inValue.ipApp = ipApp;
            inValue.usrApp = usrApp;
            inValue.cuenta = cuenta;
            inValue.numOcc = numOcc;
            inValue.motivo = motivo;
            Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AnularOCCResponse retVal = ((Claro.SIACU.ProxyService.IFI.SIACU.BillingService.TransaccionOCCPort)(this)).AnularOCC(inValue);
            return retVal.audit;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCDetalleResponse Claro.SIACU.ProxyService.IFI.SIACU.BillingService.TransaccionOCCPort.GeneraOCCDetalle(Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCDetalleRequest request) {
            return base.Channel.GeneraOCCDetalle(request);
        }
        
        public Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AuditType GeneraOCCDetalle(string txId, string ipApp, string usrApp, decimal customerId, decimal codigoOcc, decimal nroCuotas, float montoOcc, System.DateTime recDate, string remark, out string numeroOcc) {
            Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCDetalleRequest inValue = new Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCDetalleRequest();
            inValue.txId = txId;
            inValue.ipApp = ipApp;
            inValue.usrApp = usrApp;
            inValue.customerId = customerId;
            inValue.codigoOcc = codigoOcc;
            inValue.nroCuotas = nroCuotas;
            inValue.montoOcc = montoOcc;
            inValue.recDate = recDate;
            inValue.remark = remark;
            Claro.SIACU.ProxyService.IFI.SIACU.BillingService.GeneraOCCDetalleResponse retVal = ((Claro.SIACU.ProxyService.IFI.SIACU.BillingService.TransaccionOCCPort)(this)).GeneraOCCDetalle(inValue);
            numeroOcc = retVal.numeroOcc;
            return retVal.audit;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.IFI.SIACU.BillingService.EliminarOCCResponse Claro.SIACU.ProxyService.IFI.SIACU.BillingService.TransaccionOCCPort.EliminarOCC(Claro.SIACU.ProxyService.IFI.SIACU.BillingService.EliminarOCCRequest request) {
            return base.Channel.EliminarOCC(request);
        }
        
        public Claro.SIACU.ProxyService.IFI.SIACU.BillingService.AuditType EliminarOCC(string txId, string ipApp, string usrApp, decimal customerId, decimal numSeq) {
            Claro.SIACU.ProxyService.IFI.SIACU.BillingService.EliminarOCCRequest inValue = new Claro.SIACU.ProxyService.IFI.SIACU.BillingService.EliminarOCCRequest();
            inValue.txId = txId;
            inValue.ipApp = ipApp;
            inValue.usrApp = usrApp;
            inValue.customerId = customerId;
            inValue.numSeq = numSeq;
            Claro.SIACU.ProxyService.IFI.SIACU.BillingService.EliminarOCCResponse retVal = ((Claro.SIACU.ProxyService.IFI.SIACU.BillingService.TransaccionOCCPort)(this)).EliminarOCC(inValue);
            return retVal.audit;
        }
    }
}
