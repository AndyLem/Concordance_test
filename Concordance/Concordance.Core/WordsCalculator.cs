using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Concordance.Core.Interfaces;
using Concordance.Core.Model;

namespace Concordance.Core
{
    public class WordsCalculator : IWordsCalculator
    {
        private readonly IConfigProvider _configProvider;

        public WordsCalculator(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }


        public IEnumerable<WordStats> Run(string input)
        {
            if (null == input) throw new ArgumentNullException(nameof(input));
            
            var dict = new Dictionary<string, WordStats>();

            var sentences = SplitBySentences(input).ToList();

            for (var sentenceIndex = 0; sentenceIndex < sentences.Count; sentenceIndex++)
            {
                var words = sentences[sentenceIndex].Split(_configProvider.WordDelimiters.ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    var lowercaseWord = word.ToLower();
                    var stats = dict.ContainsKey(lowercaseWord) ? dict[lowercaseWord] : new WordStats(lowercaseWord);
                    UpdateStats(stats, sentenceIndex);
                    dict[lowercaseWord] = stats;
                }
            }

            var sortedWords = _configProvider.SortAsc
                ? dict.Keys.OrderBy(x => x)
                : dict.Keys.OrderByDescending(x => x);

            return sortedWords.Select(word => dict[word]).ToList();
        }

        private void UpdateStats(WordStats stats, int sentenceIndex)
        {
            stats.Occurences++;
            var indexToStore = sentenceIndex + _configProvider.SentenceNumberStartsFrom;

            // uncomment next line of code to count multiple appearances in a single sentence as one 
            // this will affect following Unit Tests:
            // * TestMultipleOccurencesInOneSentence (should pass if the line is commented, or fail otherwise)
            // * TestMultipleOccurencesInOneSentence_MergedIntoOne (should pass if the line is UNcommented, or fail otherwise)

            //if (!stats.SentenceNumbers.Contains(indexToStore)) 
            stats.SentenceNumbers.Add(indexToStore);
        }

        private IEnumerable<string> SplitBySentences(string input)
        { 
            var regex = new Regex(@"(?<!\w\.\w.)(?<![A-Z][a-z]\.)(?<=\.|\?|\!)\s");
            var sentences = regex.Split(input);
            foreach (var sentence in sentences)
            {
                yield return sentence.TrimEnd('.', '?', '!');
            }

        }
    }
}