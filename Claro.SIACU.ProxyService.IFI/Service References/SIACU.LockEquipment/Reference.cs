﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BloqueoDesbloqueoIMEIRequest", Namespace="http://pe/com/claro/esb/services/hlr/bloqueodesbloqueoimei/schemas/BloqueoDesbloq" +
        "ueoIMEI.xsd")]
    [System.SerializableAttribute()]
    public partial class BloqueoDesbloqueoIMEIRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string IMEIField;
        
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
        public string IMEI {
            get {
                return this.IMEIField;
            }
            set {
                if ((object.ReferenceEquals(this.IMEIField, value) != true)) {
                    this.IMEIField = value;
                    this.RaisePropertyChanged("IMEI");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="BloqueoDesbloqueoIMEIResponse", Namespace="http://pe/com/claro/esb/services/hlr/bloqueodesbloqueoimei/schemas/BloqueoDesbloq" +
        "ueoIMEI.xsd")]
    [System.SerializableAttribute()]
    public partial class BloqueoDesbloqueoIMEIResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string transaccionField;
        
        private string resultadoField;
        
        private string mensajeField;
        
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
        public string transaccion {
            get {
                return this.transaccionField;
            }
            set {
                if ((object.ReferenceEquals(this.transaccionField, value) != true)) {
                    this.transaccionField = value;
                    this.RaisePropertyChanged("transaccion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=1)]
        public string resultado {
            get {
                return this.resultadoField;
            }
            set {
                if ((object.ReferenceEquals(this.resultadoField, value) != true)) {
                    this.resultadoField = value;
                    this.RaisePropertyChanged("resultado");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false, Order=2)]
        public string mensaje {
            get {
                return this.mensajeField;
            }
            set {
                if ((object.ReferenceEquals(this.mensajeField, value) != true)) {
                    this.mensajeField = value;
                    this.RaisePropertyChanged("mensaje");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://pe/com/claro/esb/services/hlr/bloqueodesbloqueoimei/ws", ConfigurationName="SIACU.LockEquipment.BloqueoDesbloqueoIMEIWS")]
    public interface BloqueoDesbloqueoIMEIWS {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento BloqueoDesbloqueoIMEIRequest del espacio de nombres http://pe/com/claro/esb/services/hlr/bloqueodesbloqueoimei/schemas/BloqueoDesbloqueoIMEI.xsd no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.bloqueoIMEIResponse bloqueoIMEI(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.bloqueoIMEIRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento BloqueoDesbloqueoIMEIRequest del espacio de nombres http://pe/com/claro/esb/services/hlr/bloqueodesbloqueoimei/schemas/BloqueoDesbloqueoIMEI.xsd no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.desbloqueoIMEIResponse desbloqueoIMEI(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.desbloqueoIMEIRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bloqueoIMEIRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bloqueoIMEI", Namespace="http://pe/com/claro/esb/services/hlr/bloqueodesbloqueoimei/ws", Order=0)]
        public Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.bloqueoIMEIRequestBody Body;
        
        public bloqueoIMEIRequest() {
        }
        
        public bloqueoIMEIRequest(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.bloqueoIMEIRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://pe/com/claro/esb/services/hlr/bloqueodesbloqueoimei/schemas/BloqueoDesbloq" +
        "ueoIMEI.xsd")]
    public partial class bloqueoIMEIRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIRequest BloqueoDesbloqueoIMEIRequest;
        
        public bloqueoIMEIRequestBody() {
        }
        
        public bloqueoIMEIRequestBody(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIRequest BloqueoDesbloqueoIMEIRequest) {
            this.BloqueoDesbloqueoIMEIRequest = BloqueoDesbloqueoIMEIRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bloqueoIMEIResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bloqueoIMEIResponse", Namespace="http://pe/com/claro/esb/services/hlr/bloqueodesbloqueoimei/ws", Order=0)]
        public Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.bloqueoIMEIResponseBody Body;
        
        public bloqueoIMEIResponse() {
        }
        
        public bloqueoIMEIResponse(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.bloqueoIMEIResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://pe/com/claro/esb/services/hlr/bloqueodesbloqueoimei/schemas/BloqueoDesbloq" +
        "ueoIMEI.xsd")]
    public partial class bloqueoIMEIResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIResponse BloqueoDesbloqueoIMEIResponse;
        
        public bloqueoIMEIResponseBody() {
        }
        
        public bloqueoIMEIResponseBody(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIResponse BloqueoDesbloqueoIMEIResponse) {
            this.BloqueoDesbloqueoIMEIResponse = BloqueoDesbloqueoIMEIResponse;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class desbloqueoIMEIRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="desbloqueoIMEI", Namespace="http://pe/com/claro/esb/services/hlr/bloqueodesbloqueoimei/ws", Order=0)]
        public Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.desbloqueoIMEIRequestBody Body;
        
        public desbloqueoIMEIRequest() {
        }
        
        public desbloqueoIMEIRequest(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.desbloqueoIMEIRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://pe/com/claro/esb/services/hlr/bloqueodesbloqueoimei/schemas/BloqueoDesbloq" +
        "ueoIMEI.xsd")]
    public partial class desbloqueoIMEIRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIRequest BloqueoDesbloqueoIMEIRequest;
        
        public desbloqueoIMEIRequestBody() {
        }
        
        public desbloqueoIMEIRequestBody(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIRequest BloqueoDesbloqueoIMEIRequest) {
            this.BloqueoDesbloqueoIMEIRequest = BloqueoDesbloqueoIMEIRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class desbloqueoIMEIResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="desbloqueoIMEIResponse", Namespace="http://pe/com/claro/esb/services/hlr/bloqueodesbloqueoimei/ws", Order=0)]
        public Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.desbloqueoIMEIResponseBody Body;
        
        public desbloqueoIMEIResponse() {
        }
        
        public desbloqueoIMEIResponse(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.desbloqueoIMEIResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://pe/com/claro/esb/services/hlr/bloqueodesbloqueoimei/schemas/BloqueoDesbloq" +
        "ueoIMEI.xsd")]
    public partial class desbloqueoIMEIResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIResponse BloqueoDesbloqueoIMEIResponse;
        
        public desbloqueoIMEIResponseBody() {
        }
        
        public desbloqueoIMEIResponseBody(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIResponse BloqueoDesbloqueoIMEIResponse) {
            this.BloqueoDesbloqueoIMEIResponse = BloqueoDesbloqueoIMEIResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface BloqueoDesbloqueoIMEIWSChannel : Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIWS, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BloqueoDesbloqueoIMEIWSClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIWS>, Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIWS {
        
        public BloqueoDesbloqueoIMEIWSClient() {
        }
        
        public BloqueoDesbloqueoIMEIWSClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BloqueoDesbloqueoIMEIWSClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BloqueoDesbloqueoIMEIWSClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BloqueoDesbloqueoIMEIWSClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.bloqueoIMEIResponse Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIWS.bloqueoIMEI(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.bloqueoIMEIRequest request) {
            return base.Channel.bloqueoIMEI(request);
        }
        
        public Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIResponse bloqueoIMEI(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIRequest BloqueoDesbloqueoIMEIRequest) {
            Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.bloqueoIMEIRequest inValue = new Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.bloqueoIMEIRequest();
            inValue.Body = new Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.bloqueoIMEIRequestBody();
            inValue.Body.BloqueoDesbloqueoIMEIRequest = BloqueoDesbloqueoIMEIRequest;
            Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.bloqueoIMEIResponse retVal = ((Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIWS)(this)).bloqueoIMEI(inValue);
            return retVal.Body.BloqueoDesbloqueoIMEIResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.desbloqueoIMEIResponse Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIWS.desbloqueoIMEI(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.desbloqueoIMEIRequest request) {
            return base.Channel.desbloqueoIMEI(request);
        }
        
        public Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIResponse desbloqueoIMEI(Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIRequest BloqueoDesbloqueoIMEIRequest) {
            Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.desbloqueoIMEIRequest inValue = new Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.desbloqueoIMEIRequest();
            inValue.Body = new Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.desbloqueoIMEIRequestBody();
            inValue.Body.BloqueoDesbloqueoIMEIRequest = BloqueoDesbloqueoIMEIRequest;
            Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.desbloqueoIMEIResponse retVal = ((Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment.BloqueoDesbloqueoIMEIWS)(this)).desbloqueoIMEI(inValue);
            return retVal.Body.BloqueoDesbloqueoIMEIResponse;
        }
    }
}