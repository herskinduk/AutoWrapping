/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

	namespace Sitecore.Data.Items
	{
		public interface IItem : Sitecore.Abstraction.IAutoWrappedObject<Sitecore.Data.Items.Item>
		{
			// Instance Properties
								Sitecore.Security.AccessControl.ItemAccess Access { get;   }
								Sitecore.Data.Items.ItemAppearance Appearance { get;   }
								Sitecore.Data.Items.ItemAxes Axes { get;   }
								Sitecore.Data.Items.BranchItem Branch { get;   }
								Sitecore.Data.IID BranchId { get; set;  }
								Sitecore.Data.Items.BranchItem[] Branches { get;   }
								IEnumerable<Sitecore.Data.Items.IItem> Children { get;   }
								Sitecore.Data.IDatabase Database { get;   }
								string DisplayName { get;   }
								Sitecore.Data.Items.ItemEditing Editing { get;   }
								bool Empty { get;   }
								IEnumerable<Sitecore.Data.Fields.IField> Fields { get;   }
								bool HasChildren { get;   }
								bool HasClones { get;   }
								Sitecore.Data.Items.ItemHelp Help { get;   }
								Sitecore.Data.IID ID { get;   }
								Sitecore.Data.ItemData InnerData { get;   }
								bool IsClone { get;   }
								bool IsEditing { get;   }
								string Key { get;   }
								Sitecore.Globalization.Language Language { get;   }
								Sitecore.Globalization.Language[] Languages { get;   }
								Sitecore.Links.ItemLinks Links { get;   }
								Sitecore.Data.Locking.ItemLocking Locking { get;   }
								Sitecore.Data.Items.BranchItem Master { get;   }
								Sitecore.Data.IID MasterID { get; set;  }
								Sitecore.Data.Items.BranchItem[] Masters { get;   }
								bool Modified { get;   }
								string Name { get; set;  }
								Sitecore.Data.IID OriginatorId { get;   }
								Sitecore.Data.Items.IItem Parent { get;   }
								Sitecore.Data.IID ParentID { get;   }
								Sitecore.Data.ItemPath Paths { get;   }
								Sitecore.Data.Items.ItemPublishing Publishing { get;   }
								Sitecore.Data.Items.ItemRuntimeSettings RuntimeSettings { get;   }
								Sitecore.Security.AccessControl.ItemSecurity Security { get; set;  }
								Sitecore.Data.Items.IItem Source { get;   }
								Sitecore.Data.ItemUri SourceUri { get;   }
								Sitecore.Data.Items.ItemState State { get;   }
								Sitecore.Data.Items.ItemStatistics Statistics { get;   }
								object SyncRoot { get;   }
								Sitecore.Data.Items.TemplateItem Template { get;   }
								Sitecore.Data.IID TemplateID { get; set;  }
								string TemplateName { get;   }
								Sitecore.Data.ItemUri Uri { get;   }
								Sitecore.Data.Version Version { get;   }
								Sitecore.Data.Items.ItemVersions Versions { get;   }
								Sitecore.Data.Items.ItemVisualization Visualization { get;   }
								bool IsItemClone { get;   }
								Sitecore.Data.Items.IItem SharedFieldsSource { get;   }
			
					// Instance Methods
			Sitecore.Data.Items.IItem PasteItem(string xml, bool changeIDs, Sitecore.Data.Items.PasteMode mode);
        System.Guid Recycle();
        int RecycleChildren();
        System.Guid RecycleVersion();
        void Reload();
        string GetUniqueId();
        Sitecore.Data.Items.IItem Add(string name, Sitecore.Data.Items.BranchItem branch);
        Sitecore.Data.Items.IItem Add(string name, Sitecore.Data.BranchId branchId);
        Sitecore.Data.Items.IItem Add(string name, Sitecore.Data.Items.TemplateItem template);
        Sitecore.Data.Items.IItem Add(string name, Sitecore.Data.TemplateID templateID);
        void BeginEdit();
        void ChangeTemplate(Sitecore.Data.Items.TemplateItem template);
        Sitecore.Data.Items.IItem Clone(Sitecore.Data.Items.IItem item);
        Sitecore.Data.Items.IItem Clone(Sitecore.Data.IID cloneID, Sitecore.Data.IDatabase ownerDatabase);
        Sitecore.Data.Items.IItem CloneTo(Sitecore.Data.Items.IItem destination);
        Sitecore.Data.Items.IItem CloneTo(Sitecore.Data.Items.IItem destination, bool deep);
        Sitecore.Data.Items.IItem CloneTo(Sitecore.Data.Items.IItem destination, string name, bool deep);
        Sitecore.Data.Items.IItem CopyTo(Sitecore.Data.Items.IItem destination, string copyName);
        Sitecore.Data.Items.IItem CopyTo(Sitecore.Data.Items.IItem destination, string copyName, Sitecore.Data.IID copyID, bool deep);
        void Delete();
        int DeleteChildren();
        Sitecore.Data.Items.IItem Duplicate();
        Sitecore.Data.Items.IItem Duplicate(string copyName);
        bool EndEdit();
        IEnumerable<Sitecore.Data.Items.IItem> GetChildren();
        IEnumerable<Sitecore.Data.Items.IItem> GetChildren(Sitecore.Collections.ChildListOptions options);
        IEnumerable<Sitecore.Data.Items.IItem> GetClones();
        IEnumerable<Sitecore.Data.Items.IItem> GetClones(bool processChildren);
        string GetOuterXml(bool includeSubitems);
        string GetOuterXml(Sitecore.Data.Items.ItemSerializerOptions options);
        void MoveTo(Sitecore.Data.Items.IItem destination);
        void Paste(string xml, bool changeIDs, Sitecore.Data.Items.PasteMode mode);
		}
	}
