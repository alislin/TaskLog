using System;

namespace TaskLog.DataModel
{
    /// <summary>
    /// 代办事项
    /// </summary>
    public class Todo : TodoBase, IProjectBase
    {
        public Todo() : base()
        {
        }

        public Todo(string name):base(name)
        {
        }

        /// <summary>
        /// 项目Id ({项目名称}_{启动年月日})
        /// </summary>
        public string ProjcectId { get; set; }
        /// <summary>
        /// 引入总结报告
        /// </summary>
        public bool Reported { get; set; }
    }

}
