using BankAccount.Repository;
using BankAccount.Domain.Models;


namespace BankAccount.Domain.Services
{
    public class AccountService : IAccountService
    {

        IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository) {
            this.accountRepository = accountRepository;
        }

        public Account getAccountStatements(int id)
        {
            Account account = accountRepository.FindAccountById(id);
            account.Operations.Reverse();
            return account;
        }

        public void depositCurrency(int id, int amount)
        {
            Account account = accountRepository.FindAccountById(id);
            account.Deposit(amount);
            accountRepository.Save(account);
        }

        public void withdrawCurrency(int id, int amount)
        {
            Account account = accountRepository.FindAccountById(id);
            account.Withdraw(amount);
            accountRepository.Save(account);
        }

        public void transferCurrency(int id,int receiverId, int amount)
        {
            Account account = accountRepository.FindAccountById(id);
            Account receiverAccount = accountRepository.FindAccountById(receiverId);

            account.RequestTransfer(amount);
            receiverAccount.ReceiveTransfer(amount);

            //Os saves serão realizados através de uma transação
            accountRepository.Save(account);
            accountRepository.Save(receiverAccount);
        }
    }
}
