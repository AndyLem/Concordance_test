using Concordance.App.Entities;
using Concordance.App.Interfaces;

namespace Concordance.App.Providers
{
    /// <summary>
    /// Provides an input from the task description
    /// </summary>
    class DemoInputProvider : IInputProvider
    {
        public string GetInput()
        {
            return
                @"Given an arbitrary text document written in English, write a program that will generate a concordance, i.e. an alphabetical list of all word occurrences, labeled with word frequencies. Bonus: label each word with the sentence numbers in which each occurrence appeared.";
        }

        public InputProviderSource SupportedSource => InputProviderSource.Demo;
    }
}