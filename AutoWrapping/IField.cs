/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Data.Fields
{
	public interface IField : Sitecore.Abstraction.IAutoWrappedObject<Sitecore.Data.Fields.Field>
	{
		bool CanRead{get;}
bool CanWrite{get;}
bool ContainsStandardValue{get;}
bool InheritsValueFromOtherItem{get;}
Sitecore.Data.IDatabase Database{get;}
Sitecore.Data.Templates.TemplateField Definition{get;}
string Description{get;}
string DisplayName{get;}
string SectionDisplayName{get;}
bool HasBlobStream{get;}
bool HasValue{get;}
string HelpLink{get;}
Sitecore.Data.IID ID{get;}
string InheritedValue{get;}
bool IsBlobField{get;}
bool IsModified{get;}
Sitecore.Data.Items.IItem Item{get;}
string Key{get;}
Sitecore.Globalization.Language Language{get;}
string Name{get;}
bool ResetBlank{get;}
string Section{get;}
string SectionNameByUILocale{get;}
int SectionSortorder{get;}
bool Shared{get;}
bool ShouldBeTranslated{get;}
int Sortorder{get;}
string Source{get;}
string Style{get;}
string Title{get;}
string ToolTip{get;}
bool Translatable{get;}
string Type{get;}
string TypeKey{get;}
bool Unversioned{get;}
string Validation{get;}
string ValidationText{get;}
string Value{get;set;}

		// Instance Methods
		System.IO.Stream GetBlobStream();
string GetStandardValue();
string GetInheritedValue();
string GetValue(bool allowStandardValue);
string GetValue(bool allowStandardValue, bool allowDefaultValue);
void SetBlobStream(System.IO.Stream stream);
void SetValue(string value, bool force);
Sitecore.Data.Templates.TemplateField GetTemplateField();
void Reset();
string ToString();
string GetUniqueId();
	}
}
