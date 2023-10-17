using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormHtml5DataList.MaskInput
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ROCMonthTextBoxControl runat=server></{0}:ROCMonthTextBoxControl>")]
    public class ROCMonthTextBoxControl : MaskTextBoxControl
    {
        public ROCMonthTextBoxControl()
        {
            base.Format = "00000";
            base.Width = 36;
            base.PlaceHolder = "_____";
        }

        [DefaultValue("00000")]
        public override string Format
        {
            set { base.Format = value; }
            get { return base.Format; }
        }

        [DefaultValue(36)]
        public override Unit Width
        {
            set { base.Width = value; }
            get { return base.Width; }
        }

        [DefaultValue("_____")]
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
