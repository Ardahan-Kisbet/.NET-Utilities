using System;

namespace CSharpUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Helpers.RunningObjectTableHelper.ROTHelper.GetIDEInstances(true, Helpers.RunningObjectTableHelper.ROTHelper.IDE.VisualStudio);
            
        }
    }
}
