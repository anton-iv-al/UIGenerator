using System;
using System.ComponentModel;

namespace Starter
{
    public class TestModel
    {
        [DisplayName("label of Text1")]
        public string Text1 { get; set; } = "default of Text1";
        
        public string Text2 { get;} = "default of Text2";
        
        [DisplayName("label of Button1")]
        public event Action<TestModel> Button1;
    }
}