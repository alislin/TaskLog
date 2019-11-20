using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Shared;
using TaskLog.DataModel;

namespace TaskLog.Client.Pages
{
    public class ReportOpenBase:TLComponent
    {
        private Report report = new Report();
        protected List<string> TodoKeys { get; set; } = new List<string>();

        /// <summary>
        /// 编辑模式
        /// </summary>
        [Parameter] public EditMode EditMode { get; set; } = EditMode.Create;
        [Parameter] public string Key { get; set; }
        /// <summary>
        /// 报告
        /// </summary>
        [Parameter]
        public Report Report
        {
            get => report;
            set
            {
                report = value;
                EditMode = EditMode.Edit;
                LoadSelectedTodos(report);
            }
        }

        private void LoadSelectedTodos(Report report)
        {
            TodoKeys = report.Items.Select(x => x.Key).ToList();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!string.IsNullOrWhiteSpace(Key))
            {
                Report = local.Storage.Reports.FirstOrDefault(x => x.Key == Key) ?? new Report();
            }
        }
    }
}
