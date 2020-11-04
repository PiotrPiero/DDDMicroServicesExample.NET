using HomeBudget.Core;
using System;
using HomeBudget.Core.Impl;

namespace HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate
{
    public class FinOperationValue : IValueObject
    {

        public FinOperationValue(decimal gross, decimal net)
        {
            if (gross == 0 && net != 0)
                throw new ArgumentException("Operation value has to have gross price");

            Gross = gross;
            Net = net;
        }

        public decimal Gross { get; private set; }
        public decimal Net { get; private set; }

        public static decimal CalculateGross(decimal taxPercentage, decimal net) =>
            net * taxPercentage;

        public static decimal CalculateNet(decimal taxPercentage, decimal gross) =>
            gross * (100 - taxPercentage);

    }
}
