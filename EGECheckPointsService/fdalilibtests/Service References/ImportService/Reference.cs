﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18046
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace fdalilibtests.ImportService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ImportService.IImportService")]
    public interface IImportService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetDictionariesList", ReplyAction="http://tempuri.org/IImportService/GetDictionariesListResponse")]
        System.Xml.XmlElement GetDictionariesList(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetDictionariesList", ReplyAction="http://tempuri.org/IImportService/GetDictionariesListResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetDictionariesListAsync(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetDictionaryDetails", ReplyAction="http://tempuri.org/IImportService/GetDictionaryDetailsResponse")]
        System.Xml.XmlElement GetDictionaryDetails(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetDictionaryDetails", ReplyAction="http://tempuri.org/IImportService/GetDictionaryDetailsResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetDictionaryDetailsAsync(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetInstitutionInfo", ReplyAction="http://tempuri.org/IImportService/GetInstitutionInfoResponse")]
        System.Xml.XmlElement GetInstitutionInfo(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetInstitutionInfo", ReplyAction="http://tempuri.org/IImportService/GetInstitutionInfoResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetInstitutionInfoAsync(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetInstitutionPartOfInfo", ReplyAction="http://tempuri.org/IImportService/GetInstitutionPartOfInfoResponse")]
        System.Xml.XmlElement GetInstitutionPartOfInfo(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetInstitutionPartOfInfo", ReplyAction="http://tempuri.org/IImportService/GetInstitutionPartOfInfoResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetInstitutionPartOfInfoAsync(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/DoImport", ReplyAction="http://tempuri.org/IImportService/DoImportResponse")]
        System.Xml.XmlElement DoImport(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/DoImport", ReplyAction="http://tempuri.org/IImportService/DoImportResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> DoImportAsync(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/DoImportApplicationSingle", ReplyAction="http://tempuri.org/IImportService/DoImportApplicationSingleResponse")]
        System.Xml.XmlElement DoImportApplicationSingle(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/DoImportApplicationSingle", ReplyAction="http://tempuri.org/IImportService/DoImportApplicationSingleResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> DoImportApplicationSingleAsync(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetImportResult", ReplyAction="http://tempuri.org/IImportService/GetImportResultResponse")]
        System.Xml.XmlElement GetImportResult(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetImportResult", ReplyAction="http://tempuri.org/IImportService/GetImportResultResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetImportResultAsync(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/DoValidate", ReplyAction="http://tempuri.org/IImportService/DoValidateResponse")]
        System.Xml.XmlElement DoValidate(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/DoValidate", ReplyAction="http://tempuri.org/IImportService/DoValidateResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> DoValidateAsync(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/DoDelete", ReplyAction="http://tempuri.org/IImportService/DoDeleteResponse")]
        System.Xml.XmlElement DoDelete(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/DoDelete", ReplyAction="http://tempuri.org/IImportService/DoDeleteResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> DoDeleteAsync(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetDeleteResult", ReplyAction="http://tempuri.org/IImportService/GetDeleteResultResponse")]
        System.Xml.XmlElement GetDeleteResult(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetDeleteResult", ReplyAction="http://tempuri.org/IImportService/GetDeleteResultResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetDeleteResultAsync(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/DoCheckApplication", ReplyAction="http://tempuri.org/IImportService/DoCheckApplicationResponse")]
        System.Xml.XmlElement DoCheckApplication(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/DoCheckApplication", ReplyAction="http://tempuri.org/IImportService/DoCheckApplicationResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> DoCheckApplicationAsync(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/DoCheckApplicationSingle", ReplyAction="http://tempuri.org/IImportService/DoCheckApplicationSingleResponse")]
        System.Xml.XmlElement DoCheckApplicationSingle(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/DoCheckApplicationSingle", ReplyAction="http://tempuri.org/IImportService/DoCheckApplicationSingleResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> DoCheckApplicationSingleAsync(System.Xml.XmlElement data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetTestImport", ReplyAction="http://tempuri.org/IImportService/GetTestImportResponse")]
        System.Xml.XmlElement GetTestImport();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetTestImport", ReplyAction="http://tempuri.org/IImportService/GetTestImportResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetTestImportAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetTestRemove", ReplyAction="http://tempuri.org/IImportService/GetTestRemoveResponse")]
        System.Xml.XmlElement GetTestRemove();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetTestRemove", ReplyAction="http://tempuri.org/IImportService/GetTestRemoveResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetTestRemoveAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetTestDictionariesList", ReplyAction="http://tempuri.org/IImportService/GetTestDictionariesListResponse")]
        System.Xml.XmlElement GetTestDictionariesList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetTestDictionariesList", ReplyAction="http://tempuri.org/IImportService/GetTestDictionariesListResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetTestDictionariesListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetTestDictionaryDetails", ReplyAction="http://tempuri.org/IImportService/GetTestDictionaryDetailsResponse")]
        System.Xml.XmlElement GetTestDictionaryDetails();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetTestDictionaryDetails", ReplyAction="http://tempuri.org/IImportService/GetTestDictionaryDetailsResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetTestDictionaryDetailsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetTestCheckApplication", ReplyAction="http://tempuri.org/IImportService/GetTestCheckApplicationResponse")]
        System.Xml.XmlElement GetTestCheckApplication();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImportService/GetTestCheckApplication", ReplyAction="http://tempuri.org/IImportService/GetTestCheckApplicationResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> GetTestCheckApplicationAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IImportServiceChannel : fdalilibtests.ImportService.IImportService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ImportServiceClient : System.ServiceModel.ClientBase<fdalilibtests.ImportService.IImportService>, fdalilibtests.ImportService.IImportService {
        
        public ImportServiceClient() {
        }
        
        public ImportServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ImportServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ImportServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ImportServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Xml.XmlElement GetDictionariesList(System.Xml.XmlElement data) {
            return base.Channel.GetDictionariesList(data);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetDictionariesListAsync(System.Xml.XmlElement data) {
            return base.Channel.GetDictionariesListAsync(data);
        }
        
        public System.Xml.XmlElement GetDictionaryDetails(System.Xml.XmlElement data) {
            return base.Channel.GetDictionaryDetails(data);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetDictionaryDetailsAsync(System.Xml.XmlElement data) {
            return base.Channel.GetDictionaryDetailsAsync(data);
        }
        
        public System.Xml.XmlElement GetInstitutionInfo(System.Xml.XmlElement data) {
            return base.Channel.GetInstitutionInfo(data);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetInstitutionInfoAsync(System.Xml.XmlElement data) {
            return base.Channel.GetInstitutionInfoAsync(data);
        }
        
        public System.Xml.XmlElement GetInstitutionPartOfInfo(System.Xml.XmlElement data) {
            return base.Channel.GetInstitutionPartOfInfo(data);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetInstitutionPartOfInfoAsync(System.Xml.XmlElement data) {
            return base.Channel.GetInstitutionPartOfInfoAsync(data);
        }
        
        public System.Xml.XmlElement DoImport(System.Xml.XmlElement data) {
            return base.Channel.DoImport(data);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> DoImportAsync(System.Xml.XmlElement data) {
            return base.Channel.DoImportAsync(data);
        }
        
        public System.Xml.XmlElement DoImportApplicationSingle(System.Xml.XmlElement data) {
            return base.Channel.DoImportApplicationSingle(data);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> DoImportApplicationSingleAsync(System.Xml.XmlElement data) {
            return base.Channel.DoImportApplicationSingleAsync(data);
        }
        
        public System.Xml.XmlElement GetImportResult(System.Xml.XmlElement data) {
            return base.Channel.GetImportResult(data);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetImportResultAsync(System.Xml.XmlElement data) {
            return base.Channel.GetImportResultAsync(data);
        }
        
        public System.Xml.XmlElement DoValidate(System.Xml.XmlElement data) {
            return base.Channel.DoValidate(data);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> DoValidateAsync(System.Xml.XmlElement data) {
            return base.Channel.DoValidateAsync(data);
        }
        
        public System.Xml.XmlElement DoDelete(System.Xml.XmlElement data) {
            return base.Channel.DoDelete(data);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> DoDeleteAsync(System.Xml.XmlElement data) {
            return base.Channel.DoDeleteAsync(data);
        }
        
        public System.Xml.XmlElement GetDeleteResult(System.Xml.XmlElement data) {
            return base.Channel.GetDeleteResult(data);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetDeleteResultAsync(System.Xml.XmlElement data) {
            return base.Channel.GetDeleteResultAsync(data);
        }
        
        public System.Xml.XmlElement DoCheckApplication(System.Xml.XmlElement data) {
            return base.Channel.DoCheckApplication(data);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> DoCheckApplicationAsync(System.Xml.XmlElement data) {
            return base.Channel.DoCheckApplicationAsync(data);
        }
        
        public System.Xml.XmlElement DoCheckApplicationSingle(System.Xml.XmlElement data) {
            return base.Channel.DoCheckApplicationSingle(data);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> DoCheckApplicationSingleAsync(System.Xml.XmlElement data) {
            return base.Channel.DoCheckApplicationSingleAsync(data);
        }
        
        public System.Xml.XmlElement GetTestImport() {
            return base.Channel.GetTestImport();
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetTestImportAsync() {
            return base.Channel.GetTestImportAsync();
        }
        
        public System.Xml.XmlElement GetTestRemove() {
            return base.Channel.GetTestRemove();
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetTestRemoveAsync() {
            return base.Channel.GetTestRemoveAsync();
        }
        
        public System.Xml.XmlElement GetTestDictionariesList() {
            return base.Channel.GetTestDictionariesList();
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetTestDictionariesListAsync() {
            return base.Channel.GetTestDictionariesListAsync();
        }
        
        public System.Xml.XmlElement GetTestDictionaryDetails() {
            return base.Channel.GetTestDictionaryDetails();
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetTestDictionaryDetailsAsync() {
            return base.Channel.GetTestDictionaryDetailsAsync();
        }
        
        public System.Xml.XmlElement GetTestCheckApplication() {
            return base.Channel.GetTestCheckApplication();
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> GetTestCheckApplicationAsync() {
            return base.Channel.GetTestCheckApplicationAsync();
        }
    }
}
