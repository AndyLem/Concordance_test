﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Concordance.Core.Entities;
using Concordance.Core.Interfaces;

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

            //var sentences = input.Split(_configProvider.SentenceDelimiters, StringSplitOptions.None);
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
            // uncomment following line to count multiple appearances in a single sentence as one 
            // 
            //if (!stats.SentenceNumbers.Contains(sentenceIndex)) 
            stats.SentenceNumbers.Add(sentenceIndex);
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