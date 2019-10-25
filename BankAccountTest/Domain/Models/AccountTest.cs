using BankAccount.Domain.Exceptions;
using BankAccount.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccountTest.Domain.Models
{
    [TestClass]
    public class AccountTest
    {

        public int accountId = 1;
        public int currency = 100;

        [TestMethod]
        public void shouldCreateAccount()
        {
            Account account = new Account(accountId, currency);
        }

        [TestMethod]
        public void shouldDepositCurrencyWithTax()
        {
            Account account = new Account(accountId, currency);
            account.Deposit(100);
            Assert.AreEqual(199, account.Currency.Amount);
        }

        [TestMethod]
        public void shouldWithdrawCurrencyWithTax()
        {
            Account account = new Account(accountId, currency);
            account.Withdraw(50);
            Assert.AreEqual(46, account.Currency.Amount);
        }

        [TestMethod]
        public void shouldRequestTransferWithTax()
        {
            Account account = new Account(accountId, currency);
            account.RequestTransfer(50);
            Assert.AreEqual(49, account.Currency.Amount);
        }

        [TestMethod]
        public void shouldReceiveTransfer()
        {
            Account account = new Account(accountId, currency);
            account.ReceiveTransfer(50);
            Assert.AreEqual(150, account.Currency.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(OperationNotAllowedException))]
        public void shouldNotAllowWithdrawOnUnallowedAmount()
        {
            Account account = new Account(accountId, currency);
            account.Withdraw(150);
        }

        [TestMethod]
        [ExpectedException(typeof(OperationNotAllowedException))]
        public void shouldNotAllowTransferRequestOnUnallowedAmount()
        {
            Account account = new Account(accountId, currency);
            account.RequestTransfer(150);
        }

    }
}
