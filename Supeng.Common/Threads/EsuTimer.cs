using System;
using System.Threading;
using Supeng.Common.IOs;
using Timer = System.Threading.Timer;

namespace Supeng.Common.Threads
{
    public abstract class EsuTimer : IDisposable
    {
        private readonly long distance;
        private Timer timer;

        protected EsuTimer(long distance)
        {
            this.distance = distance;
        }

        public void Start(object state)
        {
            timer = new Timer(Do, state, 0, Timeout.Infinite);
            StartProcess();
        }

        public void Stop()
        {
            if (timer != null)
                timer.Dispose();
        }

        protected virtual void Do(object state)
        {
            try
            {
                Progress();
            }
            catch (Exception ex)
            {
                LogException(ex.Message);
            }
            finally
            {
                timer.Change(distance, Timeout.Infinite);
            }
        }

        protected virtual void LogException(string errMsg)
        {
            var log = new EsuLogs("");
            log.WriteLogWithDatetime(errMsg);
        }

        //do something before your thread start
        protected virtual void StartProcess() { }

        //do something during a time
        protected abstract void Progress();
        public void Dispose()
        {
            Stop();
        }
    }
}
