using System;
using System.Globalization;
using Supeng.Common.Threads;

namespace Supeng.Common.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new TestTimer(1000);
            timer.Start(null);
            Console.Read();
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
