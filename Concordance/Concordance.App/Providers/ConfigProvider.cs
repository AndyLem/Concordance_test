using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concordance.Core.Interfaces;

namespace Concordance.App.Providers
{
    public class ConfigProvider : IConfigProvider
    {
        public string WordDelimiters => ConfigurationManager.AppSettings[nameof(WordDelimiters)];

        public string[] SentenceDelimiters
            =>
                ConfigurationManager.AppSettings[nameof(SentenceDelimiters)].Split(new[] {','},
                    StringSplitOptions.RemoveEmptyEntries).ToArray();

        public bool SortAsc => bool.Parse(ConfigurationManager.AppSettings[nameof(SortAsc)]);

        public string SentenceDelimiterChars => ConfigurationManager.AppSettings[nameof(SentenceDelimiterChars)];
    }
}
