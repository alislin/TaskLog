// <copyright file="DayLogList.cs" author="linya">
// Create time：       2019/9/20 10:56:11
// </copyright>

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Services;
using TaskLog.DataModel;
using Thunder.Blazor.Components;

namespace TaskLog.Client.Pages
{
    public class DayLogListBase : TComponent<DayLogListContext>
    {
        [Inject] public LocalService local { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ComponentService.OnMessage += (o, e) =>
            {
                if (e == local.Storage.MessageType)
                {
                    Update();
                }
            };

            DataContext.DayLogs = local.Storage.DayLogs;
        }
    }

    public class DayLogListContext : TContext
    {
        public List<DayLog> DayLogs { get; set; } = new List<DayLog>();
    }
}
