using System;

namespace EventHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            MyEventBus myEventBus = new MyEventBus();
            // myEventBus.MessageHandler += (sender, messageArgs) => Console.WriteLine($"Process message: {messageArgs}.");
            // myEventBus.SubscribeEventHandler();
            
            myEventBus.SubscribeAction((x => Console.WriteLine($"Process message: {x}")));

            Console.ReadKey();
        }
    }

}