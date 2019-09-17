﻿// <copyright file="TodoLogView.cs" author="linya">
// Create time：       2019/9/16 17:06:21
// </copyright>

using TaskLog.DataModel;
using Thunder.Blazor.Components;

namespace TaskLog.Pages
{
    public class TodoLogViewBase : TComponent<TodoLogContext>
    {
    }

    public class TodoLogContext : TContext
    {
        public TodoLog TodoLog { get; set; }
        public bool IsViewed { get; set; }
    }
}
