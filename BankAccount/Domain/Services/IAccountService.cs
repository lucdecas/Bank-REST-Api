using BankAccount.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.Domain.Services
{
    public interface IAccountService
    {
        Account getAccountStatements(int id);

        void depositCurrency(int id, int amount);

        void withdrawCurrency(int id, int amount);

        void transferCurrency(int id, int receiverId, int amount);

    }
}
