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
    }

    public class ReportItem : Todo
    {
        public string OpenContent { get; set; }
        public string ReportContent { get; set; }
        public List<string> LogKeys { get; set; }
    }
}
