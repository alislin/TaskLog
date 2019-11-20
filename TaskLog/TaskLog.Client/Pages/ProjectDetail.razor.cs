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
        private string id;

        [Inject] LocalService local { get; set; }
        [Parameter] public string Id { get => id;
            set 
            {
                id = value;
                id = id != "0" ? id : null;
                Reload();
            }
        }

        protected override void OnInitialized()
        {
            
            base.OnInitialized();
            ComponentService.OnMessage += (o, e) =>
            {
                if (e == local.Storage.MessageTypeUpdate)
                {
                    Reload();
                    Update();
                }
            };

            Reload();
        }

        protected void UpdateText((string text,object key) dat)
        {
            var item = dat.text;
            var key = (string)dat.key;
            if (key!= dataContext.Project.Key)
            {
                return;
            }
            dataContext.Project.Name = item;
            local.Storage.Update(dataContext.Project);
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
            var data = local.Storage.LoadLogs(Id);
            dataContext.Project = data.Project;
            dataContext.Todos = data.Todos;
            dataContext.TodoLogs = data.Logs;
        }
    }

    public class ProjectDetailContext : TContext
    {
        public Project Project { get; set; }
        public List<Todo> Todos { get; set; } = new List<Todo>();
        public List<TodoLog> TodoLogs { get; set; } = new List<TodoLog>();
        public bool HasTodos => Todos.Count > 0;
    }
}
