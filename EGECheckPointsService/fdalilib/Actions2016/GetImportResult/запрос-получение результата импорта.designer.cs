// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.32989
//    <NameSpace>Fdalilib</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>False</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>False</IncludeSerializeMethod><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net20</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>False</GenerateXMLAttributes><OrderXMLAttrib>False</OrderXMLAttrib><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>False</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>False</ExcludeIncludedTypes><EnableInitializeFields>True</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace Fdalilib.Actions2016.GetImportResult
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Collections.Generic;


    public partial class AuthData
    {

        private string loginField;

        private string passField;

        public string Login
        {
            get
            {
                return this.loginField;
            }
            set
            {
                this.loginField = value;
            }
        }

        public string Pass
        {
            get
            {
                return this.passField;
            }
            set
            {
                this.passField = value;
            }
        }
    }

    public partial class GetResultImportApplication
    {

        private uint itemField;

        private ItemChoiceType itemElementNameField;

        //[System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public uint PackageID
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }
    }

    public enum ItemChoiceType
    {

        /// <remarks/>
        PackageGUID,

        /// <remarks/>
        PackageID,
    }

    public partial class Root
    {

        private GetResultImportApplication getResultImportApplicationField;

        private AuthData authDataField;

        public Root()
        {
            this.authDataField = new AuthData();
            this.getResultImportApplicationField = new GetResultImportApplication();
        }

        public GetResultImportApplication GetResultImportApplication
        {
            get
            {
                return this.getResultImportApplicationField;
            }
            set
            {
                this.getResultImportApplicationField = value;
            }
        }

        public AuthData AuthData
        {
            get
            {
                return this.authDataField;
            }
            set
            {
                this.authDataField = value;
            }
        }
    }
}
