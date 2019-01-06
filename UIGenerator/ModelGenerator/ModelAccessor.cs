using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UIGenerator.ModelGenerator.Parameters;

namespace UIGenerator.ModelGenerator
{
    public class ModelAccessor<TModel>
    {
        public readonly IEnumerable<IModelParam> Params;

        public ModelAccessor(TModel model)
        {
            var members = typeof(TModel).GetMembers()
                .Select((m,i) => new {m, i})
                .ToDictionary(pair => pair.m.Name, pair => pair.i);
            
            
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
            
            
            var allParams = stringParams
                .Concat(intParams)
                .Concat(doubleParams)
                .Concat(buttonParams);

            Params = allParams.OrderBy(p => members[p.Name]);
        }
    }
}