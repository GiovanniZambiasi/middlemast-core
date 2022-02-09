using UnityEngine;

namespace MiddleMast
{
    public static class RandomExtensions
    {
        public static byte Range(byte min, byte max)
        {
            int value = Random.Range(min, max);

            return (byte)value;
        }
    }
}
