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
        [TestCase("bleis\tほげほげ　#hash", "HashTag\tほげほげ　#hash")]
        [TestCase("bleis\t#hash", "HashTag\t#hash")]
        [TestCase("bleis\tほげほげ#hash", "HashTag\tほげほげ#hash")]
        public void ハッシュタグ付きのTweetがHashTagに判定される(string record, string expected)
        {
            var categorizer = new TweetCategorizer();
            Assert.That(categorizer.Categorize(record), Is.EqualTo(expected));
        }

        [Test]
        public void リプライ付きのTweetがReplyに判定される()
        {
            var categorizer = new TweetCategorizer();
            var result = categorizer.Categorize("bleis\t@t_wada ほげほげ");
            Assert.That(result, Is.EqualTo("Reply\t@t_wada ほげほげ"));
        }

        [TestCase("bleis\t@t_wada ほげほげ", "Reply\t@t_wada ほげほげ")]
        public void リプライ付きのTweetがReplyに判定される(string record, string expected)
        {
            var categorizer = new TweetCategorizer();
            Assert.That(categorizer.Categorize(record), Is.EqualTo(expected));
        }
    }
}
