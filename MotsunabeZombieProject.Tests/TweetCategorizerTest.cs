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
            var result = categorizer.Categorize("2011/03/21 11:19:05\tbleis\tほげほげ");
            Assert.That(result.Categories, Is.EqualTo(new[] { "Normal" }));
        }

        void AssertCategory(string body, string expected)
        {
            var categorizer = new TweetCategorizer();
            var result = categorizer.Categorize("2011/03/21 13:00:55\tbleis\t" + body);
            Assert.That(result.Categories, Is.EqualTo(new[] { expected }));
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

        [TestCase("@t_wada ほげほげ#hash", new[] { "Reply", "HashTag" })]
        [TestCase("@t_wada ほげほげ#hash", new[] { "HashTag", "Reply" }, Description="順不同")]
        [TestCase("@t_wada @mzp ほげほげ", new[] { "Reply", "Mention" })]
        [TestCase("@t_wada @mzp ほげほげ#hash", new[] { "Reply", "Mention", "HashTag" })]
        [TestCase(".@t_wada @mzp ほげほげ#hash", new[] { "Mention", "HashTag" })]
        public void 複数の種類を含むTweetの判定結果に含まれるすべての種類が存在する(string body, string[] expectedCategories)
        {
            var categorizer = new TweetCategorizer();
            Assert.That(categorizer.Categorize("2011/12/23 00:00:00\tbleis\t" + body).Categories, Is.EquivalentTo(expectedCategories));
        }
    }
}