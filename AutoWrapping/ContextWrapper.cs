/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

namespace Sitecore
{
	public class ContextWrapper : IContext
	{
		public ContextWrapper()
		{
		}

		// Static Properties
		public Sitecore.Access.AccessContext Access
        {
            get { return Sitecore.Context.Access; }
        }

        public Sitecore.Configuration.ClientDataStore ClientData
        {
            get { return Sitecore.Context.ClientData; }
        }

        public Sitecore.Web.UI.Sheer.ClientPage ClientPage
        {
            get { return Sitecore.Context.ClientPage; }
            set { Sitecore.Context.ClientPage = value; }
        }

        public Sitecore.Data.IDatabase ContentDatabase
        {
            get { return new Sitecore.Data.DatabaseWrapper(Sitecore.Context.ContentDatabase); }
        }

        public Sitecore.Globalization.Language ContentLanguage
        {
            get { return Sitecore.Context.ContentLanguage; }
        }

        public System.Globalization.CultureInfo Culture
        {
            get { return Sitecore.Context.Culture; }
        }

        public Sitecore.Context.ContextData Data
        {
            get { return Sitecore.Context.Data; }
        }

        public Sitecore.Data.IDatabase Database
        {
            get { return new Sitecore.Data.DatabaseWrapper(Sitecore.Context.Database); }
            set { Sitecore.Context.Database = ((Sitecore.Data.Database)value.InnerWrappedObject); }
        }

        public Sitecore.Data.Items.DeviceItem Device
        {
            get { return Sitecore.Context.Device; }
            set { Sitecore.Context.Device = value; }
        }

        public Sitecore.Diagnostics.DiagnosticContext Diagnostics
        {
            get { return Sitecore.Context.Diagnostics; }
        }

        public Sitecore.Security.Domains.Domain Domain
        {
            get { return Sitecore.Context.Domain; }
        }

        public bool IsAdministrator
        {
            get { return Sitecore.Context.IsAdministrator; }
        }

        public bool IsBackgroundThread
        {
            get { return Sitecore.Context.IsBackgroundThread; }
        }

        public bool IsLoggedIn
        {
            get { return Sitecore.Context.IsLoggedIn; }
        }

        public bool IsUnitTesting
        {
            get { return Sitecore.Context.IsUnitTesting; }
            set { Sitecore.Context.IsUnitTesting = value; }
        }

        public bool SkipSecurityInUnitTests
        {
            get { return Sitecore.Context.SkipSecurityInUnitTests; }
            set { Sitecore.Context.SkipSecurityInUnitTests = value; }
        }

        public Sitecore.Data.Items.IItem Item
        {
            get { return new Sitecore.Data.Items.ItemWrapper(Sitecore.Context.Item); }
            set { Sitecore.Context.Item = ((Sitecore.Data.Items.Item)value.InnerWrappedObject); }
        }

        public Sitecore.Caching.ItemsContext Items
        {
            get { return Sitecore.Context.Items; }
        }

        public Sitecore.Jobs.Job Job
        {
            get { return Sitecore.Context.Job; }
            set { Sitecore.Context.Job = value; }
        }

        public Sitecore.Globalization.Language Language
        {
            get { return Sitecore.Context.Language; }
            set { Sitecore.Context.Language = value; }
        }

        public Sitecore.Events.NotificationContext Notifications
        {
            get { return Sitecore.Context.Notifications; }
        }

        public Sitecore.Layouts.PageContext Page
        {
            get { return Sitecore.Context.Page; }
        }

        public bool ProxiesActive
        {
            get { return Sitecore.Context.ProxiesActive; }
            set { Sitecore.Context.ProxiesActive = value; }
        }

        public string RawUrl
        {
            get { return Sitecore.Context.RawUrl; }
        }

        public Sitecore.Sites.SiteRequest Request
        {
            get { return Sitecore.Context.Request; }
        }

        public string RequestID
        {
            get { return Sitecore.Context.RequestID; }
        }

        public Sitecore.Resources.ResourceContext Resources
        {
            get { return Sitecore.Context.Resources; }
        }

        public Sitecore.SecurityModel.SecurityContext Security
        {
            get { return Sitecore.Context.Security; }
        }

        public Sitecore.Sites.SiteContext Site
        {
            get { return Sitecore.Context.Site; }
            set { Sitecore.Context.Site = value; }
        }

        public Sitecore.Configuration.StateContext State
        {
            get { return Sitecore.Context.State; }
        }

        public Sitecore.Tasks.TaskContext Task
        {
            get { return Sitecore.Context.Task; }
        }

        public Sitecore.Security.Accounts.User User
        {
            get { return Sitecore.Context.User; }
        }

        public Sitecore.Workflows.WorkflowContext Workflow
        {
            get { return Sitecore.Context.Workflow; }
        }


				// Static Methods
		public string GetDeviceName()
        {
            return Sitecore.Context.GetDeviceName();
        }
        public string GetSiteName()
        {
            return Sitecore.Context.GetSiteName();
        }
        public System.Collections.Generic.Stack<Sitecore.Tasks.TaskContext> GetTaskStack()
        {
            return Sitecore.Context.GetTaskStack();
        }
        public string GetUserName()
        {
            return Sitecore.Context.GetUserName();
        }
        public void Logout()
        {
            Sitecore.Context.Logout();
        }
        public void SetActiveSite(string siteName)
        {
            Sitecore.Context.SetActiveSite(siteName);
        }
        public void SetLanguage(Sitecore.Globalization.Language language, bool persistent)
        {
            Sitecore.Context.SetLanguage(language, persistent);
        }
        public void SetTraceBuffer(System.Text.StringBuilder buffer)
        {
            Sitecore.Context.SetTraceBuffer(buffer);
        }
        public void Trace(string message, object[] parameters)
        {
            Sitecore.Context.Trace(message, parameters);
        }
	}
}
