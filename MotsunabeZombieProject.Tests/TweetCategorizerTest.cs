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

        void AssertCategory(string body, string expectedCategory)
        {
            var categorizer = new TweetCategorizer();
            Assert.That(categorizer.Categorize("bleis\t" + body), Is.EqualTo(expectedCategory + "\t" + body));
        }

        [TestCase("ほげほげ #hash", "HashTag")]
        [TestCase("ほげほげ #1234", "Normal")]
        [TestCase("ほげほげa#hash", "Normal")]
        [TestCase("ほげほげ　#hash", "HashTag")]
        [TestCase("#hash", "HashTag")]
        [TestCase("ほげほげ#hash", "HashTag")]
        public void ハッシュタグ付きのTweetがHashTagに判定される(string body, string expectedCategory)
        {
            AssertCategory(body, expectedCategory);
        }

        [TestCase("@t_wada ほげほげ", "Reply")]
        [TestCase("@ ほげほげ", "Normal")]
        [TestCase(".@t_wada ほげほげ", "Mention")]
        public void リプライ付きのTweetがReplyに判定される(string body, string expectedCategory)
        {
            AssertCategory(body, expectedCategory);
        }

        [TestCase(".@t_wada ほげほげ", "Mention")]
        public void メンション付のTweetがMentionに判定される(string body, string expectedCategory)
        {
            AssertCategory(body, expectedCategory);
        }
    }
}