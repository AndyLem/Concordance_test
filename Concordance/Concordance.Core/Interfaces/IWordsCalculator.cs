using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concordance.Core.Entities;

namespace Concordance.Core.Interfaces
{
    public interface IWordsCalculator
    {
        IEnumerable<WordStats> Run(string input);
    }
}
