using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.Helpers
{
    public class StringValidations
    {
        public static string StringCheck(string txt)
        {
            string[] tuhaf = { "ı", "İ", "ý", "ş", "Ş", "Þ", "þ", "ð", "Ð", "ğ", "Ğ" };
            string[] original = { "i", "I", "i", "s", "S", "S", "s", "g", "G", "g", "G" };

            for (int i = 0; i < tuhaf.Length; i++)
            {
                txt = txt.Replace(tuhaf[i], original[i]);
            }

            return txt;

        }
        public static string StringCheckReverse(string txt)
        {
            string[] original = { "Ý", "ý", "ý", "Þ", "þ", "ð", "Ð" };
            string[] tuhaf =    { "İ", "İ", "ı", "Ş", "ş", "ğ", "Ğ" };

            for (int i = 0; i < tuhaf.Length; i++)
            {
                txt = txt.Replace(tuhaf[i], original[i]);
            }

            return txt;

        }
    }
}
