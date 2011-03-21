using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MotsunabeZombieProject;

namespace MotsunabeZombieProject.Tests
{
    [TestFixture]
    class TweetAnalyzerTest
    {
        [TestCaseSource("TweetsAndExpectedCategories")]
        public void ネットワークからTweetを取得して解析できる(string[] tweets, string[][] expectedCategories)
        {
            var ta = new TweetAnalyzer()
            {
                TweetProvider = new TweetProvider(tweets)
            };
            Assert.That(ta.Categorize("http://192.168.1.40:4567/public_timeline").Select(r => r.Categories), Is.EqualTo(expectedCategories));
        }

        static object[][] TweetsAndExpectedCategories = new[] {
            new object[] { new[] { "2011/03/21 12:16:25\tbleis\tほげほげ" }, new[] { new[] { "Normal" } } },
            new object[] { new[] { "2011/03/21 12:16:25\tbleis\tほげほげ", "2011/03/21 12:22:30\tmzp\t@bleis ほげほげ" }, new[] { new[] { "Normal" }, new [] { "Reply" } } }
        };
    }
}
