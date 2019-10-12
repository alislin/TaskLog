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
    public class ProjectListBase : TComponent
    {
        [Inject] protected LocalService local { get; set; }
        /// <summary>
        /// 编辑模式（0：非编辑模式 / 1：编辑项目 / 2：编辑任务）
        /// </summary>
        protected int EditMode { get; set; }
        protected string EditKey { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ComponentService.OnMessage += (o, e) =>
            {
                if (e == local.Storage.MessageTypeUpdate)
                {
                    Update();
                }
            };
        }

        public void UpdateValue(TodoBase project)
        {
            if (project == null)
            {
                return;
            }

            local.Storage.Update(project.To<Project>());
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

        public void EnterEditMode(string id)
        {
            EditMode = 1;
            EditKey = id;
            Update();
        }

        public void ExitEditMode()
        {
            EditMode = 0;
            EditKey = null;
            Update();
        }

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="item"></param>
        public void AddProject((string item,object key) data)
        {
            var item = data.item;
            var key = (string)data.key;
            if (string.IsNullOrWhiteSpace(item))
            {
                return;
            }
            Project dat = null;
            if (!string.IsNullOrWhiteSpace(key))
            {
                dat = local.Storage.Projects.FirstOrDefault(x => x.Key == key);
                dat.Name = item;
            }
            dat = dat ?? new Project(item);
            
            local.Storage.Update(dat);
            ExitEditMode();

        }
    }
}
