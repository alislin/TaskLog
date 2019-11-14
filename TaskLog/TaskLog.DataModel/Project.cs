using System;
using System.Collections.Generic;

namespace TaskLog.DataModel
{
    /// <summary>
    /// 项目
    /// </summary>
    public class Project : TodoBase
    {
        /// <summary>
        /// 处理日志
        /// </summary>
        public List<DateTime> LogList { get; set; } = new List<DateTime>();

        public Project(string name):base(name)
        {
        }

        public Project():base()
        {
        }
    }
}
