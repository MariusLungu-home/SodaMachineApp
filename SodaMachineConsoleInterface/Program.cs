using Microsoft.Extensions.Configuration;
using SodaMachineLibrary;


namespace SodaMachineConsoleInterface
{
    internal class Program
    {
        private IConfiguration _config;
        public Program(IConfiguration config)
        {
            _config = config;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}