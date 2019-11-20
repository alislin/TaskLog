// <copyright file="DayLogView.cs" author="linya">
// Create time：       2019/9/18 9:12:45
// </copyright>

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Services;
using TaskLog.Client.Shared;
using TaskLog.DataModel;
using Thunder.Blazor.Components;

namespace TaskLog.Client.Pages
{
    public class DayLogViewBase: TLComponent
    {
        protected int id = 0;
        [Parameter]
        public int Id
        {
            get => id;
            set
            {
                id = value;
                //int.TryParse(value, out id);
                Load(id);
            }
        }

        protected List<Todo> Todos { get; set; } = new List<Todo>();
        protected List<Project> Projects { get; set; } = new List<Project>();
        protected DayLog DayLog { get; set; }
        protected bool HasContent => DayLog != null;
        protected DateTime? DayPrev { get; set; }
        protected DateTime? DayNext { get; set; }

        protected override void OnInitialized()
        {
            BeforeMessageUpdate = () => Load(id);
            base.OnInitialized();

            Load(id);
        }

        protected void Load(int id)
        {
            Caption = GetTitle(id);
            var day = DateTime.Now.AddDays(-id)
                                  .GetDayId();
            DayLog = local.Storage.DayLogs.FirstOrDefault(x => x.Date == day);
            DayNext = local.Storage.DayLogs.OrderBy(x => x.Date).FirstOrDefault(x => x.Date > day)?.Created;
            DayPrev = local.Storage.DayLogs.OrderBy(x => x.Date).LastOrDefault(x => x.Date < day)?.Created;
            if (DayLog == null)
            {
                return;
            }
            var todoId = DayLog.TodoLogs.Select(x => x.TodoId)?
                                        .GroupBy(x => x)
                                        .Select(x => x.Key)
                                        .ToList();
            Todos.Clear();
            Todos.AddRange(local.Storage.Todos.Where(x => todoId.Contains(x.Id)));

            var projectId = DayLog.TodoLogs.Select(x => x.ProjcectId)
                                           .GroupBy(x => x)
                                           .Select(x => x.Key)
                                           .ToList();
            Projects.Clear();
            Projects.AddRange(local.Storage.Projects.Where(x => projectId.Contains(x.Id)));
        }

        /// <summary>
        /// 获取项目名称标签
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        protected string GetProjectTag(string projectId)
        {
            var tag = "未分类日志";
            tag = Projects.FirstOrDefault(x => x.Id == projectId).Name;
            return tag;
        }

        protected string GetTitle(int id)
        {
            var title = id switch
            {
                0 => "今天",
                1 => "昨天",
                _ => DateTime.Now.AddDays(-id).ToString("yyyy 年 M 月 d 日")
            };
            return title;
        }
    }
}
