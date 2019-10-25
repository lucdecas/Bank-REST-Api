using System;
using System.Runtime.Serialization;

namespace BankAccount.Domain.Models
{

  [DataContract]
  public class Currency {

        private int amount;

        public Currency(int amount)
        {
            if (amount < 0)
            {
                throw new Exception("Invalid amount: " + amount);
            }
            this.amount = amount;
        }

        public int Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
            }
        }
}
}
