using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormHtml5DataList.MaskInput
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ROCDateTextBoxControl runat=server></{0}:ROCDateTextBoxControl>")]
    public class ROCDateTextBoxControl : MaskTextBoxControl
    {
        public ROCDateTextBoxControl()
        {
            base.Format = "099/09/09";
            base.Width = 60;
            base.PlaceHolder = "___/__/__";
        }

        [DefaultValue("099/09/09")]
        public override string Format
        {
            set { base.Format = value; }
            get { return base.Format; }
        }

        [DefaultValue(60)]
        public override Unit Width
        {
            set { base.Width = value; }
            get { return base.Width; }
        }

        [DefaultValue("___/__/__")]
        public override string PlaceHolder
        {
            set { base.PlaceHolder = value; }
            get { return base.PlaceHolder; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
    }
}
