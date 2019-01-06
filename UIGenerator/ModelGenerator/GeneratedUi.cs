using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UIGenerator.Views.Main;

namespace UIGenerator.ModelGenerator
{
    public class GeneratedUi
    {
        
        public void Run<TModel>(TModel model) where TModel : new()
        {
            App app = new App();
            MainWindow window = new MainWindow();

            AddElementsToWindow(model, window);
            
            app.Run(window);
        }

        private void AddElementsToWindow<TModel>(TModel model, MainWindow window)
        {
            var modelAccessor = new ModelAccessor<TModel>(model);

            foreach (var param in modelAccessor.ParamsForWindow())
            {
                param.AddToWindow(window);
            }

            foreach (KeyValuePair<string,string> pair in ConfigurationHelper.LoadConfiguration())
            {
                window.SetTextByName(pair.Key, pair.Value);
            }
        }
    }
}