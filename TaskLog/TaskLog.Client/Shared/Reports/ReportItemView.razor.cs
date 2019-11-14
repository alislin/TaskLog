using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.DataModel;

namespace TaskLog.Client.Shared.Reports
{
    public class ReportItemViewBase: TLComponent
    {
        public ReportItem ReportItem { get; set; }
        public List<TodoLog> TodoLogs { get; set; }
    }
}
