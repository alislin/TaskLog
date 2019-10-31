// <copyright file="Storage.cs" author="linya">
// Create time：       2019/9/19 15:30:14
// </copyright>

using Blazor.IndexedDB.Framework;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TaskLog.DataModel;

namespace TaskLog.Client.Data
{
    public class ProjectDb : IndexedDb
    {
        public ProjectDb(IJSRuntime jSRuntime, string name, int version) : base(jSRuntime, name, version)
        {
        }

        public IndexedSet<Project> Projects { get; set; }
        public IndexedSet<Todo> Todos { get; set; }
        public IndexedSet<DayLog> DayLogs { get; set; }
    }
}
