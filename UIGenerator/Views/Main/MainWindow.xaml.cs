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
using UIGenerator.ModelGenerator;

namespace UIGenerator.Views.Main {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }

        public void AddTextBox(string name, string labelText, string defaultValue, Action<string> onChange, Func<string, bool> isValid)
        {
            var element = new TextBox()
            {
                Name = name,
            };

            if (!String.IsNullOrEmpty(defaultValue))
            {
                element.Text = defaultValue;
            }

            element.TextChanged += (sender, args) =>
            {
                if (isValid(element.Text))
                {
                    element.Background = Brushes.White;
                }
                else
                {
                    element.Background = Brushes.Red;
                    return;
                }
                
                onChange(element.Text);
            };
            
            AddNewRow(labelText, element);
        }

        public void AddButton(string name, string labelText, string buttonText, Action onClick)
        {
            var element = new Button()
            {
                Name = name,
                Content = buttonText,
            };

            element.Click += (sender, args) =>
            {
                SaveConfiguration();
                onClick();
            };
            
            AddNewRow(labelText, element);
        }
        
        public Action<string> AddFeedbackLabel(string name, string labelText)
        {
            var element = new Label()
            {
                Name = name,
                Content = labelText,
                Foreground = Brushes.White,
            };

            AddNewRow(labelText, element);

            return s => element.Content = s;
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

            this.RegisterName(element.Name, element);;
        }

        public void SetTextByName(string name, string text)
        {
            var textBox = this.FindName(name) as TextBox;
            if (textBox == null) return;
            textBox.Text = text ?? "";
        }

        private void SaveConfiguration()
        {
            var valuesByName = new Dictionary<string, string>();
            foreach (var child in MainGrid.Children)
            {
                var textBox = child as TextBox;
                if(textBox == null) continue;
                valuesByName[textBox.Name] = textBox.Text;
            }

            ConfigurationHelper.SaveConfiguration(valuesByName);
        }
    }
}