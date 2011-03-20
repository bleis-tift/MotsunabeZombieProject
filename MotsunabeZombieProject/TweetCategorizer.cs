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
            if (IsHashTag(body))
                return "HashTag\t" + body;
            return "Normal\t" + body;
        }

        bool IsHashTag(string body)
        {
            // TODO : #の前に入ったらダメな記号を後で調べる
            var regex = new Regex(@"(?:^|\s|[^a-zA-Z0-9_])#([a-zA-Z0-9_]+)");
            var ms = regex.Matches(body).Cast<Match>();
            return ms.Any() && ms.All(m => m.Success && IsNotInt(m.Groups[1].Value));
        }

        bool IsNotInt(string s)
        {
            int _;
            return int.TryParse(s, out _) == false;
        }
    }
}
