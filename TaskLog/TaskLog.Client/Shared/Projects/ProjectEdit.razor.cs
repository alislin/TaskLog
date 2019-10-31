using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Services;
using TaskLog.DataModel;
using Thunder.Blazor.Components;

namespace TaskLog.Client.Shared.Projects
{
    public class ProjectEditBase: TLComponent
    {
        [Parameter] public Project Project { get; set; } = new Project();
        [Parameter] public bool EditMode { get; set; }

        protected void SaveChange()
        {
            Log("保存");
        }
    }
}
