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
    }
}
