using System;
using System.Threading.Tasks;

namespace EventHandler
{
    public class MyEventBus
    {
        // define event (full declaration)
        // private EventHandler<MyMessageArgs> _messageReceived;
        //
        // public event EventHandler<MyMessageArgs> MessageReceived
        // {
        //     add => _messageReceived += value;
        //     remove => _messageReceived -= value;
        // }

        // define event (short declaration)
        public event EventHandler<MyMessageArgs> MessageReceived;
        
        public void SubscribeForMyMessage()
        {
            Task.Run(async () => await PublishMessages());   
        }
        
        private async Task PublishMessages()
        {
            Random random = new Random();

            while (true)
            {
                var message = new MyMessageArgs(random.Next());

                Console.WriteLine(message);
                // raise event
                MessageReceived?.Invoke(this, message);

                await Task.Delay(2000);
            }
        }
        
        public void SubscribeAction(Action<MyMessageArgs> action)
        {
            Task.Run(async () => await PublishMessages1(action));   
        }
        
        private async Task PublishMessages1(Action<MyMessageArgs> action)
        {
            Random random = new Random();

            while (true)
            {
                var message = new MyMessageArgs(random.Next());

                Console.WriteLine(message);
                action(message);

                await Task.Delay(2000);
            }
        }
    }
}