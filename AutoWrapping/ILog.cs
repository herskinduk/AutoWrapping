/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Diagnostics
{
	public interface ILog
	{
		// Static Properties
		bool Enabled{get;}
bool IsDebugEnabled{get;}
Sitecore.Caching.Cache Singles{get;}

		// Static Methods
		void Audit(string message, object owner);
        void Audit(string message, System.Type ownerType);
        void Audit(System.Type ownerType, string format, string[] parameters);
        void Audit(object owner, string format, string[] parameters);
        void Debug(string message, object owner);
        void Debug(string message);
        void Error(string message, object owner);
        void Error(string message, System.Type ownerType);
        void Error(string message, System.Exception exception, object owner);
        void Error(string message, System.Exception exception, System.Type ownerType);
        void Fatal(string message, object owner);
        void Fatal(string message, System.Type ownerType);
        void Fatal(string message, System.Exception exception, object owner);
        void Fatal(string message, System.Exception exception, System.Type ownerType);
        void Info(string message, object owner);
        void SingleError(string message, object owner);
        void SingleFatal(string message, System.Exception exception, object owner);
        void SingleFatal(string message, System.Exception exception, System.Type ownerType);
        void SingleWarn(string message, object owner);
        void Warn(string message, object owner);
        void Warn(string message, System.Exception exception, object owner);
			
	}
}
