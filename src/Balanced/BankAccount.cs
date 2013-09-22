// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BankAccount.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Class BankAccount.
    /// </summary>
    public class BankAccount : Resource
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="BankAccount"/> class.
        /// </summary>
        static BankAccount()
        {
            Savings = "savings";
            Checking = "checking";
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BankAccount" /> class.
        /// </summary>
        public BankAccount()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccount"/> class.
        ///     Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        public BankAccount(string uri)
            : base(uri)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccount"/> class.
        /// </summary>
        /// <param name="payload">
        /// The payload.
        /// </param>
        public BankAccount(Dictionary<string, object> payload)
            : base(payload)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the checking.
        /// </summary>
        /// <value>The checking.</value>
        public static string Checking { get; private set; }

        /// <summary>
        ///     Gets the query.
        /// </summary>
        /// <value>The query.</value>
        public static ResourceQuery<Marketplace> Query
        {
            get
            {
                return new ResourceQuery<Marketplace>(typeof(Marketplace), "/v1/bank_accounts");
            }
        }

        /// <summary>
        ///     Gets the savings.
        /// </summary>
        /// <value>The savings.</value>
        public static string Savings { get; private set; }

        /// <summary>
        ///     Gets or sets the account number.
        /// </summary>
        /// <value>The account number.</value>
        public string AccountNumber { get; set; }

        /// <summary>
        ///     Gets or sets the name of the bank.
        /// </summary>
        /// <value>The name of the bank.</value>
        public string BankName { get; set; }

        /// <summary>
        ///     Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     Gets or sets the fingerprint.
        /// </summary>
        /// <value>The fingerprint.</value>
        public string Fingerprint { get; set; }

        /// <summary>
        ///     Gets or sets the meta.
        /// </summary>
        /// <value>The meta.</value>
        public IDictionary<string, object> Meta { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the root_uri.
        /// </summary>
        /// <value>The root_uri.</value>
        public override string RootURI
        {
            get
            {
                return "/v1/bank_accounts";
            }
        }

        /// <summary>
        ///     Gets or sets the routing number.
        /// </summary>
        /// <value>The routing number.</value>
        public string RoutingNumber { get; set; }

        /// <summary>
        ///     Gets or sets the verification URI.
        /// </summary>
        /// <value>The verification URI.</value>
        public string VerificationURI { get; set; }

        /// <summary>
        ///     Gets or sets the verifications.
        /// </summary>
        /// <value>The verifications.</value>
        public BankAccountVerification.Collection Verifications { get; set; }

        /// <summary>
        ///     Gets or sets the verifications URI.
        /// </summary>
        /// <value>The verifications URI.</value>
        public string VerificationsURI { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// De-serializes the data.
        /// </summary>
        /// <param name="data">The data.</param>
        public override void Deserialize(IDictionary<string, object> data)
        {
            base.Deserialize(data);
            this.Verifications = new BankAccountVerification.Collection(this.VerificationsURI);
        }

        /// <summary>
        /// Gets the verification.
        /// </summary>
        /// <returns>The bank account verification.</returns>
        public BankAccountVerification GetVerification()
        {
            return this.VerificationURI == null ? null : new BankAccountVerification(this.VerificationURI);
        }

        /// <summary>
        /// Serializes the data.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public override void Serialize(IDictionary<string, object> data)
        {
            base.Serialize(data);
            data["meta"] = this.Meta;
            data["name"] = this.Name;
            data["account_number"] = this.AccountNumber;
            data["routing_number"] = this.RoutingNumber;
            if (this.Type != null)
            {
                data["type"] = this.Type;
            }
        }

        /// <summary>
        ///     Verifies this instance.
        /// </summary>
        /// <returns>The bank account verification.</returns>
        public BankAccountVerification Verify()
        {
            return this.Verifications.Create();
        }

        #endregion

        /// <summary>
        ///     Class Collection.
        /// </summary>
        public class Collection : ResourceCollection<BankAccount>
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Collection"/> class.
            /// </summary>
            /// <param name="uri">
            /// The URI.
            /// </param>
            public Collection(string uri)
                : base(typeof(BankAccount), uri)
            {
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// Creates the specified name.
            /// </summary>
            /// <param name="name">
            /// The name.
            /// </param>
            /// <param name="accountNumber">
            /// The account number.
            /// </param>
            /// <param name="routingNumber">
            /// The routing number.
            /// </param>
            /// <returns>
            /// The bank account.
            /// </returns>
            public BankAccount Create(string name, string accountNumber, string routingNumber)
            {
                var i = new BankAccount { Name = name, AccountNumber = accountNumber, RoutingNumber = routingNumber };
                var data = new Dictionary<string, object>();
                i.Serialize(data);
                return this.Create(data);
            }

            #endregion
        }
    }
}