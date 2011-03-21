using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace MotsunabeZombieProject
{
    public class TweetAnalyzer
    {
        public TweetAnalyzer()
        {
            TweetProvider = new TweetProvider(null);
        }

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
            var req = (HttpWebRequest)HttpWebRequest.Create(url);
            using (var res = req.GetResponse())
            using (var reader = new StreamReader(res.GetResponseStream()))
            {
                 return reader.ReadToEnd().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }
        }
    }
}