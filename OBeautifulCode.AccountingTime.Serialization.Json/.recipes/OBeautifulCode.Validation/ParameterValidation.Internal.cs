﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterValidation.Internal.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Validation source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Recipes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Contains all validations that can be applied to a <see cref="Parameter"/>.
    /// </summary>
#if !OBeautifulCodeValidationRecipesProject
    internal
#else
    public
#endif
        static partial class ParameterValidation
    {
        private static readonly HashSet<char> AlphaNumericCharactersHashSet =
            new HashSet<char>(
                new char[0]
                    .Concat(Enumerable.Range(48, 10).Select(Convert.ToChar))
                    .Concat(Enumerable.Range(65, 26).Select(Convert.ToChar))
                    .Concat(Enumerable.Range(97, 26).Select(Convert.ToChar)));

        private delegate void ValueValidationHandler(
            Validation validation);

        private static void BeNullInternal(
            Validation validation)
        {
            if (!ReferenceEquals(validation.Value, null))
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeNullExceptionMessageSuffix, Include.FailingValue);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void NotBeNullInternal(
            Validation validation)
        {
            if (ReferenceEquals(validation.Value, null))
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeNullExceptionMessageSuffix);

                if (validation.IsElementInEnumerable)
                {
                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
                else
                {
                    var exception = new ArgumentNullException(null, exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void BeTrueInternal(
            Validation validation)
        {
            var shouldThrow = ReferenceEquals(validation.Value, null) || ((bool)validation.Value != true);
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeTrueExceptionMessageSuffix, Include.FailingValue);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void NotBeTrueInternal(
            Validation validation)
        {
            var shouldNotThrow = ReferenceEquals(validation.Value, null) || ((bool)validation.Value == false);
            if (!shouldNotThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeTrueExceptionMessageSuffix);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void BeFalseInternal(
            Validation validation)
        {
            var shouldThrow = ReferenceEquals(validation.Value, null) || (bool)validation.Value;
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeFalseExceptionMessageSuffix, Include.FailingValue);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void NotBeFalseInternal(
            Validation validation)
        {
            var shouldNotThrow = ReferenceEquals(validation.Value, null) || (bool)validation.Value;
            if (!shouldNotThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeFalseExceptionMessageSuffix);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void NotBeNullNorWhiteSpaceInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var shouldThrow = string.IsNullOrWhiteSpace((string)validation.Value);
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeNullNorWhiteSpaceExceptionMessageSuffix, Include.FailingValue);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void BeNullOrNotWhiteSpaceInternal(
            Validation validation)
        {
            var shouldThrow = !ReferenceEquals(validation.Value, null) && string.IsNullOrWhiteSpace((string)validation.Value);
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeNullOrNotWhiteSpaceExceptionMessageSuffix, Include.FailingValue);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void BeEmptyGuidInternal(
            Validation validation)
        {
            var shouldThrow = ReferenceEquals(validation.Value, null) || ((Guid)validation.Value != Guid.Empty);
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeEmptyGuidExceptionMessageSuffix, Include.FailingValue);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void NotBeEmptyGuidInternal(
            Validation validation)
        {
            var shouldThrow = (!ReferenceEquals(validation.Value, null)) && ((Guid)validation.Value == Guid.Empty);
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeEmptyGuidExceptionMessageSuffix);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength", Justification = "string.IsNullOrEmpty does not work here")]
        private static void BeEmptyStringInternal(
            Validation validation)
        {
            var shouldThrow = (string)validation.Value != string.Empty;

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeEmptyStringExceptionMessageSuffix, Include.FailingValue);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength", Justification = "string.IsNullOrEmpty does not work here")]
        private static void NotBeEmptyStringInternal(
            Validation validation)
        {
            var shouldThrow = (string)validation.Value == string.Empty;

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeEmptyStringExceptionMessageSuffix);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "unused", Justification = "Cannot iterate without a local")]
        private static void BeEmptyEnumerableInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var valueAsEnumerable = validation.Value as IEnumerable;
            var shouldThrow = false;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var unused in valueAsEnumerable)
            {
                shouldThrow = true;
                break;
            }

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeEmptyEnumerableExceptionMessageSuffix);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "unused", Justification = "Cannot iterate without a local")]
        private static void NotBeEmptyEnumerableInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var valueAsEnumerable = validation.Value as IEnumerable;
            var shouldThrow = true;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var unused in valueAsEnumerable)
            {
                shouldThrow = false;
                break;
            }

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeEmptyEnumerableExceptionMessageSuffix);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "unused", Justification = "Cannot iterate without a local")]
        private static void BeEmptyDictionaryInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var valueAsDictionary = validation.Value as IDictionary;

            // ReSharper disable once PossibleNullReferenceException
            var shouldThrow = valueAsDictionary.Count != 0;
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeEmptyDictionaryExceptionMessageSuffix);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "unused", Justification = "Cannot iterate without a local")]
        private static void NotBeEmptyDictionaryInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var valueAsDictionary = validation.Value as IDictionary;

            // ReSharper disable once PossibleNullReferenceException
            var shouldThrow = valueAsDictionary.Count == 0;
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeEmptyDictionaryExceptionMessageSuffix);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void ContainSomeNullElementsInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var valueAsEnumerable = validation.Value as IEnumerable;
            var shouldThrow = true;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var unused in valueAsEnumerable)
            {
                if (ReferenceEquals(unused, null))
                {
                    shouldThrow = false;
                    break;
                }
            }

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, ContainSomeNullElementsExceptionMessageSuffix);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void NotContainAnyNullElementsInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var valueAsEnumerable = validation.Value as IEnumerable;
            var shouldThrow = false;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var unused in valueAsEnumerable)
            {
                if (ReferenceEquals(unused, null))
                {
                    shouldThrow = true;
                    break;
                }
            }

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotContainAnyNullElementsExceptionMessageSuffix);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void ContainSomeKeyValuePairsWithNullValueInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var valueAsEnumerable = validation.Value as IEnumerable;
            var shouldThrow = true;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var keyValuePair in valueAsEnumerable)
            {
                if (ReferenceEquals(((dynamic)keyValuePair).Value, null))
                {
                    shouldThrow = false;
                    break;
                }
            }

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, ContainSomeKeyValuePairsWithNullValueExceptionMessageSuffix);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void NotContainAnyKeyValuePairsWithNullValueInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var valueAsEnumerable = validation.Value as IEnumerable;
            var shouldThrow = false;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var keyValuePair in valueAsEnumerable)
            {
                if (ReferenceEquals(((dynamic)keyValuePair).Value, null))
                {
                    shouldThrow = true;
                    break;
                }
            }

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotContainAnyKeyValuePairsWithNullValueExceptionMessageSuffix);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void BeDefaultInternal(
            Validation validation)
        {
            var defaultValue = GetDefaultValue(validation.ValueType);
            var shouldThrow = !EqualUsingDefaultEqualityComparer(validation.ValueType, validation.Value, defaultValue);
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeDefaultExceptionMessageSuffix, Include.FailingValue | Include.GenericType);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void NotBeDefaultInternal(
            Validation validation)
        {
            var defaultValue = GetDefaultValue(validation.ValueType);
            var shouldThrow = EqualUsingDefaultEqualityComparer(validation.ValueType, validation.Value, defaultValue);
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeDefaultExceptionMessageSuffix, Include.GenericType);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void BeLessThanInternal(
            Validation validation)
        {
            var shouldThrow = CompareUsingDefaultComparer(validation.ValueType, validation.Value, validation.ValidationParameters[0].Value) != CompareOutcome.Value1LessThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeLessThanExceptionMessageSuffix, Include.FailingValue | Include.GenericType);

                if (validation.IsElementInEnumerable)
                {
                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
                else
                {
                    var exception = new ArgumentOutOfRangeException(exceptionMessage, (Exception)null).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void NotBeLessThanInternal(
            Validation validation)
        {
            var shouldThrow = CompareUsingDefaultComparer(validation.ValueType, validation.Value, validation.ValidationParameters[0].Value) == CompareOutcome.Value1LessThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeLessThanExceptionMessageSuffix, Include.FailingValue | Include.GenericType);

                if (validation.IsElementInEnumerable)
                {
                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
                else
                {
                    var exception = new ArgumentOutOfRangeException(exceptionMessage, (Exception)null).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void BeGreaterThanInternal(
            Validation validation)
        {
            var shouldThrow = CompareUsingDefaultComparer(validation.ValueType, validation.Value, validation.ValidationParameters[0].Value) != CompareOutcome.Value1GreaterThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeGreaterThanExceptionMessageSuffix, Include.FailingValue | Include.GenericType);

                if (validation.IsElementInEnumerable)
                {
                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
                else
                {
                    var exception = new ArgumentOutOfRangeException(exceptionMessage, (Exception)null).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void NotBeGreaterThanInternal(
            Validation validation)
        {
            var shouldThrow = CompareUsingDefaultComparer(validation.ValueType, validation.Value, validation.ValidationParameters[0].Value) == CompareOutcome.Value1GreaterThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeGreaterThanExceptionMessageSuffix, Include.FailingValue | Include.GenericType);

                if (validation.IsElementInEnumerable)
                {
                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
                else
                {
                    var exception = new ArgumentOutOfRangeException(exceptionMessage, (Exception)null).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void BeLessThanOrEqualToInternal(
            Validation validation)
        {
            var shouldThrow = CompareUsingDefaultComparer(validation.ValueType, validation.Value, validation.ValidationParameters[0].Value) == CompareOutcome.Value1GreaterThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeLessThanOrEqualToExceptionMessageSuffix, Include.FailingValue | Include.GenericType);

                if (validation.IsElementInEnumerable)
                {
                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
                else
                {
                    var exception = new ArgumentOutOfRangeException(exceptionMessage, (Exception)null).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void NotBeLessThanOrEqualToInternal(
            Validation validation)
        {
            var shouldThrow = CompareUsingDefaultComparer(validation.ValueType, validation.Value, validation.ValidationParameters[0].Value) != CompareOutcome.Value1GreaterThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeLessThanOrEqualToExceptionMessageSuffix, Include.FailingValue | Include.GenericType);

                if (validation.IsElementInEnumerable)
                {
                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data).AddData(validation.Data);

                    throw exception;
                }
                else
                {
                    var exception = new ArgumentOutOfRangeException(exceptionMessage, (Exception)null).AddData(validation.Data).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void BeGreaterThanOrEqualToInternal(
            Validation validation)
        {
            var shouldThrow = CompareUsingDefaultComparer(validation.ValueType, validation.Value, validation.ValidationParameters[0].Value) == CompareOutcome.Value1LessThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeGreaterThanOrEqualToExceptionMessageSuffix, Include.FailingValue | Include.GenericType);

                if (validation.IsElementInEnumerable)
                {
                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
                else
                {
                    var exception = new ArgumentOutOfRangeException(exceptionMessage, (Exception)null).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void NotBeGreaterThanOrEqualToInternal(
            Validation validation)
        {
            var shouldThrow = CompareUsingDefaultComparer(validation.ValueType, validation.Value, validation.ValidationParameters[0].Value) != CompareOutcome.Value1LessThanValue2;
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeGreaterThanOrEqualToExceptionMessageSuffix, Include.FailingValue | Include.GenericType);

                if (validation.IsElementInEnumerable)
                {
                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
                else
                {
                    var exception = new ArgumentOutOfRangeException(exceptionMessage, (Exception)null).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void BeEqualToInternal(
            Validation validation)
        {
            var shouldThrow = !EqualUsingDefaultEqualityComparer(validation.ValueType, validation.Value, validation.ValidationParameters[0].Value);
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeEqualToExceptionMessageSuffix, Include.FailingValue | Include.GenericType);
                if (validation.IsElementInEnumerable)
                {
                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
                else
                {
                    var exception = new ArgumentOutOfRangeException(exceptionMessage, (Exception)null).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void NotBeEqualToInternal(
            Validation validation)
        {
            var shouldThrow = EqualUsingDefaultEqualityComparer(validation.ValueType, validation.Value, validation.ValidationParameters[0].Value);
            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeEqualToExceptionMessageSuffix, Include.GenericType);
                if (validation.IsElementInEnumerable)
                {
                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
                else
                {
                    var exception = new ArgumentOutOfRangeException(exceptionMessage, (Exception)null).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void BeInRangeInternal(
            Validation validation)
        {
            ThrowIfMalformedRange(validation.ValidationParameters);

            var shouldThrow = (CompareUsingDefaultComparer(validation.ValueType, validation.Value, validation.ValidationParameters[0].Value) == CompareOutcome.Value1LessThanValue2) ||
                              (CompareUsingDefaultComparer(validation.ValueType, validation.Value, validation.ValidationParameters[1].Value) == CompareOutcome.Value1GreaterThanValue2);

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeInRangeExceptionMessageSuffix, Include.FailingValue | Include.GenericType);

                if (validation.IsElementInEnumerable)
                {
                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
                else
                {
                    var exception = new ArgumentOutOfRangeException(exceptionMessage, (Exception)null).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void NotBeInRangeInternal(
            Validation validation)
        {
            ThrowIfMalformedRange(validation.ValidationParameters);

            var shouldThrow = (CompareUsingDefaultComparer(validation.ValueType, validation.Value, validation.ValidationParameters[0].Value) != CompareOutcome.Value1LessThanValue2) &&
                              (CompareUsingDefaultComparer(validation.ValueType, validation.Value, validation.ValidationParameters[1].Value) != CompareOutcome.Value1GreaterThanValue2);

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeInRangeExceptionMessageSuffix, Include.GenericType);

                if (validation.IsElementInEnumerable)
                {
                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
                else
                {
                    var exception = new ArgumentOutOfRangeException(exceptionMessage, (Exception)null).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void ContainInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var valueAsEnumerable = (IEnumerable)validation.Value;
            var searchForItem = validation.ValidationParameters[0].Value;
            var elementType = validation.ValidationParameters[0].ValueType;
            foreach (var element in valueAsEnumerable)
            {
                if (EqualUsingDefaultEqualityComparer(elementType, element, searchForItem))
                {
                    return;
                }
            }

            var exceptionMessage = BuildArgumentExceptionMessage(validation, ContainExceptionMessageSuffix, Include.GenericType, genericTypeOverride: elementType);

            var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

            throw exception;
        }

        private static void NotContainInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var valueAsEnumerable = (IEnumerable)validation.Value;
            var searchForItem = validation.ValidationParameters[0].Value;
            var elementType = validation.ValidationParameters[0].ValueType;
            foreach (var element in valueAsEnumerable)
            {
                if (EqualUsingDefaultEqualityComparer(elementType, element, searchForItem))
                {
                    var exceptionMessage = BuildArgumentExceptionMessage(validation, NotContainExceptionMessageSuffix, Include.GenericType, genericTypeOverride: elementType);

                    var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                    throw exception;
                }
            }
        }

        private static void BeAlphanumericInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var otherAllowedCharacters = (IReadOnlyCollection<char>)validation.ValidationParameters[0].Value;

            var stringValue = (string)validation.Value;

            bool shouldThrow;
            if (otherAllowedCharacters == null)
            {
                shouldThrow = stringValue.Any(_ => !AlphaNumericCharactersHashSet.Contains(_));
            }
            else
            {
                var allowedCharactersHashSet = new HashSet<char>(AlphaNumericCharactersHashSet);
                foreach (var otherAllowedCharacter in otherAllowedCharacters)
                {
                    allowedCharactersHashSet.Add(otherAllowedCharacter);
                }

                shouldThrow = stringValue.Any(_ => !allowedCharactersHashSet.Contains(_));
            }

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeAlphanumericExceptionMessageSuffix, Include.FailingValue);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void BeAsciiPrintableInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var treatNewLineAsPrintable = (bool)validation.ValidationParameters[0].Value;

            var stringValue = (string)validation.Value;

            if (treatNewLineAsPrintable)
            {
                stringValue = stringValue.Replace(Environment.NewLine, string.Empty);
            }

            var shouldThrow = stringValue.Any(_ => ((int)_ < 32) || ((int)_ > 126));

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeAsciiPrintableExceptionMessageSuffix, Include.FailingValue);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void BeMatchedByRegexInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var regex = (Regex)validation.ValidationParameters[0].Value;

            var stringValue = (string)validation.Value;

            var shouldThrow = !regex.IsMatch(stringValue);

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, BeMatchedByRegexExceptionMessageSuffix, Include.FailingValue);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }

        private static void NotBeMatchedByRegexInternal(
            Validation validation)
        {
            NotBeNullInternal(validation);

            var regex = (Regex)validation.ValidationParameters[0].Value;

            var stringValue = (string)validation.Value;

            var shouldThrow = regex.IsMatch(stringValue);

            if (shouldThrow)
            {
                var exceptionMessage = BuildArgumentExceptionMessage(validation, NotBeMatchedByRegexExceptionMessageSuffix, Include.FailingValue);

                var exception = new ArgumentException(exceptionMessage).AddData(validation.Data);

                throw exception;
            }
        }
    }
}
