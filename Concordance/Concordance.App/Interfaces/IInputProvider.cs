using Concordance.App.Entities;

namespace Concordance.App.Interfaces
{
    public interface IInputProvider
    {
        string GetInput();
        InputProviderSource SupportedSource { get; }
    }
}