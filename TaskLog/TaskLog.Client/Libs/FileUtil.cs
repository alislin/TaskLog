using Microsoft.JSInterop;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TaskLog.Client.Libs
{
    public static class FileUtil
    {
        public static async Task SaveAs(IJSRuntime jSRuntime,string filename, byte[] data)
        {
            await jSRuntime.InvokeAsync<object>("ThunderBlazor.tasklogApp.saveAsFile",
                                          filename,
                                          Convert.ToBase64String(data)).ConfigureAwait(false);
        }
    }
}
