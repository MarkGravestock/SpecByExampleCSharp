using System;
using System.Collections.Generic;
using System.Globalization;
using CdWarehouse;
using Concordion.Runners.NUnit;
using Moq;
using NUnit.Framework;

namespace SpecByExample.Test.BuyCd
{
    [TestFixture]
    public class BuyCdInStockFixture : ExecutableSpecification
    {
        public Dictionary<String, String> BuyCdInStock(string cdTitle, string cdArtist, int chartPosition, int stock, double price,
            string cardNumber, string expiry, string securityCode, bool paymentAccepted)
        {
            var mockPayments = new Mock<IPayments>();

            double chargedPrice = 1.0;
            string chargedCardNumber = null;

            mockPayments.Setup(payments => payments.Charge(It.IsAny<double>(), It.IsAny<string>(), expiry, securityCode)).Returns(paymentAccepted)
            .Callback<double, string, string, string>((actualChargedPrice, actualCardNumber , actualExpiry, actualSecurityCode) =>
            {
                 chargedPrice = actualChargedPrice;
                 chargedCardNumber = actualCardNumber;
            });

            var mockCharts = new Mock<ICharts>();
            
            string chartNotification = null;
            mockCharts.Setup(charts => charts.Notify(It.IsAny<string>())).Callback<string>(message => chartNotification = message);
            
            var cd = new CD(cdTitle, cdArtist, chartPosition, price, stock, mockPayments.Object, mockCharts.Object);
            
            cd.Buy(cardNumber, expiry, securityCode);
            
            return new Dictionary<String, String> {{"endStock", cd.Stock.ToString(CultureInfo.InvariantCulture)}, {"chargedPrice", chargedPrice.ToString(CultureInfo.InvariantCulture)}, {"chargedCardNumber", chargedCardNumber}, {"chartNotification", chartNotification}};
        }        
    }
}