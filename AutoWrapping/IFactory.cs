/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Configuration
{
	public interface IFactory
	{
		// Static Properties
		
		// Static Methods
		Sitecore.Web.UI.WebControls.ErrorControl CreateErrorControl(string message, string details);
        Sitecore.Xml.XPath.ItemNavigator CreateItemNavigator(Sitecore.Data.Items.IItem item);
        T CreateObject<T>(System.Xml.XmlNode configNode) where T: class;
        Sitecore.Configuration.ConfigStore GetConfigStore(string configStoreName);
        System.Collections.Generic.List<Sitecore.Web.CustomHandler> GetCustomHandlers();
        Sitecore.Data.IDatabase GetDatabase(string name);
        Sitecore.Data.IDatabase GetDatabase(string name, bool assert);
        string[] GetDatabaseNames();
        IEnumerable<Sitecore.Data.IDatabase> GetDatabases();
        Sitecore.Security.Domains.Domain GetDomain(string name);
        Sitecore.Collections.StringDictionary GetDomainMap(string path);
        string[] GetDomainNames();
        System.Collections.Hashtable GetHashtable(string path, Sitecore.Configuration.Factory.HashKeyType keyType, Sitecore.Configuration.Factory.HashValueType valueType, Sitecore.Configuration.Factory.HashValueFormat format, System.Type dataType);
        Sitecore.Data.IDTables.IDTableProvider GetIDTable();
        System.Collections.Generic.IComparer<Sitecore.Data.Items.Item> GetItemComparer(Sitecore.Data.Items.IItem item);
        Sitecore.Links.LinkDatabase GetLinkDatabase();
        Sitecore.Data.MasterVariablesReplacer GetMasterVariablesReplacer();
        Sitecore.Collections.PerformanceCounterCollection GetPerformanceCounters();
        TCollection GetProviders<TProvider, TCollection>(string rootPath, out TProvider defaultProvider) where TProvider: System.Configuration.Provider.ProviderBase where TCollection: System.Configuration.Provider.ProviderCollection, new();
        Sitecore.Data.DataProviders.IRetryable GetRetryer();
        Sitecore.Text.Replacer GetReplacer(string name);
        Sitecore.Sites.SiteContext GetSite(string siteName);
        Sitecore.Web.SiteInfo GetSiteInfo(string siteName);
        System.Collections.Generic.List<Sitecore.Web.SiteInfo> GetSiteInfoList();
        string[] GetSiteNames();
        string GetString(string configPath, bool assert);
        Sitecore.Collections.Set<string> GetStringSet(string configPath);
        Sitecore.Tasks.TaskDatabase GetArchiveTaskDatabase();
        Sitecore.Tasks.TaskDatabase GetTaskDatabase();
        object CreateObject(string configPath, bool assert);
        object CreateObject(string configPath, string[] parameters, bool assert);
        object CreateObject(System.Xml.XmlNode configNode, bool assert);
        object CreateObject(System.Xml.XmlNode configNode, string[] parameters, bool assert);
        object CreateObject(System.Xml.XmlNode configNode, string[] parameters, bool assert, Sitecore.Configuration.IFactoryHelper helper);
        System.Type CreateType(System.Xml.XmlNode configNode, bool assert);
        System.Type CreateType(System.Xml.XmlNode configNode, string[] parameters, bool assert);
        System.Type FindType(string className, System.Reflection.Assembly assembly);
        string GetAttribute(string name, System.Xml.XmlNode node, string[] parameters);
        System.Xml.XmlNode GetConfigNode(string xpath);
        System.Xml.XmlNode GetConfigNode(string xpath, bool assert);
        System.Xml.XmlNodeList GetConfigNodes(string xpath);
        void Reset();
        System.Xml.XmlDocument GetConfiguration();
			
	}
}
