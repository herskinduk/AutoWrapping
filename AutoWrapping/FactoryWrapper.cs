/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Configuration
{
	public class FactoryWrapper : IFactory
	{
		public FactoryWrapper()
		{
		}

		// Static Properties
		
				// Static Methods
		public Sitecore.Web.UI.WebControls.ErrorControl CreateErrorControl(string message, string details)
        {
            return Sitecore.Configuration.Factory.CreateErrorControl(message, details);
        }
        public Sitecore.Xml.XPath.ItemNavigator CreateItemNavigator(Sitecore.Data.Items.IItem item)
        {
            return Sitecore.Configuration.Factory.CreateItemNavigator((Sitecore.Data.Items.Item)item.InnerWrappedObject);
        }
        public T CreateObject<T>(System.Xml.XmlNode configNode) where T: class
        {
            return Sitecore.Configuration.Factory.CreateObject<T>(configNode);
        }
        public Sitecore.Configuration.ConfigStore GetConfigStore(string configStoreName)
        {
            return Sitecore.Configuration.Factory.GetConfigStore(configStoreName);
        }
        public System.Collections.Generic.List<Sitecore.Web.CustomHandler> GetCustomHandlers()
        {
            return Sitecore.Configuration.Factory.GetCustomHandlers();
        }
        public Sitecore.Data.IDatabase GetDatabase(string name)
        {
            return new Sitecore.Data.DatabaseWrapper (Sitecore.Configuration.Factory.GetDatabase(name));
        }
        public Sitecore.Data.IDatabase GetDatabase(string name, bool assert)
        {
            return new Sitecore.Data.DatabaseWrapper (Sitecore.Configuration.Factory.GetDatabase(name, assert));
        }
        public string[] GetDatabaseNames()
        {
            return Sitecore.Configuration.Factory.GetDatabaseNames();
        }
        public IEnumerable<Sitecore.Data.IDatabase> GetDatabases()
        {
            return Sitecore.Configuration.Factory.GetDatabases().Select(x => new Sitecore.Data.DatabaseWrapper(x)).AsEnumerable();
        }
        public Sitecore.Security.Domains.Domain GetDomain(string name)
        {
            return Sitecore.Configuration.Factory.GetDomain(name);
        }
        public Sitecore.Collections.StringDictionary GetDomainMap(string path)
        {
            return Sitecore.Configuration.Factory.GetDomainMap(path);
        }
        public string[] GetDomainNames()
        {
            return Sitecore.Configuration.Factory.GetDomainNames();
        }
        public System.Collections.Hashtable GetHashtable(string path, Sitecore.Configuration.Factory.HashKeyType keyType, Sitecore.Configuration.Factory.HashValueType valueType, Sitecore.Configuration.Factory.HashValueFormat format, System.Type dataType)
        {
            return Sitecore.Configuration.Factory.GetHashtable(path, keyType, valueType, format, dataType);
        }
        public Sitecore.Data.IDTables.IDTableProvider GetIDTable()
        {
            return Sitecore.Configuration.Factory.GetIDTable();
        }
        public Sitecore.Data.Indexing.Index GetIndex(string name)
        {
            return Sitecore.Configuration.Factory.GetIndex(name);
        }
        public System.Collections.Generic.IComparer<Sitecore.Data.Items.Item> GetItemComparer(Sitecore.Data.Items.IItem item)
        {
            return Sitecore.Configuration.Factory.GetItemComparer((Sitecore.Data.Items.Item)item.InnerWrappedObject);
        }
        public Sitecore.Links.LinkDatabase GetLinkDatabase()
        {
            return Sitecore.Configuration.Factory.GetLinkDatabase();
        }
        public Sitecore.Data.MasterVariablesReplacer GetMasterVariablesReplacer()
        {
            return Sitecore.Configuration.Factory.GetMasterVariablesReplacer();
        }
        public Sitecore.Collections.PerformanceCounterCollection GetPerformanceCounters()
        {
            return Sitecore.Configuration.Factory.GetPerformanceCounters();
        }
        public TCollection GetProviders<TProvider, TCollection>(string rootPath, out TProvider defaultProvider) where TProvider: System.Configuration.Provider.ProviderBase where TCollection: System.Configuration.Provider.ProviderCollection, new()
        {
            return Sitecore.Configuration.Factory.GetProviders<TProvider, TCollection>(rootPath, out defaultProvider);
        }
        public Sitecore.Data.DataProviders.IRetryable GetRetryer()
        {
            return Sitecore.Configuration.Factory.GetRetryer();
        }
        public Sitecore.Text.Replacer GetReplacer(string name)
        {
            return Sitecore.Configuration.Factory.GetReplacer(name);
        }
        public Sitecore.Sites.SiteContext GetSite(string siteName)
        {
            return Sitecore.Configuration.Factory.GetSite(siteName);
        }
        public Sitecore.Web.SiteInfo GetSiteInfo(string siteName)
        {
            return Sitecore.Configuration.Factory.GetSiteInfo(siteName);
        }
        public System.Collections.Generic.List<Sitecore.Web.SiteInfo> GetSiteInfoList()
        {
            return Sitecore.Configuration.Factory.GetSiteInfoList();
        }
        public string[] GetSiteNames()
        {
            return Sitecore.Configuration.Factory.GetSiteNames();
        }
        public System.Collections.Generic.List<Sitecore.Sites.SiteContext> GetSites()
        {
            return Sitecore.Configuration.Factory.GetSites();
        }
        public System.Collections.Generic.List<Sitecore.Web.SiteInfo> GetSitesInfo()
        {
            return Sitecore.Configuration.Factory.GetSitesInfo();
        }
        public string GetString(string configPath, bool assert)
        {
            return Sitecore.Configuration.Factory.GetString(configPath, assert);
        }
        public Sitecore.Collections.Set<string> GetStringSet(string configPath)
        {
            return Sitecore.Configuration.Factory.GetStringSet(configPath);
        }
        public Sitecore.Tasks.TaskDatabase GetArchiveTaskDatabase()
        {
            return Sitecore.Configuration.Factory.GetArchiveTaskDatabase();
        }
        public Sitecore.Tasks.TaskDatabase GetTaskDatabase()
        {
            return Sitecore.Configuration.Factory.GetTaskDatabase();
        }
        public object CreateObject(string configPath, bool assert)
        {
            return Sitecore.Configuration.Factory.CreateObject(configPath, assert);
        }
        public object CreateObject(string configPath, string[] parameters, bool assert)
        {
            return Sitecore.Configuration.Factory.CreateObject(configPath, parameters, assert);
        }
        public object CreateObject(System.Xml.XmlNode configNode, bool assert)
        {
            return Sitecore.Configuration.Factory.CreateObject(configNode, assert);
        }
        public object CreateObject(System.Xml.XmlNode configNode, string[] parameters, bool assert)
        {
            return Sitecore.Configuration.Factory.CreateObject(configNode, parameters, assert);
        }
        public object CreateObject(System.Xml.XmlNode configNode, string[] parameters, bool assert, Sitecore.Configuration.IFactoryHelper helper)
        {
            return Sitecore.Configuration.Factory.CreateObject(configNode, parameters, assert, helper);
        }
        public System.Type CreateType(System.Xml.XmlNode configNode, bool assert)
        {
            return Sitecore.Configuration.Factory.CreateType(configNode, assert);
        }
        public System.Type CreateType(System.Xml.XmlNode configNode, string[] parameters, bool assert)
        {
            return Sitecore.Configuration.Factory.CreateType(configNode, parameters, assert);
        }
        public System.Type FindType(string className, System.Reflection.Assembly assembly)
        {
            return Sitecore.Configuration.Factory.FindType(className, assembly);
        }
        public string GetAttribute(string name, System.Xml.XmlNode node, string[] parameters)
        {
            return Sitecore.Configuration.Factory.GetAttribute(name, node, parameters);
        }
        public System.Xml.XmlNode GetConfigNode(string xpath)
        {
            return Sitecore.Configuration.Factory.GetConfigNode(xpath);
        }
        public System.Xml.XmlNode GetConfigNode(string xpath, bool assert)
        {
            return Sitecore.Configuration.Factory.GetConfigNode(xpath, assert);
        }
        public System.Xml.XmlNodeList GetConfigNodes(string xpath)
        {
            return Sitecore.Configuration.Factory.GetConfigNodes(xpath);
        }
        public void Reset()
        {
            Sitecore.Configuration.Factory.Reset();
        }
        public System.Xml.XmlDocument GetConfiguration()
        {
            return Sitecore.Configuration.Factory.GetConfiguration();
        }
	}
}
