using System;
using System.Collections.Generic;
using System.Linq;
using Concordance.Core;
using Concordance.Core.Interfaces;
using Concordance.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Concordance.Tests
{
    [TestClass]
    public class ConcordanceTests
    {
        protected IWordsCalculator Subject;
        protected IEnumerable<WordStats> Results;
        protected Mock<IConfigProvider> ConfigProviderMock;

        [TestInitialize]
        public void TestInit()
        {
            ConfigProviderMock = new Mock<IConfigProvider>();
            ConfigProviderMock.Setup(x => x.WordDelimiters).Returns("\r\n, ");
            ConfigProviderMock.Setup(x => x.SortAsc).Returns(true);
            ConfigProviderMock.Setup(x => x.SentenceNumberStartsFrom).Returns(0);

            Subject = new WordsCalculator(ConfigProviderMock.Object);
            Results = null;
        }

        [TestMethod]
        public void TestEmptyInput()
        {
            Results = Subject.Run(string.Empty);
            Assert.IsNotNull(Results);
            Assert.AreEqual(false, Results.Any());
        }

        [TestMethod]
        public void TestMeaninglessInput()
        {
            Results = Subject.Run("  , . \r\n   . ");
            Assert.AreEqual(false, Results.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Null input should throw an exception")]
        public void TestNullInput()
        {
            Results = Subject.Run(null);
        }

        [TestMethod]
        public void TestSingleSentenceInput()
        {
            Results = Subject.Run("Hello, this is my first test");
            var arrayOfWords = Results.ToArray();
            Assert.AreEqual(6, arrayOfWords.Length);
            var firstWordStars = arrayOfWords.First(x => x.Word == "first");
            Assert.IsNotNull(firstWordStars);
            Assert.AreEqual(1, firstWordStars.Occurences);
            Assert.AreEqual(1, firstWordStars.SentenceNumbers.Count);
            Assert.AreEqual(0, firstWordStars.SentenceNumbers[0]);
        }

        [TestMethod]
        public void TestTwoSentencesInput()
        {
            Results = Subject.Run("Hello, this is my second test. This test contains two sentences");
            var arrayOfWords = Results.ToArray();
            Assert.AreEqual(9, arrayOfWords.Length);
            var testWordStats = arrayOfWords.First(x => x.Word == "test");
            Assert.IsNotNull(testWordStats);
            Assert.AreEqual(2, testWordStats.Occurences);
            CollectionAssert.AreEqual(new long[] {0, 1}, testWordStats.SentenceNumbers.ToArray());
        }

        [TestMethod]
        public void TestSortingAsc()
        {
            ConfigProviderMock.Setup(x => x.SortAsc).Returns(true);
            Results = Subject.Run("fff, bbb, aaa, ccc, eee, aaa, fff, ddd");
            CollectionAssert.AreEqual(new[] {"aaa", "bbb", "ccc", "ddd", "eee", "fff"},
                Results.Select(x => x.Word).ToArray());
        }


        [TestMethod]
        public void TestSortingDesc()
        {
            ConfigProviderMock.Setup(x => x.SortAsc).Returns(false);
            Results = Subject.Run("fff, bbb, aaa, ccc, eee, aaa, fff, ddd");
            CollectionAssert.AreEqual(new[] { "fff", "eee", "ddd", "ccc", "bbb", "aaa" },
                Results.Select(x => x.Word).ToArray());
        }

        [TestMethod]
        public void TestMultipleOccurencesInOneSentence()
        {
            Results = Subject.Run("A word word appears twice in this sentence");
            CollectionAssert.AreEqual(new long[]{0, 0}, Results.First(x => x.Word == "word").SentenceNumbers.ToArray());
        }

        [TestMethod]
        public void TestMultipleOccurencesInOneSentence_MergedIntoOne()
        {
            // This test should fail. It depends on requirements interpretation and should be used only if multiple 
            // occurances of a word in one sentence are merged into one sentence number.
            Results = Subject.Run("A word word appears twice in this sentence but should be count as one");
            CollectionAssert.AreEqual(new long[] { 0 }, Results.First(x => x.Word == "word").SentenceNumbers.ToArray());
        }

        [TestMethod]
        public void TestShorteningsAsSingleWords()
        {
            Results = Subject.Run("Test that shortenings i.e. are single words. They should not break sentences");
            var arrayOfWords = Results.ToList();
            var ieStats = arrayOfWords.SingleOrDefault(x => x.Word == "i.e.");
            Assert.IsNotNull(ieStats);

            // checking that the word 'word is in the first sentence
            Assert.AreEqual(0, arrayOfWords.Single(x => x.Word == "words").SentenceNumbers[0]);
        }

        [TestMethod]
        public void TestLoremIpsum()
        {
            var loremIpsum = @"
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque egestas mi arcu, vel mattis arcu lobortis ut. Cras non venenatis velit. Aenean sollicitudin porta ex nec pretium. Fusce magna ante, pretium eu sagittis vel, pulvinar in lacus. Pellentesque interdum id ex vitae dapibus. Nam dignissim sit amet mauris sed facilisis. Aenean suscipit rhoncus finibus. Nunc enim felis, congue mollis diam in, convallis euismod quam. Donec enim ante, tincidunt nec porttitor ac, congue congue enim.

Sed varius pharetra ligula in porttitor. Sed magna arcu, aliquet quis pharetra vitae, tincidunt vitae nisi. Maecenas vehicula, magna at tristique malesuada, nisl velit ornare quam, nec hendrerit ipsum risus semper erat. Nam egestas ante orci. Nullam mollis, mi id porttitor faucibus, metus odio aliquet nibh, ut laoreet arcu diam at ex. Mauris et ex nec felis volutpat sollicitudin in non ante. Vivamus eu lacinia magna, ut ultricies dui. Etiam vitae felis vitae velit pharetra tempus. Nam sodales lacus sit amet eleifend vestibulum. Nunc lobortis massa rhoncus sapien pulvinar elementum a lobortis quam. Pellentesque nec sapien non arcu tincidunt pellentesque. Praesent ut sem vel turpis tincidunt facilisis. Sed vel nulla bibendum, aliquet ligula eget, molestie libero. Vestibulum facilisis quam arcu, at mollis urna vulputate eu. Duis fringilla dui a diam placerat, nec molestie tellus ullamcorper. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.

Fusce vehicula, lacus eu mollis hendrerit, purus neque maximus nibh, non aliquam ante purus non nisl. Morbi in dolor rutrum, vehicula est id, eleifend enim. Morbi ultrices tempus tortor quis lobortis. Etiam urna leo, aliquet id dapibus vitae, finibus quis lorem. Nunc convallis augue sit amet lorem rhoncus, nec lacinia nisi euismod. Pellentesque fringilla volutpat eleifend. Donec et rhoncus purus, ac scelerisque nibh. Vestibulum condimentum tortor a metus consequat, vitae gravida justo vulputate. Suspendisse pellentesque, nibh vitae elementum varius, justo diam pulvinar quam, at volutpat libero lectus vitae neque. Donec congue, nibh vestibulum pretium ultricies, neque lorem sodales urna, quis iaculis ligula lorem ac ante. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Quisque blandit quam vel magna luctus malesuada. In a dolor ex. Vestibulum ut feugiat nunc, ac imperdiet lacus. Suspendisse potenti.

In quis commodo odio. Sed lacinia tincidunt hendrerit. Suspendisse ligula velit, aliquam eget nunc sed, placerat convallis nulla. Donec semper sollicitudin tortor nec ultricies. Aliquam et ante vitae elit facilisis vehicula vel in urna. Curabitur pellentesque sollicitudin justo, sed aliquet enim bibendum vitae. In hac habitasse platea dictumst. Sed fringilla dui in consequat ullamcorper.

Ut sit amet mi id ante posuere vehicula. Nunc accumsan libero quis lacus euismod, a congue eros viverra. Pellentesque facilisis felis sed urna dapibus sagittis. Suspendisse nibh magna, faucibus vitae lacus at, molestie rhoncus lacus. Quisque dapibus enim a odio luctus accumsan. Aliquam ligula justo, tempor at elementum sit amet, dapibus quis ligula. Vestibulum efficitur elementum accumsan.
";

            Results = Subject.Run(loremIpsum);
            // Assert that the test with a relatively large text just wenk ok, it's quite hard to create meaningful checks here
        }
    }
}
