using Myself.PMS.Client.Common;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.OrderModule.ViewModels.Dialogs
{
    public class ImagePreviewViewModel : DialogViewModelBase
    {
        private List<string> _imgList;
        private string _currentImage;

        public string CurrentImage
        {
            get { return _currentImage; }
            set { SetProperty<string>(ref _currentImage, value); }
        }

        public DelegateCommand<string> FlipCommand { get; set; }

        public ImagePreviewViewModel()
        {
            FlipCommand = new DelegateCommand<string>(DoFlip);
        }

        private int _index = 0;
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            _imgList = parameters.GetValue<List<string>>("imgList");
            CurrentImage = parameters.GetValue<string>("img");

            if (_imgList != null)
            {
                _index = _imgList.IndexOf(CurrentImage);
            }
        }

        private void DoFlip(string flag)
        {
            if (flag == "L")
            {
                _index--;
                if (_index < 0)
                    _index = _imgList.Count - 1;
            }
            else if (flag == "R")
            {
                _index++;
                if (_index > _imgList.Count - 1)
                    _index = 0;
            }
            CurrentImage = _imgList[_index];
        }
    }
}
