using Npoi.Mapper.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.DataModel;

namespace TaskLog.Client.Models
{
    public class TLReport
    {
        /// <summary>
        /// 报告类型（0：月报，1：周报）
        /// </summary>
        public int ReportType => (End - Start).TotalDays > 7 ? 0 : 1;
        public string Name => ReportType == 0 ? $"{End.Month}月计划" : $"{Start.ToString("MMdd")}-{End.ToString("MMdd")}";
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<TLReportItem> Items { get; set; } = new List<TLReportItem>();
    }

    public class TLReportItem
    {
        [Column("序号")]
        public int Index { get; set; }

        [Display(Name = "数量与质量成果的描述")]
        [Column(2)]
        public string TodoName { get; set; }

        [Display(Name = "计划完成时间")]
        [Column(3,CustomFormat = "M月d日")]
        public DateTime PlanEnd { get; set; }

        [Display(Name = "工作方案")]
        [Column(4)]
        public string OpenContent { get; set; }

        [Display(Name = "权重")]
        [Column(5,CustomFormat ="0%")]
        public decimal Point { get; set; }

        [Display(Name = "完成情况描述")]
        [Column(6)]
        public string Report { get; set; }
    }

    public static class TLReportex
    {
        public static TLReport ToTLReport(this Report report,bool finished=false)
        {
            var tl = new TLReport
            {
                Start = report.Start,
                End = report.End,
                Items = report.Items.Select(x => new TLReportItem
                {
                    OpenContent = x.OpenContent,
                    PlanEnd = x.End,
                    TodoName = x.Name,
                    Report = x.ReportContent,
                    Point = x.Point
                }).ToList()
            };
            if (!finished)
            {
                tl.Items.ForEach(x => x.Report = null);
            }
            return tl;
        }
    }
}
