// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataReaderExtensions.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines extension methods for data readers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec
{
    /// <summary>
    /// Defines extension methods for data readers.
    /// </summary>
    public static class DataReaderExtensions
    {
        /// <summary>
        /// Create a dynamic object from the values contained within a
        /// data reader.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <returns>
        /// The dynamic object.
        /// </returns>
        public static dynamic ToDynamic(this IDataReader reader)
        {
            var values = new Dictionary<string, object>();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
                var value = reader[i];

                values.Add(name, value);
            }

            return new CaseInsensitiveExpandoObject(values);
        }
    }
}
