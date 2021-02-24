using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Infrastructure.Services.InMemory;
using MailSender.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.Tests.Infrastructure.Services.InMemory
{
    [TestClass]
    public class MessagesRepositoryTests
    {
        [TestMethod]
        public void GetAll_Test()
        {
            var repository = new MessagesRepository();

            var all = repository.GetAll();

            Assert.IsTrue(all.Any());
        }

        [TestMethod]
        public void Add_Test()
        {
            var repository = new MessagesRepository();
            var message = new Message
            {
                Title = "Unit test message",
                Text = "Unit test text message"
            };

            var actual_id = repository.Add(message);

            var all = repository.GetAll().ToArray();

            Assert.AreEqual(actual_id, message.Id);
            CollectionAssert.Contains(all, message);
            //StringAssert.Matches();
        }

    }
}
