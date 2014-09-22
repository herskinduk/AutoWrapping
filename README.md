AutoWrapping
============

Purpose
-------

Making Sitecore easier to test by providing the missing abstractions.

Usage
-----

1. Choose (new/existing) a Visual Studio project where you want the abstractions/wrapper to reside
2. Add references to System.Configuration, System.Web, Sitecore.Kernel and Lucene.Net in your project (AutoWrappingKickstart.tt expects find Sitecore.Kernel in $(ProjectDir)\..\lib)
3. Install AutoWrapping.Sitecore nupkg from this myget (**PRE-RELEASE**) feed: https://www.myget.org/F/autowrapping/ 

Enjoy!

Example
-------

AutoWrappingKickstart.tt generates a file called AutoWrappingKickstart.cs containing interfaces and wrapper classes. There are two types of wrappers:

- Static class wrappers
- Instance type wrappers

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

Troubleshooting
-------

* I can't find the nuget packages.
    * Have you turned on support for pre-release packages?
    * Have you setup the myget feed in Visual Studio?
* The generated code errors
    * Is the path to Sitecore.Kernel is correct in the .tt file?
    * Have you got a project reference Sitecore.Kernel.dll?
* The auto generated code is missing the abstraction I need
    * Have you tried adding it to the type setup code in the .tt file? 

Disclaimer
----------

This is very much an experimental tool and have had little testing.
