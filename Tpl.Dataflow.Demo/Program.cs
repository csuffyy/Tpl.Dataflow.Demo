using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Tpl.Dataflow.Demo
{
    class Program
    {
        private static readonly BufferBlock<int> bufferBlock = new BufferBlock<int>();
        private static readonly Random random = new Random();

        private static void Producer()
        {
            while (true)
            {
                int item = Produce();
                bufferBlock.Post(item);
            }
        }

        private static void Consumer()
        {
            while (true)
            {
                int item = bufferBlock.Receive();
                Process(item);
            }
        }

        private static int Produce()
        {
            return random.Next(00, 10000);
        }

        private static void Process(int item)
        {
            Console.WriteLine($"项：{item}");
        }

        static void Main()
        {
            var p = Task.Factory.StartNew(Producer);
            var c = Task.Factory.StartNew(Consumer);
            Task.WaitAll(p, c);
        }
    }
}
