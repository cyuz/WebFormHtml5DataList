using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace WebFormHtml5DataList.MaskInput
{
    /// <summary>
    /// textbox with mask
    /// must import jquery.mask first[https://igorescobar.github.io/jQuery-Mask-Plugin/]
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:MaskTextBoxControl runat=server></{0}:MaskTextBoxControl>")]
    public class MaskTextBoxControl : UpperTextBoxControl
    {
        /// <summary>
        ///   MaskFormat
        /// </summary>
        [DefaultValue("")]
        [Themeable(false)]
        public virtual string Format
        {
            get
            {
                String s = (String)ViewState["Format"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Format"] = value;
            }
        }

        /// <summary>
        ///   PlaceHolder
        /// </summary>
        [DefaultValue("")]
        [Themeable(false)]
        public virtual string PlaceHolder
        {
            get
            {
                String s = (String)ViewState["PlaceHolder"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["PlaceHolder"] = value;
            }
        }

        /// <summary>
        ///   Reverse
        /// </summary>
        [DefaultValue(false)]
        [Themeable(false)]
        public virtual bool Reverse
        {
            get
            {
                bool? b = (bool?)ViewState["Reverse"];
                return b.HasValue ? b.Value : false;
            }

            set
            {
                ViewState["Reverse"] = value;
            }
        }

        /// <summary>
        ///   Translation
        /// </summary>
        [DefaultValue("")]
        [Themeable(false)]
        public virtual string Translation
        {
            get
            {
                String s = (String)ViewState["Translation"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Translation"] = value;
            }
        }

        /// <summary>
        ///   ClearIfNotMatch
        /// </summary>
        [DefaultValue(false)]
        [Themeable(false)]
        public virtual bool ClearIfNotMatch
        {
            get
            {
                bool? b = (bool?)ViewState["ClearIfNotMatch"];
                return b.HasValue ? b.Value : false;
            }

            set
            {
                ViewState["ClearIfNotMatch"] = value;
            }
        }

        private bool AppendPlaceHolderOption(StringBuilder sb, bool appended)
        {
            if (string.IsNullOrEmpty(PlaceHolder))
            {
                return false;
            }

            if (appended)
            {
                sb.Append(", ");
            }

            sb.AppendFormat("placeholder: '{0}'", PlaceHolder);

            return true;
        }

        private bool AppendReverseOption(StringBuilder sb, bool appended)
        {
            if (!Reverse)
            {
                return false;
            }

            if (appended)
            {
                sb.Append(", ");
            }

            sb.AppendFormat("reverse: {0}", Reverse.ToString().ToLower());

            return true;
        }

        private bool AppendTranslationOption(StringBuilder sb, bool appended)
        {
            if (string.IsNullOrEmpty(Translation))
            {
                return false;
            }

            if (appended)
            {
                sb.Append(", ");
            }

            /// escape {}
            sb.AppendFormat("translation: {{ {0} }}", Translation);

            return true;
        }

        private bool AppendClearIfNotMatch(StringBuilder sb, bool appended)
        {
            if (!ClearIfNotMatch)
            {
                return false;
            }

            if (appended)
            {
                sb.Append(", ");
            }

            sb.AppendFormat("clearIfNotMatch: {0}", ClearIfNotMatch.ToString().ToLower());

            return true;
        }

        private bool AppendScriptOption(StringBuilder sb)
        {
            /// start Index of option
            int startIndex = sb.Length;

            bool appended = false;
            appended |= AppendPlaceHolderOption(sb, appended);
            appended |= AppendReverseOption(sb, appended);
            appended |= AppendTranslationOption(sb, appended);
            appended |= AppendClearIfNotMatch(sb, appended);

            if (appended)
            {
                sb.Insert(startIndex, ", {");
                sb.Append("}");
            }

            return true;
        }

        private string BuildScript()
        {
            if (string.IsNullOrEmpty(Format))
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("$('#{0}').mask('{1}'", this.ClientID, Format);

            AppendScriptOption(sb);

            sb.AppendFormat(");");

            return sb.ToString();
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
