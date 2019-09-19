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

namespace TaskLog.Client.Pages
{
    public class ProjectListViewBase : TComponent<ProjectListContext>
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
            dataContext.Projects.Add(new Project("项目1"));
            dataContext.Projects.Add(new Project("项目2"));
            dataContext.Projects.Add(new Project("项目3"));
        }

        public void UpdateProject(Project project)
        {
            if (project == null)
            {
                return;
            }
            var p = dataContext.Projects.FirstOrDefault(x => x.Id == project.Id);
            if (p == null)
            {
                dataContext.Projects.Add(project);
            }
            else
            {
                p = project;
            }
            dataContext.UpdateProject = null;
            Update();
        }

        public void Add()
        {
            var pe = new ProjectEdit();
            pe.Project = new Project() { Name = "新项目名称", Created = DateTime.Now };
            pe.OnConfirm = EventCallback.Factory.Create<Project>(this, e=> {
                UpdateProject(e);
                CloseModal();
            }); 


            ShowModal(new TContext<ProjectEdit>(pe));
        }
    }

    public class ProjectListContext : TContext
    {
        public List<Project> Projects { get; set; } = new List<Project>();
        public Project UpdateProject { get; set; }
    }
}
