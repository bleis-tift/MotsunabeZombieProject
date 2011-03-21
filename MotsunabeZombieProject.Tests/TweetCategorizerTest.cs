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
        string _(string category, string body) { return category + "\t" + body; }

        [Test]
        public void 普通のTweetがNormalに判定される()
        {
            var categorizer = new TweetCategorizer();
            var result = categorizer.Categorize(_("ほげほげ"));
            Assert.That(result, Is.EqualTo("Normal\tほげほげ"));
        }

        [TestCase("ほげほげ #hash", "HashTag")]
        [TestCase("ほげほげ #1234", "Normal")]
        [TestCase("ほげほげa#hash", "Normal")]
        [TestCase("ほげほげ　#hash", "HashTag")]
        [TestCase("#hash", "HashTag")]
        [TestCase("ほげほげ#hash", "HashTag")]
        public void ハッシュタグ付きのTweetがHashTagに判定される(string body, string expectedCategory)
        {
            var categorizer = new TweetCategorizer();
            Assert.That(categorizer.Categorize(_(body)), Is.EqualTo(_(expectedCategory, body)));
        }

        [TestCase("@t_wada ほげほげ", "Reply")]
        [TestCase("@ ほげほげ", "Normal")]
        [TestCase(".@t_wada ほげほげ", "Mention")]
        public void リプライ付きのTweetがReplyに判定される(string body, string expectedCategory)
        {
            var categorizer = new TweetCategorizer();
            Assert.That(categorizer.Categorize(_(body)), Is.EqualTo(_(expectedCategory, body)));
        }

        [TestCase(".@t_wada ほげほげ", "Mention")]
        public void メンション付のTweetがMentionに判定される(string body, string expectedCategory)
        {
            var categorizer = new TweetCategorizer();
            Assert.That(categorizer.Categorize(_(body)), Is.EqualTo(_(expectedCategory, body)));
        }
    }
}