using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddleMast
{
    public static class ListExtensions
    {
        private static readonly Random random = new Random(DateTime.Now.Second);

        public static void Shuffle<T>(this List<T> list, Random random)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int other = random.Next(0, list.Count);
                (list[i], list[other]) = (list[other], list[i]);
            }
        }

        public static void Shuffle<T>(this List<T> list)
        {
            list.Shuffle(random);
        }

        public static void RemoveCollection<T>(this List<T> list, IReadOnlyCollection<T> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                T itemToRemove = collection.ElementAt(i);
                list.Remove(itemToRemove);
            }
        }
    }
}
