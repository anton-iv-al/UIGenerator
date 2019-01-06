using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UIGenerator.Views.Main;

namespace UIGenerator.ModelGenerator.Parameters
{
    public class FeedbackStringParam : IModelParam
    {
        private readonly string _name;
        private readonly string _labelText;
        private string _value;

        private readonly PropertyInfo _propertyInfo;
        private readonly object _model;

        public FeedbackStringParam(PropertyInfo propertyInfo, object model)
        {
            var nameAttribute = propertyInfo.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            var labelText = nameAttribute != null ? nameAttribute.DisplayName : propertyInfo.Name;

            _name = propertyInfo.Name;
            _labelText = labelText;
            _value = propertyInfo.GetValue(model) as string;

            _propertyInfo = propertyInfo;
            _model = model;
        }

        public void AddToWindow(MainWindow window)
        {
            Action<string> setString = window.AddFeedbackLabel(
                _name, 
                _labelText
            );

            Action<string> dispatcherSetString = s => window.Dispatcher.Invoke(() => setString(s));

            var delegateInstance = Delegate.CreateDelegate(typeof(Action<string>), dispatcherSetString.Target, dispatcherSetString.Method);
            _propertyInfo.SetValue(_model, delegateInstance);
        }

        public string Name => _name;
    }
}