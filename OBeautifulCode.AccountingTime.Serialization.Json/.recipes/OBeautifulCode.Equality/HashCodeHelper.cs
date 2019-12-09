﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashCodeHelper.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Equality.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Equality.Recipes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using OBeautifulCode.Type.Recipes;

    /// <summary>
    /// Provides methods to help with generating hash codes for structures and classes. This handles
    /// value types, nullable type, and objects.
    /// </summary>
    /// <remarks>
    /// Adapted from NodaTime: <a href="https://github.com/nodatime/nodatime/blob/master/src/NodaTime/Utility/HashCodeHelper.cs"/>.
    /// The basic usage pattern is as follows.
    /// <example>
    /// <code>
    ///  public override int GetHashCode() => HashCodeHelper.Initialize().Hash(Field1).Hash(Field2).Hash(Field3).Value;
    /// </code>
    /// </example>
    /// </remarks>
#if !OBeautifulCodeEqualityRecipesProject
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Equality.Recipes", "See package version number")]
    internal
#else
    public
#endif
    class HashCodeHelper
    {
        /// <summary>
        /// The multiplier for each value.
        /// </summary>
        private const int HashCodeMultiplier = 37;

        /// <summary>
        /// The initial hash value.
        /// </summary>
        private const int HashCodeInitializer = 17;

        private static readonly MethodInfo HashDictionaryMethodInfo = typeof(HashCodeHelper).GetMethod(nameof(HashDictionary), BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly MethodInfo HashReadOnlyDictionaryMethodInfo = typeof(HashCodeHelper).GetMethod(nameof(HashReadOnlyDictionary), BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly MethodInfo HashOrderedCollectionMethodInfo = typeof(HashCodeHelper).GetMethod(nameof(HashOrderedCollection), BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly MethodInfo HashUnorderedCollectionMethodInfo = typeof(HashCodeHelper).GetMethod(nameof(HashUnorderedCollection), BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// Initializes a new instance of the <see cref="HashCodeHelper"/> class.
        /// </summary>
        /// <param name="value">The hash code value.</param>
        public HashCodeHelper(
            int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the hash code value.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Returns the initial value for a hash code.
        /// </summary>
        /// <returns>The initial integer wrapped in a <see cref="HashCodeHelper"/> value.</returns>
        public static HashCodeHelper Initialize() => new HashCodeHelper(HashCodeInitializer);

        /// <summary>
        /// Returns the initial value for a hash code.
        /// </summary>
        /// <param name="seedValue">Seed value to initialize with (often the hash code from a base class using it's base properties).</param>
        /// <returns>The initial integer wrapped in a <see cref="HashCodeHelper"/> value.</returns>
        public static HashCodeHelper Initialize(int seedValue) => new HashCodeHelper(seedValue);

        /// <summary>
        /// Adds the hash code for the given item to the current hash code and returns the new hash code.
        /// </summary>
        /// <typeparam name="T">The type of the item being hashed.</typeparam>
        /// <param name="item">The item to hash.</param>
        /// <returns>The new hash code.</returns>
        public HashCodeHelper Hash<T>(
            T item)
        {
            unchecked
            {
                HashCodeHelper result;

                var valueType = typeof(T);

                if (valueType.IsClosedSystemDictionaryType())
                {
                    var methodInfo = valueType.GetGenericTypeDefinition() == typeof(IDictionary<,>)
                        ? HashDictionaryMethodInfo
                        : HashReadOnlyDictionaryMethodInfo;

                    result = (HashCodeHelper)methodInfo.MakeGenericMethod(valueType.GenericTypeArguments).Invoke(this, new[] { (object)item });
                }
                else if (valueType.IsClosedSystemCollectionType())
                {
                    if (valueType.IsClosedSystemOrderedCollectionType())
                    {
                        result = (HashCodeHelper)HashOrderedCollectionMethodInfo.MakeGenericMethod(valueType.GenericTypeArguments).Invoke(this, new[] { (object)item });
                    }
                    else
                    {
                        result = (HashCodeHelper)HashUnorderedCollectionMethodInfo.MakeGenericMethod(valueType.GenericTypeArguments).Invoke(this, new[] { (object)item });
                    }
                }
                else if (valueType.IsArray)
                {
                    var genericArguments = valueType.GetElementType();

                    result = (HashCodeHelper)HashOrderedCollectionMethodInfo.MakeGenericMethod(genericArguments).Invoke(this, new[] { (object)item });
                }
                else
                {
                    var hashCode = (this.Value * HashCodeMultiplier) + (item?.GetHashCode() ?? 0);

                    result = new HashCodeHelper(hashCode);
                }

                return result;
            }
        }

        private HashCodeHelper HashDictionary<TKey, TValue>(
            IDictionary<TKey, TValue> dictionary)
        {
            HashCodeHelper result = this;

            if (dictionary == null)
            {
                // We cannot just use the current hash code value as is.
                // We have to hash null and thus change the current hash code.
                // Consider the following collection of dictionaries:
                // { { "a": 1 }, { "b": 2 } }
                // { { "a": 1 }, null, { "b": 2 } }
                // They would result in the same hash code if we didn't hash null.
                result = result.Hash<object>(null);
            }
            else
            {
                // Is there a comparer for the keys?
                if (!TypeExtensions.HasWorkingDefaultComparer<TKey>())
                {
                    // There is no comparer for the keys and thus we cannot sort the key/value pairs.
                    // The best we can do is hash the count, which will ensure
                    // that two dictionaries, having a different count, will
                    // not return the same hash code.
                    result = result.Hash(dictionary.Count);
                }
                else
                {
                    // The keys can be sorted.  Sort the key/value pairs by the keys
                    // and then hash the key collection and the value collection,
                    // treating them as ordered collections.
                    var keyValuePairsInOrder = dictionary.OrderBy(_ => _.Key).ToList();

                    result = result.HashOrderedCollection(keyValuePairsInOrder.Select(_ => _.Key));

                    result = result.HashOrderedCollection(keyValuePairsInOrder.Select(_ => _.Value));
                }
            }

            return result;
        }

        private HashCodeHelper HashReadOnlyDictionary<TKey, TValue>(
            IReadOnlyDictionary<TKey, TValue> dictionary)
        {
            HashCodeHelper result = this;

            if (dictionary == null)
            {
                // We cannot just use the current hash code value as is.
                // We have to hash null and thus change the current hash code.
                // Consider the following collection of dictionaries:
                // { { "a": 1 }, { "b": 2 } }
                // { { "a": 1 }, null, { "b": 2 } }
                // They would result in the same hash code if we didn't hash null.
                result = result.Hash<object>(null);
            }
            else
            {
                result = result.HashUnorderedCollection(dictionary.Keys);

                // Is there a comparer for the keys?
                if (!TypeExtensions.HasWorkingDefaultComparer<TKey>())
                {
                    // There is no comparer for the keys and thus we cannot sort the key/value pairs.
                    // The best we can do is hash the count, which will ensure
                    // that two dictionaries, having a different count, will
                    // not return the same hash code.
                    result = result.Hash(dictionary.Count);
                }
                else
                {
                    // The keys can be sorted.  Sort the key/value pairs by the keys
                    // and then hash the key collection and the value collection,
                    // treating them as ordered collections.
                    var keyValuePairsInOrder = dictionary.OrderBy(_ => _.Key).ToList();

                    result = result.HashOrderedCollection(keyValuePairsInOrder.Select(_ => _.Key));

                    result = result.HashOrderedCollection(keyValuePairsInOrder.Select(_ => _.Value));
                }
            }

            return result;
        }

        private HashCodeHelper HashOrderedCollection<TElement>(
            IEnumerable<TElement> collection)
        {
            HashCodeHelper result = this;

            if (collection == null)
            {
                // We cannot just use the current hash code value as is.
                // We have to hash null and thus change the current hash code.
                // Consider the following collection of collections:
                // { { 1, 2 }, {3, 4} }
                // { { 1, 2 }, null, {3, 4}  }
                // They would result in the same hash code if we didn't hash null.
                result = result.Hash<object>(null);
            }
            else
            {
                // Just hash each element in-order.
                foreach (var value in collection)
                {
                    result = result.Hash(value);
                }
            }

            return result;
        }

        private HashCodeHelper HashUnorderedCollection<TElement>(
            IEnumerable<TElement> collection)
        {
            HashCodeHelper result = this;

            if (collection == null)
            {
                // We cannot just use the current hash code value as is.
                // We have to hash null and thus change the current hash code.
                // Consider the following collection of collections:
                // { { 1, 2 }, {3, 4} }
                // { { 1, 2 }, null, {3, 4}  }
                // They would result in the same hash code if we didn't hash null.
                result = result.Hash<object>(null);
            }
            else
            {
                // Is there a comparer for the element type?
                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if (!TypeExtensions.HasWorkingDefaultComparer<TElement>())
                {
                    // There is no comparer and thus we cannot sort the elements.
                    // The best we can do is hash the element count, which will ensure
                    // that two unordered collections, having a different count, will
                    // not return the same hash code.
                    result = result.Hash(collection.Count());
                }
                else
                {
                    // There is a comparer; sort the collection and then treat
                    // it as an ordered collection and hash that.  This ensures that
                    // two unordered collections that are equal but having a different
                    // order will result in the same hash code.
                    result = result.HashOrderedCollection(collection.OrderBy(_ => _));
                }
            }

            return result;
        }
    }
}
