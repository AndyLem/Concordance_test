using System.Collections.Generic;

namespace Concordance.Core.Model
{
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
