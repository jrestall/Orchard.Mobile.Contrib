namespace Orchard.Mobile.Contrib.Services
{
    public interface IRuleManager : IDependency
    {
        bool Matches(string expression);
    }
}