using AuroraNative.WebSockets;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(new EventHook());
            client.Create();

            while (true)
            {
                string Command = Console.ReadLine();
                //TODO 关于指令预判 - Console.WriteLine(LevenshteinDistance.Instance.LevenshteinDistancePercent(Command, "!setid") * 100);
                Console.ReadKey();
            }
        }
    }
}
