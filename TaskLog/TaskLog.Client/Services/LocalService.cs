// <copyright file="LocalService.cs" author="linya">
// Create time：       2019/9/16 16:36:23
// </copyright>

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using TaskLog.DataModel;

namespace TaskLog.Client.Services
{
    public class LocalService
    {
        public LocalService(ILocalStorageService localStorage, NavigationManager navHelper)
        {
            LocalStorage = localStorage;
            NavHelper = navHelper;
        }

        [Inject] public ILocalStorageService LocalStorage { get; set; }
        [Inject] public NavigationManager NavHelper { get; set; }
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
