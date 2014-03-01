namespace Orchard.Mobile.Contrib.Services
{
    public class RuleContext
    {
        public string FunctionName { get; set; }
        public object[] Arguments { get; set; }
        public object Result { get; set; }
    }
}