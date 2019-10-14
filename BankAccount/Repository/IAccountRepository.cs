using BankAccount.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.Repository
{
    public interface IAccountRepository
    {
        void Save(Account account);

        Account FindAccountById(int id);
    }
}
