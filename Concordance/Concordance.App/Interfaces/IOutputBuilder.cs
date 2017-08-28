using System.Collections.Generic;
using System.IO;
using Concordance.Core.Model;

namespace Concordance.App.Interfaces
{
    public interface IOutputBuilder
    {
        void BuildOutput(IEnumerable<WordStats> results, TextWriter outputWriter);
    }
}