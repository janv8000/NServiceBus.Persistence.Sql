﻿
namespace NServiceBus.Persistence.Sql.ScriptBuilder
{
    public class SagaDefinition
    {
        public SagaDefinition(string tableSuffix, string name, CorrelationProperty correlationProperty, CorrelationProperty transitionalCorrelationProperty = null)
        {
            Guard.AgainstNullAndEmpty(nameof(tableSuffix), tableSuffix);
            Guard.AgainstNull(nameof(correlationProperty), correlationProperty);
            Guard.AgainstNullAndEmpty(nameof(name), name);
            TableSuffix = tableSuffix;
            Name = name;
            CorrelationProperty = correlationProperty;
            TransitionalCorrelationProperty = transitionalCorrelationProperty;
        }

        public string TableSuffix { get; }
        public CorrelationProperty CorrelationProperty { get; }
        public CorrelationProperty TransitionalCorrelationProperty { get; }
        public string Name { get; }
    }
}