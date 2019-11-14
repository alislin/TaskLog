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
using TaskLog.Client.Shared;

namespace TaskLog.Client.Shared.Projects
{
    public class ProjectListBase : TLComponent
    {
        /// <summary>
        /// 编辑模式（0：非编辑模式 / 1：编辑项目 / 2：编辑任务）
        /// </summary>
        protected int EditMode { get; set; }
        protected string EditKey { get; set; }
        /// <summary>
        /// 选择的待办事项
        /// </summary>
        public List<string> SelectedTodos { get; set; }
        /// <summary>
        /// 勾选模式
        /// </summary>
        [Parameter] public bool CheckMode { get; set; }

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
