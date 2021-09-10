using System;
namespace myBank
{
    public class GiftCardAccount : BankAccount
    {
        private decimal _monthtlyDeposit = 0m;

        public GiftCardAccount(string name, decimal initialBalance, decimal monthtlyDeposit = 0) : base(name, initialBalance)
            => _monthtlyDeposit = monthtlyDeposit;

        public override void PerformMonthEndTransactions()
        {
            if (_monthtlyDeposit !=0)
            {
                MakeDeposit(_monthtlyDeposit, DateTime.Now, "top up card");
            }
        }

    }
}
