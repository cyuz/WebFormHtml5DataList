using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Example
{
    public partial class DataListExtender : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("OptionText"));

                DataRow dr = dt.NewRow();
                dr["OptionText"] = "option1";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["OptionText"] = "option2";
                dt.Rows.Add(dr);

                this.HTML5DataListControlExtenderFromDataSource.DataSource = dt;
                this.HTML5DataListControlExtenderFromDataSource.DataValueField = "OptionText";
                this.HTML5DataListControlExtenderFromDataSource.DataBind();

                this.HTML5DataListControlExtenderListItem.Items.Add(new ListItem("option1"));
                this.HTML5DataListControlExtenderListItem.Items.Add(new ListItem("option2"));
            }
        }
    }
}