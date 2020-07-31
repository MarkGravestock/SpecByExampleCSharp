using CdWarehouse;
using Moq;
using NUnit.Framework;

namespace SpecByExample.Test.BuyCd
{
    [TestFixture("So", "Peter Gabriel", 101, 10, 9.99, "1234234534564567", "10/21", "817", true, 9, 9.99,
        "sales: 1, album: Peter Gabriel - So")]
    [TestFixture("Lionheart", "Kate Bush", 279, 5, 8.99, "1234234534564567", "10/21", "817", true, 4, 8.99,
        "sales: 1, album: Kate Bush - Lionheart")]
    public class BuyCdInStockTest
    {
        private readonly string _cardNumber;
        private readonly string _expiry;
        private readonly string _securityCode;
        private readonly int _endStock;
        private readonly CD _cd;
        private readonly Mock<IPayments> _mockPayments;
        private readonly double _charged;
        private readonly Mock<ICharts> _mockCharts;
        private readonly string _chartNotification;

        public BuyCdInStockTest(string cdTitle, string cdArtist, int chartPosition, int stock, double price,
            string cardNumber, string expiry, string securityCode, bool paymentAccepted, int endStock, double charged,
            string chartNotification)
        {
            _cardNumber = "1234234534564567";
            _expiry = "10/21";
            _securityCode = "817";
            _endStock = endStock; 
            _charged = charged;
            _chartNotification = chartNotification;
            
            _mockPayments = new Mock<IPayments>();
            _mockPayments.Setup(payments => payments.Charge(_charged, _cardNumber, _expiry, _securityCode))
                .Returns(paymentAccepted);

            _mockCharts = new Mock<ICharts>();
            
            _cd = new CD(cdTitle, cdArtist, chartPosition, price, stock, _mockPayments.Object, _mockCharts.Object);
            
            _cd.Buy(_cardNumber, _expiry, _securityCode);
        }

        [Test]
        public void OneCopyDeductedFromCdStock()
        {
            Assert.AreEqual(_endStock, _cd.Stock);
        }

        [Test]
        public void CustomersCardChargedOurPriceForCd()
        {
            _mockPayments.Verify(payments => payments.Charge(_charged, _cardNumber, _expiry, _securityCode));
        }

        [Test]
        public void ChartsNotifiedOfSale()
        {
            _mockCharts.Verify(charts => charts.Notify(_chartNotification));
        }
    }
}