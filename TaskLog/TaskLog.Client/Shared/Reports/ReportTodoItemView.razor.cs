using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.DataModel;
using Thunder.Blazor.Components;
using Thunder.Standard.Lib.Extension;
using Thunder.Standard.Lib.Model;

namespace TaskLog.Client.Shared.Reports
{
    public class ReportTodoItemViewBase: TLComponent
    {
        [Parameter] public DataModel.ReportItem ReportItem { get; set; } = new DataModel.ReportItem();
        [Parameter] public EventCallback<DataModel.ReportItem> ReportItemChanged { get; set; }
        [Parameter] public EditMode EditMode { get; set; }

        protected List<TodoLog> TodoLogs => LoadLog();
        public SelectOptionContext StatusOption => new SelectOptionContext() { Items = ProjectStatus.Prepare.ToSelect() };
        protected SelectOption SelectedStatus { get; set; } = new SelectOption();

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected void BindValueChanged(object obj)
        {
            ReportItemChanged.InvokeAsync(ReportItem);
            OnBindChanged.InvokeAsync(ReportItem);
        }

        private List<TodoLog> LoadLog()
        {
            return local.Storage.TodoLogs.Where(x => x.TodoId == ReportItem.Id).ToList();
        }
    }
}
