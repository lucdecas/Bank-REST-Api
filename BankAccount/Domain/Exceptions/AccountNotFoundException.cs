using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.Domain.Exceptions
{
    public class AccountNotFoundException : Exception
    {

        public AccountNotFoundException() { }

        public AccountNotFoundException(string message) : base(message)
        {

        }

    }
}
