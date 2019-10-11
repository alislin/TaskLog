﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Services;
using TaskLog.DataModel;
using Thunder.Blazor.Components;

namespace TaskLog.Client.Pages
{
    public class ProjectItemBase:TComponent
    {
        /// <summary>
        /// 编辑模式（0：非编辑模式 / 1：编辑项目 / 2：编辑任务）
        /// </summary>
        protected int EditMode { get; set; }
        protected string EditKey { get; set; }

        [Parameter]
        public Project Project
        {
            get { return m_Projcet; }
            set
            {
                m_Projcet = value;
                Load(m_Projcet);
            }
        }
        [Inject] protected LocalService local { get; set; }
        protected List<Todo> Todos { get; } = new List<Todo>();
        protected Project m_Projcet;

        protected void Goto(object obj)
        {
            local.NavHelper.NavigateTo($"/project/{Project?.Id}");
        }

        protected void Load(Project project)
        {
            var data = local.Storage.Load(project.Id);
            Todos.Clear();
            Todos.AddRange(data.Todos);
        }

        protected async void Delete(Project project)
        {
            await local.Storage.Remove(project);
        }

        protected async void Delete(Todo todo)
        {
            await local.Storage.Remove(todo);
        }

        public void EnterEditMode(int mode, string id)
        {
            EditMode = mode;
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
        /// 创建Todo
        /// </summary>
        /// <param name="item"></param>
        public void AddTodo(string item, string key)
        {
            if (string.IsNullOrWhiteSpace(item) || string.IsNullOrWhiteSpace(key))
            {
                return;
            }
            var project = local.Storage.Projects.FirstOrDefault(x => x.Key == key);
            var dat = new Todo(item);
            dat.ProjcectId = project.Id;

            local.Storage.Update(dat);
            ExitEditMode();
        }


        /// <summary>
        /// 更新Todo
        /// </summary>
        /// <param name="item"></param>
        public void UpdateTodo(string item, string key)
        {
            if (string.IsNullOrWhiteSpace(item) || string.IsNullOrWhiteSpace(key))
            {
                return;
            }
            var dat = local.Storage.Todos.FirstOrDefault(x => x.Key == key);
            dat.Name = item;

            local.Storage.Update(dat);
            ExitEditMode();
        }


        public void UpdateProject(string item, string key)
        {
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