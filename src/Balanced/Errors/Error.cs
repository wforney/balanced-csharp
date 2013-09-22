// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Error.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    ///     Class Error.
    /// </summary>
    [Serializable]
    public class Error : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Error" /> class.
        /// </summary>
        public Error()
        {
            this.Extras = new Dictionary<string, object>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the category code.
        /// </summary>
        /// <value>The category code.</value>
        public string CategoryCode { get; set; }

        /// <summary>
        ///     Gets or sets the type of the category.
        /// </summary>
        /// <value>The type of the category.</value>
        public string CategoryType { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the extras.
        /// </summary>
        /// <value>The extras.</value>
        public Dictionary<string, object> Extras { get; set; }

        /// <summary>
        ///     Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string ID { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creates the specified data.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The Error.
        /// </returns>
        public static Error Create(IDictionary<string, object> data)
        {
            Contract.Requires<ArgumentNullException>(data != null);

            var key = (string)data["category_code"];

            Type type;
            switch (key)
            {
                case "funding-destination-declined":
                case "authorization-failed":
                    type = typeof(Declined);
                    break;

                case "bank-account-authentication-not-pending":
                case "bank-account-authentication-failed":
                case "bank-account-authentication-already-exists":
                    type = typeof(BankAccountVerificationFailure);
                    break;

                default:
                    type = typeof(Error);
                    break;
            }

            var error = (Error)Activator.CreateInstance(type);
            error.Deserialize(data);
            return error;
        }

        /// <summary>
        /// De-serializes the specified data.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public virtual void Deserialize(IDictionary<string, object> data)
        {
            Contract.Requires<ArgumentNullException>(data != null);

            this.ID = (string)data["request_id"];
            this.CategoryType = (string)data["category_type"];
            this.CategoryCode = (string)data["category_code"];
            this.Description = (string)data["description"];
            this.Extras = data.ContainsKey("extras") ? new Dictionary<string, object>((IDictionary<string, object>)data["extras"]) : new Dictionary<string, object>();
        }

        #endregion
    }
}