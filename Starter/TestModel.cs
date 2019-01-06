using System;
using System.ComponentModel;

namespace Starter
{
    public class TestModel
    {
        [DisplayName("label of Text1")]
        public string Text1 { get; set; } = "default of Text1";
        
        public string Text2 { get; set; } = "default of Text2";
        
        public double Double1 { get; set; } = 4;
        
        public int Int1 { get; set; } = 5;
        
        [DisplayName("label of Button1")]
        public event Action<TestModel> Button1;
    }
}