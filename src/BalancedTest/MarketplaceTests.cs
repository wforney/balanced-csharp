// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MarketplaceTest.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BalancedTest
{
    using Balanced;
    using Balanced.Errors;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     Class MarketplaceTest.
    /// </summary>
    [TestClass]
    public class MarketplaceTests : Tests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Tests the create.
        /// </summary>
        [TestMethod]
        public void TestCreate()
        {
            Settings.Configure(null);

            var key = new APIKey();
            key.Save();
            Settings.Configure(key.Secret);

            var mp = new Marketplace();
            Assert.IsNull(mp.id);
            mp.Save();

            Assert.IsNotNull(mp.id);
        }

        /// <summary>
        ///     Tests the double create.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Error))]
        public void TestDoubleCreate()
        {
            var mp = new Marketplace();
            mp.Save();
        }

        /// <summary>
        ///     Tests the mine.
        /// </summary>
        [TestMethod]
        public void TestMine()
        {
            Settings.Configure(null);

            var key = new APIKey();
            key.Save();
            Settings.Configure(key.Secret);

            var mp = new Marketplace();
            Assert.IsNull(mp.id);
            mp.Save();

            Assert.AreEqual(mp.id, Marketplace.Mine.id);
        }

        /// <summary>
        ///     Tests the no mine.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NoResultsFound))]
        public void TestNoMine()
        {
            Settings.Configure(null);

            var key = new APIKey();
            key.Save();
            Settings.Configure(key.Secret);

            var mp = Marketplace.Mine;
        }

        /// <summary>
        ///     Tests the tokenize bank account.
        /// </summary>
        [TestMethod]
        public void TestTokenizeBankAccount()
        {
            var ba = Mine.TokenizeBankAccount("Homer Jay", "112233a", "121042882");

            Assert.AreEqual(ba.Name, "Homer Jay");
            Assert.AreEqual(ba.AccountNumber, "xxx233a");
            Assert.AreEqual(ba.RoutingNumber, "121042882");
        }

        /// <summary>
        ///     Tests the tokenize card.
        /// </summary>
        [TestMethod]
        public void TestTokenizeCard()
        {
            // ReSharper disable StringLiteralTypo
            var card = Mine.TokenizeCard(
                "123 Fake Street", 
                "Jollywood", 
                null, 
                "90210", 
                "Homer Jay", 
                "4112344112344113", 
                "123", 
                12, 
                2013);

            // ReSharper restore StringLiteralTypo
            Assert.AreEqual(card.name, "Homer Jay");
            Assert.AreEqual(card.last_four, "4113");
            Assert.AreEqual(card.expiration_year, 2013);
            Assert.AreEqual(card.expiration_month, 12);
        }

        #endregion
    }
}