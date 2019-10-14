using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.Domain.Exceptions
{
    public class OperationNotAllowedException : Exception
    {
        public OperationNotAllowedException() { }

        public OperationNotAllowedException(string message) : base(message)
        {

        }
    }
}
