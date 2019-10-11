using System;
using System.Text.RegularExpressions;

namespace TaskLog.DataModel
{
    public class TodoBase: NotifyUpdate, IProject
    {
        /// <summary>
        /// 标识KEY
        /// </summary>
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
        /// 任务ID({任务名称}_{启动年月日})
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime End { get; set; }
        /// <summary>
        /// 计划
        /// </summary>
        public string Plan { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public int Point { get; set; }
        /// <summary>
        /// 实际得分
        /// </summary>
        public int EndPoint { get; set; }

        public TodoBase()
        {
            Key = KeyHelper.Key;
        }

        public TodoBase(string name) : this()
        {
            Created = DateTime.Now;
            if (name != null)
            {
                name = name.Replace("\r", "");
                name = name.Replace("\n", "");
                Name = name;
                Id = this.MakeId();
            }
        }

        public TodoBase(TodoBase todo)
        {
            Update(todo);
        }

        public virtual void Update(TodoBase todo)
        {
            if (todo == null)
            {
                return;
            }
            Key = todo.Key;
            Creator = todo.Creator;
            Created = todo.Created;
            Name = todo.Name;
            Id = todo.Id;
            Start = todo.Start;
            End = todo.End;
            Plan = todo.Plan;
            Target = todo.Target;
            Priority = todo.Priority;
            Point = todo.Priority;
            EndPoint = todo.EndPoint;
        }

        public T To<T>() where T:TodoBase,new()
        {
            var result = new T();
            result.Update(this);
            return result;
        }
    }

    public static class IdExtensions
    {

        public static string MakeId(this IProject value)
        {
            var n = value?.Name;
            var t = value?.Created ?? DateTime.Now;
            return $"{n}_{t.ToString("yyyyMMdd")}";
        }

        public static string GetIdName(this string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return "未关联";
            }
            var regex = new Regex("(.*)_\\d{8}");
            var m = regex.Match(id);
            if (m.Success)
            {
                return m.Groups[1].Value;
            }
            return "未关联";
        }

        public static int GetDayId(this DateTime date)
        {
            var daykey = date.Year * 100 * 100 + date.Month * 100 + date.Day;
            return daykey;
        }
    }

}
