using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateServices
{
    class TestDelegate
    {
        public static void TestMethod(object? sender, string message)
        {
            Console.WriteLine(message);
        }
    }
}
