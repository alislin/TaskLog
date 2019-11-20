using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.DataModel;

namespace TaskLog.Client.Shared.Reports
{
    public class ReportContentBase : TLComponent
    {
        private string todoKey;

        [Parameter] public string Content { get; set; }
        [Parameter] public DateTime Start { get; set; }
        [Parameter] public DateTime End { get; set; }
        [Parameter] public EventCallback<string> ContentChanged { get; set; }
        [Parameter] public List<TodoLog> TodoLogs { get; set; } = new List<TodoLog>();
        [Parameter] public List<string> SelectedTodoLogKeys { get; set; } = new List<string>();
        [Parameter] public EventCallback<List<string>> SelectedTodoLogKeysChanged { get; set; }

        public void UpdateContent((string text, object dat) item)
        {
            ContentChanged.InvokeAsync(item.text);
            OnBindChanged.InvokeAsync(item.text);
        }

        public void CheckedUpdate(bool flag, string key)
        {
            if (flag)
            {
                if (!SelectedTodoLogKeys.Contains(key))
                {
                    SelectedTodoLogKeys.Add(key);
                }
            }
            else
            {
                if (SelectedTodoLogKeys.Contains(key))
                {
                    SelectedTodoLogKeys.Remove(key);
                }
            }
            SelectedTodoLogKeysChanged.InvokeAsync(SelectedTodoLogKeys);
            OnBindChanged.InvokeAsync(SelectedTodoLogKeys);
        }

    }
}
