// <copyright file="LogUpdateView.cs" author="linya">
// Create time：       2019/9/20 11:51:42
// </copyright>

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.DataModel;
using Thunder.Blazor.Components;

namespace TaskLog.Client.Pages
{
    public class LogUpdateViewBase:TComponent
    {
        [Parameter] public TodoLog TodoLog { get; set; } = new TodoLog();

        protected void OnKeyUp(KeyboardEventArgs key)
        {
            Console.WriteLine(key.Key);
            if (key.CtrlKey && key.Key == "Enter")
            {
                UpdateValue();
            }
        }

        protected void UpdateValue()
        {
            throw new NotImplementedException();
        }
    }
}
