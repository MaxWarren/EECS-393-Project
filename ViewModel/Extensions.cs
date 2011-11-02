using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace ViewModel
{
    public static class Extensions
    {
        /// <summary>
        /// Lookup a key in a dictionary given its value
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary</typeparam>
        /// <param name="dict">The dictionary itself</param>
        /// <param name="value">The value for which to lookup the key</param>
        /// <returns>The key corresponding to the given value, or the default value</returns>
        public static TKey GetKeyByValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TValue value)
        {
            return dict.SingleOrDefault(x => x.Value.Equals(value)).Key;
        }

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
    }
}
