using System;

namespace HomeBudget.Logic.Models
{
    public struct FinOperationValue
    {
        public FinOperationValue(decimal gross)
        {
            Gross = gross;
        }

        public FinOperationValue(decimal net)
        {
            Net = net;
        }

        public FinOperationValue(decimal gross, decimal net)
        {
            Gross = gross;
            Net = net;
        }

        public decimal Gross { get; private set; }
        public decimal Net { get; private set; }

        public FinOperationValue SetGross(int taxPercentage)
        {
            Gross = Net * taxPercentage / 100;
            return this;
        }

        public FinOperationValue SetNet(int taxPercentage)
        {
            Net = Gross * (100 - taxPercentage) / 100;

            return this;
        }
    }
}
