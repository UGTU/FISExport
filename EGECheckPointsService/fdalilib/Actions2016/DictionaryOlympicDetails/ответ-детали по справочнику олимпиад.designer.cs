// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.32989
//    <NameSpace>Fdalilib</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>False</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>False</IncludeSerializeMethod><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net20</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>False</GenerateXMLAttributes><OrderXMLAttrib>False</OrderXMLAttrib><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>False</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>False</ExcludeIncludedTypes><EnableInitializeFields>True</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace Fdalilib.Actions2016.DictionaryOlympicDetails
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Collections.Generic;


    public partial class DictionaryData
    {

        private uint codeField;

        private string nameField;

        private List<DictionaryDataDictionaryItem> dictionaryItemsField;

        public DictionaryData()
        {
            this.dictionaryItemsField = new List<DictionaryDataDictionaryItem>();
        }

        public uint Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("DictionaryItem", IsNullable = false)]
        public List<DictionaryDataDictionaryItem> DictionaryItems
        {
            get
            {
                return this.dictionaryItemsField;
            }
            set
            {
                this.dictionaryItemsField = value;
            }
        }
    }

    public partial class DictionaryDataDictionaryItem
    {

        private uint olympicIDField;

        private uint olympicNumberField;

        private bool olympicNumberFieldSpecified;

        private string olympicNameField;

        private DictionaryDataDictionaryItemProfiles profilesField;

        public DictionaryDataDictionaryItem()
        {
            this.profilesField = new DictionaryDataDictionaryItemProfiles();
        }

        public uint OlympicID
        {
            get
            {
                return this.olympicIDField;
            }
            set
            {
                this.olympicIDField = value;
            }
        }

        public uint OlympicNumber
        {
            get
            {
                return this.olympicNumberField;
            }
            set
            {
                this.olympicNumberField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OlympicNumberSpecified
        {
            get
            {
                return this.olympicNumberFieldSpecified;
            }
            set
            {
                this.olympicNumberFieldSpecified = value;
            }
        }

        public string OlympicName
        {
            get
            {
                return this.olympicNameField;
            }
            set
            {
                this.olympicNameField = value;
            }
        }

        public DictionaryDataDictionaryItemProfiles Profiles
        {
            get
            {
                return this.profilesField;
            }
            set
            {
                this.profilesField = value;
            }
        }
    }

    public partial class DictionaryDataDictionaryItemProfiles
    {

        private DictionaryDataDictionaryItemProfilesProfile profileField;

        public DictionaryDataDictionaryItemProfiles()
        {
            this.profileField = new DictionaryDataDictionaryItemProfilesProfile();
        }

        public DictionaryDataDictionaryItemProfilesProfile Profile
        {
            get
            {
                return this.profileField;
            }
            set
            {
                this.profileField = value;
            }
        }
    }

    public partial class DictionaryDataDictionaryItemProfilesProfile
    {

        private uint profileIDField;

        private List<uint> subjectsField;

        private uint levelIDField;

        private bool levelIDFieldSpecified;

        public DictionaryDataDictionaryItemProfilesProfile()
        {
            this.subjectsField = new List<uint>();
        }

        public uint ProfileID
        {
            get
            {
                return this.profileIDField;
            }
            set
            {
                this.profileIDField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("SubjectID", IsNullable = false)]
        public List<uint> Subjects
        {
            get
            {
                return this.subjectsField;
            }
            set
            {
                this.subjectsField = value;
            }
        }

        public uint LevelID
        {
            get
            {
                return this.levelIDField;
            }
            set
            {
                this.levelIDField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LevelIDSpecified
        {
            get
            {
                return this.levelIDFieldSpecified;
            }
            set
            {
                this.levelIDFieldSpecified = value;
            }
        }
    }

    public partial class TError
    {

        private string errorCodeField;

        private string errorTextField;

        public string ErrorCode
        {
            get
            {
                return this.errorCodeField;
            }
            set
            {
                this.errorCodeField = value;
            }
        }

        public string ErrorText
        {
            get
            {
                return this.errorTextField;
            }
            set
            {
                this.errorTextField = value;
            }
        }
    }
}
