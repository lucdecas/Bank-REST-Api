using BankAccount.Domain.Models;

namespace BankAccount.Repository
{
    public class AccountRepository : IAccountRepository
    {

        public AccountRepository() { }

        public Account FindAccountById(int id)
        {
            // Busca pelo ID em um repositório com try catch para saber se account existe
            //TODO: Implementar método 
            return new Account(id, 0);
        }

        public void Save(Account account)
        {
            // Salvar em um repositório
        }

    }
}
