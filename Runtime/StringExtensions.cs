using System;
using System.Text.RegularExpressions;

namespace MiddleMast
{
    public static class StringExtensions
    {
        private static Regex DoubleSpaceRegex
        {
            get
            {
                if (_doubleSpaceRegex == null)
                {
                    const RegexOptions options = RegexOptions.None;
                    _doubleSpaceRegex = new Regex("[ ]{2,}", options);
                }

                return _doubleSpaceRegex;
            }
        }

        private static Regex _doubleSpaceRegex;

        public static int CountLines(this string s)
        {
            return s.Split('\n').Length;
        }

        /// <summary>
        ///     Removes duplicate spaces from a string. PROBABLY GENERATES ALLOCATIONS
        /// </summary>
        public static string RemoveDoubleSpaces(this string s)
        {
            return DoubleSpaceRegex.Replace(s, " ");
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
}
