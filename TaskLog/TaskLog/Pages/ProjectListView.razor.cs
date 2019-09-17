// <copyright file="ProjectListView.cs" author="linya">
// Create time：       2019/9/17 16:23:01
// </copyright>

using System.Collections.Generic;
using TaskLog.DataModel;
using Thunder.Blazor.Components;

namespace TaskLog.Pages
{
    public class ProjectListViewBase : TComponent<ProjectListContext>
    {
    }

    public class ProjectListContext : TContext
    {
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
