// ***********************************************************************
// Assembly         : Balanced
// Author           : William
// Created          : 09-22-2013
//
// Last Modified By : William
// Last Modified On : 09-22-2013
// ***********************************************************************
// <copyright file="Account.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanced
{
    /// <summary>
    /// Class Account.
    /// </summary>
    public class Account : Resource
    {
        /// <summary>
        /// The buye r_ role
        /// </summary>
        public const string BUYER_ROLE = "buyer";
        /// <summary>
        /// The merchan t_ role
        /// </summary>
        public const string MERCHANT_ROLE = "merchant";

        /// <summary>
        /// Gets or sets the created_at.
        /// </summary>
        /// <value>The created_at.</value>
        public DateTime created_at { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the email_address.
        /// </summary>
        /// <value>The email_address.</value>
        public string email_address { get; set; }
        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public string[] roles { get; set; }
        /// <summary>
        /// Gets or sets the bank_accounts_uri.
        /// </summary>
        /// <value>The bank_accounts_uri.</value>
        public string bank_accounts_uri { get; set; }
        /// <summary>
        /// Gets or sets the cards_uri.
        /// </summary>
        /// <value>The cards_uri.</value>
        public string cards_uri { get; set; }
        /// <summary>
        /// Gets or sets the credits_uri.
        /// </summary>
        /// <value>The credits_uri.</value>
        public string credits_uri { get; set; }
        /// <summary>
        /// Gets or sets the debits_uri.
        /// </summary>
        /// <value>The debits_uri.</value>
        public string debits_uri { get; set; }
        /// <summary>
        /// Gets or sets the holds_uri.
        /// </summary>
        /// <value>The holds_uri.</value>
        public string holds_uri { get; set; }
        /// <summary>
        /// Gets or sets the meta.
        /// </summary>
        /// <value>The meta.</value>
        public IDictionary<string, object> meta { get; set; }

        /// <summary>
        /// Gets or sets the debits.
        /// </summary>
        /// <value>The debits.</value>
        public Debit.Collection Debits { get; set; }
        /// <summary>
        /// Gets or sets the credits.
        /// </summary>
        /// <value>The credits.</value>
        public Credit.Collection Credits { get; set; }
        /// <summary>
        /// Gets or sets the holds.
        /// </summary>
        /// <value>The holds.</value>
        public Hold.Collection Holds { get; set; }
        /// <summary>
        /// Gets or sets the cards.
        /// </summary>
        /// <value>The cards.</value>
        public Card.Collection Cards { get; set; }
        /// <summary>
        /// Gets or sets the bank accounts.
        /// </summary>
        /// <value>The bank accounts.</value>
        public BankAccount.Collection BankAccounts { get; set; }

        /// <summary>
        /// Class Collection.
        /// </summary>
        public class Collection : ResourceCollection<Account>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Collection"/> class.
            /// </summary>
            /// <param name="uri">The URI.</param>
            public Collection(string uri) : base(uri) { }
        };

        /// <summary>
        /// Gets the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>Account.</returns>
        public static Account Get(string uri) 
        { 
            return new Account((new Client()).Get(uri));         
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        public Account() : base() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="payload">The payload.</param>
        public Account(IDictionary<string, Object> payload) : base(payload)  {}

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource" /> class.
        /// </summary>
        /// <param name="uri">The uri.</param>
        public Account(string uri) : base(uri) {}

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="data">The data.</param>
        public override void Deserialize(IDictionary<string, object> data)
        {
            base.Deserialize(data);
            Debits = new Debit.Collection(debits_uri);
            Credits = new Credit.Collection(credits_uri);
            Holds = new Hold.Collection(holds_uri);
            Cards = new Card.Collection(cards_uri);
            BankAccounts = new BankAccount.Collection(bank_accounts_uri);
        }

        /// <summary>
        /// Credits the specified amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="description">The description.</param>
        /// <param name="destination_uri">The destination_uri.</param>
        /// <param name="appears_on_statement_as">The appears_on_statement_as.</param>
        /// <param name="meta">The meta.</param>
        /// <returns>Credit.</returns>
        public Credit Credit(int amount, 
                             string description,
                             string destination_uri,
                             string appears_on_statement_as,
                             IDictionary<string, string> meta)
        {
            return Credits.Create(amount, description, destination_uri, appears_on_statement_as, null, meta);
        }

        /// <summary>
        /// Credits the specified amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns>Credit.</returns>
        public Credit Credit(int amount) {
            return Credit(amount, null, null, null, null);
        }

        /// <summary>
        /// Debits the specified amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="description">The description.</param>
        /// <param name="source_uri">The source_uri.</param>
        /// <param name="appears_on_statement_as">The appears_on_statement_as.</param>
        /// <param name="meta">The meta.</param>
        /// <returns>Debit.</returns>
        public Debit Debit(int amount, 
                           string description,
                           string source_uri,
                           string appears_on_statement_as,
                           IDictionary<string, string> meta)
        {
            return Debits.Create(amount, description, source_uri, appears_on_statement_as, null, meta);
        }

        /// <summary>
        /// Debits the specified amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns>Debit.</returns>
        public Debit Debit(int amount) {
            return Debit(amount, null, null, null, null);
        }

        /// <summary>
        /// Holds the specified amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="description">The description.</param>
        /// <param name="source_uri">The source_uri.</param>
        /// <param name="meta">The meta.</param>
        /// <returns>Hold.</returns>
        public Hold Hold(int amount, 
                         string description,
                         string source_uri,
                         IDictionary<string, string> meta)
        {
            return Holds.Create(amount, description, source_uri, meta);
        }

        /// <summary>
        /// Holds the specified amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns>Hold.</returns>
        public Hold hold(int amount) {
            return Hold(amount, null, null, null);
        }

        /// <summary>
        /// Associates the bank account.
        /// </summary>
        /// <param name="bank_account_uri">The bank_account_uri.</param>
        public void AssociateBankAccount(string bank_account_uri)
        {
            IDictionary<string, object> payload = new Dictionary<string, object>();
            payload["bank_account_uri"] = bank_account_uri;
            IDictionary<string, object> response = Client.Put(uri, payload);
            Deserialize(response);
        }

        /// <summary>
        /// Promotes the automatic merchant.
        /// </summary>
        /// <param name="merchant">The merchant.</param>
        public void PromoteToMerchant(IDictionary<string, object> merchant)
        {
            IDictionary<string, object> payload = new Dictionary<string, object>();
            payload["merchant"] = merchant;
            IDictionary<string, object> response = Client.Put(uri, payload);
            Deserialize(response);
        }

        /// <summary>
        /// Promotes the automatic merchant.
        /// </summary>
        /// <param name="merchant_uri">The merchant_uri.</param>
        public void PromoteToMerchant(string merchant_uri)
        {
            IDictionary<string, object> payload = new Dictionary<string, object>();
            payload["merchant_uri"] = merchant_uri;
            IDictionary<string, object> response = Client.Put(uri, payload);
            Deserialize(response);
        }

        /// <summary>
        /// Associates the card.
        /// </summary>
        /// <param name="card_uri">The card_uri.</param>
        public void AssociateCard(String card_uri)
        {
            IDictionary<string, object> payload = new Dictionary<string, object>();
            payload["card_uri"] = card_uri;
            IDictionary<string, object> response = Client.Put(uri, payload);
            Deserialize(response);  
        }



    }
}
