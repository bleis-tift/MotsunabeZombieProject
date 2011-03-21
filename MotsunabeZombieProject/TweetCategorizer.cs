using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MotsunabeZombieProject
{
    using Matcher = Tuple<string, Func<string, bool>>;
    public class TweetCategorizer
    {
        public CategorizedResult Categorize(string record)
        {
            // yyyy/MM/dd HH:mm:ss\tScreenName\tBody
            Debug.Assert(record != null && Regex.IsMatch(record, @"\d{4}/\d{2}/\d{2} \d{2}:\d{2}:\d{2}\t[^\t]+\t.+"));
            var body = record.Split(new[] { '\t' }, 3)[2];
            var categories = GetCategories(body);
            return new CategorizedResult(body, categories);
        }

        string[] GetCategories(string body)
        {
            var result = matchers.Where(m => m.Value(body)).Select(m => m.Key);
            return result.Any() ? result.ToArray() : new[] { "Normal" };
        }

        static IDictionary<string, Func<string, bool>> matchers = new Dictionary<string, Func<string, bool>>() {
            { "HashTag", ContainsHashTag },
            // TODO : 後ろに入ったらダメな記号とかあるかを後で調べる
            { "Reply", body => Regex.IsMatch(body, @"^@[a-zA-Z0-9]+") },
            // TODO : ダメ記号を後で調べる
            { "Mention", body => Regex.IsMatch(body, @".@[a-zA-Z0-9]+") }
        };

        static bool ContainsHashTag(string body)
        {
            // TODO : #の前に入ったらダメな記号を後で調べる
            var ms = Regex.Matches(body, @"(?:^|\s|[^a-zA-Z0-9_])#([a-zA-Z0-9_]+)").Cast<Match>();
            return ms.Any() && ms.All(m => m.Success && IsNotInt(m.Groups[1].Value));
        }

        static bool IsNotInt(string s)
        {
            int _;
            return int.TryParse(s, out _) == false;
        }
    }
}
