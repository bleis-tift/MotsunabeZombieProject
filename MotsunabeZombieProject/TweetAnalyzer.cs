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
            return TweetProvider.GetTweets(url).Select(categorizer.Categorize);
        }

        internal TweetProvider TweetProvider { get; set; }
    }

    internal class TweetProvider
    {
        internal TweetProvider(string[] tweets)
        {
            this.tweets = tweets;
        }

        readonly IEnumerable<string> tweets;

        internal IEnumerable<string> GetTweets(string url)
        {
            return tweets ?? GetTweetsFromUrl(url);
        }

        private IEnumerable<string> GetTweetsFromUrl(string url)
        {
            throw new NotImplementedException();
        }
    }
}
