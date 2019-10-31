// <copyright file="Storage.cs" author="linya">
// Create time：       2019/9/19 15:30:14
// </copyright>

using System.Collections.Generic;
using TaskLog.DataModel;

namespace TaskLog.Client.Data
{
    public class ProjectData
    {
        public Project Project { get; set; }
        public List<Todo> Todos { get; set; } = new List<Todo>();
        public List<TodoLog> Logs { get; set; } = new List<TodoLog>();
    }
}
