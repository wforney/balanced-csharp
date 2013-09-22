// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced
{
    /// <summary>
    ///     Class Settings.
    /// </summary>
    public static class Settings
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Settings" /> class.
        /// </summary>
        static Settings()
        {
            // ReSharper disable StringLiteralTypo
            Version = "1";
            Location = "https://api.balancedpayments.com";
            Agent = "balanced-csharp";
            
            // ReSharper restore StringLiteralTypo
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the agent.
        /// </summary>
        /// <value>The agent.</value>
        public static string Agent { get; set; }

        /// <summary>
        ///     Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public static string Location { get; set; }

        /// <summary>
        ///     Gets or sets the secret.
        /// </summary>
        /// <value>The secret.</value>
        public static string Secret { get; set; }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public static string Version { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Configures the specified secret.
        /// </summary>
        /// <param name="secret">
        /// The secret.
        /// </param>
        public static void Configure(string secret)
        {
            Secret = secret;
        }

        /// <summary>
        /// Configures the specified secret.
        /// </summary>
        /// <param name="secret">
        /// The secret.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        public static void Configure(string secret, string location)
        {
            Secret = secret;
            Location = location;
        }

        #endregion
    }
}