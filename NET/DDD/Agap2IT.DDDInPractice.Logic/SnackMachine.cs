using System;
using System.Linq;
using static Agap2IT.DDDInPractice.Logic.Money;

namespace Agap2IT.DDDInPractice.Logic {
    public sealed class SnackMachine : Entity {
        public Money MoneyInside { get; private set; } = None;
        public Money MoneyInTransaction { get; private set; } = None;
        public void InsertMoney (Money money) {
            Money[] coinsAndNotes = { Cent, TenCent, Quarter, Euro, FiveEuro, TwentyEuro };
            if(!coinsAndNotes.Contains(money))
                throw new InvalidOperationException();
            MoneyInTransaction += money;
        }
        public void ReturnMoney () {
            MoneyInTransaction = None;
        }
        public void BuySnack () {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }

    }
}