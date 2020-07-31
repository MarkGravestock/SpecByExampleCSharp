namespace CdWarehouse
{
    public interface IPayments
    {
        bool Charge(double price, string cardNumber, string expiry, string securityCode);
    }
}