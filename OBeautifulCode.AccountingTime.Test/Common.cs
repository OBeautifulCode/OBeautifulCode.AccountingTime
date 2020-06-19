// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Common.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.AutoFakeItEasy;

    internal static class Common
    {
        public static readonly Type[] AllUnitOfTimeTypesExceptUnitOfTime = TypeHelper.GetAllUnitOfTimeTypes().Except(new[] { typeof(UnitOfTime) }).ToArray();

        public static IReadOnlyCollection<UnitOfTime> GetDummyOfEachUnitOfTimeKind()
        {
            var result = TypeHelper.GetAllUnitOfTimeTypes().Where(_ => !_.IsAbstract).Select(_ => (UnitOfTime)AD.ummy(_)).ToList();

            return result;
        }
    }
}
