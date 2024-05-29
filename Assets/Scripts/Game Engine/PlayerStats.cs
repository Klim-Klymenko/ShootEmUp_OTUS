using System.Collections.Generic;
using JetBrains.Annotations;

namespace GameEngine
{
    [UsedImplicitly]
    public sealed class PlayerStats
    {
        private readonly Dictionary<string, int> _stats = new();

        public void AddStat(string name, int value)
        {
            _stats.Add(name, value);
        }

        public void ChangeStat(string name, int value)
        {
            _stats[name] = value;
        }
        
        public void RemoveStat(string name)
        {
            _stats.Remove(name);
        }

        public int GetStat(string name)
        {
            return _stats[name];
        }

        public IReadOnlyDictionary<string, int> GetStats()
        {
            return _stats;
        }
    }
}