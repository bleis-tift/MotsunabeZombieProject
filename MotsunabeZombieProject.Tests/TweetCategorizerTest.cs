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
            Assert.That(result.Categories, Is.EqualTo(new[] { "Normal" }));
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
            var result = categorizer.Categorize("bleis\t" + body);
            Assert.That(result.Categories, Is.EqualTo(new[] { expectedCategory }));
        }
#if false

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

        [TestCase("@t_wada ほげほげ#hash", new[] { "Reply,HashTag", "HashTag,Reply" })]
        public void 複数の種類を含むTweetがカンマ区切りで連結される(string body, string[] expectedCategories)
        {
            var categorizer = new TweetCategorizer();
            var result = categorizer.Categorize("bleis\t" + body);
            // 順番はどうでもいい
            foreach (var exCat in expectedCategories)
            {
                if (result.StartsWith(exCat + "\t"))
                {
                    Assert.Pass();
                    return;
                }
            }
            Assert.Fail(string.Format("expected starts with [{0}] but [{1}].", string.Join(" or ", expectedCategories), result));
        }
#endif
    }
}