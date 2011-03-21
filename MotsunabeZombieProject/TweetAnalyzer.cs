using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotsunabeZombieProject
{
    public class TweetAnalyzer
    {
        internal IEnumerable<CategorizedResult> Categorize(string url)
        {
            var categorizer = new TweetCategorizer();
            return TweetProvider.Tweets.Select(categorizer.Categorize);
        }

        internal TweetProvider TweetProvider { get; set; }
    }

    internal class TweetProvider
    {
        internal TweetProvider(string[] tweets)
        {
            this.tweets = tweets;
        }

        readonly string[] tweets;
        internal IEnumerable<string> Tweets
        {
            get { return tweets; }
        }
    }
}
