// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BankAccountTest.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BalancedTest
{
    using Balanced;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     Class BankAccountTests.
    /// </summary>
    [TestClass]
    public class BankAccountTests : Tests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Tests the create.
        /// </summary>
        [TestMethod]
        public void TestCreate()
        {
            var ba = new BankAccount
                         {
                             Name = "Homer Jay",
                             AccountNumber = "112233a",
                             RoutingNumber = "121042882",
                             Type = BankAccount.Checking
                         };
        }

        #endregion
    }
}