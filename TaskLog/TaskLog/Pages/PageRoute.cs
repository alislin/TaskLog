// <copyright file="PageRoute.cs" author="linya">
// Create time：       2019/9/19 9:40:20
// </copyright>

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskLog.Pages
{
    [Route("/p")]
    public class PrototypeIndex : TaskLog.Client.Prototype.Index { }

    [Route("/")]
    public class Index : TaskLog.Client.Pages.Index { }

    [Route("/project/{Id}")]
    public class Project : TaskLog.Client.Pages.ProjectDetail { }
}
