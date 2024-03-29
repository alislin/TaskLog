﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Services;
using TaskLog.DataModel;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;
using Thunder.Standard.Lib.Model;

namespace TaskLog.Client.Shared.Logs
{
    public class TodoLogEditBase : TLComponent
    {

        [Parameter] public TodoLog TodoLog { get; set; } = new TodoLog();
        [Parameter] public bool EditMode { get; set; }
        protected SelectOption SelectedTodo { get; set; } = new SelectOption();
        protected SelectOptionContext TodoOption { get; set; }
        //[Parameter] public EventCallback<TodoLog> OnConfirm { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            InitSelect();
        }

        protected void UpdateSelected(SelectOption select)
        {
            SelectedTodo = select;
            Update();
        }

        public void UpdateValue(object objvalue)
        {
            //Console.WriteLine($"更新编辑值。value:{TodoLog.ToJson()}");
            var todo = (Todo)SelectedTodo.Object;
            TodoLog.ProjcectId = todo?.ProjcectId;
            TodoLog.TodoId = todo?.Id;
            local.Storage.Update(TodoLog);
            CloseModal();
            //OnConfirm.InvokeAsync(TodoLog);
        }

        public void Reset(object obj)
        {
            TodoLog = new TodoLog();
        }

        protected void OnKeyUp(KeyboardEventArgs key)
        {
            //Console.WriteLine(key.Key);
            if (key.CtrlKey && key.Key == "Enter")
            {

                UpdateValue(null);
            }
        }

        protected void InitSelect()
        {
            var q = from p in local.Storage.Projects
                    join t in local.Storage.Todos on p.Id equals t.ProjcectId
                    select new SelectOption
                    {
                        Value = t.Id,
                        Text = t.Name,
                        Group = p.Name,
                        Object = t,
                        Selected = TodoLog.TodoId == t.Id
                    };
            var selectOption = q.ToList();
            selectOption.Insert(0, new SelectOption
            {
                Value = null,
                Text = "未分类",
            });
            TodoOption = new SelectOptionContext
            {
                Items = selectOption
            };
        }

        protected async void DeleteLog(object objvalue)
        {
            await local.Storage.Remove(TodoLog);
            CloseModal();
        }
    }
}
