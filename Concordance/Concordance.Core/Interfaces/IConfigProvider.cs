using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concordance.Core.Interfaces
{
    public interface IConfigProvider
    {
        string WordDelimiters { get; }
        string SentenceDelimiters { get; }
        bool SortAsc { get; }
    }
}
