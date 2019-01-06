using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UIGenerator.Views.Main;

namespace UIGenerator.ModelGenerator.Parameters
{
    public class IntParam : IModelParam
    {
        private readonly string _name;
        private readonly string _labelText;
        private int _value;

        private readonly PropertyInfo _propertyInfo;
        private readonly object _model;

        public IntParam(PropertyInfo propertyInfo, object model)
        {
            var nameAttribute = propertyInfo.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            var labelText = nameAttribute != null ? nameAttribute.DisplayName : propertyInfo.Name;

            _name = propertyInfo.Name;
            _labelText = labelText;
            _value = (int)propertyInfo.GetValue(model);

            _propertyInfo = propertyInfo;
            _model = model;
        }

        public void AddToWindow(MainWindow window)
        {
            void OnChange(string s)
            {
                bool isParsed = TryParse(s, out int parsed);
                
                if (!isParsed) return;
                _propertyInfo.SetValue(_model, parsed);
            }
            
            bool IsValid(string s)
            {
                return TryParse(s, out int parsed);
            }
            
            window.AddTextBox(
                _name, 
                _labelText, 
                _value.ToString(), 
                OnChange,
                IsValid
            );
        }

        private bool TryParse(string s, out int parsed)
        {
            return int.TryParse(
                s,
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out parsed
            );
        }

        public string Name => _name;
    }
}