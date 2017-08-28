using Concordance.App.Entities;

namespace Concordance.App.Interfaces
{
    public interface IInputProviderFactory
    {
        IInputProvider GetInputProvider(InputProviderSource source);
    }
}