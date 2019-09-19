// <copyright file="ProjectDetail.cs" author="linya">
// Create time：       2019/9/19 10:22:10
// </copyright>

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.DataModel;
using Thunder.Blazor.Components;

namespace TaskLog.Client.Pages
{
    public class ProjectDetailBase : TComponent<ProjectDetailContext>
    {
        [Parameter] public string Id { get; set; }
    }

    public class ProjectDetailContext : TContext
    {
        public List<Todo> Todos { get; set; }
    }
}
