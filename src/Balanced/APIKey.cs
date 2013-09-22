// --------------------------------------------------------------------------------------------------------------------
// <copyright file="APIKey.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Class APIKey.
    /// </summary>
    public class APIKey : Resource
    {
        #region Fields

        /// <summary>
        ///     The meta
        /// </summary>
        private Dictionary<string, string> meta = new Dictionary<string, string>();

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the query.
        /// </summary>
        /// <value>The query.</value>
        public static ResourceQuery<APIKey> Query
        {
            get
            {
                return new ResourceQuery<APIKey>("/v1/api_keys");
            }
        }

        /// <summary>
        ///     Gets or sets the created attribute.
        /// </summary>
        /// <value>The created attribute.</value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the meta.
        /// </summary>
        /// <value>The meta.</value>
        public Dictionary<string, string> Meta
        {
            get
            {
                return this.meta;
            }

            set
            {
                this.meta = value;
            }
        }

        /// <summary>
        ///     Gets or sets the root URI.
        /// </summary>
        /// <value>The root URI.</value>
        public override string RootURI
        {
            get
            {
                return "/v1/api_keys";
            }
        }

        /// <summary>
        ///     Gets or sets the secret.
        /// </summary>
        /// <value>The secret.</value>
        public string Secret { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// De-serializes the data.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public override void Deserialize(IDictionary<string, object> data)
        {
            base.Deserialize(data);
            this.Id = (string)data["id"];
            DateTime createdAt;
            this.Deserialize(data["created_at"], out createdAt);
            this.CreatedAt = createdAt;
            this.Deserialize(data["meta"], out this.meta);
            this.Secret = data.ContainsKey("secret") ? (string)data["secret"] : null;
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
            data["meta"] = this.meta;
        }

        #endregion
    }
}