/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Data
{
	public class DatabaseWrapper : IDatabase
	{
		public DatabaseWrapper(Sitecore.Data.Database innerObject)
		{
			InnerWrappedObject = innerObject;
		}

		public Sitecore.Data.Database InnerWrappedObject{ get; private set;}

		// Instance Properties
		public Sitecore.Data.AliasResolver Aliases
        {
            get { return InnerWrappedObject.Aliases; }
        }

        public System.Collections.Generic.List<string> ArchiveNames
        {
            get { return InnerWrappedObject.ArchiveNames; }
        }

        public Sitecore.Data.Archiving.DataArchives Archives
        {
            get { return InnerWrappedObject.Archives; }
        }

        public Sitecore.Data.DatabaseCaches Caches
        {
            get { return InnerWrappedObject.Caches; }
        }

        public string ConnectionStringName
        {
            get { return InnerWrappedObject.ConnectionStringName; }
            set { InnerWrappedObject.ConnectionStringName = value; }
        }

        public Sitecore.Data.DataManager DataManager
        {
            get { return InnerWrappedObject.DataManager; }
        }

        public Sitecore.Data.DatabaseEngines Engines
        {
            get { return InnerWrappedObject.Engines; }
        }

        public bool HasContentItem
        {
            get { return InnerWrappedObject.HasContentItem; }
        }

        public string Icon
        {
            get { return InnerWrappedObject.Icon; }
            set { InnerWrappedObject.Icon = value; }
        }

        public Sitecore.Data.Indexing.DataIndexes Indexes
        {
            get { return InnerWrappedObject.Indexes; }
        }

        public Sitecore.Data.ItemRecords Items
        {
            get { return InnerWrappedObject.Items; }
        }

        public Sitecore.Globalization.Language[] Languages
        {
            get { return InnerWrappedObject.Languages; }
        }

        public Sitecore.Data.BranchRecords Branches
        {
            get { return InnerWrappedObject.Branches; }
        }

        public Sitecore.Data.BranchRecords Masters
        {
            get { return InnerWrappedObject.Masters; }
        }

        public string Name
        {
            get { return InnerWrappedObject.Name; }
        }

        public Sitecore.Data.DatabaseProperties Properties
        {
            get { return InnerWrappedObject.Properties; }
        }

        public bool Protected
        {
            get { return InnerWrappedObject.Protected; }
            set { InnerWrappedObject.Protected = value; }
        }

        public bool ProxiesEnabled
        {
            get { return InnerWrappedObject.ProxiesEnabled; }
            set { InnerWrappedObject.ProxiesEnabled = value; }
        }

        public Sitecore.Data.Proxies.ProxyDataProvider ProxyDataProvider
        {
            get { return InnerWrappedObject.ProxyDataProvider; }
            set { InnerWrappedObject.ProxyDataProvider = value; }
        }

        public bool PublishVirtualItems
        {
            get { return InnerWrappedObject.PublishVirtualItems; }
            set { InnerWrappedObject.PublishVirtualItems = value; }
        }

        public bool ReadOnly
        {
            get { return InnerWrappedObject.ReadOnly; }
            set { InnerWrappedObject.ReadOnly = value; }
        }

        public Sitecore.Data.Eventing.DatabaseRemoteEvents RemoteEvents
        {
            get { return InnerWrappedObject.RemoteEvents; }
        }

        public Sitecore.Resources.ResourceItems Resources
        {
            get { return InnerWrappedObject.Resources; }
        }

        public bool SecurityEnabled
        {
            get { return InnerWrappedObject.SecurityEnabled; }
            set { InnerWrappedObject.SecurityEnabled = value; }
        }

        public Sitecore.Data.Items.IItem SitecoreItem
        {
            get { return new Sitecore.Data.Items.ItemWrapper(InnerWrappedObject.SitecoreItem); }
        }

        public Sitecore.Data.TemplateRecords Templates
        {
            get { return InnerWrappedObject.Templates; }
        }

        public Sitecore.Workflows.IWorkflowProvider WorkflowProvider
        {
            get { return InnerWrappedObject.WorkflowProvider; }
            set { InnerWrappedObject.WorkflowProvider = value; }
        }

        public Sitecore.Data.Clones.NotificationProvider NotificationProvider
        {
            get { return InnerWrappedObject.NotificationProvider; }
            set { InnerWrappedObject.NotificationProvider = value; }
        }

        
        		// Instance Methods
        		public bool CleanupDatabase()
        {
            return InnerWrappedObject.CleanupDatabase();
        }
        public Sitecore.Data.Items.IItem CreateItemPath(string path)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.CreateItemPath(path));
        }
        public Sitecore.Data.Items.IItem CreateItemPath(string path, Sitecore.Data.Items.TemplateItem template)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.CreateItemPath(path, template));
        }
        public Sitecore.Data.Items.IItem CreateItemPath(string path, Sitecore.Data.Items.TemplateItem folderTemplate, Sitecore.Data.Items.TemplateItem itemTemplate)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.CreateItemPath(path, folderTemplate, itemTemplate));
        }
        public Sitecore.Data.DataProviders.DataProvider[] GetDataProviders()
        {
            return InnerWrappedObject.GetDataProviders();
        }
        public long GetDataSize(int minEntitySize, int maxEntitySize)
        {
            return InnerWrappedObject.GetDataSize(minEntitySize, maxEntitySize);
        }
        public long GetDictionaryEntryCount()
        {
            return InnerWrappedObject.GetDictionaryEntryCount();
        }
        public Sitecore.Data.Items.IItem GetItem(Sitecore.Data.IID itemId)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.GetItem((Sitecore.Data.ID)itemId.InnerWrappedObject));
        }
        public Sitecore.Data.Items.IItem GetItem(Sitecore.Data.IID itemId, Sitecore.Globalization.Language language)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.GetItem((Sitecore.Data.ID)itemId.InnerWrappedObject, language));
        }
        public Sitecore.Data.Items.IItem GetItem(Sitecore.Data.IID itemId, Sitecore.Globalization.Language language, Sitecore.Data.Version version)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.GetItem((Sitecore.Data.ID)itemId.InnerWrappedObject, language, version));
        }
        public Sitecore.Data.Items.IItem GetItem(string path)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.GetItem(path));
        }
        public Sitecore.Data.Items.IItem GetItem(string path, Sitecore.Globalization.Language language)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.GetItem(path, language));
        }
        public Sitecore.Data.Items.IItem GetItem(string path, Sitecore.Globalization.Language language, Sitecore.Data.Version version)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.GetItem(path, language, version));
        }
        public Sitecore.Data.Items.IItem GetItem(Sitecore.Data.DataUri uri)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.GetItem(uri));
        }
        public Sitecore.Collections.LanguageCollection GetLanguages()
        {
            return InnerWrappedObject.GetLanguages();
        }
        public Sitecore.Data.Items.IItem GetRootItem()
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.GetRootItem());
        }
        public Sitecore.Data.Items.IItem GetRootItem(Sitecore.Globalization.Language language)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.GetRootItem(language));
        }
        public Lucene.Net.Search.IndexSearcher GetSearcher(string name)
        {
            return InnerWrappedObject.GetSearcher(name);
        }
        public Sitecore.Data.Items.TemplateItem GetTemplate(Sitecore.Data.IID templateId)
        {
            return InnerWrappedObject.GetTemplate((Sitecore.Data.ID)templateId.InnerWrappedObject);
        }
        public Sitecore.Data.Items.TemplateItem GetTemplate(string fullName)
        {
            return InnerWrappedObject.GetTemplate(fullName);
        }
        public IEnumerable<Sitecore.Data.Items.IItem> SelectItems(string query)
        {
            return InnerWrappedObject.SelectItems(query).Select(x => new Sitecore.Data.Items.ItemWrapper(x)).AsEnumerable();
        }
        public Sitecore.Collections.ItemList SelectItemsUsingXPath(string query)
        {
            return InnerWrappedObject.SelectItemsUsingXPath(query);
        }
        public Sitecore.Data.Items.IItem SelectSingleItem(string query)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.SelectSingleItem(query));
        }
        public Sitecore.Data.Items.IItem SelectSingleItemUsingXPath(string query)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.SelectSingleItemUsingXPath(query));
        }
        public string ToString()
        {
            return InnerWrappedObject.ToString();
        }
	}
}
