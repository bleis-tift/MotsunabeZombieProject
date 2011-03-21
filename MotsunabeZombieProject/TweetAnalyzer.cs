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
            return new[] { new CategorizedResult("", "Normal") };
        }

        public TweetProvider TweetProvider { get; set; }
    }

    public class TweetProvider
    {
        private string p;

        public TweetProvider(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }
    }
}
