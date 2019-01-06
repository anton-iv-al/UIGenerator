using System.ComponentModel;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UIGenerator.Views.Main;

namespace UIGenerator.ModelGenerator.Parameters
{
    public class DoubleParam : IModelParam
    {
        private readonly string _name;
        private readonly string _labelText;
        private readonly double _defaultValue;

        private readonly PropertyInfo _propertyInfo;
        private readonly object _model;

        public DoubleParam(PropertyInfo propertyInfo, object model)
        {
            var nameAttribute = propertyInfo.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            var labelText = nameAttribute != null ? nameAttribute.DisplayName : propertyInfo.Name;

            _name = propertyInfo.Name;
            _labelText = labelText;
            _defaultValue = (double)propertyInfo.GetValue(model);

            _propertyInfo = propertyInfo;
            _model = model;
        }

        public void AddToWindow(MainWindow window)
        {
            void OnChange(string s)
            {
                bool isParsed = double.TryParse(
                    s,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double parsed
                );
                
                if (!isParsed) return;
                _propertyInfo.SetValue(_model, parsed);
            }
            
            bool IsValid(string s)
            {
                return double.TryParse(
                    s,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double parsed
                );
            }
            
            window.AddTextBox(
                _name, 
                _labelText, 
                _defaultValue.ToString(CultureInfo.InvariantCulture), 
                OnChange,
                IsValid
            );
        }

        public string Name => _name;
    }
}