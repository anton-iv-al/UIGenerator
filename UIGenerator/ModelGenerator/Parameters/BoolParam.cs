using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UIGenerator.Views.Main;

namespace UIGenerator.ModelGenerator.Parameters
{
    public class BoolParam : IModelParam
    {
        private readonly string _name;
        private readonly string _labelText;
        private bool _value;

        private readonly PropertyInfo _propertyInfo;
        private readonly object _model;

        public BoolParam(PropertyInfo propertyInfo, object model)
        {
            var nameAttribute = propertyInfo.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            var labelText = nameAttribute != null ? nameAttribute.DisplayName : propertyInfo.Name;

            _name = propertyInfo.Name;
            _labelText = labelText;
            _value = (bool)propertyInfo.GetValue(model);

            _propertyInfo = propertyInfo;
            _model = model;
        }

        public void AddToWindow(MainWindow window)
        {
            window.AddCheckBox(
                _name, 
                _labelText, 
                _value, 
                s => _propertyInfo.SetValue(_model, s)
            );
        }

        public string Name => _name;
    }
}