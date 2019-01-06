using System;
using UIGenerator.ModelGenerator;

namespace Starter
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var model = new TestModel();
            model.Button1 += Button1Click;
            
            new GeneratedUi().Run(model);
        }

        private static void Button1Click(TestModel model)
        {
            model.SetResult("button1 clicked, int1 * double1 = " + model.Int1 * model.Double1);
        }
    }
}