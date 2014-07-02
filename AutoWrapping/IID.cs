/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

	namespace Sitecore.Data
	{
		public interface IID : Sitecore.Abstraction.IAutoWrappedObject<Sitecore.Data.ID>
		{
			// Instance Properties
								System.Guid Guid { get;   }
								bool IsGlobalNullId { get;   }
								bool IsNull { get;   }
			
					// Instance Methods
			void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context);
        long GetDataLength();
        bool Equals(object obj);
        int GetHashCode();
        System.Guid ToGuid();
        Sitecore.Data.ShortID ToShortID();
        string ToString();
		}
	}
