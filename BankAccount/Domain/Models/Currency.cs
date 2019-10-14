using System.Runtime.Serialization;

namespace BankAccount.Domain.Models
{

  [DataContract]
  public class Currency {

        private int amount;

        public Currency(int amount)
        {
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
