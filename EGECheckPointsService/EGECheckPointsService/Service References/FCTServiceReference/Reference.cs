﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18033
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EGECheckPointsService.FCTServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:fbs:v2", ConfigurationName="FCTServiceReference.WSChecksSoap")]
    public interface WSChecksSoap {
        
        // CODEGEN: Контракт генерации сообщений с сообщением BatchCheckRequest имеет заголовки.
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/BatchCheck", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        EGECheckPointsService.FCTServiceReference.BatchCheckResponse BatchCheck(EGECheckPointsService.FCTServiceReference.BatchCheckRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/BatchCheck", ReplyAction="*")]
        System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.BatchCheckResponse> BatchCheckAsync(EGECheckPointsService.FCTServiceReference.BatchCheckRequest request);
        
        // CODEGEN: Контракт генерации сообщений с сообщением BatchCheckNNRequest имеет заголовки.
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/BatchCheckNN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        EGECheckPointsService.FCTServiceReference.BatchCheckNNResponse BatchCheckNN(EGECheckPointsService.FCTServiceReference.BatchCheckNNRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/BatchCheckNN", ReplyAction="*")]
        System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.BatchCheckNNResponse> BatchCheckNNAsync(EGECheckPointsService.FCTServiceReference.BatchCheckNNRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/GetBatchCheckQuerySample", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetBatchCheckQuerySample();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/GetBatchCheckQuerySample", ReplyAction="*")]
        System.Threading.Tasks.Task<string> GetBatchCheckQuerySampleAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/GetBatchCheckQuerySampleNN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetBatchCheckQuerySampleNN();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/GetBatchCheckQuerySampleNN", ReplyAction="*")]
        System.Threading.Tasks.Task<string> GetBatchCheckQuerySampleNNAsync();
        
        // CODEGEN: Контракт генерации сообщений с сообщением GetBatchCheckResultRequest имеет заголовки.
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/GetBatchCheckResult", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        EGECheckPointsService.FCTServiceReference.GetBatchCheckResultResponse GetBatchCheckResult(EGECheckPointsService.FCTServiceReference.GetBatchCheckResultRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/GetBatchCheckResult", ReplyAction="*")]
        System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.GetBatchCheckResultResponse> GetBatchCheckResultAsync(EGECheckPointsService.FCTServiceReference.GetBatchCheckResultRequest request);
        
        // CODEGEN: Контракт генерации сообщений с сообщением GetBatchCheckResultNNRequest имеет заголовки.
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/GetBatchCheckResultNN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNResponse GetBatchCheckResultNN(EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/GetBatchCheckResultNN", ReplyAction="*")]
        System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNResponse> GetBatchCheckResultNNAsync(EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/GetSingleCheckQuerySample", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetSingleCheckQuerySample();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/GetSingleCheckQuerySample", ReplyAction="*")]
        System.Threading.Tasks.Task<string> GetSingleCheckQuerySampleAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/GetSingleCheckQuerySampleNN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetSingleCheckQuerySampleNN();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/GetSingleCheckQuerySampleNN", ReplyAction="*")]
        System.Threading.Tasks.Task<string> GetSingleCheckQuerySampleNNAsync();
        
        // CODEGEN: Контракт генерации сообщений с сообщением SingleCheckRequest имеет заголовки.
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/SingleCheck", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        EGECheckPointsService.FCTServiceReference.SingleCheckResponse SingleCheck(EGECheckPointsService.FCTServiceReference.SingleCheckRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/SingleCheck", ReplyAction="*")]
        System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.SingleCheckResponse> SingleCheckAsync(EGECheckPointsService.FCTServiceReference.SingleCheckRequest request);
        
        // CODEGEN: Контракт генерации сообщений с сообщением SingleCheckNNRequest имеет заголовки.
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/SingleCheckNN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        EGECheckPointsService.FCTServiceReference.SingleCheckNNResponse SingleCheckNN(EGECheckPointsService.FCTServiceReference.SingleCheckNNRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:fbs:v2/SingleCheckNN", ReplyAction="*")]
        System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.SingleCheckNNResponse> SingleCheckNNAsync(EGECheckPointsService.FCTServiceReference.SingleCheckNNRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18033")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:fbs:v2")]
    public partial class UserCredentials : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string clientField;
        
        private string loginField;
        
        private string passwordField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Client {
            get {
                return this.clientField;
            }
            set {
                this.clientField = value;
                this.RaisePropertyChanged("Client");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Login {
            get {
                return this.loginField;
            }
            set {
                this.loginField = value;
                this.RaisePropertyChanged("Login");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
                this.RaisePropertyChanged("Password");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
                this.RaisePropertyChanged("AnyAttr");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName="BatchCheck", WrapperNamespace="urn:fbs:v2", IsWrapped=true)]
    public partial class BatchCheckRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="urn:fbs:v2")]
        public EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:fbs:v2", Order=0)]
        public string queryXML;
        
        public BatchCheckRequest() {
        }
        
        public BatchCheckRequest(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            this.UserCredentials = UserCredentials;
            this.queryXML = queryXML;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BatchCheckResponse", WrapperNamespace="urn:fbs:v2", IsWrapped=true)]
    public partial class BatchCheckResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:fbs:v2", Order=0)]
        public string BatchCheckResult;
        
        public BatchCheckResponse() {
        }
        
        public BatchCheckResponse(string BatchCheckResult) {
            this.BatchCheckResult = BatchCheckResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BatchCheckNN", WrapperNamespace="urn:fbs:v2", IsWrapped=true)]
    public partial class BatchCheckNNRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="urn:fbs:v2")]
        public EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:fbs:v2", Order=0)]
        public string queryXML;
        
        public BatchCheckNNRequest() {
        }
        
        public BatchCheckNNRequest(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            this.UserCredentials = UserCredentials;
            this.queryXML = queryXML;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BatchCheckNNResponse", WrapperNamespace="urn:fbs:v2", IsWrapped=true)]
    public partial class BatchCheckNNResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:fbs:v2", Order=0)]
        public string BatchCheckNNResult;
        
        public BatchCheckNNResponse() {
        }
        
        public BatchCheckNNResponse(string BatchCheckNNResult) {
            this.BatchCheckNNResult = BatchCheckNNResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetBatchCheckResult", WrapperNamespace="urn:fbs:v2", IsWrapped=true)]
    public partial class GetBatchCheckResultRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="urn:fbs:v2")]
        public EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:fbs:v2", Order=0)]
        public string queryXML;
        
        public GetBatchCheckResultRequest() {
        }
        
        public GetBatchCheckResultRequest(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            this.UserCredentials = UserCredentials;
            this.queryXML = queryXML;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetBatchCheckResultResponse", WrapperNamespace="urn:fbs:v2", IsWrapped=true)]
    public partial class GetBatchCheckResultResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:fbs:v2", Order=0)]
        public string GetBatchCheckResultResult;
        
        public GetBatchCheckResultResponse() {
        }
        
        public GetBatchCheckResultResponse(string GetBatchCheckResultResult) {
            this.GetBatchCheckResultResult = GetBatchCheckResultResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetBatchCheckResultNN", WrapperNamespace="urn:fbs:v2", IsWrapped=true)]
    public partial class GetBatchCheckResultNNRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="urn:fbs:v2")]
        public EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:fbs:v2", Order=0)]
        public string queryXML;
        
        public GetBatchCheckResultNNRequest() {
        }
        
        public GetBatchCheckResultNNRequest(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            this.UserCredentials = UserCredentials;
            this.queryXML = queryXML;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetBatchCheckResultNNResponse", WrapperNamespace="urn:fbs:v2", IsWrapped=true)]
    public partial class GetBatchCheckResultNNResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:fbs:v2", Order=0)]
        public string GetBatchCheckResultNNResult;
        
        public GetBatchCheckResultNNResponse() {
        }
        
        public GetBatchCheckResultNNResponse(string GetBatchCheckResultNNResult) {
            this.GetBatchCheckResultNNResult = GetBatchCheckResultNNResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SingleCheck", WrapperNamespace="urn:fbs:v2", IsWrapped=true)]
    public partial class SingleCheckRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="urn:fbs:v2")]
        public EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:fbs:v2", Order=0)]
        public string queryXML;
        
        public SingleCheckRequest() {
        }
        
        public SingleCheckRequest(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            this.UserCredentials = UserCredentials;
            this.queryXML = queryXML;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SingleCheckResponse", WrapperNamespace="urn:fbs:v2", IsWrapped=true)]
    public partial class SingleCheckResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:fbs:v2", Order=0)]
        public string SingleCheckResult;
        
        public SingleCheckResponse() {
        }
        
        public SingleCheckResponse(string SingleCheckResult) {
            this.SingleCheckResult = SingleCheckResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SingleCheckNN", WrapperNamespace="urn:fbs:v2", IsWrapped=true)]
    public partial class SingleCheckNNRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="urn:fbs:v2")]
        public EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:fbs:v2", Order=0)]
        public string queryXML;
        
        public SingleCheckNNRequest() {
        }
        
        public SingleCheckNNRequest(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            this.UserCredentials = UserCredentials;
            this.queryXML = queryXML;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SingleCheckNNResponse", WrapperNamespace="urn:fbs:v2", IsWrapped=true)]
    public partial class SingleCheckNNResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:fbs:v2", Order=0)]
        public string SingleCheckNNResult;
        
        public SingleCheckNNResponse() {
        }
        
        public SingleCheckNNResponse(string SingleCheckNNResult) {
            this.SingleCheckNNResult = SingleCheckNNResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WSChecksSoapChannel : EGECheckPointsService.FCTServiceReference.WSChecksSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WSChecksSoapClient : System.ServiceModel.ClientBase<EGECheckPointsService.FCTServiceReference.WSChecksSoap>, EGECheckPointsService.FCTServiceReference.WSChecksSoap {
        
        public WSChecksSoapClient() {
        }
        
        public WSChecksSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WSChecksSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSChecksSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSChecksSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        EGECheckPointsService.FCTServiceReference.BatchCheckResponse EGECheckPointsService.FCTServiceReference.WSChecksSoap.BatchCheck(EGECheckPointsService.FCTServiceReference.BatchCheckRequest request) {
            return base.Channel.BatchCheck(request);
        }
        
        public string BatchCheck(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            EGECheckPointsService.FCTServiceReference.BatchCheckRequest inValue = new EGECheckPointsService.FCTServiceReference.BatchCheckRequest();
            inValue.UserCredentials = UserCredentials;
            inValue.queryXML = queryXML;
            EGECheckPointsService.FCTServiceReference.BatchCheckResponse retVal = ((EGECheckPointsService.FCTServiceReference.WSChecksSoap)(this)).BatchCheck(inValue);
            return retVal.BatchCheckResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.BatchCheckResponse> EGECheckPointsService.FCTServiceReference.WSChecksSoap.BatchCheckAsync(EGECheckPointsService.FCTServiceReference.BatchCheckRequest request) {
            return base.Channel.BatchCheckAsync(request);
        }
        
        public System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.BatchCheckResponse> BatchCheckAsync(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            EGECheckPointsService.FCTServiceReference.BatchCheckRequest inValue = new EGECheckPointsService.FCTServiceReference.BatchCheckRequest();
            inValue.UserCredentials = UserCredentials;
            inValue.queryXML = queryXML;
            return ((EGECheckPointsService.FCTServiceReference.WSChecksSoap)(this)).BatchCheckAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        EGECheckPointsService.FCTServiceReference.BatchCheckNNResponse EGECheckPointsService.FCTServiceReference.WSChecksSoap.BatchCheckNN(EGECheckPointsService.FCTServiceReference.BatchCheckNNRequest request) {
            return base.Channel.BatchCheckNN(request);
        }
        
        public string BatchCheckNN(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            EGECheckPointsService.FCTServiceReference.BatchCheckNNRequest inValue = new EGECheckPointsService.FCTServiceReference.BatchCheckNNRequest();
            inValue.UserCredentials = UserCredentials;
            inValue.queryXML = queryXML;
            EGECheckPointsService.FCTServiceReference.BatchCheckNNResponse retVal = ((EGECheckPointsService.FCTServiceReference.WSChecksSoap)(this)).BatchCheckNN(inValue);
            return retVal.BatchCheckNNResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.BatchCheckNNResponse> EGECheckPointsService.FCTServiceReference.WSChecksSoap.BatchCheckNNAsync(EGECheckPointsService.FCTServiceReference.BatchCheckNNRequest request) {
            return base.Channel.BatchCheckNNAsync(request);
        }
        
        public System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.BatchCheckNNResponse> BatchCheckNNAsync(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            EGECheckPointsService.FCTServiceReference.BatchCheckNNRequest inValue = new EGECheckPointsService.FCTServiceReference.BatchCheckNNRequest();
            inValue.UserCredentials = UserCredentials;
            inValue.queryXML = queryXML;
            return ((EGECheckPointsService.FCTServiceReference.WSChecksSoap)(this)).BatchCheckNNAsync(inValue);
        }
        
        public string GetBatchCheckQuerySample() {
            return base.Channel.GetBatchCheckQuerySample();
        }
        
        public System.Threading.Tasks.Task<string> GetBatchCheckQuerySampleAsync() {
            return base.Channel.GetBatchCheckQuerySampleAsync();
        }
        
        public string GetBatchCheckQuerySampleNN() {
            return base.Channel.GetBatchCheckQuerySampleNN();
        }
        
        public System.Threading.Tasks.Task<string> GetBatchCheckQuerySampleNNAsync() {
            return base.Channel.GetBatchCheckQuerySampleNNAsync();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        EGECheckPointsService.FCTServiceReference.GetBatchCheckResultResponse EGECheckPointsService.FCTServiceReference.WSChecksSoap.GetBatchCheckResult(EGECheckPointsService.FCTServiceReference.GetBatchCheckResultRequest request) {
            return base.Channel.GetBatchCheckResult(request);
        }
        
        public string GetBatchCheckResult(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            EGECheckPointsService.FCTServiceReference.GetBatchCheckResultRequest inValue = new EGECheckPointsService.FCTServiceReference.GetBatchCheckResultRequest();
            inValue.UserCredentials = UserCredentials;
            inValue.queryXML = queryXML;
            EGECheckPointsService.FCTServiceReference.GetBatchCheckResultResponse retVal = ((EGECheckPointsService.FCTServiceReference.WSChecksSoap)(this)).GetBatchCheckResult(inValue);
            return retVal.GetBatchCheckResultResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.GetBatchCheckResultResponse> EGECheckPointsService.FCTServiceReference.WSChecksSoap.GetBatchCheckResultAsync(EGECheckPointsService.FCTServiceReference.GetBatchCheckResultRequest request) {
            return base.Channel.GetBatchCheckResultAsync(request);
        }
        
        public System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.GetBatchCheckResultResponse> GetBatchCheckResultAsync(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            EGECheckPointsService.FCTServiceReference.GetBatchCheckResultRequest inValue = new EGECheckPointsService.FCTServiceReference.GetBatchCheckResultRequest();
            inValue.UserCredentials = UserCredentials;
            inValue.queryXML = queryXML;
            return ((EGECheckPointsService.FCTServiceReference.WSChecksSoap)(this)).GetBatchCheckResultAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNResponse EGECheckPointsService.FCTServiceReference.WSChecksSoap.GetBatchCheckResultNN(EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNRequest request) {
            return base.Channel.GetBatchCheckResultNN(request);
        }
        
        public string GetBatchCheckResultNN(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNRequest inValue = new EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNRequest();
            inValue.UserCredentials = UserCredentials;
            inValue.queryXML = queryXML;
            EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNResponse retVal = ((EGECheckPointsService.FCTServiceReference.WSChecksSoap)(this)).GetBatchCheckResultNN(inValue);
            return retVal.GetBatchCheckResultNNResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNResponse> EGECheckPointsService.FCTServiceReference.WSChecksSoap.GetBatchCheckResultNNAsync(EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNRequest request) {
            return base.Channel.GetBatchCheckResultNNAsync(request);
        }
        
        public System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNResponse> GetBatchCheckResultNNAsync(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNRequest inValue = new EGECheckPointsService.FCTServiceReference.GetBatchCheckResultNNRequest();
            inValue.UserCredentials = UserCredentials;
            inValue.queryXML = queryXML;
            return ((EGECheckPointsService.FCTServiceReference.WSChecksSoap)(this)).GetBatchCheckResultNNAsync(inValue);
        }
        
        public string GetSingleCheckQuerySample() {
            return base.Channel.GetSingleCheckQuerySample();
        }
        
        public System.Threading.Tasks.Task<string> GetSingleCheckQuerySampleAsync() {
            return base.Channel.GetSingleCheckQuerySampleAsync();
        }
        
        public string GetSingleCheckQuerySampleNN() {
            return base.Channel.GetSingleCheckQuerySampleNN();
        }
        
        public System.Threading.Tasks.Task<string> GetSingleCheckQuerySampleNNAsync() {
            return base.Channel.GetSingleCheckQuerySampleNNAsync();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        EGECheckPointsService.FCTServiceReference.SingleCheckResponse EGECheckPointsService.FCTServiceReference.WSChecksSoap.SingleCheck(EGECheckPointsService.FCTServiceReference.SingleCheckRequest request) {
            return base.Channel.SingleCheck(request);
        }
        
        public string SingleCheck(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            EGECheckPointsService.FCTServiceReference.SingleCheckRequest inValue = new EGECheckPointsService.FCTServiceReference.SingleCheckRequest();
            inValue.UserCredentials = UserCredentials;
            inValue.queryXML = queryXML;
            EGECheckPointsService.FCTServiceReference.SingleCheckResponse retVal = ((EGECheckPointsService.FCTServiceReference.WSChecksSoap)(this)).SingleCheck(inValue);
            return retVal.SingleCheckResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.SingleCheckResponse> EGECheckPointsService.FCTServiceReference.WSChecksSoap.SingleCheckAsync(EGECheckPointsService.FCTServiceReference.SingleCheckRequest request) {
            return base.Channel.SingleCheckAsync(request);
        }
        
        public System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.SingleCheckResponse> SingleCheckAsync(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            EGECheckPointsService.FCTServiceReference.SingleCheckRequest inValue = new EGECheckPointsService.FCTServiceReference.SingleCheckRequest();
            inValue.UserCredentials = UserCredentials;
            inValue.queryXML = queryXML;
            return ((EGECheckPointsService.FCTServiceReference.WSChecksSoap)(this)).SingleCheckAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        EGECheckPointsService.FCTServiceReference.SingleCheckNNResponse EGECheckPointsService.FCTServiceReference.WSChecksSoap.SingleCheckNN(EGECheckPointsService.FCTServiceReference.SingleCheckNNRequest request) {
            return base.Channel.SingleCheckNN(request);
        }
        
        public string SingleCheckNN(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            EGECheckPointsService.FCTServiceReference.SingleCheckNNRequest inValue = new EGECheckPointsService.FCTServiceReference.SingleCheckNNRequest();
            inValue.UserCredentials = UserCredentials;
            inValue.queryXML = queryXML;
            EGECheckPointsService.FCTServiceReference.SingleCheckNNResponse retVal = ((EGECheckPointsService.FCTServiceReference.WSChecksSoap)(this)).SingleCheckNN(inValue);
            return retVal.SingleCheckNNResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.SingleCheckNNResponse> EGECheckPointsService.FCTServiceReference.WSChecksSoap.SingleCheckNNAsync(EGECheckPointsService.FCTServiceReference.SingleCheckNNRequest request) {
            return base.Channel.SingleCheckNNAsync(request);
        }
        
        public System.Threading.Tasks.Task<EGECheckPointsService.FCTServiceReference.SingleCheckNNResponse> SingleCheckNNAsync(EGECheckPointsService.FCTServiceReference.UserCredentials UserCredentials, string queryXML) {
            EGECheckPointsService.FCTServiceReference.SingleCheckNNRequest inValue = new EGECheckPointsService.FCTServiceReference.SingleCheckNNRequest();
            inValue.UserCredentials = UserCredentials;
            inValue.queryXML = queryXML;
            return ((EGECheckPointsService.FCTServiceReference.WSChecksSoap)(this)).SingleCheckNNAsync(inValue);
        }
    }
}
