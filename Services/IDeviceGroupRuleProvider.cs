namespace Orchard.Mobile.Contrib.Services
{
    public interface IDeviceGroupRuleProvider : IDependency
    {
        void Process(RuleContext ruleContext);
    }
}