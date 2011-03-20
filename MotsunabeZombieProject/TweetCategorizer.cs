using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MotsunabeZombieProject
{
    public class TweetCategorizer
    {
        public string Categorize(string record)
        {
            var body = record.Split(new[] { '\t' }, 2)[1];
            var regex = new Regex("#[a-zA-Z0-9_]");
            if (regex.IsMatch(body))
                return "HashTag\t" + body;
            return "Normal\t" + body;
        }
    }
}
