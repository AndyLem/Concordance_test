namespace Concordance.Core.Interfaces
{
    public interface IConfigProvider
    {
        /// <summary>
        /// A line of symbols used to separate words in a single sentence
        /// </summary>
        string WordDelimiters { get; }

        /// <summary>
        /// Specifies wether the output should be sorted ascending or descending
        /// </summary>
        bool SortAsc { get; }

        /// <summary>
        /// Allows to start sentence numbers in statistics not only from zero 
        /// </summary>
        long SentenceNumberStartsFrom { get; }
    }
}
