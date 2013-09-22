// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BankAccountVerificationTests.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BalancedTest
{
    using Balanced;
    using Balanced.Errors;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     Class BankAccountVerificationTests.
    /// </summary>
    [TestClass]
    public class BankAccountVerificationTests : Tests
    {
        #region Fields

        /// <summary>
        /// Gets or sets the backup bank account.
        /// </summary>
        /// <value>The backup bank account.</value>
        protected internal BankAccount BackupBankAccount { get; set; }

        /// <summary>
        /// Gets or sets the bank account verification.
        /// </summary>
        /// <value>The bank account verification.</value>
        /// ReSharper disable once IdentifierTypo
        protected internal BankAccountVerification BAV { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        [TestInitialize]
        public new void Initialize()
        {
            base.Initialize();
            this.BackupBankAccount = this.CreateBankAccount(null);
            this.BAV = this.BackupBankAccount.Verify();
        }

        /// <summary>
        ///     Tests the double confirm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(BankAccountVerificationFailure))]
        public void TestDoubleConfirm()
        {
            this.BAV.Confirm(1, 1);
            this.BAV.Confirm(1, 1);
        }

        /// <summary>
        ///     Tests the exhausted confirm.
        /// </summary>
        [TestMethod]
        public void TestExhaustedConfirm()
        {
            while (this.BAV.RemainingAttempts != 1)
            {
                try
                {
                    this.BAV.Confirm(12, 13);
                }
                catch (BankAccountVerificationFailure)
                {
                    this.BAV.Refresh();
                    Assert.AreEqual(BankAccountVerification.Pending, this.BAV.State);
                }
            }

            try
            {
                this.BAV.Confirm(12, 13);
            }
            catch (BankAccountVerificationFailure)
            {
                this.BAV.Refresh();
                Assert.AreEqual(BankAccountVerification.Failed, this.BAV.State);
            }

            Assert.AreEqual(this.BAV.RemainingAttempts, 0);
            this.BAV = this.BackupBankAccount.Verify();
            this.BAV.Confirm(1, 1);
            Assert.AreEqual(BankAccountVerification.Verified, this.BAV.State);
        }

        /// <summary>
        ///     Tests the failed confirm.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(BankAccountVerificationFailure))]
        public void TestFailedConfirm()
        {
            this.BAV.Confirm(12, 13);
        }

        #endregion
    }
}