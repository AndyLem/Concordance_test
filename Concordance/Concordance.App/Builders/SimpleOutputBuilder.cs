using System.Collections.Generic;
using System.IO;
using System.Linq;
using Concordance.App.Interfaces;
using Concordance.Core.Model;

namespace Concordance.App.Builders
{
    /// <summary>
    /// Produces a simple output with a single stat in a row
    /// </summary>
    public class SimpleOutputBuilder : IOutputBuilder
    {
        public void BuildOutput(IEnumerable<WordStats> results, TextWriter outputWriter)
        {
            foreach (var result in results)
            {
                outputWriter.WriteLine(
                    $"{result.Word}\t{result.Occurences}:{string.Join(",", result.SentenceNumbers.Select(x => x.ToString()))}");
            }
        }
    }
}