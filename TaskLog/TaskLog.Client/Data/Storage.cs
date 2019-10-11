﻿// <copyright file="Storage.cs" author="linya">
// Create time：       2019/9/19 15:30:14
// </copyright>

using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.DataModel;

namespace TaskLog.Client.Data
{
    public class Storage
    {
        private string dataIndexKey = "dataindex";

        public string MessageTypeUpdate = "update";

        public Storage(ILocalStorageService localStorage, Action<string> messageAction) 
        {
            this.localStorage = localStorage;
            MessageAction = messageAction;
            Init();
        }

        public ILocalStorageService localStorage { get; set; }

        /// <summary>
        /// 消息委托
        /// </summary>
        public Action<string> MessageAction { get; set; }

        /// <summary>
        /// 数据索引
        /// </summary>
        public DataIndex DataIndex { get; set; } = new DataIndex();
        /// <summary>
        /// 进行中的项目
        /// </summary>
        public List<Project> Projects { get; } = new List<Project>();
        /// <summary>
        /// 日志（读取最近一个月）
        /// </summary>
        public List<DayLog> DayLogs { get; set; } = new List<DayLog>();
        /// <summary>
        /// Todo 列表
        /// </summary>
        public List<Todo> Todos { get; set; } = new List<Todo>();
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

        /// <summary>
        /// 从浏览器加载数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> Load<T>(string key)
        {
            var result =await localStorage.GetItemAsync<T>(key);
            return result;
        }

        /// <summary>
        /// 保存数据到浏览器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Save<T>(T item) where T : ICreated
        {
            await localStorage.SetItemAsync(item.Key, item);
        }

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Remove(string itemKey)
        {
            await localStorage.RemoveItemAsync(itemKey);
        }

        /// <summary>
        /// 保存数据到浏览器
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Save(string key, object item)
        {
            await localStorage.SetItemAsync(key, item);
        }

        private async void Init()
        {
            //加载索引
            DataIndex = await Load<DataIndex>(dataIndexKey);

            if (DataIndex == null)
            {
                //首次使用
                DataIndex = new DataIndex { Key = dataIndexKey, Created = DateTime.Now, Creator = Operator.Name };
                return;
            }

            //加载数据
            foreach (var item in DataIndex.Projects)
            {
                var dat = await Load<Project>(item);
                Projects.Add(dat);
            }

            foreach (var item in DataIndex.Todos)
            {
                var dat = await Load<Todo>(item);
                Todos.Add(dat);
                if (string.IsNullOrWhiteSpace(dat.ProjcectId))
                {
                    IndexProject.Todos.Add(dat.Key);
                }
            }

            foreach (var item in DataIndex.DayLogs)
            {
                var dat = await Load<DayLog>(item);
                DayLogs.Add(dat);
                dat.TodoLogs.ForEach(x =>
                {
                    if (string.IsNullOrWhiteSpace(x.ProjcectId))
                    {
                        IndexProject.DayLogs.Add(x.Key);
                    }
                });
            }

            MessageAction?.Invoke(MessageTypeUpdate);
        }

        #region 更新方法

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="item"></param>
        public async void Update(Project item)
        {
            if (item == null)
            {
                return;
            }
            var p = Projects.FirstOrDefault(x => x.Id == item.Id);
            if (p == null)
            {
                Created(item);
                Projects.Add(item);
                DataIndex.Projects.Add(item.Key);
                await Save(DataIndex);
            }
            else
            {
                p.Update(item);
            }
            await Save(item);
            MessageAction?.Invoke(MessageTypeUpdate);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="item"></param>
        public async void Update(Todo item)
        {
            if (item == null)
            {

                return;
            }
            var p = Todos.FirstOrDefault(x => x.Id == item.Id);
            if (p == null)
            {
                Created(item);
                Todos.Add(item);
                DataIndex.Todos.Add(item.Key);
                await Save(DataIndex);
            }
            else
            {
                p.Update(item);
            }
            await Save(item);
            MessageAction?.Invoke(MessageTypeUpdate);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="item"></param>
        public async void Update(TodoLog item)
        {
            if (item == null)
            {

                return;
            }
            var daykey = item.Created.GetDayId();
            var p = DayLogs.FirstOrDefault(x => x.Date == daykey);
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
              
                DataIndex.DayLogs.Add(daylog.Key);
                await Save(daylog);
                await Save(DataIndex);
                
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
                await Save(p);
            }
            //MessageAction?.Invoke(MessageType);
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

            //移除项目
            Projects.Remove(data.Project);
            //移除索引
            DataIndex.Projects.Remove(item.Key);
            await Remove(data.Project.Key);
            await Save(DataIndex);
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
            Todos.Remove(todo);
            //移除索引
            DataIndex.Todos.Remove(item.Key);
            await Remove(todo.Key);
            await Save(DataIndex);
            MessageAction?.Invoke(MessageTypeUpdate);
        }

        public async Task Remove(TodoLog item)
        {
            if (item == null)
            {
                return;
            }

            //检查是否存在
            var data = LoadLogs(item.ProjcectId);
            var log = data.Logs.FirstOrDefault(x => x.Key == item.Key);
            var daylog = DayLogs.FirstOrDefault(x => x.TodoLogs.Count(y => y.Key == item.Key) > 0);

            //移除项目
            daylog.TodoLogs.Remove(log);

            if (daylog.TodoLogs.Count == 0)
            {
                //移除项目
                DayLogs.Remove(daylog);

                //移除索引
                DataIndex.DayLogs.Remove(daylog.Key);
                await Save(DataIndex);
            }

            await Remove(log.Key);
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
                var untypelogs = DayLogs.Where(x => x.TodoLogs.Where(y => string.IsNullOrWhiteSpace(y.ProjcectId)).Count() > 0).ToList();
                untypelogs.ForEach(x => projectData.Logs.AddRange(x.TodoLogs.Where(y => string.IsNullOrWhiteSpace(y.ProjcectId))));
                return projectData;
            }
            var daylogs = DayLogs.Where(x => x.TodoLogs.Where(y => y.ProjcectId == projectId).Count() > 0).ToList();
            daylogs.ForEach(x => projectData.Logs.AddRange(x.TodoLogs.Where(y => y.ProjcectId == projectId)));
            return projectData;
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
    }

    public class ProjectData
    {
        public Project Project { get; set; }
        public List<Todo> Todos { get; set; } = new List<Todo>();
        public List<TodoLog> Logs { get; set; } = new List<TodoLog>();
    }
}
