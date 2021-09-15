using System;
//using myApp;

namespace myBank
{
    interface IVisualise
    {
        void Visualise(); // interface method
    }

    interface ICheckInterest
    {
        Decimal CheckInterest(Decimal amount);
    }


    public class InterestEarningAccount : BankAccount, IVisualise, ICheckInterest
    {
        public InterestEarningAccount(string name, decimal initialBalance) : base(name, initialBalance)
        {
        }

        public void Visualise()
        {
            var reportVisual = new System.Text.StringBuilder();

            decimal balanceInterest = 0;
            //reportVisual.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in allTransactions)
            {
                balanceInterest += item.Amount;
                if (item.Notes.Equals("apply monthly interest")){
                    reportVisual.AppendLine($"Congrats, you earned interest at {item.Date.ToShortDateString()} of ${item.Amount}.");
                }
                
            }
            Console.WriteLine(reportVisual.ToString());

        }

        public Decimal CheckInterest(Decimal amount)
        {   
            return amount * 0.05m;
        }

        public override void PerformMonthEndTransactions()
        {
            if (Balance > 500m)
            {
                var interest = Balance * 0.05m;
                MakeDeposit(interest, DateTime.Now, "apply monthly interest");
            }
        }
        }
}
