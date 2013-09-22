// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Client.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Net;
    using System.Text;

    using Balanced.Errors;

    /// <summary>
    ///     Class Client.
    /// </summary>
    public class Client
    {
        #region Static Fields

        /// <summary>
        ///     The accept type
        /// </summary>
        public static readonly string AcceptType = "application/json";

        /// <summary>
        ///     The content type
        /// </summary>
        public static readonly string ContentType = string.Format("application/json");

        /// <summary>
        ///     The encoding
        /// </summary>
        public static readonly Encoding Encoding = Encoding.UTF8;

        /// <summary>
        ///     The base URI
        /// </summary>
        public static Uri BaseUri;

        /// <summary>
        ///     The secret
        /// </summary>
        public static string Secret;

        /// <summary>
        ///     The user agent
        /// </summary>
        public static string UserAgent;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="baseUri">
        /// The base URI.
        /// </param>
        /// <param name="secret">
        /// The secret.
        /// </param>
        public Client(string baseUri, string secret)
        {
            BaseUri = new Uri(baseUri);
            Secret = secret;
            UserAgent = string.Format("{0}/{1}", Settings.Agent, Settings.Version);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Client" /> class.
        /// </summary>
        public Client()
            : this(Settings.Location, Settings.Secret)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Deletes the specified URI.
        /// </summary>
        /// <param name="uri">
        /// The URI.
        /// </param>
        public void Delete(string uri)
        {
            this.Request("DELETE", uri, null, null);
        }

        /// <summary>
        /// Gets the specified URI.
        /// </summary>
        /// <param name="uri">
        /// The URI.
        /// </param>
        /// <returns>
        /// IDictionary{System.StringSystem.Object}.
        /// </returns>
        public IDictionary<string, object> Get(string uri)
        {
            return this.Request("GET", uri, null, null);
        }

        /// <summary>
        /// Gets the specified URI.
        /// </summary>
        /// <param name="uri">
        /// The URI.
        /// </param>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// IDictionary{System.StringSystem.Object}.
        /// </returns>
        public IDictionary<string, object> Get(string uri, NameValueCollection query)
        {
            return this.Request("GET", uri, query, null);
        }

        /// <summary>
        /// Posts the specified URI.
        /// </summary>
        /// <param name="uri">
        /// The URI.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// IDictionary{System.StringSystem.Object}.
        /// </returns>
        public IDictionary<string, object> Post(string uri, IDictionary<string, object> data)
        {
            return this.Request("POST", uri, null, data);
        }

        /// <summary>
        /// Puts the specified URI.
        /// </summary>
        /// <param name="uri">
        /// The URI.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// IDictionary{System.StringSystem.Object}.
        /// </returns>
        public IDictionary<string, object> Put(string uri, IDictionary<string, object> data)
        {
            return this.Request("PUT", uri, null, data);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the error.
        /// </summary>
        /// <param name="ex">
        /// The executable.
        /// </param>
        /// <returns>
        /// The Exception.
        /// </returns>
        private Exception CreateError(WebException ex)
        {
            Contract.Requires<ArgumentNullException>(ex != null);

            if (ex.Status != WebExceptionStatus.ProtocolError || ex.Response == null)
            {
                return null;
            }

            var response = (HttpWebResponse)ex.Response;

            string body;
            using (Stream stream = response.GetResponseStream())
            {
                var reader = new StreamReader(stream);
                body = reader.ReadToEnd();
            }

            if (response.ContentType != AcceptType || body.Length == 0)
            {
                return null;
            }

            IDictionary<string, object> data = this.Deserialize(body);
            return Error.Create(data);
        }

        /// <summary>
        /// Deserializes the specified raw.
        /// </summary>
        /// <param name="raw">
        /// The raw.
        /// </param>
        /// <returns>
        /// IDictionary{System.StringSystem.Object}.
        /// </returns>
        private IDictionary<string, object> Deserialize(string raw)
        {
            return (IDictionary<string, object>)SimpleJson.DeserializeObject(raw);
        }

        /// <summary>
        /// Requests the specified method.
        /// </summary>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <param name="relativeUri">
        /// The relative URI.
        /// </param>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// IDictionary{System.StringSystem.Object}.
        /// </returns>
        private IDictionary<string, object> Request(
            string method, 
            string relativeUri, 
            NameValueCollection query, 
            IDictionary<string, object> data)
        {
            var builder = new UriBuilder(BaseUri);
            if (query != null)
            {
                builder.Path = relativeUri;
                builder.Query = query.ToString();
            }
            else
            {
                int i = relativeUri.IndexOf('?');
                if (i == -1)
                {
                    builder.Path = relativeUri;
                }
                else
                {
                    builder.Path = relativeUri.Substring(0, i);
                    builder.Query = relativeUri.Substring(i);
                }
            }

            Uri uri = builder.Uri;

            var req = (HttpWebRequest)WebRequest.Create(uri);
            req.UserAgent = UserAgent;
            req.Method = method;
            req.Accept = AcceptType;
            req.Headers.Add("Accept-Charset", Encoding.WebName);
            if (Secret != null)
            {
                req.Headers.Add(
                    "Authorization", 
                    "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(Secret + ":")));
            }

            if (data != null)
            {
                req.ContentType = ContentType;
                string t = this.Serialize(data);
                byte[] content = Encoding.GetBytes(this.Serialize(data));
                req.ContentLength = content.Length;
                using (Stream stream = req.GetRequestStream())
                {
                    stream.Write(content, 0, content.Length);
                }
            }

            try
            {
                using (var resp = (HttpWebResponse)req.GetResponse())
                {
                    using (var stream = resp.GetResponseStream())
                    {
                        var reader = new StreamReader(stream);
                        var body = reader.ReadToEnd();
                        return body.Length == 0 ? null : this.Deserialize(body);
                    }
                }
            }
            catch (WebException ex)
            {
                Exception error = this.CreateError(ex);
                if (error == null)
                {
                    throw;
                }

                throw error;
            }
        }

        /// <summary>
        /// Serializes the specified data.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The data string.
        /// </returns>
        private string Serialize(IDictionary<string, object> data)
        {
            return SimpleJson.SerializeObject(data);
        }

        #endregion
    }
}