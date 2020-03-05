using System;
using Agap2IT.DDDInPractice.Logic;
using FluentAssertions;
using Xunit;
using static Agap2IT.DDDInPractice.Logic.Money;

namespace Agap2IT.DDDInPractice.Tests {
    public class SnackMachineSpecs {
        [Fact]
        public void Return_money_empties_money_in_transaction () {
            var snackMachine = new SnackMachine ();
            snackMachine.InsertMoney (Euro);
            snackMachine.ReturnMoney ();
            snackMachine.MoneyInTransaction.Amount.Should ().Be (0m);
        }

        [Fact]
        public void Inserted_money_goes_to_money_in_transaction () {
            var snackMachine = new SnackMachine ();
            snackMachine.InsertMoney (Cent);
            snackMachine.InsertMoney (Euro);
            snackMachine.MoneyInTransaction.Amount.Should ().Be (1.01m);
        }

        [Fact]
        public void Cannot_insert_more_than_one_coin_or_note_at_time () {
            var snackMachine = new SnackMachine ();
            var twoCent = Cent + Cent;

            Action action = () => snackMachine.InsertMoney (twoCent);
            action.Should ().Throw<InvalidOperationException> ();
        }

        [Fact]
        public void Money_in_transaction_goes_to_money_inside_after_purchase(){
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Euro);
            snackMachine.InsertMoney(Euro);

            snackMachine.BuySnack();

            snackMachine.MoneyInTransaction.Should().Be(None);
            snackMachine.MoneyInside.Amount.Should().Be(2m);
        }


    }
}