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
    }
}
