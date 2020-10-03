using System;
using IDisposable.Before;

namespace IDisposable
{
    class Program
    {
        private static DatabaseState _DatabaseState;
        
        static void Main(string[] args)
        {
            Console.WriteLine("'g' to Get Date; 'gc' to Garbage Collect; 'x' to Exit");
            var command = "";
            while (command != "x")
            {
                command = Console.ReadLine();
                switch (command)
                {
                    case "g":
                        GetDate();
                        break;

                    case "gc":
                        GC.Collect();
                        break;
                }
            }
        }
        
        private static void GetDate()
        {
            // Before
            // if (_DatabaseState == null)
            // {
            //     _DatabaseState = new DatabaseState();
            // }
            // Console.WriteLine(_DatabaseState.GetDate());
            
            // After
            // using (var databaseState = new DatabaseState())
            //     //var databaseState = new DatabaseState();
            // {
            //     Console.WriteLine(databaseState.GetDate());
            // }
        }
    }
}