// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BankAccountVerification.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced
{
    using System.Collections.Generic;

    /// <summary>
    ///     Class BankAccountVerification.
    /// </summary>
    public class BankAccountVerification : Resource
    {
        #region Constants

        /// <summary>
        ///     The failed
        /// </summary>
        public const string Failed = "failed";

        /// <summary>
        ///     The pending
        /// </summary>
        public const string Pending = "pending";

        /// <summary>
        ///     The verified
        /// </summary>
        public const string Verified = "verified";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BankAccountVerification" /> class.
        /// </summary>
        public BankAccountVerification()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountVerification"/> class. 
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        public BankAccountVerification(string uri)
            : base(uri)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccountVerification"/> class.
        /// </summary>
        /// <param name="payload">
        /// The payload.
        /// </param>
        public BankAccountVerification(IDictionary<string, object> payload)
            : base(payload)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the attempts.
        /// </summary>
        /// <value>The attempts.</value>
        public long Attempts { get; set; }

        /// <summary>
        /// Gets or sets the remaining attempts.
        /// </summary>
        /// <value>The remaining attempts.</value>
        public long RemainingAttempts { get; set; }

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Confirms the specified amounts.
        /// </summary>
        /// <param name="amount1">The amount1.</param>
        /// <param name="amount2">The amount2.</param>
        public void Confirm(int amount1, int amount2)
        {
            var data = new Dictionary<string, object>();
            data["amount_1"] = amount1;
            data["amount_2"] = amount2;
        }

        #endregion

        /// <summary>
        ///     Class Collection.
        /// </summary>
        public class Collection : ResourceCollection<BankAccountVerification>
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Collection"/> class.
            /// </summary>
            /// <param name="uri">
            /// The URI.
            /// </param>
            public Collection(string uri)
                : base(uri)
            {
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            ///     Creates this instance.
            /// </summary>
            /// <returns>The BankAccountVerification.</returns>
            public BankAccountVerification Create()
            {
                return this.Create(new Dictionary<string, object>());
            }

            #endregion
        }
    }
}