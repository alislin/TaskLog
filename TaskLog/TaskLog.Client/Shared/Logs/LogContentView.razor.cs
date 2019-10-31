using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.DataModel;
using Thunder.Blazor.Components;


namespace TaskLog.Client.Shared.Logs
{
    public class LogContentViewBase : TLComponent
    {
        [Parameter] public TodoLog TodoLog { get; set; }
    }
}
