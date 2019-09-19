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
        /// 项目状态
        /// </summary>
        public ProjectStatus Status { get; set; }
        /// <summary>
        /// 完成报告
        /// </summary>
        public string Report { get; set; }
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
