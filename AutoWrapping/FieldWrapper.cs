/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Data.Fields
{
	public class FieldWrapper : IField
	{
		public FieldWrapper(Sitecore.Data.Fields.Field innerObject)
		{
			InnerWrappedObject = innerObject;
		}

		public Sitecore.Data.Fields.Field InnerWrappedObject{ get; private set;}

		// Instance Properties
		public bool CanRead
        {
            get { return InnerWrappedObject.CanRead; }
        }

        public bool CanWrite
        {
            get { return InnerWrappedObject.CanWrite; }
        }

        public bool ContainsStandardValue
        {
            get { return InnerWrappedObject.ContainsStandardValue; }
        }

        public bool InheritsValueFromOtherItem
        {
            get { return InnerWrappedObject.InheritsValueFromOtherItem; }
        }

        public Sitecore.Data.IDatabase Database
        {
            get { return new Sitecore.Data.DatabaseWrapper(InnerWrappedObject.Database); }
        }

        public Sitecore.Data.Templates.TemplateField Definition
        {
            get { return InnerWrappedObject.Definition; }
        }

        public string Description
        {
            get { return InnerWrappedObject.Description; }
        }

        public string DisplayName
        {
            get { return InnerWrappedObject.DisplayName; }
        }

        public string SectionDisplayName
        {
            get { return InnerWrappedObject.SectionDisplayName; }
        }

        public bool HasBlobStream
        {
            get { return InnerWrappedObject.HasBlobStream; }
        }

        public bool HasValue
        {
            get { return InnerWrappedObject.HasValue; }
        }

        public string HelpLink
        {
            get { return InnerWrappedObject.HelpLink; }
        }

        public Sitecore.Data.IID ID
        {
            get { return new Sitecore.Data.IDWrapper(InnerWrappedObject.ID); }
        }

        public string InheritedValue
        {
            get { return InnerWrappedObject.InheritedValue; }
        }

        public bool IsBlobField
        {
            get { return InnerWrappedObject.IsBlobField; }
        }

        public bool IsModified
        {
            get { return InnerWrappedObject.IsModified; }
        }

        public Sitecore.Data.Items.IItem Item
        {
            get { return new Sitecore.Data.Items.ItemWrapper(InnerWrappedObject.Item); }
        }

        public string Key
        {
            get { return InnerWrappedObject.Key; }
        }

        public Sitecore.Globalization.Language Language
        {
            get { return InnerWrappedObject.Language; }
        }

        public string Name
        {
            get { return InnerWrappedObject.Name; }
        }

        public bool ResetBlank
        {
            get { return InnerWrappedObject.ResetBlank; }
        }

        public string Section
        {
            get { return InnerWrappedObject.Section; }
        }

        public string SectionNameByUILocale
        {
            get { return InnerWrappedObject.SectionNameByUILocale; }
        }

        public int SectionSortorder
        {
            get { return InnerWrappedObject.SectionSortorder; }
        }

        public bool Shared
        {
            get { return InnerWrappedObject.Shared; }
        }

        public bool ShouldBeTranslated
        {
            get { return InnerWrappedObject.ShouldBeTranslated; }
        }

        public int Sortorder
        {
            get { return InnerWrappedObject.Sortorder; }
        }

        public string Source
        {
            get { return InnerWrappedObject.Source; }
        }

        public string Style
        {
            get { return InnerWrappedObject.Style; }
        }

        public string Title
        {
            get { return InnerWrappedObject.Title; }
        }

        public string ToolTip
        {
            get { return InnerWrappedObject.ToolTip; }
        }

        public bool Translatable
        {
            get { return InnerWrappedObject.Translatable; }
        }

        public string Type
        {
            get { return InnerWrappedObject.Type; }
        }

        public string TypeKey
        {
            get { return InnerWrappedObject.TypeKey; }
        }

        public bool Unversioned
        {
            get { return InnerWrappedObject.Unversioned; }
        }

        public string Validation
        {
            get { return InnerWrappedObject.Validation; }
        }

        public string ValidationText
        {
            get { return InnerWrappedObject.ValidationText; }
        }

        public string Value
        {
            get { return InnerWrappedObject.Value; }
            set { InnerWrappedObject.Value = value; }
        }

        
        		// Instance Methods
        		public System.IO.Stream GetBlobStream()
        {
            return InnerWrappedObject.GetBlobStream();
        }
        public string GetStandardValue()
        {
            return InnerWrappedObject.GetStandardValue();
        }
        public string GetInheritedValue()
        {
            return InnerWrappedObject.GetInheritedValue();
        }
        public string GetValue(bool allowStandardValue)
        {
            return InnerWrappedObject.GetValue(allowStandardValue);
        }
        public string GetValue(bool allowStandardValue, bool allowDefaultValue)
        {
            return InnerWrappedObject.GetValue(allowStandardValue, allowDefaultValue);
        }
        public void SetBlobStream(System.IO.Stream stream)
        {
            InnerWrappedObject.SetBlobStream(stream);
        }
        public void SetValue(string value, bool force)
        {
            InnerWrappedObject.SetValue(value, force);
        }
        public Sitecore.Data.Templates.TemplateField GetTemplateField()
        {
            return InnerWrappedObject.GetTemplateField();
        }
        public void Reset()
        {
            InnerWrappedObject.Reset();
        }
        public string ToString()
        {
            return InnerWrappedObject.ToString();
        }
        public string GetUniqueId()
        {
            return InnerWrappedObject.GetUniqueId();
        }
	}
}
