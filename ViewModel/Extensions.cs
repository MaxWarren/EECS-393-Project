using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text.RegularExpressions;

namespace ViewModel
{
    public static class Extensions
    {
        /// <summary>
        /// Converts a Binary to an int
        /// </summary>
        /// <param name="bin">The binary to be converted</param>
        /// <returns>The integer value contained in the given binary</returns>
        public static int ConvertToInt(this Binary bin)
        {
            return (int)bin.ToArray()[2];
        }

        /// <summary>
        /// Converts a TaskState to a Binary
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static Binary ConvertToBinary(this TaskState state)
        {
            byte[] res = new byte[3];
            res[2] = (byte)state;
            return new Binary(res);
        }

        /// <summary>
        /// Converts a TaskType to a Binary
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Binary ConvertToBinary(this TaskType type)
        {
            byte[] res = new byte[3];
            res[2] = (byte)type;
            return new Binary(res);
        }

        /// <summary>
        /// Checks if a string is a nonnegative numeric value
        /// </summary>
        /// <param name="text">The string to check</param>
        /// <returns>True if the string is a nonnegative number, false otherwise</returns>
        public static bool IsNonNumeric(this string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return regex.IsMatch(text);
        }
    }
}
