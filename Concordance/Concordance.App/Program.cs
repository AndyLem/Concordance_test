using System;
using Autofac;
using Concordance.App.Entities;
using Concordance.App.Interfaces;
using Concordance.Core.Interfaces;

namespace Concordance.App
{
    class Program
    {
        private const string DefaultSelection = "2";
        private const int QuitValue = 0;

        static void Main(string[] args)
        {
            var container = Containers.ContainerBuilder.BuildContainer();

            IInputProvider inputProvider = null;
            InputProviderSource source = InputProviderSource.Demo;
            var inputProviderFactory = container.Resolve<IInputProviderFactory>();

            do
            {
                Console.WriteLine("Please specify the source or input:");
                Console.WriteLine("1: Console");
                Console.WriteLine("2: Demo");
                Console.WriteLine("----------");
                Console.WriteLine("0: Quit");

                var selection = Console.ReadLine();

                try
                {
                    var index = int.Parse(selection ?? DefaultSelection);
                    if (index == QuitValue) return; // exiting from the application
                    source = (InputProviderSource) index;
                    inputProvider = inputProviderFactory.GetInputProvider(source);
                    if (inputProvider == null)
                    {
                        throw new IndexOutOfRangeException("Invalid input");
                    }
                }
                catch (Exception ex) when (ex is FormatException || ex is IndexOutOfRangeException)
                {
                    Console.WriteLine($"Error {ex.Message}. Please enter a number 1, 2 or 0 to quit");
                }
            } while (inputProvider == null);

            var input = inputProvider.GetInput();

            if (source == InputProviderSource.Demo && input != null)
                Console.WriteLine(input);

            var calculator = container.Resolve<IWordsCalculator>();

            var results = calculator.Run(input);
            var outputBuilder = container.Resolve<IOutputBuilder>();
            outputBuilder.BuildOutput(results, Console.Out);

            Console.WriteLine();
            Console.WriteLine("Done. Press any key to exit");
            Console.ReadKey();
        }


    }
}
