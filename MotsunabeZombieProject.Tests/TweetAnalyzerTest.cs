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
            TestCase(Tweets("2011/03/21 12:16:25\tbleis\tほげほげ"), Categories(new[] { "Normal" })),
            TestCase(Tweets("2011/03/21 12:16:25\tbleis\tほげほげ", "2011/03/21 12:22:30\tmzp\t@bleis ほげほげ"), Categories(new[] { "Normal" }, new[] { "Reply" })),
            TestCase(Tweets("2011/03/21 14:12:00\tbleis\t@mzp ほげほげ#hash", "2011/03/21 14:12:01\tmzp\tぴよぴよ"), Categories(new[] { "HashTag", "Reply"}, new[] { "Normal" }))
        };

        static object[] TestCase(object tweets, object expectedCategories)
        {
            return new[] { tweets, expectedCategories };
        }

        static object Tweets(params string[] tweets) { return tweets; }

        static object Categories(params string[][] categories) { return categories; }
    }
}
