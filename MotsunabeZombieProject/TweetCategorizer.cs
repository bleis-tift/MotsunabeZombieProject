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
            return GetCategory(body) + "\t" + body;
        }

        string GetCategory(string body)
        {
            if (IsHashTag(body))
                return "HashTag";
            if (IsReply(body))
                return "Reply";
            if (IsMention(body))
                return "Mention";
            return "Normal";
        }

        bool IsHashTag(string body)
        {
            // TODO : #の前に入ったらダメな記号を後で調べる
            var ms = Regex.Matches(body, @"(?:^|\s|[^a-zA-Z0-9_])#([a-zA-Z0-9_]+)").Cast<Match>();
            return ms.Any() && ms.All(m => m.Success && IsNotInt(m.Groups[1].Value));
        }

        bool IsReply(string body)
        {
            // TODO : 後ろに入ったらダメな記号とかあるかを後で調べる
            return Regex.IsMatch(body, @"^@[a-zA-Z0-9]+");
        }

        bool IsMention(string body)
        {
            return Regex.IsMatch(body, @"@[a-zA-Z0-9_]+");
        }

        bool IsNotInt(string s)
        {
            int _;
            return int.TryParse(s, out _) == false;
        }
    }
}
