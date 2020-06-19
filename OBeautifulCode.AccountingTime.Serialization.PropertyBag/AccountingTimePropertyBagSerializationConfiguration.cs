// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimePropertyBagSerializationConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.PropertyBag
{
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.PropertyBag;

    using static System.FormattableString;

    /// <inheritdoc />
    public class AccountingTimePropertyBagSerializationConfiguration : PropertyBagSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<string> TypeToRegisterNamespacePrefixFilters => new[] { Invariant($"{nameof(OBeautifulCode)}.{nameof(AccountingTime)}") };

        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForPropertyBag> TypesToRegisterForPropertyBag => BuildTypesToRegisterWithSerializers();

        private static IReadOnlyCollection<TypeToRegisterForPropertyBag> BuildTypesToRegisterWithSerializers()
        {
            var result = new List<TypeToRegisterForPropertyBag>();

            var unitOfTimeTypes = TypeHelper.GetAllUnitOfTimeTypes().ToList();

            var unitOfTimeStringSerializer = new UnitOfTimeStringSerializer();

            foreach (var unitOfTimeType in unitOfTimeTypes)
            {
                var unitOfTimeTypeToRegister = new TypeToRegisterForPropertyBag(
                    unitOfTimeType,
                    MemberTypesToInclude.None,
                    RelatedTypesToInclude.None,
                    () => unitOfTimeStringSerializer);

                result.Add(unitOfTimeTypeToRegister);
            }

            var reportingPeriodStringSerializer = new ReportingPeriodStringSerializer();

            var reportingPeriodTypeToRegister = new TypeToRegisterForPropertyBag(
                typeof(ReportingPeriod),
                MemberTypesToInclude.None,
                RelatedTypesToInclude.None,
                () => reportingPeriodStringSerializer);

            result.Add(reportingPeriodTypeToRegister);

            return result;
        }
    }
}
