using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebFormHtml5DataList.Validation
{
    static class ValidationHelper
    {
        /// <summary>
        ///   驗證小數
        /// </summary>
        /// <param name="paramInt">整數部份長度</param>
        /// <param name="paramDecimal">小數部份長度</param>
        /// <param name="paramValue"></param>
        /// <param name="allowNegative">允許負數</param>
        /// <returns></returns>
        public static bool ValidateDecimal(int paramInt, int paramDecimal, string paramValue, bool allowNegative = false)
        {
            if(!decimal.TryParse(paramValue, out _))
            {
                return false;
            }

            string RegularExpressions = string.Empty;

            if (allowNegative)
            {
                RegularExpressions = @"^-?[0-9]{1," + paramInt + "}(\\.[0-9]{0," + paramDecimal + "})?$";
            }
            else
            {
                RegularExpressions = @"^[0-9]{1," + paramInt + "}(\\.[0-9]{0," + paramDecimal + "})?$";
            }

            Match m = Regex.Match(paramValue, RegularExpressions);

            return m.Success;
        }

        /// <summary>
        ///   驗證民國年月(yyyMM)格式
        /// </summary>
        /// <param name="s年月"></param>
        /// <returns></returns>
        public static bool ValdiateROCYearMonthFormat(string s年月)
        {
            return Regex.IsMatch(s年月, "^\\d{1,3}(0[1-9]|1[012])$");
        }

        /// <summary>
        ///   驗證英數欄位
        /// </summary>
        /// <param name="欄位"></param>
        /// <param name="欄位長度"></param>
        /// <returns></returns>
        public static bool ValidateCharFormat(string 欄位, int 欄位長度)
        {
            string RegularExpressions = @"[a-zA-Z0-9]{" + 欄位長度 + "}";
            Match m = Regex.Match(欄位, RegularExpressions);

            return m.Success;
        }

        /// <summary>
        /// 判斷是否含會發生Injection的符號
        /// </summary>
        /// <param name="InputString">輸入字串</param>
        /// <returns>為會特定符號則回傳true，其它則回傳false</returns>
        public static bool IsInjectionDelimiter(string InputString)
        {
            return Regex.IsMatch(InputString, "[<>'!?%#&+]");
        }

        /// <summary>
        ///  輸出的民國日期時間格式
        /// </summary>
        private const string ROCDateFormat = "yyy/MM/dd";

        public static bool ValidateROCDateFormat(string sampleDate)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("zh-TW");
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();

            return DateTime.TryParseExact(sampleDate, ROCDateFormat, culture, DateTimeStyles.None, out DateTime nothing);
        }
    }
}
