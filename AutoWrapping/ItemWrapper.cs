/// ## AUTO GENERATED DO NOT ALTER ## ///
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Data.Items
{
	public class ItemWrapper : IItem
	{
		public ItemWrapper(Sitecore.Data.Items.Item innerObject)
		{
			InnerWrappedObject = innerObject;
		}

		public Sitecore.Data.Items.Item InnerWrappedObject{ get; private set;}

		// Instance Properties
		public Sitecore.Security.AccessControl.ItemAccess Access
        {
            get { return InnerWrappedObject.Access; }
        }

        public Sitecore.Data.Items.ItemAppearance Appearance
        {
            get { return InnerWrappedObject.Appearance; }
        }

        public Sitecore.Data.Items.ItemAxes Axes
        {
            get { return InnerWrappedObject.Axes; }
        }

        public Sitecore.Data.Items.BranchItem Branch
        {
            get { return InnerWrappedObject.Branch; }
        }

        public Sitecore.Data.IID BranchId
        {
            get { return new Sitecore.Data.IDWrapper(InnerWrappedObject.BranchId); }
            set { InnerWrappedObject.BranchId = InnerWrappedObject.BranchId; }
        }

        public Sitecore.Data.Items.BranchItem[] Branches
        {
            get { return InnerWrappedObject.Branches; }
        }

        public IEnumerable<Sitecore.Data.Items.IItem> Children
        {
            get { return InnerWrappedObject.Children.Select(x => new Sitecore.Data.Items.ItemWrapper(x)).AsEnumerable(); }
        }

        public Sitecore.Data.IDatabase Database
        {
            get { return new Sitecore.Data.DatabaseWrapper(InnerWrappedObject.Database); }
        }

        public string DisplayName
        {
            get { return InnerWrappedObject.DisplayName; }
        }

        public Sitecore.Data.Items.ItemEditing Editing
        {
            get { return InnerWrappedObject.Editing; }
        }

        public bool Empty
        {
            get { return InnerWrappedObject.Empty; }
        }

        public IEnumerable<Sitecore.Data.Fields.IField> Fields
        {
            get { return InnerWrappedObject.Fields.Select(x => new Sitecore.Data.Fields.FieldWrapper(x)).AsEnumerable(); }
        }

        public bool HasChildren
        {
            get { return InnerWrappedObject.HasChildren; }
        }

        public bool HasClones
        {
            get { return InnerWrappedObject.HasClones; }
        }

        public Sitecore.Data.Items.ItemHelp Help
        {
            get { return InnerWrappedObject.Help; }
        }

        public Sitecore.Data.IID ID
        {
            get { return new Sitecore.Data.IDWrapper(InnerWrappedObject.ID); }
        }

        public Sitecore.Data.ItemData InnerData
        {
            get { return InnerWrappedObject.InnerData; }
        }

        public bool IsClone
        {
            get { return InnerWrappedObject.IsClone; }
        }

        public bool IsEditing
        {
            get { return InnerWrappedObject.IsEditing; }
        }

        public string Key
        {
            get { return InnerWrappedObject.Key; }
        }

        public Sitecore.Globalization.Language Language
        {
            get { return InnerWrappedObject.Language; }
        }

        public Sitecore.Globalization.Language[] Languages
        {
            get { return InnerWrappedObject.Languages; }
        }

        public Sitecore.Links.ItemLinks Links
        {
            get { return InnerWrappedObject.Links; }
        }

        public Sitecore.Data.Locking.ItemLocking Locking
        {
            get { return InnerWrappedObject.Locking; }
        }

        public Sitecore.Data.Items.BranchItem Master
        {
            get { return InnerWrappedObject.Master; }
        }

        public Sitecore.Data.IID MasterID
        {
            get { return new Sitecore.Data.IDWrapper(InnerWrappedObject.MasterID); }
            set { InnerWrappedObject.MasterID = InnerWrappedObject.MasterID; }
        }

        public Sitecore.Data.Items.BranchItem[] Masters
        {
            get { return InnerWrappedObject.Masters; }
        }

        public bool Modified
        {
            get { return InnerWrappedObject.Modified; }
        }

        public string Name
        {
            get { return InnerWrappedObject.Name; }
            set { InnerWrappedObject.Name = value; }
        }

        public Sitecore.Data.IID OriginatorId
        {
            get { return new Sitecore.Data.IDWrapper(InnerWrappedObject.OriginatorId); }
        }

        public Sitecore.Data.Items.IItem Parent
        {
            get { return new Sitecore.Data.Items.ItemWrapper(InnerWrappedObject.Parent); }
        }

        public Sitecore.Data.IID ParentID
        {
            get { return new Sitecore.Data.IDWrapper(InnerWrappedObject.ParentID); }
        }

        public Sitecore.Data.ItemPath Paths
        {
            get { return InnerWrappedObject.Paths; }
        }

        public Sitecore.Data.Items.ItemPublishing Publishing
        {
            get { return InnerWrappedObject.Publishing; }
        }

        public Sitecore.Data.Items.ItemRuntimeSettings RuntimeSettings
        {
            get { return InnerWrappedObject.RuntimeSettings; }
        }

        public Sitecore.Security.AccessControl.ItemSecurity Security
        {
            get { return InnerWrappedObject.Security; }
            set { InnerWrappedObject.Security = value; }
        }

        public Sitecore.Data.Items.IItem Source
        {
            get { return new Sitecore.Data.Items.ItemWrapper(InnerWrappedObject.Source); }
        }

        public Sitecore.Data.ItemUri SourceUri
        {
            get { return InnerWrappedObject.SourceUri; }
        }

        public Sitecore.Data.Items.ItemState State
        {
            get { return InnerWrappedObject.State; }
        }

        public Sitecore.Data.Items.ItemStatistics Statistics
        {
            get { return InnerWrappedObject.Statistics; }
        }

        public object SyncRoot
        {
            get { return InnerWrappedObject.SyncRoot; }
        }

        public Sitecore.Data.Items.TemplateItem Template
        {
            get { return InnerWrappedObject.Template; }
        }

        public Sitecore.Data.IID TemplateID
        {
            get { return new Sitecore.Data.IDWrapper(InnerWrappedObject.TemplateID); }
            set { InnerWrappedObject.TemplateID = InnerWrappedObject.TemplateID; }
        }

        public string TemplateName
        {
            get { return InnerWrappedObject.TemplateName; }
        }

        public Sitecore.Data.ItemUri Uri
        {
            get { return InnerWrappedObject.Uri; }
        }

        public Sitecore.Data.Version Version
        {
            get { return InnerWrappedObject.Version; }
        }

        public Sitecore.Data.Items.ItemVersions Versions
        {
            get { return InnerWrappedObject.Versions; }
        }

        public Sitecore.Data.Items.ItemVisualization Visualization
        {
            get { return InnerWrappedObject.Visualization; }
        }

        public bool IsItemClone
        {
            get { return InnerWrappedObject.IsItemClone; }
        }

        public Sitecore.Data.Items.IItem SharedFieldsSource
        {
            get { return new Sitecore.Data.Items.ItemWrapper(InnerWrappedObject.SharedFieldsSource); }
        }

        
        		// Instance Methods
        		public Sitecore.Data.Items.IItem PasteItem(string xml, bool changeIDs, Sitecore.Data.Items.PasteMode mode)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.PasteItem(xml, changeIDs, mode));
        }
        public System.Guid Recycle()
        {
            return InnerWrappedObject.Recycle();
        }
        public int RecycleChildren()
        {
            return InnerWrappedObject.RecycleChildren();
        }
        public System.Guid RecycleVersion()
        {
            return InnerWrappedObject.RecycleVersion();
        }
        public void Reload()
        {
            InnerWrappedObject.Reload();
        }
        public string GetUniqueId()
        {
            return InnerWrappedObject.GetUniqueId();
        }
        public Sitecore.Data.Items.IItem Add(string name, Sitecore.Data.Items.BranchItem branch)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.Add(name, branch));
        }
        public Sitecore.Data.Items.IItem Add(string name, Sitecore.Data.BranchId branchId)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.Add(name, branchId));
        }
        public Sitecore.Data.Items.IItem Add(string name, Sitecore.Data.Items.TemplateItem template)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.Add(name, template));
        }
        public Sitecore.Data.Items.IItem Add(string name, Sitecore.Data.TemplateID templateID)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.Add(name, templateID));
        }
        public void BeginEdit()
        {
            InnerWrappedObject.BeginEdit();
        }
        public void ChangeTemplate(Sitecore.Data.Items.TemplateItem template)
        {
            InnerWrappedObject.ChangeTemplate(template);
        }
        public Sitecore.Data.Items.IItem Clone(Sitecore.Data.Items.IItem item)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.Clone((Sitecore.Data.Items.Item)item.InnerWrappedObject));
        }
        public Sitecore.Data.Items.IItem Clone(Sitecore.Data.IID cloneID, Sitecore.Data.IDatabase ownerDatabase)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.Clone((Sitecore.Data.ID)cloneID.InnerWrappedObject, (Sitecore.Data.Database)ownerDatabase.InnerWrappedObject));
        }
        public Sitecore.Data.Items.IItem CloneTo(Sitecore.Data.Items.IItem destination)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.CloneTo((Sitecore.Data.Items.Item)destination.InnerWrappedObject));
        }
        public Sitecore.Data.Items.IItem CloneTo(Sitecore.Data.Items.IItem destination, bool deep)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.CloneTo((Sitecore.Data.Items.Item)destination.InnerWrappedObject, deep));
        }
        public Sitecore.Data.Items.IItem CloneTo(Sitecore.Data.Items.IItem destination, string name, bool deep)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.CloneTo((Sitecore.Data.Items.Item)destination.InnerWrappedObject, name, deep));
        }
        public Sitecore.Data.Items.IItem CopyTo(Sitecore.Data.Items.IItem destination, string copyName)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.CopyTo((Sitecore.Data.Items.Item)destination.InnerWrappedObject, copyName));
        }
        public Sitecore.Data.Items.IItem CopyTo(Sitecore.Data.Items.IItem destination, string copyName, Sitecore.Data.IID copyID, bool deep)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.CopyTo((Sitecore.Data.Items.Item)destination.InnerWrappedObject, copyName, (Sitecore.Data.ID)copyID.InnerWrappedObject, deep));
        }
        public void Delete()
        {
            InnerWrappedObject.Delete();
        }
        public int DeleteChildren()
        {
            return InnerWrappedObject.DeleteChildren();
        }
        public Sitecore.Data.Items.IItem Duplicate()
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.Duplicate());
        }
        public Sitecore.Data.Items.IItem Duplicate(string copyName)
        {
            return new Sitecore.Data.Items.ItemWrapper (InnerWrappedObject.Duplicate(copyName));
        }
        public bool EndEdit()
        {
            return InnerWrappedObject.EndEdit();
        }
        public IEnumerable<Sitecore.Data.Items.IItem> GetChildren()
        {
            return InnerWrappedObject.GetChildren().Select(x => new Sitecore.Data.Items.ItemWrapper(x)).AsEnumerable();
        }
        public IEnumerable<Sitecore.Data.Items.IItem> GetChildren(Sitecore.Collections.ChildListOptions options)
        {
            return InnerWrappedObject.GetChildren(options).Select(x => new Sitecore.Data.Items.ItemWrapper(x)).AsEnumerable();
        }
        public IEnumerable<Sitecore.Data.Items.IItem> GetClones()
        {
            return InnerWrappedObject.GetClones().Select(x => new Sitecore.Data.Items.ItemWrapper(x)).AsEnumerable();
        }
        public IEnumerable<Sitecore.Data.Items.IItem> GetClones(bool processChildren)
        {
            return InnerWrappedObject.GetClones(processChildren).Select(x => new Sitecore.Data.Items.ItemWrapper(x)).AsEnumerable();
        }
        public string GetOuterXml(bool includeSubitems)
        {
            return InnerWrappedObject.GetOuterXml(includeSubitems);
        }
        public string GetOuterXml(Sitecore.Data.Items.ItemSerializerOptions options)
        {
            return InnerWrappedObject.GetOuterXml(options);
        }
        public void MoveTo(Sitecore.Data.Items.IItem destination)
        {
            InnerWrappedObject.MoveTo((Sitecore.Data.Items.Item)destination.InnerWrappedObject);
        }
        public void Paste(string xml, bool changeIDs, Sitecore.Data.Items.PasteMode mode)
        {
            InnerWrappedObject.Paste(xml, changeIDs, mode);
        }
	}
}
