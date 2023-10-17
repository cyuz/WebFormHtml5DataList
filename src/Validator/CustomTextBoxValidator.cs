using System;
using WebFormHtml5DataList.Validation;

namespace WebFormHtml5DataList.Validator
{
    public class CustomTextBoxValidator : CustomDependepntValidator
    {
        /// <summary>
        ///  最大可輸入長度
        /// </summary>
        public int MaxLength
        {
            get
            {
                int? v = (int?)ViewState["MaxLength"];
                return v.HasValue ? v.Value : 0;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("長度不可<0");
                }
                ViewState["MaxLength"] = value;
            }
        }

        /// <summary>
        ///   是否可輸入中文
        /// </summary>
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

        protected override bool CheckCustomLogic()
        {
            // Get the name of the control to validate.
            string controlToValidate = this.ControlToValidate;

            if (string.IsNullOrWhiteSpace(controlToValidate))
            {
                throw new Exception(this.ID + "一定要指定controlToValidate");
            }

            if (controlToValidate.Length > 0)
            {
                // Get the control's value.
                string controlValue = GetControlValidationValue(controlToValidate);

                if (string.IsNullOrWhiteSpace(controlValue))
                {
                    if (this.ValidateEmptyText)
                    {
                        this.ErrorMessage = "不可為空";
                        return false;
                    }
                }
                else
                {

                    if (MaxLength > 0)
                    {
                        if (controlValue.Trim().Length > MaxLength)
                        {
                            this.ErrorMessage = $"最多限{MaxLength}字";
                            return false;
                        }

                        if (!UTF8)
                        {
                            if(!ValidationHelper.ValidateCharFormat(controlValue.Trim(), MaxLength))
                            {
                                this.ErrorMessage = $"請輸入{MaxLength}碼英數字";
                                return false;
                            }
                        }
                    }

                    if (ValidationHelper.IsInjectionDelimiter(controlValue))
                    {
                        this.ErrorMessage = "不可含有 <>'!?% 等特殊字元";
                        return false;
                    }
                }
            }

            return true;
        }
    }
}