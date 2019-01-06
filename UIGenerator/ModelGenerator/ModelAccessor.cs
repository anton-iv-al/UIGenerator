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
            IEnumerable<IModelParam> stringParams = typeof(TModel).GetProperties()
                .Where(pi => pi.PropertyType == typeof(string))
                .Select(pi => new StringParam(pi, model));
            
            IEnumerable<IModelParam> buttonParams = typeof(TModel).GetEvents()
                .Select(ei => new ButtonParam(ei, model));

            Params = stringParams.Concat(buttonParams);
        }
    }
}