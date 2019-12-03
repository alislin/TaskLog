using Microsoft.AspNetCore.Components;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskLog.Client.Shared;
using TaskLog.DataModel;
using Thunder.Standard.Lib.Extension;
using Npoi.Mapper;
using TaskLog.Client.Models;

namespace TaskLog.Client.Pages
{
    public class ReportDetailBase : TLComponent
    {
        private Report report = new Report();
        private List<string> todoKeys = new List<string>();
        private List<ReportItem> reportItemsList = new List<ReportItem>();
        private string key;

        protected List<ReportItem> ReportItems => reportItemsList.Where(x => todoKeys.Contains(x.Key)).ToList();
        protected bool SaveFlag => Report?.NeedSave ?? true;

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
                LoadReport();
            }
        }
        [Parameter] public EventCallback<Report> ReportChanged { get; set; }
        [Parameter] public string Key
        {
            get => key; 
            set
            {
                key = value;
                report = local.Storage.Reports.FirstOrDefault(x => x.Key == Key) ?? new Report();
                LoadReport();
            }
        }
        /// <summary>
        /// 编辑模式
        /// </summary>
        [Parameter] public EditMode EditMode { get; set; }

        [Parameter]
        public List<string> TodoKeys
        {
            get => todoKeys;
            set
            {
                todoKeys = value;
                LoadTodos(todoKeys);
            }
        }
        [Parameter] public EventCallback<List<string>> TodoKeysChanged { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            report = local.Storage.Reports.FirstOrDefault(x => x.Key == Key) ?? new Report();
            LoadReport();
            TodoKeysChanged.InvokeAsync(todoKeys);
        }

        protected void LoadReport()
        {
            key = report.Key;
            reportItemsList = report.Items;
            todoKeys = reportItemsList.Select(x => x.Key).ToList();
        }

        protected void LoadTodos(List<string> keys)
        {
            var list = reportItemsList.Select(x => x.Key);
            var add = keys.Except(list);
            var todos = local.Storage.Todos.Where(x => add.Contains(x.Key)).Select(x => new ReportItem(x)).ToList();
            reportItemsList.AddRange(todos);
        }

        protected void UpdateReport()
        {
            report.Items = ReportItems;
            report.NeedSave = false;
            local.Storage.Update(Report);
            LoadReport();
        }

        protected void BindValueChanged(object obj)
        {
            report.NeedSave = true;
            OnBindChanged.InvokeAsync(Report);
        }

        /// <summary>
        /// 导出
        /// </summary>
        protected async void Export()
        {
            using MemoryStream stream = new MemoryStream();
            var tlreport = report.ToTLReport();
            var mapper = new Mapper();
            //mapper.Map<TLReportItem>(1, x => x.Index)
            //      .Map<TLReportItem>(3, x => x.TodoName)
            //      .Map<TLReportItem>(4, x => x.PlanEnd)
            //      .Map<TLReportItem>(5, x => x.OpenContent)
            //      .Map<TLReportItem>(6, x => x.Point)
            //      .Map<TLReportItem>(7, x => x.Report);
            //mapper.Format<TLReportItem>("yyyy/MM/dd", x => x.PlanEnd);
            mapper.Put(tlreport.Items, tlreport.Name);
            mapper.Save(stream);

            await local.SaveAs($"{report.Name}.xlsx", stream.ToArray());
        }
    }


}
