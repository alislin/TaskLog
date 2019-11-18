// <copyright file="Storage.cs" author="linya">
// Create time：       2019/9/19 15:30:14
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using TaskLog.DataModel;

namespace TaskLog.Client.Data
{
    public interface IStorage
    {
        ArchiveData ArchiveData { get; set; }
        List<DayLog> DayLogs { get; set; }
        DataIndex IndexProject { get; set; }
        Action<string> MessageAction { get; set; }
        OperatorUser Operator { get; set; }
        List<Project> Projects { get; }
        List<TodoLog> TodoLogs { get; }
        List<Todo> Todos { get; set; }
        string MessageTypeUpdate { get; set; }
        List<Report> Reports { get; set; }

        ProjectData Load(string projectId);
        ProjectData LoadLogs(string projectId);
        Task Remove(Project item);
        Task Remove(Todo item);
        Task Remove(TodoLog item);
        Task Remove(Report item);
        Task Update(Report item);
        Task Update(Project item);
        Task Update(Todo item);
        Task Update(TodoLog item);
    }
}