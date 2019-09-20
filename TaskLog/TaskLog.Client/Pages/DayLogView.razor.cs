// <copyright file="DayLogView.cs" author="linya">
// Create time：       2019/9/18 9:12:45
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.DataModel;
using Thunder.Blazor.Components;

namespace TaskLog.Client.Pages
{
    public class DayLogViewBase:TComponent<DayLogContext>
    {

    }

    public class DayLogContext : TContext
    {
        public List<TodoLog> TodoLogs { get; set; }
    }
}
