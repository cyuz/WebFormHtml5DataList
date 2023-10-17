using System;
using WebFormHtml5DataList.Validation;

namespace WebFormHtml5DataList.Validator
{
    public class CustomDecimalValidator : CustomDependepntValidator
    {

        private int _intPart;

        /// <summary>
        ///  整數部份
        /// </summary>
        public int IntPart
        {
            get
            {
                return _intPart;
            }
            set
            {
                _intPart = value;
            }
        }

        private int _decimalPart;

        /// <summary>
        ///   小數點部份
        /// </summary>
        public int DecimalPart
        {
            get
            {
                return _decimalPart;
            }
            set
            {
                _decimalPart = value;
            }
        }

        private bool _allowNegatvie;

        /// <summary>
        ///  是否允許負值
        /// </summary>
        public bool AllowNegatvie
        {
            get
            {
                return _allowNegatvie;
            }
            set
            {
                _allowNegatvie = value;
            }
        }

        private string _formatErrorMsg;

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

            object[] allStates = new object[5];
            allStates[0] = baseState;
            allStates[1] = _intPart;
            allStates[2] = _decimalPart;
            allStates[3] = _formatErrorMsg;
            allStates[4] = _allowNegatvie;
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
                    _intPart = (int)myState[1];
                }

                if (myState[2] != null)
                {
                    _decimalPart = (int)myState[2];
                }

                if (myState[3] != null)
                {
                    _formatErrorMsg = (string)myState[3];
                }

                if (myState[4] != null)
                {
                    _allowNegatvie = (bool)myState[4];
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

            if (IntPart < 0 || DecimalPart < 0)
            {
                throw new Exception(this.ID + "IntPart及DecimalPart不可<0");
            }

            if (IntPart == 0 && DecimalPart == 0)
            {
                throw new Exception("IntPart及DecimalPart至少要一不為0");
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
                    if (!ValidationHelper.ValidateDecimal(this.IntPart, this.DecimalPart, controlValue, this.AllowNegatvie))
                    {
                        if (string.IsNullOrWhiteSpace(FormatErrorMsg))
                        {
                            if (DecimalPart == 0)
                            {
                                this.ErrorMessage = "請輸入整數";
                            }
                            else
                            {
                                if (!this.AllowNegatvie)
                                {
                                    this.ErrorMessage = string.Format("須為正數值(整數最多{0}位,小數{1}位)", IntPart, DecimalPart);
                                }
                                else
                                {
                                    this.ErrorMessage = string.Format("須為數值(整數最多{0}位,小數{1}位)", IntPart, DecimalPart);
                                }
                            }
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
