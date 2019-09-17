// <copyright file="LocalService.cs" author="linya">
// Create time：       2019/9/16 16:36:23
// </copyright>

using Blazored.LocalStorage;
using System.Collections.Generic;
using TaskLog.DataModel;

namespace TaskLog.Services
{
    public class LocalService
    {
        public LocalService(ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
        }

        public ILocalStorageService LocalStorage { get; set; }
        /// <summary>
        /// 进行中的项目
        /// </summary>
        public List<Project> Projects { get; } = new List<Project>();
        /// <summary>
        /// 日志（读取最近一个月）
        /// </summary>
        public List<DayLog> DayLogs { get; set; } = new List<DayLog>();
        public List<Todo> Todos { get; set; } = new List<Todo>();
        public List<TodoLog> TodoLogs { get; set; } = new List<TodoLog>();
        /// <summary>
        /// 已经关闭的项目，默认不加载
        /// </summary>
        public List<Project> ClosedProjects { get; } = new List<Project>();

    }
}
