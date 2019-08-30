using System;

namespace TaskLog.DataModel
{
    /// <summary>
    /// 待办日志
    /// </summary>
    public class TodoLog : IProjectBase, ICreated
    {
        /// <summary>
        /// 项目Id ({项目名称}_{启动年月日})
        /// </summary>
        public string ProjcectId { get; set; }
        /// <summary>
        /// 任务ID({任务名称}_{启动年月日})
        /// </summary>
        public string TodoId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 完成报告
        /// </summary>
        public string Report { get; set; }
    }
}
