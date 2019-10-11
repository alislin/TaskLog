// <copyright file="DayLogView.cs" author="linya">
// Create time：       2019/9/18 9:12:45
// </copyright>

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Services;
using TaskLog.DataModel;
using Thunder.Blazor.Components;

namespace TaskLog.Client.Pages
{
    public class DayLogViewBase:TComponent
    {
        protected int id = 0;
        [Inject] LocalService local { get; set; }
        [Parameter]
        public int Id
        {
            get => id;
            set
            {
                id = value;
                Load(id);
            }
        }

        protected List<Todo> Todos { get; set; } = new List<Todo>();
        protected List<Project> Projects { get; set; } = new List<Project>();
        protected DayLog DayLog { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected void Load(int id)
        {
            var day = DateTime.Now.AddDays(-id).GetDayId();
            DayLog = local.Storage.DayLogs.FirstOrDefault(x => x.Date == day);
            var todoId = DayLog.TodoLogs.Select(x => x.TodoId).GroupBy(x => x).Select(x => x.Key).ToList();
            Todos.Clear();
            Todos.AddRange(local.Storage.Todos.Where(x => todoId.Contains(x.Id)));

            var projectId = DayLog.TodoLogs.Select(x => x.ProjcectId).GroupBy(x => x).Select(x => x.Key).ToList();
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
            //var title = id switch {
            //    0 => "",
            //};
            return "";
        }
    }
}
