using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TaskLog.DataModel
{
    public class TodoBase: NotifyUpdate, IProject
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
        /// 任务ID({任务名称}_{启动年月日})
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        [Required(ErrorMessage ="请输入项目名称"),MinLength(5,ErrorMessage = "最少需要5个字"),MaxLength(50,ErrorMessage ="超过最大长度")]
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
        /// 目标
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 分值比重
        /// </summary>
        public int Point { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public ProjectStatus Status { get; set; }

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
            Target = todo.Target;
            Priority = todo.Priority;
            Point = todo.Priority;
            Status = todo.Status;
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
