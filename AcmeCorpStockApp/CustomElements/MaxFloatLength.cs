using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace AcmeCorpStockApp.CustomElements
{
    public class MaxFloatLength : ValidationAttribute
    {
        public int MaxLength { get; set; }
        public override bool IsValid(object value)
        {
            string stringVal = Convert.ToString(value);

            bool result = IsLong(stringVal);

            //this statement checks weather comming value is int or float. Because if 
            //it is float we only needs to check int part length. So I just substring 
            //the integer part from float value if value is float

            string intValue = result == false && stringVal.Count() > 0 ?
                stringVal.Substring(0, stringVal.IndexOf('.'))
                : stringVal;
            if (intValue.Length > MaxLength)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool IsLong(string value)
        {
            long val;
            bool result = long.TryParse(value, out val);
            return result;
        }
    }

}
