namespace CdWarehouse
{
    public class CD
    {
        private readonly string _cdTitle;
        private readonly string _cdArtist;
        private readonly int _chartPosition;
        private readonly double _price;
        private readonly IPayments _payments;
        private readonly ICharts _charts;

        public CD(string cdTitle, string cdArtist, int chartPosition, double price, int stock, IPayments payments, ICharts charts)
        {
            _cdTitle = cdTitle;
            _cdArtist = cdArtist;
            _chartPosition = chartPosition;
            _price = price;
            _payments = payments;
            _charts = charts;
            Stock = stock;
        }

        public double Stock { get; private set;  }

        public void Buy(string cardNumber, string expiry, string securityCode)
        {
            _payments.Charge(_price, cardNumber, expiry, securityCode);
            Stock--;
            _charts.Notify("sales: 1, album: " + _cdArtist + " - " + _cdTitle);
        }
    }
}