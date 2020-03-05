using DDDInPractice.Logic;
using DDDInPractice.UI.Common;

namespace Agap2IT.DDDInPractice.UI {
    public class SnackMachineViewModel : ViewModel {
        private readonly SnackMachine _snackMachine;
        public override string Caption => "Snack Machine";
        public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString ();
        public Money MoneyInside => _snackMachine.MoneyInside + _snackMachine.MoneyInTransaction;

        private string _message = "";

        public string Message {
            get { return _message; }
            private set {
                _message = value;
                Notify ();
            }
        }

        public Command InsertCentCommand { get; private set; }
        public Command InsertTenCentCommand { get; private set; }
        public Command InsertQuarterCommand { get; private set; }
        public Command InsertEuroCommand { get; private set; }
        public Command InsertFiveEuroCommand { get; private set; }
        public Command InsertTwentyEuroCommand { get; private set; }
        public Command ReturnMoneyCommand { get; private set; }
        public Command BuySnackCommand { get; private set; }
        public SnackMachineViewModel (SnackMachine snackMachine) {
            _snackMachine = snackMachine;
        }

    }
}