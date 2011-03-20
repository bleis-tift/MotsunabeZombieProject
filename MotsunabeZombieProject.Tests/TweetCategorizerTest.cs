using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MotsunabeZombieProject;

namespace MotsunabeZombieProject.Tests
{
    [TestFixture]
    class TweetCategorizerTest
    {
        [Test]
        public void 普通のTweetがNormalに判定される()
        {
            var categorizer = new TweetCategorizer();
            var result = categorizer.Categorize("bleis\tほげほげ");
            Assert.That(result, Is.EqualTo("Normal\tほげほげ"));
        }

        [Test]
        public void ハッシュタグ付きのTweetがHashTagに判定される()
        {
            var categorizer = new TweetCategorizer();
            var result = categorizer.Categorize("bleis\tほげほげ #hash");
            Assert.That(result, Is.EqualTo("HashTag\tほげほげ #hash"));
        }
    }
}
