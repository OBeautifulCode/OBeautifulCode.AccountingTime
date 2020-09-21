// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCommon.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.CodeAnalysis.Recipes;

    public static class TestCommon
    {
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = ObcSuppressBecause.CA2104_DoNotDeclareReadOnlyMutableReferenceTypes_TypeIsImmutable)]
        public static readonly IReadOnlyCollection<Type> AllUnitOfTimeTypesExceptUnitOfTime = TypeHelper.GetAllUnitOfTimeTypes().Except(new[] { typeof(UnitOfTime) }).ToArray();

        public static IReadOnlyCollection<UnitOfTime> GetDummyOfEachUnitOfTimeKind()
        {
            var result = TypeHelper.GetAllUnitOfTimeTypes().Where(_ => !_.IsAbstract).Select(_ => (UnitOfTime)AD.ummy(_)).ToList();

            return result;
        }
    }
}
