// <copyright file="DayLogList.cs" author="linya">
// Create time：       2019/9/20 10:56:11
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.DataModel;
using Thunder.Blazor.Components;

namespace TaskLog.Client.Pages
{
    public class DayLogListBase : TComponent<DayLogListContext>
    {
    }

    public class DayLogListContext : TContext
    {
        public List<DayLog> DayLogs { get; set; }
    }
}
