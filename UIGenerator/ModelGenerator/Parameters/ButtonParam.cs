﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UIGenerator.Views.Main;

namespace UIGenerator.ModelGenerator.Parameters
{
    public class ButtonParam : IModelParam
    {
        private readonly string _name;
        private readonly string _labelText;

        private readonly EventInfo _eventInfo;
        private readonly object _model;

        public ButtonParam(EventInfo eventInfo, object model)
        {
            var nameAttribute = eventInfo.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            var labelText = nameAttribute != null ? nameAttribute.DisplayName : eventInfo.Name;

            _name = eventInfo.Name;
            _labelText = labelText;

            _eventInfo = eventInfo;
            _model = model;
        }

        public void AddToWindow(MainWindow window)
        {
            void OnClick()
            {
                TriggerEvent(_model, _eventInfo.Name, new[] {Clone(_model)});
            }
            
            window.AddButton(
                _name, 
                _labelText,
                _labelText, 
                OnClick
            );
        }
        
        private void TriggerEvent(object sender, string eventName, object[] eventParams)
        {
            MulticastDelegate eventDelegate =
                (MulticastDelegate)sender.GetType().GetField(eventName,
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic).GetValue(sender);

            if (eventDelegate == null) return;

            Delegate[] delegates = eventDelegate.GetInvocationList();

            foreach (Delegate dlg in delegates)
            {
                dlg.Method.Invoke(dlg.Target, eventParams);
            } 
        }

        private object Clone(object original)
        {
            var memberwiseClone = original.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            return memberwiseClone.Invoke(original, null);
        }

        public string Name => _name;
    }
}