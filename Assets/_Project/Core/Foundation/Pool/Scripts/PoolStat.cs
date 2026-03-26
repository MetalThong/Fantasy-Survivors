using System;

namespace Core.Foundation.Pool
{
    public class PoolStats
    {
        public Type Type { get; set; }

        public int MaxActive { get; set; }
        public int CurrentActive { get; set; }
        public int CurrentAvailable { get; set; }

        public int CreatedCount { get; set; }
        public int GetCount { get; set; }
        public int ReleaseCount { get; set; }

        public PoolStats(Type type)
        {
            Type = type;

            MaxActive = 0;
            CurrentActive = 0;
            CurrentAvailable = 0;

            GetCount = 0;
            ReleaseCount = 0;
        }
    }
}