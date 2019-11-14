using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Shared;
using TaskLog.DataModel;

namespace TaskLog.Client.Pages
{
    public class ReportDetailBase:TLComponent
    {
        protected Report Report { get; set; }
        [Parameter] public string Key { get; set; }
    }
}
