using System;
using System.Threading.Tasks;

namespace EventHandler
{
    public class MessageArgs : EventArgs
    {
        public MessageArgs(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public override string ToString()
        {
            return $"Message {{Id: {Id}}}";
        }
    }

    public class MyEventBus
    {
        public event EventHandler<MessageArgs> MessageHandler;

        public void SubscribeEventHandler()
        {
            Task.Run(async () => await PublishMessages());   
        }
        
        private async Task PublishMessages()
        {
            Random random = new Random();

            while (true)
            {
                var message = new MessageArgs(random.Next());

                Console.WriteLine(message);
                MessageHandler?.Invoke(this, message);

                await Task.Delay(2000);
            }
        }
        
        public void SubscribeAction(Action<MessageArgs> action)
        {
            Task.Run(async () => await PublishMessages1(action));   
        }
        
        private async Task PublishMessages1(Action<MessageArgs> action)
        {
            Random random = new Random();

            while (true)
            {
                var message = new MessageArgs(random.Next());

                Console.WriteLine(message);
                action(message);

                await Task.Delay(2000);
            }
        }
    }
}