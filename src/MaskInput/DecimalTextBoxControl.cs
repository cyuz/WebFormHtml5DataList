using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace WebFormHtml5DataList.MaskInput
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:DecimalTextBoxControl runat=server></{0}:DecimalTextBoxControl>")]
    public class DecimalTextBoxControl : MaskTextBoxControl
    {
        private const int TEXTWIDTH = 7;

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
                if (value < 0)
                {
                    throw new Exception("IntPart不可<0");
                }
                _intPart = value;
            }
        }

        public int _decimalPart;

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
                if (value < 0)
                {
                    throw new Exception("DecimalPart不可<0");
                }
                _decimalPart = value;
            }
        }

        private bool _allowNegatvie;

        /// <summary>
        ///   是否允許負值
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

        protected void UpdateFormat()
        {
            StringBuilder sb = new StringBuilder();
            if (AllowNegatvie)
            {
                sb.Append("Z");
            }

            if (_intPart > 0)
            {
                sb.Append("0");
            }

            for (int i = 1; i < _intPart; i++)
            {
                sb.Append("9");
            }

            if (_decimalPart > 0)
            {
                sb.Append(".");
            }

            for (int i = 0; i < _decimalPart; i++)
            {
                sb.Append("9");
            }

            this.Format = sb.ToString();
            this.Translation = "'Z': {pattern: /[\\-]/, optional: true}";
        }

        protected void UpdateWidth()
        {
            int totalLen = _intPart + _decimalPart;
            if (_allowNegatvie)
            {
                totalLen += 1;
            }
            if (_decimalPart > 0)
            {
                totalLen += 1;
            }
            this.Width = totalLen * TEXTWIDTH;          
        }

        protected override object SaveViewState()
        {
            // Save State as a cumulative array of objects.
            object baseState = base.SaveViewState();

            object[] allStates = new object[4];
            allStates[0] = baseState;
            allStates[1] = _intPart;
            allStates[2] = _decimalPart;
            allStates[3] = _allowNegatvie;
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
                    _allowNegatvie = (bool)myState[3];
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (IntPart == 0 && DecimalPart == 0)
            {
                throw new Exception("IntPart及DecimalPart不可皆為0");
            }

            if (string.IsNullOrEmpty(this.Format))
            {
                UpdateFormat();
            }

            if (this.Width.IsEmpty)
            {
                UpdateWidth();
            }

            base.OnPreRender(e);
        }
    }
}
