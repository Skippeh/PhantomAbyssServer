using System;
using System.Collections.Generic;

namespace PhantomAbyssServer.Extensions
{
    public static class RandomExtensions
    {
        private static readonly Random Random;
        
        static RandomExtensions()
        {
            Random = new();
        }
        
        public static T GetRandomItem<T>(this IList<T> collection) where T : class
        {
            if (collection.Count == 0)
                return null;
            
            int index = Random.Next(collection.Count);
            return collection[index];
        }
    }
}