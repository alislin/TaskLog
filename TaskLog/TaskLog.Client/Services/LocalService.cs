// <copyright file="LocalService.cs" author="linya">
// Create time：       2019/9/16 16:36:23
// </copyright>

using Blazor.IndexedDB.Framework;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using TaskLog.Client.Data;
using TaskLog.DataModel;
using Thunder.Blazor.Services;

namespace TaskLog.Client.Services
{
    public class LocalService
    {
        public LocalService(ILocalStorageService localStorage, NavigationManager navHelper, ComponentService componentService, IJSRuntime jSRuntime, IStorage storage)
        {
            LocalStorage = localStorage;
            NavHelper = navHelper;
            ComponentService = componentService;
            JSRuntime = jSRuntime;
            Storage = storage;
            Init();
        }

        [Inject] public ILocalStorageService LocalStorage { get; set; }
        [Inject] public NavigationManager NavHelper { get; set; }
        [Inject] public ComponentService ComponentService { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public IStorage Storage { get; set; }

        public SidebarService Sidebar { get; set; }

        public void Init()
        {
            Sidebar = new SidebarService(JSRuntime);
        }

        public void HighLight()
        {
            //JSRuntime.InvokeAsync<object>("ThunderBlazor.tasklogApp.highlight", null);
            System.Console.WriteLine("highlight.js loaded.");
        }

    }

}
