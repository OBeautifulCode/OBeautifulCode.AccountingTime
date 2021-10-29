// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITimeseries.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A series of <see cref="IDatapoint"/>.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "This interface exists as a marker/way to identify a group of types and should be used at compile-time, not runtime.")]
    public interface ITimeseries
    {
    }
}
