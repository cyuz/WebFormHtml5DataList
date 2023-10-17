using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Example
{
    public partial class CusutomGridViewEncode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Val");

            DataRow dr = dt.NewRow();
            dr["Val"] = "123<br/>456";
            dt.Rows.Add(dr);

            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
    }
}