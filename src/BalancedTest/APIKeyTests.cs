// --------------------------------------------------------------------------------------------------------------------
// <copyright file="APIKeyTest.cs" company="Balanced">
//   Copyright © 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BalancedTest
{
    using System.Collections.Generic;
    using System.Linq;

    using Balanced;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     Class APIKeyTest.
    /// </summary>
    [TestClass]
    public class APIKeyTests : Tests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Tests all.
        /// </summary>
        [TestMethod]
        public void TestAll()
        {
            Settings.Configure(null);

            var key1 = new APIKey();
            key1.Save();
            Settings.Configure(key1.Secret);

            var key2 = new APIKey();
            key2.Save();

            var key3 = new APIKey();
            key3.Save();

            IList<APIKey> keys = APIKey.Query.All();
            var expectedKeyIds = keys.Select(v => v.Id).ToList();
            var keyIds = keys.Select(v => v.Id).ToList();
            CollectionAssert.AreEqual(expectedKeyIds, keyIds);
        }

        /// <summary>
        ///     Tests the create.
        /// </summary>
        [TestMethod]
        public void TestCreate()
        {
            Assert.IsNotNull(Settings.Secret);

            var key = new APIKey();
            key.Save();
            Assert.IsNotNull(key.Id);
            Assert.IsNotNull(key.Secret);
        }

        /// <summary>
        ///     Tests the create anonymous.
        /// </summary>
        [TestMethod]
        public void TestCreateAnonymous()
        {
            Settings.Configure(null);

            var key = new APIKey();
            key.Meta["test"] = "this";
            key.Save();
            Assert.IsNotNull(key.Id);
            Assert.IsNotNull(key.Secret);
            var meta = new Dictionary<string, string> { { "test", "this" } };
            CollectionAssert.AreEqual(key.Meta, meta);
        }

        /// <summary>
        ///     Tests the delete.
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            Settings.Configure(null);

            var key = new APIKey();
            key.Save();
            Settings.Configure(key.Secret);
            key = new APIKey();
            key.Save();
            Assert.AreEqual(2, APIKey.Query.total);
            key.Delete();
            Assert.AreEqual(1, APIKey.Query.total);
        }

        #endregion
    }
}