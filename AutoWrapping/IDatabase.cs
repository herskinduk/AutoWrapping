/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Data
{
	public interface IDatabase : Sitecore.Abstraction.IAutoWrappedObject<Sitecore.Data.Database>
	{
		Sitecore.Data.AliasResolver Aliases{get;}
System.Collections.Generic.List<string> ArchiveNames{get;}
Sitecore.Data.Archiving.DataArchives Archives{get;}
Sitecore.Data.DatabaseCaches Caches{get;}
string ConnectionStringName{get;set;}
Sitecore.Data.DataManager DataManager{get;}
Sitecore.Data.DatabaseEngines Engines{get;}
bool HasContentItem{get;}
string Icon{get;set;}
Sitecore.Data.ItemRecords Items{get;}
Sitecore.Globalization.Language[] Languages{get;}
Sitecore.Data.BranchRecords Branches{get;}
string Name{get;}
Sitecore.Data.DatabaseProperties Properties{get;}
bool Protected{get;set;}
bool ProxiesEnabled{get;set;}
Sitecore.Data.Proxies.ProxyDataProvider ProxyDataProvider{get;set;}
bool PublishVirtualItems{get;set;}
bool ReadOnly{get;set;}
Sitecore.Data.Eventing.DatabaseRemoteEvents RemoteEvents{get;}
Sitecore.Resources.ResourceItems Resources{get;}
bool SecurityEnabled{get;set;}
Sitecore.Data.Items.IItem SitecoreItem{get;}
Sitecore.Data.TemplateRecords Templates{get;}
Sitecore.Workflows.IWorkflowProvider WorkflowProvider{get;set;}
Sitecore.Data.Clones.NotificationProvider NotificationProvider{get;set;}

		// Instance Methods
		bool CleanupDatabase();
Sitecore.Data.Items.IItem CreateItemPath(string path);
Sitecore.Data.Items.IItem CreateItemPath(string path, Sitecore.Data.Items.TemplateItem template);
Sitecore.Data.Items.IItem CreateItemPath(string path, Sitecore.Data.Items.TemplateItem folderTemplate, Sitecore.Data.Items.TemplateItem itemTemplate);
Sitecore.Data.DataProviders.DataProvider[] GetDataProviders();
long GetDataSize(int minEntitySize, int maxEntitySize);
long GetDictionaryEntryCount();
Sitecore.Data.Items.IItem GetItem(Sitecore.Data.IID itemId);
Sitecore.Data.Items.IItem GetItem(Sitecore.Data.IID itemId, Sitecore.Globalization.Language language);
Sitecore.Data.Items.IItem GetItem(Sitecore.Data.IID itemId, Sitecore.Globalization.Language language, Sitecore.Data.Version version);
Sitecore.Data.Items.IItem GetItem(string path);
Sitecore.Data.Items.IItem GetItem(string path, Sitecore.Globalization.Language language);
Sitecore.Data.Items.IItem GetItem(string path, Sitecore.Globalization.Language language, Sitecore.Data.Version version);
Sitecore.Data.Items.IItem GetItem(Sitecore.Data.DataUri uri);
Sitecore.Collections.LanguageCollection GetLanguages();
Sitecore.Data.Items.IItem GetRootItem();
Sitecore.Data.Items.IItem GetRootItem(Sitecore.Globalization.Language language);
Sitecore.Data.Items.TemplateItem GetTemplate(Sitecore.Data.IID templateId);
Sitecore.Data.Items.TemplateItem GetTemplate(string fullName);
IEnumerable<Sitecore.Data.Items.IItem> SelectItems(string query);
Sitecore.Collections.ItemList SelectItemsUsingXPath(string query);
Sitecore.Data.Items.IItem SelectSingleItem(string query);
Sitecore.Data.Items.IItem SelectSingleItemUsingXPath(string query);
string ToString();
	}
}
