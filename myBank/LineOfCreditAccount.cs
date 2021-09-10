using System;
namespace myBank
{
    public class LineOfCreditAccount : BankAccount
    {
        //private decimal _interest = 0m;
        public decimal interest = 0m;
        //private decimal _creditLimit = 0m;

        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name, initialBalance, -creditLimit)
        //=> _interest = interest;
        //=> _creditLimit = creditLimit;
        { }


        public override void PerformMonthEndTransactions()
        {
            if (Balance != 0) {
                MakeWithdrawal(Balance * interest, DateTime.Now, "interest charged");
            }

        }
        //if balance + withdrawal > creditLimit{ charge fee}

        protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) =>
        isOverdrawn
        ? new Transaction(-20, DateTime.Now, "Apply overdraft fee")
        : default;
            
        
    }
}
