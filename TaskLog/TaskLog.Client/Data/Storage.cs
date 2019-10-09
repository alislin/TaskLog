// <copyright file="Storage.cs" author="linya">
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

        public Action<string> MessageAction { get; set; }

        public DataIndex DataIndex { get; set; } = new DataIndex();
        /// <summary>
        /// 进行中的项目
        /// </summary>
        public List<Project> Projects { get; } = new List<Project>();
        /// <summary>
        /// 日志（读取最近一个月）
        /// </summary>
        public List<DayLog> DayLogs { get; set; } = new List<DayLog>();
        public List<Todo> Todos { get; set; } = new List<Todo>();
        public ArchiveData ArchiveData { get; set; } = new ArchiveData();
        public OperatorUser Operator { get; set; } = new OperatorUser { Name = "Loger" };

        public async Task<T> Load<T>(string key)
        {
            var result =await localStorage.GetItemAsync<T>(key);
            return result;
        }

        public async Task Save<T>(T item) where T : ICreated
        {
            await localStorage.SetItemAsync(item.Key, item);
        }

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
            }

            foreach (var item in DataIndex.DayLogs)
            {
                var dat = await Load<DayLog>(item);
                DayLogs.Add(dat);
            }

            MessageAction?.Invoke(MessageTypeUpdate);
        }

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
            //MessageAction?.Invoke(MessageType);
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
            //MessageAction?.Invoke(MessageType);
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
            var day = item.Created;
            var daykey = day.Year * 100 * 100 + day.Month * 100 + day.Day;
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

        private void Created(ICreated item)
        {
            item.Creator = Operator.Name;
            item.Created = DateTime.Now;
            item.Key = KeyHelper.Key;
        }


    }
}
