// <copyright file="ProjectListView.cs" author="linya">
// Create time：       2019/9/17 16:23:01
// </copyright>

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskLog.DataModel;
using Thunder.Blazor.Components;
using Microsoft.AspNetCore.Components.Web;
using TaskLog.Client.Services;

namespace TaskLog.Client.Pages
{
    public class ProjectListViewBase : TComponent<ProjectListContext>
    {
        [Inject] LocalService local { get; set; }
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

            dataContext.Projects = local.Storage.Projects;
        }

        public void UpdateValue(TodoBase project)
        {
            if (project == null)
            {
                return;
            }

            local.Storage.Update(project.To<Project>());
            //dataContext.Projects = local.Storage.Projects;

            //Update();
        }

        public void Add(object obj)
        {
            var pe = new TodoBaseEdit();
            //pe.Project = new Project() { Name = "新项目名称", Created = DateTime.Now };
            pe.OnConfirm = EventCallback.Factory.Create<TodoBase>(this, e=> {
                UpdateValue(e);
                CloseModal();
            }); 


            ShowModal(new TContext<TodoBaseEdit>(pe));
        }
    }

    public class ProjectListContext : TContext
    {
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
