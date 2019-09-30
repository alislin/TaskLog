// <copyright file="LocalService.cs" author="linya">
// Create time：       2019/9/16 16:36:23
// </copyright>

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskLog.Client.Data;
using TaskLog.DataModel;
using Thunder.Blazor.Services;

namespace TaskLog.Client.Services
{
    public class LocalService
    {
        public LocalService(ILocalStorageService localStorage, NavigationManager navHelper, ComponentService comService)
        {
            LocalStorage = localStorage;
            NavHelper = navHelper;
            ComponentService = comService;
            Init();
        }

        [Inject] public ILocalStorageService LocalStorage { get; set; }
        [Inject] public NavigationManager NavHelper { get; set; }
        [Inject] public ComponentService ComponentService { get; set; }

        public Storage Storage { get; set; }

        public void Init()
        {
            Storage = new Storage(LocalStorage, ComponentService.SendMessage);
        }

    }

}
