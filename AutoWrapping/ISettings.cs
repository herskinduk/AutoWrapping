/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Configuration
{
	public interface ISettings
	{
		// Static Properties
		string AccountNameValidation{get;}
bool AliasesActive{get;}
bool AllowLogoutOfAllUsers{get;}
bool AppendQSToUrlRendering{get;}
bool AutoMaintainProxyDefinitions{get;}
bool AutomaticDataBind{get;}
bool AutomaticLockOnSave{get;}
bool AutomaticUnlockOnSaved{get;}
bool CheckSecurityOnLanguages{get;}
string ClientLanguage{get;}
System.Web.Configuration.CustomErrorsMode CustomErrorsMode{get;}
bool ConfigurationIsSet{get;}
string DataFolder{get;}
string DebugBorderColor{get;}
string DebugBorderTag{get;}
bool DebugEnabled{get;}
string DebugFolder{get;}
string DefaultBaseTemplate{get;}
string DefaultDesktop{get;}
string DefaultIcon{get;}
string DefaultItem{get;}
string DefaultLanguage{get;}
string DefaultLayoutFile{get;}
string DefaultPageName{get;}
string DefaultPublishingTargets{get;}
string DefaultRegionalIsoCode{get;}
string DefaultShellControl{get;}
int DefaultSortOrder{get;}
System.TimeSpan DefaultSQLTimeout{get;}
string DefaultTheme{get;}
string DefaultThumbnail{get;}
bool DumpClientPageData{get;}
bool DisableBrowserCaching{get;}
string EmailValidation{get;}
bool EnableEventQueues{get;}
bool EnableSiteConfigFiles{get;}
bool EnableXslDocumentFunction{get;}
bool EnableXslScripts{get;}
string ErrorPage{get;}
bool FastPublishing{get;}
bool FastQueryDescendantsDisabled{get;}
bool GenerateThumbnails{get;}
int GridPageSize{get;}
Sitecore.SecurityModel.Cryptography.HashEncryption.EncryptionProvider HashEncryptionProvider{get;}
System.TimeSpan HealthMonitorInterval{get;}
System.TimeSpan HeartbeatInterval{get;}
string[] IgnoreUrlPrefixes{get;}
string ImageTypes{get;}
bool IncludeProxiesInLinkDatabase{get;}
string IndexFolder{get;}
char[] InvalidItemNameChars{get;}
string ItemNameValidation{get;}
string ItemNotFoundUrl{get;}
bool KeepLockAfterSaveForAdminUsers{get;}
string LayoutFolder{get;}
string LayoutNotFoundUrl{get;}
string LayoutPageEvent{get;}
string LicenseFile{get;}
string LinkItemNotFoundUrl{get;}
string LogFolder{get;}
string LoginLayout{get;}
string LoginPage{get;}
string MailServer{get;}
string MailServerPassword{get;}
int MailServerPort{get;}
string MailServerUserName{get;}
string MasterVariablesReplacer{get;}
int MaxCallLevel{get;}
int MaxItemNameLength{get;}
int MaxSqlBatchStatements{get;}
int MaxTreeDepth{get;}
int MaxWorkerThreads{get;}
string MediaFolder{get;}
string NoAccessUrl{get;}
string NoLicenseUrl{get;}
string PackagePath{get;}
string PageStateStore{get;}
string PortalPrincipalResolver{get;}
string PortalStorage{get;}
string ProfileItemDatabase{get;}
int ProcessHistoryCount{get;}
bool ProtectedSite{get;}
bool RecycleBinActive{get;}
int RamBufferSize{get;}
int MaxFacets{get;}
int MaxDocumentBufferSize{get;}
int IndexMergeFactor{get;}
System.Collections.Generic.IEnumerable<string> RedirectUrlPrefixes{get;}
bool RequireLockBeforeEditing{get;}
string SerializationFolder{get;}
string SerializationPassword{get;}
string InstanceName{get;}
System.Collections.Generic.List<Sitecore.Web.SiteInfo> Sites{get;}
string SystemBlockedUrl{get;}
string TempFolderPath{get;}
int ThumbnailHeight{get;}
string ThumbnailsBackgroundColor{get;}
int ThumbnailWidth{get;}
bool UnlockAfterCopy{get;}
string VersionFilePath{get;}
string ViewStateStore{get;}
string Wallpaper{get;}
string WallpapersPath{get;}
string WebStylesheet{get;}
string WelcomeTitle{get;}
string XHtmlSchemaFile{get;}
string XmlControlAspxFile{get;}
string XmlControlExtension{get;}

		// Static Methods
		bool ConnectionStringExists(string connectionStringName);
        string GetAppSetting(string name);
        string GetAppSetting(string name, string defaultValue);
        bool GetBoolSetting(string name, bool defaultValue);
        string GetConnectionString(string connectionStringName);
        double GetDoubleSetting(string name, double defaultValue);
        string GetFileSetting(string name);
        string GetFileSetting(string name, string defaultValue);
        int GetIntSetting(string name, int defaultValue);
        long GetLongSetting(string name, long defaultValue);
        int GetNumberOfSettings();
        object GetProviderObject(string implementationPath, System.Type expectedType);
        object GetProviderObject(string referencePath, string implementationPath, System.Type expectedType);
        object[] GetProviderObjects(string referencePath, string implementationPath, System.Type expectedType);
        string GetSetting(string name);
        string GetSetting(string name, string defaultValue);
        T GetSystemSection<T>(string sectionName) where T: class;
        System.TimeSpan GetTimeSpanSetting(string name, string defaultValue);
        System.TimeSpan GetTimeSpanSetting(string name, System.TimeSpan defaultValue);
        System.TimeSpan GetTimeSpanSetting(string name, System.TimeSpan defaultValue, System.Globalization.CultureInfo cultureInfo);
        void Reset();
        bool WriteSetting(string key, string value, bool overwrite);
        bool WriteSetting(string path, string match, string content, bool overwrite);
			
	}
}
