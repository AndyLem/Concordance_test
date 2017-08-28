using System.Configuration;
using Concordance.Core.Interfaces;

namespace Concordance.App.Providers
{
    public class ConfigProvider : IConfigProvider
    {
        public string WordDelimiters => ConfigurationManager.AppSettings[nameof(WordDelimiters)];

        public bool SortAsc => bool.Parse(ConfigurationManager.AppSettings[nameof(SortAsc)]);

        public long SentenceNumberStartsFrom => long.Parse(ConfigurationManager.AppSettings[nameof(SentenceNumberStartsFrom)]);
    }
}
