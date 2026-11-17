// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FixedDayFiscalQuarterScheme.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using static System.FormattableString;

    /// <summary>
    /// A fiscal quarter scheme that defines the fiscal year using <see cref="FiscalYearScheme"/>
    /// and splits that year into 4 quarters that each end on <see cref="FixedDayFiscalYearScheme.LastDayInFiscalYear"/>,
    /// every 3 months (e.g. for a fiscal year that ends on April 25th, the quarters end on July 25th, October 25th, January 25th, and April 25th).
    /// </summary>
    /// <remarks>
    /// The fixed day must occur every year in each of the 4 months in which a fiscal quarter ends.
    /// So, for example, a fiscal year that ends on May 30th is not allowed, because February 30th does not exist.
    /// </remarks>
    public partial class FixedDayFiscalQuarterScheme : FiscalQuarterSchemeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixedDayFiscalQuarterScheme"/> class.
        /// </summary>
        /// <param name="fiscalYearScheme">The fiscal year scheme.</param>
        public FixedDayFiscalQuarterScheme(
            FixedDayFiscalYearScheme fiscalYearScheme)
        {
            if (fiscalYearScheme == null)
            {
                throw new ArgumentNullException(nameof(fiscalYearScheme));
            }

            // The fiscal year scheme already ensures that the day occurs in the month in which the fourth quarter ends.
            for (var monthsAfterLastMonthInFiscalYear = 3; monthsAfterLastMonthInFiscalYear < 12; monthsAfterLastMonthInFiscalYear += 3)
            {
                var quarterEndMonth = (MonthOfYear)((((int)fiscalYearScheme.LastMonthInFiscalYear - 1 + monthsAfterLastMonthInFiscalYear) % 12) + 1);

                if (!FixedDayFiscalYearScheme.DayOccursInMonthEveryYear(fiscalYearScheme.LastDayInFiscalYear, quarterEndMonth))
                {
                    throw new ArgumentException(Invariant($"'{nameof(fiscalYearScheme)}.{nameof(FixedDayFiscalYearScheme.LastDayInFiscalYear)}' == '{fiscalYearScheme.LastDayInFiscalYear}' does not occur every year in '{quarterEndMonth}', which is a month in which a fiscal quarter ends."));
                }
            }

            this.FiscalYearScheme = fiscalYearScheme;
        }

        /// <summary>
        /// Gets the fiscal year scheme.
        /// </summary>
        public FixedDayFiscalYearScheme FiscalYearScheme { get; private set; }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"><paramref name="fiscalQuarter"/> is null.</exception>
        public override ReportingPeriod GetCalendarDayPeriod(
            FiscalQuarter fiscalQuarter)
        {
            if (fiscalQuarter == null)
            {
                throw new ArgumentNullException(nameof(fiscalQuarter));
            }

            var lastDayInFiscalYear = this.FiscalYearScheme.GetLastDayInFiscalYear(fiscalQuarter.Year);

            var quarterNumber = (int)fiscalQuarter.QuarterNumber;

            // The constructor ensures that the day occurs in every month in which a fiscal quarter ends,
            // so adding months never shifts the day.
            var lastDayInPriorQuarter = lastDayInFiscalYear.AddMonths((3 * quarterNumber) - 15);

            var lastDayInQuarter = lastDayInFiscalYear.AddMonths((3 * quarterNumber) - 12);

            var result = new ReportingPeriod(lastDayInPriorQuarter.AddDays(1).ToCalendarDay(), lastDayInQuarter.ToCalendarDay());

            return result;
        }
    }
}
