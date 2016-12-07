// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportingPeriodInclusive{T}.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime
{
    using System;

    using Math;

    using static System.FormattableString;

    /// <inheritdoc />
    [Serializable]
    public class ReportingPeriodInclusive<T> : ReportingPeriod<T>, IReportingPeriodInclusive<T>, IEquatable<ReportingPeriodInclusive<T>>, IEquatable<IReportingPeriod<UnitOfTime>>
        where T : UnitOfTime
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingPeriodInclusive{T}"/> class.
        /// </summary>
        /// <param name="start">The start of the reporting period.</param>
        /// <param name="end">The end of the reporting period.</param>
        /// <exception cref="ArgumentNullException"><paramref name="start"/> or <paramref name="end"/> are null.</exception>
        /// <exception cref="ArgumentException"><paramref name="start"/> and <paramref name="end"/> are not of the same type.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="start"/> must be less than equal to <paramref name="end"/>.</exception>
        public ReportingPeriodInclusive(T start, T end)
            : base(start, end)
        {
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="ReportingPeriodInclusive{T}" /> are equal.
        /// </summary>
        /// <remarks>
        /// Reporting periods are equal if they have the same start and end unit-of-time.
        /// </remarks>
        /// <param name="left">The first reporting period to compare.</param>
        /// <param name="right">The second reporting period to compare.</param>
        /// <returns>true if the two reporting periods are equal; false otherwise.</returns>
        public static bool operator ==(ReportingPeriodInclusive<T> left, ReportingPeriodInclusive<T> right) => IsEqual(left, right);

        /// <summary>
        /// Determines whether an object of type <see cref="ReportingPeriodInclusive{T}" /> and <see cref="IReportingPeriod{T}" /> are equal.
        /// </summary>
        /// <remarks>
        /// Reporting periods are equal if they have the same start and end unit-of-time.
        /// </remarks>
        /// <param name="left">The first reporting period to compare.</param>
        /// <param name="right">The second reporting period to compare.</param>
        /// <returns>true if the two reporting periods are equal; false otherwise.</returns>
        public static bool operator ==(ReportingPeriodInclusive<T> left, IReportingPeriod<UnitOfTime> right) => IsEqual(left, right);

        /// <summary>
        /// Determines whether an object of type <see cref="ReportingPeriodInclusive{T}" /> and <see cref="IReportingPeriod{T}" /> are equal.
        /// </summary>
        /// <remarks>
        /// Reporting periods are equal if they have the same start and end unit-of-time.
        /// </remarks>
        /// <param name="left">The first reporting period to compare.</param>
        /// <param name="right">The second reporting period to compare.</param>
        /// <returns>true if the two reporting periods are equal; false otherwise.</returns>
        public static bool operator ==(IReportingPeriod<UnitOfTime> left, ReportingPeriodInclusive<T> right) => IsEqual(right, left);

        /// <summary>
        /// Determines whether two objects of type <see cref="ReportingPeriodInclusive{T}" /> are not equal.
        /// </summary>
        /// <remarks>
        /// Reporting periods are not equal if they have different start and end unit-of-time.
        /// </remarks>
        /// <param name="left">The first reporting period to compare.</param>
        /// <param name="right">The second reporting period to compare.</param>
        /// <returns>true if the two reporting periods are not equal; false otherwise.</returns>
        public static bool operator !=(ReportingPeriodInclusive<T> left, ReportingPeriodInclusive<T> right) => !(left == right);

        /// <summary>
        /// Determines whether an object of type <see cref="ReportingPeriodInclusive{T}" /> and <see cref="IReportingPeriod{T}" /> are not equal.
        /// </summary>
        /// <remarks>
        /// Reporting periods are not equal if they have different start and end unit-of-time.
        /// </remarks>
        /// <param name="left">The first reporting period to compare.</param>
        /// <param name="right">The second reporting period to compare.</param>
        /// <returns>true if the two reporting periods are not equal; false otherwise.</returns>
        public static bool operator !=(IReportingPeriod<UnitOfTime> left, ReportingPeriodInclusive<T> right) => !(left == right);

        /// <summary>
        /// Determines whether an object of type <see cref="ReportingPeriodInclusive{T}" /> and <see cref="IReportingPeriod{T}" /> are not equal.
        /// </summary>
        /// <remarks>
        /// Reporting periods are not equal if they have different start and end unit-of-time.
        /// </remarks>
        /// <param name="left">The first reporting period to compare.</param>
        /// <param name="right">The second reporting period to compare.</param>
        /// <returns>true if the two reporting periods are not equal; false otherwise.</returns>
        public static bool operator !=(ReportingPeriodInclusive<T> left, IReportingPeriod<UnitOfTime> right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="ReportingPeriodInclusive{T}"/> is equal to this one.
        /// </summary>
        /// <remarks>
        /// Reporting periods are equal if they have the same start and end unit-of-time.
        /// </remarks>
        /// <param name="other">The reporting period to compare this one with.</param>
        /// <returns>true if this reporting period is equal to the specified reporting period; false otherwise.</returns>
        public bool Equals(ReportingPeriodInclusive<T> other) => this == other;

        /// <summary>
        /// Determines whether the specified <see cref="IReportingPeriod{T}"/> is equal to this one.
        /// </summary>
        /// <remarks>
        /// Reporting periods are equal if they have the same start and end unit-of-time.
        /// </remarks>
        /// <param name="other">The reporting period to compare this one with.</param>
        /// <returns>true if this reporting period is equal to the specified reporting period; false otherwise.</returns>
        public bool Equals(IReportingPeriod<UnitOfTime> other) => this == other;

        /// <summary>
        /// Determines whether the specified object is equal to this one, as per <see cref="Equals(ReportingPeriodInclusive{T})"/>.
        /// </summary>
        /// <param name="obj">The value to compare this one with.</param>
        /// <returns>true if the other object is a reporting period equal to this one; false otherwise, consistent with <see cref="Equals(ReportingPeriodInclusive{T})"/>.</returns>
        public override bool Equals(object obj) => this == (obj as IReportingPeriod<UnitOfTime>);

        /// <summary>
        /// Returns the hash code for this reporting period.
        /// </summary>
        /// <returns>The hash code for this reporting period.</returns>
        public override int GetHashCode() =>
            HashCodeHelper.Initialize()
                // ReSharper disable NonReadonlyMemberInGetHashCode
                .Hash(this.Start)
                .Hash(this.End)
                .Value;
                // ReSharper restore NonReadonlyMemberInGetHashCode

        /// <summary>
        /// Returns a friendly string representation of this reporting period.
        /// </summary>
        /// <returns>
        /// A friendly string representation of this reporting period.
        /// </returns>
        public override string ToString()
        {
            // ReSharper disable RedundantToStringCall
            var result = Invariant($"{this.Start.ToString()} to {this.End.ToString()}");
            // ReSharper restore RedundantToStringCall

            return result;
        }

        /// <inheritdoc />
        public override IReportingPeriod<T> Clone()
        {
            var startClone = this.Start.Clone<T>();
            var endClone = this.End.Clone<T>();
            var result = new ReportingPeriodInclusive<T>(startClone, endClone);
            return result;
        }

        private static bool IsEqual(ReportingPeriodInclusive<T> left, IReportingPeriod<UnitOfTime> right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            // ReSharper disable UseMethodIsInstanceOfType
             if (right.GetType().GetGenericTypeDefinition() != typeof(ReportingPeriodInclusive<>))
             {
                 return false;
             }

            // ReSharper restore UseMethodIsInstanceOfType

            try
            {
                var result = (left.Start.CompareTo(right.Start) == 0) && (left.End.CompareTo(right.End) == 0);
                return result;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}

// ReSharper restore CheckNamespace