// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Callback.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced
{
    using System.Collections.Generic;

    /// <summary>
    ///     Class Callback.
    /// </summary>
    public class Callback : Resource
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Callback" /> class.
        /// </summary>
        public Callback()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Callback"/> class. 
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        public Callback(string uri)
            : base(uri)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Callback"/> class.
        /// </summary>
        /// <param name="payload">
        /// The payload.
        /// </param>
        public Callback(IDictionary<string, object> payload)
            : base(payload)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string url { get; set; }

        #endregion

        /// <summary>
        ///     Class Collection.
        /// </summary>
        public class Collection : ResourceCollection<Callback>
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Collection"/> class.
            /// </summary>
            /// <param name="uri">
            /// The URI.
            /// </param>
            public Collection(string uri)
                : base(typeof(Callback), uri)
            {
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// Creates the specified URL.
            /// </summary>
            /// <param name="url">
            /// The URL.
            /// </param>
            /// <returns>
            /// Callback.
            /// </returns>
            public Callback Create(string url)
            {
                var callback = new Callback();
                callback.uri = this.uri;
                callback.url = url;
                callback.Save();
                return callback;
            }

            #endregion
        }
    }
}