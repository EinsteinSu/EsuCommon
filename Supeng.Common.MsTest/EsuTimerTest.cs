using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Common.Threads;

namespace Supeng.Common.MsTest
{
    [TestClass]
    public class EsuTimerTest
    {
        [TestMethod]
        public void Test()
        {
            var timer = new TestTimer(1000);
            timer.Start(null);
        }
    }

    internal class TestTimer : EsuTimer
    {
        public TestTimer(long distance)
            : base(distance)
        {
        }

        protected override void StartProcess()
        {
            Console.WriteLine("Start process");
        }

        private int i = 0;
        protected override void Progress()
        {
            i++;
            Console.WriteLine(i.ToString(CultureInfo.InvariantCulture));
        }
    }
}
