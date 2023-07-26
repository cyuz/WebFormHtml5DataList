using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormHtml5DataList
{
    [ToolboxData("<{0}:HTML5DataListControlExtender runat=server></{0}:HTML5DataListControlExtender>")]
	[ParseChildren(true, "Items")]
	public class HTML5DataListControlExtender : DataBoundControl
    {
		private ListItemCollection items;

		private const string DATALIST_TAG = "datalist";

		protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Unknown;

		protected override string TagName => DATALIST_TAG;

		[DefaultValue(false)]
		[Themeable(false)]
		[Category("Behavior")]
		[Description("HTML5DataListControlExtender_AppendDataBoundItems")]
		public virtual bool AppendDataBoundItems
		{
			get
			{
				object obj = ViewState["AppendDataBoundItems"];
				if (obj != null)
				{
					return (bool)obj;
				}
				return false;
			}
			set
			{
				ViewState["AppendDataBoundItems"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		[DefaultValue("")]
		[Themeable(false)]
		[Category("Data")]
		[Description("HTML5DataListControlExtender_DataValueField")]
		public virtual string DataValueField
		{
			get
			{
				object obj = ViewState["DataValueField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				ViewState["DataValueField"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		[Category("Default")]
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[Description("HTML5DataListControlExtender_Items")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public virtual ListItemCollection Items
		{
			get
			{
				if (items == null)
				{
					items = new ListItemCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)items).TrackViewState();
					}
				}
				return items;
			}
		}

		[DefaultValue("")]
		[Themeable(false)]
		[Category("Behavior")]
		[Description("HTML5DataListControlExtender_TargetControlID")]
		public string TargetControlID
        {
            get
            {
                String s = (String)ViewState["TargetControlID"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["TargetControlID"] = value;
            }
        }

		public HTML5DataListControlExtender()
		{

		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (Page != null)
			{
				Page.VerifyRenderingInServerForm(this);
			}
			base.AddAttributesToRender(writer);
		}

        protected override void PerformSelect()
        {
			/// Disable in design mode
			if (base.DesignMode)
			{
				return;
			}

			// Call OnDataBinding here if bound to a data source using the
			// DataSource property (instead of a DataSourceID), because the
			// databinding statement is evaluated before the call to GetData.       
			if (!IsBoundUsingDataSourceID)
            {
                OnDataBinding(EventArgs.Empty);
            }

            // The GetData method retrieves the DataSourceView object from  
            // the IDataSource associated with the data-bound control.            
            GetData().Select(CreateDataSourceSelectArguments(),
                OnDataSourceViewSelectCallback);

            // The PerformDataBinding method has completed.
            RequiresDataBinding = false;
            MarkAsDataBound();

            // Raise the DataBound event.
            OnDataBound(EventArgs.Empty);
        }

        private void OnDataSourceViewSelectCallback(IEnumerable retrievedData)
        {
			/// Disable in design mode
			if (base.DesignMode)
			{
				return;
			}

			// Call OnDataBinding only if it has not already been 
			// called in the PerformSelect method.
			if (IsBoundUsingDataSourceID)
            {
                OnDataBinding(EventArgs.Empty);
            }
            // The PerformDataBinding method binds the data in the  
            // retrievedData collection to elements of the data-bound control.
            PerformDataBinding(retrievedData);
        }

        protected override void PerformDataBinding(IEnumerable dataSource)
		{
			/// Disable in design mode
			if (base.DesignMode)
			{
				return;
			}

			base.PerformDataBinding(dataSource);
			if (dataSource != null)
			{
				string dataValueField = DataValueField;
				if (!AppendDataBoundItems)
				{
					this.Items.Clear();
				}
				if (dataSource is ICollection collection)
				{
					this.Items.Capacity = collection.Count + Items.Count;
				}
				foreach (object item in dataSource)
				{
					ListItem listItem = new ListItem();
					listItem.Value = DataBinder.GetPropertyValue(item, dataValueField, null);
					this.Items.Add(listItem);
				}
			}
		}

		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)Items).TrackViewState();
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			ListItemCollection listItemCollection = this.Items;
			int count = listItemCollection.Count;
			if (count <= 0)
			{
				return;
			}

			// Make sure the control is declared in a form tag 
			// with runat=server.
			if (Page != null)
			{
				Page.VerifyRenderingInServerForm(this);
			}

			writer.WriteLine();

			for (int i = 0; i < count; i++)
			{
				ListItem listItem = listItemCollection[i];
				writer.WriteBeginTag("option");
				if (!listItem.Enabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
				}
				writer.WriteAttribute("value", listItem.Value, fEncode: true);
				if (listItem.Attributes.Count > 0)
				{
					listItem.Attributes.Render(writer);
				}
				if (Page != null)
				{
					Page.ClientScript.RegisterForEventValidation(UniqueID, listItem.Value);
				}
				writer.Write('>');
				writer.WriteEndTag("option");
				writer.WriteLine();
			}
		}

		private string BuildScript()
		{
			if (string.IsNullOrEmpty(this.TargetControlID))
			{
				throw new Exception("Please specify TargetControlID");
			}

			Control targetControl = this.NamingContainer.FindControl(TargetControlID);
			if (targetControl is null)
			{
				throw new Exception($"{this.ID} depened on no exists control - {TargetControlID}");
			}

			string script = $@"
let targetElement{targetControl.ClientID} = document.querySelector('#{targetControl.ClientID}');
targetElement{targetControl.ClientID}.setAttribute('list', '{this.ClientID}');
";

			return script;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			string script = BuildScript();
			if (String.IsNullOrEmpty(script))
			{
				return;
			}

			ScriptManager.RegisterStartupScript(this.Page, Type.GetType("System.String"), this.ClientID + "_mask", script, true);
		}
	}
}
