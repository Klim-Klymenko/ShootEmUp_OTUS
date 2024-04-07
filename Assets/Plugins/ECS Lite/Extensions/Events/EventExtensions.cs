using Leopotam.EcsLite;

namespace ECSLite.Extensions.Events
{
    public static class EventExtensions
    {
        public static IEcsSystems OneFrame<T> (this IEcsSystems systems, string worldName = null) where T : struct
        {
            if (systems.GetWorld(worldName) == null)
                throw new System.Exception ($"Requested world \"{(string.IsNullOrEmpty (worldName) ? "[default]" : worldName)}\" not found.");

            return systems.Add(new OneFrameSystem<T>(systems.GetWorld(worldName)));
        }
    }
}