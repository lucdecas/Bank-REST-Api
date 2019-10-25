using BankAccount.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccount.Domain.Models;
using BankAccount.Domain.Services;
using Moq;
using BankAccount.Domain.Exceptions;

namespace BankAccountTest.Domain.Services
{
    [TestClass]
    public class AccountServiceTest
    {
        Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
        
        private int currency = 1000;
        private int accountId = 1;

        [TestMethod]
        public void ShouldReturnCurrencyAmountFromAccount()
        {
            Account account = new Account(accountId, currency);

            AccountService accountService = new AccountService(mock.Object);
            mock.Setup(m => m.FindAccountById(accountId)).Returns(account);

            Account resultAccount = accountService.getAccountStatements(accountId);
            Assert.AreEqual(currency, resultAccount.Currency.Amount);
        }

        [TestMethod]
        public void ShouldDepositOnAccount() {
            Account account = new Account(accountId, currency);

            AccountService accountService = new AccountService(mock.Object);
            mock.Setup(m => m.FindAccountById(accountId)).Returns(account);

            accountService.depositCurrency(accountId,100);
            Assert.AreEqual(1099,account.Currency.Amount);

        }

        [TestMethod]
        public void ShouldWithdrawFromAccount()
        {
            Account account = new Account(accountId, currency);

            AccountService accountService = new AccountService(mock.Object);
            mock.Setup(m => m.FindAccountById(accountId)).Returns(account);

            accountService.withdrawCurrency(accountId, 100);
            Assert.AreEqual(896,account.Currency.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(OperationNotAllowedException))]
        public void ShouldErrorOnUnallowedWithdrawRequest()
        {
            Account account = new Account(accountId, currency);

            AccountService accountService = new AccountService(mock.Object);
            mock.Setup(m => m.FindAccountById(accountId)).Returns(account);

            accountService.withdrawCurrency(accountId, 1500);
        }

        [TestMethod]
        public void ShouldTransferCurrency()
        {
            int receiverAccountId = 2;
            Account account = new Account(accountId, currency);
            Account receiverAccount = new Account(receiverAccountId,currency);

            AccountService accountService = new AccountService(mock.Object);
            mock.Setup(m => m.FindAccountById(accountId)).Returns(account);
            mock.Setup(m => m.FindAccountById(receiverAccountId)).Returns(receiverAccount);

            accountService.transferCurrency(accountId,receiverAccountId, 300);
            Assert.AreEqual(699,account.Currency.Amount);
            Assert.AreEqual(1300,receiverAccount.Currency.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(OperationNotAllowedException))]
        public void ShouldErrorOnUnallowedTransferRequest()
        {
            int receiverAccountId = 2;
            Account account = new Account(accountId, currency);
            Account receiverAccount = new Account(receiverAccountId, currency);

            AccountService accountService = new AccountService(mock.Object);
            mock.Setup(m => m.FindAccountById(accountId)).Returns(account);
            mock.Setup(m => m.FindAccountById(receiverAccountId)).Returns(receiverAccount);

            accountService.transferCurrency(accountId, receiverAccountId, 1500);
        }


    }
}
