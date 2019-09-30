using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Services;
using TaskLog.DataModel;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;

namespace TaskLog.Client.Pages
{
    public class TodoLogEditBase:TComponent
    {
        [Inject] public LocalService local { get; set; }
        [Parameter] public TodoLog TodoLog { get; set; } = new TodoLog();
        //[Parameter] public EventCallback<TodoLog> OnConfirm { get; set; }

        public void UpdateValue(object obj)
        {
            Console.WriteLine($"更新编辑值。value:{TodoLog.ToJson()}");
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
            Console.WriteLine(key.Key);
            if (key.CtrlKey && key.Key == "Enter")
            {

                UpdateValue(null);
            }
        }
    }
}
