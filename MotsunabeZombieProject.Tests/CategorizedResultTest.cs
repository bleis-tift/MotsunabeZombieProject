using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MotsunabeZombieProject;

namespace MotsunabeZombieProject.Tests
{
    [TestFixture]
    class CategorizedResultTest
    {
        [Test]
        public void CategorizedResultは元のTweet本体を持っている()
        {
            var cr = new CategorizedResult("ほげほげ");
            Assert.That(cr.Body, Is.EqualTo("ほげほげ"));
        }

        [Test]
        public void 判別結果を持っている()
        {
            var cr = new CategorizedResult("ほげほげ", "Normal");
            Assert.That(cr.Categories, Is.EqualTo(new[] { "Normal" }));
        }
    }
}
