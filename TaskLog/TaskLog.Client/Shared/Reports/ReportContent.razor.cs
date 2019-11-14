using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.DataModel;

namespace TaskLog.Client.Shared.Reports
{
    public class ReportContentBase: TLComponent
    {
        [Parameter] public string Content { get; set; }
        [Parameter] public List<TodoLog> TodoLogs { get; set; } = new List<TodoLog>();
        [Parameter] public List<string> SelectedTodoLogKeys { get; set; } = new List<string>();
    }
}
