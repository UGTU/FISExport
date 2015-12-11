// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.32989
//    <NameSpace>Fdalilib</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>False</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>False</IncludeSerializeMethod><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net20</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>False</GenerateXMLAttributes><OrderXMLAttrib>False</OrderXMLAttrib><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>False</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>False</ExcludeIncludedTypes><EnableInitializeFields>True</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Fdalilib.ImportClasses
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Collections.Generic;


    public partial class PackageData
    {

        private List<ItemsChoiceType> itemsElementNameField;

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public List<ItemsChoiceType> ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
    }

    public partial class TStatusOrUID
    {

        private object itemField;

        public object Item
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
    }

    public partial class PackageDataApplications
    {

        private List<TStatusOrUID> statusOrUIDField;

        public PackageDataApplications()
        {
            this.statusOrUIDField = new List<TStatusOrUID>();
        }

        public List<TStatusOrUID> StatusOrUID
        {
            get
            {
                return this.statusOrUIDField;
            }
            set
            {
                this.statusOrUIDField = value;
            }
        }
    }

    public partial class PackageDataCampaigns
    {

        private List<string> campaignUIDField;

        public PackageDataCampaigns()
        {
            this.campaignUIDField = new List<string>();
        }

        public List<string> CampaignUID
        {
            get
            {
                return this.campaignUIDField;
            }
            set
            {
                this.campaignUIDField = value;
            }
        }
    }

    public enum ItemsChoiceType
    {

        /// <remarks/>
        AdmissionVolume,

        /// <remarks/>
        AllowedDirections,

        /// <remarks/>
        Applications,

        /// <remarks/>
        Campaigns,

        /// <remarks/>
        CompetitiveGroups,

        /// <remarks/>
        DistributedAdmissionVolume,

        /// <remarks/>
        InstitutionAchievements,

        /// <remarks/>
        InstitutionDetails,

        /// <remarks/>
        OrdersOfAdmission,

        /// <remarks/>
        RecommendedLists,

        /// <remarks/>
        Structure,
    }

}
