// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Hold.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Class Hold.
    /// </summary>
    public class Hold : Resource
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Hold" /> class.
        /// </summary>
        public Hold()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hold"/> class. 
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        public Hold(string uri)
            : base(uri)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hold"/> class.
        /// </summary>
        /// <param name="payload">
        /// The payload.
        /// </param>
        public Hold(IDictionary<string, object> payload)
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
        ///     Gets or sets the card.
        /// </summary>
        /// <value>The card.</value>
        public Card Card { get; set; }

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
        ///     Gets or sets the card_uri.
        /// </summary>
        /// <value>The card_uri.</value>
        public string card_uri { get; set; }

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
        ///     Gets or sets the expires_at.
        /// </summary>
        /// <value>The expires_at.</value>
        public DateTime expires_at { get; set; }

        /// <summary>
        ///     Gets or sets the is_void.
        /// </summary>
        /// <value>The is_void.</value>
        public bool is_void { get; set; }

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
        /// Gets the specified URI.
        /// </summary>
        /// <param name="uri">
        /// The URI.
        /// </param>
        /// <returns>
        /// Hold.
        /// </returns>
        public static Hold Get(string uri)
        {
            return new Hold((new Client()).Get(uri));
        }

        /// <summary>
        /// Captures the specified amount.
        /// </summary>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <returns>
        /// Debit.
        /// </returns>
        public Debit Capture(int amount)
        {
            IDictionary<string, object> payload = new Dictionary<string, object>();
            payload["hold_uri"] = this.uri;
            payload["amount"] = amount;
            this.Debit = this.Account.Debits.Create(payload);
            return this.Debit;
        }

        /// <summary>
        ///     Captures this instance.
        /// </summary>
        /// <returns>Debit.</returns>
        public Debit Capture()
        {
            IDictionary<string, object> payload = new Dictionary<string, object>();
            payload["hold_uri"] = this.uri;
            this.Debit = this.Account.Debits.Create(payload);
            return this.Debit;
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public override void Deserialize(IDictionary<string, object> data)
        {
            base.Deserialize(data);
            this.Customer = new Customer(this.customer_uri);
            this.Account = new Account(this.account_uri);
            this.Card = new Card(this.card_uri);
            this.Debit = new Debit(this.debit_uri);
        }

        /// <summary>
        ///     Gets the account.
        /// </summary>
        /// <returns>Account.</returns>
        public Account GetAccount()
        {
            if (this.Account == null)
            {
                this.Account = new Account();
            }

            return this.Account;
        }

        /// <summary>
        ///     Gets the card.
        /// </summary>
        /// <returns>Card.</returns>
        public Card GetCard()
        {
            if (this.Card == null)
            {
                this.Card = new Card();
            }

            return this.Card;
        }

        /// <summary>
        ///     Void_s this instance.
        /// </summary>
        public void Void_()
        {
            this.is_void = true;
            this.Save();
        }

        #endregion

        /// <summary>
        ///     Class Collection.
        /// </summary>
        public class Collection : ResourceCollection<Hold>
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Collection"/> class.
            /// </summary>
            /// <param name="uri">
            /// The URI.
            /// </param>
            public Collection(string uri)
                : base(typeof(Hold), uri)
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
            /// <param name="sourceURI">
            /// The source URI.
            /// </param>
            /// <param name="meta">
            /// The meta.
            /// </param>
            /// <returns>
            /// Hold.
            /// </returns>
            public Hold Create(int amount, string description, string sourceURI, IDictionary<string, string> meta)
            {
                IDictionary<string, object> payload = new Dictionary<string, object>();
                payload["amount"] = amount;
                if (description != null)
                {
                    payload["description"] = description;
                }

                if (sourceURI != null)
                {
                    payload["source"] = sourceURI;
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