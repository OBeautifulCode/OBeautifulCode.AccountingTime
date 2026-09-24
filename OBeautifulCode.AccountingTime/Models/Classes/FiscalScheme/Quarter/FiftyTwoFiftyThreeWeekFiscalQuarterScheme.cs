// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiftyTwoFiftyThreeWeekFiscalQuarterScheme.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static System.FormattableString;

    /// <summary>
    /// A fiscal quarter scheme that defines the fiscal year using <see cref="FiftyTwoFiftyThreeWeekFiscalYearScheme"/>
    /// and specifies a fixed number of weeks per individual quarter along with which quarter should absorb the extra week in a 53-week fiscal year.
    /// </summary>
    /// <remarks>
    /// The most common configuration is 13 weeks in each quarter (e.g. 4-4-5, 4-5-4, or 5-4-4 week months), with the 53rd week added to the fourth quarter.
    /// Other configurations include 12-12-12-16 and 16-12-12-12 (common among grocery retailers) and 12-12-16-12.
    /// </remarks>
    public partial class FiftyTwoFiftyThreeWeekFiscalQuarterScheme : FiscalQuarterSchemeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiftyTwoFiftyThreeWeekFiscalQuarterScheme"/> class.
        /// </summary>
        /// <param name="fiscalYearScheme">The fiscal year scheme.</param>
        /// <param name="weeksPerQuarter">The number of weeks in the first, second, third, and fourth fiscal quarter (in that order) of a 52-week fiscal year.</param>
        /// <param name="quarterWithFiftyThirdWeek">The fiscal quarter that the 53rd week is added to in a 53-week fiscal year.</param>
        public FiftyTwoFiftyThreeWeekFiscalQuarterScheme(
            FiftyTwoFiftyThreeWeekFiscalYearScheme fiscalYearScheme,
            IReadOnlyList<int> weeksPerQuarter,
            QuarterNumber quarterWithFiftyThirdWeek)
        {
            if (fiscalYearScheme == null)
            {
                throw new ArgumentNullException(nameof(fiscalYearScheme));
            }

            if (weeksPerQuarter == null)
            {
                throw new ArgumentNullException(nameof(weeksPerQuarter));
            }

            if (weeksPerQuarter.Count != 4)
            {
                throw new ArgumentException(Invariant($"'{nameof(weeksPerQuarter)}' contains '{weeksPerQuarter.Count}' elements; it must contain '{4}' elements, one for each fiscal quarter."));
            }

            if (weeksPerQuarter.Any(_ => _ < 12))
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(weeksPerQuarter)}' contains an element that is < '{12}'."), (Exception)null);
            }

            if (weeksPerQuarter.Any(_ => _ > 16))
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(weeksPerQuarter)}' contains an element that is > '{16}'."), (Exception)null);
            }

            var totalWeeks = weeksPerQuarter.Sum();

            if (totalWeeks != 52)
            {
                throw new ArgumentException(Invariant($"The sum of the elements in '{nameof(weeksPerQuarter)}' is '{totalWeeks}'; it must be '{52}'."));
            }

            if (quarterWithFiftyThirdWeek == QuarterNumber.Invalid)
            {
                throw new ArgumentOutOfRangeException(Invariant($"'{nameof(quarterWithFiftyThirdWeek)}' == '{QuarterNumber.Invalid}'"), (Exception)null);
            }

            this.FiscalYearScheme = fiscalYearScheme;
            this.WeeksPerQuarter = weeksPerQuarter;
            this.QuarterWithFiftyThirdWeek = quarterWithFiftyThirdWeek;
        }

        /// <summary>
        /// Gets the fiscal year scheme.
        /// </summary>
        public FiftyTwoFiftyThreeWeekFiscalYearScheme FiscalYearScheme { get; private set; }

        /// <summary>
        /// Gets the number of weeks in the first, second, third, and fourth fiscal quarter (in that order) of a 52-week fiscal year.
        /// </summary>
        public IReadOnlyList<int> WeeksPerQuarter { get; private set; }

        /// <summary>
        /// Gets the fiscal quarter that the 53rd week is added to in a 53-week fiscal year.
        /// </summary>
        public QuarterNumber QuarterWithFiftyThirdWeek { get; private set; }

        /// <inheritdoc />
        public override ReportingPeriod GetCalendarDayPeriod(
            FiscalQuarter fiscalQuarter)
        {
            if (fiscalQuarter == null)
            {
                throw new ArgumentNullException(nameof(fiscalQuarter));
            }

            var fiscalYearReportingPeriod = this.FiscalYearScheme.GetCalendarDayPeriod(new FiscalYear(fiscalQuarter.Year));

            var firstDayInFiscalYear = (CalendarDay)fiscalYearReportingPeriod.Start;

            var lastDayInFiscalYear = (CalendarDay)fiscalYearReportingPeriod.End;

            var weeksInFiscalYear = ((lastDayInFiscalYear.ToDateTime() - firstDayInFiscalYear.ToDateTime()).Days + 1) / 7;

            var weeksInEachQuarter = this.WeeksPerQuarter.ToArray();

            if (weeksInFiscalYear == 53)
            {
                weeksInEachQuarter[(int)this.QuarterWithFiftyThirdWeek - 1]++;
            }

            var quarterIndex = (int)fiscalQuarter.QuarterNumber - 1;

            var firstDayInQuarter = firstDayInFiscalYear.Plus(7 * weeksInEachQuarter.Take(quarterIndex).Sum());

            var lastDayInQuarter = firstDayInQuarter.Plus((7 * weeksInEachQuarter[quarterIndex]) - 1);

            var result = new ReportingPeriod(firstDayInQuarter, lastDayInQuarter);

            return result;
        }
    }
}
