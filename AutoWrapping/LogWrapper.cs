/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Diagnostics
{
	public class LogWrapper : ILog
	{
		public LogWrapper()
		{
		}

		// Static Properties
		public bool Enabled
        {
            get { return Sitecore.Diagnostics.Log.Enabled; }
        }

        public bool IsDebugEnabled
        {
            get { return Sitecore.Diagnostics.Log.IsDebugEnabled; }
        }

        public Sitecore.Caching.Cache Singles
        {
            get { return Sitecore.Diagnostics.Log.Singles; }
        }


				// Static Methods
		public void Audit(string message, object owner)
        {
            Sitecore.Diagnostics.Log.Audit(message, owner);
        }
        public void Audit(string message, System.Type ownerType)
        {
            Sitecore.Diagnostics.Log.Audit(message, ownerType);
        }
        public void Audit(System.Type ownerType, string format, string[] parameters)
        {
            Sitecore.Diagnostics.Log.Audit(ownerType, format, parameters);
        }
        public void Audit(object owner, string format, string[] parameters)
        {
            Sitecore.Diagnostics.Log.Audit(owner, format, parameters);
        }
        public void Debug(string message, object owner)
        {
            Sitecore.Diagnostics.Log.Debug(message, owner);
        }
        public void Debug(string message)
        {
            Sitecore.Diagnostics.Log.Debug(message);
        }
        public void Error(string message, object owner)
        {
            Sitecore.Diagnostics.Log.Error(message, owner);
        }
        public void Error(string message, System.Type ownerType)
        {
            Sitecore.Diagnostics.Log.Error(message, ownerType);
        }
        public void Error(string message, System.Exception exception, object owner)
        {
            Sitecore.Diagnostics.Log.Error(message, exception, owner);
        }
        public void Error(string message, System.Exception exception, System.Type ownerType)
        {
            Sitecore.Diagnostics.Log.Error(message, exception, ownerType);
        }
        public void Fatal(string message, object owner)
        {
            Sitecore.Diagnostics.Log.Fatal(message, owner);
        }
        public void Fatal(string message, System.Type ownerType)
        {
            Sitecore.Diagnostics.Log.Fatal(message, ownerType);
        }
        public void Fatal(string message, System.Exception exception, object owner)
        {
            Sitecore.Diagnostics.Log.Fatal(message, exception, owner);
        }
        public void Fatal(string message, System.Exception exception, System.Type ownerType)
        {
            Sitecore.Diagnostics.Log.Fatal(message, exception, ownerType);
        }
        public void Info(string message, object owner)
        {
            Sitecore.Diagnostics.Log.Info(message, owner);
        }
        public void SingleError(string message, object owner)
        {
            Sitecore.Diagnostics.Log.SingleError(message, owner);
        }
        public void SingleFatal(string message, System.Exception exception, object owner)
        {
            Sitecore.Diagnostics.Log.SingleFatal(message, exception, owner);
        }
        public void SingleFatal(string message, System.Exception exception, System.Type ownerType)
        {
            Sitecore.Diagnostics.Log.SingleFatal(message, exception, ownerType);
        }
        public void SingleWarn(string message, object owner)
        {
            Sitecore.Diagnostics.Log.SingleWarn(message, owner);
        }
        public void Warn(string message, object owner)
        {
            Sitecore.Diagnostics.Log.Warn(message, owner);
        }
        public void Warn(string message, System.Exception exception, object owner)
        {
            Sitecore.Diagnostics.Log.Warn(message, exception, owner);
        }
	}
}
