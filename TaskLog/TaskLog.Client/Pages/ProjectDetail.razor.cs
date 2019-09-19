// <copyright file="ProjectDetail.cs" author="linya">
// Create time：       2019/9/19 10:22:10
// </copyright>

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Services;
using TaskLog.DataModel;
using Thunder.Blazor.Components;

namespace TaskLog.Client.Pages
{
    public class ProjectDetailBase : TComponent<ProjectDetailContext>
    {
        [Inject] LocalService local { get; set; }
        [Parameter] public string Id { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            ComponentService.OnMessage += (o, e) =>
            {
                if (e == local.Storage.MessageType)
                {
                    Update();
                }
            };

            Reload();
        }

        public void UpdateValue(TodoBase project)
        {
            if (project == null)
            {
                return;
            }
            var dat = project.To<Todo>();
            dat.ProjcectId = Id;
            local.Storage.Update(dat);
            Reload();
        }

        public void Reload()
        {
            dataContext.Todos = local.Storage.Todos.Where(x => x.ProjcectId == Id).ToList();
        }
    }

    public class ProjectDetailContext : TContext
    {
        public List<Todo> Todos { get; set; } = new List<Todo>();
    }
}
