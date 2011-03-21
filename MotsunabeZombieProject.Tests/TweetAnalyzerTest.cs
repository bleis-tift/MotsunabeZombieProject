﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MotsunabeZombieProject;

namespace MotsunabeZombieProject.Tests
{
    [TestFixture]
    class TweetAnalyzerTest
    {
        [Test]
        public void ネットワークからTweetを取得して解析できる()
        {
            var ta = new TweetAnalyzer();
            Assert.That(ta.Categorize("http://192.168.1.40:4567/public_timeline").Select(r => r.Categories), Is.EqualTo(new[] { new[] { "Normal" } }));
        }
    }
}
