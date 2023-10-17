using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Example
{
    public partial class Validator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CV1_CV_ServerValidate(object source, ServerValidateEventArgs args)
        {
            CustomValidator cv = (CustomValidator)source;

            int val = 0;

            if(!int.TryParse(args.Value, out val))
            {
                args.IsValid = false;
                cv.ErrorMessage = "Not integer format";
                return;
            }

            if(val > 50)
            {
                args.IsValid = false;
                cv.ErrorMessage = "value must less than 50";
                return;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(!this.IsValid)
            {
                return;
            }
        }
    }
}