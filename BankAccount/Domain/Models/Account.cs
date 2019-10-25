using BankAccount.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace BankAccount.Domain.Models
{
    public class Account
    {

        private int id;
        private Currency currency;
        private List<Operation> operations;
        private int transferRequestTax = 1;
        private int withdrawTax = 4;
        private int depositTaxMultiplier = 100;

        public enum OperationType { Deposit, Withdraw, TransferRequest, TransferReceive }

        public class Operation
        {

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

        public Account(int id, int amount)
        {
            this.id = id;
            this.currency = new Currency(amount);
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
            int operationAmount = DefineOperationValue(value, OperationType.Deposit);
            this.Currency.Amount += operationAmount;
        }

        public void Withdraw(int value)
        {
            int operationAmount = DefineOperationValue(value, OperationType.Withdraw);
            ValidateOperation(operationAmount);
            this.Currency.Amount -= operationAmount;
        }

        public void RequestTransfer(int value)
        {
            int operationAmount = DefineOperationValue(value, OperationType.TransferRequest);
            ValidateOperation(operationAmount);
            this.Currency.Amount -= operationAmount;
        }

        public void ReceiveTransfer(int value)
        {
            int operationAmount = DefineOperationValue(value, OperationType.TransferReceive);
            this.Currency.Amount += operationAmount;
        }


        private int DefineOperationValue(int value, OperationType operationType)
        {
            int operationAmount = TaxOperation(value, operationType);
            operations.Add(new Operation(operationType, value, operationAmount - value));
            return operationAmount;
        }

        private void ValidateOperation(int value)
        {
            if (value > Currency.Amount)
            {
                throw new OperationNotAllowedException("Operação inválida");
            }
        }

        private int TaxOperation(int value, OperationType type)
        {
            switch (type)
            {
                case OperationType.Deposit:
                    return value - (value/depositTaxMultiplier);
                case OperationType.Withdraw:
                    return value + withdrawTax;
                case OperationType.TransferRequest:
                    return value + transferRequestTax;
                case OperationType.TransferReceive:
                    return value;
                default:
                    return value;
            }
        }

    }
}