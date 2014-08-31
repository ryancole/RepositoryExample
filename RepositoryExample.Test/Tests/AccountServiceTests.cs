using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryExample.Entity;
using RepositoryExample.Service;

namespace RepositoryExample.Test
{
    [TestClass]
    public class AccountServiceTests
    {
        #region Tests

        [TestMethod]
        public void CanInsert()
        {
            using (var session = new WarcraftSession())
            {
                var account = session.Accounts.Insert(new Account
                {
                    Email = "foobar@test.com",
                    Password = "test"
                });

                Assert.IsNotNull(account);
                Assert.IsTrue(session.SaveChanges());
            }
        }

        [TestMethod]
        public void CanGetByEmail()
        {
            using (var session = new WarcraftSession())
            {
                var account = session.Accounts.GetByEmail("test@test.com");

                Assert.IsNotNull(account);
            }
        }

        [TestMethod]
        public void CanGetByEmailAndPassword()
        {
            using (var session = new WarcraftSession())
            {
                var account = session.Accounts.GetByEmailAndPassword("test@test.com", "test");

                Assert.IsNotNull(account);
            }
        }

        #endregion
    }
}
