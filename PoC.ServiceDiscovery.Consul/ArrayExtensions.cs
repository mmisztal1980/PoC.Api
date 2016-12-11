using System;

namespace PoC.ServiceDiscovery.Consul
{
    public static class ArrayExtensions
    {
        private static readonly int seed = DateTime.UtcNow.Millisecond;

        public static T Random<T>(this T[] items)
        {
            var random = new Random(seed);

            return items[random.Next(items.Length)];
        }
    }
}
