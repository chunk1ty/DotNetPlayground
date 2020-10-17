using System;

namespace EventHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            MyEventBus myEventBus = new MyEventBus();

            // attach event handler
            myEventBus.MessageReceived += (sender, messageArgs) => Console.WriteLine($"Process message: {messageArgs}.");
            myEventBus.SubscribeForMyMessage();
            
            // myEventBus.SubscribeAction((x => Console.WriteLine($"Process message: {x}")));

            Console.ReadKey();
        }
    }

}