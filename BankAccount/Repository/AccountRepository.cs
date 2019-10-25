using BankAccount.Domain.Models;

namespace BankAccount.Repository
{
    public class AccountRepository : IAccountRepository
    {

        public AccountRepository() { }

        public void Save(Account account)
        {
            // Salvar em um repositório
        }

        public Account FindAccountById(int id)
        {
            // Busca pelo ID em um repositório com try catch para saber se account existe
            return new Account(id,new Currency(0));
        }

    }
}
