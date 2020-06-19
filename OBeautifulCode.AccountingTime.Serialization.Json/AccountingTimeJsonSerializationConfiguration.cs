// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTimeJsonSerializationConfiguration.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Serialization.Json
{
    using System.Collections.Generic;
    using System.Linq;

    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Json;

    using static System.FormattableString;

    /// <inheritdoc />
    public class AccountingTimeJsonSerializationConfiguration : JsonSerializationConfigurationBase
    {
        /// <inheritdoc />
        protected override IReadOnlyCollection<string> TypeToRegisterNamespacePrefixFilters => new[] { Invariant($"{nameof(OBeautifulCode)}.{nameof(AccountingTime)}") };

        /// <inheritdoc />
        protected override IReadOnlyCollection<TypeToRegisterForJson> TypesToRegisterForJson => new[]
            {
                typeof(AccountingPeriodSystem).ToTypeToRegisterForJson(),
            }
            .Concat(BuildTypesToRegisterWithConverters())
            .ToList();

        private static IReadOnlyCollection<TypeToRegisterForJson> BuildTypesToRegisterWithConverters()
        {
            var result = new List<TypeToRegisterForJson>();

            var unitOfTimeTypes = TypeHelper.GetAllUnitOfTimeTypes().ToList();

            var unitOfTimeConverter = new UnitOfTimeJsonConverter();

            var unitOfTimeConverterBuilder = new JsonConverterBuilder(
                "unit-of-time-converter",
                () => unitOfTimeConverter,
                () => unitOfTimeConverter,
                JsonConverterOutputKind.String);

            foreach (var unitOfTimeType in unitOfTimeTypes)
            {
                var unitOfTimeTypeToRegister = new TypeToRegisterForJson(
                    unitOfTimeType,
                    MemberTypesToInclude.None,
                    RelatedTypesToInclude.None,
                    unitOfTimeConverterBuilder);

                result.Add(unitOfTimeTypeToRegister);
            }

            var reportingPeriodConverter = new ReportingPeriodJsonConverter();

            var reportingPeriodConverterBuilder = new JsonConverterBuilder(
                "reporting-period-converter",
                () => reportingPeriodConverter,
                () => reportingPeriodConverter,
                JsonConverterOutputKind.String);

            var reportingPeriodTypeToRegister = new TypeToRegisterForJson(
                typeof(ReportingPeriod),
                MemberTypesToInclude.None,
                RelatedTypesToInclude.None,
                reportingPeriodConverterBuilder);

            result.Add(reportingPeriodTypeToRegister);

            return result;
        }
    }
}
