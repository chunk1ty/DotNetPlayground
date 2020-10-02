using System;

namespace MyIoCContainer.Services
{
    public interface IRepository
    {
    }
    
    public class Repository : IRepository
    {
        public Repository()
        {
            Console.WriteLine("Repository constructor!");
        }
    }
}