using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concordance.Core.Entities
{
    public class CalculatorResults
    {
        public IEnumerable<WordStats> SortedWords;
    }

    public class WordStats
    {
        public string Word;
        public long Occurences;
        public IList<long> SentenceNumbers;

        public WordStats(string word)
        {
            Word = word;
            Occurences = 0;
            SentenceNumbers = new List<long>();
        }
    }
}
