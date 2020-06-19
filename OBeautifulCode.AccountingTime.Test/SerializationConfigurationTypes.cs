// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializationConfigurationTypes.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime.Test
{
    using OBeautifulCode.AccountingTime.Serialization.Bson;
    using OBeautifulCode.AccountingTime.Serialization.Json;

    using OBeautifulCode.Serialization.Bson;
    using OBeautifulCode.Serialization.Json;

    public static class SerializationConfigurationTypes
    {
        public static BsonSerializationConfigurationType BsonSerializationConfigurationType => typeof(AccountingTimeBsonSerializationConfiguration).ToBsonSerializationConfigurationType();

        public static JsonSerializationConfigurationType JsonSerializationConfigurationType => typeof(AccountingTimeJsonSerializationConfiguration).ToJsonSerializationConfigurationType();
    }
}
