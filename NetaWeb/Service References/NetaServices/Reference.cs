﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetaWeb.NetaServices {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BBandPassRate", Namespace="http://schemas.datacontract.org/2004/07/NetaService")]
    [System.SerializableAttribute()]
    public partial class BBandPassRate : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal AverageSpeedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal PassRateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PostCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SchoolNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal AverageSpeed {
            get {
                return this.AverageSpeedField;
            }
            set {
                if ((this.AverageSpeedField.Equals(value) != true)) {
                    this.AverageSpeedField = value;
                    this.RaisePropertyChanged("AverageSpeed");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal PassRate {
            get {
                return this.PassRateField;
            }
            set {
                if ((this.PassRateField.Equals(value) != true)) {
                    this.PassRateField = value;
                    this.RaisePropertyChanged("PassRate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PostCode {
            get {
                return this.PostCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.PostCodeField, value) != true)) {
                    this.PostCodeField = value;
                    this.RaisePropertyChanged("PostCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SchoolName {
            get {
                return this.SchoolNameField;
            }
            set {
                if ((object.ReferenceEquals(this.SchoolNameField, value) != true)) {
                    this.SchoolNameField = value;
                    this.RaisePropertyChanged("SchoolName");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="TopBroadbandSpeed", Namespace="http://schemas.datacontract.org/2004/07/NetaService")]
    [System.SerializableAttribute()]
    public partial class TopBroadbandSpeed : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal AverageSpeedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal LatField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal LngField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal AverageSpeed {
            get {
                return this.AverageSpeedField;
            }
            set {
                if ((this.AverageSpeedField.Equals(value) != true)) {
                    this.AverageSpeedField = value;
                    this.RaisePropertyChanged("AverageSpeed");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Lat {
            get {
                return this.LatField;
            }
            set {
                if ((this.LatField.Equals(value) != true)) {
                    this.LatField = value;
                    this.RaisePropertyChanged("Lat");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Lng {
            get {
                return this.LngField;
            }
            set {
                if ((this.LngField.Equals(value) != true)) {
                    this.LngField = value;
                    this.RaisePropertyChanged("Lng");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/NetaService")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="NetaServices.INetaService")]
    public interface INetaService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetaService/GetData", ReplyAction="http://tempuri.org/INetaService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetaService/GetData", ReplyAction="http://tempuri.org/INetaService/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetaService/MyView", ReplyAction="http://tempuri.org/INetaService/MyViewResponse")]
        NetaWeb.NetaServices.BBandPassRate[] MyView();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetaService/MyView", ReplyAction="http://tempuri.org/INetaService/MyViewResponse")]
        System.Threading.Tasks.Task<NetaWeb.NetaServices.BBandPassRate[]> MyViewAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetaService/Coordinates", ReplyAction="http://tempuri.org/INetaService/CoordinatesResponse")]
        NetaWeb.NetaServices.TopBroadbandSpeed[] Coordinates();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetaService/Coordinates", ReplyAction="http://tempuri.org/INetaService/CoordinatesResponse")]
        System.Threading.Tasks.Task<NetaWeb.NetaServices.TopBroadbandSpeed[]> CoordinatesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetaService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/INetaService/GetDataUsingDataContractResponse")]
        NetaWeb.NetaServices.CompositeType GetDataUsingDataContract(NetaWeb.NetaServices.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetaService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/INetaService/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<NetaWeb.NetaServices.CompositeType> GetDataUsingDataContractAsync(NetaWeb.NetaServices.CompositeType composite);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface INetaServiceChannel : NetaWeb.NetaServices.INetaService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NetaServiceClient : System.ServiceModel.ClientBase<NetaWeb.NetaServices.INetaService>, NetaWeb.NetaServices.INetaService {
        
        public NetaServiceClient() {
        }
        
        public NetaServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NetaServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NetaServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NetaServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public NetaWeb.NetaServices.BBandPassRate[] MyView() {
            return base.Channel.MyView();
        }
        
        public System.Threading.Tasks.Task<NetaWeb.NetaServices.BBandPassRate[]> MyViewAsync() {
            return base.Channel.MyViewAsync();
        }
        
        public NetaWeb.NetaServices.TopBroadbandSpeed[] Coordinates() {
            return base.Channel.Coordinates();
        }
        
        public System.Threading.Tasks.Task<NetaWeb.NetaServices.TopBroadbandSpeed[]> CoordinatesAsync() {
            return base.Channel.CoordinatesAsync();
        }
        
        public NetaWeb.NetaServices.CompositeType GetDataUsingDataContract(NetaWeb.NetaServices.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<NetaWeb.NetaServices.CompositeType> GetDataUsingDataContractAsync(NetaWeb.NetaServices.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
    }
}
