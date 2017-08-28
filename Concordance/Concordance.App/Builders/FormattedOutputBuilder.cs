using System.Collections.Generic;
using System.IO;
using System.Linq;
using Concordance.App.Interfaces;
using Concordance.Core.Model;

namespace Concordance.App.Builders
{
    /// <summary>
    /// Formats the output like it is formatted in the task description
    /// </summary>
    class FormattedOutputBuilder : IOutputBuilder
    {
        public void BuildOutput(IEnumerable<WordStats> results, TextWriter outputWriter)
        {
            var resultsList = results.ToList();
            var totalCount = resultsList.Count;
            if (totalCount == 0) return;

            var median = totalCount / 2;
            if (totalCount % 2 == 1) median++;

            for (var i = 0; i < median; i++)
            {
                var secondIndex = median + i;
                RenderLine(outputWriter, resultsList[i], i);
                if (secondIndex < totalCount)
                    RenderLine(outputWriter, resultsList[secondIndex], secondIndex);
                outputWriter.WriteLine();
            }
        }

        private static void RenderLine(TextWriter outputWriter, WordStats stat, int index)
        {
            outputWriter.Write($"{BuildAlphaIndex(index),-6}{stat.Word,-28}{BuildWordStatistics(stat),-20}");
        }

        private static string BuildWordStatistics(WordStats stat)
        {
            return $"{{{stat.Occurences}:{string.Join(",", stat.SentenceNumbers.Select(x => x.ToString()))}}}";
        }

        private static string BuildAlphaIndex(int index)
        {
            var internalIndex = index % 26; // 26 is a number of letters in English alphabet
            var repeats = 1 + index / 26;
            var symbol = (char) ('a' + internalIndex);
            return new string(symbol, repeats) + ".";
        }
    }
}