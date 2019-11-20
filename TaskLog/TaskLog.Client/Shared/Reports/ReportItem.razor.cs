using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Services;
using TaskLog.Client.Shared;
using TaskLog.DataModel;
using Thunder.Blazor.Components;

namespace TaskLog.Client.Shared.Projects
{
    public class ReportItemBase: TLComponent
    {
        [Parameter]
        public Report Report
        {
            get { return m_Report; }
            set
            {
                m_Report = value;
                Load(m_Report);
            }
        }
        protected List<Report> ChildReports { get; set; } = new List<Report>();
        protected Report m_Report;

        protected void Goto(object obj)
        {
            local.NavHelper.NavigateTo($"/report/view/{Report?.Key}");
        }

        protected void Load(Project project)
        {
            ChildReports = local.Storage.Reports.Where(x => x.ParentKey==project.Key).ToList();
        }


    }
}
