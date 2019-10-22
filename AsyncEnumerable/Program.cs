using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncEnumerable
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*await foreach (var item in GenerateNumbersAsync())
            {
                System.Console.WriteLine(item);
            }

            foreach(var item in GenerateNumbers())
            {
                System.Console.WriteLine(item);
            }*/

            await foreach (var item in GenerateStingsAsync())
            {
                System.Console.WriteLine(item);
            }

            static async IAsyncEnumerable<string> GenerateStingsAsync()
            {
                string[] names = new [] {"James", "Raj", "Andrews", "Arun", "Aravinda", "Viki", "Mathi"};
                foreach (var item in names)
                {
                    await Task.Delay(5);
                    yield return item;
                }
            }

            static async IAsyncEnumerable<int> GenerateNumbersAsync()
            {                
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(10);
                    yield return i;
                }
            }
            
            /*
             If we async keywork below the method it throws following error

             error CS1983: The return type of an async method must be void, Task, Task<T>, 
             a task-like type, IAsyncEnumerable<T>, or IAsyncEnumerator<T>
             */
            static IEnumerable<int> GenerateNumbers()
            {
                for (int i = 0; i < 10; i++)
                {                                       
                    yield return i;
                }
            }
        }
    }
}
