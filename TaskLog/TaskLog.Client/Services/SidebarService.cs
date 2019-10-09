// <copyright file="LocalService.cs" author="linya">
// Create time：       2019/9/16 16:36:23
// </copyright>

using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace TaskLog.Client.Services
{
    public class SidebarService
    {
        public SidebarService(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
        }

        public IJSRuntime JSRuntime { get; set; }

        public async Task Show()
        {
            await JSRuntime.InvokeAsync<object>("ThunderBlazor.tasklogApp.showSidebar", null);
        }

        public async Task Hide()
        {
            await JSRuntime.InvokeAsync<object>("ThunderBlazor.tasklogApp.hideSidebar", null);
        }
    }

}
