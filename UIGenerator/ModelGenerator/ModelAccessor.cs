using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UIGenerator.ModelGenerator.Parameters;

namespace UIGenerator.ModelGenerator
{
    public class ModelAccessor<TModel>
    {
        private readonly TModel _model;
        
        public readonly IEnumerable<IModelParam> Params;

        public ModelAccessor(TModel model)
        {
            _model = model;
            
            var allParams = AllModelParams(model);

            allParams = WithLoadedConfiguration(allParams);
            SetParamsToModel(allParams, model);

            Params = Sorted(allParams);
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
                .Select(ei => new ButtonParam(ei, model, SaveConfiguration));
            
            
            return stringParams
                .Concat(intParams)
                .Concat(doubleParams)
                .Concat(buttonParams);
        }

        private IEnumerable<IModelParam> Sorted(IEnumerable<IModelParam> modelParams)
        {
            var members = typeof(TModel).GetMembers()
                .Select((m,i) => new {m, i})
                .ToDictionary(pair => pair.m.Name, pair => pair.i);

            return modelParams.OrderBy(p => members[p.Name]);
        }

        private IEnumerable<IModelParam> WithLoadedConfiguration(IEnumerable<IModelParam> allParams)
        {
            Dictionary<string, string> valuesByName = ConfigurationHelper.LoadConfiguration();
            if (!valuesByName.Any()) return allParams;

            var allParamsByName = allParams.ToDictionary(p => p.Name);

            foreach (var pair in valuesByName)
            {
                if (!allParamsByName.ContainsKey(pair.Key)) continue;
                allParamsByName[pair.Key].Value = pair.Value;
            }

            return allParamsByName.Values;
        }

        private void SaveConfiguration()
        {
            var allParams = AllModelParams(_model);
            
            var valuesByName = allParams.ToDictionary(p => p.Name, p => p.Value);
            
            ConfigurationHelper.SaveConfiguration(valuesByName);
        }

        private void SetParamsToModel(IEnumerable<IModelParam> modelParams, TModel model)
        {
            typeof(TModel).GetProperties()
        }
    }
}