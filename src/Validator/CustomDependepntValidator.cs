using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebFormHtml5DataList.Validator
{
    public class CustomDependepntValidator : CustomValidator
    {
        public string DependentValidatorControl
        {
            get;
            set;
        }

        protected override object SaveViewState()
        {
            // Save State as a cumulative array of objects.
            object baseState = base.SaveViewState();

            object[] allStates = new object[2];
            allStates[0] = baseState;
            allStates[1] = DependentValidatorControl;
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
                    DependentValidatorControl = (string)myState[1];
                }
            }
        }

        protected sealed override bool EvaluateIsValid()
        {
            if(string.IsNullOrEmpty(DependentValidatorControl))
            {
                return ContinueEvaluateIsValid();
            }

            string[] dependents = DependentValidatorControl.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
            foreach(string dependent in dependents)
            {
                BaseValidator cv = this.NamingContainer.FindControl(dependent) as BaseValidator;
                if(cv is null)
                {
                    throw new Exception($"{this.ID}依賴於不存在的驗證控制項{dependent}");
                }

                if(!cv.IsValid)
                {
                    this.ErrorMessage = string.Empty;
                    return false;
                }
            }

            return ContinueEvaluateIsValid();
        }

        protected bool ContinueEvaluateIsValid()
        {
            if (!CheckCustomLogic())
            {
                return false;
            }

            return base.EvaluateIsValid();
        }

        /// <summary>
        ///  在繼續ServerValidate之前的驗證
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckCustomLogic()
        {
            return true;
        }
    }
}