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
        /// <summary>
        /// MessageUpdate 之前操作
        /// </summary>
        public Action BeforeMessageUpdate { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ComponentService.OnMessage += (o, e) =>
            {
                if (e == local.Storage.MessageTypeUpdate)
                {
                    BeforeMessageUpdate?.Invoke();
                    Update();
                }
            };

        }
    }
}
