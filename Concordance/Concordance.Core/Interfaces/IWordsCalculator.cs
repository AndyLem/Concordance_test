using System.Collections.Generic;
using Concordance.Core.Model;

namespace Concordance.Core.Interfaces
{
    public interface IWordsCalculator
    {
        IEnumerable<WordStats> Run(string input);
    }
}
