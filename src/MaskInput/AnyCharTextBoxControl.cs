using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace WebFormHtml5DataList.MaskInput
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:AnyCharTextBoxControl runat=server></{0}:AnyCharTextBoxControl>")]
    public class AnyCharTextBoxControl : MaskTextBoxControl
    {
        private const int CHAR_TEXTWIDTH = 10;
        private const int UTF8_TEXTWIDTH = 14;
        public override int MaxLength
        {
            get
            {
                return base.MaxLength;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("MaxLength不可<0");
                }
                base.MaxLength = value;
            }
        }

        /// <summary>
        ///   是否可輸入中文
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Themeable(false)]
        public bool UTF8
        {
            get
            {
                bool? b = (bool?)ViewState["UTF8"];
                return b.HasValue ? b.Value : true;
            }

            set
            {
                ViewState["UTF8"] = value;
            }
        }

        /// <summary>
        ///   非UTF8是否可輸入空白，預設為false
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Themeable(false)]
        public bool AllowSpace
        {
            get
            {
                bool? b = (bool?)ViewState["AllowSpace"];
                return b.HasValue ? b.Value : false;
            }

            set
            {
                ViewState["AllowSpace"] = value;
            }
        }

        protected void UpdateFormat()
        {
            if(UTF8)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < MaxLength; i++)
            {
                sb.Append("A");
            }

            this.Format = sb.ToString();

            if(AllowSpace)
            {
                this.Translation = "'A': {pattern: /[A-Za-z0-9 ]/, optional: true}";
            }
        }

        protected void UpdateWidth()
        {
            if(this.MaxLength > 0)
            {
                this.Width = MaxLength * (UTF8 ? UTF8_TEXTWIDTH: CHAR_TEXTWIDTH);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if(string.IsNullOrEmpty(this.Format))
            {
                UpdateFormat();
            }

            if(this.MaxLength > 0 && this.Width.IsEmpty)
            {
                UpdateWidth();
            }

            base.OnPreRender(e);
        }
    }
}