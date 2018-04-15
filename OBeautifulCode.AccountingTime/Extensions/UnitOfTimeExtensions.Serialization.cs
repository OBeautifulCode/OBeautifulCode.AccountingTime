// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfTimeExtensions.Serialization.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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
        public static T DeserializeFromSortableString<T>(
            this string unitOfTime)
            where T : UnitOfTime
        {
            var ret = (T)DeserializeFromSortableString(unitOfTime, typeof(T));
            return ret;
        }

        /// <summary>
        /// Deserializes a <see cref="UnitOfTime"/> from a sortable string.
        /// </summary>
        /// <param name="unitOfTime">The serialized, sortable unit-of-time string to deserialize.</param>
        /// <param name="type">Specific <see cref="UnitOfTime" /> type to deserialize into.</param>
        /// <returns>
        /// Gets a unit-of-time deserialized from it's string representation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfTime"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="unitOfTime"/> is whitespace.</exception>
        /// <exception cref="InvalidOperationException">Cannot deserialize string; it is not valid unit-of-time.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Keeping previous layout, is easy to read.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Keeping previous layout, is easy to read.")]
        public static UnitOfTime DeserializeFromSortableString(this string unitOfTime, Type type)
        {
            if (!UnitOfTime.IsUnitOfTimeType(type))
            {
                throw new NotSupportedException(Invariant($"Unsupported type {type?.FullName ?? "<NULL TYPE>"}, expected an implmenter {nameof(UnitOfTime)}"));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

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
            if (!type.IsAssignableFrom(serializedType))
            {
                throw new InvalidOperationException(Invariant($"The unit-of-time appears to be a {serializedType.Name} which cannot be casted to a {type.Name}."));
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
                    return deserialized;
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
                    return deserialized;
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
                    return deserialized;
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
                    return deserialized;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(CalendarUnbounded))
            {
                var deserialized = new CalendarUnbounded();
                return deserialized;
            }

            if (serializedType == typeof(FiscalMonth))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var month = ParseEnumOrThrow<MonthNumber>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new FiscalMonth(year, month);
                    return deserialized;
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
                    return deserialized;
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
                    return deserialized;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(FiscalUnbounded))
            {
                var deserialized = new FiscalUnbounded();
                return deserialized;
            }

            if (serializedType == typeof(GenericMonth))
            {
                var year = ParseIntOrThrow(tokens[0], errorMessage);
                var month = ParseEnumOrThrow<MonthNumber>(tokens[1], errorMessage);

                try
                {
                    var deserialized = new GenericMonth(year, month);
                    return deserialized;
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
                    return deserialized;
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
                    return deserialized;
                }
                catch (ArgumentException)
                {
                    throw new InvalidOperationException(errorMessage);
                }
            }

            if (serializedType == typeof(GenericUnbounded))
            {
                var deserialized = new GenericUnbounded();
                return deserialized;
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
        public static string SerializeToSortableString(
            this UnitOfTime unitOfTime)
        {
            if (unitOfTime == null)
            {
                throw new ArgumentNullException(nameof(unitOfTime));
            }

            var unitOfTimeType = unitOfTime.GetType();

            switch (unitOfTime)
            {
                case CalendarDay unitOfTimeAsCalendarDay:
                {
                    var result = Invariant($"c-{unitOfTimeAsCalendarDay.Year:D4}-{(int)unitOfTimeAsCalendarDay.MonthNumber:D2}-{(int)unitOfTimeAsCalendarDay.DayOfMonth:D2}");
                    return result;
                }
                case CalendarMonth unitOfTimeAsCalendarMonth:
                {
                    var result = Invariant($"c-{unitOfTimeAsCalendarMonth.Year:D4}-{(int)unitOfTimeAsCalendarMonth.MonthNumber:D2}");
                    return result;
                }
                case CalendarQuarter unitOfTimeAsCalendarQuarter:
                {
                    var result = Invariant($"c-{unitOfTimeAsCalendarQuarter.Year:D4}-Q{(int)unitOfTimeAsCalendarQuarter.QuarterNumber}");
                    return result;
                }
                case CalendarYear unitOfTimeAsCalendarYear:
                {
                    var result = Invariant($"c-{unitOfTimeAsCalendarYear.Year:D4}");
                    return result;
                }
                case CalendarUnbounded _:
                {
                    var result = "c-unbounded";
                    return result;
                }
                case FiscalMonth unitOfTimeAsFiscalMonth:
                {
                    var result = Invariant($"f-{unitOfTimeAsFiscalMonth.Year:D4}-{(int)unitOfTimeAsFiscalMonth.MonthNumber:D2}");
                    return result;
                }
                case FiscalQuarter unitOfTimeAsFiscalQuarter:
                {
                    var result = Invariant($"f-{unitOfTimeAsFiscalQuarter.Year:D4}-Q{(int)unitOfTimeAsFiscalQuarter.QuarterNumber}");
                    return result;
                }
                case FiscalYear unitOfTimeAsFiscalYear:
                {
                    var result = Invariant($"f-{unitOfTimeAsFiscalYear.Year:D4}");
                    return result;
                }
                case FiscalUnbounded _:
                {
                    var result = "f-unbounded";
                    return result;
                }
                case GenericMonth unitOfTimeAsGenericMonth:
                {
                    var result = Invariant($"g-{unitOfTimeAsGenericMonth.Year:D4}-{(int)unitOfTimeAsGenericMonth.MonthNumber:D2}");
                    return result;
                }
                case GenericQuarter unitOfTimeAsGenericQuarter:
                {
                    var result = Invariant($"g-{unitOfTimeAsGenericQuarter.Year:D4}-Q{(int)unitOfTimeAsGenericQuarter.QuarterNumber}");
                    return result;
                }
                case GenericYear unitOfTimeAsGenericYear:
                {
                    var result = Invariant($"g-{unitOfTimeAsGenericYear.Year:D4}");
                    return result;
                }
                case GenericUnbounded _:
                {
                    var result = "g-unbounded";
                        return result;
                }
                default:
                    throw new NotSupportedException("this type of unit-of-time is not supported: " + unitOfTimeType);
            }
        }

        private static int ParseIntOrThrow(
            string token,
            string errorMessage)
        {
            if (!int.TryParse(token, out var intValue))
            {
                throw new InvalidOperationException(errorMessage);
            }

            return intValue;
        }

        private static T ParseEnumOrThrow<T>(
            string token,
            string errorMessage)
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
