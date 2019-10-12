using System;
using System.Collections.Generic;

namespace TaskLog.DataModel
{
    /// <summary>
    /// 日志
    /// </summary>
    public class DayLog :NotifyUpdate, ICreated
    {
        /// <summary>
        /// 标识KEY
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 日期（20190901）
        /// </summary>
        public int Date { get; set; }
        /// <summary>
        /// 事项日志
        /// </summary>
        public List<TodoLog> TodoLogs { get; set; } = new List<TodoLog>();
    }
}
