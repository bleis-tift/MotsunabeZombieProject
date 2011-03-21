using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotsunabeZombieProject
{
    public class CategorizedResult
    {
        public CategorizedResult(string body, params string[] categories)
        {
            Body = body;
            Categories = categories;
        }

        public string Body { get; private set; }

        public IEnumerable<string> Categories { get; private set; }
    }
}
