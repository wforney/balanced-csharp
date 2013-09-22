// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Customer.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Class Customer.
    /// </summary>
    public class Customer : Resource
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Customer" /> class.
        /// </summary>
        public Customer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class. 
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        public Customer(string uri)
            : base(uri)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="payload">
        /// The payload.
        /// </param>
        public Customer(IDictionary<string, object> payload)
            : base(payload)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the bank accounts.
        /// </summary>
        /// <value>The bank accounts.</value>
        public BankAccount.Collection BankAccounts { get; set; }

        /// <summary>
        ///     Gets or sets the cards.
        /// </summary>
        /// <value>The cards.</value>
        public Card.Collection Cards { get; set; }

        /// <summary>
        ///     Gets or sets the credits.
        /// </summary>
        /// <value>The credits.</value>
        public Credit.Collection Credits { get; set; }

        /// <summary>
        ///     Gets or sets the debits.
        /// </summary>
        /// <value>The debits.</value>
        public Debit.Collection Debits { get; set; }

        /// <summary>
        ///     Gets or sets the holds.
        /// </summary>
        /// <value>The holds.</value>
        public Hold.Collection Holds { get; set; }

        /// <summary>
        ///     Gets or sets the refunds.
        /// </summary>
        /// <value>The refunds.</value>
        public Refund.Collection Refunds { get; set; }

        /// <summary>
        ///     Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public Dictionary<string, object> address { get; set; }

        /// <summary>
        ///     Gets or sets the bank_accounts_uri.
        /// </summary>
        /// <value>The bank_accounts_uri.</value>
        public string bank_accounts_uri { get; set; }

        /// <summary>
        ///     Gets or sets the business_name.
        /// </summary>
        /// <value>The business_name.</value>
        public string business_name { get; set; }

        /// <summary>
        ///     Gets or sets the cards_uri.
        /// </summary>
        /// <value>The cards_uri.</value>
        public string cards_uri { get; set; }

        /// <summary>
        ///     Gets or sets the credits_uri.
        /// </summary>
        /// <value>The credits_uri.</value>
        public string credits_uri { get; set; }

        /// <summary>
        ///     Gets or sets the debits_uri.
        /// </summary>
        /// <value>The debits_uri.</value>
        public string debits_uri { get; set; }

        /// <summary>
        ///     Gets or sets the dob.
        /// </summary>
        /// <value>The dob.</value>
        public string dob { get; set; }

        /// <summary>
        ///     Gets or sets the ein.
        /// </summary>
        /// <value>The ein.</value>
        public string ein { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string email { get; set; }

        /// <summary>
        ///     Gets or sets the facebook.
        /// </summary>
        /// <value>The facebook.</value>
        public string facebook { get; set; }

        /// <summary>
        ///     Gets or sets the holds_uri.
        /// </summary>
        /// <value>The holds_uri.</value>
        public string holds_uri { get; set; }

        /// <summary>
        ///     Gets or sets the is_identity_verified.
        /// </summary>
        /// <value>The is_identity_verified.</value>
        public bool is_identity_verified { get; set; }

        /// <summary>
        ///     Gets or sets the meta.
        /// </summary>
        /// <value>The meta.</value>
        public IDictionary<string, object> meta { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string name { get; set; }

        /// <summary>
        ///     Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public string phone { get; set; }

        /// <summary>
        ///     Gets or sets the refunds_uri.
        /// </summary>
        /// <value>The refunds_uri.</value>
        public string refunds_uri { get; set; }

        /// <summary>
        ///     Gets or sets the ssn_last4.
        /// </summary>
        /// <value>The ssn_last4.</value>
        public string ssn_last4 { get; set; }

        /// <summary>
        ///     Gets or sets the twitter.
        /// </summary>
        /// <value>The twitter.</value>
        public string twitter { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the specified URI.
        /// </summary>
        /// <param name="uri">
        /// The URI.
        /// </param>
        /// <returns>
        /// Customer.
        /// </returns>
        public static Customer Get(string uri)
        {
            return new Customer((new Client()).Get(uri));
        }

        /// <summary>
        ///     Queries this instance.
        /// </summary>
        /// <returns>ResourceQuery{Customer}.</returns>
        public static ResourceQuery<Customer> Query()
        {
            return new ResourceQuery<Customer>(
                typeof(Customer), 
                string.Format("/v/%s/%s", Settings.Version, "customers"));
        }

        /// <summary>
        ///     Actives the bank account.
        /// </summary>
        /// <returns>BankAccount.</returns>
        public BankAccount ActiveBankAccount()
        {
            return
                this.BankAccounts.Query.Filter("is_valid", true)
                    .OrderBy("created_at", ResourceQueryOrder.DESCENDING)
                    .First();
        }

        /// <summary>
        ///     Actives the card.
        /// </summary>
        /// <returns>Card.</returns>
        public Card ActiveCard()
        {
            return
                this.Cards.Query.Filter("is_valid", true).OrderBy("created_at", ResourceQueryOrder.DESCENDING).First();
        }

        /// <summary>
        /// Adds the bank account.
        /// </summary>
        /// <param name="bankAccountUri">
        /// The bank account URI.
        /// </param>
        public void AddBankAccount(string bankAccountUri)
        {
            IDictionary<string, object> payload = new Dictionary<string, object>();
            payload["bank_account_uri"] = bankAccountUri;
            IDictionary<string, object> response = this.Client.Put(this.uri, payload);
            this.Deserialize(response);
        }

        /// <summary>
        /// Adds the bank account.
        /// </summary>
        /// <param name="bankAccount">
        /// The bank account.
        /// </param>
        public void AddBankAccount(BankAccount bankAccount)
        {
            AddBankAccount(bankAccount.uri);
        }

        /// <summary>
        /// Adds the card.
        /// </summary>
        /// <param name="card_uri">
        /// The card_uri.
        /// </param>
        public void AddCard(string card_uri)
        {
            IDictionary<string, object> payload = new Dictionary<string, object>();
            payload["card_uri"] = card_uri;
            IDictionary<string, object> response = this.Client.Put(this.uri, payload);
            this.Deserialize(response);
        }

        /// <summary>
        /// Adds the card.
        /// </summary>
        /// <param name="card">
        /// The card.
        /// </param>
        public void AddCard(Card card)
        {
            AddCard(card.uri);
        }

        /// <summary>
        /// Credits the specified amount.
        /// </summary>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="destinationUri">
        /// The destination URI.
        /// </param>
        /// <param name="appearsOnStatementAs">
        /// The appears configuration statement asynchronous.
        /// </param>
        /// <param name="debitUri">
        /// The debit URI.
        /// </param>
        /// <param name="meta">
        /// The meta.
        /// </param>
        /// <returns>
        /// Credit.
        /// </returns>
        public Credit Credit(
            int amount, 
            string description, 
            string destinationUri, 
            string appearsOnStatementAs, 
            string debitUri, 
            IDictionary<string, string> meta)
        {
            return this.Credits.Create(amount, description, destinationUri, appearsOnStatementAs, debitUri, meta);
        }

        /// <summary>
        /// Debits the specified amount.
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
        public Debit Debit(
            int amount, 
            string description, 
            string sourceUri, 
            string appearsOnStatementAs, 
            string onBehalfOfUri, 
            IDictionary<string, string> meta)
        {
            return this.Debits.Create(amount, description, sourceUri, appearsOnStatementAs, onBehalfOfUri, meta);
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
            this.BankAccounts = new BankAccount.Collection(this.bank_accounts_uri);
            this.Cards = new Card.Collection(this.cards_uri);
            this.Holds = new Hold.Collection(this.holds_uri);
            this.Credits = new Credit.Collection(this.credits_uri);
            this.Debits = new Debit.Collection(this.debits_uri);
            this.Refunds = new Refund.Collection(this.refunds_uri);
        }

        /// <summary>
        ///     The save.
        /// </summary>
        public override void Save()
        {
            if (this.id == null && this.uri == null)
            {
                this.uri = string.Format("/v%s/%s", Settings.Version, "customers");
            }

            base.Save();
        }

        #endregion

        /// <summary>
        ///     Class Collection.
        /// </summary>
        public class Collection : ResourceCollection<Customer>
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Collection"/> class.
            /// </summary>
            /// <param name="uri">
            /// The URI.
            /// </param>
            public Collection(string uri)
                : base(typeof(Customer), uri)
            {
            }

            #endregion
        };
    }
}