using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_3
{
    static class ParseTesting
    {
        public static void Parsing(string cost, CultureInfo culture)
        {
            decimal result;
            if (decimal.TryParse(cost, NumberStyles.Currency, culture, out result))
            {
                Console.WriteLine("Parsed {0} into {1} using the {2} culture.", cost, result, culture.DisplayName);
            }
            else
            {
                Console.WriteLine("Could not parse {0} into a decimal using the {1} culture", cost, culture.DisplayName);
            }
        }

        public static bool RegexZipCodes(string zipCode)
        {
            Match match = Regex.Match(zipCode, @"^[1-9][0-9]{4}$");

            return match.Success;
        }
    }
}