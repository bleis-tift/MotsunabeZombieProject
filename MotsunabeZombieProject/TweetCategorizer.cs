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
            var body = record.Split(new[] { '\t' }, 2);
            var regex = new Regex("#[a-zA-Z0-9_]");
            if (regex.IsMatch(body[1]))
                return "HashTag\t" + body[1];
            return "Normal\t" + body[1];
        }
    }
}
