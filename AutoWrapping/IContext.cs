/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

namespace Sitecore
{
	public interface IContext
	{
		// Static Properties
		Sitecore.Access.AccessContext Access{get;}
Sitecore.Configuration.ClientDataStore ClientData{get;}
Sitecore.Web.UI.Sheer.ClientPage ClientPage{get;set;}
Sitecore.Data.IDatabase ContentDatabase{get;}
Sitecore.Globalization.Language ContentLanguage{get;}
System.Globalization.CultureInfo Culture{get;}
Sitecore.Context.ContextData Data{get;}
Sitecore.Data.IDatabase Database{get;set;}
Sitecore.Data.Items.DeviceItem Device{get;set;}
Sitecore.Diagnostics.DiagnosticContext Diagnostics{get;}
Sitecore.Security.Domains.Domain Domain{get;}
bool IsAdministrator{get;}
bool IsBackgroundThread{get;}
bool IsLoggedIn{get;}
bool IsUnitTesting{get;set;}
bool SkipSecurityInUnitTests{get;set;}
Sitecore.Data.Items.IItem Item{get;set;}
Sitecore.Caching.ItemsContext Items{get;}
Sitecore.Jobs.Job Job{get;set;}
Sitecore.Globalization.Language Language{get;set;}
Sitecore.Events.NotificationContext Notifications{get;}
Sitecore.Layouts.PageContext Page{get;}
bool ProxiesActive{get;set;}
string RawUrl{get;}
Sitecore.Sites.SiteRequest Request{get;}
string RequestID{get;}
Sitecore.Resources.ResourceContext Resources{get;}
Sitecore.SecurityModel.SecurityContext Security{get;}
Sitecore.Sites.SiteContext Site{get;set;}
Sitecore.Configuration.StateContext State{get;}
Sitecore.Tasks.TaskContext Task{get;}
Sitecore.Security.Accounts.User User{get;}
Sitecore.Workflows.WorkflowContext Workflow{get;}

		// Static Methods
		string GetDeviceName();
        string GetSiteName();
        System.Collections.Generic.Stack<Sitecore.Tasks.TaskContext> GetTaskStack();
        string GetUserName();
        void Logout();
        void SetActiveSite(string siteName);
        void SetLanguage(Sitecore.Globalization.Language language, bool persistent);
        void SetTraceBuffer(System.Text.StringBuilder buffer);
        void Trace(string message, object[] parameters);
			
	}
}
