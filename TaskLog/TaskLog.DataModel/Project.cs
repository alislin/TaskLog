using System;
using System.Collections.Generic;

namespace TaskLog.DataModel
{
    /// <summary>
    /// 项目
    /// </summary>
    public class Project : IProject
    {
        /// <summary>
        /// 项目Id ({项目名称}_{启动年月日})
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime End { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public ProjectStatus Status { get; set; }
        /// <summary>
        /// 计划
        /// </summary>
        public string Plan { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 完成报告
        /// </summary>
        public string Report { get; set; }
        /// <summary>
        /// 处理日志
        /// </summary>
        public List<DateTime> LogList { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public int Point { get; set; }
        /// <summary>
        /// 实际得分
        /// </summary>
        public int EndPoint { get; set; }
    }
}
