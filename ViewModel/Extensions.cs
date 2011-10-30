using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
