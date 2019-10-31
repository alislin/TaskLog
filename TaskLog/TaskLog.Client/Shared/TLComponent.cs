using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Services;
using Thunder.Blazor.Components;

namespace TaskLog.Client.Shared
{
    public class TLComponent: TComponent
    {
        [Inject] public LocalService local { get; set; }
    }
}
