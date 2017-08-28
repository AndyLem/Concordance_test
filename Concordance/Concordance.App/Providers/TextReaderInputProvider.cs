using System.IO;
using Concordance.App.Entities;
using Concordance.App.Interfaces;

namespace Concordance.App.Providers
{
    /// <summary>
    /// Allows to enter an input using a console / file etc. 
    /// </summary>
    class TextReaderInputProvider : IInputProvider
    {
        private readonly TextReader _reader;
        private readonly TextWriter _writer;

        public TextReaderInputProvider(TextReader reader, TextWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }


        public string GetInput()
        {
            _writer?.WriteLine("Please enter the text:");
            return _reader.ReadLine();
        }

        public InputProviderSource SupportedSource => InputProviderSource.TextReader;
    }
}