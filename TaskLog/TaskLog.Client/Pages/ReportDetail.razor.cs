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
            // 表头
            var headerRowCount = 4;
            // 数据行
            var dataRow = 5;
            // 表脚
            var footerRow = 6;

            var headerStream = new MemoryStream(TaskLog.Client.Properties.Resources.dataTemplate);

            IWorkbook workbookHeader = new XSSFWorkbook(headerStream);
            ISheet sheetHeader = workbookHeader.GetSheetAt(0);

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
            var workbook = mapper.Workbook;

            for (short i = 0; i < workbookHeader.NumberOfFonts; i++)
            {
                var font = workbook.CreateFont();
                var src = workbookHeader.GetFontAt(i);
                if (src != null)
                {
                    font = src;
                }
            }

            //复制样式
            for (short i = 0; i < workbookHeader.NumCellStyles; i++)
            {
                var style = workbook.CreateCellStyle();
                var src = workbookHeader.GetCellStyleAt(i);
                if (src != null)
                {
                    style = src;
                    //style.CloneStyleFrom(src);
                }
                
            }

            

            var dataSheet = workbook.GetSheet(tlreport.Name);
            //数据下移
            dataSheet.ShiftRows(1, tlreport.Items.Count + 1, headerRowCount - 1, true, false);
            

            //合并单元格
            for (int i = 0; i < sheetHeader.NumMergedRegions; i++)
            {
                dataSheet.AddMergedRegion(sheetHeader.GetMergedRegion(i));
            }

            //插入模板头部
            for (int i = 0; i < headerRowCount; i++)
            {
                var rowInsert = dataSheet.CreateRow(i);
                var rowHeader = sheetHeader.GetRow(i);
                if (rowHeader.RowStyle != null)
                {
                    rowInsert.RowStyle.CloneStyleFrom(rowHeader.RowStyle);
                }
                for (int col = 0; col < rowHeader.LastCellNum; col++)
                {
                    var cellHeader = rowHeader.GetCell(col);
                    var cellInsert = rowInsert.CreateCell(col,cellHeader.CellType);
                    if (cellHeader.CellStyle != null)
                    {
                        cellInsert.CellStyle.CloneStyleFrom(cellHeader.CellStyle);
                    }
                    cellInsert.SetCellValue(cellHeader.StringCellValue);
                    dataSheet.SetColumnWidth(col, sheetHeader.GetColumnWidth(col));
                    dataSheet.SetDefaultColumnStyle(col, sheetHeader.GetColumnStyle(col));
                }
            }
            //设置样式
            //var rowData = sheetHeader.GetRow(dataRow);
            //for (int i = headerRowCount; i < dataSheet.LastRowNum; i++)
            //{
            //    var row = dataSheet.GetRow(i);
            //    row.RowStyle = rowData.RowStyle;
            //    for (int col = 0; col < row.LastCellNum; col++)
            //    {
            //        var cell = row.GetCell(col);
            //        if (cell==null)
            //        {
            //            continue;
            //        }
            //        var cellHeader = rowData.GetCell(col);
            //        if (cellHeader.CellStyle != null)
            //        {
            //            cell.CellStyle.CloneStyleFrom(cellHeader.CellStyle);
            //        }
            //    }
            //}
            //添加合计
            //var rowFooter = sheetHeader.GetRow(footerRow);
            //var footer = dataSheet.CreateRow(dataSheet.LastRowNum);
            //footer.RowStyle = rowFooter.RowStyle;
            //for (int col = 0; col < rowFooter.LastCellNum; col++)
            //{
            //    var cell = footer.CreateCell(col);
            //    var cellFooter = rowFooter.GetCell(col);
            //    if (cellFooter.CellStyle != null)
            //    {
            //        cell.CellStyle.CloneStyleFrom(cellFooter.CellStyle);
            //    }
            //    cell.SetCellValue(cellFooter.StringCellValue);
            //}

            workbook.Write(stream);
            //mapper.Save(stream);

            await local.SaveAs($"{report.Name}.xlsx", stream.ToArray());
        }
    }


}
