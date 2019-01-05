using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;

namespace UIGenerator.Views.Main {
    public class MainPresenter : Presenter<MainWindow> {
        private string _originDirKey = "origin_dir";
        private string _sizeKey = "size";
        private string _borderKey = "border";

        public MainPresenter(MainWindow view) : base(view) {
            
        }

        public void OnCutButtonClick() {
            
        }

    }
}