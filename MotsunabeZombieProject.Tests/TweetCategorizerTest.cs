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
        string _(string body) { return "bleis\t" + body; }

        [Test]
        public void 普通のTweetがNormalに判定される()
        {
            var categorizer = new TweetCategorizer();
            var result = categorizer.Categorize(_("ほげほげ"));
            Assert.That(result, Is.EqualTo("Normal\tほげほげ"));
        }

        [TestCase("ほげほげ #hash", "HashTag\tほげほげ #hash")]
        [TestCase("ほげほげ #1234", "Normal\tほげほげ #1234")]
        [TestCase("ほげほげa#hash", "Normal\tほげほげa#hash")]
        [TestCase("ほげほげ　#hash", "HashTag\tほげほげ　#hash")]
        [TestCase("#hash", "HashTag\t#hash")]
        [TestCase("ほげほげ#hash", "HashTag\tほげほげ#hash")]
        public void ハッシュタグ付きのTweetがHashTagに判定される(string body, string expected)
        {
            var categorizer = new TweetCategorizer();
            Assert.That(categorizer.Categorize(_(body)), Is.EqualTo(expected));
        }

        [TestCase("@t_wada ほげほげ", "Reply\t@t_wada ほげほげ")]
        [TestCase("@ ほげほげ", "Normal\t@ ほげほげ")]
        [TestCase(".@t_wada ほげほげ", "Normal\t.@t_wada ほげほげ")]
        public void リプライ付きのTweetがReplyに判定される(string body, string expected)
        {
            var categorizer = new TweetCategorizer();
            Assert.That(categorizer.Categorize(_(body)), Is.EqualTo(expected));
        }
    }
}
