using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Services;
using TaskLog.DataModel;
using Thunder.Blazor.Components;
using Thunder.Standard.Lib.Model;
using Thunder.Standard.Lib.Extension;

namespace TaskLog.Client.Shared.Projects
{
    public class ProjectEditBase: TLComponent
    {
        [Parameter] public Project Project { get; set; } = new Project();
        [Parameter] public bool EditMode { get; set; }
        protected SelectOption SelectedStatus { get; set; } = new SelectOption();
        protected SelectOptionContext StatusOption { get; set; } = new SelectOptionContext();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            InitSelect();
        }

        protected void SaveChange()
        {
            Log("保存");
        }

        protected void InitSelect()
        {
            ProjectStatus ps = ProjectStatus.Prepare;
            StatusOption.Items = ps.ToSelect();
        }
    }
}
