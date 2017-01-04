// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeExtensions.Serialization.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using static System.FormattableString;

    /// <summary>
    /// Serialization-related extension methods on <see cref="UnitOfTime"/>.
    /// </summary>
    public static partial class UnitOfTimeExtensions
    {
        private static readonly SerializationFormat[] SerializationFormatByType =
        {
            #pragma warning disable SA1025 // Code must not contain multiple whitespace in a row
            #pragma warning disable SA1009 // Closing parenthesis must be spaced correctly
            #pragma warning disable SA1001 // Commas must be spaced correctly
            new SerializationFormat { Type = typeof(CalendarDay)      , Regex = new Regex("^c-(\\d{4})-(\\d{2})-(\\d{2})$") },
            new SerializationFormat { Type = typeof(CalendarMonth)    , Regex = new Regex("^c-(\\d{4})-(\\d{2})$") },
            new SerializationFormat { Type = typeof(CalendarQuarter)  , Regex = new Regex("^c-(\\d{4})-Q(\\d)$") },
            new SerializationFormat { Type = typeof(CalendarYear)     , Regex = new Regex("^c-(\\d{4})$") },
            new SerializationFormat { Type = typeof(CalendarUnbounded), Regex = new Regex("^c-unbounded$") },
            new SerializationFormat { Type = typeof(FiscalMonth)      , Regex = new Regex("^f-(\\d{4})-(\\d{2})$") },
            new SerializationFormat { Type = typeof(FiscalQuarter)    , Regex = new Regex("^f-(\\d{4})-Q(\\d)$") },
            new SerializationFormat { Type = typeof(FiscalYear)       , Regex = new Regex("^f-(\\d{4})$") },
            new SerializationFormat { Type = typeof(FiscalUnbounded)  , Regex = new Regex("^f-unbounded$") },
            new SerializationFormat { Type = typeof(GenericMonth)     , Regex = new Regex("^g-(\\d{4})-(\\d{2})$") },
            new SerializationFormat { Type = typeof(GenericQuarter)   , Regex = new Regex("^g-(\\d{4})-Q(\\d)$") },
            new SerializationFormat { Type = typeof(GenericYear)      , Regex = new Regex("^g-(\\d{4})$") },
            new SerializationFormat { Type = typeof(GenericUnbounded) , Regex = new Regex("^g-unbounded$") }
            #pragma warning restore SA1001 // Commas must be spaced correctly
            #pragma warning restore SA1009 // Closing parenthesis must be spaced correctly
            #pragma warning restore SA1025 // Code must not contain multiple whitespace in a row
        };

        /// <summary>
        /// Deserializes a <see cref="UnitOfTime"/> from a sortable string.
        /// </summary>
        /// <typeparam name="T">The type of unit-of-time.</typeparam>
        /// <param name="unitOfTime">The serialized, sortable unit-of-time string to deserialize.</param>
        /// <returns>
        /// Gets a unit-of-time deserialized from it's string representation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="unitOfTime"/> is whitespace.</exception>
        /// <exception cref="InvalidOperationException">Cannot deserialize string; it is not valid unit-of-time.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Deserializing is inheritently complex and requires lots of types.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Deserializing is inheritently complex and requires lots of types.")]
        public static T DeserializeFromSortableString<T>(this string unitOfTime)
            where T : UnitOfTime
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            if (string.IsNullOrWhiteSpace(unitOfTime))
            {
                throw new ArgumentException("unit-of-time string is whitespace", nameof(unitOfTime));
            }

            var serializationFormatMatch = SerializationFormatByType.Select(_ => new { Match = _.Regex.Match(unitOfTime), SerializationFormat = _ }).SingleOrDefault(_ => _.Match.Success);
            if (serializationFormatMatch == null)
            {
                throw new InvalidOperationException("Cannot deserialize string; it is not valid unit-of-time.");
            }

            var serializedType = serializationFormatMatch.SerializationFormat.Type;
            var returnType = typeof(T);
            if (!returnType.IsAssignableFrom(serializedType))
            {
                throw new InvalidOperationException(Invariant($"The unit-of-time appears to be a {serializedType.Name} which cannot be casted to a {returnType.Name}."));
            }

            string errorMessage = Invariant($"Cannot deserialize string;  it appears to be a {serializedType.Name} but it is malformed.");
            var tokens = serializationFormatMatch.SerializationFormat.Regex.GetGroupNames().Skip(1).Select(_ => serializationFormatMatch.Match.Groups[_].Value).ToArray();

            if (serializedType == typeof(CalendarDay))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var month = ParseEnumOrThrow<MonthOfYear>(tokens[1], errorMessage);
                var day = ParseEnumOrThrow<DayOfMonth>(tokens[2], errorMessage);

                try
                {
                    var deserialized = new CalendarDay(year, month, day);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(CalendarMonth))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var month = ParseEnumOrThrow<MonthOfYear>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new CalendarMonth(year, month);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(CalendarQuarter))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var quarter = ParseEnumOrThrow<QuarterNumber>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new CalendarQuarter(year, quarter);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(CalendarYear))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);

                try
                {
                    var deserialized = new CalendarYear(year);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(CalendarUnbounded))
            {
                var deserialized = new CalendarUnbounded();
                return deserialized as T;
            }

            if (serializedType == typeof(FiscalMonth))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var month = ParseEnumOrThrow<MonthNumber>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new FiscalMonth(year, month);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(FiscalQuarter))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var quarter = ParseEnumOrThrow<QuarterNumber>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new FiscalQuarter(year, quarter);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(FiscalYear))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);

                try
                {
                    var deserialized = new FiscalYear(year);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(FiscalUnbounded))
            {
                var deserialized = new FiscalUnbounded();
                return deserialized as T;
            }

            if (serializedType == typeof(GenericMonth))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var month = ParseEnumOrThrow<MonthNumber>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new GenericMonth(year, month);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(GenericQuarter))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var quarter = ParseEnumOrThrow<QuarterNumber>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new GenericQuarter(year, quarter);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(GenericYear))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);

                try
                {
                    var deserialized = new GenericYear(year);
                    return deserialized as T;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(GenericUnbounded))
            {
                var deserialized = new GenericUnbounded();
                return deserialized as T;
            }

            throw new NotSupportedException("this type of unit-of-time is not supported: " + serializedType);
        }

        /// <summary>
        /// Serializes a <see cref="UnitOfTime"/> to a sortable string.
        /// </summary>
        /// <param name="unitOfTime">The unit-of-time to serialize.</param>
        /// <returns>
        /// Gets a string representation of a unit-of-time that can be deserialized
        /// into the same unit-of-time and which sorts in the same way that the
        /// other unit-of-times (of the same type) would sort (earlier time first, later time last).
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        public static string SerializeToSortableString(this UnitOfTime unitOfTime)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            var unitOfTimeType = unitOfTime.GetType();
            var unitOfTimeAsCalendarDay = unitOfTime as CalendarDay;
            if (unitOfTimeAsCalendarDay != null)
            {
                string result = Invariant($"c-{unitOfTimeAsCalendarDay.Year:D4}-{(int)unitOfTimeAsCalendarDay.MonthNumber:D2}-{(int)unitOfTimeAsCalendarDay.DayOfMonth:D2}");
                return result;
            }

            var unitOfTimeAsCalendarMonth = unitOfTime as CalendarMonth;
            if (unitOfTimeAsCalendarMonth != null)
            {
                string result = Invariant($"c-{unitOfTimeAsCalendarMonth.Year:D4}-{(int)unitOfTimeAsCalendarMonth.MonthNumber:D2}");
                return result;
            }

            var unitOfTimeAsCalendarQuarter = unitOfTime as CalendarQuarter;
            if (unitOfTimeAsCalendarQuarter != null)
            {
                string result = Invariant($"c-{unitOfTimeAsCalendarQuarter.Year:D4}-Q{(int)unitOfTimeAsCalendarQuarter.QuarterNumber}");
                return result;
            }

            var unitOfTimeAsCalendarYear = unitOfTime as CalendarYear;
            if (unitOfTimeAsCalendarYear != null)
            {
                string result = Invariant($"c-{unitOfTimeAsCalendarYear.Year:D4}");
                return result;
            }

            var unitOfTimeAsCalendarUnbounded = unitOfTime as CalendarUnbounded;
            if (unitOfTimeAsCalendarUnbounded != null)
            {
                return "c-unbounded";
            }

            var unitOfTimeAsFiscalMonth = unitOfTime as FiscalMonth;
            if (unitOfTimeAsFiscalMonth != null)
            {
                string result = Invariant($"f-{unitOfTimeAsFiscalMonth.Year:D4}-{(int)unitOfTimeAsFiscalMonth.MonthNumber:D2}");
                return result;
            }

            var unitOfTimeAsFiscalQuarter = unitOfTime as FiscalQuarter;
            if (unitOfTimeAsFiscalQuarter != null)
            {
                string result = Invariant($"f-{unitOfTimeAsFiscalQuarter.Year:D4}-Q{(int)unitOfTimeAsFiscalQuarter.QuarterNumber}");
                return result;
            }

            var unitOfTimeAsFiscalYear = unitOfTime as FiscalYear;
            if (unitOfTimeAsFiscalYear != null)
            {
                string result = Invariant($"f-{unitOfTimeAsFiscalYear.Year:D4}");
                return result;
            }

            var unitOfTimeAsFiscalUnbounded = unitOfTime as FiscalUnbounded;
            if (unitOfTimeAsFiscalUnbounded != null)
            {
                return "f-unbounded";
            }

            var unitOfTimeAsGenericMonth = unitOfTime as GenericMonth;
            if (unitOfTimeAsGenericMonth != null)
            {
                string result = Invariant($"g-{unitOfTimeAsGenericMonth.Year:D4}-{(int)unitOfTimeAsGenericMonth.MonthNumber:D2}");
                return result;
            }

            var unitOfTimeAsGenericQuarter = unitOfTime as GenericQuarter;
            if (unitOfTimeAsGenericQuarter != null)
            {
                string result = Invariant($"g-{unitOfTimeAsGenericQuarter.Year:D4}-Q{(int)unitOfTimeAsGenericQuarter.QuarterNumber}");
                return result;
            }

            var unitOfTimeAsGenericYear = unitOfTime as GenericYear;
            if (unitOfTimeAsGenericYear != null)
            {
                string result = Invariant($"g-{unitOfTimeAsGenericYear.Year:D4}");
                return result;
            }

            var unitOfTimeAsGenericUnbounded = unitOfTime as GenericUnbounded;
            if (unitOfTimeAsGenericUnbounded != null)
            {
                return "g-unbounded";
            }

            throw new NotSupportedException("this type of unit-of-time is not supported: " + unitOfTimeType);
        }

        private static int ParseIntOrThrow(string token, string errorMessage)
        {
            int intValue;
            if (!int.TryParse(token, out intValue))
            {
                throw new InvalidOperationException(errorMessage);
            }

            return intValue;
        }

        private static T ParseEnumOrThrow<T>(string token, string errorMessage)
            where T : struct, IConvertible
        {
            int intValue = ParseIntOrThrow(token, errorMessage);
            var enumType = typeof(T);
            T enumValue = (T)Enum.ToObject(enumType, intValue);
            if (!Enum.IsDefined(enumType, enumValue))
            {
                throw new InvalidOperationException(errorMessage);
            }

            return enumValue;
        }

        private class SerializationFormat
        {
            public Type Type { get; set; }

            public Regex Regex { get; set; }
        }
    }
}

// ReSharper restore CheckNamespace