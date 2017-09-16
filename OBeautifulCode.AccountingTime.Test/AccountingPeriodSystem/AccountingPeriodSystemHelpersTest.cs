// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingPeriodSystemHelpersTest.cs" company="OBeautifulCode">
//    Copyright (c) OBeautifulCode 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace OBeautifulCode.AccountingTime.Test
{
    using System;

    using Xunit;

    public static class AccountingPeriodSystemHelpersTest
    {
        // ReSharper disable InconsistentNaming
        [Fact]
        public static void Next___Should_return_next_occurrence_of_specified_day_of_week___When_called()
        {
            var referenceDate1 = new DateTime(2010, 11, 21);
            Assert.Equal(new DateTime(2010, 11, 28), referenceDate1.Next(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 22), referenceDate1.Next(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 23), referenceDate1.Next(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 11, 24), referenceDate1.Next(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 11, 25), referenceDate1.Next(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 11, 26), referenceDate1.Next(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 11, 27), referenceDate1.Next(DayOfWeek.Saturday));

            var referenceDate2 = new DateTime(2010, 11, 22);
            Assert.Equal(new DateTime(2010, 11, 28), referenceDate2.Next(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 29), referenceDate2.Next(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 23), referenceDate2.Next(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 11, 24), referenceDate2.Next(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 11, 25), referenceDate2.Next(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 11, 26), referenceDate2.Next(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 11, 27), referenceDate2.Next(DayOfWeek.Saturday));

            var referenceDate3 = new DateTime(2010, 11, 23);
            Assert.Equal(new DateTime(2010, 11, 28), referenceDate3.Next(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 29), referenceDate3.Next(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 30), referenceDate3.Next(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 11, 24), referenceDate3.Next(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 11, 25), referenceDate3.Next(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 11, 26), referenceDate3.Next(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 11, 27), referenceDate3.Next(DayOfWeek.Saturday));

            var referenceDate4 = new DateTime(2010, 11, 24);
            Assert.Equal(new DateTime(2010, 11, 28), referenceDate4.Next(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 29), referenceDate4.Next(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 30), referenceDate4.Next(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 12, 1), referenceDate4.Next(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 11, 25), referenceDate4.Next(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 11, 26), referenceDate4.Next(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 11, 27), referenceDate4.Next(DayOfWeek.Saturday));

            var referenceDate5 = new DateTime(2010, 11, 25);
            Assert.Equal(new DateTime(2010, 11, 28), referenceDate5.Next(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 29), referenceDate5.Next(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 30), referenceDate5.Next(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 12, 1), referenceDate5.Next(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 12, 2), referenceDate5.Next(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 11, 26), referenceDate5.Next(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 11, 27), referenceDate5.Next(DayOfWeek.Saturday));

            var referenceDate6 = new DateTime(2010, 11, 26);
            Assert.Equal(new DateTime(2010, 11, 28), referenceDate6.Next(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 29), referenceDate6.Next(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 30), referenceDate6.Next(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 12, 1), referenceDate6.Next(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 12, 2), referenceDate6.Next(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 12, 3), referenceDate6.Next(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 11, 27), referenceDate6.Next(DayOfWeek.Saturday));

            var referenceDate7 = new DateTime(2010, 11, 27);
            Assert.Equal(new DateTime(2010, 11, 28), referenceDate7.Next(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 29), referenceDate7.Next(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 30), referenceDate7.Next(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 12, 1), referenceDate7.Next(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 12, 2), referenceDate7.Next(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 12, 3), referenceDate7.Next(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 12, 4), referenceDate7.Next(DayOfWeek.Saturday));
        }

        [Fact]
        public static void Previous___Should_return_occurrence_of_specified_day_of_week___When_called()
        {
            var referenceDate1 = new DateTime(2010, 11, 7);
            Assert.Equal(new DateTime(2010, 10, 31), referenceDate1.Previous(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 1), referenceDate1.Previous(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 2), referenceDate1.Previous(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 11, 3), referenceDate1.Previous(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 11, 4), referenceDate1.Previous(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 11, 5), referenceDate1.Previous(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 11, 6), referenceDate1.Previous(DayOfWeek.Saturday));

            var referenceDate2 = new DateTime(2010, 11, 8);
            Assert.Equal(new DateTime(2010, 11, 7), referenceDate2.Previous(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 1), referenceDate2.Previous(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 2), referenceDate2.Previous(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 11, 3), referenceDate2.Previous(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 11, 4), referenceDate2.Previous(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 11, 5), referenceDate2.Previous(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 11, 6), referenceDate2.Previous(DayOfWeek.Saturday));

            var referenceDate3 = new DateTime(2010, 11, 9);
            Assert.Equal(new DateTime(2010, 11, 7), referenceDate3.Previous(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 8), referenceDate3.Previous(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 2), referenceDate3.Previous(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 11, 3), referenceDate3.Previous(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 11, 4), referenceDate3.Previous(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 11, 5), referenceDate3.Previous(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 11, 6), referenceDate3.Previous(DayOfWeek.Saturday));

            var referenceDate4 = new DateTime(2010, 11, 10);
            Assert.Equal(new DateTime(2010, 11, 7), referenceDate4.Previous(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 8), referenceDate4.Previous(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 9), referenceDate4.Previous(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 11, 3), referenceDate4.Previous(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 11, 4), referenceDate4.Previous(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 11, 5), referenceDate4.Previous(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 11, 6), referenceDate4.Previous(DayOfWeek.Saturday));

            var referenceDate5 = new DateTime(2010, 11, 11);
            Assert.Equal(new DateTime(2010, 11, 7), referenceDate5.Previous(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 8), referenceDate5.Previous(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 9), referenceDate5.Previous(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 11, 10), referenceDate5.Previous(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 11, 4), referenceDate5.Previous(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 11, 5), referenceDate5.Previous(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 11, 6), referenceDate5.Previous(DayOfWeek.Saturday));

            var referenceDate6 = new DateTime(2010, 11, 12);
            Assert.Equal(new DateTime(2010, 11, 7), referenceDate6.Previous(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 8), referenceDate6.Previous(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 9), referenceDate6.Previous(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 11, 10), referenceDate6.Previous(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 11, 11), referenceDate6.Previous(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 11, 5), referenceDate6.Previous(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 11, 6), referenceDate6.Previous(DayOfWeek.Saturday));

            var referenceDate7 = new DateTime(2010, 11, 13);
            Assert.Equal(new DateTime(2010, 11, 7), referenceDate7.Previous(DayOfWeek.Sunday));
            Assert.Equal(new DateTime(2010, 11, 8), referenceDate7.Previous(DayOfWeek.Monday));
            Assert.Equal(new DateTime(2010, 11, 9), referenceDate7.Previous(DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2010, 11, 10), referenceDate7.Previous(DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2010, 11, 11), referenceDate7.Previous(DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2010, 11, 12), referenceDate7.Previous(DayOfWeek.Friday));
            Assert.Equal(new DateTime(2010, 11, 6), referenceDate7.Previous(DayOfWeek.Saturday));
        }

        // ReSharper restore InconsistentNaming
    }
}

// ReSharper restore CheckNamespace