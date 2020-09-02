using CdWarehouse;
using Moq;
using Xunit;
using Xunit.Gherkin.Quick;

namespace SpecByExample.GherkinTest.BuyCd.Gherkin
{
    // Example GWT style test in this case the examples are more hidden in the fixtures, I've just included an example value from the specification,
    // but as Gojko points out we should considered eliminating inputs/outputs which are not related specifically to the aspect of the feature we are testing.
    [FeatureFile("./BuyCd/Gherkin/BuyCdInStock.feature")]
    public sealed class BuyCdInStockFeature : Feature
    {
        private const int InStock = 10;
        private const int OutsideTop100 = 123;
        
        readonly Mock<IPayments> mockPayments = new Mock<IPayments>();
        readonly Mock<ICharts> mockCharts = new Mock<ICharts>();

        private CD sut;
        private int chartPosition;
        private int stock;
        private bool paymentAccepted;

        private double chargedPrice;
        
        [Given(@"a CD that's not in the Top 100")]
        public void A_CD_thats_not_in_the_top_100()
        {
            chartPosition = OutsideTop100;
        }

        [And(@"we have it in stock")]
        public void we_have_it_in_stock()
        {
            stock = InStock;
        }

        [And(@"the customer's card payment will be accepted")]
        public void the_customers_card_payment_will_be_accepted()
        {
            paymentAccepted = true;
        }

        [When(@"The customer buys that CD priced at £(-?\d+(?:\.\d+)?)")]
        public void the_customer_buys_that_CD(double price)
        {
            string cdTitle = "So";
            string cdArtist = "Peter Gabriel";

            sut = new CD(cdTitle, cdArtist, chartPosition, price, stock, mockPayments.Object, mockCharts.Object);

            string cardNumber = "1234234534564567";
            string expiry = "10/21";
            string securityCode = "817";

            mockPayments.Setup(payments => payments.Charge(It.IsAny<double>(), It.IsAny<string>(), expiry, securityCode)).Returns(paymentAccepted)
                .Callback<double, string, string, string>((actualChargedPrice, actualCardNumber , actualExpiry, actualSecurityCode) =>
                {
                    chargedPrice = actualChargedPrice;
                });

            sut.Buy(cardNumber, expiry, securityCode);
        }

        [Then("One copy is deducted from CD's stock")]
        public void one_copy_is_deducted_from_the_cds_stock()
        {
            var expectedStock = stock - 1;
            Assert.Equal(expectedStock, sut.Stock);
        }

        [And(@"The customer's card is charged our price £(-?\d+(?:\.\d+)?) for that CD")]
        public void the_customers_card_is_charged_our_price_for_that_CD(double actualPrice)
        {
            Assert.Equal(actualPrice, chargedPrice);
        }

        [And("The charts are notified of the sale")]
        public void the_charts_are_notified_of_the_sale()
        {
            mockCharts.Verify(charts => charts.Notify(It.IsAny<string>()));
        }
    }
}