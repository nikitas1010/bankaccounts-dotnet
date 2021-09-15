using System;
using System.Collections.Generic;

namespace myBank
{
    public class BankAccount
    {
        private static int accountNumberSeed = 1234567890;

        private readonly decimal minimumBalance;

        //for regular accounts, initial balance and minimum is 0
        public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0) { }
        //used for LOC, where min can be negative. e.g. -5000, based on credit limit
        public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
        {
            this.Owner = name;
            this.Number = (accountNumberSeed + 1).ToString();
            accountNumberSeed++;

            this.minimumBalance = minimumBalance;
            if (initialBalance > 0)
            {
                MakeDeposit(initialBalance, DateTime.Now, "Initial Deposit");
            }
        }
        protected List<Transaction> allTransactions = new List<Transaction>();

        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }
                return balance;
            }
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            // this.Balance += amount;            
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, date, note); //just floating in air
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
               throw new ArgumentOutOfRangeException("amount", amount, "Amount of " +
                    "withdrawal has to be positive and less than minimum balance");
            
            var overdraftTransaction = CheckWithdrawalLimit(Balance - amount < minimumBalance);
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
            if (overdraftTransaction != null)
                allTransactions.Add(overdraftTransaction);
           
        }
        /*The added method is protected, which means that it can be called only from derived classes. 
         * That declaration prevents other clients from calling the method. It's also virtual so that 
         * derived classes can change the behavior. The return type is a Transaction?. The ? annotation 
         * indicates that the method may return null. */
        protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if (isOverdrawn)
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            else
                return default;
        }

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }

            return report.ToString();
        }

        public virtual void PerformMonthEndTransactions() {}
        //public abstract void PerformMonthEndTransactions() { } // then derived class MUST override. no implementation provides

    }
}