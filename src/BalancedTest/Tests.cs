// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Test.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BalancedTest
{
    using System;

    using Balanced;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     Class Test.
    /// </summary>
    [TestClass]
    public class Tests
    {
        #region Static Fields

        /// <summary>
        ///     The location
        /// </summary>
        protected static string Location = "https://api.balancedpayments.com";

        /// <summary>
        ///     The mine
        /// </summary>
        protected static Marketplace Mine;

        /// <summary>
        ///     The secret
        /// </summary>
        protected static string Secret = "fbb068c6c76e11e2b025026ba7f8ec28";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Tests" /> class.
        /// </summary>
        static Tests()
        {
            string location = Environment.GetEnvironmentVariable("BALANCED_LOCATION");
            if (location != null)
            {
                Location = location;
            }

            string secret = Environment.GetEnvironmentVariable("BALANCED_SECRET");
            if (secret != null)
            {
                Secret = secret;
            }

            Settings.Configure(Secret, Location);
            Mine = Marketplace.Mine;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Settings.Configure(Secret, Location);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the bank account.
        /// </summary>
        /// <param name="mp">
        /// The implementation.
        /// </param>
        /// <returns>
        /// The bank account.
        /// </returns>
        protected BankAccount CreateBankAccount(Marketplace mp)
        {
            if (mp == null)
            {
                mp = Mine;
            }

            return mp.TokenizeBankAccount("Homer Jay", "112233a", "121042882");
        }

        #endregion
    }
}