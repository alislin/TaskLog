using System;
using System.Diagnostics;

namespace TaskLog.Client.Libs
{
    public class DebugTime
    {
        private static DebugTime current = new DebugTime();

        /// <summary>
        /// 时间打点
        /// </summary>
        public static DebugTime dt => current;
        public DebugTime()
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
        }
        private Stopwatch Stopwatch { get; set; }
        /// <summary>
        /// 输出耗时
        /// </summary>
        /// <param name="log"></param>
        public void Check(string log)
        {
            Console.WriteLine($"[{log}]: 耗时 {Stopwatch.Elapsed.TotalMilliseconds.ToString("N2")} 毫秒");
            Stopwatch.Restart();
        }

        /// <summary>
        /// 开始计时点
        /// </summary>
        public void Check()
        {
            Stopwatch.Restart();
        }
    }
}
