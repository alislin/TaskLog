/* Ceated by Ya Lin. 2019/11/14 10:13:02 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskLog.DataModel
{
    public class Report: ICreated
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
        /// 创建时间
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        /// <summary>
        /// 报告明细
        /// </summary>
        public List<ReportItem> Items { get; set; } = new List<ReportItem>();
        /// <summary>
        /// 父节点
        /// </summary>
        public string ParentKey { get; set; }
        /// <summary>
        /// 存储标志
        /// </summary>
        public bool NeedSave { get; set; }

        public void ValueCheck()
        {
            Start = Start == DateTime.MinValue ? DateTime.Now.Date : Start;
            var end = string.IsNullOrWhiteSpace(ParentKey) ? Start.AddMonths(1) : Start.AddDays(7);
            End = End == DateTime.MinValue ? end : End;
            Name = string.IsNullOrWhiteSpace(Name) ? $"{Start.ToString("yyyy年M月d日")} - {End.ToString("M月d日")}" : Name;
        }
    }

    public class ReportItem : Todo
    {
        public ReportItem()
        {
        }

        public ReportItem(string name) : base(name)
        {
        }

        public ReportItem(TodoBase todo) : base(todo)
        {
        }

        public string OpenContent { get; set; }
        public string ReportContent { get; set; }
        public List<string> LogKeys { get; set; } = new List<string>();
    }
}
