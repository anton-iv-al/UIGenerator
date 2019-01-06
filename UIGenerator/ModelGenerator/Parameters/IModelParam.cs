using System.ComponentModel;
using System.Reflection;
using UIGenerator.Views.Main;

namespace UIGenerator.ModelGenerator.Parameters
{
    public interface  IModelParam{
        void AddToWindow(MainWindow window);
    }
}