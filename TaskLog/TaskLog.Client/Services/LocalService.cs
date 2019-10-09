// <copyright file="LocalService.cs" author="linya">
// Create time：       2019/9/16 16:36:23
// </copyright>

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
        public LocalService(ILocalStorageService localStorage, NavigationManager navHelper, ComponentService comService,IJSRuntime jSRuntime)
        {
            LocalStorage = localStorage;
            NavHelper = navHelper;
            ComponentService = comService;
            JSRuntime = jSRuntime;
            Init();
        }

        [Inject] public ILocalStorageService LocalStorage { get; set; }
        [Inject] public NavigationManager NavHelper { get; set; }
        [Inject] public ComponentService ComponentService { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }

        public Storage Storage { get; set; }
        public SidebarService Sidebar { get; set; }

        public void Init()
        {
            Storage = new Storage(LocalStorage, ComponentService.SendMessage);
            Sidebar = new SidebarService(JSRuntime);
        }

    }

}
