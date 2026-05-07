using LiveCharts;
using Microsoft.Win32;
using Myself.SmartParing.IService;
using Myself.SmartParking.Models;
using Myself.SmartParking.ViewModels.Pages;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Prism.Commands;
using Prism.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Myself.SmartParking.ViewModels
{
    public class ReportViewModel : ViewModelBase<ReportModel>
    {
        public ICommand PrintCommand { get; set; }
        public ICommand ExportCommand { get; set; }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }

        public ChartValues<double> Series1Value { get; set; } =
            new ChartValues<double>();
        public ChartValues<double> Series2Value { get; set; } =
            new ChartValues<double>();
        public ChartValues<double> Series3Value { get; set; } =
            new ChartValues<double>();
        public ChartValues<double> Series4Value { get; set; } =
            new ChartValues<double>();
        public ChartValues<double> Series5Value { get; set; } =
            new ChartValues<double>();
        public List<string> XLabels { get; set; } = new List<string>();

        public ObservableCollection<ReportModel> ReportDatas { get; set; } = new ObservableCollection<ReportModel>();

        IReportService _reportService;
        IDialogService _dialogService;
        public ReportViewModel(IRegionManager regionManager,
            IReportService reportService, IDialogService dialogService) : base(regionManager)
        {
            this.PageTitle = "停车场收费日报";

            _reportService = reportService;
            _dialogService = dialogService;

            // 默认查询15天内的订单数据
            this.EndDate = DateTime.Now;
            this.StartDate = DateTime.Now.AddDays(-15);

            PrintCommand = new DelegateCommand<FrameworkElement>(DoPrint);
            ExportCommand = new DelegateCommand(DoExport);

            this.Refresh();
        }

        private void DoPrint(FrameworkElement element)
        {
            DialogParameters dps = new DialogParameters();
            dps.Add("element", element);
            _dialogService.ShowDialog("PrintView", dps);
        }

        private void DoExport()
        {
            string tempPath = "temp.xlsx";// 
            if (!File.Exists(tempPath)) return;

            // 报表数据导出为Excel    NPOI
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "2007Excell文件|*.xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                // 先数据写到一个模板，再将模板文件流写到指定的文件

                // 1、根据模板创建导出对象


                IWorkbook workbook = null;
                using (var fs = new FileStream(tempPath, FileMode.Open, FileAccess.ReadWrite))
                {
                    // 根据temp文件的扩展名进行选择性加载
                    workbook = new XSSFWorkbook(fs);// xlsx
                    //workbook = new HSSFWorkbook(fs);// xls
                }
                // 2、加载Sheet，并且填充数据
                var sheet = workbook.GetSheet("zhaoxi");
                for (int i = 0; i < ReportDatas.Count; i++)
                {
                    if (sheet.GetRow(i + 2) == null) sheet.CreateRow(i + 2);

                    for (int c = 0; c < 8; c++)
                    {
                        if (sheet.GetRow(i + 2).GetCell(c) == null)
                            sheet.GetRow(i + 2).CreateCell(c);
                    }

                    sheet.GetRow(i + 2).GetCell(0).SetCellValue(ReportDatas[i].Index);
                    sheet.GetRow(i + 2).GetCell(1).SetCellValue(ReportDatas[i].Date);
                    sheet.GetRow(i + 2).GetCell(2).SetCellValue(ReportDatas[i].TotalCount);
                    sheet.GetRow(i + 2).GetCell(3).SetCellValue(ReportDatas[i].ReceAmount);
                    sheet.GetRow(i + 2).GetCell(4).SetCellValue(ReportDatas[i].CashPayment);
                    sheet.GetRow(i + 2).GetCell(5).SetCellValue(ReportDatas[i].ElecPayment);
                    sheet.GetRow(i + 2).GetCell(6).SetCellValue(ReportDatas[i].Subtotal);
                    sheet.GetRow(i + 2).GetCell(7).SetCellValue(ReportDatas[i].DeduAmount);
                }

                // 3、保存到本地文件
                string savePath = saveFileDialog.FileName;
                using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    workbook.Write(fs);
                }

            }
        }

        public override void Refresh()
        {
            ReportDatas.Clear();
            var ris = _reportService.GetParkingReport(StartDate, EndDate);
            ris.ForEach(ri =>
            {
                int index = ReportDatas.Count + 1;
                ReportDatas.Add(new ReportModel
                {
                    Index = index,
                    Date = ri.DateTime,
                    TotalCount = ri.OrderCount,
                    ReceAmount = ri.Payable,

                    CashPayment = ri.PaymentCash,
                    ElecPayment = ri.PaymentElec,
                    Subtotal = ri.PaymentCash + ri.PaymentElec,
                    // 应付-小计
                    DeduAmount = ri.Payable - ri.PaymentCash - ri.PaymentElec
                });


                XLabels.Add(ri.DateTime);
                Series1Value.Add(ri.Payable);
                Series2Value.Add(ri.PaymentCash);
                Series3Value.Add(ri.PaymentElec);
                Series4Value.Add(ri.PaymentCash + ri.PaymentElec);
                Series5Value.Add(ri.Payable - ri.PaymentCash - ri.PaymentElec);
            });



        }
    }
}
