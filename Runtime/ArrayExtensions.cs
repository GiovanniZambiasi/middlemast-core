using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddleMast
{
    public static class ArrayExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> list, T value)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                T element = list.ElementAt(i);

                if (element.Equals(value))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        ///     Used for iterating over a collection in a flip flop manner, from the initial index.
        ///     For a length 6 array, would return: 0, 5, 1, 4, 2, 3
        /// </summary>
        /// <param name="initialIndex">Where to start the flip flop from</param>
        /// <param name="i">Current iteration</param>
        public static int GetIndexFlipFlop<T>(this IEnumerable<T> list, int initialIndex, int i)
        {
            if (i == 0)
            {
                return initialIndex;
            }

            int count = list.Count();
            int index = initialIndex;

            for (int iteration = 0; iteration <= i; iteration++)
            {
                bool flipFlop = iteration % 2 == 0;
                index = (index + (flipFlop ? iteration : -iteration)).Repeat(count);
            }

            return index;
        }

        public static T[] GetCopy<T>(this IEnumerable<T> array)
        {
            T[] copy = new T[array.Count()];

            for (int i = 0; i < copy.Length; i++)
            {
                copy[i] = array.ElementAt(i);
            }

            return copy;
        }

        public static T[,] GetCopy<T>(this T[,] array)
        {
            T[,] copy = new T[array.GetLength(0), array.GetLength(1)];

            for (int i = 0; i < copy.GetLength(0); i++)
            {
                for (int j = 0; j < copy.GetLength(1); j++)
                {
                    copy[i, j] = array[i, j];
                }
            }

            return copy;
        }

        /// <summary>
        ///     Returns the array randomized.
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] Shuffle<T>(this T[] array, Random random)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int j = random.Next(i, array.Length);
                (array[i], array[j]) = (array[j], array[i]);
            }

            return array;
        }

        /// <summary>
        ///     Returns a random element from the array.
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomElement<T>(this T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }
    }
}
