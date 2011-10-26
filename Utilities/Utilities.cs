using System.Text.RegularExpressions;

namespace Utilities
{
    public static class Utility
    {
        public static bool IsTextNumeric(string text)
        {
            Regex regex = new Regex("[^0-9]+"); 
            return regex.IsMatch(text);
        }
    }
}
