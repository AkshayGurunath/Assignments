using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.Exceptions
{
    public class InsufficientFundsException : ApplicationException
    {
        public decimal RequiredAmount { get; }
        public decimal AvailableAmount { get; }

        public InsufficientFundsException(decimal requiredAmount, decimal availableAmount)
            : base($"Insufficient funds: Required {requiredAmount:C}, but only {availableAmount:C} is available.")
        {
            RequiredAmount = requiredAmount;
            AvailableAmount = availableAmount;
        }
    }
}
