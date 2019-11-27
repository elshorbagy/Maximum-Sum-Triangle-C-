using Microsoft.Extensions.DependencyInjection;
using System;
using TriAngleUi.Models;

namespace TriAngleUi
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var summation = RegisterService(args);
                var result = summation.GetSummation();

                Console.WriteLine("Max Sum for the Triangle : {0}", result.Split('|')[0]);
                Console.WriteLine("Path                     : {0}", result.Split('|')[1]);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }

        private static ISummation RegisterService(string[] args)
        {
            var service = DependencyBuilding.Build(args[0]);
            var summation = service
                .GetService<ISummation>();
            return summation;
        }
    }
}