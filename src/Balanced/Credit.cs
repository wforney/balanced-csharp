// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Credit.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Class Credit.
    /// </summary>
    public class Credit : Resource
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the account.
        /// </summary>
        /// <value>The account.</value>
        public Account Account { get; set; }

        /// <summary>
        ///     Gets or sets the customer.
        /// </summary>
        /// <value>The customer.</value>
        public Customer Customer { get; set; }

        /// <summary>
        ///     Gets or sets the account_uri.
        /// </summary>
        /// <value>The account_uri.</value>
        public string account_uri { get; set; }

        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        /// <value>The amount.</value>
        public int amount { get; set; }

        /// <summary>
        ///     Gets or sets the bank_account.
        /// </summary>
        /// <value>The bank_account.</value>
        public BankAccount bank_account { get; set; }

        /// <summary>
        ///     Gets or sets the created_at.
        /// </summary>
        /// <value>The created_at.</value>
        public DateTime created_at { get; set; }

        /// <summary>
        ///     Gets or sets the customer_uri.
        /// </summary>
        /// <value>The customer_uri.</value>
        public string customer_uri { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string description { get; set; }

        /// <summary>
        ///     Gets or sets the meta.
        /// </summary>
        /// <value>The meta.</value>
        public IDictionary<string, object> meta { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string status { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public override void Deserialize(IDictionary<string, object> data)
        {
            base.Deserialize(data);
            this.Account = new Account(this.account_uri);
            this.Customer = new Customer(this.customer_uri);
        }

        #endregion

        /// <summary>
        ///     Class Collection.
        /// </summary>
        public class Collection : ResourceCollection<Credit>
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Collection"/> class.
            /// </summary>
            /// <param name="uri">
            /// The URI.
            /// </param>
            public Collection(string uri)
                : base(typeof(Credit), uri)
            {
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// Creates the specified amount.
            /// </summary>
            /// <param name="amount">
            /// The amount.
            /// </param>
            /// <param name="description">
            /// The description.
            /// </param>
            /// <param name="destination_uri">
            /// The destination_uri.
            /// </param>
            /// <param name="appears_on_statement_as">
            /// The appears_on_statement_as.
            /// </param>
            /// <param name="debit_uri">
            /// The debit_uri.
            /// </param>
            /// <param name="meta">
            /// The meta.
            /// </param>
            /// <returns>
            /// Credit.
            /// </returns>
            public Credit Create(
                int amount, 
                string description, 
                string destination_uri, 
                string appears_on_statement_as, 
                string debit_uri, 
                IDictionary<string, string> meta)
            {
                IDictionary<string, object> payload = new Dictionary<string, object>();
                payload["amount"] = amount;
                if (description != null)
                {
                    payload["description"] = description;
                }

                if (destination_uri != null)
                {
                    payload["destination_uri"] = destination_uri;
                }

                if (appears_on_statement_as != null)
                {
                    payload["appears_on_statement_as"] = appears_on_statement_as;
                }

                if (meta != null)
                {
                    payload["meta"] = meta;
                }

                return this.Create(payload);
            }

            #endregion
        };
    }
}