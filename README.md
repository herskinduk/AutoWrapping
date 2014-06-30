AutoWrapping
============

Purpose
-------

Making Sitecore easier to test by providing the missing abstrations.

Usage
-----

1. Copy the AutoWrapping.tt file into your Sitecore Visual Studio project
2. Insert the relative path to Sitecore.Kernel and Lucene.Net dlls where it says "replace with realtive path"
3. Add references to System.Comfiguration, Sitecore.Kernel and Lucene.Net in your project
4. Runt T4 template (right click .tt file and chose "Run Custom Tool")
 
Enjoy

Example
-------

AutoWrapping.tt generates a file called AutoWrapping.cs containing interfaces and wrapper classes. There are two types of wrappers:

- Static class wrappers
- Type without appropriate interface

Here is a code example of how you could use the interfaces and wrappers:

    using Sitecore;
    using Sitecore.Diagnostics;

    namespace Example
    {
        // BAD
        public class HardDependencies
        {
            public void LogMessage(string message)
            {
                Log.Info(message, this);
            }

            public string ReadContextItemName()
            {
                return Sitecore.Context.Item.Name;
            }
        }

        // GOOD
        public class InjectedDependencies
        {
            private readonly ILog _log;
            private readonly IContext _context; 

            public InjectedDependencies()
                : this(new LogWrapper(), new ContextWrapper())
            { }

            public InjectedDependencies(ILog log, IContext context)
            {
                _log = log;
                _context = context;
            }

            public void LogMessage(string message)
            {
                _log.Info(message, this);
            }

            public string ReadContextItemName()
            {
                return _context.Item.Name; // Note item here is an IItem
            }
        }
    }

So obviously in the example above we have to write more code to use ILog and LogWrapper, but we get the option of replacing ILog in test scenarios. The sort types you may wish to do this for could be Sitecore.Context, Item and Database.

Disclaimer
----------

This is very much an experimental tool and have had no testing.
