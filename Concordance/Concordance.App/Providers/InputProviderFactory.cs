using System.Collections.Generic;
using System.Linq;
using Concordance.App.Entities;
using Concordance.App.Interfaces;

namespace Concordance.App.Providers
{
    public class InputProviderFactory : IInputProviderFactory
    {
        private readonly IEnumerable<IInputProvider> _providers;

        public InputProviderFactory(IEnumerable<IInputProvider> providers)
        {
            _providers = providers;
        }

        public IInputProvider GetInputProvider(InputProviderSource source)
        {
            return _providers.FirstOrDefault(x => x.SupportedSource == source);
        }
    }
}