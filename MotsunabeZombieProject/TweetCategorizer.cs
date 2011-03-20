using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotsunabeZombieProject
{
    public class TweetCategorizer
    {
        public string Categorize(string record)
        {
            var body = record.Split(new[] { '\t' }, 2);
            return "Normal\t" + body[1];
        }
    }
}
