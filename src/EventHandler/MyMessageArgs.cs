using System;

namespace EventHandler
{
    // event parameters
    public class MyMessageArgs : EventArgs
    {
        public MyMessageArgs(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public override string ToString()
        {
            return $"Message {{Id: {Id}}}";
        }
    }
}