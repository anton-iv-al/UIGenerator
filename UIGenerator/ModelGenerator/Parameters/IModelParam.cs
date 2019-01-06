using System.ComponentModel;
using System.Reflection;
using UIGenerator.Views.Main;

namespace UIGenerator.ModelGenerator.Parameters
{
    public interface  IModelParam
    {
        string Name { get; }
        string Value { get; set; }
        void AddToWindow(MainWindow window);
    }
}