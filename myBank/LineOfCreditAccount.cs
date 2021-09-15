using System;
namespace myBank
{
    public class LineOfCreditAccount : BankAccount
    {
        //private decimal _interest = 0m;
        public decimal interest = 0.07m;
        //private decimal _creditLimit = 0m;

        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name, initialBalance, -creditLimit)
        //=> _interest = interest;
        //=> _creditLimit = creditLimit;
        { }


        //public override void PerformMonthEndTransactions()
        //{
        //    if (Balance != 0)   
        //        MakeWithdrawal(Math.Abs(Balance) * interest, DateTime.Now, "interest charged");
        //}
        public override void PerformMonthEndTransactions()
        {
            if (Balance < 0)
            {
                // Negate the balance to get a positive interest charge:
                var interest = -Balance * 0.07m;
                MakeWithdrawal(interest, DateTime.Now, "Charge monthly interest");
            }
        }
        //if balance + withdrawal > creditLimit{ charge fee}

        protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) =>
        isOverdrawn
        ? new Transaction(-20, DateTime.Now, "Apply overdraft fee")
        : default;
            
        
    }
}
