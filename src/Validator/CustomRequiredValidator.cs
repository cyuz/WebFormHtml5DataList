using System;
using System.Web.UI.WebControls;

namespace WebFormHtml5DataList.Validator
{
    public class CustomRequiredValidator : BaseValidator
    {
        public event ServerValidateEventHandler ServerValidate;

        protected override bool EvaluateIsValid()
        {
            // Get the name of the control to validate.
            string controlToValidate = this.ControlToValidate;

            if (string.IsNullOrWhiteSpace(controlToValidate))
            {
                throw new Exception(this.ID + "一定要指定controlToValidate");
            }

            string controlValue = string.Empty;

            if (controlToValidate.Length > 0)
            {
                // Get the control's value.
                controlValue = GetControlValidationValue(controlToValidate);

                if (string.IsNullOrWhiteSpace(controlValue))
                {
                    if(string.IsNullOrWhiteSpace(this.ErrorMessage))
                    {
                        this.ErrorMessage = "不可為空";
                    }
                    return false;
                }
            }

            return this.OnServerValidate(controlValue);
        }

        protected virtual bool OnServerValidate(string value)
        {
            ServerValidateEventArgs serverValidateEventArgs = new ServerValidateEventArgs(value, true);
            if (ServerValidate != null)
            {
                ServerValidate(this, serverValidateEventArgs);
                return serverValidateEventArgs.IsValid;
            }
            return true;
        }
    }
}
