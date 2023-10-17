using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormHtml5DataList.GridView
{
    /// <summary>
    ///  因為BoundField標題要換行，要使用htmlEcode=False，這會造成DataField也沒有Encode
    ///  checkmarx會回報Reflected_XSS_All_Clients
    ///  懶得把BoundField拆成TemplateField/HeaderTemplate
    ///  所以寫一個Header可以單獨控制編碼(HeaderHtmlEncode)的BoundField
    /// </summary>

    public class CustomHeaderEncodeBoundField : BoundField
    {
        private bool _htmlEncode;

        private bool _htmlEncodeSet;

        private bool _suppressHtmlEncodeFieldChange;

        /// <summary>
        ///   HeaderHtmlEncode
        /// </summary>
        [DefaultValue(true)]
        [Themeable(false)]
        public virtual bool HeaderHtmlEncode
        {
            get
            {
                bool? b = (bool?)ViewState["HeaderHtmlEncode"];
                return b.HasValue ? b.Value : true;
            }

            set
            {
                ViewState["HeaderHtmlEncode"] = value;
                OnFieldChanged();
            }
        }

        public override bool HtmlEncode
        {
            get
            {
                if (!_htmlEncodeSet)
                {
                    object obj = ViewState["HtmlEncode"];
                    if (obj != null)
                    {
                        _htmlEncode = (bool)obj;
                    }
                    else
                    {
                        _htmlEncode = true;
                    }
                    _htmlEncodeSet = true;
                }
                return _htmlEncode;
            }
            set
            {
                object obj = ViewState["HtmlEncode"];
                if (obj == null || (bool)obj != value)
                {
                    ViewState["HtmlEncode"] = value;
                    _htmlEncode = value;
                    _htmlEncodeSet = true;
                    if(!_suppressHtmlEncodeFieldChange)
                    {
                        OnFieldChanged();
                    }
                }
            }
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            bool htmlEncodeOverwrited = false;

            if(cellType == DataControlCellType.Header && HtmlEncode && !HeaderHtmlEncode)
            {
                /// 因為要透過程式修改, 停止觸發FieldChange
                _suppressHtmlEncodeFieldChange = true;
                /// HeaderHtmlEncode=false, header停用htmlEncode
                HtmlEncode = false;
                htmlEncodeOverwrited = true;
            }

            base.InitializeCell(cell, cellType, rowState, rowIndex);

            /// 改回來
            if (htmlEncodeOverwrited)
            {
                HtmlEncode = true;
                _suppressHtmlEncodeFieldChange = false;
            }
        }

        protected override void CopyProperties(DataControlField newField)
        {
            ((CustomHeaderEncodeBoundField)newField).HeaderHtmlEncode = HeaderHtmlEncode;
            base.CopyProperties(newField);
        }
    }
}