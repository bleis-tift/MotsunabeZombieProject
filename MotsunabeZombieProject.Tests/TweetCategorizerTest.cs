using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MotsunabeZombieProject;

namespace MotsunabeZombieProject.Tests
{
    [TestFixture]
    class TweetCategorizerTest
    {
        [Test]
        public void 普通のTweetがNormalに判定される()
        {
            var categorizer = new TweetCategorizer();
            var result = categorizer.Categorize("bleis\tほげほげ");
            Assert.That(result, Is.EqualTo("Normal\tほげほげ"));
        }

        [TestCase("bleis\tほげほげ #hash", "HashTag\tほげほげ #hash")]
        [TestCase("bleis\tほげほげ #1234", "Normal\tほげほげ #1234")]
        [TestCase("bleis\tほげほげa#hash", "Normal\tほげほげa#hash")]
        public void ハッシュタグ付きのTweetがHashTagに判定される(string record, string expected)
        {
            var categorizer = new TweetCategorizer();
            Assert.That(categorizer.Categorize(record), Is.EqualTo(expected));
        }
    }
}
