// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Debit.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Class Debit.
    /// </summary>
    public class Debit : Resource
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Debit" /> class.
        /// </summary>
        public Debit()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Debit"/> class. 
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        public Debit(string uri)
            : base(uri)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Debit"/> class.
        /// </summary>
        /// <param name="payload">
        /// The payload.
        /// </param>
        public Debit(IDictionary<string, object> payload)
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
        ///     Gets or sets the hold.
        /// </summary>
        /// <value>The hold.</value>
        public Hold Hold { get; set; }

        /// <summary>
        ///     Gets or sets the refunds.
        /// </summary>
        /// <value>The refunds.</value>
        public Refund.Collection Refunds { get; set; }

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
        ///     Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string description { get; set; }

        /// <summary>
        ///     Gets or sets the hold_uri.
        /// </summary>
        /// <value>The hold_uri.</value>
        public string hold_uri { get; set; }

        /// <summary>
        ///     Gets or sets the meta.
        /// </summary>
        /// <value>The meta.</value>
        public IDictionary<string, object> meta { get; set; }

        /// <summary>
        ///     Gets or sets the refunds_uri.
        /// </summary>
        /// <value>The refunds_uri.</value>
        public string refunds_uri { get; set; }

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
            this.Hold = new Hold(this.hold_uri);
            this.Account = new Account(this.account_uri);
            this.Customer = new Customer(this.customer_uri);
            this.Card = new Card(this.card_uri);
            this.Refunds = new Refund.Collection(this.refunds_uri);
        }

        /// <summary>
        ///     Gets the account.
        /// </summary>
        /// <returns>Account.</returns>
        public Account GetAccount()
        {
            if (this.Account == null)
            {
                this.Account = new Account(this.account_uri);
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
                this.Card = new Card(this.card_uri);
            }

            return this.Card;
        }

        /// <summary>
        ///     Gets the hold.
        /// </summary>
        /// <returns>Hold.</returns>
        public Hold GetHold()
        {
            if (this.Hold == null)
            {
                this.Hold = new Hold(this.hold_uri);
            }

            return this.Hold;
        }

        /// <summary>
        /// Refunds the specified amount.
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
        public Refund Refund(int amount, string description, IDictionary<string, string> meta)
        {
            return this.Refunds.Create(amount, description, meta);
        }

        /// <summary>
        /// Refunds the specified amount.
        /// </summary>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <returns>
        /// Refund.
        /// </returns>
        public Refund Refund(int amount)
        {
            return this.Refund(amount, null, null);
        }

        /// <summary>
        ///     Refunds this instance.
        /// </summary>
        /// <returns>Refund.</returns>
        public Refund Refund()
        {
            return new Refund();
        }

        #endregion

        /// <summary>
        ///     Class Collection.
        /// </summary>
        public class Collection : ResourceCollection<Debit>
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Collection"/> class.
            /// </summary>
            /// <param name="uri">
            /// The URI.
            /// </param>
            public Collection(string uri)
                : base(typeof(Debit), uri)
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
            /// <param name="sourceUri">
            /// The source URI.
            /// </param>
            /// <param name="appearsOnStatementAs">
            /// The appears configuration statement asynchronous.
            /// </param>
            /// <param name="onBehalfOfUri">
            /// The configuration behalf of URI.
            /// </param>
            /// <param name="meta">
            /// The meta.
            /// </param>
            /// <returns>
            /// Debit.
            /// </returns>
            public Debit Create(
                int amount, 
                string description, 
                string sourceUri, 
                string appearsOnStatementAs, 
                string onBehalfOfUri, 
                IDictionary<string, string> meta)
            {
                IDictionary<string, object> payload = new Dictionary<string, object>();
                payload["amount"] = amount;
                if (description != null)
                {
                    payload["description"] = description;
                }

                if (sourceUri != null)
                {
                    payload["source_uri"] = sourceUri;
                }

                if (appearsOnStatementAs != null)
                {
                    payload["appears_on_statement_as"] = appearsOnStatementAs;
                }

                if (onBehalfOfUri != null)
                {
                    payload["on_behalf_of_uri"] = onBehalfOfUri;
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