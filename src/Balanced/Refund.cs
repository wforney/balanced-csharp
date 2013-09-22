// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Refund.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Class Refund.
    /// </summary>
    public class Refund : Resource
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Refund" /> class.
        /// </summary>
        public Refund()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Refund"/> class.
        /// </summary>
        /// <param name="payload">
        /// The payload.
        /// </param>
        public Refund(IDictionary<string, object> payload)
            : base(payload)
        {
        }

        #endregion

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
        ///     Gets or sets the debit.
        /// </summary>
        /// <value>The debit.</value>
        public Debit Debit { get; set; }

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
        ///     Gets or sets the appears_on_statement_as.
        /// </summary>
        /// <value>The appears_on_statement_as.</value>
        public string appears_on_statement_as { get; set; }

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
        ///     Gets or sets the debit_uri.
        /// </summary>
        /// <value>The debit_uri.</value>
        public string debit_uri { get; set; }

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
        ///     Gets or sets the transaction_number.
        /// </summary>
        /// <value>The transaction_number.</value>
        public string transaction_number { get; set; }

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
            this.Debit = new Debit(this.debit_uri);
        }

        #endregion

        /// <summary>
        ///     Class Collection.
        /// </summary>
        public class Collection : ResourceCollection<Refund>
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Collection"/> class.
            /// </summary>
            /// <param name="uri">
            /// The URI.
            /// </param>
            public Collection(string uri)
                : base(typeof(Refund), uri)
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
            /// <param name="meta">
            /// The meta.
            /// </param>
            /// <returns>
            /// Refund.
            /// </returns>
            public Refund Create(int amount, string description, IDictionary<string, string> meta)
            {
                IDictionary<string, object> payload = new Dictionary<string, object>();
                payload["amount"] = amount;
                if (description != null)
                {
                    payload["description"] = description;
                }

                if (meta != null)
                {
                    payload["meta"] = meta;
                }

                return this.Create(payload);
            }

            #endregion
        }
    }
}