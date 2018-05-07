using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employees.App
{
    class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        internal void Run()
        {
            while (true)
            {
                string input = Console.ReadLine();

                string[] commandTokens = input.Split();

                string commandName = commandTokens[0];
                string[] commandArgs = commandTokens.Skip(1).ToArray();
            }
        }
    }
}
