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
    public class ReportItemViewBase: TLComponent
    {
        public ReportItem ReportItem { get; set; }
        public List<TodoLog> TodoLogs { get; set; }
        public SelectOptionContext StatusOption => new SelectOptionContext() { Items = ProjectStatus.Prepare.ToSelect() };
        protected SelectOption SelectedStatus { get; set; } = new SelectOption();
    }
}
