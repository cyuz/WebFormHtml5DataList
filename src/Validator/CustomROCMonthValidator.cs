using System;
using WebFormHtml5DataList.Validation;

namespace WebFormHtml5DataList.Validator
{
    public class CustomROCMonthValidator : CustomDependepntValidator
    {
        public string _formatErrorMsg;

        /// <summary>
        ///  不符合格式時的錯誤訊息
        /// </summary>
        public string FormatErrorMsg
        {
            get
            {
                return _formatErrorMsg;
            }
            set
            {
                _formatErrorMsg = value;
            }
        }

        protected override object SaveViewState()
        {
            // Save State as a cumulative array of objects.
            object baseState = base.SaveViewState();

            object[] allStates = new object[2];
            allStates[0] = baseState;
            allStates[1] = _formatErrorMsg;
            return allStates;
        }

        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                // Load State from the array of objects that was saved during SavedViewState.
                object[] myState = (object[])savedState;
                if (myState[0] != null)
                {
                    base.LoadViewState(myState[0]);
                }

                if (myState[1] != null)
                {
                    _formatErrorMsg = (string)myState[1];
                }
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
                    if (!ValidationHelper.ValdiateROCYearMonthFormat(controlValue))
                    {
                        if (string.IsNullOrWhiteSpace(FormatErrorMsg))
                        {
                            this.ErrorMessage = "請輸入YYYMM格式之年月";
                        }
                        else
                        {
                            this.ErrorMessage = FormatErrorMsg;
                        }

                        return false;
                    }
                }
            }

            return true;
        }
    }
}
