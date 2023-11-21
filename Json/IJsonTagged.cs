using System;
using System.Collections.ObjectModel;

namespace ArkReplay.Json
{
    /// <summary>
    /// A secure, safe way to store enumerables with data in JSON files.
    /// <desc>
    /// This stores enumerated classes in an externally tagged format.
    /// </desc>
    /// </summary>
    public interface IJsonTagged
    {
        /// <summary>
        /// Sets the inner tagged value.
        /// </summary>
        /// <param name="value">The new value.</param>
        public void SetTagged(object value);

        /// <summary>
        /// Gets the inner tagged value.
        /// </summary>
        /// <returns>The inner value.</returns>
        public object GetTagged();
    }
}