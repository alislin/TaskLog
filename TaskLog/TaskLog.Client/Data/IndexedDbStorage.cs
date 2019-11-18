// <copyright file="Storage.cs" author="linya">
// Create time：       2019/9/19 15:30:14
// </copyright>

using Blazor.IndexedDB.Framework;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Libs;
using TaskLog.DataModel;
using Thunder.Blazor.Services;
using static TaskLog.Client.Libs.DebugTime;

namespace TaskLog.Client.Data
{
    public class IndexedDbStorage : IStorage
    {
        private string dataIndexKey = "dataindex";
        public string MessageTypeUpdate { get; set; } = "update";

        public IndexedDbStorage(IIndexedDbFactory dbFactory, ComponentService componentService)
        {
            DbFactory = dbFactory;
            ComponentService = componentService;
            MessageAction = ComponentService.SendMessage;

            Init();
        }

        [Inject] public IIndexedDbFactory DbFactory { get; set; }
        [Inject] public ComponentService ComponentService { get; set; }

        /// <summary>
        /// 消息委托
        /// </summary>
        public Action<string> MessageAction { get; set; }

        /// <summary>
        /// 进行中的项目
        /// </summary>
        public List<Project> Projects { get; set; } = new List<Project>();
        /// <summary>
        /// 日志（读取最近一个月）
        /// </summary>
        public List<DayLog> DayLogs { get; set; } = new List<DayLog>();
        /// <summary>
        /// Todo 列表
        /// </summary>
        public List<Todo> Todos { get; set; } = new List<Todo>();
        public List<TodoLog> TodoLogs => GetTodoLogs();
        /// <summary>
        /// 报告列表
        /// </summary>
        public List<Report> Reports { get; set; } = new List<Report>();
        /// <summary>
        /// 归档数据
        /// </summary>
        public ArchiveData ArchiveData { get; set; } = new ArchiveData();
        /// <summary>
        /// 操作人
        /// </summary>
        public OperatorUser Operator { get; set; } = new OperatorUser { Name = "Loger" };
        /// <summary>
        /// 未分配项目日志条目
        /// </summary>
        public DataIndex IndexProject { get; set; } = new DataIndex();

        private async void Init()
        {
            //加载数据
            await LoadProject();
            await LoadTodo();
            await LoadDayLog();

            MessageAction?.Invoke(MessageTypeUpdate);
        }

        #region 数据加载
        private async Task LoadProject()
        {
            using var db = await NewDb;
            Projects = db.Projects.Where(x => x.Status != ProjectStatus.Archived).ToList();
        }

        private async Task LoadTodo()
        {
            using var db = await NewDb;
            var plist = Projects.Select(x => x.Key).ToList();
            Todos = db.Todos.Where(x => plist.Contains(x.ProjcectId)).ToList();
            IndexProject.Todos.AddRange(Todos.Where(x => string.IsNullOrWhiteSpace(x.ProjcectId)).Select(x => x.Key));
        }

        private async Task LoadDayLog()
        {
            using var db = await NewDb;
            DayLogs = db.DayLogs.ToList();
            DayLogs.Select(x => x.TodoLogs).ToList().ForEach(x =>
            {
                IndexProject.DayLogs.AddRange(x.Where(y => string.IsNullOrWhiteSpace(y.ProjcectId)).Select(y => y.Key));
            });
        }

        private async Task LoadReport()
        {
            using var db = await NewDb;
            Reports = db.Reports.OrderByDescending(x=>x.Created).ToList();
        }
        #endregion

        #region 更新方法

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="item"></param>
        public async Task Update(Project item)
        {
            if (item == null)
            {
                return;
            }
            dt.Check();
            using var db = await NewDb;
            dt.Check("数据连接");
            var p = db.Projects.FirstOrDefault(x => x.Id == item.Id);
            dt.Check("读取数据");
            if (p == null)
            {
                Created(item);
                db.Projects.Add(item);
            }
            else
            {
                p.Update(item);
            }
            await db.SaveChanges();
            dt.Check("保存");
            await LoadProject();
            dt.Check("重新加载");
            MessageAction?.Invoke(MessageTypeUpdate);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="item"></param>
        public async Task Update(Todo item)
        {
            if (item == null)
            {
                return;
            }
            using var db =await NewDb;
            var p = db.Todos.FirstOrDefault(x => x.Id == item.Id);
            if (p == null)
            {
                Created(item);
                db.Todos.Add(item);
            }
            else
            {
                p.Update(item);
            }
            await db.SaveChanges();
            await LoadTodo();
            MessageAction?.Invoke(MessageTypeUpdate);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="item"></param>
        public async Task Update(TodoLog item)
        {
            if (item == null)
            {
                return;
            }

            using var db = await NewDb;

            var daykey = item.Created.GetDayId();
            var p = db.DayLogs.FirstOrDefault(x => x.Date == daykey);
            if (p == null)
            {
                //创建新的日期
                var daylog = new DayLog
                {
                    Date = daykey,
                    TodoLogs = new List<TodoLog>()
                };
                Created(daylog);
                Created(item);
                daylog.TodoLogs.Add(item);
                db.DayLogs.Add(daylog);


            }
            else
            {
                //检查记录是否存在
                var tl = p.TodoLogs.FirstOrDefault(x => x.Key == item.Key && x.ProjcectId == item.ProjcectId && x.TodoId == item.TodoId);
                if (tl == null)
                {
                    Created(item);
                    //创建新记录
                    p.TodoLogs.Add(item);
                }
                else
                {
                    //更新
                    tl.Update(item);
                }
            }

            await db.SaveChanges();
            await LoadDayLog();

            MessageAction?.Invoke(MessageTypeUpdate);
        }

        /// <summary>
        /// 更新报告
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Update(Report item)
        {
            if (item == null)
            {
                return;
            }
            using var db = await NewDb;
            var report = db.Reports.FirstOrDefault(x => x.Key == item.Key);
            if (Reports == null)
            {
                Created(item);
                db.Reports.Add(item);
            }
            else
            {
                report.End = item.End;
                report.Items = item.Items;
                report.ParentKey = item.ParentKey;
                report.Start = item.Start;
            }
            await db.SaveChanges();
            await LoadReport();

            MessageAction?.Invoke(MessageTypeUpdate);
        }
        #endregion

        #region 删除方法
        public async Task Remove(Project item)
        {
            if (item == null)
            {
                return;
            }

            //检查是否存在
            var data = LoadLogs(item.Id);
            if (data.Todos.Count > 0)
            {
                //项目不为空，跳过处理
                Console.WriteLine("项目不为空，不能删除。");
                return;
            }

            using var db = await NewDb;

            //移除项目
            var p = db.Projects.FirstOrDefault(x => x.Key == item.Key);
            if (p != null)
            {
                db.Projects.Remove(p);
                await db.SaveChanges();
                await LoadProject();
            }
            MessageAction?.Invoke(MessageTypeUpdate);
        }

        public async Task Remove(Todo item)
        {
            if (item == null)
            {
                return;
            }

            //检查是否存在
            var data = LoadLogs(item.ProjcectId);
            var todo = data.Todos.FirstOrDefault(x => x.Id == item.Id);
            if (data.Logs.Count(x => x.TodoId == item.Id) > 0)
            {
                //项目不为空，跳过处理
                Console.WriteLine("项目不为空，不能删除。");
                return;
            }

            //移除项目
            using var db = await NewDb;
            var p = db.Todos.FirstOrDefault(x => x.Key == item.Key);
            if (p != null)
            {
                db.Todos.Remove(p);
                await db.SaveChanges();
                await LoadTodo();
            }
            MessageAction?.Invoke(MessageTypeUpdate);
        }

        public async Task Remove(TodoLog item)
        {
            if (item == null)
            {
                return;
            }

            //检查是否存在
            //var data = LoadLogs(item.ProjcectId);
            //var log = data.Logs.FirstOrDefault(x => x.Key == item.Key);
            //var daylog = DayLogs.FirstOrDefault(x => x.TodoLogs.Count(y => y.Key == item.Key) > 0);

            using var db = await NewDb;
            var daylog = db.DayLogs.FirstOrDefault(x => x.TodoLogs.Count(y => y.Key == item.Key) > 0);
            if (daylog != null)
            {
                var log = daylog.TodoLogs.FirstOrDefault(x => x.Key == item.Key);
                daylog.TodoLogs.Remove(log);
                if (daylog.TodoLogs.Count == 0)
                {
                    db.DayLogs.Remove(daylog);
                }

            }

            await db.SaveChanges();
            await LoadDayLog();

            MessageAction?.Invoke(MessageTypeUpdate);
        }

        public async Task Remove(Report item)
        {
            if (item == null)
            {
                return;
            }

            using var db = await NewDb;
            var report = db.Reports.FirstOrDefault(x => x.Key == item.Key);
            if (report == null)
            {
                return;
            }
            Reports.RemoveAll(x => x.Key == item.Key);
            db.Reports.Remove(report);
            await db.SaveChanges();

            MessageAction?.Invoke(MessageTypeUpdate);
        }
        #endregion

        #region 加载方法
        /// <summary>
        /// 加载项目数据(不加载日志条目)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ProjectData Load(string projectId)
        {
            var projectData = new ProjectData();
            projectData.Project = Projects.FirstOrDefault(x => x.Id == projectId);
            projectData.Todos = Todos.Where(x => x.ProjcectId == projectId).ToList();
            return projectData;
        }

        /// <summary>
        /// 加载项目数据(加载日志条目)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ProjectData LoadLogs(string projectId)
        {
            var projectData = Load(projectId);
            if (string.IsNullOrWhiteSpace(projectId))
            {
                //加载未分配数据
                projectData.Project = new Project { Name = "待分配日志", Created = DateTime.Now, Id = "0" };
                projectData.Todos.Add(new Todo { Name = "未分类", Created = DateTime.Now });
                projectData.Logs.AddRange(TodoLogs.Where(x => string.IsNullOrWhiteSpace(x.ProjcectId)));

                return projectData;
            }
            projectData.Logs.AddRange(TodoLogs.Where(x => x.ProjcectId == projectId));
            return projectData;
        }

        private List<TodoLog> GetTodoLogs()
        {
            var list = new List<TodoLog>();
            DayLogs.ForEach(x => list.AddRange(x.TodoLogs));
            return list;
        }
        #endregion

        /// <summary>
        /// 默认创建信息，生成Key，创建人，创建时间
        /// </summary>
        /// <param name="item"></param>
        private void Created(ICreated item)
        {
            item.Creator = Operator.Name;
            item.Created = DateTime.Now;
            item.Key = KeyHelper.Key;
        }

        private Task<ProjectDb> NewDb => DbFactory.Create<ProjectDb>();
    }
}
