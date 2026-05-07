using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Myself.SmartParking.ViewModels.Pages.Dialogs
{
    public class PrintViewModel : DialogViewModelBase
    {
        public DelegateCommand<FrameworkElement> PrintCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }

        public PrintQueueCollection PrinterList { get; set; }
        public PrintQueue CurrentPrinter { get; set; }

        public BitmapImage Image { get; set; }

        private FrameworkElement _element;
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            _element = parameters.GetValue<FrameworkElement>("element");
            if (_element == null) return;

            int width = (int)_element.ActualWidth;
            int height = (int)_element.ActualHeight;

            RenderTargetBitmap renderTargetBitmap =
                new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(_element);
            BmpBitmapEncoder bmpBitmapEncoder = new BmpBitmapEncoder();
            bmpBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            MemoryStream memoryStream = new MemoryStream();
            bmpBitmapEncoder.Save(memoryStream);
            memoryStream.Position = 0;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();
            Image = bitmapImage;
        }

        public PrintViewModel()
        {
            PrintCommand = new DelegateCommand<FrameworkElement>(DoPrint);
            CloseCommand = new DelegateCommand(DoClose);

            LocalPrintServer printServer = new LocalPrintServer();
            PrinterList = printServer.GetPrintQueues();
            CurrentPrinter = PrinterList.First();
        }

        private void DoPrint(FrameworkElement element)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintTicket.PageOrientation = PageOrientation.Portrait;
            printDialog.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);
            //printDialog.PrintTicket.PageOrientation = PageOrientation.Portrait;
            printDialog.PrintTicket.PageBorderless = PageBorderless.None;
            // 设置所选择的打印机
            printDialog.PrintQueue = CurrentPrinter;
            printDialog.PrintVisual(element, "打印");

            //printDialog.ShowDialog();
            this.DoSave();
        }

        private void DoClose()
        {
            this.DoSave();
        }
    }
}
