using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MotsunabeZombieProject
{
    public class CategorizedResult
    {
        public CategorizedResult(string body, params string[] categories)
        {
            Debug.Assert(categories != null && categories.Length > 0);
            Body = body;
            Categories = categories;
        }

        public string Body { get; private set; }

        public IEnumerable<string> Categories { get; private set; }

        public override string ToString()
        {
            return string.Join(",", Categories) + "\t" + Body;
        }
    }
}
