using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskLog.DataModel
{
    /// <summary>
    /// 待办日志
    /// </summary>
    public class TodoLog : NotifyUpdate, IProjectBase, ICreated
    {
        /// <summary>
        /// 标识KEY
        /// </summary>
        [Key]
        public string Key { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
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
        public DateTime Created { get; set; } = DateTime.Now;
        /// <summary>
        /// 完成报告
        /// </summary>
        public string Report { get; set; }

        public virtual void Update(TodoLog todo)
        {
            if (todo == null)
            {
                return;
            }
            Report = todo.Report;
        }
    }
}
