using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UIGenerator.Views.Main;

namespace UIGenerator.ModelGenerator.Parameters
{
    public class StringParam : IModelParam
    {
        private readonly string _name;
        private readonly string _labelText;
        private readonly string _defaultValue;

        private readonly PropertyInfo _propertyInfo;
        private readonly object _model;

        public StringParam(PropertyInfo propertyInfo, object model)
        {
            var nameAttribute = propertyInfo.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            var labelText = nameAttribute != null ? nameAttribute.DisplayName : propertyInfo.Name;

            _name = propertyInfo.Name;
            _labelText = labelText;
            _defaultValue = propertyInfo.GetValue(model) as string;

            _propertyInfo = propertyInfo;
            _model = model;
        }

        public void AddToWindow(MainWindow window)
        {
            window.AddTextBox(
                _name, 
                _labelText, 
                _defaultValue, 
                s => _propertyInfo.SetValue(_model, s)
            );
        }
    }
}