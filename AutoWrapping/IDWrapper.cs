/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Data
{
	public class IDWrapper : IID
	{
		public IDWrapper(Sitecore.Data.ID innerObject)
		{
			InnerWrappedObject = innerObject;
		}

		public Sitecore.Data.ID InnerWrappedObject{ get; private set;}

		// Instance Properties
		public System.Guid Guid
        {
            get { return InnerWrappedObject.Guid; }
        }

        public bool IsGlobalNullId
        {
            get { return InnerWrappedObject.IsGlobalNullId; }
        }

        public bool IsNull
        {
            get { return InnerWrappedObject.IsNull; }
        }

        
        		// Instance Methods
        		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            InnerWrappedObject.GetObjectData(info, context);
        }
        public long GetDataLength()
        {
            return InnerWrappedObject.GetDataLength();
        }
        public bool Equals(object obj)
        {
            return InnerWrappedObject.Equals(obj);
        }
        public int GetHashCode()
        {
            return InnerWrappedObject.GetHashCode();
        }
        public System.Guid ToGuid()
        {
            return InnerWrappedObject.ToGuid();
        }
        public Sitecore.Data.ShortID ToShortID()
        {
            return InnerWrappedObject.ToShortID();
        }
        public string ToString()
        {
            return InnerWrappedObject.ToString();
        }
	}
}
