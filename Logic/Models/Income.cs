
using System;

namespace HomeBudget.Logic.Models
{
    public struct Income
    {
        public Income(FinOperationValue value, string name, Guid? id)
        {
            Name = name;
            Value = value;
            
        }

        public Guid Id { get; set; }
        public FinOperationValue Value { get; private set; }
        public string Name { get; private set; }
    }
}
