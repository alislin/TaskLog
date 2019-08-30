using System;
using System.Collections.Generic;

namespace TaskLog.DataModel
{
    /// <summary>
    /// 日志
    /// </summary>
    public class DayLog : ICreated
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 事项日志
        /// </summary>
        public List<TodoLog> TodoLogs { get; set; }
    }
}
