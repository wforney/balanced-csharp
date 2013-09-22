// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Resource.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Balanced
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Script.Serialization;

    using Balanced.Errors;

    /// <summary>
    /// The resource.
    /// </summary>
    public class Resource
    {
        #region Fields

        /// <summary>
        /// The id.
        /// </summary>
        public string id;

        /// <summary>
        /// The uri.
        /// </summary>
        public string uri;

        /// <summary>
        /// The client.
        /// </summary>
        protected Client client;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource" /> class.
        /// </summary>
        public Resource()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource" /> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public Resource(IDictionary<string, object> data)
        {
            this.Deserialize(data);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource" /> class.
        /// </summary>
        /// <param name="uri">The uri.</param>
        public Resource(string uri)
        {
            IDictionary<string, object> Payload = this.client.Get(uri);
            this.Deserialize(Payload);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>The client.</value>
        public Client Client
        {
            get
            {
                if (this.client == null)
                {
                    this.client = new Client();
                }

                return this.client;
            }
        }

        /// <summary>
        /// Gets or sets the root_uri.
        /// </summary>
        /// <value>The root_uri.</value>
        public virtual string RootURI
        {
            get
            {
                return null;
            }

            set
            {
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The delete.
        /// </summary>
        public virtual void Delete()
        {
            this.client.Delete(this.uri);
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="data">The data.</param>
        public virtual void Deserialize(IDictionary<string, object> data)
        {
            this.uri = (String)data["uri"];

            PropertyInfo[] Properties = this.GetType().GetProperties();
            IEnumerable<string> PropertyNames = from x in Properties select x.Name;
            foreach (var entry in data)
            {
                if (PropertyNames.Any(x => x == entry.Key))
                {
                    var clsProperty = this.GetType().GetProperty(entry.Key);
                    switch (clsProperty.PropertyType.Name)
                    {
                        case "DateTime":
                            {
                                DateTime dttm = DateTime.Parse((string)entry.Value, null, DateTimeStyles.RoundtripKind);
                                clsProperty.SetValue(this, dttm, null);
                                break;
                            }

                        case "Dictionary`2":
                            {
                                if (entry.Value != null && entry.Value.ToString() != string.Empty)
                                {
                                    var jsSerializer = new JavaScriptSerializer();
                                    clsProperty.SetValue(
                                        this, 
                                        jsSerializer.DeserializeObject(entry.Value.ToString()), 
                                        null);
                                }

                                break;
                            }

                        default:
                            {
                                clsProperty.SetValue(this, entry.Value, null);
                                break;
                            }
                    }
                }
            }
        }

        /// <summary>
        /// The refresh.
        /// </summary>
        /// <exception cref="Exception">Cannot refresh before creation</exception>
        public virtual void Refresh()
        {
            if (this.uri == null)
            {
                throw new Exception("Cannot refresh before creation");
            }

            this.Deserialize(this.client.Get(this.uri));
        }

        /// <summary>
        /// The save.
        /// </summary>
        /// <exception cref="Exception">Cannot create top level</exception>
        public virtual void Save()
        {
            if (this.uri == null && this.RootURI != null)
            {
                var data = new Dictionary<string, object>();
                this.Serialize(data);
                this.Deserialize(this.Client.Post(this.RootURI, data));
            }
            else if (this.uri != null)
            {
                var data = new Dictionary<string, object>();
                this.Serialize(data);
                this.Deserialize(this.Client.Put(this.uri, data));
            }
            else
            {
                if (this.RootURI == null)
                {
                    throw new Exception("Cannot create top level");
                }

                var data = new Dictionary<string, object>();
                this.Serialize(data);
                this.Deserialize(this.Client.Post(this.RootURI, data));
            }
        }

        /// <summary>
        /// The serialize.
        /// </summary>
        /// <param name="data">The data.</param>
        public virtual void Serialize(IDictionary<string, object> data)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="dst">The dst.</param>
        protected void Deserialize(object src, out Dictionary<string, string> dst)
        {
            dst = ((IDictionary<string, object>)src).ToDictionary(kv => kv.Key, kv => (string)kv.Value);

            PropertyInfo[] Properties = this.GetType().GetProperties();
            IEnumerable<string> PropertyNames = from x in Properties select x.Name;
            foreach (var entry in dst)
            {
                if (PropertyNames.Any(x => x == entry.Key))
                {
                    var clsProperty = this.GetType().GetProperty(entry.Key);
                    clsProperty.SetValue(this, entry.Value);
                }
            }
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="dst">The dst.</param>
        protected void Deserialize(object src, out DateTime dst)
        {
            dst = DateTime.Parse((string)src, null, DateTimeStyles.RoundtripKind);
        }

        #endregion
    }

    /// <summary>
    /// The resource page.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResourcePage<T>
        where T : Resource, new()
    {
        #region Fields

        /// <summary>
        /// The _first_uri.
        /// </summary>
        public string _first_uri;

        /// <summary>
        /// The _items.
        /// </summary>
        public List<T> _items;

        /// <summary>
        /// The _last_uri.
        /// </summary>
        public string _last_uri;

        /// <summary>
        /// The _next_uri.
        /// </summary>
        public string _next_uri;

        /// <summary>
        /// The _previous_uri.
        /// </summary>
        public string _previous_uri;

        /// <summary>
        /// The _total.
        /// </summary>
        public long _total;

        /// <summary>
        /// The uri.
        /// </summary>
        public string uri;

        /// <summary>
        /// The _client.
        /// </summary>
        protected Client _client;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePage{T}" /> class.
        /// </summary>
        /// <param name="uri">The uri.</param>
        public ResourcePage(string uri)
        {
            this.uri = uri;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePage{T}" /> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public ResourcePage(IDictionary<string, object> data)
        {
            this.Deserialize(data);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>The client.</value>
        public Client Client
        {
            get
            {
                if (this._client == null)
                {
                    this._client = new Client();
                }

                return this._client;
            }
        }

        /// <summary>
        /// Gets the last.
        /// </summary>
        /// <value>The last.</value>
        public ResourcePage<T> Last
        {
            get
            {
                if (this.LastUri == null)
                {
                    return null;
                }

                return new ResourcePage<T>(this.LastUri);
            }
        }

        /// <summary>
        /// Gets the last uri.
        /// </summary>
        /// <value>The last URI.</value>
        public string LastUri
        {
            get
            {
                if (!this.Loaded)
                {
                    this.Load();
                }

                return this._last_uri;
            }
        }

        /// <summary>
        /// Gets the next.
        /// </summary>
        /// <value>The next.</value>
        public ResourcePage<T> Next
        {
            get
            {
                if (this.next_uri == null)
                {
                    return null;
                }

                return new ResourcePage<T>(this.next_uri);
            }
        }

        /// <summary>
        /// Gets the previous.
        /// </summary>
        /// <value>The previous.</value>
        public ResourcePage<T> Previous
        {
            get
            {
                if (this.previous_uri == null)
                {
                    return null;
                }

                return new ResourcePage<T>(this.previous_uri);
            }
        }

        /// <summary>
        /// Gets the first.
        /// </summary>
        /// <value>The first.</value>
        public ResourcePage<T> first
        {
            get
            {
                if (this.first_uri == null)
                {
                    return null;
                }

                return new ResourcePage<T>(this.first_uri);
            }
        }

        /// <summary>
        /// Gets the first_uri.
        /// </summary>
        /// <value>The first_uri.</value>
        public string first_uri
        {
            get
            {
                if (!this.Loaded)
                {
                    this.Load();
                }

                return this._first_uri;
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public List<T> items
        {
            get
            {
                if (!this.Loaded)
                {
                    this.Load();
                }

                return this._items;
            }
        }

        /// <summary>
        /// Gets the next_uri.
        /// </summary>
        /// <value>The next_uri.</value>
        public string next_uri
        {
            get
            {
                if (!this.Loaded)
                {
                    this.Load();
                }

                return this._next_uri;
            }
        }

        /// <summary>
        /// Gets the previous_uri.
        /// </summary>
        /// <value>The previous_uri.</value>
        public string previous_uri
        {
            get
            {
                if (!this.Loaded)
                {
                    this.Load();
                }

                return this._previous_uri;
            }
        }

        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value>The total.</value>
        public long total
        {
            get
            {
                if (!this.Loaded)
                {
                    this.Load();
                }

                return this._total;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether loaded.
        /// </summary>
        /// <value><c>true</c> if loaded; otherwise, <c>false</c>.</value>
        protected bool Loaded
        {
            get
            {
                return this._items != null;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The deserialize item.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The item.</returns>
        protected static T DeserializeItem(IDictionary<string, object> data)
        {
            var i = new T();
            i.Deserialize(data);
            return i;
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="data">The data.</param>
        protected virtual void Deserialize(IDictionary<string, object> data)
        {
            this.uri = (string)data["uri"];
            this._items =
                ((IList<object>)data["items"]).Select(v => DeserializeItem((IDictionary<string, object>)v)).ToList();
            this._first_uri = (string)data["first_uri"];
            this._previous_uri = (string)data["previous_uri"];
            this._next_uri = (string)data["next_uri"];
            this._last_uri = (string)data["last_uri"];
            this._total = (long)data["total"];
        }

        /// <summary>
        /// The load.
        /// </summary>
        protected void Load()
        {
            this.Deserialize(this.Client.Get(this.uri));
        }

        #endregion
    }

    /// <summary>
    /// The resource pagination.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResourcePagination<T> : IEnumerable<T>
        where T : Resource, new()
    {
        #region Fields

        /// <summary>
        /// The _client.
        /// </summary>
        protected Client _client;

        /// <summary>
        /// The cls.
        /// </summary>
        protected Type cls;

        /// <summary>
        /// The parameters.
        /// </summary>
        protected NameValueCollection parameters;

        /// <summary>
        /// The path.
        /// </summary>
        protected string path;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePagination{T}" /> class.
        /// </summary>
        /// <param name="uri">The uri.</param>
        public ResourcePagination(string uri)
        {
            int i = uri.IndexOf('?');
            if (i == -1)
            {
                this.path = uri;
                this.parameters = HttpUtility.ParseQueryString(string.Empty);
            }
            else
            {
                this.path = uri.Substring(0, i);
                this.parameters = HttpUtility.ParseQueryString(uri.Substring(i));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePagination{T}" /> class.
        /// </summary>
        /// <param name="cls">The cls.</param>
        /// <param name="uri">The uri.</param>
        public ResourcePagination(Type cls, string uri)
        {
            this.cls = cls;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>The client.</value>
        public Client Client
        {
            get
            {
                if (this._client == null)
                {
                    this._client = new Client();
                }

                return this._client;
            }
        }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>The limit.</value>
        public int? limit
        {
            get
            {
                if (this.parameters["limit"] == null)
                {
                    return null;
                }

                return int.Parse(this.parameters["limit"]);
            }

            set
            {
                this.parameters["limit"] = value.ToString();
            }
        }

        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value>The total.</value>
        public long total
        {
            get
            {
                int? limit = this.limit;
                limit = 1;
                var current = new ResourcePage<T>(this.uri);
                long total = current.total;
                this.limit = limit;
                return total;
            }
        }

        /// <summary>
        /// Gets the uri.
        /// </summary>
        /// <value>The URI.</value>
        public string uri
        {
            get
            {
                if (this.parameters.Count == 0)
                {
                    return this.path;
                }

                return this.path + "?" + this.parameters;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The all.
        /// </summary>
        /// <returns>The <see cref="IList" />.</returns>
        public IList<T> All()
        {
            return new List<T>(this);
        }

        /// <summary>
        /// The first.
        /// </summary>
        /// <returns>The item.</returns>
        public T First()
        {
            int? theLimit = this.limit;
            this.limit = 1;
            IList<T> items = this.All();
            this.limit = theLimit;
            if (items.Count == 0)
            {
                return null;
            }

            return items[0];
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator" />.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var current = new ResourcePage<T>(this.uri);
            while (current != null)
            {
                foreach (var i in current.items)
                {
                    yield return i;
                }

                current = current.Next;
            }
        }

        /// <summary>
        /// The one.
        /// </summary>
        /// <returns>The item.</returns>
        /// <exception cref="NoResultsFound"></exception>
        /// <exception cref="MultipleResultsFound"></exception>
        public T One()
        {
            int? limit = this.limit;
            this.limit = 2;
            IList<T> items = this.All();
            this.limit = limit;
            if (items.Count == 0)
            {
                throw new NoResultsFound();
            }

            if (items.Count > 1)
            {
                throw new MultipleResultsFound();
            }

            return items[0];
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator" />.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }

    /// <summary>
    /// The resource query order.
    /// </summary>
    public enum ResourceQueryOrder
    {
        /// <summary>
        /// The ascending.
        /// </summary>
        ASCENDING,

        /// <summary>
        /// The descending.
        /// </summary>
        DESCENDING, 
    }

    /// <summary>
    /// The resource query.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResourceQuery<T> : ResourcePagination<T>
        where T : Resource, new()
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceQuery{T}" /> class.
        /// </summary>
        /// <param name="uri">The uri.</param>
        public ResourceQuery(string uri)
            : base(uri)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceQuery{T}" /> class.
        /// </summary>
        /// <param name="cls">The cls.</param>
        /// <param name="uri">The uri.</param>
        public ResourceQuery(Type cls, string uri)
            : base(cls, uri)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The filter.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="ResourceQuery{T}" />.</returns>
        public ResourceQuery<T> Filter(string field, params object[] values)
        {
            string n = string.Format("{0}", field);
            string v = string.Join(",", new List<string>(this.Serialize(values)));
            this.parameters.Add(n, v);
            return this;
        }

        /// <summary>
        /// The filter.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="op">The op.</param>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="ResourceQuery{T}" />.</returns>
        public ResourceQuery<T> Filter(string field, string op, params object[] values)
        {
            string n = string.Format("{0}[{1}]", field, op);
            string v = string.Join(",", new List<string>(this.Serialize(values)));
            this.parameters.Add(n, v);
            return this;
        }

        /// <summary>
        /// The order by.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The <see cref="ResourceQuery{T}" />.</returns>
        public ResourceQuery<T> OrderBy(string field)
        {
            this.parameters.Add("sort", field);
            return this;
        }

        /// <summary>
        /// The order by.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>The <see cref="ResourceQuery{T}" />.</returns>
        public ResourceQuery<T> OrderBy(string field, ResourceQueryOrder direction)
        {
            string sort = string.Format("{0},{1}", field, direction == ResourceQueryOrder.ASCENDING ? "asc" : "desc");
            this.parameters.Add("sort", sort);
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The serialize.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="IEnumerable" />.</returns>
        protected IEnumerable<string> Serialize(object[] values)
        {
            return values.Select(this.Serialize);
        }

        /// <summary>
        /// The serialize.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="string" />.</returns>
        protected string Serialize(object value)
        {
            var s = value as string;
            if (s != null)
            {
                return s;
            }

            if (value is DateTime)
            {
                return ((DateTime)value).ToString("o");
            }

            return value.ToString();
        }

        #endregion
    }

    /// <summary>
    /// The resource collection.
    /// </summary>
    /// <typeparam name="T">The type of resource.</typeparam>
    public class ResourceCollection<T> : ResourcePagination<T>
        where T : Resource, new()
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCollection{T}" /> class.
        /// </summary>
        /// <param name="uri">The uri.</param>
        public ResourceCollection(string uri)
            : base(uri)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCollection{T}" /> class.
        /// </summary>
        /// <param name="cls">The cls.</param>
        /// <param name="uri">The uri.</param>
        public ResourceCollection(Type cls, string uri)
            : base(cls, uri)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>The query.</value>
        public ResourceQuery<T> Query
        {
            get
            {
                return new ResourceQuery<T>(this.uri);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The item.</returns>
        public T Create(IDictionary<string, object> data)
        {
            IDictionary<string, object> result = this.Client.Post(this.uri, data);
            var t = new T();
            t.Deserialize(result);
            return t;
        }

        #endregion
    }
}