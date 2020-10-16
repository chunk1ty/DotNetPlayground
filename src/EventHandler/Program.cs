using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventHandler
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            EventBus eventBus = new EventBus();
            Task.Run(() => SendMessages(eventBus));

            Streamer streamer = new Streamer(new Listener(eventBus));
            streamer.CatStream(1);
            streamer.DogStream(1);
            
            Streamer streamer1 = new Streamer(new Listener(eventBus));
            streamer1.CatStream(1);
            streamer1.DogStream(1);
            
            Streamer streamer2 = new Streamer(new Listener(eventBus));
            streamer2.CatStream(2);
            streamer2.DogStream(2);

            Console.ReadKey();
        }

        private static async Task SendMessages(EventBus eventBus)
        {
            Random random = new Random();
            
            while (true)
            {
                var number = random.Next();
                if (number % 2 == 0)
                {
                    eventBus.OnCatResponseReceived(new CatResponse(number));
                }
                else
                {
                    eventBus.OnDogResponseReceived(new DogResponse(number));
                }

                
                await Task.Delay(1000);
            }
        }
    }

    public class Streamer
    {
        private readonly Listener _listener;

        public Streamer(Listener listener)
        {
            _listener = listener;
        }

        public void CatStream(int connectionId)
        {
            _listener.Listen(connectionId);
            _listener._connectionHandler[connectionId].CatResponseEventHandler += (sender, response) =>  Console.WriteLine($"Received message {response} on connection {connectionId}");;
        }
        
        public void DogStream(int connectionId)
        {
             _listener.Listen(connectionId);
             _listener._connectionHandler[connectionId].DogResponseEventHandler += (sender, response) =>  Console.WriteLine($"Received message {response} on connection {connectionId}");
        }
    }

    public class Listener
    {
        public Dictionary<int, MyEventHandler> _connectionHandler = new Dictionary<int, MyEventHandler>();

        private readonly EventBus _eventBus;

        public Listener(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Listen(int connectionId)
        {
            if (_connectionHandler.ContainsKey(connectionId))
            {
                return;
            }
            
            _connectionHandler.Add(connectionId, new MyEventHandler());
                
            _eventBus.SubscribeCatResponse((sender, response) => _connectionHandler[connectionId].CatResponseEventHandler?.Invoke(this, response));
            _eventBus.SubscribeDogResponse((sender, response) => _connectionHandler[connectionId].DogResponseEventHandler?.Invoke(this, response));
        }
    }

    public class EventBus
    {
        private EventHandler<CatResponse> _catResponseEventHandler;
        private EventHandler<DogResponse> _dogResponseEventHandler;

        public void OnCatResponseReceived(CatResponse catResponse)
        {
            _catResponseEventHandler?.Invoke(this, catResponse);
        }
        
        public void OnDogResponseReceived(DogResponse catResponse)
        {
            _dogResponseEventHandler?.Invoke(this, catResponse);
        }

        public void SubscribeCatResponse(EventHandler<CatResponse> eventHandler)
        {
            _catResponseEventHandler += eventHandler;
        }
        
        public void SubscribeDogResponse(EventHandler<DogResponse> eventHandler)
        {
            _dogResponseEventHandler += eventHandler;
        }
    }

    public class MyEventHandler
    {
        public EventHandler<CatResponse> CatResponseEventHandler;

        public EventHandler<DogResponse> DogResponseEventHandler;
    }

    public class CatResponse
    {
        private readonly int _number;
        
        public CatResponse(int number)
        {
            _number = number;
        }
        
        public override string ToString()
        {
            return $"Cat: {_number}";
        }
    }

    public class DogResponse
    {
        private readonly int _number;
        
        public DogResponse(int number)
        {
            _number = number;
        }
        
        public override string ToString()
        {
            return $"Dog: {_number}";
        }
    }
}