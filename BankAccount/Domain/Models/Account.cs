using BankAccount.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BankAccount.Domain.Models
{
    public class Account
    {

        private int id;
        private Currency currency;
        private List<Operation> operations;

        public class Operation
        {
            public enum OperationType{Deposit,Withdraw,Transfer}

            public string operationType;
            public int operationValue;
            public int taxPayed;
            public string operationDate;


            public Operation(OperationType operationType, int operationValue, int taxPayed)
            {
                this.operationType = operationType.ToString();
                this.operationValue = operationValue;
                this.taxPayed = taxPayed;
                operationDate = DateTime.Now.ToString();
            }
        }

        public Account(int id, Currency currency)
        {
            this.id = id;
            this.currency = currency;
             operations = new List<Operation>();
    }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public Currency Currency
        {
            get
            {
                return currency;
            }

            set
            {
                currency = value;
            }
        }

        public List<Operation> Operations
        {
            get
            {
                return operations;
            }
        }

        public void Deposit(int value)
        {
            int tax = (value/100);
            int taxedAmount = value - tax;
            this.Currency.Amount += taxedAmount;
            operations.Add(new Operation(Operation.OperationType.Deposit, value, tax));
        }

        public void Withdraw(int value)
        {
            int tax = 4;
            int taxedAmount = value + tax;
            if(taxedAmount > Currency.Amount)
            {
                throw new OperationNotAllowedException("Operação inválida");
            }

            this.Currency.Amount -= taxedAmount;
            operations.Add(new Operation(Operation.OperationType.Withdraw, value, tax));
        }

        public void RequestTransfer(int value)
        {
            int tax = 1;
            int taxedAmount = value + tax;
            if (taxedAmount > Currency.Amount)
            {
                throw new OperationNotAllowedException("Operação inválida");
            }

            this.Currency.Amount -= taxedAmount;
            operations.Add(new Operation(Operation.OperationType.Transfer, value, tax));
        }

        public void ReceiveTransfer(int value)
        {
            this.Currency.Amount += value;
            operations.Add(new Operation(Operation.OperationType.Transfer, value, 0));
        }
    }
}