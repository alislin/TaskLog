using System;
using System.Collections.Generic;

namespace TaskLog.DataModel
{
    /// <summary>
    /// 索引(Key)
    /// </summary>
    public class DataIndex : NotifyUpdate, ICreated
    {
        public string Key { get; set; }
        public DateTime Created { get; set; }
        public string Creator { get; set; }

        /// <summary>
        /// 项目集合
        /// </summary>
        public List<string> Projects { get; set; } = new List<string>();
        /// <summary>
        /// Todo集合（按照项目）
        /// </summary>
        public List<string> Todos { get; set; } = new List<string>();
        /// <summary>
        /// 日志记录
        /// </summary>
        public List<string> DayLogs { get; set; } = new List<string>();
    }

    /// <summary>
    /// 归档记录
    /// </summary>
    public class ArchiveData
    {
        public List<Project> ArchivedProject { get; set; }
        public List<Todo> Todos { get; set; }
        public List<TodoLog> TodoLogs { get; set; }
    }

    /// <summary>
    /// Key 生成助手
    /// </summary>
    public static class KeyHelper
    {
        public static Random RndKey { get; set; } = new Random(DateTime.Now.Millisecond);
        public static string Key => $"{DateTime.Now.ToFileTimeUtc()}{RndKey.Next(99999999).ToString("00000000")}";
        public static void AddUniq<T>(this IList<T> list,T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }
    }
}
