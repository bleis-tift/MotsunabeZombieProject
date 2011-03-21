using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MotsunabeZombieProject
{
    public class TweetCategorizer
    {
        public CategorizedResult Categorize(string record)
        {
            var body = record.Split(new[] { '\t' }, 3)[2];
            var categories = GetCategories(body);
            return new CategorizedResult(body, categories);
        }

        string[] GetCategories(string body)
        {
            var result = new HashSet<string>();
            if (ContainsHashTag(body))
                result.Add("HashTag");
            if (ContainsReply(body))
                result.Add("Reply");
            if (ContainsMention(body))
                result.Add("Mention");
            return result.Any() ? result.ToArray() : new[] { "Normal" };
        }

        bool ContainsHashTag(string body)
        {
            // TODO : #の前に入ったらダメな記号を後で調べる
            var ms = Regex.Matches(body, @"(?:^|\s|[^a-zA-Z0-9_])#([a-zA-Z0-9_]+)").Cast<Match>();
            return ms.Any() && ms.All(m => m.Success && IsNotInt(m.Groups[1].Value));
        }

        bool ContainsReply(string body)
        {
            // TODO : 後ろに入ったらダメな記号とかあるかを後で調べる
            return Regex.IsMatch(body, @"^@[a-zA-Z0-9]+");
        }

        bool ContainsMention(string body)
        {
            // TODO : ダメ記号を後で調べる
            return Regex.IsMatch(body, @".@[a-zA-Z0-9_]+");
        }

        bool IsNotInt(string s)
        {
            int _;
            return int.TryParse(s, out _) == false;
        }
    }
}
