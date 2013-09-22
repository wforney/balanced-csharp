// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Marketplace.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced
{
    using System.Collections.Generic;

    /// <summary>
    ///     Class Marketplace.
    /// </summary>
    public class Marketplace : Resource
    {
        #region Fields

        /// <summary>
        ///     The bank accounts
        /// </summary>
        private BankAccount.Collection bankAccounts;

        /// <summary>
        ///     The cards
        /// </summary>
        private Card.Collection cards;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the mine.
        /// </summary>
        /// <value>The mine.</value>
        public static Marketplace Mine
        {
            get
            {
                return Query.One();
            }
        }

        /// <summary>
        ///     Gets the query.
        /// </summary>
        /// <value>The query.</value>
        public static ResourceQuery<Marketplace> Query
        {
            get
            {
                return new ResourceQuery<Marketplace>("/v1/marketplaces");
            }
        }

        /// <summary>
        ///     Gets or sets the callbacks URI.
        /// </summary>
        /// <value>The callbacks URI.</value>
        public string CallbacksURI { get; set; }

        /// <summary>
        ///     Gets or sets the credits URI.
        /// </summary>
        /// <value>The credits URI.</value>
        public string CreditsURI { get; set; }

        /// <summary>
        ///     Gets or sets the customers URI.
        /// </summary>
        /// <value>The customers URI.</value>
        public string CustomersURI { get; set; }

        /// <summary>
        ///     Gets or sets the debits URI.
        /// </summary>
        /// <value>The debits URI.</value>
        public string DebitsURI { get; set; }

        /// <summary>
        ///     Gets or sets the domain URL.
        /// </summary>
        /// <value>The domain URL.</value>
        public string DomainUrl { get; set; }

        /// <summary>
        ///     Gets or sets the events URI.
        /// </summary>
        /// <value>The events URI.</value>
        public string EventsURI { get; set; }

        /// <summary>
        ///     Gets or sets the holds URI.
        /// </summary>
        /// <value>The holds URI.</value>
        public string HoldsURI { get; set; }

        /// <summary>
        ///     Gets or sets the in escrow.
        /// </summary>
        /// <value>The in escrow.</value>
        public long InEscrow { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the refunds URI.
        /// </summary>
        /// <value>The refunds URI.</value>
        public string RefundsURI { get; set; }

        /// <summary>
        ///     Gets or sets the root URI.
        /// </summary>
        /// <value>The root URI.</value>
        public override string RootURI
        {
            get
            {
                return "/v1/marketplaces";
            }
        }

        /// <summary>
        ///     Gets or sets the support email addresses.
        /// </summary>
        /// <value>The support email addresses.</value>
        public string SupportEmailAddresses { get; set; }

        /// <summary>
        ///     Gets or sets the support phone number.
        /// </summary>
        /// <value>The support phone number.</value>
        public string SupportPhoneNumber { get; set; }

        #endregion

        #region Public Methods and Operators

        /// ReSharper disable CommentTypo
        /// <summary>
        /// Deserializes the data.
        /// </summary>
        /// ReSharper restore CommentTypo
        /// <param name="data">
        /// The data.
        /// </param>
        /// ReSharper disable IdentifierTypo
        public override void Deserialize(IDictionary<string, object> data)
        {
            // ReSharper restore IdentifierTypo
            base.Deserialize(data);

            this.id = (string)data["id"];
            this.Name = (string)data["name"];
            this.SupportEmailAddresses = (string)data["support_email_address"];
            this.SupportPhoneNumber = (string)data["support_phone_number"];
            this.DomainUrl = (string)data["domain_url"];
            this.InEscrow = (long)data["in_escrow"];
            this.bankAccounts = new BankAccount.Collection((string)data["bank_accounts_uri"]);
            this.cards = new Card.Collection((string)data["cards_uri"]);
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

            if (this.Name != null)
            {
                data["name"] = this.Name;
            }

            if (this.SupportEmailAddresses != null)
            {
                data["support_email_address"] = this.SupportEmailAddresses;
            }

            if (this.SupportPhoneNumber != null)
            {
                data["support_phone_number"] = this.SupportPhoneNumber;
            }

            if (this.DomainUrl != null)
            {
                data["domain_url"] = this.DomainUrl;
            }
        }

        /// ReSharper disable CommentTypo
        /// <summary>
        /// Tokenizes the bank account.
        /// </summary>
        /// ReSharper restore CommentTypo
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
        public BankAccount TokenizeBankAccount(string name, string accountNumber, string routingNumber)
        {
            return this.bankAccounts.Create(name, accountNumber, routingNumber);
        }

        /// ReSharper disable CommentTypo
        /// <summary>
        /// Tokenizes the card.
        /// </summary>
        /// ReSharper restore CommentTypo
        /// <param name="streetAddress">
        /// The street_address.
        /// </param>
        /// <param name="city">
        /// The city.
        /// </param>
        /// <param name="region">
        /// The region.
        /// </param>
        /// <param name="postalCode">
        /// The postal_code.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="cardNumber">
        /// The card_number.
        /// </param>
        /// <param name="securityCode">
        /// The security_code.
        /// </param>
        /// <param name="expirationMonth">
        /// The expiration_month.
        /// </param>
        /// <param name="expirationYear">
        /// The expiration_year.
        /// </param>
        /// <returns>
        /// The Card.
        /// </returns>
        public Card TokenizeCard(
            string streetAddress, 
            string city, 
            string region, 
            string postalCode, 
            string name, 
            string cardNumber, 
            string securityCode, 
            int expirationMonth, 
            int expirationYear)
        {
            return this.cards.Create(
                streetAddress, 
                city, 
                region, 
                postalCode, 
                name, 
                cardNumber, 
                securityCode, 
                expirationMonth, 
                expirationYear);
        }

        #endregion
    }
}