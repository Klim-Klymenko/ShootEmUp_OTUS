using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace GameSystem
{
    [UsedImplicitly]
    public sealed class CharacterStats
    {
        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;
        
        private readonly HashSet<CharacterStat> _stats = new();

        public void AddStat(CharacterStat stat)
        {
            if (_stats.Add(stat))
                OnStatAdded?.Invoke(stat);
        }

        public void RemoveStat(CharacterStat stat)
        {
            if (_stats.Remove(stat)) 
                OnStatRemoved?.Invoke(stat);
        }

        public CharacterStat GetStat(string name)
        {
            foreach (var stat in _stats)
            {
                if (stat.Name == name) 
                    return stat;
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public CharacterStat[] GetStats() => _stats.ToArray();
    }
}