namespace ACEOMM.Domain.Model.Businesses
{
    public class Bank : Business
    {
        public Bank()
        {
            Type = BusinessType.Bank;
        }
        private BankType _bankType;
        public BankType BankType
        {
            get { return _bankType; }
            set { SetProperty(ref _bankType, value); }
        }
    }
}
