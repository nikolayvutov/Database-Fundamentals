using System;
using System.Threading;
using EmployeesMapping.Core.Contracts;

namespace EmployeesMapping.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Console.WriteLine("Program will close after: ");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
            
            Environment.Exit(0);

            return null;
        }
    }
}