using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormHtml5DataList.MaskInput
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:UpperTextBoxControl runat=server></{0}:UpperTextBoxControl>")]
    public class UpperTextBoxControl : TextBox
    {
        /// <summary>
        ///   Auto ToUpperCase
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Themeable(false)]
        public bool AutoToUpperCase
        {
            get
            {
                bool? b = (bool?)ViewState["AutoToUpperCase"];
                return b.HasValue ? b.Value : true;
            }

            set
            {
                ViewState["AutoToUpperCase"] = value;
            }
        }

        /// <summary>
        ///   Auto Trim
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Themeable(false)]
        public bool AutoTrim
        {
            get
            {
                bool? b = (bool?)ViewState["AutoTrim"];
                return b.HasValue ? b.Value : true;
            }

            set
            {
                ViewState["AutoTrim"] = value;
            }
        }

        public override string Text
        {
            get
            {
                string str = base.Text;

                if (AutoToUpperCase)
                {
                    str = str?.ToUpper();
                }

                if(AutoTrim)
                {
                    str = str?.Trim();
                }

                return str;
            }
            set
            {
                base.Text = value;
            }
        }
    }
}