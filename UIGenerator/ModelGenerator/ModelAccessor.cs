using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UIGenerator.ModelGenerator.Parameters;

namespace UIGenerator.ModelGenerator
{
    public class ModelAccessor<TModel>
    {
        private readonly TModel _model;
        
        public ModelAccessor(TModel model)
        {
            _model = model;
        }

        public IEnumerable<IModelParam> ParamsForWindow()
        {
            var allParams = AllModelParams(_model);
            return SortedParams(allParams);
        }

        private IEnumerable<IModelParam> AllModelParams(TModel model)
        {
            IEnumerable<IModelParam> stringParams = typeof(TModel).GetProperties()
                .Where(pi => pi.PropertyType == typeof(string))
                .Select(pi => new StringParam(pi, model));
            
            IEnumerable<IModelParam> intParams = typeof(TModel).GetProperties()
                .Where(pi => pi.PropertyType == typeof(int))
                .Select(pi => new IntParam(pi, model));
            
            IEnumerable<IModelParam> doubleParams = typeof(TModel).GetProperties()
                .Where(pi => pi.PropertyType == typeof(double))
                .Select(pi => new DoubleParam(pi, model));
            
            IEnumerable<IModelParam> buttonParams = typeof(TModel).GetEvents()
                .Select(ei => new ButtonParam(ei, model));
            
            IEnumerable<IModelParam> feedBackStringParams = typeof(TModel).GetProperties()
                .Where(pi => pi.PropertyType == typeof(Action<string>))
                .Select(pi => new FeedbackStringParam(pi, model));
            
            
            return stringParams
                .Concat(intParams)
                .Concat(doubleParams)
                .Concat(buttonParams)
                .Concat(feedBackStringParams);
        }

        private IEnumerable<IModelParam> SortedParams(IEnumerable<IModelParam> modelParams)
        {
            var members = typeof(TModel).GetMembers()
                .Select((m,i) => new {m, i})
                .ToDictionary(pair => pair.m.Name, pair => pair.i);

            return modelParams.OrderBy(p => members[p.Name]);
        }
    }
}