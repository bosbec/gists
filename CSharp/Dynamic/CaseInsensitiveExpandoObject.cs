// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CaseInsensitiveExpandoObject.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines a case insensitive expando object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec
{
    /// <summary>
    /// Defines a case insensitive expando object.
    /// </summary>
    public class CaseInsensitiveExpandoObject : DynamicObject, IDictionary<string, object>
    {
        /// <summary>
        /// The values.
        /// </summary>
        private readonly IDictionary<string, object> _values = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="CaseInsensitiveExpandoObject"/> class.
        /// </summary>
        public CaseInsensitiveExpandoObject()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CaseInsensitiveExpandoObject"/> class.
        /// </summary>
        /// <param name="values">
        /// The values.
        /// </param>
        public CaseInsensitiveExpandoObject(IEnumerable<KeyValuePair<string, object>> values)
        {
            foreach (var value in values)
            {
                _values.Add(value);
            }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/>
        /// containing the values in the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/>
        /// containing the values in the object that implements
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        ICollection<object> IDictionary<string, object>.Values
        {
            get
            {
                return _values.Values;
            }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/>
        /// containing the keys of the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/>
        /// containing the keys of the object that implements
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        ICollection<string> IDictionary<string, object>.Keys
        {
            get
            {
                return _values.Keys;
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        int ICollection<KeyValuePair<string, object>>.Count
        {
            get
            {
                return _values.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        bool ICollection<KeyValuePair<string, object>>.IsReadOnly
        {
            get
            {
                return _values.IsReadOnly;
            }
        }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <returns>
        /// The element with the specified key.
        /// </returns>
        /// <param name="key">The key of the element to get or set.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
        /// The property is retrieved and <paramref name="key"/> is not found.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The property is set and the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/> is read-only.
        /// </exception>
        object IDictionary<string, object>.this[string key]
        {
            get
            {
                return _values[key];
            }

            set
            {
                _values[key] = value;
            }
        }

        /// <summary>
        /// Provides the implementation for operations that get member values.
        /// Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/>
        /// class can override this method to specify dynamic behavior for
        /// operations such as getting a value for a property.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the operation is successful; otherwise, <c>false</c>.
        /// If this method returns false, the run-time binder of the language
        /// determines the behavior. (In most cases, a run-time exception is thrown.)
        /// </returns>
        /// <param name="binder">
        /// Provides information about the object that called the dynamic operation.
        /// The binder.Name property provides the name of the member on which the
        /// dynamic operation is performed. For example, for the
        /// Console.WriteLine(sampleObject.SampleProperty) statement, where
        /// sampleObject is an instance of the class derived from the
        /// <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Name
        /// returns "SampleProperty". The binder.IgnoreCase property
        /// specifies whether the member name is case-sensitive.
        /// </param>
        /// <param name="result">
        /// The result of the get operation. For example, if the method is
        /// called for a property, you can assign the property value
        /// to <paramref name="result"/>.
        /// </param>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _values.TryGetValue(binder.Name, out result);
        }

        /// <summary>
        /// Provides the implementation for operations that set member values.
        /// Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/>
        /// class can override this method to specify dynamic behavior for
        /// operations such as setting a value for a property.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the operation is successful; otherwise, <c>false</c>.
        /// If this method returns false, the run-time binder of the language
        /// determines the behavior. (In most cases, a language-specific
        /// run-time exception is thrown.)
        /// </returns>
        /// <param name="binder">
        /// Provides information about the object that called the dynamic
        /// operation. The binder.Name property provides the name of the
        /// member to which the value is being assigned. For example, for
        /// the statement sampleObject.SampleProperty = "Test", where
        /// sampleObject is an instance of the class derived from the
        /// <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Name
        /// returns "SampleProperty". The binder.IgnoreCase property specifies
        /// whether the member name is case-sensitive.
        /// </param>
        /// <param name="value">
        /// The value to set to the member. For example, for
        /// sampleObject.SampleProperty = "Test", where sampleObject is
        /// an instance of the class derived from the
        /// <see cref="T:System.Dynamic.DynamicObject"/> class, the
        /// <paramref name="value"/> is "Test".
        /// </param>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _values[binder.Name] = value;

            return true;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/>
        /// that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that
        /// can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_values).GetEnumerator();
        }

        /// <summary>
        /// Adds an item to the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to add to the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/>
        /// is read-only.
        /// </exception>
        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            _values.Add(item);
        }

        /// <summary>
        /// Removes all items from the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/>
        /// is read-only.
        /// </exception>
        void ICollection<KeyValuePair<string, object>>.Clear()
        {
            _values.Clear();
        }

        /// <summary>
        /// Determines whether the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>
        /// contains a specific value.
        /// </summary>
        /// <returns>
        /// <c>true</c> if <paramref name="item"/> is found in the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <param name="item">
        /// The object to locate in the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            return _values.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/> to
        /// an <see cref="T:System.Array"/>, starting at a particular
        /// <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="T:System.Array"/> that is the
        /// destination of the elements copied from
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// The <see cref="T:System.Array"/> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying
        /// begins.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than 0.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// The number of elements in the source
        /// <see cref="T:System.Collections.Generic.ICollection`1"/> is
        /// greater than the available space from <paramref name="arrayIndex"/>
        /// to the end of the destination <paramref name="array"/>.
        /// </exception>
        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            _values.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// <c>true</c> if <paramref name="item"/> was successfully removed from the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise,
        /// <c>false</c>. This method also returns false if <paramref name="item"/>
        /// is not found in the original
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <param name="item">
        /// The object to remove from the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            return _values.Remove(item);
        }

        /// <summary>
        /// Determines whether the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/> contains
        /// an element with the specified key.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the <see cref="T:System.Collections.Generic.IDictionary`2"/>
        /// contains an element with the key; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="key">
        /// The key to locate in the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        bool IDictionary<string, object>.ContainsKey(string key)
        {
            return _values.ContainsKey(key);
        }

        /// <summary>
        /// Adds an element with the provided key and value to the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <param name="key">
        /// The object to use as the key of the element to add.
        /// </param><param name="value">
        /// The object to use as the value of the element to add.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// An element with the same key already exists in the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IDictionary`2"/> is read-only.
        /// </exception>
        void IDictionary<string, object>.Add(string key, object value)
        {
            _values.Add(key, value);
        }

        /// <summary>
        /// Removes the element with the specified key from the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the element is successfully removed; otherwise, <c>false</c>.
        /// This method also returns false if <paramref name="key"/> was
        /// not found in the original
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        /// <param name="key">
        /// The key of the element to remove.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IDictionary`2"/> is read-only.
        /// </exception>
        bool IDictionary<string, object>.Remove(string key)
        {
            return _values.Remove(key);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <returns>
        /// true if the object that implements
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>
        /// contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <param name="key">
        /// The key whose value to get.
        /// </param>
        /// <param name="value">
        /// When this method returns, the value associated with the specified key,
        /// if the key is found; otherwise, the default value for the type of the
        /// <paramref name="value"/> parameter. This parameter is passed uninitialized.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        bool IDictionary<string, object>.TryGetValue(string key, out object value)
        {
            return _values.TryGetValue(key, out value);
        }
		}
}
