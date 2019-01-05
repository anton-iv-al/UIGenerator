using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIGenerator.Views.Main {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private MainPresenter _presenter;

        public MainWindow() {
            InitializeComponent();
            _presenter = new MainPresenter(this);
            
            AddTextBox("name1", "name1Label", null);
            AddButton("name2", "name2Label", "name2Button", () => { (this.FindName("name1") as TextBox).Text = "changed!!!"; });
        }

        public void AddTextBox(string name, string labelText, string defaultValue)
        {
            var element = new TextBox()
            {
                Name = name,
            };

            if (!String.IsNullOrEmpty(defaultValue))
            {
                element.Text = defaultValue;
            }
            
            AddNewRow(labelText, element);
        }

        public void AddButton(string name, string labelText, string buttonText, Action onClick)
        {
            var element = new Button()
            {
                Name = name,
                Content = buttonText,
            };

            element.Click += (sender, args) => onClick();
            
            AddNewRow(labelText, element);
        }

        private void AddNewRow(string labelText, FrameworkElement element)
        {
            var row = new RowDefinition() {Height = GridLength.Auto};
            MainGrid.RowDefinitions.Add(row);
            int rowIndex = MainGrid.RowDefinitions.Count - 1;
            
            var label = new Label()
            {
                Content = labelText, 
                Foreground = Brushes.White,
            };
            
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, rowIndex);
            MainGrid.Children.Add(label);
            
            Grid.SetColumn(element, 1);
            Grid.SetRow(element, rowIndex);
            MainGrid.Children.Add(element);
            
            this.RegisterName(element.Name, element);
        }

//        public string DirectoryText {
//            get => DirectoryControl.Text;
//            set => DirectoryControl.Text = value;
//        }
//
//        public int SizeText {
//            get => int.Parse(SizeControl.Text);
//            set => SizeControl.Text = value.ToString();
//        }
//
//        private void CutButtonControl_Click(object sender, RoutedEventArgs e) {
//            _presenter.OnCutButtonClick();
//        }
    }
}