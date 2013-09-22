// ***********************************************************************
// Assembly         : Balanced
// Author           : William
// Created          : 09-22-2013
//
// Last Modified By : William
// Last Modified On : 09-22-2013
// ***********************************************************************
// <copyright file="Card.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balanced
{
    /// <summary>
    /// Class Card.
    /// </summary>
    public class Card : Resource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        public Card() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource" /> class.
        /// </summary>
        /// <param name="uri">The uri.</param>
        public Card(string uri) : base(uri) {}

        /// <summary>
        /// Class Collection.
        /// </summary>
        public class Collection : ResourceCollection<Card>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Collection"/> class.
            /// </summary>
            /// <param name="uri">The URI.</param>
            public Collection(string uri)
                : base(uri)
            { }

            /// <summary>
            /// Creates the specified street_address.
            /// </summary>
            /// <param name="street_address">The street_address.</param>
            /// <param name="city">The city.</param>
            /// <param name="region">The region.</param>
            /// <param name="postal_code">The postal_code.</param>
            /// <param name="name">The name.</param>
            /// <param name="card_number">The card_number.</param>
            /// <param name="security_code">The security_code.</param>
            /// <param name="expiration_month">The expiration_month.</param>
            /// <param name="expiration_year">The expiration_year.</param>
            /// <returns>Card.</returns>
            public Card Create(
                string street_address,
                string city,
                string region,
                string postal_code,
                string name,
                string card_number,
                string security_code,
                int expiration_month,
                int expiration_year
                )
            {
                var i = new Card() 
                { 
                    street_address = street_address,
                    postal_code = postal_code,
                    name = name,
                    expiration_month = expiration_month,
                    expiration_year = expiration_year
                };
                Dictionary<string, object> data = new Dictionary<string, object>();
                data["city"] = city;
                data["region"] = region;
                data["card_number"] = card_number;
                data["security_code"] = security_code;
                i.Serialize(data);
                return base.Create(data);
            }
        }

        /// <summary>
        /// Gets or sets the created_at.
        /// </summary>
        /// <value>The created_at.</value>
        public DateTime created_at { get; set; }
        /// <summary>
        /// Gets or sets the meta.
        /// </summary>
        /// <value>The meta.</value>
        public IDictionary<string, object> meta { get; set; }
        /// <summary>
        /// Gets or sets the street_address.
        /// </summary>
        /// <value>The street_address.</value>
        public string street_address { get; set; }
        /// <summary>
        /// Gets or sets the postal_code.
        /// </summary>
        /// <value>The postal_code.</value>
        public string postal_code { get; set; }
        /// <summary>
        /// Gets or sets the country_code.
        /// </summary>
        /// <value>The country_code.</value>
        public string country_code { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the expiration_month.
        /// </summary>
        /// <value>The expiration_month.</value>
        public long expiration_month { get; set; }
        /// <summary>
        /// Gets or sets the expiration_year.
        /// </summary>
        /// <value>The expiration_year.</value>
        public long expiration_year { get; set; }
        /// <summary>
        /// Gets or sets the last_four.
        /// </summary>
        /// <value>The last_four.</value>
        public string last_four { get; set; }
        /// <summary>
        /// Gets or sets the brand.
        /// </summary>
        /// <value>The brand.</value>
        public string brand { get; set; }
        /// <summary>
        /// Gets or sets the is_valid.
        /// </summary>
        /// <value>The is_valid.</value>
        public Boolean is_valid { get; set; }
        /// <summary>
        /// Gets or sets the fingerprint.
        /// </summary>
        /// <value>The fingerprint.</value>
        public string fingerprint { get; set; }

        /// <summary>
        /// Invalidates this instance.
        /// </summary>
        public void invalidate()
        {
            is_valid = false;
            Save();
        }

        /// <summary>
        /// The serialize.
        /// </summary>
        /// <param name="data">The data.</param>
        public override void Serialize(IDictionary<string, object> data)
        {
            base.Serialize(data);

            data["meta"] = meta;
            data["street_address"] = street_address;
            data["postal_code"] = postal_code;
            data["country_code"] = country_code;
            data["name"] = name;
            data["expiration_month"] = expiration_month;
            data["expiration_year"] = expiration_year;
            data["last_four"] = last_four;
            data["brand"] = brand;
            data["is_valid"] = is_valid;
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="data">The data.</param>
        public override void Deserialize(IDictionary<string, object> data)
        {
            base.Deserialize(data);
        }
    }
}
