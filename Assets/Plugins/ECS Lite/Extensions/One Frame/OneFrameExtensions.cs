using Leopotam.EcsLite;

namespace ECSLite.Extensions.Events
{
    public static class OneFrameExtensions
    {
        public static IEcsSystems OneFrame<T> (this IEcsSystems systems, string worldName = null) 
            where T : struct
        {
            if (systems.GetWorld(worldName) == null)
                throw new System.Exception ($"Requested world \"{(string.IsNullOrEmpty (worldName) ? "[default]" : worldName)}\" not found.");

            return systems.Add(new OneFrameSystem<T>(systems.GetWorld(worldName)));
        }
        
        public static IEcsSystems DelHereSpecific<T, T1> (this IEcsSystems systems, string worldName = null)
            where T : struct
            where T1 : struct
        {
            if (systems.GetWorld(worldName) == null)
                throw new System.Exception ($"Requested world \"{(string.IsNullOrEmpty (worldName) ? "[default]" : worldName)}\" not found.");

            return systems.Add(new DelHereSpecificSystem<T, T1>(systems.GetWorld(worldName)));
        }
        
        public static IEcsSystems AddHere<T> (this IEcsSystems systems, string worldName = null)
            where T : struct
        {
            if (systems.GetWorld(worldName) == null)
                throw new System.Exception ($"Requested world \"{(string.IsNullOrEmpty (worldName) ? "[default]" : worldName)}\" not found.");

            return systems.Add(new AddHere<T>(systems.GetWorld(worldName)));
        }
        
        public static IEcsSystems AddHereSpecific<T, T1> (this IEcsSystems systems, string worldName = null)
            where T : struct
            where T1 : struct
        {
            if (systems.GetWorld(worldName) == null)
                throw new System.Exception ($"Requested world \"{(string.IsNullOrEmpty (worldName) ? "[default]" : worldName)}\" not found.");

            return systems.Add(new AddHereSpecific<T, T1>(systems.GetWorld(worldName)));
        }
    }
}